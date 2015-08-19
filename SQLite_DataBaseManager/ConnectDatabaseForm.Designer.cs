namespace SQLite_DataBaseManager
{
    partial class ConnectDatabaseForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.DBNameLabel = new System.Windows.Forms.Label();
            this.ConnectDBbtn = new System.Windows.Forms.Button();
            this.BtnTest = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label1.Location = new System.Drawing.Point(94, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name";
            // 
            // DBNameLabel
            // 
            this.DBNameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.DBNameLabel.AutoSize = true;
            this.DBNameLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DBNameLabel.Location = new System.Drawing.Point(181, 79);
            this.DBNameLabel.Name = "DBNameLabel";
            this.DBNameLabel.Size = new System.Drawing.Size(40, 17);
            this.DBNameLabel.TabIndex = 0;
            this.DBNameLabel.Text = "nada";
            // 
            // ConnectDBbtn
            // 
            this.ConnectDBbtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.ConnectDBbtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ConnectDBbtn.Location = new System.Drawing.Point(97, 173);
            this.ConnectDBbtn.Name = "ConnectDBbtn";
            this.ConnectDBbtn.Size = new System.Drawing.Size(184, 89);
            this.ConnectDBbtn.TabIndex = 2;
            this.ConnectDBbtn.Text = "Connect to Database";
            this.ConnectDBbtn.UseVisualStyleBackColor = true;
            this.ConnectDBbtn.Click += new System.EventHandler(this.ConnectDBbtn_Click);
            // 
            // BtnTest
            // 
            this.BtnTest.Location = new System.Drawing.Point(97, 290);
            this.BtnTest.Name = "BtnTest";
            this.BtnTest.Size = new System.Drawing.Size(75, 23);
            this.BtnTest.TabIndex = 3;
            this.BtnTest.Text = "Test";
            this.BtnTest.UseVisualStyleBackColor = true;
            this.BtnTest.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(192, 290);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "test : ";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // ConnectDatabaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(455, 325);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.BtnTest);
            this.Controls.Add(this.ConnectDBbtn);
            this.Controls.Add(this.DBNameLabel);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConnectDatabaseForm";
            this.Text = "Connect Database";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label DBNameLabel;
        private System.Windows.Forms.Button ConnectDBbtn;
        private System.Windows.Forms.Button BtnTest;
        private System.Windows.Forms.Label label3;
    }
}