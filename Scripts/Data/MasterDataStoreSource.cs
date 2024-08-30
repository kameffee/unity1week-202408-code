using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Unity1week202408
{
    public abstract class MasterDataStoreSource<TMasterData> : ScriptableObject where TMasterData : ScriptableObject
    {
        public TMasterData[] DataList => _dataList;
        public int Count => _dataList.Length;

        [SerializeField]
        private TMasterData[] _dataList;

        public void Validate()
        {
            _dataList = _dataList.Distinct()
                .Where(data => data != null)
                .OrderBy(data => data.name)
                .ToArray();
        }
    }
}