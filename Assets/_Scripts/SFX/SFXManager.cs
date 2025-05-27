using System.Linq;
using _Scripts.Singleton;
using UnityEngine;

namespace _Scripts.SFX
{
    public class SFXManager : MonoSingleton<SFXManager>
    {
        private SFXData[] sfxs;

        protected override void Awake()
        {
            base.Awake();
            sfxs = Resources.LoadAll<SFXData>("Data/SFXs");
        }

        public void PlaySFX(string sfxName)
        {
            SFXData data = sfxs.FirstOrDefault(item => item.Name == sfxName);
            SFXObject sfxObject = SFXObjectPool.Instance.GetFromPool();
            sfxObject.Play(data);
        }
        
        public void PlaySFX(string sfxName, Vector3 position)
        {
            SFXData data = sfxs.FirstOrDefault(item => item.Name == sfxName);
            SFXObject sfxObject = SFXObjectPool.Instance.GetFromPool();
            sfxObject.SetPosition(position);
            sfxObject.Play(data);
        }
        
    }
}