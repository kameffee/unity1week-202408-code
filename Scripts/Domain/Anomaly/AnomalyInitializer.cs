using UnityEngine;

namespace Unity1week202408.Anomaly
{
    public class AnomalyInitializer
    {
        private readonly AnomalyObjectContainer _anomalyObjectContainer;
        private readonly AnomalyLotteryCalculator _anomalyLotteryCalculator;

        public AnomalyInitializer(
            AnomalyObjectContainer anomalyObjectContainer,
            AnomalyLotteryCalculator anomalyLotteryCalculator)
        {
            _anomalyObjectContainer = anomalyObjectContainer;
            _anomalyLotteryCalculator = anomalyLotteryCalculator;
        }

        public void Initialize()
        {
            var anomalyObjects = Object.FindObjectsByType<AnomalyObjectSwitchView>(FindObjectsSortMode.None);
            _anomalyObjectContainer.AddRange(anomalyObjects);

            // 全て通常状態にする
            _anomalyObjectContainer.DefaultAll();

            // 異変登録と確率設定(60%)
            _anomalyLotteryCalculator.Initialize(_anomalyObjectContainer.Ids, probabilityPercent: 65);
        }
    }
}