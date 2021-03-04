using System;
using System.Collections.Generic;
using System.IO;
using alice_bot_cs.Entity;
using alice_bot_cs.Extensions;
using alice_bot_cs.Tools;
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
                return 0;
        }

        private int InitCoreConfig() // 初始化核心配置文件
        {
            string configPath = AppDomain.CurrentDomain.BaseDirectory + @"/config/";
            string configFilePath = configPath + @"coreconfig.yaml";
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
                LogExtension.Log("", "读写config的操作似乎执行了");
            }
            return 0;
        }
    }
}
