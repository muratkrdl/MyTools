using _Scripts.ObjectPool;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Pool;

namespace _Scripts.SFX
{
    public class SFXObject : MonoBehaviour, IPoolObject<SFXObject>
    {
        [SerializeField] private AudioSource audioSource;
    
        private ObjectPool<SFXObject> _pool;

        public void SetPool(ObjectPool<SFXObject> pool)
        {
            _pool = pool;
        }

        public void Play(SFXData data, AudioMixerGroup audioMixerGroupByType)
        {
            audioSource.clip = data.GetClip();
            audioSource.loop = data.Loop;
            audioSource.volume = data.Volume;
            audioSource.pitch = data.GetPitch();
            audioSource.spatialBlend = data.Is3D;
            audioSource.maxDistance = data.MaxDistance;
            audioSource.outputAudioMixerGroup = audioMixerGroupByType;
            audioSource.Play();
            Release().Forget();
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        private async UniTaskVoid Release()
        {
            await UniTask.WaitUntil(() => !audioSource.isPlaying);
            ReleasePool();
        }

        public void ReleasePool()
        {
            _pool.Release(this);
        }
    
    }
}
