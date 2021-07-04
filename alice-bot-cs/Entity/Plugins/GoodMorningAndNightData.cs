namespace alice_bot_cs.Entity.Plugins
{
    public class GoodMorningAndNightData
    {
        public string account { get; set; }
        public Details details { get; set; }
    }

    public class Details
    {
        public int isWakeUp { get; set; }
        public int isSleep { get; set; }
        public long wakeTime { get; set; }
        public long sleepTime { get; set; }
    }
}
