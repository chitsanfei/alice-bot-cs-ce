using System;
using System.Data.SQLite;

namespace alice_bot_cs.Core
{
    public class Database
    {
        public static string db = AppDomain.CurrentDomain.BaseDirectory + @"/database/Bot.db";
        public static SQLiteConnection SqliteConnection = new SQLiteConnection("data source=" + db);
    }
}