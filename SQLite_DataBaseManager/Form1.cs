using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace SQLite_DataBaseManager
{
    public partial class Form1 : Form
    {
        string lastSelect = "";
        SQLiteManager _sqliteManager = new SQLiteManager();
        CreateViewForm viewForm;
        CreateDatabaseForm createForm;
        CreateTableForm createTableForm;
        CreateIndexForm indexform;
        CreateTriggerForm triggerForm;
        FileConnections fileConnections;
        public Form1()
        {
            this.Refresh();
            fileConnections = new FileConnections();
            InitializeComponent();
            InitializeDatabases();
        }

        private void InitializeDatabases()
        {
            DatabaseTree.Nodes.Clear();
            DatabaseTree.Nodes.Add("Databases", "Databases","servers.png", "servers.png");
            DatabaseTree.Nodes["Databases"].ContextMenuStrip = DatabasesMenuStrip;

            var connections = fileConnections.getConnections();
            foreach (DatabaseConnection connection in connections)
            {
                DatabaseTree.Nodes[0].Nodes.Add(connection.Path,
                    connection.Name + "(" + connection.Path + ")",
                    "db.png",
                    "db.png");
            }
            int n = DatabaseTree.Nodes[0].Nodes.Count;
            for (int i = 0; i < n; i++)
            {
                DatabaseTree.Nodes[0].Nodes[i].ContextMenuStrip = DatabaseMenuStrip;
            }
            DatabaseTree.Nodes[0].ExpandAll();
        }

        private void LoadTables()
        {
            DataTable dt = _sqliteManager.GetTables();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DatabaseTree.SelectedNode.Nodes["Tables"].Nodes.Add(
                    dt.Rows[i].ItemArray[0].ToString(), 
                    dt.Rows[i].ItemArray[0].ToString(),
                    "table.png", "table.png");
                DatabaseTree.SelectedNode.Nodes["Tables"].Nodes[i].ContextMenuStrip = TableMenuStrip;

                DatabaseTree.SelectedNode.Nodes["Tables"].Nodes[i].Nodes.Add("Columns", "Columns", "columns.png", "columns.png");

                DatabaseTree.SelectedNode.Nodes["Tables"].Nodes[i].Nodes.Add("Indexes", "Indexes", "tags.png", "tags.png");
                DatabaseTree.SelectedNode.Nodes["Tables"].Nodes[i].Nodes["Indexes"].ContextMenuStrip = IndexesMenuStrip;

                DatabaseTree.SelectedNode.Nodes["Tables"].Nodes[i].Nodes.Add("Triggers", "Triggers", "tools.png", "tools.png");
                DatabaseTree.SelectedNode.Nodes["Tables"].Nodes[i].Nodes["Triggers"].ContextMenuStrip = TriggersMenuStrip;

                string tableName = DatabaseTree.SelectedNode.Nodes["Tables"].Nodes[i].Text; 
                LoadColumns(tableName);
                LoadIndexes(dt.Rows[i].ItemArray[0].ToString());
                LoadTriggers(tableName);
            }
            this.Refresh();
        }

        private void LoadTriggers(string table)
        {
            DataTable dt = _sqliteManager.GetTriggers(table);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DatabaseTree.SelectedNode.Nodes["Tables"].Nodes[table].Nodes["Triggers"].Nodes.Add(dt.Rows[i].ItemArray[0].ToString(),
                    dt.Rows[i].ItemArray[0].ToString(), "tool.png", "tool.png");
                DatabaseTree.SelectedNode.Nodes["Tables"].Nodes[table].Nodes["Triggers"].Nodes[i].ContextMenuStrip = TriggerMenuStrip;
            }
        }

        private void LoadIndexes(string table)
        {
            DataTable dt = _sqliteManager.GetIndexes(table);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DatabaseTree.SelectedNode.Nodes["Tables"].Nodes[table].Nodes["Indexes"].Nodes.Add(dt.Rows[i].ItemArray[0].ToString(),
                    dt.Rows[i].ItemArray[0].ToString(), "tag.png", "tag.png");
                DatabaseTree.SelectedNode.Nodes["Tables"].Nodes[table].Nodes["Indexes"].Nodes[i].ContextMenuStrip = IndexMenuStrip;
            }
        }

        private void LoadColumns(string table)
        {
            DataTable dt = _sqliteManager.GetTableColumns(table);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DatabaseTree.SelectedNode.Nodes["Tables"].Nodes[table].Nodes["Columns"].Nodes.Add(dt.Rows[i].ItemArray[1].ToString(),
                    dt.Rows[i].ItemArray[1].ToString(), "column.png", "column.png");
            }
        }
        private void LoadViews()
        {
            DataTable dt = _sqliteManager.GetViews();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DatabaseTree.SelectedNode.Nodes["Views"].Nodes.Add(dt.Rows[i].ItemArray[0].ToString(), dt.Rows[i].ItemArray[0].ToString(),
                    "vista.png", "vista.png");
                DatabaseTree.SelectedNode.Nodes["Views"].Nodes[i].ContextMenuStrip = ViewMenuStrip;
            }
        }

        private void CreateDatabaseAction_Click(object sender, EventArgs e)
        {
            createForm = new CreateDatabaseForm(_sqliteManager);
            createForm.ShowDialog();
            this.refreshToolStripMenuItem_Click_1(sender,e);
        }

        private void RefreshDatabasesAction_Click(object sender, EventArgs e)
        {
            InitializeDatabases();
            DatabaseTree.Nodes[0].Expand();
        }

        private void removeTheDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("Drop database: " + DatabaseTree.SelectedNode.Text.ToString() + "?", "Drop database",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question,
            MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes))
            {
                _sqliteManager.DeleteDatabase(DatabaseTree.SelectedNode.Text.ToString());
                RefreshDatabasesAction_Click(sender, e);
            }
        }

        private void CreateView_Click(object sender, EventArgs e)
        {
            viewForm = new CreateViewForm(_sqliteManager);
            viewForm.ShowDialog();
            this.refreshToolStripMenuItem_Click_1(sender, e);
        }

        private void createToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            viewForm = new CreateViewForm(_sqliteManager);
            viewForm.ShowDialog();
            this.refreshToolStripMenuItem_Click_1(sender, e);
        }
        

        private void createToolStripMenuItem_Click(object sender, EventArgs e)
        {
            createTableForm = new CreateTableForm(_sqliteManager);
            createTableForm.ShowDialog();
            this.refreshToolStripMenuItem_Click_1(sender, e);
        }

        private void RunAction_Click(object sender, EventArgs e)
        {
            string sql;
            if (!String.IsNullOrEmpty(QueryTextBox.SelectedText))
            {
                sql = QueryTextBox.SelectedText;
            }
            else{
                sql = this.QueryTextBox.Text;
            }
            if (sql.Contains("select"))
            {
                lastSelect = sql;
            }
            
            if (!String.IsNullOrEmpty(sql))
            {
                try
                {
                    DataTable dt = new DataTable();

                    this.dataGridView1.DataSource = _sqliteManager.ExecuteWithResults(sql);
                    tabControl1.SelectedIndex = 0;
                    string message= "sql Excecuted successfully";
                    AppendText(message, Color.Blue);

                    
                    string date = DateTime.Now.ToString();
                    string rows = dataGridView1.Rows.Count.ToString();
                    this.HistoryGrid.Rows.Add();
                }
                catch (Exception ex)
                {
                    AppendText(ex.Message, Color.Red);
                }
            }
            else
            {
                string message = "Cannot execute empty query.";
                AppendText(message, Color.Black);
                
            }
            
        }
        private void AppendText(string message,Color color)
        {
            int length = statusTextBox.TextLength;
            statusTextBox.AppendText(DateTime.Now.ToString()+" "+message+"\n");
            statusTextBox.SelectionStart = length;
            statusTextBox.SelectionLength = message.Length+DateTime.Now.ToString().Length+1;
            statusTextBox.SelectionColor = color;
            statusTextBox.SelectionStart = statusTextBox.Text.Length;
            
            statusTextBox.ScrollToCaret();
        }

        private void Refresh_Click(object sender, EventArgs e)
        {
            InitializeDatabases();
        }

        private void ConnectDatabase_Click(object sender, EventArgs e)
        {

            if (DatabaseTree.SelectedNode.Text != "Databases")
            {
                try
                {
                    string[] nameElements = DatabaseTree.SelectedNode.Text.ToString().Split('(');
                    string name = nameElements[0];
                    string path = nameElements[1].Replace(")","");
                    string filePathName = path + "\\" + name;

                    _sqliteManager.ConnectDatabase(filePathName);
                    if (DatabaseTree.SelectedNode.Nodes.Count == 0)
                    {
                        DatabaseTree.SelectedNode.Nodes.Add("Tables", "Tables", "tables.png", "tables.png");
                        DatabaseTree.SelectedNode.Nodes["Tables"].ContextMenuStrip = TablesMenuStrip;
                        LoadTables();
                        DatabaseTree.SelectedNode.Nodes.Add("Views", "Views", "vistas.png", "vistas.png");
                        DatabaseTree.SelectedNode.Nodes["Views"].ContextMenuStrip = ViewsMenuStrip;
                        LoadViews();
                        DatabaseTree.SelectedNode.Expand();
                    }

                }
                catch (Exception ex)
                {
                    AppendText(ex.Message, Color.Red);
                }
            }
        }

        private void DisconnectDatabase_Click(object sender, EventArgs e)
        {

            _sqliteManager.CloseConnection();
            InitializeDatabases();
            DatabaseTree.Nodes[0].Expand();
        }

        private void RemoveDatabase_Click(object sender, EventArgs e)
        {
            removeTheDatabaseToolStripMenuItem_Click(sender, e);
        }

        private void createTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            createToolStripMenuItem_Click(sender, e);
        }

        private void viewDataToolStripMenuItem_Click(object send, EventArgs e)
        {
            try
            {
                dataGridView1.DataSource = null;
                dataGridView1.Refresh();
                string tableName = "";
                tableName += DatabaseTree.SelectedNode.Text;
                this.dataGridView1.DataSource = _sqliteManager.ExecuteWithResults("SELECT * FROM " + tableName);
                tabControl1.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void reToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("Drop table: " + DatabaseTree.SelectedNode.Text.ToString() + "?", "Drop table",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question,
            MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes))
            {
                _sqliteManager.ExecuteWithResults("DROP TABLE " + DatabaseTree.SelectedNode.Text.ToString());
                RefreshDatabasesAction_Click(sender, e);
            }
        }
        private void insertDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddDataForm dataform = new AddDataForm(DatabaseTree.SelectedNode.Text, _sqliteManager);
            dataform.Show();
        }

        private void createIndexToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            indexform = new CreateIndexForm(_sqliteManager, this.DatabaseTree.SelectedNode.Parent.Text);
            indexform.ShowDialog();

        }

        private void createTriggerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            triggerForm = new CreateTriggerForm(_sqliteManager,DatabaseTree.SelectedNode.Parent.Text);
            triggerForm.ShowDialog();
        }

        private void deToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("Drop index: " + DatabaseTree.SelectedNode.Text.ToString() + "?", "Drop index",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question,
            MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes))
            {
                _sqliteManager.ExecuteWithResults("DROP INDEX " + DatabaseTree.SelectedNode.Text.ToString());
                RefreshDatabasesAction_Click(sender, e);
            }
        }

        private void deleteViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("Drop view: " + DatabaseTree.SelectedNode.Text.ToString() + "?", "Drop view",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question,
            MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes))
            {
                _sqliteManager.ExecuteWithResults("DROP VIEW " + DatabaseTree.SelectedNode.Text.ToString());
                RefreshDatabasesAction_Click(sender, e);
            }
        }

        private void DatabaseTree_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            DatabaseTree.SelectedNode = e.Node;
            if (e.Node.Parent != null && e.Node.Parent.Text == "Databases")
            {
                foreach (var node in DatabaseTree.Nodes[0].Nodes)
                {
                    ((TreeNode)node).NodeFont = new Font(DatabaseTree.Font, FontStyle.Regular);
                    if (((TreeNode)node).Text == e.Node.Text)
                    {
                        ((TreeNode)node).NodeFont = new Font(DatabaseTree.Font, FontStyle.Bold);   
                    }
                }
                
            }
            if (DatabaseTree.SelectedNode.Parent != null)
            {
                if (DatabaseTree.SelectedNode.Parent.Text == "Databases")
                {
                    this.ConnectDatabase_Click(sender, e);
                }
                else if (DatabaseTree.SelectedNode.Parent.Text == "Tables")
                {
                    this.DDL_TextBox.Text = _sqliteManager.GetDDL(DatabaseTree.SelectedNode.Text, "table");
                }
                else if (DatabaseTree.SelectedNode.Parent.Text == "Views")
                {
                    this.DDL_TextBox.Text = _sqliteManager.GetDDL(DatabaseTree.SelectedNode.Text, "view");
                }
                else if (DatabaseTree.SelectedNode.Parent.Text == "Indexes")
                {
                    this.DDL_TextBox.Text = _sqliteManager.GetDDL(DatabaseTree.SelectedNode.Text, "index");
                }
                else if (DatabaseTree.SelectedNode.Parent.Text == "Triggers")
                {
                    this.DDL_TextBox.Text = _sqliteManager.GetDDL(DatabaseTree.SelectedNode.Text, "trigger");
                }
                tabControl1.SelectedIndex = 1;
            }
        }

        private void OpenFileAction_Click(object sender, EventArgs e)
        {
            int size = -1;
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                string file = openFileDialog1.FileName;
                try
                {
                    string text = File.ReadAllText(file);
                    size = text.Length;
                    this.QueryTextBox.Text = text;
                }
                catch (IOException)
                {
                }
            }
        }

        private void SaveFileAction_Click(object sender, EventArgs e)
        {
            //this.saveFileDialog1.ShowDialog();
            // Create a SaveFileDialog to request a path and file name to save to.
            SaveFileDialog saveFile1 = new SaveFileDialog();

            // Initialize the SaveFileDialog to specify the RTF extension for the file.
            saveFile1.DefaultExt = "*.txt";
            saveFile1.Filter = "txt Files|*.txt";

            // Determine if the user selected a file name from the saveFileDialog.
            if (saveFile1.ShowDialog() == System.Windows.Forms.DialogResult.OK &&
               saveFile1.FileName.Length > 0)
            {
                // Save the contents of the RichTextBox into the file.
                this.QueryTextBox.SaveFile(saveFile1.FileName, RichTextBoxStreamType.PlainText);
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                this.RunAction_Click(sender,e);
            }
        }

        private void QueryTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                this.RunAction_Click(sender, e);
            }
            if (e.KeyCode == Keys.O && e.Control)
            {
                this.OpenFileAction_Click(sender, e);
            }
            if (e.KeyCode == Keys.S && e.Control)
            {
                this.SaveFileAction_Click(sender, e);
            }
        }

        private void QueryTextBox_TextChanged(object sender, EventArgs e)
        {
            string query = QueryTextBox.Text;
            if (query.Contains("select"))
            {
                
            }
            this.QueryTextBox.Font = new Font(QueryTextBox.Font, FontStyle.Bold);
        }

        private void createADatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateDatabaseAction_Click(sender, e);
        }

        private void createConnectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateConnectionForm createConnectionForm = new CreateConnectionForm();
            createConnectionForm.ShowDialog();
            this.refreshToolStripMenuItem_Click_1(sender, e);
        }

        private void refreshToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            InitializeDatabases();
        }

        private void createViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.CreateView_Click(sender, e);
        }

        private void deleteTriggerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("Drop trigger: " + DatabaseTree.SelectedNode.Text.ToString() + "?", "Drop trigger",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question,
            MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes))
            {
                _sqliteManager.ExecuteWithResults("DROP TRIGGER " + DatabaseTree.SelectedNode.Text.ToString());
                RefreshDatabasesAction_Click(sender, e);
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                this.dataGridView1.DataSource = null;
                this.dataGridView1.DataSource = _sqliteManager.ExecuteWithResults(lastSelect);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not refresh. Make sure database is selected.");
            }
        }

        private void removeConnectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var a = ((ToolStripItem)(sender)).Name;
            MessageBox.Show("");
        }

        private void DatabaseTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (DatabaseTree.SelectedNode.Parent != null)
            {
                if (DatabaseTree.SelectedNode.Parent.Text == "Tables")
                {
                    this.DDL_TextBox.Text = _sqliteManager.GetDDL(DatabaseTree.SelectedNode.Text, "table");
                }
                else if (DatabaseTree.SelectedNode.Parent.Text == "Views")
                {
                    this.DDL_TextBox.Text = _sqliteManager.GetDDL(DatabaseTree.SelectedNode.Text, "view");
                }
                else if (DatabaseTree.SelectedNode.Parent.Text == "Indexes")
                {
                    this.DDL_TextBox.Text = _sqliteManager.GetDDL(DatabaseTree.SelectedNode.Text, "index");
                }
                else if (DatabaseTree.SelectedNode.Parent.Text == "Triggers")
                {
                    this.DDL_TextBox.Text = _sqliteManager.GetDDL(DatabaseTree.SelectedNode.Text, "trigger");
                }
                tabControl1.SelectedIndex = 1;
            }
        }
    }
}
