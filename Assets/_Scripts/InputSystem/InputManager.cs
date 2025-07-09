using _Scripts.Singleton;
using _Scripts.Utilities;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts.InputSystem
{
    public class InputManager : MonoSingleton<InputManager>
    { 
        private PlayerInputActions playerInputActions;

        protected override void Awake()
        {
            base.Awake();
            playerInputActions = new PlayerInputActions();
            playerInputActions.Player.Enable();
            playerInputActions.UI.Enable();
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            playerInputActions.Player.Move.started += OnMoveStart;
            playerInputActions.Player.Move.canceled += OnMoveStop;
            playerInputActions.Player.Jump.started += OnJump;
        }

        private void OnMoveStart(InputAction.CallbackContext obj)
        {
            InputEvents.Instance.OnMoveStart?.Invoke(obj.ReadValue<Vector2>());
        }

        private void OnMoveStop(InputAction.CallbackContext obj)
        {
            InputEvents.Instance.OnMoveStop?.Invoke(ConstUtilities.Zero2);
        }
        
        private void OnJump(InputAction.CallbackContext obj)
        {
            InputEvents.Instance.OnJump?.Invoke();
        }
        
        private void UnSubscribeEvents()
        {
            playerInputActions.Player.Move.started -= OnMoveStart;
            playerInputActions.Player.Move.canceled -= OnMoveStop;
            playerInputActions.Player.Jump.started -= OnJump;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
        
    }
}