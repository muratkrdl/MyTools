using UnityEngine.Events;

namespace _Scripts.Console
{
    public class ConsoleCommand : ConsoleCommandBase
    {
        private readonly UnityAction _command;
        
        public ConsoleCommand(string id, string description, string format, UnityAction command) : base(id, description, format)
        {
            _command = command;
        }

        public void Invoke()
        {
            _command?.Invoke();
        }
    }
    
    public class ConsoleCommand<T1> : ConsoleCommandBase
    {
        private readonly UnityAction<T1> _command;
        
        public ConsoleCommand(string id, string description, string format, UnityAction<T1> command) : base(id, description, format)
        {
            _command = command;
        }

        public void Invoke(T1 value)
        {
            _command?.Invoke(value);
        }
    }
}