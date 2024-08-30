using System.Collections.Generic;
using System.Linq;
using RandomExtensions;
using RandomExtensions.Linq;
using Unity1week202408.Anomaly;

namespace Unity1week202408
{
    public class AnomalyLotteryCalculator
    {
        private readonly Queue<AnomalyId> _anomalyQueue = new();
        private readonly List<AnomalyId> _anomalyList = new();
        private readonly IRandom _random;

        // 異変が発生する確率
        private Percent _probabilityOfAnomaly;

        public AnomalyLotteryCalculator()
        {
            _random = RandomEx.Create();
        }

        public void Initialize(IEnumerable<AnomalyId> ids, Percent probabilityPercent)
        {
            _anomalyList.AddRange(ids);
            _probabilityOfAnomaly = probabilityPercent;
        }

        public AnomalyId Lottery()
        {
            var isAnomaly = _random.NextInt(0, 100) <= _probabilityOfAnomaly;

            if (!isAnomaly)
            {
                // 異変なし
                return AnomalyId.Empty;
            }

            if (!_anomalyQueue.Any())
            {
                // 無くなったら新たに追加
                foreach (var id in _anomalyList.Shuffle())
                {
                    _anomalyQueue.Enqueue(id);
                }
            }

            return _anomalyQueue.Dequeue();
        }
    }
}