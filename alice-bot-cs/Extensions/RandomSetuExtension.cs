using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using alice_bot_cs.Entity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace alice_bot_cs.Extensions
{
    public class RandomSetuExtension // 随机色图插件
    {
        /*
         * 下为色图API及基本信息变量生成
         */
        string Apikey = "365007185fc06c84ac62e6";
        int pid = 0;
        int p = 0;
        int uid = 0;
        string title = "";
        string author = "";
        string originalurl = "";
        string largeurl = "";
        bool r18 = false;
        int width = 0;
        int height = 0;
        List<string> tag;
        /*
         * 下为色图数据储存位置
         */
        string setudata = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "data/RandomSetu");
        string setufile;
        /*
         * todo:添加压缩图片方法，简化代码 @author MashiroSA 
         * todo:添加对r18的外部控制 @author MashiroSA 
         */

        public RandomSetuExtension() // 构造方法
        {
        }

        public static string HttpGet(string Url, string postDataStr) // Http请求方法
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url + (postDataStr == "" ? "" : "?") + postDataStr);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return retString;
        }

        public int getSetu() // 色图获取方法
        {
            string setuJson = HttpGet($"http://api.lolicon.app/setu?apikey={Apikey}&r18=0","");
            parseSetu(setuJson); // 处理由lolicon返回的json信息，并进行解析
            return 0;
        }

        public string returnSetu() // 色图文件存放地址的获取，这是一个测试的方法，以后会被删除
        {
            return setufile;
        }

        public string getSetuUrl() // url获取，这是一个测试的方法，以后会被删除
        {
            return originalurl;
        }

        public int getSetuPid() // pid，这是一个测试的方法，以后会被删除
        {
            return pid;
        }

        private void parseSetu(string setuJson)
        {
            LoliconJson result = JsonConvert.DeserializeObject<LoliconJson>(setuJson);
            if(result.code == 0)
            {
                originalurl = result.data[0].url;
                pid = result.data[0].pid;
            }
        }

        public bool downloadSetu()
        {
            if (originalurl.Contains("jpg"))
            {
                setufile = Path.Combine(setudata, pid + ".jpg");
            }
            else if(originalurl.Contains("png")){
                setufile = Path.Combine(setudata, pid + ".png");
            }
            else
            {
                setufile = Path.Combine(setudata, pid + ".jpg");
            }
            byte[] pic = GetBytesFromUrl(this.originalurl);
            WriteBytesToFile(this.setufile, pic);
            bool flag = false;
            return flag;
        }

        public byte[] GetBytesFromUrl(string url) // 从Url读取流式数据
        {
            byte[] b = null;
            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(url);
            WebResponse myResp = myReq.GetResponse();
            using (Stream stream = myResp.GetResponseStream())
            using (MemoryStream ms = new MemoryStream())
            {
                int count = 0;
                do
                {
                    byte[] buf = new byte[1024];
                    count = stream.Read(buf, 0, 1024);
                    ms.Write(buf, 0, count);
                } while (stream.CanRead && count > 0);
                b = ms.ToArray();
            }
            return b;
        }

        public void WriteBytesToFile(string fileName, byte[] content) // 将流式数据写入储存
        {
            if (false == System.IO.Directory.Exists(setudata))
            {
                System.IO.Directory.CreateDirectory(setudata);
            }
            FileStream fs = new FileStream(fileName, FileMode.Create);
            BinaryWriter w = new BinaryWriter(fs);
            try
            {
                w.Write(content);
            }
            finally
            {
                fs.Close();
                w.Close();
            }
        }
    }
}
