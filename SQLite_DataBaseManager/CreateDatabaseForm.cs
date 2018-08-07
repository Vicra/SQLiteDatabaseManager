using System;
using System.IO;
using System.Text.RegularExpressions;
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

        private void btnCreateDatabase_Click(object sender, EventArgs e)
        {
            string name = txtDatabaseName.Text + ".sqlite";
            string path = txtLocation.Text;
            bool savePassword = checkBoxSaveConnection.Checked;
            try
            {
                if (hasValidName(name) && hasValidPath(path))
                {
                    db.CreateDatabase(name, path, savePassword);
                    MessageBox.Show("Database created successfully.");
                    this.Close();
                }
                else if (!hasValidName(name))
                {
                    MessageBox.Show("Invalid file name input.");
                }
                else if (!hasValidPath(path))
                {
                    MessageBox.Show("Invalid path input.");
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                if (ex.InnerException != null && ex.InnerException.Message != null)
                {
                    error += " InnerException: " + ex.InnerException.Message;
                }
                MessageBox.Show(error);
            }
        }

        private bool hasValidPath(string path)
        {
            return Directory.Exists(path);
        }

        private bool hasValidName(string fileName)
        {
            return true;
        }

        private void btnOpenFileBrowser_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtLocation.Text = folderBrowserDialog1.SelectedPath;
            }
        }
    }
}
