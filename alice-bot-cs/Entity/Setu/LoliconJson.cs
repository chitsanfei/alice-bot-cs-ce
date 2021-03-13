using System.Collections.Generic;

namespace alice_bot_cs.Entity.Setu
{
    public class LoliconJson // 这是插件RandomSetu的实体类，用于对json的反序列化用
    {
        public int code { get; set; }
        public string msg { get; set; }
        public int quota { get; set; }
        public int quota_min_ttl { get; set; }
        public int count { get; set; }
        public List<SetuImageJson> data { get; set; }
    }

    public class SetuImageJson
    {
        public int pid { get; set; }
        public int p { get; set; }
        public int uid { get; set; }
        public string title { get; set; }
        public string author { get; set; }
        public string url { get; set; }
        public bool r18 { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public List<string> tags { get; set; }
    }
}
