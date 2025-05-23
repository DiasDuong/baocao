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
            this.cboMamua = new System.Windows.Forms.ComboBox();
            this.cboMadoituong = new System.Windows.Forms.ComboBox();
            this.cboMaNSX = new System.Windows.Forms.ComboBox();
            this.cboMamau = new System.Windows.Forms.ComboBox();
            this.cboMachatlieu = new System.Windows.Forms.ComboBox();
            this.cboMaco = new System.Windows.Forms.ComboBox();
            this.cboMaloai = new System.Windows.Forms.ComboBox();
            this.txtTensanpham = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.lblTen = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMasanpham = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgridSanpham)).BeginInit();
            this.grpThongtin.SuspendLayout();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(178)))), ((int)(((byte)(255)))));
            this.label4.Location = new System.Drawing.Point(353, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(306, 32);
            this.label4.TabIndex = 12;
            this.label4.Text = "TÌM KIẾM SẢN PHẨM";
            // 
            // dgridSanpham
            // 
            this.dgridSanpham.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgridSanpham.Location = new System.Drawing.Point(56, 255);
            this.dgridSanpham.Name = "dgridSanpham";
            this.dgridSanpham.RowHeadersWidth = 51;
            this.dgridSanpham.RowTemplate.Height = 24;
            this.dgridSanpham.Size = new System.Drawing.Size(738, 136);
            this.dgridSanpham.TabIndex = 13;
            // 
            // btnTim
            // 
            this.btnTim.BackColor = System.Drawing.Color.Silver;
            this.btnTim.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTim.Location = new System.Drawing.Point(121, 407);
            this.btnTim.Name = "btnTim";
            this.btnTim.Size = new System.Drawing.Size(150, 36);
            this.btnTim.TabIndex = 14;
            this.btnTim.Text = "Tìm kiếm";
            this.btnTim.UseVisualStyleBackColor = false;
            this.btnTim.Click += new System.EventHandler(this.btnTim_Click);
            // 
            // btnHienthi
            // 
            this.btnHienthi.BackColor = System.Drawing.Color.Silver;
            this.btnHienthi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHienthi.Location = new System.Drawing.Point(359, 407);
            this.btnHienthi.Name = "btnHienthi";
            this.btnHienthi.Size = new System.Drawing.Size(150, 36);
            this.btnHienthi.TabIndex = 15;
            this.btnHienthi.Text = "Hiển thị";
            this.btnHienthi.UseVisualStyleBackColor = false;
            this.btnHienthi.Click += new System.EventHandler(this.btnHienthi_Click);
            // 
            // btnThoat
            // 
            this.btnThoat.BackColor = System.Drawing.Color.Silver;
            this.btnThoat.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThoat.Location = new System.Drawing.Point(630, 407);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(150, 36);
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
            this.grpThongtin.Location = new System.Drawing.Point(12, 81);
            this.grpThongtin.Name = "grpThongtin";
            this.grpThongtin.Size = new System.Drawing.Size(864, 168);
            this.grpThongtin.TabIndex = 17;
            this.grpThongtin.TabStop = false;
            this.grpThongtin.Text = "Thông tin tìm kiếm";
            // 
            // cboMamua
            // 
            this.cboMamua.FormattingEnabled = true;
            this.cboMamua.Location = new System.Drawing.Point(662, 79);
            this.cboMamua.Name = "cboMamua";
            this.cboMamua.Size = new System.Drawing.Size(153, 24);
            this.cboMamua.TabIndex = 17;
            // 
            // cboMadoituong
            // 
            this.cboMadoituong.FormattingEnabled = true;
            this.cboMadoituong.Location = new System.Drawing.Point(662, 33);
            this.cboMadoituong.Name = "cboMadoituong";
            this.cboMadoituong.Size = new System.Drawing.Size(153, 24);
            this.cboMadoituong.TabIndex = 16;
            // 
            // cboMaNSX
            // 
            this.cboMaNSX.FormattingEnabled = true;
            this.cboMaNSX.Location = new System.Drawing.Point(662, 118);
            this.cboMaNSX.Name = "cboMaNSX";
            this.cboMaNSX.Size = new System.Drawing.Size(153, 24);
            this.cboMaNSX.TabIndex = 15;
            // 
            // cboMamau
            // 
            this.cboMamau.FormattingEnabled = true;
            this.cboMamau.Location = new System.Drawing.Point(364, 126);
            this.cboMamau.Name = "cboMamau";
            this.cboMamau.Size = new System.Drawing.Size(153, 24);
            this.cboMamau.TabIndex = 14;
            // 
            // cboMachatlieu
            // 
            this.cboMachatlieu.FormattingEnabled = true;
            this.cboMachatlieu.Location = new System.Drawing.Point(364, 82);
            this.cboMachatlieu.Name = "cboMachatlieu";
            this.cboMachatlieu.Size = new System.Drawing.Size(153, 24);
            this.cboMachatlieu.TabIndex = 13;
            // 
            // cboMaco
            // 
            this.cboMaco.FormattingEnabled = true;
            this.cboMaco.Location = new System.Drawing.Point(364, 35);
            this.cboMaco.Name = "cboMaco";
            this.cboMaco.Size = new System.Drawing.Size(153, 24);
            this.cboMaco.TabIndex = 12;
            // 
            // cboMaloai
            // 
            this.cboMaloai.FormattingEnabled = true;
            this.cboMaloai.Location = new System.Drawing.Point(106, 126);
            this.cboMaloai.Name = "cboMaloai";
            this.cboMaloai.Size = new System.Drawing.Size(153, 24);
            this.cboMaloai.TabIndex = 11;
            // 
            // txtTensanpham
            // 
            this.txtTensanpham.Location = new System.Drawing.Point(106, 84);
            this.txtTensanpham.Name = "txtTensanpham";
            this.txtTensanpham.Size = new System.Drawing.Size(153, 22);
            this.txtTensanpham.TabIndex = 10;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(523, 35);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(102, 16);
            this.label10.TabIndex = 9;
            this.label10.Text = "Tên đối tượng";
            // 
            // lblTen
            // 
            this.lblTen.AutoSize = true;
            this.lblTen.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTen.Location = new System.Drawing.Point(6, 87);
            this.lblTen.Name = "lblTen";
            this.lblTen.Size = new System.Drawing.Size(94, 16);
            this.lblTen.TabIndex = 8;
            this.lblTen.Text = "Tên quần áo";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(6, 126);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(64, 16);
            this.label8.TabIndex = 7;
            this.label8.Text = "Tên loại";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(265, 35);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 16);
            this.label7.TabIndex = 6;
            this.label7.Text = "Tên cỡ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(265, 87);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 16);
            this.label6.TabIndex = 5;
            this.label6.Text = "Tên chất liệu";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(265, 126);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 16);
            this.label5.TabIndex = 4;
            this.label5.Text = "Tên màu";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(523, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "Tên mùa";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(523, 126);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Tên nhà sản xuất";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Mã quần áo";
            // 
            // txtMasanpham
            // 
            this.txtMasanpham.Location = new System.Drawing.Point(106, 35);
            this.txtMasanpham.Name = "txtMasanpham";
            this.txtMasanpham.Size = new System.Drawing.Size(153, 22);
            this.txtMasanpham.TabIndex = 0;
            // 
            // Tksanpham
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(888, 468);
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