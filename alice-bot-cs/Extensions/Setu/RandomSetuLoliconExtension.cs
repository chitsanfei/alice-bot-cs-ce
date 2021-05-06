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
        string _apikey = "365007185fc06c84ac62e6"; // 我忘记这个api是哪里找到的了，todo:建议修改api @author MashiroSA
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

        /// <summary>
        /// 获取色图
        /// </summary>
        /// <returns>执行结果</returns>
        public int GetSetu() // 色图获取方法
        {
            string setuJson = HttpTool.Get($"http://api.lolicon.app/setu?apikey={_apikey}&r18=0","");
            ParseSetu(setuJson); // 处理由lolicon返回的json信息，并进行解析
            return 0;
        }

        /// <summary>
        /// 返回色图存放地址
        /// </summary>
        /// <returns>色图存放地址</returns>
        public string ReturnSetu() // 色图文件存放地址的获取，这是一个测试的方法，以后会被删除
        {
            return _setuFile;
        }

        /// <summary>
        /// 获得色图的url
        /// </summary>
        /// <returns>色图的url</returns>
        public string GetSetuUrl() // url获取，这是一个测试的方法，以后会被删除
        {
            return _originalUrl;
        }

        /// <summary>
        /// 获得色图的pid
        /// </summary>
        /// <returns>色图的pid</returns>
        public int GetSetuPid() // pid，这是一个测试的方法，以后会被删除
        {
            return _pid;
        }

        /// <summary>
        /// 反序列化色图信息
        /// </summary>
        /// <param name="setuJson">待处理的信息</param>
        private void ParseSetu(string setuJson)
        {
            LoliconJson result = JsonConvert.DeserializeObject<LoliconJson>(setuJson);
            if(result.code == 0)
            {
                _originalUrl = result.data[0].url;
                _pid = result.data[0].pid;
            }
        }

        /// <summary>
        /// 下载色图
        /// </summary>
        /// <returns>下载色图的情况</returns>
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
