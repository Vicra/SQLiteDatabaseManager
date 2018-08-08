using System;
using System.IO;
using System.Windows.Forms;

namespace SQLite_DataBaseManager
{
    public partial class CreateConnectionForm : Form
    {
        SQLiteManager _sqliteManager = new SQLiteManager();
        public CreateConnectionForm()
        {
            InitializeComponent();
        }

        private void btnSearchFile_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtFilePathName.Text = openFileDialog.FileName;
            }
        }

        private void btnSaveConnection_Click(object sender, EventArgs e)
        {
            try
            {
                string filePathName = txtFilePathName.Text;
                if (isValidPath(filePathName))
                {
                    _sqliteManager.createNewConnection(filePathName);
                    MessageBox.Show("Connection performed successfully.");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Invalid file selected. Extension must be .sqlite");
                }
            }
            catch (Exception ex)
            {
                showErrorMessage(ex);
            }
        }

        private bool isValidPath(string filePathName)
        {
            return File.Exists(filePathName) && filePathName.EndsWith(".sqlite");
        }

        private void showErrorMessage(Exception ex)
        {
            string error = ex.Message;
            if (ex.InnerException != null && ex.InnerException.Message != null)
            {
                error += " InnerException: " + ex.InnerException.Message;
            }
            MessageBox.Show(error);
        }
    }
}
