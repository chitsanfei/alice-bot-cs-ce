using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using alice_bot_cs.Entity;
using alice_bot_cs.Entity.Setu;
using alice_bot_cs.Tools;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace alice_bot_cs.Extensions.Setu
{
    public class RandomSetuElExtension // 随机色图插件，使用的是Elbot的API
    {
        /*
         * 下为色图API及基本信息变量生成
         */
        int _pid = 0;
        int _p = 0;
        int _uid = 0;
        string _title = "";
        string _author = "";
        string _originalUrl = "";
        string _largeUrl = "";
        bool _r18 = false;
        int _width = 0;
        int _height = 0;
        List<string> _tags;

        /*
         * 下为色图数据储存位置
         */
        string _setuData = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "data/RandomSetu/elbot");
        string _setuFile;

        public RandomSetuElExtension() // 构造方法
        {
        }

        public int GetSetu() // 色图获取方法
        {
            string setuJson = HttpTool.Get($"https://el-bot-api.vercel.app/api/setu", "");
            ParseSetu(setuJson); // 处理由lolicon返回的json信息，并进行解析
            return 0;
        }

        public string ReturnSetu() // 色图文件存放地址的获取，这是一个测试的方法，以后会被删除
        {
            return _setuFile;
        }

        public string GetSetuUrl() // url获取，这是一个测试的方法，以后会被删除
        {
            return _originalUrl;
        }

        public int GetSetuPid() // pid，这是一个测试的方法，以后会被删除
        {
            return _pid;
        }

        private void ParseSetu(string setuJson)
        {
            ElbotSetuJson result = JsonConvert.DeserializeObject<ElbotSetuJson>(setuJson);
            if (result.ToString() != null)
            {
                _originalUrl = result.url;
                _pid = result.pid;
            }
        }

        public bool DownloadSetu()
        {
            if (_originalUrl.Contains("jpg"))
            {
                _setuFile = Path.Combine(_setuData, _pid + ".jpg");
            }
            else if (_originalUrl.Contains("png"))
            {
                _setuFile = Path.Combine(_setuData, _pid + ".png");
            }
            else
            {
                _setuFile = Path.Combine(_setuData, _pid + ".jpg");
            }

            byte[] pic = HttpTool.GetBytesFromUrl(_originalUrl);
            HttpTool.WriteBytesToFile(_setuFile, _setuData, pic);
            bool flag = false;
            return flag;
        }
    }
}
