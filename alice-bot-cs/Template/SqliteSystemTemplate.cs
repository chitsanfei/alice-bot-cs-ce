using System;
using System.Data;
using System.Data.SQLite;

namespace alice_bot_cs.Template
{
    public class SqliteSystemTemplate
    {
        private string _dbName = "";
        private SQLiteConnection _sqLiteConn = null;     //连接对象
        private SQLiteTransaction _sqLiteTrans = null;   //事务对象
        private bool _isRunTrans = false;        //事务运行标识
        private string _sqLiteConnString = null; //连接字符串
        private bool _autoCommit = false; //事务自动提交标识
 
        public string sqLiteConnString
        {
            set { this._sqLiteConnString = value; }
            get { return this._sqLiteConnString; }
        }

        public SqliteSystemTemplate(string dbPath)
        {
            this._dbName = dbPath;
            this._sqLiteConnString = "Data Source=" + dbPath;
        }

        /// <summary>
        /// 新建数据库文件
        /// </summary>
        /// <param name="dbPath">数据库文件路径及名称</param>
        /// <returns>新建成功，返回true，否则返回false</returns>
        static public Boolean NewDbFile(string dbPath)
        {
            try
            {
                SQLiteConnection.CreateFile(dbPath);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("新建数据库文件" + dbPath + "失败：" + ex.Message);
            }
        }

        /// <summary>
        /// 创建表
        /// </summary>
        /// <param name="dbPath">指定数据库文件</param>
        /// <param name="tableName">表名称</param>
        static public void NewTable(string dbPath, string tableName)
        {
            SQLiteConnection sqliteConn = new SQLiteConnection("data source=" + dbPath);
            if (sqliteConn.State != System.Data.ConnectionState.Open)
                {
                    sqliteConn.Open();
                    SQLiteCommand cmd = new SQLiteCommand();
                    cmd.Connection = sqliteConn;
                    cmd.CommandText = "CREATE TABLE " + tableName + "(Name varchar,Team varchar, Number varchar)";
                    cmd.ExecuteNonQuery();
                }
            sqliteConn.Close();
        }

        /// <summary>
        /// 打开当前数据库的连接
        /// </summary>
        /// <returns></returns>
        public Boolean OpenDb()
        {
            try
            {
                this._sqLiteConn = new SQLiteConnection(this._sqLiteConnString);
                this._sqLiteConn.Open();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("打开数据库：" + _dbName + "的连接失败：" + ex.Message);
            }
        }
    
        /// <summary>
        /// 打开指定数据库的连接
        /// </summary>
        /// <param name="dbPath">数据库路径</param>
        /// <returns></returns>
        public Boolean OpenDb(string dbPath)
        {
            try
            {
                string sqliteConnString = "Data Source=" + dbPath;
                this._sqLiteConn = new SQLiteConnection(sqliteConnString);
                this._dbName = dbPath;
                this._sqLiteConnString = sqliteConnString;
                this._sqLiteConn.Open();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("打开数据库：" + dbPath + "的连接失败：" + ex.Message);
            }
        }
        
        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        public void CloseDb()
        {
            if (this._sqLiteConn != null && this._sqLiteConn.State != ConnectionState.Closed)
            {
                if (this._isRunTrans && this._autoCommit)
                {
                    this.Commit();
                }
                this._sqLiteConn.Close();
                this._sqLiteConn = null;
            }
        }
        
        /// <summary>
        /// 开始数据库事务
        /// </summary>
        public void BeginTransaction()
        {
            this._sqLiteConn.BeginTransaction();
            this._isRunTrans = true;
        }
        
        /// <summary>
        /// 开始数据库事务
        /// </summary>
        /// <param name="isoLevel">事务锁级别</param>
        public void BeginTransaction(IsolationLevel isoLevel)
        {
            this._sqLiteConn.BeginTransaction(isoLevel);
            this._isRunTrans = true;
        }
        
        /// <summary>
        /// 提交当前挂起的事务
        /// </summary>
        public void Commit()
        {
            if (this._isRunTrans)
            {
                    this._sqLiteTrans.Commit();
                    this._isRunTrans = false;
            }
        }
    }
}
