using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using alice_bot_cs.Entity;
using alice_bot_cs.Extensions;
using Mirai_CSharp;
using Mirai_CSharp.Extensions;
using Mirai_CSharp.Models;
using Mirai_CSharp.Plugin.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace alice_bot_cs.Modules
{
    public partial class GoodMorningAndNight : IGroupMessage
    {
        public async Task<bool> GroupMessage(MiraiHttpSession session, IGroupMessageEventArgs e) // 群消息处理方法
        {
            string str = string.Join(null, (IEnumerable<IMessageBase>)e.Chain);
            if (str.Contains("早安"))
            {
                Random ran = new Random();
                int n = ran.Next(1, 100);
                IMessageBuilder builder = new MessageBuilder();
                builder.AddPlainMessage($"早安哦！{e.Sender.Name}，送给你一个幸运数字：{n}，今天也要有个好心情啦！");
                await session.SendGroupMessageAsync(e.Sender.Group.Id, builder);
                return false;
            }
            if (str.Contains("晚安"))
            {
                var time = DateTime.Now.ToLocalTime().ToString();
                IMessageBuilder builder = new MessageBuilder();
                builder.AddPlainMessage($"晚安哦！{e.Sender.Name}，现在的时间是：{time}，要做个好梦哦！");
                await session.SendGroupMessageAsync(e.Sender.Group.Id, builder);
                return false;
            }
            return false;
        }
    }
}
