using System;
using System.Collections.Generic;
using System.IO;
using alice_bot_cs.Entity;
using alice_bot_cs.Extensions;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace alice_bot_cs.Core
{
    public class Init
    {
        public Init()
        {
            InitDirectory();
            InitCoreConfig();
            InitBotBehaviourConfig();
        }

        private int InitDirectory() // 初始化文件夹
        {
            string currPath = AppDomain.CurrentDomain.BaseDirectory;
            string configPath = currPath + @"/config/";
            string dataPath = currPath + @"/data/";
            if (false == System.IO.Directory.Exists(configPath))
            {
                System.IO.Directory.CreateDirectory(configPath);
            }
            if (false == System.IO.Directory.Exists(dataPath))
            {
                System.IO.Directory.CreateDirectory(dataPath);
            }
            LogExtension.Log("", "初始化:数据文件夹:执行成功");
            return 0;
        }

        private int InitCoreConfig() // 初始化核心配置文件
        {
            string configPath = AppDomain.CurrentDomain.BaseDirectory + @"/config/";
            string configFilePath = configPath + @"CoreConfig.yaml";
            if (false == System.IO.File.Exists(configFilePath))
            {
                FileStream fs = new FileStream(configFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                StreamWriter sw = new StreamWriter(fs);
                sw.Close();
                var cc = new CoreConfig
                {
                    account = "2227391033",
                    ip = "127.0.0.1",
                    port = "8080",
                    authkey = "1145141919810",
                };
                var serializer = new SerializerBuilder().WithNamingConvention(CamelCaseNamingConvention.Instance).Build();
                var yaml = serializer.Serialize(cc);
                FileStream afs = new FileStream(configFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                StreamWriter asw = new StreamWriter(afs);
                asw.Write(yaml);
                asw.Close();
                LogExtension.Log("", "初始化:InitCoreConfig:执行成功");
            }
            return 0;
        }

        private int InitBotBehaviourConfig()
        {
            string configPath = AppDomain.CurrentDomain.BaseDirectory + @"/config/";
            string configFilePath = configPath + @"BotBehaviourConfig.yaml";
            if (false == System.IO.File.Exists(configFilePath))
            {
                FileStream fs = new FileStream(configFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                StreamWriter sw = new StreamWriter(fs);
                sw.Close();
                var bbc = new BotBehaviourConfig
                {
                    request = new Request
                    {
                        friendRequest = "f",
                        groupRequest = "f",
                    },
                    menu = new Menu
                    {
                        help = "Alice Core Menu \n输入以下指令查看详情\n.list:查看可用指令\n.info:查看项目详情",
                        list = "可用指令如下：\n- 随机色图:获取一张色图\n- 早安、晚安:一个简单的早晚安",
                        info = "Project Alice - 一个多人协作写的屑QQBOT\n- 使用项目:Mirai、MiraiCS、MiraiHttp\n- 开发团队:https://github.com/MeowCatZ",
                    }
                };
                var serializer = new SerializerBuilder().WithNamingConvention(CamelCaseNamingConvention.Instance).Build();
                var yaml = serializer.Serialize(bbc);
                FileStream afs = new FileStream(configFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                StreamWriter asw = new StreamWriter(afs);
                asw.Write(yaml);
                asw.Close();
                LogExtension.Log("", "初始化:InitBotBehaviourConfig:执行成功");
            }
            return 0;
        }
    }
}
