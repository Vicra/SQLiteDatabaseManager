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
    public partial class CreateTriggerForm : Form
    {
        SQLite db;
        string sql="";
        public CreateTriggerForm(SQLite database,string table_name)
        {
            db = database;
            InitializeComponent();
            this.OnTableComboBox.Text = table_name;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(this.txtName.Text) || !String.IsNullOrEmpty(this.ActionComboBox.Text) || !String.IsNullOrEmpty(this.CodeTextBox.Text))
            {
                GenerarDDL();
                if (!ExisteTrigger(this.OnTableComboBox.Text,this.txtName.Text))
                {
                    try
                    {
                        string sql = this.richTextBox2.Text;
                        db.ExecuteNonQuery(sql);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    MessageBox.Show("trigger " + this.txtName.Text + " created successfully!");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("La trigger " + txtName.Text + " ya existe");
                }
            }
            else
            {
                MessageBox.Show("missing fields");
            }
            
        }

        private bool ExisteTrigger(string tableName, string trigger)
        {
            DataTable dt = db.GetTriggers(tableName);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i].ItemArray[0].ToString() == trigger)
                    return true;
            }
            return false;
        }

        private void GenerarDDL()
        {
            sql = "";
            string name = this.txtName.Text;
            string when = this.WhenComboBox.Text;
            string action = this.ActionComboBox.Text;
            string table = this.OnTableComboBox.Text;
            string scope = this.ScopeComboBox.Text;
            string code = this.CodeTextBox.Text;

            sql += "CREATE TRIGGER "+name+"\n";
            sql += "\t"+when+" ";
            sql += action;
            sql += "\n\tON "+table+"\n";
            sql += scope;

            sql += "\nBEGIN \n";
            sql += "\t"+code;
            sql += "\nEND;";

            this.richTextBox2.Text = sql;

        }

        private void tabControl1_Leave(object sender, EventArgs e)
        {
            GenerarDDL();
        }

        private void tabPage1_Leave(object sender, EventArgs e)
        {
            GenerarDDL();
        }
    }
}
