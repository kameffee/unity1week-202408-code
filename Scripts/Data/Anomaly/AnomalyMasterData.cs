using UnityEngine;

namespace Unity1week202408.Anomaly
{
    [CreateAssetMenu(fileName = "Anomaly_", menuName = "MasterData/AnomalyData", order = 0)]
    public class AnomalyMasterData : ScriptableObject
    {
        public AnomalyId AnomalyId => new(_anomalyId);
        public string Description => _description;

        [SerializeField]
        private int _anomalyId;

        [TextArea(1, 2)]
        [SerializeField]
        private string _description;
    }
}