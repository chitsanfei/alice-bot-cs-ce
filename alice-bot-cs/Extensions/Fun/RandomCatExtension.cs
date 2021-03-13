using System;
using System.IO;
using alice_bot_cs.Entity;
using alice_bot_cs.Entity.Fun;
using alice_bot_cs.Tools;
using Newtonsoft.Json;

namespace alice_bot_cs.Extensions.Fun
{
    public class RandomCatExtension
    {
        private string URL;
        private string catData = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "data/RandomCat");
        private string catFile;

        public RandomCatExtension()
        {
        }

        public int GetCat() // 猫猫图片获取方法
        {
            string catJson = HttpTool.Get($"http://aws.random.cat/meow", "");
            ParseCat(catJson); // 处理猫猫api返回的信息
            return 0;
        }

        public string GetCatPath()
        {
            Random ran = new Random();
            int id = ran.Next(1, 10000);
            string path = null;

            if (URL.Contains("jpg"))
            {
                catFile = Path.Combine(catData, id + ".jpg");
            }
            else if (URL.Contains("png"))
            {
                catFile = Path.Combine(catData, id + ".png");
            }
            else
            {
                catFile = Path.Combine(catData, id + ".jpg");
            }

            byte[] pic = HttpTool.GetBytesFromUrl(this.URL);
            HttpTool.WriteBytesToFile(catFile, catData, pic);
            path = catFile;
            return path;
        }

        public string GetCatURL()
        {
            return URL;
        }

        private void ParseCat(string catJson)
        {
            CatJson result = JsonConvert.DeserializeObject<CatJson>(catJson);
            if (result.file.Length > 0)
            {
                URL = result.file;
            }
        }
    }
}
