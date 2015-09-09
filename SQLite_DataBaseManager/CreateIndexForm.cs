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
        public CreateIndexForm(SQLite database,string tableName)
        {

            InitializeComponent(); 
            this.TablesComboBox.Text = tableName;
            db = database;
            AddColumnsToGrid();
        }

        private void AddColumnsToGrid()
        {
            DataTable dt = new DataTable();
            dt = db.ExecuteWithResults("pragma table_info(" + this.TablesComboBox.Text + ");");
            TablesGrid.Rows.Add();
            TablesGrid.Rows[0].Cells[1].Value = dt.Rows[0].ItemArray[1].ToString();
            TablesGrid.Rows[0].Cells[2].Value = "BINARY";
            TablesGrid.Rows[0].Cells[3].Value = "ASC";
        }
        

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(this.txtIndexName.Text))
            {
                GenerarDDL();
                if (!ExisteIndex(this.txtIndexName.Text, TablesComboBox.Text))
                {
                    try
                    {
                        string sql = this.GeneratedSQL.Text;
                        db.ExecuteNonQuery(sql);
                        MessageBox.Show("Index " + this.txtIndexName.Text + " created successfully");
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("el Index " + txtIndexName.Text + " ya existe");
                }
            }
            else
            {
                MessageBox.Show("User please,... add an index name");
            }
        }

        private bool ExisteIndex(string indexName,string tablename)
        {
            DataTable dt = db.GetIndexes(tablename);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i].ItemArray[0].ToString() == indexName)
                    return true;
            }
            return false;
        }

        private void GenerarDDL()
        {
            try
            {
                sql = "CREATE";
                if (UniqueCheck.Checked)
                {
                    sql += " UNIQUE ";
                }
                sql += " INDEX " + this.txtIndexName.Text;
                sql += " ON " + TablesComboBox.Text + " (\n";

                if (!String.IsNullOrEmpty(TablesGrid.Rows[0].Cells[1].Value.ToString()))
                {
                    sql += "\t" + TablesGrid.Rows[0].Cells[1].Value.ToString() + " ";
                }
                
                if (!String.IsNullOrEmpty(TablesGrid.Rows[0].Cells[2].Value.ToString()) && !String.IsNullOrEmpty(TablesGrid.Rows[0].Cells[3].Value.ToString()))
                {
                    sql += "COLLATE " + TablesGrid.Rows[0].Cells[2].Value.ToString() + " ";
                    sql += TablesGrid.Rows[0].Cells[3].Value.ToString() + "\n";
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

        private void TablesGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void TablesGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            
        }
    }
}
