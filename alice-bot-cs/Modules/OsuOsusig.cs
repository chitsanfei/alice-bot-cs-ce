using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using alice_bot_cs.Extensions;
using Mirai_CSharp;
using Mirai_CSharp.Models;
using Mirai_CSharp.Plugin.Interfaces;

namespace alice_bot_cs.Modules
{
    public partial class OsuOsusig : IGroupMessage 
    {
        public OsuOsusig() 
        {
        }

        public async Task<bool> GroupMessage(MiraiHttpSession session, IGroupMessageEventArgs e) 
        { 
            string str = string.Join(null, (IEnumerable<IMessageBase>) e.Chain);
            // IMessageBase plain = new PlainMessage("");
            if (str.Contains(".osusig"))
            {
                try
                {
                    int blank = str.Split(' ').Length - 1;
                    string name = str.Substring((str.IndexOf(" ")), (str.Length - str.IndexOf(" ")));
                    name = name.Replace(" ", "");
                    // plain = new PlainMessage(name);
                    LogExtension.Log("", name + " 尝试调用查询");

                    OsuOussigLoliconExtension oole = new OsuOussigLoliconExtension();
                    string path = oole.GetOsuSig(name);
                    await SendPictureAsync(session, path, e.Sender.Group.Id);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    throw;
                }
            }
            // await session.SendGroupMessageAsync(e.Sender.Group.Id, plain); // 向消息来源群异步发送由以上chain表示的消息
            return false; // 消息阻隔
        }
        
        private async Task SendPictureAsync(MiraiHttpSession session, string path, long target) // 发送图片方法
        {
            LogExtension.Log("", "OSUSIG:调用发送模块，路径为:" + path + " 目标群：" + target);
            ImageMessage msg = await session.UploadPictureAsync(UploadTarget.Group, path);
            IMessageBase[] chain = new IMessageBase[] { msg }; 
            await session.SendGroupMessageAsync(target, chain);
        }
    }
}