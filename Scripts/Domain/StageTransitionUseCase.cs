using Unity1week202408.Anomaly;
using Unity1week202408.Calender;
using UnityEngine;

namespace Unity1week202408
{
    public class StageTransitionUseCase
    {
        private readonly ProgressService _progressService;
        private readonly Situation _situation;
        private readonly AnomalyService _anomalyService;

        public StageTransitionUseCase(ProgressService progressService, Situation situation,
            AnomalyService anomalyService)
        {
            _progressService = progressService;
            _situation = situation;
            _anomalyService = anomalyService;
        }

        public void Next()
        {
            if (_situation.ExistAnomaly)
            {
                // 異変がある状態で次のステージに進むと0に戻る
                _progressService.Reset();
                return;
            }

            _progressService.Next();
        }

        public void Return()
        {
            if (_situation.ExistAnomaly)
            {
                _anomalyService.Record(_situation.CurrentAnomalyId.CurrentValue);

                // 異変がある状態で最終日だった場合はそのまま
                if (_progressService.IsFinal())
                {
                    return;
                }

                // 異変がある状態で戻った場合は次のステージに進む
                _progressService.Next();
                return;
            }

            // 異変がなく戻った場合は0に戻る
            _progressService.Reset();
        }
    }
}