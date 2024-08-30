using R3;
using Unity1week.Audio;
using Unity1week.Extensions;
using VContainer.Unity;

namespace Unity1week202408.Stage
{
    public class StageAreaPresenter : Presenter, IInitializable
    {
        private readonly StageCoverView _stageCoverView;
        private readonly StageArea _stageArea;
        private readonly LightController _lightController;
        private readonly AudioPlayer _audioPlayer;

        public StageAreaPresenter(
            StageCoverView stageCoverView,
            StageArea stageArea,
            LightController lightController,
            AudioPlayer audioPlayer)
        {
            _stageCoverView = stageCoverView;
            _stageArea = stageArea;
            _lightController = lightController;
            _audioPlayer = audioPlayer;
        }

        public void Initialize()
        {
            _stageArea.StayPlayer
                .Subscribe(stay => _stageCoverView.SetActive(!stay))
                .AddTo(this);

            _stageArea.OnExitPlayerAsObservable()
                .Where(direction => direction == Direction.Left)
                .Subscribe(_ =>
                {
                    _lightController.Flash();
                    _audioPlayer.PlaySe("InGame/LightNoise");
                })
                .AddTo(this);
        }
    }
}