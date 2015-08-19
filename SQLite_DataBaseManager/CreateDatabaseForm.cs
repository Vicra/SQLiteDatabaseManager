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
    public partial class CreateDatabaseForm : Form
    {
        SQLite db;
        public CreateDatabaseForm(SQLite database)
        {
            db = database;
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void CreateDatabaseForm_Load(object sender, EventArgs e)
        {

        }

        private void CreateDBbtn_Click_1(object sender, EventArgs e)
        {
            string name = DBNameText.Text;
            db.CreateDatabase(name);
            this.Close();
        }
    }
}
