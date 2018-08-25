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
        /// <summary>
        /// 任务模型id
        /// </summary>
        public int Id;

        /// <summary>
        /// 任务执行的时间
        /// </summary>
        public long Time;

        /// <summary>
        /// 当定时器到达时间后的触发
        /// </summary>
        private CallBack call;

        public TimerModel(int id, long time, CallBack call)
        {
            this.Id = id;
            this.Time = time;
            this.call = call;
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

