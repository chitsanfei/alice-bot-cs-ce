using System;
using alice_bot_cs.Tools;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace alice_bot_cs.Extensions
{
    public class RandomNBExtension
    {
        private string str = "";

        public RandomNBExtension()
        {
        }

        public string GetNBTalk()
        {
            str = HttpTool.Get("https://el-bot-api.vercel.app/api/words/niubi", "");
            ParseNB(str, "某人");
            return str;
        }

        public string GetNBTalk(string name)
        {
            str = HttpTool.Get("https://el-bot-api.vercel.app/api/words/niubi", "");
            ParseNB(str, name);
            return str;
        }

        private void ParseNB(string json, string name)
        {
            string talk = "";
            JArray ja = (JArray)JsonConvert.DeserializeObject(json);
            talk = ja[0].ToString();
            talk = Regex.Replace(talk, "\\${name}", name);
            LogExtension.Log("", "NB插件:有人请求了NB话");
            this.str = talk;
        }
    }
}
