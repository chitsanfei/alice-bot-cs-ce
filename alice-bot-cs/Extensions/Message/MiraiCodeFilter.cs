namespace alice_bot_cs.Extensions.Message
{
    public static class MiraiCodeFilter
    {
        /// <summary>
        /// 分割Mirai码(MiraiCS实现的前缀)
        /// </summary>
        /// <param name="message">需要分割的消息</param>
        /// <returns>分割后的消息</returns>
        public static string MessageDivisionMiraiPrefix(string message)
        {
            string[] strArray = message.Split(new char[2] { '[', ']' }); // 分割Mirai码部分
            message = strArray[2];
            return message;
        }
    }
}