using System;
using System.Data;
using System.Windows.Forms;

namespace SQLite_DataBaseManager
{
    public partial class CreateTriggerForm : Form
    {
        private SQLiteManager _sqliteManager;
        private string sql = "";
        public CreateTriggerForm(SQLiteManager manager, string table_name)
        {
            _sqliteManager = manager;
            InitializeComponent();
            this.comboBoxOnTable.Text = table_name;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(this.txtName.Text) || !String.IsNullOrEmpty(this.comboBoxAction.Text) || !String.IsNullOrEmpty(this.txtCode.Text))
            {
                GenerateDDL();
                if (!TriggerExists(this.comboBoxOnTable.Text,this.txtName.Text))
                {
                    try
                    {
                        string sql = this.generatedDDLRichTextBox.Text;
                        _sqliteManager.ExecuteNonQuery(sql);
                        MessageBox.Show("Trigger " + this.txtName.Text + " created successfully!");
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Trigger " + txtName.Text + " already exists.");
                }
            }
            else
            {
                MessageBox.Show("Missing input fields.");
            }
        }

        private bool TriggerExists(string tableName, string triggerName)
        {
            DataTable dataTable = _sqliteManager.GetTriggers(tableName);
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                if (dataTable.Rows[i].ItemArray[0].ToString() == triggerName)
                    return true;
            }
            return false;
        }

        private void GenerateDDL()
        {
            sql = "";
            string name = this.txtName.Text;
            string when = this.comboBoxWhen.Text;
            string action = this.comboBoxAction.Text;
            string table = this.comboBoxOnTable.Text;
            string scope = this.comboBoxScope.Text;
            string code = this.txtCode.Text;

            sql += "CREATE TRIGGER "+name+"\n";
            sql += "\t"+when+" ";
            sql += action;
            sql += "\n\t ON "+table+" \n";
            sql += scope+" ";

            sql += "\nBEGIN \n";
            sql += "\t"+code;
            sql += "\nEND;";

            this.generatedDDLRichTextBox.Text = sql;

        }

        private void tabControl1_Leave(object sender, EventArgs e)
        {
            GenerateDDL();
        }

        private void tabPage1_Leave(object sender, EventArgs e)
        {
            GenerateDDL();
        }
    }
}
