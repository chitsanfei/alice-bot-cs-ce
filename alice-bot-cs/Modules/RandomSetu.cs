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
            if (str.Contains("随机色图"))
            {
                LogExtension.Log("", "色图搜寻开始");
                IMessageBase plainStart = new PlainMessage($"正在为你寻找色图，稍安勿躁哦！");
                await session.SendGroupMessageAsync(e.Sender.Group.Id, plainStart);

                RandomSetuExtension rse = new RandomSetuExtension();
                rse.GetSetu(); // 调用获取色图方法
                url = rse.GetSetuUrl(); // 获得色图原url
                pid = rse.GetSetuPid(); // 获得色图pid
                LogExtension.Log("", "请求到内容：" + url + " pid为：" + pid);

                bool flag = rse.DownloadSetu(); // 下载色图，必须在获取色图后
                LogExtension.Log("", "下载模块返回：" + flag);
                path = rse.ReturnSetu(); // 返回路径，必须在下载色图后

                IMessageBase plainFetchedLine1 = new PlainMessage($"已解析到色图=w=\n");
                IMessageBase plainFetchedLine2 = new PlainMessage($"色图地址：{url}\n");
                IMessageBase plainFetchedLine3 = new PlainMessage($"色图PID：{pid}\n");
                IMessageBase plainFetchedLine4 = new PlainMessage($"下载错误情况：{flag}");
                await session.SendGroupMessageAsync(e.Sender.Group.Id, plainFetchedLine1, plainFetchedLine2, plainFetchedLine3, plainFetchedLine4);

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
