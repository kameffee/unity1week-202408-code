using Unity1week202408.Anomaly;

namespace Unity1week202408
{
    public class ApplyAnomalyUseCase
    {
        private readonly Situation _situation;
        private readonly AnomalyService _anomalyService;

        public ApplyAnomalyUseCase(
            Situation situation,
            AnomalyService anomalyService)
        {
            _situation = situation;
            _anomalyService = anomalyService;
        }

        public void Apply(AnomalyId anomalyId)
        {
            _anomalyService.DefaultAll();

            _situation.SetAnomaly(anomalyId);
            _anomalyService.Mutate(anomalyId);
        }
    }
}