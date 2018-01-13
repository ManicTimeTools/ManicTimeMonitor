namespace ManicTimeMonitor
{
    partial class Form1
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
			this.UpdateBtn = new System.Windows.Forms.Button();
			this.QuitBtn = new System.Windows.Forms.Button();
			this.UpdateUrlTxt = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.DatabaseLocationTxt = new System.Windows.Forms.TextBox();
			this.LogTxt = new System.Windows.Forms.TextBox();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.DatabaseLocationBtn = new System.Windows.Forms.Button();
			this.ConfigureTaskScheduleBtn = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// UpdateBtn
			// 
			this.UpdateBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.UpdateBtn.Location = new System.Drawing.Point(11, 422);
			this.UpdateBtn.Name = "UpdateBtn";
			this.UpdateBtn.Size = new System.Drawing.Size(78, 28);
			this.UpdateBtn.TabIndex = 0;
			this.UpdateBtn.Text = "Update Now";
			this.UpdateBtn.UseVisualStyleBackColor = true;
			this.UpdateBtn.Click += new System.EventHandler(this.UpdateBtn_Click);
			// 
			// QuitBtn
			// 
			this.QuitBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.QuitBtn.Location = new System.Drawing.Point(396, 422);
			this.QuitBtn.Name = "QuitBtn";
			this.QuitBtn.Size = new System.Drawing.Size(76, 28);
			this.QuitBtn.TabIndex = 1;
			this.QuitBtn.Text = "Quit";
			this.QuitBtn.UseVisualStyleBackColor = true;
			this.QuitBtn.Click += new System.EventHandler(this.QuitBtn_Click);
			// 
			// UpdateUrlTxt
			// 
			this.UpdateUrlTxt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.UpdateUrlTxt.Location = new System.Drawing.Point(12, 36);
			this.UpdateUrlTxt.Name = "UpdateUrlTxt";
			this.UpdateUrlTxt.Size = new System.Drawing.Size(460, 20);
			this.UpdateUrlTxt.TabIndex = 2;
			this.UpdateUrlTxt.Leave += new System.EventHandler(this.UpdateUrlTxt_Leave);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(12, 15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(91, 18);
			this.label1.TabIndex = 3;
			this.label1.Text = "Update URL:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(12, 67);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(133, 18);
			this.label2.TabIndex = 5;
			this.label2.Text = "Database Location:";
			// 
			// DatabaseLocationTxt
			// 
			this.DatabaseLocationTxt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.DatabaseLocationTxt.Location = new System.Drawing.Point(12, 88);
			this.DatabaseLocationTxt.Name = "DatabaseLocationTxt";
			this.DatabaseLocationTxt.ReadOnly = true;
			this.DatabaseLocationTxt.Size = new System.Drawing.Size(423, 20);
			this.DatabaseLocationTxt.TabIndex = 4;
			this.DatabaseLocationTxt.TextChanged += new System.EventHandler(this.DatabaseLocationTxt_TextChanged);
			// 
			// LogTxt
			// 
			this.LogTxt.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.LogTxt.Location = new System.Drawing.Point(11, 114);
			this.LogTxt.Multiline = true;
			this.LogTxt.Name = "LogTxt";
			this.LogTxt.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.LogTxt.Size = new System.Drawing.Size(461, 302);
			this.LogTxt.TabIndex = 6;
			// 
			// openFileDialog
			// 
			this.openFileDialog.FileName = "openFileDialog";
			this.openFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog_FileOk);
			// 
			// DatabaseLocationBtn
			// 
			this.DatabaseLocationBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.DatabaseLocationBtn.Location = new System.Drawing.Point(442, 88);
			this.DatabaseLocationBtn.Name = "DatabaseLocationBtn";
			this.DatabaseLocationBtn.Size = new System.Drawing.Size(30, 20);
			this.DatabaseLocationBtn.TabIndex = 7;
			this.DatabaseLocationBtn.Text = "...";
			this.DatabaseLocationBtn.UseVisualStyleBackColor = true;
			this.DatabaseLocationBtn.Click += new System.EventHandler(this.DatabaseLocationBtn_Click);
			// 
			// ConfigureTaskScheduleBtn
			// 
			this.ConfigureTaskScheduleBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.ConfigureTaskScheduleBtn.Location = new System.Drawing.Point(252, 422);
			this.ConfigureTaskScheduleBtn.Name = "ConfigureTaskScheduleBtn";
			this.ConfigureTaskScheduleBtn.Size = new System.Drawing.Size(138, 28);
			this.ConfigureTaskScheduleBtn.TabIndex = 8;
			this.ConfigureTaskScheduleBtn.Text = "Configure Task Schedule";
			this.ConfigureTaskScheduleBtn.UseVisualStyleBackColor = true;
			this.ConfigureTaskScheduleBtn.Click += new System.EventHandler(this.ConfigureTaskScheduleBtn_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(484, 462);
			this.Controls.Add(this.ConfigureTaskScheduleBtn);
			this.Controls.Add(this.DatabaseLocationBtn);
			this.Controls.Add(this.LogTxt);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.DatabaseLocationTxt);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.UpdateUrlTxt);
			this.Controls.Add(this.QuitBtn);
			this.Controls.Add(this.UpdateBtn);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Form1";
			this.Text = "ManicTimeMonitor";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button UpdateBtn;
        private System.Windows.Forms.Button QuitBtn;
        private System.Windows.Forms.TextBox UpdateUrlTxt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox DatabaseLocationTxt;
        private System.Windows.Forms.TextBox LogTxt;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.Button DatabaseLocationBtn;
		private System.Windows.Forms.Button ConfigureTaskScheduleBtn;

    }
}

