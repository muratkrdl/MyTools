using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

namespace _Scripts.GSheetDataUpdater
{
    public static class SheetUpdateHelper
    {
        public static void UpdateExistingData<TItem,TSo>(TItem[] data ,Func<TItem, string> getAssetPath,
            Action<TSo, TItem> applyRowToSo) where TItem : class where TSo : ScriptableObject
        {
            foreach (var obj in data)
            {
                var assetPath = getAssetPath(obj);
                var so = AssetDatabase.LoadAssetAtPath<TSo>(assetPath);
                
                if (!so) continue;

                applyRowToSo(so, obj);
                Debug.Log($"Updated: {so.name}");
            }

            AssetDatabase.SaveAssets();
        }
        
        public static void CreateNonExistingData<TItem,TSo>(TItem[] data ,Func<TItem, string> getAssetPath,
            Action<TSo, TItem> applyRowToSo) where TItem : class where TSo : ScriptableObject
        {
            foreach (var obj in data)
            {
                var assetPath = getAssetPath(obj);

                if (AssetDatabase.LoadAssetAtPath<TSo>(assetPath)) continue;

                Type soType;

                if (typeof(TSo).IsAbstract)
                {
                    soType = TypeCache.GetTypesDerivedFrom<TSo>()
                        .FirstOrDefault(t => t.Name == obj.ToString());
                }
                else
                {
                    soType = typeof(TSo);
                }
                
                if (soType == null || !typeof(ScriptableObject).IsAssignableFrom(soType))
                {
                    continue;
                }

                TSo so = ScriptableObject.CreateInstance(soType) as TSo;
                
                if (!so)
                {
                    Debug.LogError($"Failed to create '{soType.Name}' object");
                    continue;
                }
                
                AssetDatabase.CreateAsset(so, assetPath);
                
                applyRowToSo(so, obj);
                
                Debug.Log($"Updated: {so.name}");
            }

            AssetDatabase.Refresh();
            AssetDatabase.SaveAssets();
        }
        
        public static async Task<string> FetchSheetData(string sheetUrl)
        {
            using HttpClient client = new HttpClient();
            try
            {
                return await client.GetStringAsync(sheetUrl);
            }
            catch (Exception e)
            {
                Debug.LogWarning($"Failed to fetch sheet data: {e.Message}");
                return null;
            }
        }
        
        public static T1[] GetJsonData<T1>(string json) where T1 : class
        {
            return JsonConvert.DeserializeObject<T1[]>(json);
        }

    }
}
