namespace _Scripts.Console
{
    public class ConsoleCommandBase
    {
        private readonly string _id;
        private readonly string _description;
        private readonly string _format;
        
        public string GetID() => _id;
        public string GetDescription() => _description;
        public string GetFormat() => _format;

        protected ConsoleCommandBase(string id, string description, string format)
        {
            _id = id;
            _description = description;
            _format = format;
        }
    }
}
