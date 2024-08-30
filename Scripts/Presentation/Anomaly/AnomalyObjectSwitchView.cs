using UnityEngine;

namespace Unity1week202408.Anomaly
{
    public class AnomalyObjectSwitchView : MonoBehaviour
    {
        public AnomalyId Id => _anomalyMasterData.AnomalyId;

        [SerializeField]
        private AnomalyMasterData _anomalyMasterData;

        [SerializeField]
        private GameObject _normal;

        [SerializeField]
        private GameObject _anomaly;

        public void Default()
        {
            Switch(false);
        }

        public void Mutate()
        {
            Switch(true);
        }

        private void Switch(bool isAnomaly)
        {
            if (_normal != null)
                _normal.SetActive(!isAnomaly);

            if (_anomaly != null)
                _anomaly?.SetActive(isAnomaly);
        }
    }
}