using System.Threading;
using Cysharp.Threading.Tasks;
using R3;
using Unity1week.Audio;
using Unity1week202408.Anomaly;
using Unity1week202408.Calender;
using Unity1week.Extensions;
using Unity1week202408.Ending;
using Unity1week202408.Player;
using Unity1week202408.Stage;
using Unity1week202408.Title;
using UnityEngine;
using VContainer.Unity;

namespace Unity1week202408
{
    public class InGameLoop : Presenter, IInitializable, IAsyncStartable
    {
        private readonly AnomalyInitializer _anomalyInitializer;
        private readonly LotteryAnomalyUseCase _lotteryAnomalyUseCase;
        private readonly ProgressService _progressService;
        private readonly NextStagePoint _nextStagePoint;
        private readonly TeleportPlayerUseCase _teleportPlayerUseCase;
        private readonly ApplyAnomalyUseCase _applyAnomalyUseCase;
        private readonly StageTransitionUseCase _stageTransitionUseCase;
        private readonly AudioPlayer _audioPlayer;
        private readonly TerminationCalculateUseCase _terminationCalculateUseCase;
        private readonly StageArea _stageArea;
        private readonly GoalPoint _goalPoint;
        private readonly PlayerService _playerService;
        private readonly TitlePresenter _titlePresenter;
        private readonly EndingPerformPresenter _endingPerformPresenter;

        public InGameLoop(
            AnomalyInitializer anomalyInitializer,
            LotteryAnomalyUseCase lotteryAnomalyUseCase,
            ProgressService progressService,
            NextStagePoint nextStagePoint,
            TeleportPlayerUseCase teleportPlayerUseCase,
            ApplyAnomalyUseCase applyAnomalyUseCase,
            StageTransitionUseCase stageTransitionUseCase,
            AudioPlayer audioPlayer,
            TerminationCalculateUseCase terminationCalculateUseCase,
            StageArea stageArea,
            GoalPoint goalPoint,
            PlayerService playerService,
            TitlePresenter titlePresenter,
            EndingPerformPresenter endingPerformPresenter)
        {
            _anomalyInitializer = anomalyInitializer;
            _lotteryAnomalyUseCase = lotteryAnomalyUseCase;
            _progressService = progressService;
            _nextStagePoint = nextStagePoint;
            _teleportPlayerUseCase = teleportPlayerUseCase;
            _applyAnomalyUseCase = applyAnomalyUseCase;
            _stageTransitionUseCase = stageTransitionUseCase;
            _audioPlayer = audioPlayer;
            _terminationCalculateUseCase = terminationCalculateUseCase;
            _stageArea = stageArea;
            _goalPoint = goalPoint;
            _playerService = playerService;
            _titlePresenter = titlePresenter;
            _endingPerformPresenter = endingPerformPresenter;
        }

        public void Initialize()
        {
            _anomalyInitializer.Initialize();
            _progressService.SetFirst();
            _playerService.SetCurrentPositionToInitialPosition();

            _nextStagePoint.OnPlayerEnterAsObservable()
                .SubscribeAwait(async (_, cancellationToken) => await OnNextStagePoint(cancellationToken))
                .AddTo(this);

            _stageArea.OnExitPlayerAsObservable()
                .Where(direction => direction == Direction.Left)
                .Subscribe(_ => OnReturnStagePoint())
                .AddTo(this);
        }

        public async UniTask StartAsync(CancellationToken cancellation)
        {
            _audioPlayer.PlayBgm("InGame/Main", isLoop: true);

            _titlePresenter.Show();
            await _titlePresenter.OnClickStartButtonAsObservable().FirstAsync(cancellation);
            await _titlePresenter.HideAsync(cancellation);

            while (!cancellation.IsCancellationRequested)
            {
                _playerService.SetMovable(true);

                var anomalyId = _lotteryAnomalyUseCase.Lottery();
                _applyAnomalyUseCase.Apply(anomalyId);

                // ゴールするまで待つ
                await _goalPoint.OnPlayerEnterAsObservable().FirstAsync(cancellationToken: cancellation);

                _playerService.SetMovable(false);

                // ゴールしたらエンディングを再生
                await _endingPerformPresenter.PerformAsync(cancellation);
                await _endingPerformPresenter.OnClickTitleButtonAsObservable().FirstAsync(cancellation);

                await _titlePresenter.ShowAsync(cancellation);
                _endingPerformPresenter.Hide();

                // 初期位置に戻す
                _playerService.ApplyInitialPosition();
                // 進捗をリセット
                _progressService.SetFirst();

                await _titlePresenter.OnClickStartButtonAsObservable().FirstAsync(cancellation);
                await _titlePresenter.HideAsync(cancellation);
            }
        }

        private async UniTask OnNextStagePoint(CancellationToken cancellationToken)
        {
            if (_terminationCalculateUseCase.IsTermination())
            {
                // ワープさせない
                Debug.Log("Game Clear");
                return;
            }

            _stageTransitionUseCase.Next();

            await _teleportPlayerUseCase.TeleportToStartPoint(cancellationToken);
            NextStage();
        }

        private void OnReturnStagePoint()
        {
            _stageTransitionUseCase.Return();
            NextStage();
        }

        private void NextStage()
        {
            var anomalyId = _lotteryAnomalyUseCase.Lottery();
            _applyAnomalyUseCase.Apply(anomalyId);
        }
    }
}