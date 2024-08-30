using System;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using LitMotion;
using LitMotion.Extensions;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace Unity1week202408.Ending
{
    public class EndingPerformView : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup _canvasGroup;

        [SerializeField]
        private Image _endingImage;

        [SerializeField]
        private CanvasGroup _thanksText;

        [SerializeField]
        private Button _titleButton;

        [SerializeField]
        private CanvasGroup _buttonCanvasGroup;

        private void Awake()
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.interactable = false;
            _thanksText.alpha = 0;
            _endingImage.color = new Color(1f, 1f, 1f, 0f);
            _buttonCanvasGroup.alpha = 0;
        }

        public async UniTask PerformAsync(CancellationToken cancellationToken)
        {
            await LMotion.Create(0f, 1f, 3f)
                .BindToCanvasGroupAlpha(_canvasGroup)
                .AddTo(this)
                .ToValueTask(cancellationToken);

            await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: cancellationToken);

            await LMotion.Create(0f, 1f, 2f)
                .BindToColorA(_endingImage)
                .AddTo(this)
                .ToValueTask(cancellationToken);

            await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: cancellationToken);

            await LMotion.Create(0f, 1f, 1f)
                .BindToCanvasGroupAlpha(_thanksText)
                .AddTo(this)
                .ToValueTask(cancellationToken);

            await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: cancellationToken);

            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.interactable = true;

            await LMotion.Create(0f, 1f, 0.2f)
                .WithEase(Ease.Linear)
                .BindToCanvasGroupAlpha(_buttonCanvasGroup)
                .AddTo(this)
                .ToValueTask(cancellationToken);
        }

        public Observable<Unit> OnClickTitleButtonAsObservable() => _titleButton.OnClickAsObservable();

        public void Hide()
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.interactable = false;
            _thanksText.alpha = 0;
            _endingImage.color = new Color(1f, 1f, 1f, 0f);
            _buttonCanvasGroup.alpha = 0;
        }
    }
}