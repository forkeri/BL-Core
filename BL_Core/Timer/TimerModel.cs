using System;
using System.Collections.Generic;
using System.Text;

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
        public  CallBack CallBack { private set; get; }

        public TimerModel( float time, CallBack call)
        {
            this.Id = _id.Add_Get();
            this.DelayTime = time;
            this.CallBack = call;
        }

        /// <summary>
        /// 触发任务的
        /// </summary>
        public void Run()
        {
            call();
        }
    }
}

