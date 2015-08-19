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
    public partial class SQLEditorForm : Form
    {
        SQLite db;
        public SQLEditorForm(SQLite database)
        {
            db = database;
            InitializeComponent();
        }

        private void toolStripSplitButton1_ButtonClick(object sender, EventArgs e)
        {

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void RunAction_Click(object sender, EventArgs e)
        {
            string sql;
            if (this.SQLEditor.SelectedText != null)
                sql = this.SQLEditor.SelectedText;
            else
                sql = this.SQLEditor.Text;
            try
            {
                db.ExecuteNonQuery(sql);
                this.richTextBox1.Text += "sql ejecutado";
            }
            catch(Exception ex)
            {
                this.richTextBox1.Text += ex.ToString() + "\n" + "\n";
            }
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void SQLEditor_TextChanged(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.richTextBox1.Clear();
        }
        
    }
}
