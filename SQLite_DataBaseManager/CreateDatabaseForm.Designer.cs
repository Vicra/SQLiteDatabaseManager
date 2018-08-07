namespace SQLite_DataBaseManager
{
    partial class CreateDatabaseForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnCreateDatabase = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDatabaseName = new System.Windows.Forms.TextBox();
            this.txtLocation = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOpenFileBrowser = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.checkBoxSaveConnection = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnCreateDatabase
            // 
            this.btnCreateDatabase.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnCreateDatabase.Location = new System.Drawing.Point(120, 161);
            this.btnCreateDatabase.Margin = new System.Windows.Forms.Padding(2);
            this.btnCreateDatabase.Name = "btnCreateDatabase";
            this.btnCreateDatabase.Size = new System.Drawing.Size(163, 33);
            this.btnCreateDatabase.TabIndex = 7;
            this.btnCreateDatabase.Text = "Create Database";
            this.btnCreateDatabase.UseVisualStyleBackColor = true;
            this.btnCreateDatabase.Click += new System.EventHandler(this.btnCreateDatabase_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 48);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Database Name";
            // 
            // txtDatabaseName
            // 
            this.txtDatabaseName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDatabaseName.Location = new System.Drawing.Point(120, 48);
            this.txtDatabaseName.Margin = new System.Windows.Forms.Padding(2);
            this.txtDatabaseName.Name = "txtDatabaseName";
            this.txtDatabaseName.Size = new System.Drawing.Size(197, 20);
            this.txtDatabaseName.TabIndex = 5;
            // 
            // txtLocation
            // 
            this.txtLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLocation.Location = new System.Drawing.Point(120, 91);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.Size = new System.Drawing.Size(161, 20);
            this.txtLocation.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(59, 94);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Location";
            // 
            // btnOpenFileBrowser
            // 
            this.btnOpenFileBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenFileBrowser.Location = new System.Drawing.Point(289, 89);
            this.btnOpenFileBrowser.Name = "btnOpenFileBrowser";
            this.btnOpenFileBrowser.Size = new System.Drawing.Size(32, 23);
            this.btnOpenFileBrowser.TabIndex = 14;
            this.btnOpenFileBrowser.Text = "...";
            this.btnOpenFileBrowser.UseVisualStyleBackColor = true;
            this.btnOpenFileBrowser.Click += new System.EventHandler(this.btnOpenFileBrowser_Click);
            // 
            // checkBoxSaveConnection
            // 
            this.checkBoxSaveConnection.AutoSize = true;
            this.checkBoxSaveConnection.Checked = true;
            this.checkBoxSaveConnection.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSaveConnection.Location = new System.Drawing.Point(120, 126);
            this.checkBoxSaveConnection.Name = "checkBoxSaveConnection";
            this.checkBoxSaveConnection.Size = new System.Drawing.Size(108, 17);
            this.checkBoxSaveConnection.TabIndex = 16;
            this.checkBoxSaveConnection.Text = "Save Connection";
            this.checkBoxSaveConnection.UseVisualStyleBackColor = true;
            // 
            // CreateDatabaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 222);
            this.Controls.Add(this.checkBoxSaveConnection);
            this.Controls.Add(this.btnOpenFileBrowser);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtLocation);
            this.Controls.Add(this.btnCreateDatabase);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtDatabaseName);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximumSize = new System.Drawing.Size(396, 261);
            this.MinimumSize = new System.Drawing.Size(396, 261);
            this.Name = "CreateDatabaseForm";
            this.Text = "Create Database";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCreateDatabase;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDatabaseName;
        private System.Windows.Forms.TextBox txtLocation;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnOpenFileBrowser;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.CheckBox checkBoxSaveConnection;
    }
}