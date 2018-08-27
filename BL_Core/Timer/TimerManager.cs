using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace BL_Core.Timer
{
    /// <summary>
    /// 计时器管理者
    /// </summary>
    public class TimerManager:Singleton<TimerManager>
    {

        /// <summary>
        /// 这个字典存储： 任务id  和  任务模型 的映射
        /// </summary>
        private Dictionary<int, TimerModel> idModelDict = new Dictionary<int, TimerModel>();

        /// <summary>
        /// 获取定时任务队列
        /// </summary>
        /// <returns></returns>
        public List<TimerModel> GetTimerList() {
            return idModelDict.Values.ToList();
        }

       /// <summary>
       /// 添加定时任务
       /// </summary>
       /// <param name="delayTime">延时</param>
       /// <param name="call">回调函数，带一个float参数</param>
       /// <param name="isOnce">是否只执行一次</param>
       /// <param name="isScale">是否忽略时间缩放</param>
       /// <returns></returns>
        public int AddTimerEvent(float  delayTime, CallBack<float> call,bool isOnce=false,bool isScale=false)
        {
            TimerModel model = new TimerModel(delayTime,call,isOnce,isScale);
            model.RecalculateTime();
            idModelDict.Add(model.Id, model);
            return model.Id;
        }

       

        /// <summary>
        /// 添加移除指定id 的定时任务
        /// </summary>
        /// <param name="id"></param>
        public TimerModel RemoveTimeEvent(int id)
        {
            for (int i = 0; i < idModelDict.Count; i++)
            {
                if (idModelDict.ContainsKey(id))
                {
                    var model = idModelDict[id];
                    idModelDict.Remove(id);
                    return model;
                }
            }
            Debug.LogFormat("当前不存在该定时任务，定时任务Id：{0}", id);
            return null;
        }

        /// <summary>
        /// 移除定时任务
        /// </summary>
        /// <param name="call">回调函数</param>
        /// <returns></returns>
        public TimerModel RemoveTimeEvent(CallBack<float> call)
        {
            for (int i = 0; i < idModelDict.Count; i++)
            {
                if (idModelDict[i].CallBack == call) {
                    var model = idModelDict[i];
                    idModelDict.Remove(model.Id);
                    return model;
                }
            }
            Debug.LogFormat("当前不存在该定时任务，定时任务CallBack is：{0}", call.Method.Name);
            return null;
        }

        /// <summary>
        /// 更新执行
        /// </summary>
        internal void Update() {
            for (int i = idModelDict.Count - 1; i >= 0; i--)
            {
                if (idModelDict[i] == null)
                {
                    idModelDict.Remove(i);
                    continue;
                }
                if (idModelDict[i].IsComplete())
                {
                    idModelDict[i].CallBack?.Invoke(idModelDict[i].RealInterval);
                    if (idModelDict[i] != null)
                    {
                        if (idModelDict[i].IsOnce)
                            idModelDict[i] = null;
                        else
                            idModelDict[i].RecalculateTime();
                    }
                }
            }
        }


    }
}
