using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace BL_Core.Language
{
    public enum Languages {
        Chainese,
        English,
        other
    }

    /// <summary>
    /// 语言包管理
    /// </summary>
    public class Language
    {
        /// <summary>
        /// 语言包词典
        ///  键 -- 值，文字
        /// </summary>
        private static Dictionary<string, string> LgeDict=new Dictionary<string, string>(2048);

        /// <summary>
        /// 存储语言包路径
        /// 语种 -- 路径
        /// </summary>
        private static Dictionary<string, string> LgePathDict = new Dictionary<string, string>(2);


       

        /// <summary>
        /// 加载语言包的配置
        /// </summary>
        /// <param name="path">语言包 config 的路径</param>
        /// <param name="callBack">回调</param>
        public static void LoadConfig(string paths, CallBack call=null)
        {
            Reset();
            //读取csv二进制文件  
            TextAsset binAsset = Resources.Load(paths)as TextAsset;
            //数据按行解析,得到各个语言包路径
            string[] lines = binAsset.text.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            int lineLen = lines.Length;
            //CSV格式批分符
            char[] splitStr = { '\t' };
            for (int i = 0; i < lineLen; i++)
            {
                string tmpStr = lines[i];
                //注释行，忽略
                if (tmpStr.StartsWith("//"))
                {
                    continue;
                }
                int splitPos = tmpStr.IndexOf("\t");
                //空行忽略
                if (splitPos < 1)
                {
                    continue;
                }
                string[] kv = tmpStr.Split(splitStr);
                //对key做判断
                if (LgeDict.ContainsKey(kv[0]) && string.IsNullOrEmpty(kv[0]))
                {
                    Debug.LogError("语言包key有重复，错误位置 ->{" + tmpStr + "}");
                }
                else {
                    //保存配置
                    LgePathDict.Add(kv[0], kv[1]);
                }
            }
            //回调
            call?.Invoke();
        }

        /// <summary>
        /// 加载语言包
        /// </summary>
        /// <param name="path"></param>
        /// <param name="call"></param>
        public static void LoadLge(string path,CallBack call=null) {

            //读取csv二进制文件  
            TextAsset[] binAsset = Resources.LoadAll<TextAsset>(path);
            for (int i = 0; i < binAsset.Length; i++)
            {
                string[] lines = binAsset[i].text.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                int lineLen = lines.Length;
                //CSV格式批分符
                char[] splitStr = { '\t' };
                for (int j = 0; j < lineLen; j++)
                {
                    string tmpStr = lines[j];
                    //注释行，忽略
                    if (tmpStr.StartsWith("//"))
                    {
                        continue;
                    }
                    int splitPos = tmpStr.IndexOf("\t");
                    //空行忽略
                    if (splitPos < 1)
                    {
                        continue;
                    }
                    string[] kv = tmpStr.Split(splitStr);
                    //对key做判断
                    if (LgeDict.ContainsKey(kv[0]) && string.IsNullOrEmpty(kv[0]))
                    {
                        Debug.LogError("语言包key有重复，错误位置 ->{" + tmpStr + "}");
                    }
                    else
                    {
                        //保存
                        LgeDict.Add(kv[0], kv[1]);
                    }
                }
            }
            //回调
            call?.Invoke();
        }


        /// <summary>
        /// 翻译语言
        /// </summary>
        /// <param name="key">##+key</param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string Trans(string key, params object[] args)
        {
            if (LgeDict == null)
            {
                return key;
            }
            string realKey;
            if (key.StartsWith("##"))
            {
                realKey = key.Substring(2);
                if (LgeDict.TryGetValue(realKey, out String value))
                {
                    if (args.Length > 0)
                    {
                        return string.Format(value, args);
                    }
                    return value;
                }
                return "<" + key + ">Not Find";
            }
            return key;
        }
       
        /// <summary>
        /// 翻译语言
        /// </summary>
        /// <param name="key"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string Trans(int key, params object[] args)
        {
            string realKey = key.ToString();
            if (LgeDict.TryGetValue(realKey, out String value))
            {
                if (args.Length > 0)
                {
                    return string.Format(value, args);
                }
                return value;
            }
            return realKey;
        }
       

        /// <summary>
        /// 重置
        /// </summary>
        private static void Reset() {
            LgePathDict.Clear();
            LgeDict.Clear();
            
        }
    }
}
