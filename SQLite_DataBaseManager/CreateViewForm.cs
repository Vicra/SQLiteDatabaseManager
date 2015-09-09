using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace SQLite_DataBaseManager
{
    public partial class CreateViewForm : Form
    {
        string sql = "";
        SQLite db;
        public CreateViewForm(SQLite database)
        {
            db = database;
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.GeneratedSQL.ReadOnly == false)
                this.GeneratedSQL.ReadOnly = true;
            else if (this.GeneratedSQL.ReadOnly )
                this.GeneratedSQL.ReadOnly = false;
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBox1.Text))
            {
                GenerarDDL();
                try
                {
                    string sql = this.GeneratedSQL.Text;
                    db.ExecuteNonQuery(sql);
                    MessageBox.Show("View " + textBox1.Text+" created succesfully");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("User please,... add a view name");
            }
            
            
        }
        

        private void textBox1_Leave(object sender, EventArgs e)
        {

            GenerarDDL();
        }

        private void richTextBox1_Leave(object sender, EventArgs e)
        {
            GenerarDDL();
        }
        private void GenerarDDL()
        {
            sql = "CREATE VIEW ";
            sql += this.textBox1.Text;
            sql += " AS " + this.richTextBox1.Text;
            this.GeneratedSQL.Text = sql;
        }

        private void tabPage1_Leave(object sender, EventArgs e)
        {
            GenerarDDL();
        }

    }
}
