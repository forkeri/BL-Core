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
        public int Id;

        /// <summary>
        /// 任务执行的事件
        /// </summary>
        public long Time;

        private CallBack call;//委托

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

