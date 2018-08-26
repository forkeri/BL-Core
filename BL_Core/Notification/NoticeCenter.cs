using System;
using System.Collections.Generic;
using System.Text;

namespace BL_Core.Notification
{
    public class NoticeCenter
    {
        private static NoticeCenter _default = new NoticeCenter();
        /// <summary>
        /// 默认的通知中心
        /// </summary>
        public static NoticeCenter Default { get => _default; }

        private Dictionary<ushort, Dictionary<IHandler, IHandler>> cmd2Handlers = null;
        private Dictionary<IHandler, Dictionary<ushort, ushort>> handler2Cmds = null;

        public NoticeCenter()
        {
            cmd2Handlers = new Dictionary<ushort, Dictionary<IHandler, IHandler>>();
            handler2Cmds = new Dictionary<IHandler, Dictionary<ushort, ushort>>();
        }

        /// <summary>
        /// 注册通知
        /// </summary>
        /// <param name="cmd">通知命令</param>
        /// <param name="handler">通知处理器</param>
        public void RegistCmd(ushort cmd, IHandler handler)
        {
            Dictionary<IHandler, IHandler> handlers;
            Dictionary<ushort, ushort> cmds;
            if (cmd2Handlers.TryGetValue(cmd, out handlers))
            {
                if (!handlers.ContainsKey(handler))
                    handlers.Add(handler, handler);
            }
            else
            {
                cmd2Handlers.Add(cmd, new Dictionary<IHandler, IHandler>());
                cmd2Handlers[cmd].Add(handler, handler);
            }
            if (handler2Cmds.TryGetValue(handler, out cmds))
            {
                if (!cmds.ContainsKey(cmd))
                    cmds.Add(cmd, cmd);
            }
            else
            {
                handler2Cmds.Add(handler, new Dictionary<ushort, ushort>());
                handler2Cmds[handler].Add(cmd, cmd);
            }
        }

        /// <summary>
        /// 派发通知
        /// </summary>
        /// <param name="cmd">通知命令</param>
        /// <param name="args">通知附带参数</param>
        public void Dispatcher(ushort cmd,params object[] args) {
            Dictionary<IHandler, IHandler> handlers;
            if (cmd2Handlers.TryGetValue(cmd, out handlers)) {
                foreach (var handler in handlers.Values)
                {
                    handler.HandCmd(cmd, args);
                }
            }
        }

        /// <summary>
        /// 移除处理器
        /// </summary>
        /// <param name="handler">处理器</param>
        public void RemoveHandler(IHandler handler) {
            Dictionary<ushort, ushort> cmds;
            if (handler2Cmds.TryGetValue(handler, out cmds)) {
                foreach (var cmd in cmds.Values)
                {
                    cmd2Handlers[cmd].Remove(handler);
                }
                handler2Cmds.Remove(handler);
            }
        }

        /// <summary>
        /// 移除某条通知
        /// </summary>
        /// <param name="cmd"></param>
        public void RemoveCmd(ushort cmd) {
            Dictionary<IHandler, IHandler> handlers;
            if (cmd2Handlers.TryGetValue(cmd, out handlers))
            {
                foreach (var handler in handlers.Values)
                {
                    handler2Cmds[handler].Remove(cmd);
                }
                cmd2Handlers.Remove(cmd);
            }
        }
    }
}
