using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using alice_bot_cs.Core;
using alice_bot_cs.Extensions;
using alice_bot_cs.Extensions.Setu;
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
            string str = string.Join(null, (IEnumerable<IMessageBase>)e.Chain); // 取消息
            string[] strArray = str.Split(new char[2] { '[', ']' }); // 分割Mirai码部分
            str = strArray[2];

            /*
             * 色图api的提醒方法
             */
            if (str.Equals(".setu api"))
            {
                IMessageBase tips = new PlainMessage($"- 随机色图支持的api有：lolicon elbot ecy mty\n- 您可以使用.setu [api名称]（如.setu lolicon）请求对应api的色图 ");
                await session.SendGroupMessageAsync(e.Sender.Group.Id, tips);
            }

            /*
             * 请求lolicon api数据
             */
            if (str.Equals("随机色图") || str.Equals(".setu lolicon") || str.Equals(".setu"))
            {
                if (str.Equals(".setu"))
                {
                    IMessageBase warning = new PlainMessage($"您并未指定使用的api，Alice将默认使用Lolicon API");
                    await session.SendGroupMessageAsync(e.Sender.Group.Id, warning);
                }
                
                TraceLog.Log("", "色图插件:对LoliconApi的色图搜寻开始");
                IMessageBase plainStart = new PlainMessage($"正在为你寻找色图，稍安勿躁哦！\n目标API:Lolicon API");
                await session.SendGroupMessageAsync(e.Sender.Group.Id, plainStart);

                RandomSetuLoliconExtension rsle = new RandomSetuLoliconExtension();
                rsle.GetSetu(); // 调用获取色图方法
                url = rsle.GetSetuUrl(); // 获得色图原url
                pid = rsle.GetSetuPid(); // 获得色图pid
                TraceLog.Log("", "色图插件:请求到内容：" + url + " pid为：" + pid);

                bool flag = rsle.DownloadSetu(); // 下载色图，必须在获取色图后
                TraceLog.Log("", "色图插件:下载模块返回：" + flag);
                path = rsle.ReturnSetu(); // 返回路径，必须在下载色图后

                IMessageBase plainFetchedLine1 = new PlainMessage($"Alice已寻找到色图，正在调用发送方法\n");
                IMessageBase plainFetchedLine2 = new PlainMessage($"色图地址：{url}\n");
                IMessageBase plainFetchedLine3 = new PlainMessage($"色图PID：{pid}\n");
                IMessageBase plainFetchedLine4 = new PlainMessage($"下载错误情况：{flag}");
                await session.SendGroupMessageAsync(e.Sender.Group.Id, plainFetchedLine1, plainFetchedLine2, plainFetchedLine3, plainFetchedLine4);

                await SendPictureAsync(session, path, e.Sender.Group.Id);
            }

            /*
             * 请求elbot api数据
             */
            if (str.Equals(".setu elbot"))
            {
                TraceLog.Log("", "色图插件:对ElbotApi的色图搜寻开始");
                IMessageBase plainStart = new PlainMessage($"正在为你寻找色图，稍安勿躁哦！\n目标API:Elbot API");
                await session.SendGroupMessageAsync(e.Sender.Group.Id, plainStart);

                RandomSetuElExtension rsee = new RandomSetuElExtension();
                rsee.GetSetu(); // 调用获取色图方法
                url = rsee.GetSetuUrl(); // 获得色图原url
                pid = rsee.GetSetuPid(); // 获得色图pid
                TraceLog.Log("", "色图插件:请求到内容：" + url + " pid为：" + pid);

                bool flag = rsee.DownloadSetu(); // 下载色图，必须在获取色图后
                TraceLog.Log("", "色图插件:下载模块返回：" + flag);
                path = rsee.ReturnSetu(); // 返回路径，必须在下载色图后

                IMessageBase plainFetchedLine1 = new PlainMessage($"Alice已寻找到色图，正在调用发送方法\n");
                IMessageBase plainFetchedLine2 = new PlainMessage($"色图地址：{url}\n");
                IMessageBase plainFetchedLine3 = new PlainMessage($"色图PID：{pid}\n");
                IMessageBase plainFetchedLine4 = new PlainMessage($"下载错误情况：{flag}");
                await session.SendGroupMessageAsync(e.Sender.Group.Id, plainFetchedLine1, plainFetchedLine2, plainFetchedLine3, plainFetchedLine4);

                await SendPictureAsync(session, path, e.Sender.Group.Id);
            }
            
            /*
             * 请求ecy api数据
             */
            if (str.Equals(".setu ecy"))
            {
                TraceLog.Log("", "色图插件:对EcyApi的色图搜寻开始");
                IMessageBase plainStart = new PlainMessage($"正在为你寻找色图，稍安勿躁哦！\n目标API:Ecy API");
                await session.SendGroupMessageAsync(e.Sender.Group.Id, plainStart);

                RandomSetuEcyExtension ecy = new RandomSetuEcyExtension();
                path = ecy.GetSetu();

                IMessageBase plainFetchedLine1 = new PlainMessage($"Alice已寻找到色图，正在调用发送方法\n请注意，该API不支持详细信息解析");
                await session.SendGroupMessageAsync(e.Sender.Group.Id, plainFetchedLine1);

                await SendPictureAsync(session, path, e.Sender.Group.Id);
            }
            
            /*
             * 请求墨天逸 api数据
             */
            if (str.Equals(".setu mty"))
            {
                TraceLog.Log("", "色图插件:对MtyApi的色图搜寻开始");
                IMessageBase plainStart = new PlainMessage($"正在为你寻找色图，稍安勿躁哦！\n目标API:墨天逸 API");
                await session.SendGroupMessageAsync(e.Sender.Group.Id, plainStart);

                RandomSetuMtyExtension rsme = new RandomSetuMtyExtension();
                path = rsme.GetSetu();

                IMessageBase plainFetchedLine1 = new PlainMessage($"Alice已寻找到色图，正在调用发送方法\n请注意，该API不支持详细信息解析");
                await session.SendGroupMessageAsync(e.Sender.Group.Id, plainFetchedLine1);

                await SendPictureAsync(session, path, e.Sender.Group.Id);
            }
            return false;
        }

        private async Task SendPictureAsync(MiraiHttpSession session, string path, long target) // 发送图片方法
        {
            TraceLog.Log("", "色图插件:调用色图发送模块，路径为:" + path + " 目标群：" + target);
            ImageMessage msg = await session.UploadPictureAsync(UploadTarget.Group, path);
            IMessageBase[] chain = new IMessageBase[] { msg }; 
            await session.SendGroupMessageAsync(target, chain);
        }
    }
}
