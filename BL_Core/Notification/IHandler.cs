using System;
using System.Collections.Generic;
using System.Text;

namespace BL_Core.Notification
{
    /// <summary>
    /// 通知回调接口
    /// 通过实现该接口获得被获取通知的能力
    /// </summary>
    public interface IHandler
    {
        /// <summary>
        /// 处理通知回调
        /// </summary>
        /// <param name="cmd">命令</param>
        /// <param name="args">对应参数</param>
        void HandCmd(ushort cmd,params object[] args);
    }

    /// <summary>
    /// 对接口进行扩展，方便实现接口之后的注册操作
    /// </summary>
    public static class HandlerExtend {
        /// <summary>
        /// 注册进通知中心
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="cmd"></param>
        public static void RegistCmd(this IHandler handler,ushort cmd) {
            NoticeCenter.Default.RegistCmd(cmd, handler);
        }

        public static void RemoveCmd(this IHandler handler) {
            NoticeCenter.Default.RemoveHandler(handler);
        }
    }
}
