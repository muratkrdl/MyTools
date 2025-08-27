using System;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace _Scripts.GSheetDataUpdater
{
    [InitializeOnLoad]
    public static class MyDataSheetUpdater
    {
        // URL to fetch JSON data from Google Sheets via Apps Script
        private const string SheetUrl = "https://script.google.com/macros/s/YOUR_SCRIPT_ID/exec";

        private static readonly Func<MyDataSheetRow, string> GetAssetPath;
        private static readonly Action<MyDataSo, MyDataSheetRow> ApplyRowToSo;
        
        static MyDataSheetUpdater()
        {
            Debug.Log("Creating MyData Sheet Updater");
            
            GetAssetPath = row => $"Assets/Resources/Data/MyData/{row.Name}.asset";
            ApplyRowToSo = (so, row) => so.UpdateData(row.ID, row.Name, row.Value, row.Type);
        }
        
        // [MenuItem("Tools/Data Update/Update MyData")]
        public static void UpdateMyDataValues()
        {
            Debug.Log("Updating MyData");
            _ = UpdateJokerValuesAsync();
        }

        private static async Task UpdateJokerValuesAsync()
        {
            string json = await SheetUpdateHelper.FetchSheetData(SheetUrl);
            if (!string.IsNullOrEmpty(json))
            {
                ApplyDataToScriptableObjects(json);
            }
        }
        
        private static void ApplyDataToScriptableObjects(string json)
        {
            try
            {
                MyDataSheetRow[] data = SheetUpdateHelper.GetJsonData<MyDataSheetRow>(json);
                SheetUpdateHelper.UpdateExistingData(data, GetAssetPath, ApplyRowToSo);
                SheetUpdateHelper.CreateNonExistingData(data, GetAssetPath, ApplyRowToSo);
            }
            catch (Exception e)
            {
                Debug.LogError($"JSON parse error: {e.Message}");
            }
        }

    }
}