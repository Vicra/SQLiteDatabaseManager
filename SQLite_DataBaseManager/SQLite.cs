using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;

namespace SQLite_DataBaseManager
{
    public class SQLite
    {
        public SQLiteConnection dbConnection { get; set; }
        public SQLite()
        {

        }
        public void ConnectDatabase(string name, string password)
        {
            dbConnection = new SQLiteConnection("Data Source=" + name + ".sqlite;Version=3;Password  = " + password);
            dbConnection.Open();
        }
        public void ConnectDatabase(string name)
        {
            dbConnection = new SQLiteConnection("Data Source=" + name + ".sqlite;Version=3;");
            dbConnection.Open();
        }
        public void CloseConnection()
        {
            dbConnection.Close();
        }
        public void CreateDatabase(string name)
        {
            SQLiteConnection.CreateFile(name + ".sqlite");
            ConnectDatabase(name);

        }
        public void CreateDatabase(string name,string password)
        {
            SQLiteConnection.CreateFile(name+".sqlite");
            ConnectDatabase(name,password);
        }
        public void DeleteDatabase(string name)
        {
            if (System.IO.File.Exists("C:/Users/dell/Documents/Visual Studio 2013/Projects/SQLite_DataBaseManager/SQLite_DataBaseManager/bin/Debug/" + name + ".sqlite"))
                System.IO.File.Delete("C:/Users/dell/Documents/Visual Studio 2013/Projects/SQLite_DataBaseManager/SQLite_DataBaseManager/bin/Debug/" + name + ".sqlite"); 
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
            return reader.GetSchemaTable();
            //verificar la funcion getschema table
        }
        public DataTable GetTables()
        {
            return ExecuteWithResults("select name from sqlite_master where type = 'table';");
        }
        public DataTable GetIndexes()
        {
            return ExecuteWithResults("select name from sqlite_master where type = 'index';");
        }
        public DataTable GetTriggers()
        {
            return ExecuteWithResults("select name from sqlite_master where type = 'trigger';");
        }
        public DataTable GetTableColumns(string nombreTabla)
        {
            return ExecuteWithResults("pragma  table_info(" + nombreTabla + ");");
        }

        public void addData(){
            
        }
        public void read()
        {

            try
            {
                string sql = "select * from tabla1;";
                SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine("id: " + reader["id"] + "\tname: " + reader["name"]);

                }
                    
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
    }
}
