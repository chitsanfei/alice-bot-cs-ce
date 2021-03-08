using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using alice_bot_cs.Extensions;
using Mirai_CSharp;
using Mirai_CSharp.Models;
using Mirai_CSharp.Plugin.Interfaces;

namespace alice_bot_cs.Modules
{
    public partial class RandomCat : IGroupMessage
    {
        string url;
        string path;

        public RandomCat()
        {
        }

        public async Task<bool> GroupMessage(MiraiHttpSession session, IGroupMessageEventArgs e)
        {
            string str = string.Join(null, (IEnumerable<IMessageBase>)e.Chain);
            if (str.Contains("随机猫猫"))
            {
                LogExtension.Log("", "猫猫搜寻开始");
                IMessageBase plainStart = new PlainMessage($"正在为你寻找猫猫，稍安勿躁哦！");
                await session.SendGroupMessageAsync(e.Sender.Group.Id, plainStart);

                RandomCatExtension rc = new RandomCatExtension();
                rc.GetCat();
                path = rc.GetCatPath();
                url = rc.GetCatURL();

                IMessageBase plainFetchedLine1 = new PlainMessage($"已解析到猫猫哦\n");
                IMessageBase plainFetchedLine2 = new PlainMessage($"猫猫地址：{url}\n正在调用发送方法发送猫猫");
                await session.SendGroupMessageAsync(e.Sender.Group.Id, plainFetchedLine1, plainFetchedLine2);

                await SendPictureAsync(session, path, e.Sender.Group.Id);
            }
            return false;
        }

        private async Task SendPictureAsync(MiraiHttpSession session, string path, long target) // 发送图片方法
        {
            LogExtension.Log("", "调用猫猫发送模块，路径为:" + path + " 目标群：" + target);
            ImageMessage msg = await session.UploadPictureAsync(UploadTarget.Group, path);
            IMessageBase[] chain = new IMessageBase[] { msg };
            await session.SendGroupMessageAsync(target, chain);
        }
    }
}
