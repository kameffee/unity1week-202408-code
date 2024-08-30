using System.Collections.Generic;
using UnityEngine;

namespace Unity1week202408.Anomaly
{
    public class AnomalyRecordRepository
    {
        public int Count => _anomalyIds.Count;

        private readonly HashSet<AnomalyId> _anomalyIds = new();

        public void Record(AnomalyId id)
        {
            if (id.IsEmpty) return;

            // 異変のIDを記録する
            _anomalyIds.Add(id);
            Debug.Log($"Record Anomaly: {id}");
        }
    }
}