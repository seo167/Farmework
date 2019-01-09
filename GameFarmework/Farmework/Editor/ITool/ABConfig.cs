using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="ABConfig",menuName ="CreateABConfig",order =0)]
public class ABConfig : ScriptableObject {
    //单个文件所在文件夹路径,会遍历这个文件夹下的所有Prefab，Prefab的名字不能重复
    public List<string> m_AllPrefabPath = new List<string>();
    public List<FileDirABName> m_AllFileDirAB = new List<FileDirABName>();

    [System.Serializable]
    public struct FileDirABName {
        public string ABName;
        public string Path;
    }

}
