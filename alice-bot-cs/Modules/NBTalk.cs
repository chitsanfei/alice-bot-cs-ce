using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using alice_bot_cs.Extensions;
using Mirai_CSharp;
using Mirai_CSharp.Models;
using Mirai_CSharp.Plugin.Interfaces;

namespace alice_bot_cs.Modules
{
    public partial class NBTalk : IGroupMessage 
    {
        string talk = "";

        public NBTalk()
        {
        }

        public async Task<bool> GroupMessage(MiraiHttpSession session, IGroupMessageEventArgs e)
        {
            string str = string.Join(null, (IEnumerable<IMessageBase>)e.Chain);

            if (str.Contains(".nb"))
            {
                RandomNBExtension rnbe = new RandomNBExtension();
                talk = rnbe.GetNBTalk(e.Sender.Name);
                IMessageBase plain = new PlainMessage(talk);
                await session.SendGroupMessageAsync(e.Sender.Group.Id, plain);
            }
            return false;
        }
    }
}
