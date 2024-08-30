using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using LitMotion;
using LitMotion.Extensions;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace Unity1week202408.Title
{
    public class TitleView : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup _canvasGroup;

        [SerializeField]
        private Button _startButton;

        [SerializeField]
        private CanvasGroup _white;

        public Observable<Unit> OnClickStartButtonAsObservable() => _startButton.OnClickAsObservable();

        private void Awake()
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.interactable = false;
            _white.alpha = 0;
        }

        public void ShowFast()
        {
            _canvasGroup.alpha = 1;
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.interactable = true;
        }

        public async UniTask ShowAsync(CancellationToken cancellationToken)
        {
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.interactable = true;

            await LMotion.Create(0f, 1f, 1f)
                .WithEase(Ease.Linear)
                .BindToCanvasGroupAlpha(_canvasGroup)
                .AddTo(this)
                .ToValueTask(cancellationToken);
        }

        public async UniTask HideAsync(CancellationToken cancellationToken)
        {
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.interactable = false;

            // 白フェードアウト
            await LMotion.Create(0f, 1f, 1f)
                .WithEase(Ease.Linear)
                .BindToCanvasGroupAlpha(_white)
                .AddTo(this)
                .ToValueTask(cancellationToken);
            
            _canvasGroup.alpha = 0;
            await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: cancellationToken);

            await LMotion.Create(1f, 0f, 1f)
                .WithEase(Ease.Linear)
                .BindToCanvasGroupAlpha(_white)
                .AddTo(this)
                .ToValueTask(cancellationToken);
        }
    }
}