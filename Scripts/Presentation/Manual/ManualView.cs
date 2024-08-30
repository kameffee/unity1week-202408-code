using System.Threading;
using Cysharp.Threading.Tasks;
using LitMotion;
using LitMotion.Extensions;
using UnityEngine;

namespace Unity1week202408.Manual
{
    public class ManualView : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup _canvasGroup;

        private void Awake()
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.interactable = false;
        }

        public async UniTask ShowAsync(CancellationToken cancellationToken)
        {
            await LMotion.Create(0f, 1f, 0.2f)
                .WithEase(Ease.Linear)
                .BindToCanvasGroupAlpha(_canvasGroup)
                .ToValueTask(cancellationToken);
        }

        public async UniTask HideAsync(CancellationToken cancellationToken)
        {
            await LMotion.Create(1f, 0f, 0.1f)
                .WithEase(Ease.Linear)
                .BindToCanvasGroupAlpha(_canvasGroup)
                .ToValueTask(cancellationToken);
        }
    }
}