using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Farmework {
    [System.Serializable]
    public class AudioManager{

		AudioSource BG;

        static AudioManager audioSystem;

        static public AudioManager Instace {
            get {
				if(audioSystem==null){
                    
					audioSystem = new AudioManager();
                    

                }
                return audioSystem;
            }
        }

        Dictionary<string, AudioSource> AudioSourceDictionary;

        private AudioManager() {
            AudioSourceDictionary = new Dictionary<string, AudioSource>();
        }

        AudioSource CreateAudio(string AudioName) {
             AudioClip temp = Resources.Load<AudioClip>(AudioName);
            GameObject a = new GameObject();
            a.name = AudioName;
            AudioSource tempAudioSource = a.AddComponent<AudioSource>();
            tempAudioSource.clip = temp;
            AudioSourceDictionary.Add(AudioName, tempAudioSource);
            return tempAudioSource;
        }

        public void PlayClip(string name,bool isLoop=false,float volume=1) {

            AudioSource tempAudioSource = (AudioSourceDictionary.ContainsKey(name)) ? AudioSourceDictionary[name] : CreateAudio(name);

            if (tempAudioSource!=null) {
                tempAudioSource.loop = isLoop;
                tempAudioSource.volume = volume;
                tempAudioSource.Play();

            }
        }


        public void StopPlayClip(string name) {
            if (AudioSourceDictionary.ContainsKey(name)) {
                AudioSourceDictionary[name].Stop();
            }
        }

        public void PlayBgm(string name) {

        }
    }
}


