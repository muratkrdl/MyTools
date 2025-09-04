using System.Collections.Generic;
using System.Linq;
using _Scripts.Singleton;
using _Scripts.Utilities;
using UnityEngine;
using UnityEngine.Audio;

namespace _Scripts.SFX
{
    public class SFXManager : MonoSingleton<SFXManager>
    {
        private Dictionary<string, SFXData> sfxDataDictionary;
        private AudioMixer audioMixer;

        protected override void Awake()
        {
            base.Awake();
            SFXData[] sfxs = Resources.LoadAll<SFXData>("Data/SFXs");
            sfxDataDictionary = sfxs.ToDictionary(sfx => sfx.name, sfx => sfx);
            
            audioMixer = Resources.Load<AudioMixer>("SFXAudioMixer");
        }

        public void PlaySFX(string sfxName, Vector3 position = default) // position = Vector3.Zero
        {
            if (!sfxDataDictionary.TryGetValue(sfxName, out SFXData data)) return;
            
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