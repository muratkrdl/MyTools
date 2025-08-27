using System;

namespace _Scripts.GSheetDataUpdater
{
    [Serializable]
    public class MyDataSheetRow
    {
        public int ID;
        public string Name;
        public int Value;
        public string Type;

        public override string ToString()
        {
            return Name;
        }
    }
}