using System.IO;
using alice_bot_cs.Tools;

namespace alice_bot_cs.Extensions.Setu
{
    public class RandomSetuFfsExtension
    {
        /*
         * 下为色图数据储存位置
         */
        private string setuData = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "data/RandomSetu");
        private string setuFile;
        private static string apiUrl = "http://random.firefliestudio.com/";
        
        public string GetSetu()
        {
            DownloadSetu();
            return setuFile;
        }

        private bool DownloadSetu()
        {
            string picName = RNGCryptoRandomService.GetRandomString(8);
            setuFile = Path.Combine(setuData, picName + ".jpg");
            byte[] pic = HttpTool.GetBytesFromUrl(apiUrl);
            HttpTool.WriteBytesToFile(this.setuFile, this.setuData, pic);
            return true;
        }
    }
}