using System;
using System.IO;
using System.Threading;
using alice_bot_cs.Tools;

namespace alice_bot_cs.Extensions
{
    public class OsuOussigLoliconExtension
    {
        private string username = "";
        private string dataPath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "data/OsusigPic");
        private string dataFile = "";

        public string GetOsuSig(string username)
        {
            this.username = username;
            bool flag = DownloadOusSigPic();
            LogExtension.Log("", "OSUSIG:调用下载返回了:" + flag); 
            return dataFile;
        }

        private bool DownloadOusSigPic()
        {
            string color = "black";
            Random rd = new Random();
            int n = rd.Next(1, 5);
            switch (n)
            {
                case 1:
                    color = "yellow";
                    break;
                case 2:
                    color = "green";
                    break;
                case 3:
                    color = "blue";
                    break;
                case 4:
                    color = "pink";
                    break;
                case 5:
                    color = "purple";
                    break;
                default:
                    color = "black";
                    break;
            }

            LogExtension.Log("", $"OSUSIG查询:有人调用了查询，{username}");
            string api = $"https://osusig.lolicon.app/sig.php?colour={color}&uname={username}&pp=1&countryrank&removeavmargin&rankedscore&xpbar";
            dataFile = Path.Combine(dataPath, username + ".png");
            byte[] pic = HttpTool.GetBytesFromUrl(api);
            HttpTool.WriteBytesToFile(dataFile, dataPath, pic);
            return true;
        }
    }
}