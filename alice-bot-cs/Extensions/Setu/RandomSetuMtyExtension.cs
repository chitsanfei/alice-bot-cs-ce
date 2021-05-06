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
        private string _setuData = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "data/RandomSetu/mty");
        private string _setuFile;
        private static string _apiUrl = "https://api.mtyqx.cn/tapi/random.php?return=json";
        
        /*
         * 下为色图json解析后信息
         */
        private string _imgAcgurl = "";
        private string _imgCode = "";
        private string _imgWidth = "";
        private string _imgHeight = "";
        
        /// <summary>
        /// 获得色图
        /// </summary>
        /// <returns>色图地址</returns>
        public string GetSetu()
        {
            string setuJson = HttpTool.Get(_apiUrl,"");
            ParseSetu(setuJson);
            DownloadSetu();
            return _setuFile;
        }
        
        private void ParseSetu(string setuJson)
        {
            MtySetuJson result = JsonConvert.DeserializeObject<MtySetuJson>(setuJson);
            _imgCode = result.code;
            _imgHeight = result.height;
            _imgWidth = result.width;
            _imgAcgurl = result.imgurl;
        }

        private bool DownloadSetu()
        {
            string picName = RNGCryptoRandomService.GetRandomString(16);
            
            if (_imgAcgurl.Contains("jpg"))
            {
                _setuFile = Path.Combine(_setuData, picName + ".jpg");
            }
            else if(_imgAcgurl.Contains("png")){
                _setuFile = Path.Combine(_setuData, picName + ".png");
            }
            else
            {
                _setuFile = Path.Combine(_setuData, picName + ".jpg");
            }
            
            byte[] pic = HttpTool.GetBytesFromUrl(_imgAcgurl);
            HttpTool.WriteBytesToFile(this._setuFile, this._setuData, pic);
            return true;
        }

    }
}