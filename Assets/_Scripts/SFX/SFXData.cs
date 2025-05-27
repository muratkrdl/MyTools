using UnityEngine;

namespace _Scripts.SFX
{
    [CreateAssetMenu(fileName = "New SFXData", menuName = "SO/SFXData")]
    public class SFXData : ScriptableObject
    {
        public string Name;
        
        public AudioClip[] Clips;
        
        public bool Loop;
        
        [Range(0,1)] public float Volume;
        [Range(-3,3)] public float MinPitch;
        [Range(-3,3)] public float MaxPitch;
        [Range(0,1)] public float Is3D;
        
        public float MaxDistance = 500f;

        public AudioClip GetClip()
        {
            return Clips[Random.Range(0, Clips.Length)];
        }

        public float GetPitch()
        {
            return Random.Range(MinPitch, MaxPitch);
        }
        
    }
}
