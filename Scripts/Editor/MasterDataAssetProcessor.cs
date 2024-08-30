using UnityEditor;

namespace Unity1week202408.Editor
{
    public class MasterDataAssetProcessor : AssetPostprocessor
    {
        private static readonly AnomalyMasterDataAssetProcess _anomalyMasterDataAssetProcess = new();

        public static void OnPostprocessAllAssets(
            string[] importedAssets,
            string[] deletedAssets,
            string[] movedAssets,
            string[] movedFromAssetPaths)
        {
            _anomalyMasterDataAssetProcess.OnPostprocessAllAssets(
                importedAssets,
                deletedAssets,
                movedAssets,
                movedFromAssetPaths);
        }
    }
}