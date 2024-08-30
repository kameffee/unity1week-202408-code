namespace Unity1week202408.Anomaly
{
    public class AnomalyMasterDataRepository
    {
        private readonly AnomalyMasterDataStoreSource _anomalyMasterDataStoreSource;

        public AnomalyMasterDataRepository(AnomalyMasterDataStoreSource anomalyMasterDataStoreSource)
        {
            _anomalyMasterDataStoreSource = anomalyMasterDataStoreSource;
        }

        public AnomalyMasterData Get(AnomalyId anomalyId)
        {
            return _anomalyMasterDataStoreSource.Get(anomalyId);
        }

        public int Count() => _anomalyMasterDataStoreSource.Count;
    }
}