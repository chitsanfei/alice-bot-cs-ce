using System.Collections.Generic;

namespace alice_bot_cs.Entity.Fun
{
    public class TouhouPicJson
    {
        public string author { get; set; }
        public string jpegurl { get; set; }
        public string preview { get; set; }
        public string timestamp { get; set; }
        public string url { get; set; }
        public List<string> tags { get; set; }
    }
}