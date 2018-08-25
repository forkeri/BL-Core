using System;
using System.Collections.Generic;
using System.Text;

namespace BL_Core.Timer
{
    /// <summary>
    /// 线程安全的int类型，简单地讲作为一个Id制造器
    ///     这样写的目的是 即使同时访问到此，也会保证Id唯一
    /// </summary>
    public class ConcurrentInt
    {
        private int value;

        public ConcurrentInt(int value)
        {
            this.value = value;
        }

        /// <summary>
        /// 添加并获取
        /// </summary>
        /// <returns></returns>
        public int Add_Get()
        {
            lock (this)
            {
                value++;
                return value;
            }
        }

        /// <summary>
        /// 减少并获取
        /// </summary>
        /// <returns></returns>
        public int Reduce_Get()
        {
            lock (this)
            {
                value--;
                return value;
            }
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <returns></returns>
        public int Get()
        {
            return value;
        }

    }
}
