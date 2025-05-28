using _Scripts.Singleton;
using UnityEngine.Events;

namespace _Scripts.GameState
{
    public class GameStateManager : MonoSingleton<GameStateManager>
    {
        private GameState _currentState;
        
        public UnityAction onGamePause;
        public UnityAction onGameResume;

        protected override void Awake()
        {
            base.Awake();
            _currentState = GameState.Playing;
        }

        private void OnEnable()
        {
            onGamePause += OnGamePause;
            onGameResume += OnGameResume;
        }

        private void OnGamePause() => _currentState = GameState.Paused;
        private void OnGameResume() => _currentState = GameState.Playing;

        private  void OnDisable()
        {
            GameStateManager.Instance.onGamePause -= OnGamePause;
            GameStateManager.Instance.onGameResume -= OnGameResume;
        }

        public GameState GetCurrentState() => _currentState;

    }
}