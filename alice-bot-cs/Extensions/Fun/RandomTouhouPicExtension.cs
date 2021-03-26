using System.IO;
using alice_bot_cs.Core;
using alice_bot_cs.Entity.Fun;
using alice_bot_cs.Tools;
using Newtonsoft.Json;

namespace alice_bot_cs.Extensions.Fun
{
    public class RandomTouhouPicExtension
    {
        private string _picData = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "data/RandomTouhouPic");
        private string _picFile;
        private static string _apiUrl = "https://img.paulzzh.tech/touhou/random";

        private string _picUrl = "";
        private string _picAuthor = "";
        private string _picTimestamp = "";
        
        public RandomTouhouPicExtension()
        { }
        
        public string GetTouhouPic()
        {
            DownloadTouhouPic();
            return _picFile;
        }

        private void ParseTouhouPic(string picJson)
        {
            TouhouPicJson result = JsonConvert.DeserializeObject<TouhouPicJson>(picJson);
            if(result.ToString() != null)
            {
                _picUrl = result.jpegurl;
                _picAuthor = result.author;
                _picTimestamp = result.timestamp;
            }
        }
        
        private bool DownloadTouhouPic()
        {
            string picName = RNGCryptoRandomService.GetRandomString(8);
            _picFile = Path.Combine(_picData, picName + ".jpeg");
            byte[] pic = HttpTool.GetBytesFromUrl(_apiUrl);
            HttpTool.WriteBytesToFile(this._picFile, this._picData, pic);
            return true;
        }

        public string GetAuthor()
        {
            return _picAuthor;
        }

        public string GetTimestamp()
        {
            return _picTimestamp;
        }
    }
}