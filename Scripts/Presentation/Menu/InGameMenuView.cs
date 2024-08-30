using System.Threading;
using Cysharp.Threading.Tasks;
using LitMotion;
using LitMotion.Extensions;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Unity1week202408.Menu
{
    public class InGameMenuView : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup _canvasGroup;

        [SerializeField]
        private TextMeshProUGUI _recordAnomalyCount;

        [SerializeField]
        private Button _closeButton;

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

            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.interactable = true;
        }

        public async UniTask HideAsync(CancellationToken cancellationToken)
        {
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.interactable = false;

            await LMotion.Create(1f, 0f, 0.1f)
                .WithEase(Ease.Linear)
                .BindToCanvasGroupAlpha(_canvasGroup)
                .ToValueTask(cancellationToken);
        }

        public Observable<Unit> OnClickCloseButtonAsObservable() => _closeButton.OnClickAsObservable();

        public void ApplyViewModel(ViewModel viewModel)
        {
            SetRecordAnomalyCount(viewModel.RecordedAnomalyCount, viewModel.MaxAnomalyCount);
        }

        private void SetRecordAnomalyCount(int count, int max)
        {
            _recordAnomalyCount.text = $"{count}/{max}";
        }

        public class ViewModel
        {
            public int RecordedAnomalyCount { get; }
            public int MaxAnomalyCount { get; }
            
            public ViewModel(int recordedAnomalyCount, int maxAnomalyCount)
            {
                RecordedAnomalyCount = recordedAnomalyCount;
                MaxAnomalyCount = maxAnomalyCount;
            }
        }
    }
}