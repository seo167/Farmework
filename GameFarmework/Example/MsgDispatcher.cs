/***************************************************
 * 文件：MsgDispatcher.cs
 * 作者：Gavin
 * 邮箱：a277152071@163.com
 * 功能：公共库
 * 更新：2018-12-24 加入存储对象字典；
 *                  加入注册对象函数；
 *                  获取对象函数；
 *                  移除注册对象函数；
 * *************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace Farmework
{
    public class MsgDispatcher
    {

        static Dictionary<string, Action<object>> MsgDictionary = new Dictionary<string, Action<object>>();//消息字典
        static Dictionary<string, GameObject> GameObjectDictionary = new Dictionary<string,GameObject>();//对象字典
        static public void RegisterMsg(string msgname, Action<object> action){
            if (!MsgDictionary.ContainsKey(msgname)){
                MsgDictionary.Add(msgname, _ => { });//传入空的监听
            }
            MsgDictionary[msgname] += action;
        }

        static public void SendMsg(string msgname, object obj){
            if (MsgDictionary.ContainsKey(msgname)){
                MsgDictionary[msgname](obj);
            }
        }

        static public void UnRegister(string msgname, Action<object> action){
            if (MsgDictionary.ContainsKey(msgname)){
                MsgDictionary[msgname] -= action;
            }
        }

        static public void UnRegisterAll(string msgname){
            if (MsgDictionary.ContainsKey(msgname)){
                MsgDictionary.Remove(msgname);
            }

        }

        static public void RegisterGameObject(string ObjName,GameObject gameObject) {
            if (!GameObjectDictionary.ContainsKey(ObjName)){
                GameObjectDictionary.Add(ObjName, gameObject);//传入GameObject
            }
        }

        static public GameObject GetGameObject(string ObjName) {
            if (GameObjectDictionary.ContainsKey(ObjName)) {
                return GameObjectDictionary[ObjName];
            }
            return null;
        }

        static public void UnRegisterObj(string objName)
        {
            if (GameObjectDictionary.ContainsKey(objName))
            {
                GameObjectDictionary.Remove(objName);
            }
        }

        static public void UnRegisterAllObj() {
            GameObjectDictionary.Clear();
        }

    }
}
