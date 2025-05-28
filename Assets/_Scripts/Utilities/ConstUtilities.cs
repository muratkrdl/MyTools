using UnityEngine;

namespace _Scripts.Utilities
{
    public static class ConstUtilities
    {
        
#region GameObjects

        public static readonly Camera MainCamera = Camera.main;
        
 #endregion

#region Vectors
        
        public static readonly Vector2 Zero2 = Vector2.zero;
        public static readonly Vector2 One2 = Vector2.one;
        
        public static readonly Vector3 Zero3 = Vector3.zero;
        public static readonly Vector3 One3 = Vector3.one;
        
#endregion

#region Layers

        public static readonly int AllLayers = ~0;
        public static readonly int LayerPlayer = LayerMask.NameToLayer("Player");

#endregion

#region LayerMasks
        
        public static readonly LayerMask LayerMaskPlayer = LayerMask.GetMask("Player");

#endregion

#region Tags

        public const string PLAYER_TAG = "Player";

#endregion

#region Animation Hash

        // Triggers
        public static readonly int Walk_Anim = Animator.StringToHash("Walk");

        // Booleans

        // Floats

        // Ints

#endregion

    }
}