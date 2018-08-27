using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace BL_Core
{
    /// <summary>
    /// 全局单例模板，继承了该类的单例类在整个游戏生命周期中不会销毁：真实的单例
    /// </summary>
    /// <typeparam name="T">单例类类型</typeparam>
    public class SingletonBehaviour<T>: MonoBehaviour where T : MonoBehaviour
    {
        //单例对象
        static T instance = null;
        //挂载单例的游戏对象
        static GameObject singObj = null;
        public static T Instance {
            get {
                //如果还没有这个物体：创建这个物体
                if (singObj == null)
                {
                    singObj = new GameObject();
                    //让其不销毁
                    DontDestroyOnLoad(singObj);
                }
                //尝试获取脚本
                instance = singObj.GetComponent<T>();
                //没有获取到则添加脚本
                if (instance == null)
                    instance = singObj.AddComponent<T>();
                //返回脚本实例
                return instance;
            }
        }
    }
}
