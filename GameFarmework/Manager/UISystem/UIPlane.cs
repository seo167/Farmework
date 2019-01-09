using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Farmework
{
    public class UIPlane : MonoBehaviourSimply{
        public string UIName="UIPlane";
        public RectTransform rectTransform;
        private void Awake(){
            Init();
            rectTransform = GetComponent<RectTransform>();
            RegisterMsg(UIName, (object _oject) =>Logic(_oject));
        }

        protected virtual void Init() {
            //UI视图初始化时逻辑
        }

        public virtual void Reset() {
            //UI状态重置
        }

        protected virtual void Logic(object _oject){
            //UI视图逻辑
        }

        protected override void OnBeforeDestroy() {
            UIManager.DeletePlane(UIName);
        }

        private void OnDisable(){
            Reset();
        }

    }
}


