using System;
using System.IO;
using System.Net;
using System.Text;

namespace alice_bot_cs.Tools
{
    public class HttpTool
    {
        public HttpTool()
        {
        }

        public static string Get(string Url, string postDataStr) // Http请求方法
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

        public static byte[] GetBytesFromUrl(string url) // 从Url读取流式数据
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

        public static void WriteBytesToFile(string fileName, string path, byte[] content) // 将流式数据写入储存
        {
            if (false == System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
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
