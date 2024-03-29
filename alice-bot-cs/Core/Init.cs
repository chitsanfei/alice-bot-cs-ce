﻿using System;
using System.Data.SQLite;
using System.IO;
using alice_bot_cs.Entity.Core;
using alice_bot_cs.Entity.Plugins;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace alice_bot_cs.Core
{
    public class Init
    {
        /// <summary>
        /// 执行初始化方法
        /// </summary>
        public Init()
        {
            InitDirectory();
            InitCoreConfig();
            InitBotBehaviourConfig();
            InitBotDatabase();
            InitBotDatabaseConfig();
        }
        
        ///<summary>
        /// 初始化机器人所需文件夹信息
        ///</summary>
        /// <returns>执行情况</returns>
        private int InitDirectory() // 初始化文件夹
        {
            string currPath = AppDomain.CurrentDomain.BaseDirectory;
            string configPath = currPath + @"/config/";
            string dataPath = currPath + @"/data/";
            string databasePath = currPath + @"/database/";
            if (false == System.IO.Directory.Exists(configPath))
            {
                System.IO.Directory.CreateDirectory(configPath);
            }
            if (false == System.IO.Directory.Exists(dataPath))
            {
                System.IO.Directory.CreateDirectory(dataPath);
            }
            if (false == System.IO.Directory.Exists(databasePath))
            {
                System.IO.Directory.CreateDirectory(databasePath);
            }
            TraceLog.Log("", "初始化:数据文件夹:执行成功");
            return 0;
        }

        /// <summary>
        /// 初始化核心配置文件
        /// </summary>
        /// <returns>执行情况</returns>
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
                    account = "3410869782",
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
                TraceLog.Log("", "初始化:InitCoreConfig:执行成功");
                
            }
            return 0;
        }
        
        /// <summary>
        /// 初始化机器人所需行为基本参数信息，插件类为BotBehaviourControl
        /// </summary>
        /// <returns>执行情况</returns>
        private int InitBotBehaviourConfig()
        {
            string configPath = AppDomain.CurrentDomain.BaseDirectory + @"/config/";
            string configFilePath = configPath + @"BotBehaviourConfig.yaml";
            if (false == System.IO.File.Exists(configFilePath))
            {
                FileStream fs = new FileStream(configFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                StreamWriter sw = new StreamWriter(fs);
                sw.Close();
                var botBehaviourConfig = new BotBehaviourConfig
                {
                    request = new Request
                    {
                        friendRequest = "f",
                        groupRequest = "f",
                    },
                    menu = new Menu
                    {
                        help = "Alice Core Menu \n输入以下指令查看详情\n.list：查看可用指令\n.info：查看项目详情",
                        list = "可用指令如下：\n- .setu (api) 或 随机色图： 获取一张色图\n- 早安、晚安：一个简单的早晚安\n- .cat 或 随机猫猫：获得一个随机猫猫\n- .nb：生成一个nb话\n- .osusig id：查询osu资料\n- .setu api：查询可供使用的api",
                        info = "Project Alice - 一个多人协作写的屑QQBOT\n- 使用项目:Mirai、MiraiCS、MiraiHttp\n- 开发团队:https://github.com/MeowCatZ",
                    }
                };
                var serializer = new SerializerBuilder().WithNamingConvention(CamelCaseNamingConvention.Instance).Build();
                var yaml = serializer.Serialize(botBehaviourConfig);
                FileStream afs = new FileStream(configFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                StreamWriter asw = new StreamWriter(afs);
                asw.Write(yaml);
                asw.Close();
                TraceLog.Log("", "初始化:InitBotBehaviourConfig:执行成功");
            }
            return 0;
        }
      
        /// <summary>
        /// Init bot database using SQLite
        /// </summary>
        /// <returns>execution</returns>
        private int InitBotDatabase()
        {
            string dbPath = AppDomain.CurrentDomain.BaseDirectory + @"/database/";
            string dbFilePath = dbPath + @"Bot.db";
            
            if (false == System.IO.File.Exists(dbFilePath)) // 初始化鉴别
            {
                FileStream fs = new FileStream(dbFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                StreamWriter sw = new StreamWriter(fs);
                sw.Close();
                // SQLiteConnection.CreateFile(dbPath);
                
                if (Database.SqliteConnection.State != System.Data.ConnectionState.Open)
                {
                    Database.SqliteConnection.Open();
                    SQLiteCommand cmd = new SQLiteCommand();
                    cmd.Connection = Database.SqliteConnection;
                    
                    cmd.CommandText = "CREATE TABLE " + "user" +
                                      "(id int, qqnumber varchar, permission int, mcid varchar, osuid varchar, arcid bigint)";
                    cmd.ExecuteNonQuery();
                    
                    cmd.CommandText = "CREATE TABLE " + "qqgroup" +
                                      "(id int, qgnumber varchar, permission int, setuset int)";
                    cmd.ExecuteNonQuery();
                    
                    cmd.CommandText = "CREATE TABLE " + "config" +
                                      "(id int, subject varchar, data varchar)";
                    cmd.ExecuteNonQuery();
                    
                    Database.SqliteConnection.Close();
                }
            }
            
            TraceLog.Log("", "初始化:InitBotDatabase:执行成功");
            return 0;
        }

        /// <summary>
        /// Set bot config in scheme 'config' of sqlite database, including transforming data from yml text.
        /// 对本地sqlite数据库的机器人config进行初始化，包括对yml的转储。
        /// </summary>
        /// <returns>execution</returns>
        private int InitBotDatabaseConfig()
        {
            if (Database.SqliteConnection.State != System.Data.ConnectionState.Open)
            {
                // Deserialize BotBehaviourConfig
                // 初始化反序列化机器人参数文本
                var deserializer = new DeserializerBuilder()
                    .WithNamingConvention(CamelCaseNamingConvention.Instance)
                    .Build();
                string s = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"/config/BotBehaviourConfig.yaml");
                var c = deserializer.Deserialize<BotBehaviourConfig>(s);
                string menuHelp = c.menu.help;
                string menuList = c.menu.list;
                string menuInfo = c.menu.info;
                
                // Connect to sqlite Database
                // 连接到数据库
                Database.SqliteConnection.Open();
                SQLiteCommand cmd = new SQLiteCommand();
                cmd.Connection = Database.SqliteConnection;

                cmd.CommandText = "INSERT INTO " + "config" + " " + 
                                  $"VALUES ('1','menu_help', '{menuHelp}')";
                cmd.ExecuteNonQueryAsync();
                
                cmd.CommandText = "INSERT INTO " + "config" + " " + 
                                  $"VALUES ('2', 'menu_list', '{menuList}')";
                cmd.ExecuteNonQueryAsync();
                
                cmd.CommandText = "INSERT INTO " + "config" + " " + 
                                  $"VALUES ('3', 'menu_info', '{menuInfo}')";
                cmd.ExecuteNonQueryAsync();
                
                TraceLog.Log("", "初始化:InitBotDatabaseConfig:执行成功");
                Database.SqliteConnection.Close();
            }
            return 0;
        }
    }
}
