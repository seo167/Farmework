/***************************************************
 * 文件：LoadWindow.cs
 * 作者：Gavin
 * 邮箱：a277152071@163.com
 * 功能：Loading工具
 * *************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Farmework {
    public class LoadWindow : UIPlane{
        [SerializeField]protected Image ProgressBar;
        [SerializeField]protected Text ProgressBarText;
        protected override void Init(){
            UIName = "LoadingPage";
            GameObject.DontDestroyOnLoad(this.gameObject);
        }

        protected override void Logic(object _oject){
            ProgressBar.fillAmount = (float)_oject;
            ProgressBarText.text = ProgressBar.fillAmount*100 + " %";
            if (ProgressBar.fillAmount>=1) {
                UIManager.DeletePlane("LoadingPage");
                DelayCoroutine(0.5f,()=> {
                    GameObject.Destroy(gameObject);
                });
            }
        }

        public override void Reset(){
            if (ProgressBar != null)
                ProgressBar.fillAmount = 0;
            if (ProgressBarText != null)
                ProgressBarText.text= "0 %";
        }

    }
}


