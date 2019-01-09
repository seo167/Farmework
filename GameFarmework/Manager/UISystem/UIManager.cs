using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Farmework
{
    public class UIManager
    {
        static private Dictionary<string, UIPlane> UIDictionary=new Dictionary<string, UIPlane>();

        //创建UI
        static public void Create(string UIName,Vector3 pos,Transform parent=null) {
            if (HasPlane(UIName)){
                UIDictionary[UIName].Show();
            }else {
                var UI = Resources.Load<UIPlane>(UIName);
                UI = Object.Instantiate<UIPlane>(UI);
                UI.transform.SetParent(parent);
                if(UI.rectTransform!=null)
                    UI.rectTransform.anchoredPosition = pos;
                SavePlane(UIName, UI);
            }
            
        }

        static public void SavePlane(string UIName,UIPlane plane) {
            if (!UIDictionary.ContainsKey(UIName)) {
                UIDictionary.Add(UIName, plane);
            }
            
        }

        static public UIPlane GetPlane(string UIName) {
            if (!HasPlane(UIName)) {
                Debug.LogError("不存在该UI");
            }
            return UIDictionary[UIName];
        }

        static public bool HasPlane(string UIName) {
            if (UIDictionary.ContainsKey(UIName))
            {
                return true;
            }
            else {
                return false;
            }
        }

        static public void DeletePlane(string UIName) {
            if (UIDictionary.ContainsKey(UIName)) {
                UIDictionary.Remove(UIName);
            }
        }

        static void Clear() {
            UIDictionary.Clear();
        }

    }
}


