using System;
using System.Windows.Forms;

namespace SQLite_DataBaseManager
{
    public partial class CreateViewForm : Form
    {
        private SQLiteManager _sqliteManager;
        private string sql = "";
        public CreateViewForm(SQLiteManager manager)
        {
            _sqliteManager = manager;
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.generatedDDLRichTextBox.ReadOnly == false)
                this.generatedDDLRichTextBox.ReadOnly = true;
            else if (this.generatedDDLRichTextBox.ReadOnly )
                this.generatedDDLRichTextBox.ReadOnly = false;
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtViewName.Text))
            {
                GenerateDDL();
                try
                {
                    string sql = this.generatedDDLRichTextBox.Text;
                    _sqliteManager.ExecuteNonQuery(sql);
                    MessageBox.Show("View " + txtViewName.Text+" created succesfully.");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Missing view name field.");
            }
        }
        

        private void textBox1_Leave(object sender, EventArgs e)
        {
            GenerateDDL();
        }

        private void richTextBox1_Leave(object sender, EventArgs e)
        {
            GenerateDDL();
        }
        private void GenerateDDL()
        {
            sql = "CREATE VIEW ";
            sql += this.txtViewName.Text;
            sql += " AS " + this.richTextBox1.Text;
            this.generatedDDLRichTextBox.Text = sql;
        }

        private void tabPage1_Leave(object sender, EventArgs e)
        {
            GenerateDDL();
        }
    }
}
