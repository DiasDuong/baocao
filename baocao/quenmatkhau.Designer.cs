namespace baocao
{
    partial class quenmatkhau
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnlaymk = new System.Windows.Forms.Button();
            this.txttendn = new System.Windows.Forms.TextBox();
            this.txtemail = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label1.Location = new System.Drawing.Point(359, 132);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(228, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "Quên mật khẩu ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(275, 224);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(156, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Tên đăng nhập :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(360, 300);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 25);
            this.label3.TabIndex = 2;
            this.label3.Text = "Email :";
            // 
            // btnlaymk
            // 
            this.btnlaymk.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnlaymk.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnlaymk.Location = new System.Drawing.Point(375, 403);
            this.btnlaymk.Name = "btnlaymk";
            this.btnlaymk.Size = new System.Drawing.Size(192, 48);
            this.btnlaymk.TabIndex = 3;
            this.btnlaymk.Text = "Lấy lại mật khẩu";
            this.btnlaymk.UseVisualStyleBackColor = false;
            this.btnlaymk.Click += new System.EventHandler(this.btnlaymk_Click);
            // 
            // txttendn
            // 
            this.txttendn.Location = new System.Drawing.Point(499, 228);
            this.txttendn.Name = "txttendn";
            this.txttendn.Size = new System.Drawing.Size(237, 22);
            this.txttendn.TabIndex = 4;
            // 
            // txtemail
            // 
            this.txtemail.Location = new System.Drawing.Point(499, 304);
            this.txtemail.Name = "txtemail";
            this.txtemail.Size = new System.Drawing.Size(237, 22);
            this.txtemail.TabIndex = 5;
            // 
            // quenmatkhau
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1003, 594);
            this.Controls.Add(this.txtemail);
            this.Controls.Add(this.txttendn);
            this.Controls.Add(this.btnlaymk);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "quenmatkhau";
            this.Text = "quenmatkhau";
            this.Load += new System.EventHandler(this.quenmatkhau_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnlaymk;
        private System.Windows.Forms.TextBox txttendn;
        private System.Windows.Forms.TextBox txtemail;
    }
}