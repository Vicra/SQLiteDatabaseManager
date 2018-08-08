using System;
using System.Data;
using System.Windows.Forms;

namespace SQLite_DataBaseManager
{
    public partial class CreateTableForm : Form
    {
        private SQLiteManager _sqliteManager;
        private string sql = "";
        public CreateTableForm(SQLiteManager manager)
        {
            _sqliteManager = manager;
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.generatedSQLRichTextBox.ReadOnly == false)
                this.generatedSQLRichTextBox.ReadOnly = true;
            else if (this.generatedSQLRichTextBox.ReadOnly)
                this.generatedSQLRichTextBox.ReadOnly = false;
        }

        private bool TableExists(string tableName)
        {
            DataTable dt = _sqliteManager.GetTables();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i].ItemArray[0].ToString() == tableName)
                    return true;
            }
            return false;
        }

        private void tabPage1_Leave(object sender, EventArgs e)
        {
            GenerateDDL();
        }

        private void GenerateDDL()
        {
            sql = "CREATE TABLE " + this.txtTableName.Text + " (";
            int n = this.ColumnsGrid.Rows.Count;
            var row = ColumnsGrid.Rows;
            for (int i = 0; i < n; i++)
            {
                var pk = row[i].Cells[0].Value;
                var nombreColumna = row[i].Cells[1].Value;
                var tipo_de_dato = row[i].Cells[2].Value;
                var tamano = row[i].Cells[3].Value;
                var noNulo = row[i].Cells[4].Value;

                sql += "\n    " + nombreColumna + " ";
                sql += tipo_de_dato + " ";
                if (tamano != null)
                {
                    sql += "(" + tamano + ") ";
                }
                if (noNulo != null)
                {
                    sql += "NOT NULL ";
                }
                if (pk != null && (bool)pk == true)
                {
                    sql += "PRIMARY KEY";
                }
                if (i != n - 1)
                {
                    sql += ", ";
                }
            }
            sql += "\n );";
            this.generatedSQLRichTextBox.Text = sql;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(this.txtTableName.Text))
            {
                GenerateDDL();
                if (!TableExists(this.txtTableName.Text))
                {
                    try
                    {
                        string sql = this.generatedSQLRichTextBox.Text;
                        _sqliteManager.ExecuteNonQuery(sql);
                        MessageBox.Show("Table " + this.txtTableName.Text + " created successfully!");
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        var errorMessage = ex.Message;
                        if (ex.InnerException != null && ex.InnerException.Message != null)
                        {
                            errorMessage += ex.InnerException.Message;
                        }
                        MessageBox.Show(errorMessage);
                    }
                }
                else
                {
                    MessageBox.Show("The table " + txtTableName.Text + " already exists.");
                }
            }
            else
            {
                MessageBox.Show("Missing input table name.");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void addColumnToolStripButton_Click(object sender, EventArgs e)
        {
            ColumnsGrid.Rows.Add();
        }

        private void deleteColumnToolStripBtn_Click(object sender, EventArgs e)
        {
            ColumnsGrid.Rows.Remove(ColumnsGrid.Rows[ColumnsGrid.SelectedCells[0].RowIndex]);
        }
    }
}
