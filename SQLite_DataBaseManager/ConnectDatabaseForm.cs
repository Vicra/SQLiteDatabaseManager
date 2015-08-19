using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQLite_DataBaseManager
{
    public partial class ConnectDatabaseForm : Form
    {
        SQLite db ;
        string name;
        public ConnectDatabaseForm(string dbname, SQLite database)
        {
            db = database;
            InitializeComponent();
            name = dbname;
            this.DBNameLabel.Text = dbname;
        }

        private void ConnectDBbtn_Click(object sender, EventArgs e)
        {
            db.ConnectDatabase(name);
            this.Close();
            
        }

        // addindg new code here
        private void createTable()
        {
            string sql = "CREATE TABLE highscores (name VARCHAR(20), score INT)";
            ExcecuteQuery(sql);
        }

        private void ExcecuteQuery(string sql)
        {
            db.ExecuteNonQuery(sql);
        }

        private void addData()
        {
            string sql = "insert into highscores (name, score) values ('Me', 3000)";
            ExcecuteQuery(sql);
            sql = "insert into highscores (name, score) values ('Myself', 6000)";
            ExcecuteQuery(sql);
            sql = "insert into highscores (name, score) values ('And I', 9001)";
            ExcecuteQuery(sql);
        }


        //adding new code here

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            db.ConnectDatabase(name);
            if (db.isConnected())
            {
                this.label3.Text = "connected";
            }
            else
            {
                this.label3.Text = "unsecseful";
            }
            //string s = db.TestConnection();
            //this.label3.Text = s;
        }
    }
}
