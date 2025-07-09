using _Scripts.Singleton;
using Unity.Mathematics;
using UnityEngine.Events;

namespace _Scripts.InputSystem
{
    public class InputEvents : MonoSingleton<InputEvents>
    {
        public UnityAction<float2> OnMoveStart;
        public UnityAction<float2> OnMoveStop;
        public UnityAction OnJump;
    }
}