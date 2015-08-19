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
    public partial class CreateIndexForm : Form
    {
        string sql = "";
        SQLite db;
        public CreateIndexForm(SQLite database)
        {
            db = database;
            InitializeComponent();
            this.TablesGrid.Rows.Add();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            GenerarDDL();
            try
            {
                string sql = this.GeneratedSQL.Text;
                db.ExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void GenerarDDL()
        {
            sql = "CREATE"; 
            if (UniqueCheck.Checked)
            {
                sql += " UNIQUE ";
            }
            sql += " INDEX " + this.txtIndexName.Text;
            sql+= " ON "+TablesComboBox.Text+" (";

            sql += "\n );";
            this.GeneratedSQL.Text = sql;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tabControl1_Leave(object sender, EventArgs e)
        {
            GenerarDDL();
        }

        private void tabPage1_Leave(object sender, EventArgs e)
        {
            GenerarDDL();
        }

        private void PartialCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (this.richTextBox2.Enabled == false)
                this.richTextBox2.Enabled = true;
            else if (this.richTextBox2.Enabled)
                this.richTextBox2.Enabled = false;
        }
    }
}
