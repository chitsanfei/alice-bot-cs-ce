using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mirai_CSharp;
using Mirai_CSharp.Extensions;
using Mirai_CSharp.Models;
using Mirai_CSharp.Plugin.Interfaces;

namespace alice_bot_cs.Plugins
{
    public partial class GoodMorningAndNight : IGroupMessage
    {
        public async Task<bool> GroupMessage(MiraiHttpSession session, IGroupMessageEventArgs e) // 群消息处理方法
        {
            string str = string.Join(null, (IEnumerable<IMessageBase>)e.Chain); // 取消息
            string[] strArray = str.Split(new char[2] { '[', ']' }); // 分割Mirai码部分
            str = strArray[2];
            
            if (str.Equals("早安"))
            {
                Random ran = new Random();
                int n = ran.Next(1, 100);
                IMessageBuilder builder = new MessageBuilder();
                builder.AddPlainMessage($"早安哦！{e.Sender.Name}，送给你一个幸运数字：{n}，今天也要有个好心情啦！");
                await session.SendGroupMessageAsync(e.Sender.Group.Id, builder);
                return false;
            }
            if (str.Equals("晚安"))
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
