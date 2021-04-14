using System.IO;
using alice_bot_cs.Core;
using alice_bot_cs.Tools;

namespace alice_bot_cs.Extensions.Setu
{
    public class RandomSetuToubExtension
    {
        /*
         * 下为色图数据储存位置
         */
        private string _setuData = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "data/RandomSetu/toub");
        private string _setuFile;
        private static string _apiUrl = "https://acg.toubiec.cn/random.php";
        
        public string GetSetu()
        {
            DownloadSetu();
            return _setuFile;
        }

        private bool DownloadSetu()
        {
            string picName = RNGCryptoRandomService.GetRandomString(16);
            _setuFile = Path.Combine(_setuData, picName + ".jpg");
            byte[] pic = HttpTool.GetBytesFromUrl(_apiUrl);
            HttpTool.WriteBytesToFile(this._setuFile, this._setuData, pic);
            return true;
        }
    }
}