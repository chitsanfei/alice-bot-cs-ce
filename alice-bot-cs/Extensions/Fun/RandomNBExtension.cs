using System;
using alice_bot_cs.Tools;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using alice_bot_cs.Core;

namespace alice_bot_cs.Extensions.Fun
{
    public class RandomNBExtension
    {
        private string _str = "";

        public RandomNBExtension()
        {
        }

        public string GetNbTalk()
        {
            _str = HttpTool.Get("https://el-bot-api.vercel.app/api/words/niubi", "");
            ParseNb(_str, "某人");
            return _str;
        }

        public string GetNbTalk(string name)
        {
            _str = HttpTool.Get("https://el-bot-api.vercel.app/api/words/niubi", "");
            ParseNb(_str, name);
            return _str;
        }

        private void ParseNb(string json, string name)
        {
            string talk = "";
            JArray ja = (JArray)JsonConvert.DeserializeObject(json);
            talk = ja[0].ToString();
            talk = Regex.Replace(talk, "\\${name}", name);
            TraceLog.Log("", "NB插件:有人请求了NB话");
            this._str = talk;
        }
    }
}
