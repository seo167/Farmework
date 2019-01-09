/***************************************************
 * 文件：LevelManager.cs
 * 作者：Gavin
 * 邮箱：a277152071@163.com
 * 功能：场景管理器
 * *************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
namespace Farmework {
    public class LevelManager : Single<LevelManager>{
        public void AsyncLoadScenen(int index,Action action) {
            string LevelName = GameManager.Instance.LevelName[index];
            AsyncOperation async=SceneManager.LoadSceneAsync(LevelName);
            if (GameManager.Instance.LoadScenenHasLoading) {
                GameManager.Instance.ClearAction();
                UIManager.Create("UI/LoadingPage",Vector3.zero);
                GameManager.Instance.RegisterAction(()=> {
                    SendMsg("LoadingPage", async.progress);
                },GameManagerActionType.UpdateAction);
                
                
            }
        }
    }
}


