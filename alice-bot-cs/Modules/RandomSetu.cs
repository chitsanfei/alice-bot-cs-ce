using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using alice_bot_cs.Extensions;
using Mirai_CSharp;
using Mirai_CSharp.Models;
using Mirai_CSharp.Plugin.Interfaces;

namespace alice_bot_cs.Modules
{
    public partial class RandomSetu: IGroupMessage
    {
        string url;
        string path;
        int pid;

        public RandomSetu()
        {
        }

        public async Task<bool> GroupMessage(MiraiHttpSession session, IGroupMessageEventArgs e)
        {
            string str = string.Join(null, (IEnumerable<IMessageBase>)e.Chain);
            if (str.Contains("搞色图"))
            {
                LogExtension.Log("", "色图搜寻开始");
                RandomSetuExtension rse = new RandomSetuExtension();
                rse.getSetu();
                url = rse.getSetuUrl();
                pid = rse.getSetuPid();
                LogExtension.Log("", "请求到内容：" + url + " pid为：" + pid);
                bool flag = rse.downloadSetu();
                LogExtension.Log("", "下载模块返回：" + flag);
                path = rse.returnSetu();
                await SendPictureAsync(session, path, e.Sender.Group.Id);
            }
            return false;
        }

        private async Task SendPictureAsync(MiraiHttpSession session, string path, long target) // 发送图片方法
        {
            LogExtension.Log("","调用色图发送模块，路径为:" + path + " 目标群：" + target);
            ImageMessage msg = await session.UploadPictureAsync(UploadTarget.Group, path);
            IMessageBase[] chain = new IMessageBase[] { msg }; 
            await session.SendGroupMessageAsync(target, chain);
        }
    }
}
