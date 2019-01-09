using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Farmework {
    public class AudioFSMState{
        public void AudioPlay(AudioSource _AudioSource) {
            _AudioSource.Play();
        }

		public void RepeatAudioPlay(AudioSource _AudioSource){
			if(_AudioSource.isPlaying){
				return;
			}
			_AudioSource.Play();
		}

        public void AudioPause(AudioSource _AudioSource) {
            _AudioSource.Pause();
        }

        public void AddVolume(AudioSource _AudioSource) {
            if (_AudioSource.volume<1.0f) {
                _AudioSource.volume += 0.1f;
            }
        }

        public void AddVolume(AudioSource _AudioSource, float volume)
        {
            if (_AudioSource.volume < 1.0f)
            {
                _AudioSource.volume += volume;
            }
        }

        public void LessVolume(AudioSource _AudioSource) {
            if (_AudioSource.volume > 0.0f){
                _AudioSource.volume -= 0.1f;
            }
        }

        public void LessVolume(AudioSource _AudioSource,float volume)
        {
            if (_AudioSource.volume > 0.0f)
            {
                _AudioSource.volume -= volume;
            }
        }

        public void SetVolume(AudioSource _AudioSource,float volume) {
            _AudioSource.volume= volume;
        }

        public void LoopPlay(AudioSource _AudioSource) {
			if(!_AudioSource.isPlaying){
				_AudioSource.loop = true;
				_AudioSource.Play();
			}
            
        }

        public void CancelLoop(AudioSource _AudioSource) {
            _AudioSource.loop = false;
			_AudioSource.Pause();
        }
    }
}


