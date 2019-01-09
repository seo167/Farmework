/***************************************************
 * 文件：AutioAddNameSpace.cs
 * 作者：Gavin
 * 邮箱：a277152071@163.com
 * 功能：AutioAddNameSpace类
 * 更新：2019-01-08 自动加入命名空间；
 * AssetModificationProcessor用来监听Project视图中资源的创建，删除，移动和保存
 * *************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text.RegularExpressions;
namespace Farmework
{
    public class AutioAddNameSpace : AssetModificationProcessor
    {
        private static void OnWillCreateAsset(string assetName) {
            
            assetName = assetName.Replace(".meta", "");//将.meta置为空
            if (assetName.EndsWith(".cs")) {
                string text = "";
                text += File.ReadAllText(assetName);
                var newtext= UpdateScript(GetClassName(text));
                File.WriteAllText(assetName,newtext);
            }
        }

        //更新脚本内容
        private static string UpdateScript(string className)
        {
            var script = new CreateScriptHelp();
            script.WriteUsing("System.Collections;");
            script.WriteUsing("System.Collections.Generic;");
            script.WriteUsing("UnityEngine;");
            script.WriteUsing("Farmework;");
            //script.WriteNameSpace("Farmework");
            ////script.IndentTimes++;
            script.WriteClass(className);
            script.IndentTimes++;
            //script.IndentTimes++;
            script.WriteFun("Start","private void ");
            script.WriteFun("Update","private void ");
            script.WriteFun("OnBeforeDestroy", "protected override void ");
            return script.ToString();
        }

        private static string GetClassName(string text) {
            string patterm = "public class ([A-Za-z0-9_]+)\\s*:\\s*MonoBehaviour";
           
            var data = Regex.Match(text,patterm);
            if (data.Success) {
                
                return data.Groups[1].Value;
            }
            return null;
        }

    }
}


