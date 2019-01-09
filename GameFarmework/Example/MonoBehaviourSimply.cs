/***************************************************
 * 文件：MonoBehaviourSimply.cs
 * 作者：Gavin
 * 邮箱：a277152071@163.com
 * 功能：扩展MonoBehaviour类
 * 更新：2018-12-24 加入注册对象函数；
 *                  获取对象函数；
 *                  移除注册对象函数；
 * *************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace Farmework {
    public partial class MonoBehaviourSimply {
        protected void RegisterObj() {
            MsgDispatcher.RegisterGameObject(gameObject.name,gameObject);
        }

        protected GameObject GetGameObject(string name) {
            return MsgDispatcher.GetGameObject(name);
        }

        protected void UnRegisterGameObject(string name) {
            MsgDispatcher.UnRegisterObj(name);
        }

    }
    public abstract partial class MonoBehaviourSimply {
        List<MsgRecoder> msgListRecoder = new List<MsgRecoder>();
        //消息记录类
        class MsgRecoder {
           
            static public Stack<MsgRecoder> MsgRecoderStack=new Stack<MsgRecoder>();//存储消息记录对象池
            public string MsgName;
            public Action<object> Msg;

            private MsgRecoder() { }

            //为消息存储分配内存
            public static MsgRecoder Allocate(string _MsgName,Action<object> _Msg) {
                var recoder = MsgRecoderStack.Count> 0 ? MsgRecoderStack.Pop() : new MsgRecoder();
                recoder.MsgName = _MsgName;
                recoder.Msg = _Msg;
                return recoder;
            }

            //初始化消息
            public void Recycle() {
                MsgName = null;
                Msg = null;
                MsgRecoderStack.Push(this);
            }
        }
        //注册消息
        protected void RegisterMsg(string msgName,Action<object> action) {
            MsgDispatcher.RegisterMsg(msgName,action);
            msgListRecoder.Add(MsgRecoder.Allocate(msgName,action));
        }

        //单独注销消息
        protected void UnRegister(string msgName,Action<object> action) {
            var selectRecoder= msgListRecoder.FindAll(recoder=> recoder.MsgName==msgName&& recoder.Msg==action);
            selectRecoder.ForEach(recoder=> {
            MsgDispatcher.UnRegister(recoder.MsgName,recoder.Msg);
                msgListRecoder.Remove(recoder);
                recoder.Recycle();
            });
            selectRecoder.Clear();
        }

        //发送消息
        protected void SendMsg(string msgName,object obj) {
            MsgDispatcher.SendMsg(msgName,obj);
        }

        private void OnDestroy(){
            OnBeforeDestroy();
            foreach (var nsfRecider in msgListRecoder) {
                MsgDispatcher.UnRegister(nsfRecider.MsgName, nsfRecider.Msg);
            }
            UnRegisterGameObject(gameObject.name);
            msgListRecoder.Clear();
        }
        protected abstract void OnBeforeDestroy();

    }

    public partial class MonoBehaviourSimply : MonoBehaviour{
        public void Hide()
        {
            SetObject.Hide(gameObject);
        }

        public void Show() {
            SetObject.Show(gameObject);
        }

        public void Rotate(float x,float y,float z) {
            SetObject.SetRotate(transform,x,y,z);
        }

        public void RotateX(float x){
            SetObject.SetRotateX(transform, x);
        }

        public void RotateY(float y){
            SetObject.SetRotateY(transform,y);
        }

        public void RotateZ(float z){
            SetObject.SetRotateZ(transform,z);
        }

        public void Position(float x,float y,float z) {
            SetObject.SetPosition(transform,x,y,z);
        }

        public void PositionX(float x) {
            SetObject.SetPositionX(transform, x);
        }

        public void PositionY(float y)
        {
            SetObject.SetPositionY(transform, y);
        }

        public void PositionZ(float z)
        {
            SetObject.SetPositionZ(transform,z);
        }

        public void Scale(float x, float y, float z) {
            SetObject.SetScale(transform,x,y,z);
        }

        public void ScaleX(float x) {
            SetObject.SetScaleX(transform,x);
        }

        public void ScaleY(float y) {
            SetObject.SetScaleY(transform,y);
        }

        public void ScaleZ(float z) {
            SetObject.SetScaleZ(transform,z);
        }

        public void Identity() {
            SetObject.Identity(transform);
        }

        public void DontDestory()
        {
            GameObject.DontDestroyOnLoad(gameObject);
        }

        public void DelayLoopCoroutine(Action<bool> loopAction,Action onfinish){
            StartCoroutine(LoopDelay(loopAction, onfinish));
        }

        private IEnumerator LoopDelay(Action<bool> loopAction,Action onfinish){
            bool isLoop = true;
            while (isLoop) {
                loopAction(isLoop);
                yield return null;
            }
            onfinish();

        }

        //延迟执行
        public void DelayCoroutine(float delay, Action onfinish) {
            StartCoroutine(Delay(delay, onfinish));
        }

        private IEnumerator Delay(float delay,Action onfinish) {
            yield return new WaitForSeconds(delay);
            onfinish();
        }
    }
}


