/***************************************************
 * 文件：GameManager.cs
 * 作者：Gavin
 * 邮箱：a277152071@163.com
 * 功能：存储关卡名
 *       是否拥有loading界面
 *       当前场景的FixedUpdate,Update,LateUpdate逻辑函数
 *       
 *       
 *       
 *       
 *       
 **************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace Farmework {

    public enum GameManagerActionType {
        UpdateAction,
        FixedUpdateAction,
        LateUpdateAction
    }

    public class GameManager : Single<GameManager>{
        public string[] LevelName;
        public bool LoadScenenHasLoading = true;//加载场景是否需要添加Loading
        Action ScenenFixedUpdateAction;
        Action ScenenUpdateAction;
        Action ScenenLateAction;

        private void Awake(){
            DontDestory();
        }

        public void RegisterAction(Action action,GameManagerActionType type) {
            switch (type) {
                case GameManagerActionType.UpdateAction:
                    ScenenUpdateAction += action;
                    break;
                case GameManagerActionType.FixedUpdateAction:
                    ScenenFixedUpdateAction += action;
                    break;
                case GameManagerActionType.LateUpdateAction:
                    ScenenLateAction += action;
                    break;
            }
        }

        public void UnRegisterAction(Action action, GameManagerActionType type) {
            switch (type){
                case GameManagerActionType.UpdateAction:
                    ScenenUpdateAction -= action;
                    break;
                case GameManagerActionType.FixedUpdateAction:
                    ScenenFixedUpdateAction-= action;
                    break;
                case GameManagerActionType.LateUpdateAction:
                    ScenenLateAction-= action;
                    break;
            }
        }

        public void ClearAction()
        {
            ScenenFixedUpdateAction = null;
            ScenenUpdateAction = null;
            ScenenLateAction = null;
        }

        private void FixedUpdate(){
            if (ScenenFixedUpdateAction != null)
                ScenenFixedUpdateAction();
        }

        private void Update(){
            if (ScenenUpdateAction != null)
                ScenenUpdateAction();
        }

        private void LateUpdate()
        {
            if (ScenenLateAction != null)
                ScenenLateAction();
        }

    }
}


