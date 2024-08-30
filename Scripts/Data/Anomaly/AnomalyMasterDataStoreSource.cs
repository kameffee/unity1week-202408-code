using System.Linq;
using UnityEngine;

namespace Unity1week202408.Anomaly
{
    [CreateAssetMenu(fileName = "AnomalyMasterDataStoreSource", menuName = "MasterData/AnomalyMasterDataStoreSource", order = 0)]
    public class AnomalyMasterDataStoreSource : MasterDataStoreSource<AnomalyMasterData>
    {
        public AnomalyMasterData Get(AnomalyId id)
        {
            return DataList.First(data => data.AnomalyId == id);
        }
    }
}