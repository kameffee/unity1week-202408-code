using System;
using Unity1week202408.Anomaly;

namespace Unity1week202408
{
    public class LotteryAnomalyUseCase
    {
        private readonly AnomalyService _anomalyService;
        private readonly AnomalyLotteryCalculator _anomalyLotteryCalculator;

        public LotteryAnomalyUseCase(
            AnomalyService anomalyService,
            AnomalyLotteryCalculator anomalyLotteryCalculator)
        {
            _anomalyService = anomalyService;
            _anomalyLotteryCalculator = anomalyLotteryCalculator;
        }

        public AnomalyId Lottery()
        {
            AnomalyId anomalyId;
            var lastAnomalyId = _anomalyService.LastAnomalyId;
            while (true)
            {
                anomalyId = _anomalyLotteryCalculator.Lottery();

                // 直前の異変と同じ異変は出さない
                if (lastAnomalyId == anomalyId)
                {
                    continue;
                }

                break;
            }

            return anomalyId;
        }
    }
}