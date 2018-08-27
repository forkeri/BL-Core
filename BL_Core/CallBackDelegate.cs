using System;
using System.Collections.Generic;
using System.Text;

namespace BL_Core
{
    /// <summary>
    /// 无参委托
    /// </summary>
    public delegate void CallBack();
    /// <summary>
    /// 带一个参数的委托
    /// </summary>
    public delegate void CallBack<T>(T obj);
    /// <summary>
    /// 带两个泛型参数的委托
    /// </summary>
    public delegate void CallBack<T1,T2>(T1 obj1,T2 obj2);
    /// <summary>
    /// 带三个泛型参数的委托
    /// </summary>
    public delegate void CallBack<T1, T2,T3>(T1 obj1, T2 obj2,T3 obj3);
    /// <summary>
    /// 可选参数委托
    /// </summary>
    public delegate void CallBacks(params object[] objs);

    
    public static class CallBackDelegate {
       
       
    }
}
