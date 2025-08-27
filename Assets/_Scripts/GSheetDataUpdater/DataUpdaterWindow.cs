using UnityEditor;
using UnityEngine;

namespace _Scripts.GSheetDataUpdater
{
    public class DataUpdaterWindow : EditorWindow
    {
        [MenuItem("Tools/Data Update Window")]
        public static void ShowWindow()
        {
            GetWindow<DataUpdaterWindow>("Data Updater");
        }

        void OnGUI()
        {
            /// <summary>
            /// You can also use [MenuItem("Window/Data Update/Update MyData")]
            /// on the MyDataSheetUpdater.UpdateMyDataValues function.
            /// You don't need that script
            /// </summary>

            if (GUILayout.Button("Update MyData Data"))
            {
                MyDataSheetUpdater.UpdateMyDataValues();
            }
            // Other data update buttons
        }
    }
}