using System.Collections.Generic;
using System.IO;
using alice_bot_cs.Entity.Setu;
using alice_bot_cs.Tools;
using Newtonsoft.Json;

namespace alice_bot_cs.Extensions.Setu
{
    public class RandomSetuLoliconExtension // 随机色图插件
    {
        /*
         * 下为色图API及基本信息变量生成
         */
        string _apikey = "365007185fc06c84ac62e6";
        int _pid = 0;
        int _p = 0;
        int _uid = 0;
        string _title = "";
        string _author = "";
        string _originalUrl = "";
        string _largeUrl = "";
        bool _r18 = false;
        int _width = 0;
        int _height = 0;
        List<string> _tag;

        /*
         * 下为色图数据储存位置
         */
        string _setuData = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "data/RandomSetu/lolicon");
        string _setuFile;

        /*
         * todo:添加压缩图片方法，简化代码 @author MashiroSA 
         * todo:添加对r18的外部控制 @author MashiroSA 
         */
        public RandomSetuLoliconExtension() // 构造方法
        {
        }

        public int GetSetu() // 色图获取方法
        {
            string setuJson = HttpTool.Get($"http://api.lolicon.app/setu?apikey={_apikey}&r18=0","");
            ParseSetu(setuJson); // 处理由lolicon返回的json信息，并进行解析
            return 0;
        }

        public string ReturnSetu() // 色图文件存放地址的获取，这是一个测试的方法，以后会被删除
        {
            return _setuFile;
        }

        public string GetSetuUrl() // url获取，这是一个测试的方法，以后会被删除
        {
            return _originalUrl;
        }

        public int GetSetuPid() // pid，这是一个测试的方法，以后会被删除
        {
            return _pid;
        }

        private void ParseSetu(string setuJson)
        {
            LoliconJson result = JsonConvert.DeserializeObject<LoliconJson>(setuJson);
            if(result.code == 0)
            {
                _originalUrl = result.data[0].url;
                _pid = result.data[0].pid;
            }
        }

        public bool DownloadSetu()
        {
            if (_originalUrl.Contains("jpg"))
            {
                _setuFile = Path.Combine(_setuData, _pid + ".jpg");
            }
            else if(_originalUrl.Contains("png")){
                _setuFile = Path.Combine(_setuData, _pid + ".png");
            }
            else
            {
                _setuFile = Path.Combine(_setuData, _pid + ".jpg");
            }

            byte[] pic = HttpTool.GetBytesFromUrl(this._originalUrl);
            HttpTool.WriteBytesToFile(this._setuFile, this._setuData, pic);
            bool flag = false;
            return flag;
        }
    }
}
