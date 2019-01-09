using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
namespace Farmework{
	//新版本将改成时间判断,取消Update
	public class Audio : MonoBehaviourSimply {
		AudioSource _AudioSource;

		string MyAudioName;

		float AudioLength;//音频长度
        Action<string> action;

        private void Awake()
        {
            AudioLength = GetComponent<AudioSource>().clip.length;
            //DelayCoroutine(AudioLength,()=> {
            //    Hide();
            //});
        }


        protected override void OnBeforeDestroy(){
            
        }
    }
}


