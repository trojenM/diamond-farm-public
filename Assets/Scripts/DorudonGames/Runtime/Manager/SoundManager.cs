using System.Linq;
using UnityEngine;
using System.Collections.Generic;
using DorudonGames.Runtime.Misc;
using Random = UnityEngine.Random;

namespace DorudonGames.Runtime.Manager
{
    public class SoundManager : Singleton<SoundManager>
    {
        // Serialized Field
        [SerializeField] private List<Sound> m_sounds;

        // Private Field
        private bool state;
        

        /// <summary>
        /// This function called when before first frame.
        /// </summary>
        protected override void Awake()
        {
            foreach (Sound sound in m_sounds)
            {
                AudioSource source = gameObject.AddComponent<AudioSource>();
                
                if(sound.Clips.Length == 0)
                    continue;
                
                AudioClip audioClip = sound.Clips[Random.Range(0, sound.Clips.Length)];
                
                source.clip = audioClip;
                source.pitch = sound.Pitch;
                source.volume = sound.Volume;
                source.loop = sound.IsLoop;

                sound.Source = source;  
            }

            state = PlayerPrefs.GetInt(CommonTypes.SOUND_STATE_KEY, 1) != 0;

            base.Awake();
        }

        /// <summary>
        /// This function helper for play sound with tag.
        /// </summary>
        /// <param name="tag"></param>
        public void Play(string tag)
        {
            Sound targetSound = m_sounds.SingleOrDefault(x => x.Tag == tag);
            AudioClip targetClip = null;
            
            if (targetSound == null)
                return;

            if(targetSound.Clips.Length == 0)
                return;
            
            targetClip = targetSound.Clips[Random.Range(0, targetSound.Clips.Length)];

            if (targetClip == null)
            {
                return;
            }
            
            targetSound.Source.PlayOneShot(targetClip);
        }

        /// <summary>
        /// This function helper for change sound state.
        /// </summary>
        /// <param name="state"></param>
        public void ChangeActiveState()
        {
            state = !state;
            AudioListener.volume = state ? 1 : 0;
            PlayerPrefs.SetInt(CommonTypes.SOUND_STATE_KEY, state ? 1 : 0);
        }

        /// <summary>
        /// This function returns related state.
        /// </summary>
        /// <returns></returns>
        public bool GetState()
        {
            return state;
        }
    }
}


