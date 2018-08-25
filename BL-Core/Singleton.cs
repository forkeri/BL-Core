using System;
using System.Collections.Generic;
using System.Text;

namespace BL_Core
{
    /// <summary>
    /// 单例模板，满足条件：类，无参构造器
    /// </summary>
    public class Singleton<T> where T : class, new()
    {
        public static T _instance = null;
        public static T Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new T();
                return _instance;
            }
        }
    }
}
