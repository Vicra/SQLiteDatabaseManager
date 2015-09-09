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
            this.CreateDBbtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.DBNameText = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // CreateDBbtn
            // 
            this.CreateDBbtn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CreateDBbtn.Location = new System.Drawing.Point(135, 146);
            this.CreateDBbtn.Name = "CreateDBbtn";
            this.CreateDBbtn.Size = new System.Drawing.Size(174, 66);
            this.CreateDBbtn.TabIndex = 7;
            this.CreateDBbtn.Text = "Create Database";
            this.CreateDBbtn.UseVisualStyleBackColor = true;
            this.CreateDBbtn.Click += new System.EventHandler(this.CreateDBbtn_Click_1);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(84, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "Name";
            // 
            // DBNameText
            // 
            this.DBNameText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DBNameText.Location = new System.Drawing.Point(135, 72);
            this.DBNameText.Name = "DBNameText";
            this.DBNameText.Size = new System.Drawing.Size(174, 22);
            this.DBNameText.TabIndex = 5;
            // 
            // CreateDatabaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 307);
            this.Controls.Add(this.CreateDBbtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DBNameText);
            this.Name = "CreateDatabaseForm";
            this.Text = "Create Database";
            this.Load += new System.EventHandler(this.CreateDatabaseForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button CreateDBbtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox DBNameText;

    }
}