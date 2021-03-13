using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using alice_bot_cs.Entity;
using alice_bot_cs.Tools;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace alice_bot_cs.Extensions
{
    public class RandomSetuLoliconExtension // 随机色图插件
    {
        /*
         * 下为色图API及基本信息变量生成
         */
        string apikey = "365007185fc06c84ac62e6";
        int pid = 0;
        int p = 0;
        int uid = 0;
        string title = "";
        string author = "";
        string originalUrl = "";
        string largeUrl = "";
        bool r18 = false;
        int width = 0;
        int height = 0;
        List<string> tag;

        /*
         * 下为色图数据储存位置
         */
        string setuData = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "data/RandomSetu");
        string setuFile;

        /*
         * todo:添加压缩图片方法，简化代码 @author MashiroSA 
         * todo:添加对r18的外部控制 @author MashiroSA 
         */
        public RandomSetuLoliconExtension() // 构造方法
        {
        }

        public int GetSetu() // 色图获取方法
        {
            string setuJson = HttpTool.Get($"http://api.lolicon.app/setu?apikey={apikey}&r18=0","");
            ParseSetu(setuJson); // 处理由lolicon返回的json信息，并进行解析
            return 0;
        }

        public string ReturnSetu() // 色图文件存放地址的获取，这是一个测试的方法，以后会被删除
        {
            return setuFile;
        }

        public string GetSetuUrl() // url获取，这是一个测试的方法，以后会被删除
        {
            return originalUrl;
        }

        public int GetSetuPid() // pid，这是一个测试的方法，以后会被删除
        {
            return pid;
        }

        private void ParseSetu(string setuJson)
        {
            LoliconJson result = JsonConvert.DeserializeObject<LoliconJson>(setuJson);
            if(result.code == 0)
            {
                originalUrl = result.data[0].url;
                pid = result.data[0].pid;
            }
        }

        public bool DownloadSetu()
        {
            if (originalUrl.Contains("jpg"))
            {
                setuFile = Path.Combine(setuData, pid + ".jpg");
            }
            else if(originalUrl.Contains("png")){
                setuFile = Path.Combine(setuData, pid + ".png");
            }
            else
            {
                setuFile = Path.Combine(setuData, pid + ".jpg");
            }

            byte[] pic = HttpTool.GetBytesFromUrl(this.originalUrl);
            HttpTool.WriteBytesToFile(this.setuFile, this.setuData, pic);
            bool flag = false;
            return flag;
        }
    }
}
