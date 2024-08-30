using R3;
using Unity1week202408.Anomaly;
using UnityEngine;

namespace Unity1week202408
{
    public class Situation
    {
        public bool ExistAnomaly => _currentAnomalyId.Value.Valid;
        public ReadOnlyReactiveProperty<AnomalyId> CurrentAnomalyId => _currentAnomalyId;

        private readonly ReactiveProperty<AnomalyId> _currentAnomalyId = new(AnomalyId.Empty);

        public void SetAnomaly(AnomalyId id)
        {
            Debug.Log($"Set Anomaly: {id}");
            _currentAnomalyId.Value = id;
        }

        public void SetNormal()
        {
            _currentAnomalyId.Value = AnomalyId.Empty;
        }
    }
}