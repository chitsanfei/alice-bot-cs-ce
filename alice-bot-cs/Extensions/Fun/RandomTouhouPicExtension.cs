using System.IO;
using alice_bot_cs.Core;
using alice_bot_cs.Entity.Fun;
using alice_bot_cs.Tools;
using Newtonsoft.Json;

namespace alice_bot_cs.Extensions.Fun
{
    public class RandomTouhouPicExtension
    {
        private string picData = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "data/RandomTouhouPic");
        private string picFile;
        private static string apiUrl = "https://img.paulzzh.tech/touhou/random";

        private string picUrl = "";
        private string picAuthor = "";
        private string PicTimestamp = "";
        
        public RandomTouhouPicExtension()
        { }
        
        public string GetTouhouPic()
        {
            DownloadTouhouPic();
            return picFile;
        }

        private void ParseTouhouPic(string picJson)
        {
            TouhouPicJson result = JsonConvert.DeserializeObject<TouhouPicJson>(picJson);
            if(result.ToString() != null)
            {
                picUrl = result.jpegurl;
                picAuthor = result.author;
                PicTimestamp = result.timestamp;
            }
        }
        
        private bool DownloadTouhouPic()
        {
            string picName = RNGCryptoRandomService.GetRandomString(8);
            picFile = Path.Combine(picData, picName + ".jpeg");
            byte[] pic = HttpTool.GetBytesFromUrl(apiUrl);
            HttpTool.WriteBytesToFile(this.picFile, this.picData, pic);
            return true;
        }

        public string GetAuthor()
        {
            return picAuthor;
        }

        public string GetTimestamp()
        {
            return PicTimestamp;
        }
    }
}