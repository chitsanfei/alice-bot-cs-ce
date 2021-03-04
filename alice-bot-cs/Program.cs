using alice_bot_cs.Entity;
using alice_bot_cs.Extensions;
using alice_bot_cs.Habit;
using alice_bot_cs.Modules;
using alice_bot_cs.Tools;
using Mirai_CSharp;
using Mirai_CSharp.Example;
using Mirai_CSharp.Models;
using System;
using System.IO;
using System.Threading.Tasks;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace alice_bot_cs
{
    public static class Program
    {
        public static async Task Main()
        {
            Init init = new Init(); // 初始化参数文件
            /*
             * 下列是对coreconfig反序列化读取各项配置
             */
            var deserializer = new DeserializerBuilder().WithNamingConvention(UnderscoredNamingConvention.Instance).Build();
            string s = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"/config/coreconfig.yaml");
            var c = deserializer.Deserialize<CoreConfig>(s);
            string ip = c.ip;
            int port = int.Parse(c.port);
            string authkey = c.authkey;
            long qq = long.Parse(c.account);
            MiraiHttpSessionOptions options = new MiraiHttpSessionOptions(ip, port, authkey); //MiraiQQ Http
            await using MiraiHttpSession session = new MiraiHttpSession();
            /*
             * 插件装载
             */
            GoodMoringAndNight goodMoringAndNight = new GoodMoringAndNight(); // 早晚安插件
            GroupEventNotice groupEventNotice = new GroupEventNotice(); // 事件通知插件
            RandomSetu randomSetu = new RandomSetu();
            session.AddPlugin(goodMoringAndNight);
            LogExtension.Log("", "早晚安插件装载成功");
            session.AddPlugin(groupEventNotice);
            LogExtension.Log("", "事件通知插件装载成功");
            session.AddPlugin(randomSetu);
            LogExtension.Log("", "色图插件装载成功");
            LogExtension.Log("", "插件装载成功，Alice已经启动");
            /*
             * QQ
             */
            await session.ConnectAsync(options, qq); // QQ号
            while (true)
            {
                if (await Console.In.ReadLineAsync() == "exit")
                {
                    return;
                }
            }
        }
    }
}