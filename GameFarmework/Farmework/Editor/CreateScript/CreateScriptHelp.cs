/***************************************************
 * 文件：AutioAddNameSpace.cs
 * 作者：Gavin
 * 邮箱：a277152071@163.com
 * 功能：CreateScriptHelp类
 * 更新：2019-01-08 配合AutioAddNameSpace使用；
 * 
 * *************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
namespace Farmework {
    public class CreateScriptHelp
    {
        private StringBuilder _stringBuilder;
        private string _LineBrake="\r\n";
        private int currentIndex = 0;
        public int IndentTimes { get; set; }
        public CreateScriptHelp() {
            _stringBuilder = new StringBuilder();
        }

        public void Write(string context,bool needIndent) {
            if (needIndent) {
                context = GetIndent() + context;
            }

            if (currentIndex == _stringBuilder.Length)
            {
                _stringBuilder.Append(context);
            }
            else {
                _stringBuilder.Insert(currentIndex,context);
            }
            currentIndex += context.Length;

        }

        

        public void WriteLine(string content,bool needIndent) {
            Write(content+ _LineBrake, needIndent);
        }
        //缩进
        public string GetIndent() {
            string indent = "";
            for (int i=0;i<IndentTimes;++i) {
                indent += "   ";
            }

            return indent;
        }

        public int WriteCurlyBrackets() {
            var start = GetIndent() + "{"+ _LineBrake;
            var end= GetIndent() + "}"+ _LineBrake;
            Write(start+end,false);
            return end.Length;
        }

        public void WriteNameSpace(string name) {
            Write("namespace "+name,false);
           int Length=WriteCurlyBrackets();
            currentIndex -= Length;
        }

        public void WriteClass(string name) {
            Write("public class " + name+ " : MonoBehaviourSimply", false);
            int Length = WriteCurlyBrackets();
            currentIndex -= Length;
        }

        public void WriteUsing(string name) {
            Write("using " + name+ _LineBrake, false);
        }

        /// <summary>
        /// 创建函数
        /// </summary>
        /// <param name="name">函数名</param>
        /// <param name="type"> “public void，int”等</param>
        /// <param name="paraName">函数参数</param>
        public void WriteFun(string name,string type,params string[] paraName) {
            StringBuilder temp = new StringBuilder();
            temp.Append(type+name+"()");

            if (paraName.Length != 0)
            {
                foreach (var t in paraName)
                {
                    temp.Insert(temp.Length - 1, t + ",");
                }
                temp.Remove(temp.Length-2, 1);
            }
            Write(temp.ToString(),true);
            WriteCurlyBrackets();
        }

        public override string ToString()
        {
            return _stringBuilder.ToString();
        }

    }
}


