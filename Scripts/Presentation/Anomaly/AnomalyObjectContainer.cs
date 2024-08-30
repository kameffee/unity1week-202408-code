using System.Collections.Generic;
using UnityEngine;

namespace Unity1week202408.Anomaly
{
    public class AnomalyObjectContainer
    {
        public IEnumerable<AnomalyId> Ids => _anomalySwitchViews.Keys;

        private readonly Dictionary<AnomalyId, AnomalyObjectSwitchView> _anomalySwitchViews = new();

        public void AddRange(IEnumerable<AnomalyObjectSwitchView> anomalySwitchViews)
        {
            foreach (var anomalySwitchView in anomalySwitchViews)
            {
                Add(anomalySwitchView);
            }
        }

        public AnomalyObjectSwitchView Get(AnomalyId id)
        {
            if (!_anomalySwitchViews.TryGetValue(id, out var view))
            {
                Debug.LogError($"AnomalySwitchView id:{id} is not found.");
                return null;
            }

            return view;
        }

        public void DefaultAll()
        {
            foreach (var anomalySwitchView in _anomalySwitchViews.Values)
            {
                anomalySwitchView.Default();
            }
        }

        private void Add(AnomalyObjectSwitchView anomalyObjectSwitchView)
        {
            if (_anomalySwitchViews.ContainsKey(anomalyObjectSwitchView.Id))
            {
                Debug.LogError($"AnomalySwitchView id:{anomalyObjectSwitchView.Id} is already exists.");
                return;
            }

            _anomalySwitchViews.Add(anomalyObjectSwitchView.Id, anomalyObjectSwitchView);
        }
    }
}