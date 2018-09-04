using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BL_Core.Language
{
    /// <summary>
    /// 编辑器下所用的language类
    /// </summary>
    public class LanguageEditor
    {
        private Dictionary<string, string> LgeDict = new Dictionary<string, string>();


        /// <summary>
        /// 翻译
        /// </summary>
        /// <param name="key"></param>
        /// <param name="args"></param>
        public string Trans(string key,params object[] args) {

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

    }
}
