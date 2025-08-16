using System.Linq;
using _Scripts.Singleton;
using _Scripts.Utilities;
using UnityEngine;
using UnityEngine.Audio;

namespace _Scripts.SFX
{
    public class SFXManager : MonoSingleton<SFXManager>
    {
        private SFXData[] sfxs;
        private AudioMixer audioMixer;

        protected override void Awake()
        {
            base.Awake();
            sfxs = Resources.LoadAll<SFXData>("Data/SFXs");
            audioMixer = Resources.Load<AudioMixer>("SFXAudioMixer");
        }

        public void PlaySFX(string sfxName, Vector3 position = default) // position = Vector3.Zero
        {
            SFXData data = sfxs.FirstOrDefault(item => item.Name == sfxName);
            SFXObject sfxObject = SFXObjectPool.Instance.GetFromPool();
            sfxObject.SetPosition(position);
            sfxObject.Play(data, GetAudioMixerGroupByType(data.type));
        }
        
        private AudioMixerGroup GetAudioMixerGroupByType(SFXType type)
        {
            return audioMixer.FindMatchingGroups(type switch
            {
                SFXType.Single => ConstUtilities.SINGLE,
                _ => ConstUtilities.LOOP
            })[0];
        }

    }
}