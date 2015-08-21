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
    public partial class AddDataForm : Form
    {
        string name;
        SQLite db;
        string sql;
        public AddDataForm(string tableName,SQLite database)
        {
            db = database;
            name = tableName;
            InitializeComponent();
            InitializeDataGrid();
        }

        private void InitializeDataGrid()
        {
            DataTable dt = new DataTable();
            dt = db.ExecuteWithResults("pragma table_info(" + name + ");");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.dataGridView1.Columns.Add(dt.Rows[i].ItemArray[1].ToString(), dt.Rows[i].ItemArray[1].ToString());
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
            int rowsInserted = GenerarDDL();
            try
            {
                db.ExecuteWithResults(sql);
                MessageBox.Show(rowsInserted+" Row(s) Inserted");
                this.Close();
            }
            catch(Exception ex){
                MessageBox.Show(ex.Message);
            }
            
        }

        private int GenerarDDL()
        {
            string rowSQL = "";
            int n = this.dataGridView1.Columns.Count;
            int m = this.dataGridView1.Rows.Count;
            for (int j = 0; j < m; j++)
            {
                rowSQL += "INSERT INTO " + this.name + " values(";
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
            sql = rowSQL;
            return m;
        }
    }
}
