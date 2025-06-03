namespace baocao
{
    partial class TimkiemHDN
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
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.dataGridViewTimkiemHDN = new System.Windows.Forms.DataGridView();
            this.btnTimkiem = new System.Windows.Forms.Button();
            this.btnTimlai = new System.Windows.Forms.Button();
            this.btnDong = new System.Windows.Forms.Button();
            this.txtTongtien = new System.Windows.Forms.TextBox();
            this.txtNgayNhap = new System.Windows.Forms.TextBox();
            this.cboSoHDN = new System.Windows.Forms.ComboBox();
            this.txtMaNCC = new System.Windows.Forms.TextBox();
            this.txtMaNV = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTimkiemHDN)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(178)))), ((int)(((byte)(255)))));
            this.label1.Location = new System.Drawing.Point(306, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(436, 37);
            this.label1.TabIndex = 0;
            this.label1.Text = "TÌM KIẾM HÓA ĐƠN NHẬP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(113, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Số hóa đơn";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(573, 148);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "Tổng tiền";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(573, 93);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(131, 20);
            this.label5.TabIndex = 4;
            this.label5.Text = "Mã nhà cung cấp";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(113, 188);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(103, 20);
            this.label6.TabIndex = 5;
            this.label6.Text = "Mã nhân viên";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(113, 130);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(85, 20);
            this.label7.TabIndex = 6;
            this.label7.Text = "Ngày nhập";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // dataGridViewTimkiemHDN
            // 
            this.dataGridViewTimkiemHDN.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTimkiemHDN.Location = new System.Drawing.Point(117, 257);
            this.dataGridViewTimkiemHDN.Name = "dataGridViewTimkiemHDN";
            this.dataGridViewTimkiemHDN.RowHeadersWidth = 62;
            this.dataGridViewTimkiemHDN.RowTemplate.Height = 28;
            this.dataGridViewTimkiemHDN.Size = new System.Drawing.Size(748, 150);
            this.dataGridViewTimkiemHDN.TabIndex = 13;
            this.dataGridViewTimkiemHDN.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewTimkiemHDN_CellContentClick);
            this.dataGridViewTimkiemHDN.DoubleClick += new System.EventHandler(this.dataGridViewTimkiemHDN_DoubleClick);
            // 
            // btnTimkiem
            // 
            this.btnTimkiem.Location = new System.Drawing.Point(117, 458);
            this.btnTimkiem.Name = "btnTimkiem";
            this.btnTimkiem.Size = new System.Drawing.Size(112, 42);
            this.btnTimkiem.TabIndex = 14;
            this.btnTimkiem.Text = "Tìm kiếm";
            this.btnTimkiem.UseVisualStyleBackColor = true;
            this.btnTimkiem.Click += new System.EventHandler(this.btnTimkiem_Click);
            // 
            // btnTimlai
            // 
            this.btnTimlai.Location = new System.Drawing.Point(441, 460);
            this.btnTimlai.Name = "btnTimlai";
            this.btnTimlai.Size = new System.Drawing.Size(103, 42);
            this.btnTimlai.TabIndex = 15;
            this.btnTimlai.Text = "Tìm lại";
            this.btnTimlai.UseVisualStyleBackColor = true;
            this.btnTimlai.Click += new System.EventHandler(this.btnTimlai_Click);
            // 
            // btnDong
            // 
            this.btnDong.Location = new System.Drawing.Point(770, 460);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(95, 38);
            this.btnDong.TabIndex = 16;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = true;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // txtTongtien
            // 
            this.txtTongtien.Location = new System.Drawing.Point(722, 142);
            this.txtTongtien.Name = "txtTongtien";
            this.txtTongtien.Size = new System.Drawing.Size(143, 26);
            this.txtTongtien.TabIndex = 19;
            // 
            // txtNgayNhap
            // 
            this.txtNgayNhap.Location = new System.Drawing.Point(272, 130);
            this.txtNgayNhap.Name = "txtNgayNhap";
            this.txtNgayNhap.Size = new System.Drawing.Size(155, 26);
            this.txtNgayNhap.TabIndex = 21;
            // 
            // cboSoHDN
            // 
            this.cboSoHDN.FormattingEnabled = true;
            this.cboSoHDN.Location = new System.Drawing.Point(272, 79);
            this.cboSoHDN.Name = "cboSoHDN";
            this.cboSoHDN.Size = new System.Drawing.Size(155, 28);
            this.cboSoHDN.TabIndex = 23;
            // 
            // txtMaNCC
            // 
            this.txtMaNCC.Location = new System.Drawing.Point(722, 82);
            this.txtMaNCC.Name = "txtMaNCC";
            this.txtMaNCC.Size = new System.Drawing.Size(143, 26);
            this.txtMaNCC.TabIndex = 26;
            // 
            // txtMaNV
            // 
            this.txtMaNV.Location = new System.Drawing.Point(272, 188);
            this.txtMaNV.Name = "txtMaNV";
            this.txtMaNV.Size = new System.Drawing.Size(155, 26);
            this.txtMaNV.TabIndex = 27;
            // 
            // TimkiemHDN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(999, 585);
            this.Controls.Add(this.txtMaNV);
            this.Controls.Add(this.txtMaNCC);
            this.Controls.Add(this.cboSoHDN);
            this.Controls.Add(this.txtNgayNhap);
            this.Controls.Add(this.txtTongtien);
            this.Controls.Add(this.btnDong);
            this.Controls.Add(this.btnTimlai);
            this.Controls.Add(this.btnTimkiem);
            this.Controls.Add(this.dataGridViewTimkiemHDN);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "TimkiemHDN";
            this.Text = "TimkiemHDN";
            this.Load += new System.EventHandler(this.TimkiemHDN_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTimkiemHDN)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView dataGridViewTimkiemHDN;
        private System.Windows.Forms.Button btnTimkiem;
        private System.Windows.Forms.Button btnTimlai;
        private System.Windows.Forms.Button btnDong;
        private System.Windows.Forms.TextBox txtTongtien;
        private System.Windows.Forms.TextBox txtNgayNhap;
        private System.Windows.Forms.ComboBox cboSoHDN;
        private System.Windows.Forms.TextBox txtMaNCC;
        private System.Windows.Forms.TextBox txtMaNV;
    }
}