using R3;
using TMPro;
using Unity1week202408.Anomaly;
using Unity1week202408.Calender;
using UnityEngine;
using VContainer;

namespace Unity1week202408
{
    public class InGameDebugCanvas : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _progress;

        [SerializeField]
        private TextMeshProUGUI _anomalyId;

        [SerializeField]
        private TextMeshProUGUI _description;

        [Inject]
        private ProgressService _progressService;

        [Inject]
        private Situation _situation;

        [Inject]
        private AnomalyMasterDataRepository _anomalyMasterDataRepository;

        private void Awake()
        {
            gameObject.SetActive(Debug.isDebugBuild);
        }

        [Inject]
        private void Initialize()
        {
            _progressService.Current
                .Subscribe(progress => _progress.text = progress.ToString())
                .AddTo(this);

            _situation.CurrentAnomalyId
                .Subscribe(OnUpdateAnomalyId)
                .AddTo(this);
        }

        private void OnUpdateAnomalyId(AnomalyId id)
        {
            _anomalyId.text = id.Valid ? id.ToString() : "None";
            UpdateAnomalyDescription(id);
        }

        private void UpdateAnomalyDescription(AnomalyId id)
        {
            if (id.IsEmpty)
            {
                _description.text = string.Empty;
                return;
            }

            var data = _anomalyMasterDataRepository.Get(id);
            _description.text = data.Description;
        }
    }
}