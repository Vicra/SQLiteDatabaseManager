using System;
using System.Data;
using System.Windows.Forms;

namespace SQLite_DataBaseManager
{
    public partial class AddDataForm : Form
    {
        private SQLiteManager _sqliteManager;
        private string _tableName;
        private string _sql;
        public AddDataForm(string tableName,SQLiteManager manager)
        {
            _sqliteManager = manager;
            _tableName = tableName;
            InitializeComponent();
            InitializeDataGrid();
        }

        private void InitializeDataGrid()
        {
            DataTable dataTable = new DataTable();
            dataTable = _sqliteManager.ExecuteWithResults("pragma table_info(" + _tableName + ");");
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                this.dataGridView1.Columns.Add(
                    dataTable.Rows[i].ItemArray[1].ToString(), 
                    dataTable.Rows[i].ItemArray[1].ToString());
            }

        }
        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            this.dataGridView1.Rows.Add();
        }

        private void toolStripButton2_Click_1(object sender, EventArgs e)
        {
            dataGridView1.Rows.Remove(dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex]);

        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            int rowsInserted = GenerateDDL();
            try
            {
                _sqliteManager.ExecuteWithResults(_sql);
                MessageBox.Show(rowsInserted+" Row(s) Inserted");
                this.Close();
            }
            catch(Exception ex){
                MessageBox.Show(ex.Message);
            }
        }

        private int GenerateDDL()
        {
            string rowSQL = "";
            int n = this.dataGridView1.Columns.Count;
            int m = this.dataGridView1.Rows.Count;
            for (int j = 0; j < m; j++)
            {
                rowSQL += "INSERT INTO " + this._tableName + " values(";
                for (int i = 0; i < n; i++)
                {
                    rowSQL += dataGridView1.Rows[j].Cells[i].Value;
                    if (i != n - 1)
                    {
                        rowSQL += ", ";
                    }
                }
                rowSQL += ");\n";
            }
            _sql = rowSQL;
            return m;
        }
    }
}
