using System;
using UnityEngine;

namespace _Scripts.GSheetDataUpdater
{
    [CreateAssetMenu(fileName = "MyDataSO", menuName = "Data/MyDataSO")]
    public class MyDataSo : ScriptableObject
    {
        public int id;
        public new string name;
        public int value;
        public MyDataType type;

        public void UpdateData(int newID,string newName, int newValue, string newType)
        {
            id = newID;
            name = newName;
            value = newValue;
            type = Enum.TryParse(newType, out MyDataType outType) ? outType : MyDataType.Default;
            
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }

        public override string ToString()
        {
            return name;
        }
    }
}