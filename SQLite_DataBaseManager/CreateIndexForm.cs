using System;
using System.Data;
using System.Windows.Forms;

namespace SQLite_DataBaseManager
{
    public partial class CreateIndexForm : Form
    {
        private SQLiteManager _sqliteManager;
        private string sql = "";
        public CreateIndexForm(SQLiteManager manager, string tableName)
        {
            InitializeComponent(); 
            this.comboBoxTables.Text = tableName;
            _sqliteManager = manager;
            AddColumnsToGrid();
        }

        private void AddColumnsToGrid()
        {
            DataTable dataTable = new DataTable();
            dataTable = _sqliteManager.ExecuteWithResults("pragma table_info(" + this.comboBoxTables.Text + ");");
            dataGridViewTables.Rows.Add();
            dataGridViewTables.Rows[0].Cells[1].Value = dataTable.Rows[0].ItemArray[1].ToString();
            dataGridViewTables.Rows[0].Cells[2].Value = "BINARY";
            dataGridViewTables.Rows[0].Cells[3].Value = "ASC";
        }

        private bool IndexExists(string indexName, string tablename)
        {
            DataTable dataTable = _sqliteManager.GetIndexes(tablename);
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                if (dataTable.Rows[i].ItemArray[0].ToString() == indexName)
                    return true;
            }
            return false;
        }

        private void GenerateDDL()
        {
            try
            {
                sql = "CREATE";
                if (checkBoxIsUnique.Checked)
                {
                    sql += " UNIQUE ";
                }
                sql += " INDEX " + this.txtIndexName.Text;
                sql += " ON " + comboBoxTables.Text + " (\n";

                if (!String.IsNullOrEmpty(dataGridViewTables.Rows[0].Cells[1].Value.ToString()))
                {
                    sql += "\t" + dataGridViewTables.Rows[0].Cells[1].Value.ToString() + " ";
                }
                
                if (!String.IsNullOrEmpty(dataGridViewTables.Rows[0].Cells[2].Value.ToString()) && 
                    !String.IsNullOrEmpty(dataGridViewTables.Rows[0].Cells[3].Value.ToString()))
                {
                    sql += "COLLATE " + dataGridViewTables.Rows[0].Cells[2].Value.ToString() + " ";
                    sql += dataGridViewTables.Rows[0].Cells[3].Value.ToString() + "\n";
                }
                

                sql += " );";
                this.GeneratedSQL.Text = sql;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tabControl1_Leave(object sender, EventArgs e)
        {
            GenerateDDL();
        }

        private void tabPage1_Leave(object sender, EventArgs e)
        {
            GenerateDDL();
        }

        private void PartialCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (this.richTextBox2.Enabled == false)
                this.richTextBox2.Enabled = true;
            else if (this.richTextBox2.Enabled)
                this.richTextBox2.Enabled = false;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(this.txtIndexName.Text))
            {
                GenerateDDL();
                if (!IndexExists(this.txtIndexName.Text, comboBoxTables.Text))
                {
                    try
                    {
                        string sql = this.GeneratedSQL.Text;
                        _sqliteManager.ExecuteNonQuery(sql);
                        MessageBox.Show("Index " + this.txtIndexName.Text + " created successfully.");
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Index " + txtIndexName.Text + " already exists.");
                }
            }
            else
            {
                MessageBox.Show("Missing index name.");
            }
        }
    }
}
