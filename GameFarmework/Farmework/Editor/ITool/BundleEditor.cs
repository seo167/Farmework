using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using Farmework;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public class BundleEditor {
    static string FilePath = Application.streamingAssetsPath;
    public static string ABConfigPath = "Assets/GameFarmework/Farmework/Editor/ITool/ABConfig.asset";
    //Key是包名，value是路径，所有文件夹ab包的dic
    public static Dictionary<string, string> m_AllFileDir = new Dictionary<string, string>();
    //过滤的list
    public static List<string> m_AllFileAB = new List<string>();

    //单个prefab的AB包
    public static Dictionary<string, List<string>> m_AllPrefabDir = new Dictionary<string, List<string>>();

    //储存所有有效的路径
    private static List<string> m_ConfigFil = new List<string>();

    [MenuItem("ITool/打包")]
    public static void Build() {
        m_AllFileDir.Clear();
        m_AllFileAB.Clear();
        m_AllPrefabDir.Clear();
        m_ConfigFil.Clear();
        ABConfig abconfig = AssetDatabase.LoadAssetAtPath<ABConfig>(ABConfigPath);

        foreach (ABConfig.FileDirABName fileDir in abconfig.m_AllFileDirAB) {
            if (m_AllFileDir.ContainsKey(fileDir.ABName)) {
                Debug.LogError("AB包名字配置重复");
            } else {
                m_AllFileDir.Add(fileDir.ABName, fileDir.Path);
                m_AllFileAB.Add(fileDir.Path);
                m_ConfigFil.Add(fileDir.Path);
            }
        }

        //返回的是GUID
        string[] allStr = AssetDatabase.FindAssets("t:Prefab", abconfig.m_AllPrefabPath.ToArray());

        for (int i = 0; i < allStr.Length; ++i) {
            //将GUID转回正常路径
            string path = AssetDatabase.GUIDToAssetPath(allStr[i]);
            //显示进度条
            EditorUtility.DisplayProgressBar("查找Prefab", "Prefab:" + path, i * 1 / allStr.Length);
            m_ConfigFil.Add(path);
            if (!ContainAllFileAB(path)) {
                GameObject obj = AssetDatabase.LoadAssetAtPath<GameObject>(path);
                //获取obj对象所有依赖项
                string[] allDepend = AssetDatabase.GetDependencies(path);
                List<string> allDependPath = new List<string>();
                //allDependPath.Add(path);
                for (int j = 0; j < allDepend.Length; ++j) {
                    if (!ContainAllFileAB(allDepend[j]) && !allDepend[j].EndsWith(".cs")) {
                        m_AllFileAB.Add(allDepend[j]);
                        allDependPath.Add(allDepend[j]);
                    }
                }
                if (m_AllPrefabDir.ContainsKey(obj.name)) {
                    Debug.LogError("存在相同名字的Prefab");
                } else {
                    m_AllPrefabDir.Add(obj.name, allDependPath);
                }
            }
        }

        foreach (var name in m_AllFileDir.Keys) {
            SetABName(name,m_AllFileDir[name]);
        }

        foreach (string name in m_AllPrefabDir.Keys) {
            SetABName(name,m_AllPrefabDir[name]);
        }
        BunildAssetBundle();
        string[] oldStr = AssetDatabase.GetAllAssetBundleNames();
        for (int i=0;i<oldStr.Length;++i) {
            AssetDatabase.RemoveAssetBundleName(oldStr[i],true);
            EditorUtility.DisplayProgressBar("清除AB包名","名字:"+ oldStr[i],i/oldStr.Length);
        }

        //清除进度条
        EditorUtility.ClearProgressBar();

    }


    static void SetABName(string abName, string path) {
        AssetImporter assetImporter = AssetImporter.GetAtPath(path);
        if (assetImporter == null) {
            Debug.LogError("不存在该路径:" + path);
        }
        assetImporter.assetBundleName = abName;
    }

    static void SetABName(string abName,List<string> path) {
        for (int i=0;i<path.Count;++i) {
            SetABName(abName,path[i]);
        }
    }

    static void BunildAssetBundle() {
        string[] allBundles = AssetDatabase.GetAllAssetBundleNames();
        Dictionary<string, string> resPathDic = new Dictionary<string, string>();
        for (int i=0;i<allBundles.Length;++i) {
            string[] allBundlePath = AssetDatabase.GetAssetPathsFromAssetBundle(allBundles[i]);
            for (int j=0;i<allBundlePath.Length;++j) {
                if (allBundlePath[j].EndsWith(".cs"))
                    continue;
                Debug.Log("此AB包:"+allBundles[i]+"下面包含的资源文件路径:"+allBundlePath[j]);
                if (ValidPath(allBundlePath[j])) {
                    resPathDic.Add(allBundlePath[j], allBundles[i]);
                }
            }
        }
        DeleteAB();
        //生成配置表
        WriteData(resPathDic);
        BuildPipeline.BuildAssetBundles(FilePath, BuildAssetBundleOptions.ChunkBasedCompression,EditorUserBuildSettings.activeBuildTarget);
    }

    //写入数据
    static void WriteData(Dictionary<string,string> resPathDic) {
        AssetBundleConfig config = new AssetBundleConfig();
        config.ABList = new List<ABBase>();
        foreach (var t in resPathDic.Keys) {
            ABBase aBBase = new ABBase();
            aBBase.Path = t;
            aBBase.Crc = CRC32.GetCRC32(t);
            aBBase.ABName = resPathDic[t];
            aBBase.AssetName = t.Remove(0,t.LastIndexOf("/")+1);
            aBBase.ABDependce = new List<string>();
            string[] resDependce = AssetDatabase.GetDependencies(t);
            //从依赖项进行过滤
            for (int i=0;i<resDependce.Length;++i) {
                string tempPath = resDependce[i];
                if (tempPath==t||t.EndsWith(".cs")) {
                    continue;
                }
                //获取依赖项在那个AB包内
                string abName = "";
                if (resPathDic.TryGetValue(tempPath, out abName)) {
                    if (abName==resPathDic[t]) {//如果等于自己
                        continue;
                    }
                    if (!aBBase.ABDependce.Contains(abName)) {
                        aBBase.ABDependce.Add(abName);
                    }
                }
            }
        }

        //写入XML
        string xmlPath = Application.dataPath + "/AssetbundleConfig.xml";
        if (File.Exists(xmlPath))
            File.Delete(xmlPath);
        FileStream fileStream = new FileStream(xmlPath,FileMode.Create,FileAccess.ReadWrite,FileShare.ReadWrite);
        StreamWriter streamWriter = new StreamWriter(fileStream,System.Text.Encoding.UTF8);
        XmlSerializer xs = new XmlSerializer(config.GetType());
        xs.Serialize(streamWriter,config);//序列化
        streamWriter.Close();
        fileStream.Close();
        //写入二进制
        foreach (ABBase ab in config.ABList) {
            ab.Path = "";
        }


        string bytePath = FilePath + "AssetBundleConfig.bytes";
        FileStream fs = new FileStream(bytePath,FileMode.Create,FileAccess.ReadWrite,FileShare.ReadWrite);
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(fs,config);
        fs.Close();
    }

    //删除没用的AB包
    static void DeleteAB() {
        string[] allBundlesName = AssetDatabase.GetAllAssetBundleNames();
        DirectoryInfo info = new DirectoryInfo(FilePath);
        FileInfo[] fileInof = info.GetFiles("*",SearchOption.AllDirectories);
        for (int i=0;i<fileInof.Length;++i) {
            if (!ConatinABName(fileInof[i].Name, allBundlesName) || fileInof[i].Name.EndsWith(".merta")){
                continue;
            }
            else {
                Debug.Log("此AB包已经被删除或改名:"+fileInof[i].Name);
                if (File.Exists(fileInof[i].FullName)) {
                    File.Delete(fileInof[i].FullName);
                }
            }
        }
    }

    //是否有效路径
    static bool ValidPath(string path) {
        for (int i=0;i<m_ConfigFil.Count;++i) {
            //包含为有效路径
            if (path.Contains(m_ConfigFil[i])) {
                return true;
            }
        }
        return false;
    }

    //遍历资源文件夹里的文件名与设置的所有AB进行检查
    static bool ConatinABName(string name,string[] strs) {
        for (int i=0;i<strs.Length;++i) {
            if (name.Contains(strs[i])) {
                return true;
            }
        }
        return false;
    }

    //判断是否已经包含该路径
    static bool ContainAllFileAB(string path) {
        for (int i=0;i<m_AllFileAB.Count;++i) {
            if (m_AllFileAB[i].Equals(path)||path.Contains(m_AllFileAB[i])) {
                return true;
            }
        }
        return false;
    }

}
