namespace baocao
{
    partial class doimatkhau
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
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txttendn = new System.Windows.Forms.TextBox();
            this.txtmkmoii = new System.Windows.Forms.TextBox();
            this.txtmkcu = new System.Windows.Forms.TextBox();
            this.txtmkmoi = new System.Windows.Forms.TextBox();
            this.btndoimk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(250, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(239, 39);
            this.label1.TabIndex = 0;
            this.label1.Text = "Đổi mật khẩu ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(121, 303);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(228, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Xác nhận mật khẩu mới :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(209, 245);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(140, 25);
            this.label3.TabIndex = 2;
            this.label3.Text = "Mật khẩu mới :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(178, 186);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(171, 25);
            this.label4.TabIndex = 3;
            this.label4.Text = "Mật khẩu hiện tại :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(193, 116);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(156, 25);
            this.label5.TabIndex = 4;
            this.label5.Text = "Tên đăng nhập :";
            // 
            // txttendn
            // 
            this.txttendn.Location = new System.Drawing.Point(389, 120);
            this.txttendn.Name = "txttendn";
            this.txttendn.Size = new System.Drawing.Size(196, 22);
            this.txttendn.TabIndex = 5;
            // 
            // txtmkmoii
            // 
            this.txtmkmoii.Location = new System.Drawing.Point(389, 307);
            this.txtmkmoii.Name = "txtmkmoii";
            this.txtmkmoii.Size = new System.Drawing.Size(196, 22);
            this.txtmkmoii.TabIndex = 6;
            // 
            // txtmkcu
            // 
            this.txtmkcu.Location = new System.Drawing.Point(389, 190);
            this.txtmkcu.Name = "txtmkcu";
            this.txtmkcu.Size = new System.Drawing.Size(196, 22);
            this.txtmkcu.TabIndex = 7;
            // 
            // txtmkmoi
            // 
            this.txtmkmoi.Location = new System.Drawing.Point(389, 248);
            this.txtmkmoi.Name = "txtmkmoi";
            this.txtmkmoi.Size = new System.Drawing.Size(196, 22);
            this.txtmkmoi.TabIndex = 8;
            // 
            // btndoimk
            // 
            this.btndoimk.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btndoimk.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btndoimk.Location = new System.Drawing.Point(271, 356);
            this.btndoimk.Name = "btndoimk";
            this.btndoimk.Size = new System.Drawing.Size(158, 48);
            this.btndoimk.TabIndex = 9;
            this.btndoimk.Text = "Đổi mật khẩu";
            this.btndoimk.UseVisualStyleBackColor = false;
            this.btndoimk.Click += new System.EventHandler(this.btndoimk_Click);
            // 
            // doimatkhau
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btndoimk);
            this.Controls.Add(this.txtmkmoi);
            this.Controls.Add(this.txtmkcu);
            this.Controls.Add(this.txtmkmoii);
            this.Controls.Add(this.txttendn);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "doimatkhau";
            this.Text = "doimatkhau";
            this.Load += new System.EventHandler(this.doimatkhau_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txttendn;
        private System.Windows.Forms.TextBox txtmkmoii;
        private System.Windows.Forms.TextBox txtmkcu;
        private System.Windows.Forms.TextBox txtmkmoi;
        private System.Windows.Forms.Button btndoimk;
    }
}