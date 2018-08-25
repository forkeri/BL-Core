using System;
using System.Collections;
using System.Collections.Generic;
using BL_Core;

namespace BL_Core.Timer
{
    /// <summary>
    /// 计时器管理者
    /// </summary>
    public class TimerManager
    {
        //单例
        private static TimerManager instance = null;
        public static TimerManager Instance
        {
            get
            {
                lock (instance)
                {
                    if (instance == null)
                        instance = new TimerManager();
                    return instance;
                }
            }
        }


        /// <summary>
        /// 这个字典存储： 任务id  和  任务模型 的映射
        /// </summary>
        private Dictionary<int, TimerModel> idModelDict = new Dictionary<int, TimerModel>();

        /// <summary>
        /// 要移除的任务ID列表
        /// </summary>
        private List<int> removeList = new List<int>();

        /// <summary>
        /// 用来表示ID
        /// </summary>
        private ConcurrentInt id = new ConcurrentInt(-1);

        /// <summary>
        /// 实现定时器的主要功能就是这个Timer类
        /// </summary>
        private System.Timers.Timer timer;
        public TimerManager()
        {
            timer = new System.Timers.Timer(100);//默认100毫秒监听一次
            timer.Elapsed += Timer_Elapsed;
        }

        /// <summary>
        /// 达到时间间隔时候触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            lock (removeList)
            {
                TimerModel tmpModel = null;
                foreach (var id in removeList)
                {
                    idModelDict.Remove(id);
                }
                removeList.Clear();
            }

            foreach (var model in idModelDict.Values)
            {

                if (model.Time <= DateTime.Now.Ticks)
                {
                    model.Run();
                }
            }
        }

        /// <summary>
        /// 添加定时任务 指定延迟的时间  2017年8月6日18:44:33
        /// </summary>
        public void AddTimerEvent(DateTime datetime, CallBack call)
        {
            long delayTime = datetime.Ticks - DateTime.Now.Ticks;
            if (delayTime <= 0)
                return;
            AddTimerEvent(delayTime, call);
        }

        /// <summary>
        /// 添加定时任务 指定延迟的时间  40s
        /// </summary>
        /// <param name="delayTime">毫秒！！</param>
        /// <param name="timeDelegate"></param>
        public void AddTimerEvent(long delayTime, CallBack call)
        {
            TimerModel model = new TimerModel(id.Add_Get(), DateTime.Now.Ticks + delayTime, call);
            idModelDict.Add(model.Id, model);
        }

        /// <summary>
        /// 添加移除指定id 的定时任务
        /// </summary>
        /// <param name="id"></param>
        public void AddRemoveTimeEvent(int id)
        {

            if (removeList.Contains(id))
            {
                removeList[id] = id;
            }
            else
            {
                removeList.Add(id);
            }
        }
    }
}
