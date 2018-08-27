using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace BL_Core.Timer
{
    /// <summary>
    /// 定时器任务的数据模型
    /// </summary>
    public class TimerModel
    {
        static ConcurrentInt _id = new ConcurrentInt(-1);
        /// <summary>
        /// 任务模型id
        /// </summary>
        public int Id { private set; get; }

        /// <summary>
        /// 延迟时间
        /// </summary>
        public float DelayTime { private set; get; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public float EndTime { private set; get; }

        /// <summary>
        /// 是否忽略时间缩放
        /// </summary>
        private bool IsIgnoreTimeScaling { set; get; }

        /// <summary>
        /// 是否执行
        /// </summary>
        public bool IsOnce { set; get; }

        /// <summary>
        /// 当定时器到达时间后的触发，回调函数
        /// </summary>
        public  CallBack<float> CallBack { private set; get; }

        /// <summary>
        /// 真正的延迟间隔
        /// </summary>
        public float RealInterval { private set; get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="time">延迟时间</param>
        /// <param name="call">回调函数</param>
        /// <param name="isOnce">是否只执行一次</param>
        /// <param name="isScale">是否忽略时间缩放</param>
        public TimerModel(float time, CallBack<float> call, bool isOnce,bool isScale)
        {
            this.Id = _id.Add_Get();
            this.DelayTime = time;
            this.CallBack = call;
            this.IsOnce = isOnce;
            this.IsIgnoreTimeScaling = isScale;
        }

        /// <summary>
        /// 重新计算时长
        /// </summary>
        public float RecalculateTime() {
            EndTime = (IsIgnoreTimeScaling ? Time.realtimeSinceStartup : Time.time) + DelayTime;
            return EndTime;
        }

        /// <summary>
        /// 是否完成
        /// </summary>
        /// <returns></returns>
        public bool IsComplete() {
            if (EndTime <= (IsIgnoreTimeScaling ? Time.realtimeSinceStartup : Time.time)) {
                if (IsIgnoreTimeScaling)
                {
                     RealInterval = Time.realtimeSinceStartup - EndTime + DelayTime;
                }
                else {
                   RealInterval = Time.time - EndTime + DelayTime;
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// 执行回调函数
        /// </summary>
        public void Run()
        {
            CallBack(RealInterval);
        }
    }
}

