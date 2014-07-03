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
            this.SuspendLayout();
            // 
            // UpdateBtn
            // 
            this.UpdateBtn.Location = new System.Drawing.Point(11, 349);
            this.UpdateBtn.Name = "UpdateBtn";
            this.UpdateBtn.Size = new System.Drawing.Size(78, 28);
            this.UpdateBtn.TabIndex = 0;
            this.UpdateBtn.Text = "Update Now";
            this.UpdateBtn.UseVisualStyleBackColor = true;
            this.UpdateBtn.Click += new System.EventHandler(this.UpdateBtn_Click);
            // 
            // QuitBtn
            // 
            this.QuitBtn.Location = new System.Drawing.Point(195, 349);
            this.QuitBtn.Name = "QuitBtn";
            this.QuitBtn.Size = new System.Drawing.Size(76, 28);
            this.QuitBtn.TabIndex = 1;
            this.QuitBtn.Text = "Quit";
            this.QuitBtn.UseVisualStyleBackColor = true;
            this.QuitBtn.Click += new System.EventHandler(this.QuitBtn_Click);
            // 
            // UpdateUrlTxt
            // 
            this.UpdateUrlTxt.Location = new System.Drawing.Point(12, 36);
            this.UpdateUrlTxt.Name = "UpdateUrlTxt";
            this.UpdateUrlTxt.Size = new System.Drawing.Size(259, 20);
            this.UpdateUrlTxt.TabIndex = 2;
            this.UpdateUrlTxt.TextChanged += new System.EventHandler(this.UpdateUrlTxt_TextChanged);
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
            this.DatabaseLocationTxt.Location = new System.Drawing.Point(12, 88);
            this.DatabaseLocationTxt.Name = "DatabaseLocationTxt";
            this.DatabaseLocationTxt.Size = new System.Drawing.Size(259, 20);
            this.DatabaseLocationTxt.TabIndex = 4;
            this.DatabaseLocationTxt.TextChanged += new System.EventHandler(this.DatabaseLocationTxt_TextChanged);
            // 
            // LogTxt
            // 
            this.LogTxt.Location = new System.Drawing.Point(11, 114);
            this.LogTxt.Multiline = true;
            this.LogTxt.Name = "LogTxt";
            this.LogTxt.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.LogTxt.Size = new System.Drawing.Size(261, 226);
            this.LogTxt.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 389);
            this.Controls.Add(this.LogTxt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.DatabaseLocationTxt);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.UpdateUrlTxt);
            this.Controls.Add(this.QuitBtn);
            this.Controls.Add(this.UpdateBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
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

    }
}

