using System.IO;
using alice_bot_cs.Core;
using alice_bot_cs.Entity.Setu;
using alice_bot_cs.Tools;
using Newtonsoft.Json;

namespace alice_bot_cs.Extensions.Setu
{
    public class RandomSetuMtyExtension
    {
        /*
         * 下为色图数据储存位置
         */
        private string setuData = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "data/RandomSetu");
        private string setuFile;
        private static string apiUrl = "https://api.mtyqx.cn/tapi/random.php?return=json";
        
        /*
         * 下为色图json解析后信息
         */
        private string imgAcgurl = "";
        private string imgCode = "";
        private string imgWidth = "";
        private string imgHeight = "";
        
        public string GetSetu()
        {
            string setuJson = HttpTool.Get(apiUrl,"");
            ParseSetu(setuJson);
            DownloadSetu();
            return setuFile;
        }
        
        private void ParseSetu(string setuJson)
        {
            MtySetuJson result = JsonConvert.DeserializeObject<MtySetuJson>(setuJson);
            imgCode = result.code;
            imgHeight = result.height;
            imgWidth = result.width;
            imgAcgurl = result.imgurl;
        }

        private bool DownloadSetu()
        {
            string picName = RNGCryptoRandomService.GetRandomString(8);
            
            if (imgAcgurl.Contains("jpg"))
            {
                setuFile = Path.Combine(setuData, picName + ".jpg");
            }
            else if(imgAcgurl.Contains("png")){
                setuFile = Path.Combine(setuData, picName + ".png");
            }
            else
            {
                setuFile = Path.Combine(setuData, picName + ".jpg");
            }
            
            byte[] pic = HttpTool.GetBytesFromUrl(imgAcgurl);
            HttpTool.WriteBytesToFile(this.setuFile, this.setuData, pic);
            return true;
        }

    }
}