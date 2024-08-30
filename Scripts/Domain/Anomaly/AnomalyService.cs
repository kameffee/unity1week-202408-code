namespace Unity1week202408.Anomaly
{
    public class AnomalyService
    {
        public AnomalyId LastAnomalyId { get; private set; }

        private readonly AnomalyObjectContainer _anomalyObjectContainer;
        private readonly AnomalyRecordRepository _anomalyRecordRepository;

        public AnomalyService(
            AnomalyObjectContainer anomalyObjectContainer,
            AnomalyRecordRepository anomalyRecordRepository)
        {
            _anomalyObjectContainer = anomalyObjectContainer;
            _anomalyRecordRepository = anomalyRecordRepository;
        }

        public void Mutate(AnomalyId id)
        {
            if (id.IsEmpty) return;

            LastAnomalyId = id;
            _anomalyObjectContainer.Get(id).Mutate();
        }

        public void Default(AnomalyId id)
        {
            if (id.IsEmpty) return;
            _anomalyObjectContainer.Get(id).Default();
        }

        public void DefaultAll()
        {
            _anomalyObjectContainer.DefaultAll();
        }

        public void Record(AnomalyId id)
        {
            if (id.IsEmpty) return;

            // 異変のIDを記録する
            _anomalyRecordRepository.Record(id);
        }
    }
}