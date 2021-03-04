using alice_bot_cs.Extensions;
using alice_bot_cs.Habit;
using Mirai_CSharp.Extensions;
using Mirai_CSharp.Models;
using Mirai_CSharp.Plugin.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Mirai_CSharp.Example
{
    public partial class GoodMoringAndNight : IGroupMessage
    {
        private int count = 1; // 计次
        private int day; // 计日程

        public GoodMoringAndNight() // 这是一个早晚安功能的模块，构造方法
        {
            CreateData(); // 需要创建数据文件
        }

        public void CreateData() // 创建txt作为发送者id数据存储
        {
            String path = AppDomain.CurrentDomain.BaseDirectory + @"/data/GoodMoringData.txt";
            if (false == System.IO.Directory.Exists(path))
            {
                FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                StreamWriter sw = new StreamWriter(fs);
                sw.Close();
            }
        }

        public int Checker(String sub) // 检查发送者是否重复发送早安
        {
            StreamReader sr = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + @"/data/GoodMoringData.txt", Encoding.Default);
            String line;
            int checkbox = 0;
            while ((line = sr.ReadLine()) != null)
            {
                //Console.WriteLine(line.ToString()); // 用来测试的
                if (line.ToString().Contains(sub))
                {
                    checkbox = 1;
                    break;
                }
            }
            return checkbox;
        }

        public void WriteData(string sub) // 写入发送者的QQ号
        {
            StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + @"/data/GoodMoringData.txt", true);
            sw.WriteLine(sub);
            sw.Flush();
            sw.Close();
        }

        public int Timer() // 检查时间，使得第二天的数据清除且计次重置
        {
            DateTime dt = new DateTime();
            day = dt.Day;
            if(day == dt.Day){
                return 0;
            }
            else
            {
                count = 1;
                DirectoryInfo d = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + @"/config/GoodMoringData.txt");
                LogExtension.Log("", "早晚安插件的重置发生，这是一个检查输出，当无问题时可以删除");
                d.Delete(true);
                CreateData();
                return 0;
            }
        }

        public async Task<bool> GroupMessage(MiraiHttpSession session, IGroupMessageEventArgs e) // 群消息处理方法
        {
            Timer();
            string str = string.Join(null, (IEnumerable<IMessageBase>)e.Chain);
            if (str.Contains("早安"))
            {
                int i = Checker(e.Sender.Id.ToString());
                IMessageBuilder builder = new MessageBuilder();
                if(i == 1)
                {
                    builder.AddPlainMessage($"{e.Sender.Name}，你已经起过床啦！");
                    await session.SendGroupMessageAsync(e.Sender.Group.Id, builder);
                    return false;
                }else
                if(i == 0){
                    builder.AddPlainMessage($"早安哦！{e.Sender.Name}，你是今天第{count}个起床的啦，今天也要有个好心情啦！");
                    await session.SendGroupMessageAsync(e.Sender.Group.Id, builder);
                    WriteData(e.Sender.Id.ToString());
                    count += 1;
                    return false;
                }
                else
                {
                    return true; // 出现问题，阻断消息发送
                }
            }
            if (str.Contains("晚安"))
            {
                IMessageBuilder builder = new MessageBuilder();
                builder.AddPlainMessage($"晚安哦！{e.Sender.Name}，要做个好梦哦！");
                await session.SendGroupMessageAsync(e.Sender.Group.Id, builder);
                return false;
            }
            return false;
        }
    }
}