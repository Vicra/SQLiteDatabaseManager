using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Windows;

namespace SQLite_DataBaseManager
{
    public partial class Form1 : Form
    {
        SQLite db = new SQLite();
        CreateViewForm viewForm;
        ConnectDatabaseForm connectForm;
        CreateDatabaseForm createForm;
        CreateTableForm createTableForm;
        SQLEditorForm editorForm;
        CreateIndexForm indexform;
        public Form1()
        {
            InitializeComponent();
            InitializeDatabases();
        }

        private void InitializeDatabases()
        {
            DatabaseTree.Nodes.Clear();

            DatabaseTree.Nodes.Add("Databases", "Databases","server.png","server.png");
            string[] files = Directory.GetFiles("C:/Users/dell/Documents/Visual Studio 2013/Projects/SQLite_DataBaseManager/SQLite_DataBaseManager/bin/Debug");
            string[] dabatases;
            foreach (string file in files)
            {
                if (file.EndsWith(".sqlite"))
                {
                    dabatases = file.Split('\\','.');
                    DatabaseTree.Nodes[0].Nodes.Add(dabatases[dabatases.Length - 2].ToString(),
                        dabatases[dabatases.Length - 2].ToString(),0,0);
                }
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void eventLog1_EntryWritten(object sender, System.Diagnostics.EntryWrittenEventArgs e)
        {

        }

        private void connectToDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DatabaseTree.SelectedNode.Text.ToString() != "Databases")
            {
                //connectForm = new ConnectDatabaseForm(DatabaseTree.SelectedNode.Text.ToString(),db);
                //connectForm.Show();

                db.ConnectDatabase(DatabaseTree.SelectedNode.Text.ToString());
                //expand objects
                DatabaseTree.SelectedNode.Nodes.Add("Tables", "Tables",5,5);
                DatabaseTree.SelectedNode.Nodes.Add("Views","Views",6,6);
                DatabaseTree.SelectedNode.Expand();

                this.dataGridView1.DataSource = db.ExecuteWithResults("select * from sqlite_master;");
                //
            }   
        }

        private void CreateDatabaseAction_Click(object sender, EventArgs e)
        {
            createForm = new CreateDatabaseForm(db);
            createForm.Show();
        }

        private void RefreshDatabasesAction_Click(object sender, EventArgs e)
        {
            InitializeDatabases();
            DatabaseTree.Nodes[0].Expand();
        }

        private void removeTheDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (DatabaseTree.SelectedNode.Text.ToString() != "Databases")
            {
                if ((MessageBox.Show("Drop database: " + DatabaseTree.SelectedNode.Text.ToString() + "?", "Drop database",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes))
                {
                    db.DeleteDatabase(DatabaseTree.SelectedNode.Text.ToString());
                    RefreshDatabasesAction_Click(sender, e);
                }
            }
        }

        private void splitContainer2_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void openSDLEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editorForm = new SQLEditorForm(db);
            editorForm.Show();
        }

        private void disconnectToDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

           
        }

        private void databaseToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void CreateView_Click(object sender, EventArgs e)
        {
            viewForm = new CreateViewForm(db);
            viewForm.Show();
        }

        private void createToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            viewForm = new CreateViewForm(db);
            viewForm.Show();
        }
        public void addStatusText(string str)
        {
            this.statusTextBox.Text += str+"\n"+"\n";
        }

        private void createToolStripMenuItem_Click(object sender, EventArgs e)
        {
            createTableForm = new CreateTableForm(db);
            createTableForm.Show();
        }

        private void createToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            indexform = new CreateIndexForm(db);
            indexform.Show();
        }

        private void RunAction_Click(object sender, EventArgs e)
        {
            string sql;
            sql = this.QueryTextBox.Text;
            if (!String.IsNullOrEmpty(sql))
            {
                try
                {
                    if (sql.Contains("select"))
                    {
                        DataTable dt = new DataTable();
                        this.dataGridView1.DataSource = db.ExecuteWithResults(sql);
                    }
                    db.ExecuteNonQuery(sql);
                    this.statusTextBox.Text += "sql ejecutado";
                }
                catch (Exception ex)
                {
                    this.statusTextBox.Text += ex.ToString() + "\n" + "\n";
                }
            }
            else
            {
                this.statusTextBox.Text += "Cannot execute empty query.\n\n";
            }
            
        }
    }
}
