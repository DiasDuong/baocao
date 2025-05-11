namespace baocao
{
    partial class Tksanpham
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
            this.label4 = new System.Windows.Forms.Label();
            this.dgridSanpham = new System.Windows.Forms.DataGridView();
            this.btnTim = new System.Windows.Forms.Button();
            this.btnHienthi = new System.Windows.Forms.Button();
            this.btnThoat = new System.Windows.Forms.Button();
            this.grpThongtin = new System.Windows.Forms.GroupBox();
            this.txtMasanpham = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblTen = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtTensanpham = new System.Windows.Forms.TextBox();
            this.cboMaloai = new System.Windows.Forms.ComboBox();
            this.cboMaco = new System.Windows.Forms.ComboBox();
            this.cboMachatlieu = new System.Windows.Forms.ComboBox();
            this.cboMamau = new System.Windows.Forms.ComboBox();
            this.cboMaNSX = new System.Windows.Forms.ComboBox();
            this.cboMadoituong = new System.Windows.Forms.ComboBox();
            this.cboMamua = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgridSanpham)).BeginInit();
            this.grpThongtin.SuspendLayout();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.Font = new System.Drawing.Font("Microsoft YaHei UI", 25.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(353, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(502, 57);
            this.label4.TabIndex = 12;
            this.label4.Text = "TÌM KIẾM SẢN PHẨM";
            // 
            // dgridSanpham
            // 
            this.dgridSanpham.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgridSanpham.Location = new System.Drawing.Point(90, 287);
            this.dgridSanpham.Name = "dgridSanpham";
            this.dgridSanpham.RowHeadersWidth = 51;
            this.dgridSanpham.RowTemplate.Height = 24;
            this.dgridSanpham.Size = new System.Drawing.Size(1103, 217);
            this.dgridSanpham.TabIndex = 13;
            // 
            // btnTim
            // 
            this.btnTim.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnTim.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTim.Location = new System.Drawing.Point(145, 524);
            this.btnTim.Name = "btnTim";
            this.btnTim.Size = new System.Drawing.Size(212, 66);
            this.btnTim.TabIndex = 14;
            this.btnTim.Text = "Tìm kiếm";
            this.btnTim.UseVisualStyleBackColor = false;
            this.btnTim.Click += new System.EventHandler(this.btnTim_Click);
            // 
            // btnHienthi
            // 
            this.btnHienthi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnHienthi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHienthi.Location = new System.Drawing.Point(549, 524);
            this.btnHienthi.Name = "btnHienthi";
            this.btnHienthi.Size = new System.Drawing.Size(212, 66);
            this.btnHienthi.TabIndex = 15;
            this.btnHienthi.Text = "Hiển thị";
            this.btnHienthi.UseVisualStyleBackColor = false;
            this.btnHienthi.Click += new System.EventHandler(this.btnHienthi_Click);
            // 
            // btnThoat
            // 
            this.btnThoat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnThoat.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThoat.Location = new System.Drawing.Point(928, 524);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(212, 66);
            this.btnThoat.TabIndex = 16;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = false;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // grpThongtin
            // 
            this.grpThongtin.Controls.Add(this.cboMamua);
            this.grpThongtin.Controls.Add(this.cboMadoituong);
            this.grpThongtin.Controls.Add(this.cboMaNSX);
            this.grpThongtin.Controls.Add(this.cboMamau);
            this.grpThongtin.Controls.Add(this.cboMachatlieu);
            this.grpThongtin.Controls.Add(this.cboMaco);
            this.grpThongtin.Controls.Add(this.cboMaloai);
            this.grpThongtin.Controls.Add(this.txtTensanpham);
            this.grpThongtin.Controls.Add(this.label10);
            this.grpThongtin.Controls.Add(this.lblTen);
            this.grpThongtin.Controls.Add(this.label8);
            this.grpThongtin.Controls.Add(this.label7);
            this.grpThongtin.Controls.Add(this.label6);
            this.grpThongtin.Controls.Add(this.label5);
            this.grpThongtin.Controls.Add(this.label3);
            this.grpThongtin.Controls.Add(this.label2);
            this.grpThongtin.Controls.Add(this.label1);
            this.grpThongtin.Controls.Add(this.txtMasanpham);
            this.grpThongtin.Location = new System.Drawing.Point(146, 91);
            this.grpThongtin.Name = "grpThongtin";
            this.grpThongtin.Size = new System.Drawing.Size(1019, 176);
            this.grpThongtin.TabIndex = 17;
            this.grpThongtin.TabStop = false;
            this.grpThongtin.Text = "Thông tin tìm kiếm";
            // 
            // txtMasanpham
            // 
            this.txtMasanpham.Location = new System.Drawing.Point(141, 38);
            this.txtMasanpham.Name = "txtMasanpham";
            this.txtMasanpham.Size = new System.Drawing.Size(153, 22);
            this.txtMasanpham.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(46, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Mã quần áo";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(649, 141);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Tên nhà sản xuất";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(697, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "Tên mùa";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(362, 143);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 16);
            this.label5.TabIndex = 4;
            this.label5.Text = "Tên màu";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(345, 90);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 16);
            this.label6.TabIndex = 5;
            this.label6.Text = "Tên chất liệu";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(374, 38);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 16);
            this.label7.TabIndex = 6;
            this.label7.Text = "Tên cỡ";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(46, 138);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(64, 16);
            this.label8.TabIndex = 7;
            this.label8.Text = "Tên loại";
            // 
            // lblTen
            // 
            this.lblTen.AutoSize = true;
            this.lblTen.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTen.Location = new System.Drawing.Point(40, 90);
            this.lblTen.Name = "lblTen";
            this.lblTen.Size = new System.Drawing.Size(94, 16);
            this.lblTen.TabIndex = 8;
            this.lblTen.Text = "Tên quần áo";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(671, 38);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(102, 16);
            this.label10.TabIndex = 9;
            this.label10.Text = "Tên đối tượng";
            // 
            // txtTensanpham
            // 
            this.txtTensanpham.Location = new System.Drawing.Point(141, 90);
            this.txtTensanpham.Name = "txtTensanpham";
            this.txtTensanpham.Size = new System.Drawing.Size(153, 22);
            this.txtTensanpham.TabIndex = 10;
            // 
            // cboMaloai
            // 
            this.cboMaloai.FormattingEnabled = true;
            this.cboMaloai.Location = new System.Drawing.Point(141, 138);
            this.cboMaloai.Name = "cboMaloai";
            this.cboMaloai.Size = new System.Drawing.Size(153, 24);
            this.cboMaloai.TabIndex = 11;
            // 
            // cboMaco
            // 
            this.cboMaco.FormattingEnabled = true;
            this.cboMaco.Location = new System.Drawing.Point(459, 38);
            this.cboMaco.Name = "cboMaco";
            this.cboMaco.Size = new System.Drawing.Size(153, 24);
            this.cboMaco.TabIndex = 12;
            // 
            // cboMachatlieu
            // 
            this.cboMachatlieu.FormattingEnabled = true;
            this.cboMachatlieu.Location = new System.Drawing.Point(462, 87);
            this.cboMachatlieu.Name = "cboMachatlieu";
            this.cboMachatlieu.Size = new System.Drawing.Size(153, 24);
            this.cboMachatlieu.TabIndex = 13;
            // 
            // cboMamau
            // 
            this.cboMamau.FormattingEnabled = true;
            this.cboMamau.Location = new System.Drawing.Point(462, 138);
            this.cboMamau.Name = "cboMamau";
            this.cboMamau.Size = new System.Drawing.Size(153, 24);
            this.cboMamau.TabIndex = 14;
            // 
            // cboMaNSX
            // 
            this.cboMaNSX.FormattingEnabled = true;
            this.cboMaNSX.Location = new System.Drawing.Point(810, 135);
            this.cboMaNSX.Name = "cboMaNSX";
            this.cboMaNSX.Size = new System.Drawing.Size(153, 24);
            this.cboMaNSX.TabIndex = 15;
            // 
            // cboMadoituong
            // 
            this.cboMadoituong.FormattingEnabled = true;
            this.cboMadoituong.Location = new System.Drawing.Point(810, 35);
            this.cboMadoituong.Name = "cboMadoituong";
            this.cboMadoituong.Size = new System.Drawing.Size(153, 24);
            this.cboMadoituong.TabIndex = 16;
            // 
            // cboMamua
            // 
            this.cboMamua.FormattingEnabled = true;
            this.cboMamua.Location = new System.Drawing.Point(810, 87);
            this.cboMamua.Name = "cboMamua";
            this.cboMamua.Size = new System.Drawing.Size(153, 24);
            this.cboMamua.TabIndex = 17;
            // 
            // Tksanpham
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1261, 611);
            this.Controls.Add(this.grpThongtin);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnHienthi);
            this.Controls.Add(this.btnTim);
            this.Controls.Add(this.dgridSanpham);
            this.Controls.Add(this.label4);
            this.Name = "Tksanpham";
            this.Text = "Tksanpham";
            this.Load += new System.EventHandler(this.Tksanpham_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgridSanpham)).EndInit();
            this.grpThongtin.ResumeLayout(false);
            this.grpThongtin.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dgridSanpham;
        private System.Windows.Forms.Button btnTim;
        private System.Windows.Forms.Button btnHienthi;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.GroupBox grpThongtin;
        private System.Windows.Forms.TextBox txtMasanpham;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblTen;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTensanpham;
        private System.Windows.Forms.ComboBox cboMamau;
        private System.Windows.Forms.ComboBox cboMachatlieu;
        private System.Windows.Forms.ComboBox cboMaco;
        private System.Windows.Forms.ComboBox cboMaloai;
        private System.Windows.Forms.ComboBox cboMamua;
        private System.Windows.Forms.ComboBox cboMadoituong;
        private System.Windows.Forms.ComboBox cboMaNSX;
    }
}