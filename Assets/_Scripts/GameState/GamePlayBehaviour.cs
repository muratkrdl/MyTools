using UnityEngine;

namespace _Scripts.GameState
{
    public abstract class GamePlayBehaviour : MonoBehaviour
    {
        protected virtual void OnEnable()
        {
            GameStateManager.Instance.onGamePause += OnGamePause;
            GameStateManager.Instance.onGameResume += OnGameResume;
        }

        protected abstract void OnGamePause();
        protected abstract void OnGameResume();

        protected virtual void OnDisable()
        {
            GameStateManager.Instance.onGamePause -= OnGamePause;
            GameStateManager.Instance.onGameResume -= OnGameResume;
        }
    }
}
