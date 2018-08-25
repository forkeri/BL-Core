using System;
using System.Collections.Generic;
using System.Text;

namespace BL_Core.Notification
{
    public class NoticeCenter
    {
        private static NoticeCenter _default = new NoticeCenter();
        public static NoticeCenter Default { get => _default; }

        private Dictionary<ushort, Dictionary<IHandler, IHandler>> cmd2Handlers = null;
        private Dictionary<IHandler, Dictionary<ushort, ushort>> handler2Cmds = null;

        public NoticeCenter()
        {
            cmd2Handlers = new Dictionary<ushort, Dictionary<IHandler, IHandler>>();
            handler2Cmds = new Dictionary<IHandler, Dictionary<ushort, ushort>>();
        }

        public void RegistCmd(ushort cmd, IHandler handler)
        {
            Dictionary<IHandler, IHandler> handlers;
            if (cmd2Handlers.TryGetValue(cmd, out handlers))
            {

            }
            else
            {
            }
        }
    }
}
