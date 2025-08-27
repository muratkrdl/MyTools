using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace _Scripts.Console
{
    public class ConsoleController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI exampleText;

        [SerializeField] private byte fontSize = 24;
        [SerializeField] private byte yValue;
        [SerializeField] private byte consoleHeight = 60;

        private bool _showConsole;
        private bool _showHelp;
        private string _input = "";
        private Vector2 _scroll;

        private List<ConsoleCommandBase> _commandList;

        private void Awake()
        {
            _commandList = new List<ConsoleCommandBase>();

            Register("get_money", "Get 1000 Money", "get_money", 
                () => exampleText.text = "Getting 1000 Money");

            Register<int>("set_money", "Set money specific value", "set_money <money_amount>", 
                x => exampleText.text = $"set money {x}");

            Register("get_gun_item", "Get random gun item", "get_gun_item", 
                () => exampleText.text = "Getting random gun item");

            Register<string>("get_gun_item_s", "Get specific gun item", "get_gun_item_s <space_card_name>", 
                x => exampleText.text = $"Getting gun item {x}");

            Register("help", "Shows a list of commands", "help", 
                () => _showHelp = !_showHelp);
        }

        private void Update()
        {
            // Open-Close
            if (Input.GetKeyDown(KeyCode.Space))
                _showConsole = !_showConsole;

            // Enter Cheat Code
            if (Input.GetKeyDown(KeyCode.N))
                CheckCheatWord();
        }

        private void OnGUI()
        {
            if (!_showConsole) return;

            float y = yValue;
            GUIStyle textFieldStyle = new GUIStyle(GUI.skin.textField)
            {
                fontSize = fontSize
            };

            if (_showHelp)
                DrawHelp(ref y, textFieldStyle);

            DrawConsoleInput(y, textFieldStyle);
        }

        private void DrawHelp(ref float y, GUIStyle style)
        {
            GUI.Box(new Rect(0, y, Screen.width, consoleHeight * 3), "");

            Rect viewPort = new Rect(0, 0, Screen.width - consoleHeight, (float)consoleHeight * 2 / 3 * _commandList.Count);
            _scroll = GUI.BeginScrollView(new Rect(0, y + 5f, Screen.width, consoleHeight * 3), _scroll, viewPort);

            for (int i = 0; i < _commandList.Count; i++)
            {
                ConsoleCommandBase command = _commandList[i];
                string label = $"{command.GetFormat()} - {command.GetDescription()}";
                Rect labelRect = new Rect(5, (float)consoleHeight * 2 / 3 * i, viewPort.width - 100, (float)consoleHeight * 2 / 3);
                GUI.Label(labelRect, label, style);
            }

            GUI.EndScrollView();
            y += consoleHeight * 3;
        }

        private void DrawConsoleInput(float y, GUIStyle style)
        {
            GUI.Box(new Rect(0, y, Screen.width, consoleHeight), "");
            GUI.backgroundColor = Color.black;
            _input = GUI.TextField(new Rect(20f, y + 10f, Screen.width - 50f, (float)consoleHeight * 2 / 3), _input, style);
        }

        private void CheckCheatWord()
        {
            if (!_showConsole) return;
            HandleInput();
            _input = "";
        }

        private void HandleInput()
        {
            if (string.IsNullOrWhiteSpace(_input)) return;

            string[] parts = _input.Split(' ');
            string commandId = parts[0];

            foreach (var command in _commandList.Where(command => command.GetID() == commandId))
            {
                switch (command)
                {
                    case ConsoleCommand cmd:
                        cmd.Invoke();
                        break;
                    case ConsoleCommand<int> cmd when parts.Length > 1 && int.TryParse(parts[1], out int intVal):
                        cmd.Invoke(intVal);
                        break;
                    case ConsoleCommand<string> cmd when parts.Length > 1:
                        cmd.Invoke(parts[1]);
                        break;
                }
                return;
            }

            exampleText.text = $"Unknown command: {commandId}";
        }

        private void Register(string id, string desc, string format, UnityAction action)
        {
            var cmd = new ConsoleCommand(id, desc, format, action);
            _commandList.Add(cmd);
        }

        private void Register<T>(string id, string desc, string format, UnityAction<T> action)
        {
            var cmd = new ConsoleCommand<T>(id, desc, format, action);
            _commandList.Add(cmd);
        }
        
    }
}