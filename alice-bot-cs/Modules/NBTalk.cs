using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using alice_bot_cs.Extensions.Fun;
using Mirai_CSharp;
using Mirai_CSharp.Models;
using Mirai_CSharp.Plugin.Interfaces;

namespace alice_bot_cs.Modules
{
    public partial class NBTalk : IGroupMessage 
    {
        string _talk = "";

        public NBTalk()
        {
        }

        public async Task<bool> GroupMessage(MiraiHttpSession session, IGroupMessageEventArgs e)
        {
            string str = string.Join(null, (IEnumerable<IMessageBase>)e.Chain); // 取消息
            string[] strArray = str.Split(new char[2] { '[', ']' }); // 分割Mirai码部分
            str = strArray[2];

            if (str.Equals(".nb"))
            {
                RandomNBExtension rnbe = new RandomNBExtension();
                _talk = rnbe.GetNbTalk(e.Sender.Name);
                IMessageBase plain = new PlainMessage(_talk);
                await session.SendGroupMessageAsync(e.Sender.Group.Id, plain);
            }
            return false;
        }
    }
}
