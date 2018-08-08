using System;
using System.Data.SQLite;
using System.Data;
using System.IO;

namespace SQLite_DataBaseManager
{
    public class SQLiteManager
    {
        public SQLiteConnection dbConnection { get; set; }
        public FileConnections fileConnections;
        public SQLiteManager()
        {
            fileConnections = new FileConnections();
        }
        public void ConnectDatabase(string name)
        {
            dbConnection = new SQLiteConnection("Data Source=" + name + ";Version=3;");
            dbConnection.Open();
        }

        public void createNewConnection(string filePathName)
        {
            string fileName = Path.GetFileName(filePathName);
            string path = Path.GetDirectoryName(filePathName);
            fileConnections.addConnection(fileName, path);
        }

        public void CloseConnection()
        {
            if (dbConnection != null)
            {
                dbConnection.Close();
            }
        }

        public void checkConnection(string filePathName)
        {
            SQLiteConnection testConnection;
            if (File.Exists(filePathName))
            {
                testConnection = new SQLiteConnection("Data Source=" + filePathName + ";Version=3");
                testConnection.Open();
                testConnection.Close();
            }
        }

        public void CreateDatabase(string name, string path, bool saveConnection)
        {
            SQLiteConnection.CreateFile(path + "\\" + name);
            var createDatabaseConnection = new SQLiteConnection("Data Source=" + path + "\\" + name + ";Version=3;");
            createDatabaseConnection.Open();
            createDatabaseConnection.Close();

            if (saveConnection)
            {
                fileConnections.addConnection(name, path);
            }
        }
        public void DeleteDatabase(string name)
        {
            if (System.IO.File.Exists("C:/Users/vicra/Documents/GitHub/SQLiteDatabaseManager/SQLite_DataBaseManager/bin/Debug/" + name + ".sqlite"))
                System.IO.File.Delete("C:/Users/vicra/Documents/GitHub/SQLiteDatabaseManager/SQLite_DataBaseManager/bin/Debug/" + name + ".sqlite"); 
        }
       
        public bool isConnected()
        {
            if (TestConnection() == "Test successfully commpleted")
                return true;
            return false;
        }
        public  string TestConnection()
        {
            try
            {
                this.dbConnection.Open();
                this.dbConnection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return "";
        }
        public void ExecuteNonQuery(string sql)
        {
            SQLiteCommand command = new SQLiteCommand(sql, this.dbConnection);
            command.ExecuteNonQuery();
        }
        public DataTable ExecuteWithResults(string sql)
        {
            SQLiteCommand command = new SQLiteCommand(sql, this.dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            DataTable dt= new DataTable();
            dt.Load(reader);
            return dt;
        }
       
        public DataTable GetTables()
        {
            return ExecuteWithResults("select name from sqlite_master where type = 'table';");
        }
        public DataTable GetIndexes(string table)
        {
            DataTable dt = new DataTable();
            dt= ExecuteWithResults("select name from sqlite_master where type = 'index' AND tbl_name = '"+table+"';");
            return dt;
        }
        public DataTable GetTriggers(string table)
        {
            DataTable dt = new DataTable();
            dt = ExecuteWithResults("select name from sqlite_master where type = 'trigger' AND tbl_name = '" + table + "';");
            return dt;
        }
        public DataTable GetViews()
        {
            return ExecuteWithResults("select name from sqlite_master where type = 'view';");
        }
        public DataTable GetTableColumns(string nombreTabla)
        {
            DataTable dt = new DataTable();
            dt = ExecuteWithResults("pragma  table_info(" + nombreTabla + ");");
            return dt;
        }
        public string GetDDL(string objectName, string type)
        {
            DataTable dt = new DataTable();
            dt = ExecuteWithResults("SELECT sql FROM sqlite_master where name = '"+objectName+"' AND type = '"+type+"';");
            return dt.Rows[0].ItemArray[0].ToString();
        }
        
    }
}
