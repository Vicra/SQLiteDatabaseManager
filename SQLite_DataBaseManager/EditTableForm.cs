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
    public partial class EditTableForm : Form
    {
        SQLite db;
        public EditTableForm(SQLite database)
        {
            db = database;
            InitializeComponent();
        }

        private void txtTableName_TextChanged(object sender, EventArgs e)
        {
            //generar ddl alter table name
        }
    }
}
