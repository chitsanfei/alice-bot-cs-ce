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
        private string _url;
        private string _catData = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "data/RandomCat");
        private string _catFile;

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

            if (_url.Contains("jpg"))
            {
                _catFile = Path.Combine(_catData, id + ".jpg");
            }
            else if (_url.Contains("png"))
            {
                _catFile = Path.Combine(_catData, id + ".png");
            }
            else
            {
                _catFile = Path.Combine(_catData, id + ".jpg");
            }

            byte[] pic = HttpTool.GetBytesFromUrl(this._url);
            HttpTool.WriteBytesToFile(_catFile, _catData, pic);
            path = _catFile;
            return path;
        }

        public string GetCatUrl()
        {
            return _url;
        }

        private void ParseCat(string catJson)
        {
            CatJson result = JsonConvert.DeserializeObject<CatJson>(catJson);
            if (result.file.Length > 0)
            {
                _url = result.file;
            }
        }
    }
}
