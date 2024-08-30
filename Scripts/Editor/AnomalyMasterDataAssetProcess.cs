using System.Linq;
using NUnit.Framework;
using Unity1week202408.Anomaly;
using UnityEditor;

namespace Unity1week202408
{
    public class AnomalyMasterDataAssetProcess
    {
        private const string FolderPath = "Assets/Application/ScriptableObjects/MasterData/Anomaly/";

        private const string DataStorePath =
            "Assets/Application/ScriptableObjects/MasterData/DataStoreSource/AnomalyMasterDataStoreSource.asset";
        public void OnPostprocessAllAssets(
            string[] importedAssets,
            string[] deletedAssets,
            string[] movedAssets,
            string[] movedFromAssetPaths)
        {
            var dataStore = AssetDatabase.LoadAssetAtPath<AnomalyMasterDataStoreSource>(DataStorePath);
            Assert.IsNotNull(dataStore, $"DataStoreSource not found at {DataStorePath}");

            var dataStoreSerializedObject = new SerializedObject(dataStore);
            dataStoreSerializedObject.Update();

            var targetImportedAssets = importedAssets
                .Where(path => path.StartsWith(FolderPath))
                .ToArray();

            var targetDeletedAssets = deletedAssets
                .Where(path => path.StartsWith(FolderPath))
                .ToArray();

            var targetMovedAssets = movedAssets
                .Where(path => path.StartsWith(FolderPath))
                .ToArray();

            var targetMovedFromAssetPaths = movedFromAssetPaths
                .Where(path => path.StartsWith(FolderPath))
                .ToArray();

            if (!targetImportedAssets
                    .Concat(targetDeletedAssets)
                    .Concat(targetMovedAssets)
                    .Concat(targetMovedFromAssetPaths)
                    .Any())
            {
                // 何も変更がない場合は何もしない
                return;
            }

            // 新規で追加されたアセットをデータストアに追加する
            var assets = AssetDatabase.FindAssets($"t:{nameof(AnomalyMasterData)}", new[] { FolderPath })
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<AnomalyMasterData>)
                .ToArray();

            var monsterMasterDataArray = dataStoreSerializedObject.FindProperty("_dataList");
            monsterMasterDataArray.ClearArray();
            foreach (var masterData in assets)
            {
                monsterMasterDataArray.arraySize++;
                var arrayElement = monsterMasterDataArray.GetArrayElementAtIndex(monsterMasterDataArray.arraySize - 1);
                arrayElement.objectReferenceValue = masterData;
                arrayElement.serializedObject.ApplyModifiedProperties();
            }

            dataStore.Validate();
            dataStoreSerializedObject.ApplyModifiedProperties();
            EditorUtility.SetDirty(dataStore);
            AssetDatabase.SaveAssets();
        }
    }
}