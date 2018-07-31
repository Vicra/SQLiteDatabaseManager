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
    public partial class CreateTableForm : Form
    {
        string sql = "";
        SQLite db;
        public CreateTableForm(SQLite database)
        {
            db = database;
            InitializeComponent();
        }

        private void CreateTableForm_Load(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.GeneratedSQL.ReadOnly == false)
                this.GeneratedSQL.ReadOnly = true;
            else if (this.GeneratedSQL.ReadOnly)
                this.GeneratedSQL.ReadOnly = false;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AgregarColumnaStripBtn_Click(object sender, EventArgs e)
        {
            ColumnasGrid.Rows.Add();
        }

        private void EliminarColumnaStripBtn_Click(object sender, EventArgs e)
        {
            ColumnasGrid.Rows.Remove(ColumnasGrid.Rows[ColumnasGrid.SelectedCells[0].RowIndex]);
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(this.txtTableName.Text))
            {
                GenerarDDL();
                if (!ExisteTabla(this.txtTableName.Text))
                {
                    try
                    {
                        string sql = this.GeneratedSQL.Text;
                        db.ExecuteNonQuery(sql);
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
                MessageBox.Show("Add a table name");
            }
        }

        private bool ExisteTabla(string nombreTabla)
        {
            DataTable dt = db.GetTables();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i].ItemArray[0].ToString() == nombreTabla)
                    return true;
            }
            return false;
        }

        private void tabPage1_Leave(object sender, EventArgs e)
        {
            GenerarDDL();
        }

        private void GenerarDDL()
        {
                sql = "CREATE TABLE " + this.txtTableName.Text + " (";
                int n = this.ColumnasGrid.Rows.Count;
                var row = ColumnasGrid.Rows;
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
                this.GeneratedSQL.Text = sql;
            
                
            
            
        }
    }
}
