using System.Collections.Generic;
using System.Threading.Tasks;
using alice_bot_cs.Core;
using alice_bot_cs.Extensions.Fun;
using Mirai_CSharp;
using Mirai_CSharp.Models;
using Mirai_CSharp.Plugin.Interfaces;

namespace alice_bot_cs.Plugins
{
    public partial class RandomTouhouPic : IGroupMessage
    {
        private string _path = "";
        
        public RandomTouhouPic(){}
        
        public async Task<bool> GroupMessage(MiraiHttpSession session, IGroupMessageEventArgs e) 
        {
            string str = string.Join(null, (IEnumerable<IMessageBase>)e.Chain); // 取消息
            string[] strArray = str.Split(new char[2] { '[', ']' }); // 分割Mirai码部分
            str = strArray[2];
            // IMessageBase test = new PlainMessage(strArray[2]);
            // await session.SendGroupMessageAsync(e.Sender.Group.Id, test);
            /*
             * 请求随机东方数据
             */
            if (str.Equals("随机东方") || str.Equals(".touhou"))
            {
                TraceLog.Log("", "东方图片插件:东方图片搜寻开始");
                IMessageBase plainStart = new PlainMessage($"有车车人！正在与幻想乡通讯中");
                await session.SendGroupMessageAsync(e.Sender.Group.Id, plainStart);

                RandomTouhouPicExtension rtpe = new RandomTouhouPicExtension();
                _path = rtpe.GetTouhouPic(); // 获取东方图片
                TraceLog.Log("", "东方图片插件:已获取到东方图片");

                IMessageBase plainFetchedLine1 = new PlainMessage($"Alice寻找到了东方图片，正在尝试发送");
                await session.SendGroupMessageAsync(e.Sender.Group.Id, plainFetchedLine1);

                await SendPictureAsync(session, _path, e.Sender.Group.Id);
            }
            return false;
        }
        
        private async Task SendPictureAsync(MiraiHttpSession session, string path, long target) // 发送图片方法
        {
            TraceLog.Log("", "东方图片插件:调用图片发送模块，路径为:" + path + " 目标群：" + target);
            ImageMessage msg = await session.UploadPictureAsync(UploadTarget.Group, path);
            IMessageBase[] chain = new IMessageBase[] { msg }; 
            await session.SendGroupMessageAsync(target, chain);
        }
    }
}