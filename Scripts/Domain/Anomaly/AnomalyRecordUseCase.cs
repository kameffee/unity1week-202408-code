namespace Unity1week202408.Anomaly
{
    public class AnomalyRecordUseCase
    {
        private readonly AnomalyRecordRepository _anomalyRecordRepository;

        public AnomalyRecordUseCase(AnomalyRecordRepository anomalyRecordRepository)
        {
            _anomalyRecordRepository = anomalyRecordRepository;
        }

        public void Record(AnomalyId id)
        {
            if (id.IsEmpty) return;

            // 異変のIDを記録する
            _anomalyRecordRepository.Record(id);
        }
    }
}