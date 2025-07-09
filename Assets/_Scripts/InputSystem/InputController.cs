using Unity.Mathematics;
using UnityEngine;

namespace _Scripts.InputSystem
{
    public class InputController : MonoBehaviour
    {
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            InputEvents.Instance.OnMoveStart += OnMoveStart;
            InputEvents.Instance.OnMoveStop += OnMoveStop;
            InputEvents.Instance.OnJump += OnJump;
        }

        private void OnMoveStart(float2 value)
        {
            Debug.Log("OnMoveStart" + value);
        }

        private void OnMoveStop(float2 value)
        {
            Debug.Log("OnMoveStop" + value);
        }

        private void OnJump()
        {
            Debug.Log("OnJump");
        }

        private void UnSubscribeEvents()
        {
            InputEvents.Instance.OnMoveStart -= OnMoveStart;
            InputEvents.Instance.OnMoveStop -= OnMoveStop;
            InputEvents.Instance.OnJump -= OnJump;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
        
    }
}