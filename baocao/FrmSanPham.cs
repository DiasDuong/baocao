using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace baocao
{
    public partial class FrmSanPham : Form
    {

        DataTable SanPham;

        public FrmSanPham()
        {
            InitializeComponent();

        }

        private void FrmSanPham_Load(object sender, EventArgs e)
        {
            txtMahang.Enabled = false;
            btnLuu.Enabled = false;
            btnBoqua.Enabled = false;
            Load_DataGridView();
            function.FillCombo2("SELECT MaChatLieu, Tenchatlieu FROM Chatlieu", cboMachatlieu, "MaChatLieu", "TenChatLieu");
            function.FillCombo2("SELECT MaLoai, TenLoai FROM TheLoai", cboMaloai, "MaLoai", "TenLoai");
            function.FillCombo2("SELECT MaCo, TenCo FROM Co", cboMaco, "MaCo", "TenCo");
            function.FillCombo2("SELECT MaMau, TenMau FROM Mau", cboMamau, "MaMau", "TenMau");
            function.FillCombo2("SELECT MaDoiTuong, TenDoiTuong FROM DoiTuong", cboMadoituong, "MaDoiTuong", "TenDoiTuong");
            function.FillCombo2("SELECT MaMua, TenMua FROM Mua", cboMamua, "MaMua", "TenMua");
            function.FillCombo2("SELECT MaNSX, TenNSX FROM NoiSanXuat", cboMaNSX, "MaNSX", "TenNSX");
            cboMachatlieu.SelectedIndex = -1;
            ResetValues();
        }
        private void ResetValues()
        {
            txtMahang.Text = "";
            txtTenhang.Text = "";
            cboMachatlieu.Text = "";
            cboMaloai.Text = "";
            cboMaco.Text = "";
            cboMamau.Text = "";
            cboMadoituong.Text = "";
            cboMamua.Text = "";
            cboMaNSX.Text = "";
            txtSoluong.Text = "0";
            txtDongianhap.Text = "0";
            txtDongiaban.Text = "0";
            txtSoluong.Text = "";
            txtDongianhap.Text = "";
            txtDongiaban.Text = "";
            txtAnh.Text = "";
            picAnh.Image = null;
        }

        private void Load_DataGridView()
        {
            string sql = @"SELECT MaQuanAo, TenQuanAo, 
                      SanPham.MaChatLieu, TenChatLieu,
                      SanPham.MaLoai, TenLoai,
                      SanPham.MaCo, TenCo,
                      SanPham.MaMau, TenMau,
                      SanPham.MaDoiTuong, TenDoiTuong,
                      SanPham.MaMua, TenMua,
                      SanPham.MaNSX, TenNSX,
                      SoLuong, DonGiaNhap, DonGiaBan, Anh
               FROM SanPham
               JOIN ChatLieu ON SanPham.MaChatLieu = ChatLieu.MaChatLieu
               JOIN TheLoai ON SanPham.MaLoai = TheLoai.MaLoai
               JOIN Co ON SanPham.MaCo = Co.MaCo
               JOIN Mau ON SanPham.MaMau = Mau.MaMau
               JOIN DoiTuong ON SanPham.MaDoiTuong = DoiTuong.MaDoiTuong
               JOIN Mua ON SanPham.MaMua = Mua.MaMua
               JOIN NoiSanXuat ON SanPham.MaNSX = NoiSanXuat.MaNSX";
            SanPham = function.GetDataToTable(sql);
            dataGridView1.DataSource = SanPham;

            dataGridView1.Columns[0].HeaderText = "Mã quần áo";
            dataGridView1.Columns[1].HeaderText = "Tên quần áo";
            dataGridView1.Columns[2].HeaderText = "Mã chất liệu";
            dataGridView1.Columns[3].HeaderText = "Tên chất liệu";
            dataGridView1.Columns[4].HeaderText = "Mã loại";
            dataGridView1.Columns[5].HeaderText = "Tên loại";
            dataGridView1.Columns[6].HeaderText = "Mã cỡ";
            dataGridView1.Columns[7].HeaderText = "Tên cỡ";
            dataGridView1.Columns[8].HeaderText = "Mã màu";
            dataGridView1.Columns[9].HeaderText = "Tên màu";
            dataGridView1.Columns[10].HeaderText = "Mã đối tượng";
            dataGridView1.Columns[11].HeaderText = "Tên đối tượng";
            dataGridView1.Columns[12].HeaderText = "Mã mùa";
            dataGridView1.Columns[13].HeaderText = "Tên mùa";
            dataGridView1.Columns[14].HeaderText = "Mã NSX";
            dataGridView1.Columns[15].HeaderText = "Tên NSX";
            dataGridView1.Columns[16].HeaderText = "Số lượng";
            dataGridView1.Columns[17].HeaderText = "Đơn giá nhập";
            dataGridView1.Columns[18].HeaderText = "Đơn giá bán";
            dataGridView1.Columns[19].HeaderText = "Ảnh";
        }

        private void LoadProductImage(string imageName)
        {
            try
            {
                // Giải phóng ảnh cũ nếu có
                if (picAnh.Image != null)
                {
                    picAnh.Image.Dispose();
                    picAnh.Image = null;
                }

                string imagePath = Path.Combine(Application.StartupPath, "Images", imageName);
                if (File.Exists(imagePath))
                {
                    // Đọc file ảnh vào memory stream trước
                    byte[] imageBytes = File.ReadAllBytes(imagePath);
                    using (var ms = new MemoryStream(imageBytes))
                    {
                        // Tạo ảnh từ memory stream
                        picAnh.Image = new Bitmap(ms);
                    }
                    txtAnh.Text = imageName;
                }
                else
                {
                    picAnh.Image = null;
                    txtAnh.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải ảnh: " + ex.Message);
                picAnh.Image = null;
                txtAnh.Text = "";
            }
        }

        private void LoadProductInfo(DataGridViewRow row)
        {
            if (row == null) return;

            try
            {
                string ma;
                txtMahang.Text = row.Cells["MaQuanAo"].Value.ToString();
                txtTenhang.Text = row.Cells["TenQuanAo"].Value.ToString();
                
                ma = row.Cells["MaChatLieu"].Value.ToString();
                cboMachatlieu.Text = function.GetFieldValues("SELECT TenChatLieu FROM ChatLieu WHERE MaChatLieu = N'" + ma + "'");
                
                ma = row.Cells["MaLoai"].Value.ToString();
                cboMaloai.Text = function.GetFieldValues("SELECT TenLoai FROM TheLoai WHERE MaLoai = N'" + ma + "'");
                
                ma = row.Cells["MaCo"].Value.ToString();
                cboMaco.Text = function.GetFieldValues("SELECT TenCo FROM Co WHERE MaCo = N'" + ma + "'");
                
                ma = row.Cells["MaMau"].Value.ToString();
                cboMamau.Text = function.GetFieldValues("SELECT TenMau FROM Mau WHERE MaMau = N'" + ma + "'");
                
                ma = row.Cells["MaDoiTuong"].Value.ToString();
                cboMadoituong.Text = function.GetFieldValues("SELECT TenDoiTuong FROM DoiTuong WHERE MaDoiTuong = N'" + ma + "'");
                
                ma = row.Cells["MaMua"].Value.ToString();
                cboMamua.Text = function.GetFieldValues("SELECT TenMua FROM Mua WHERE MaMua = N'" + ma + "'");
                
                ma = row.Cells["MaNSX"].Value.ToString();
                cboMaNSX.Text = function.GetFieldValues("SELECT TenNSX FROM NoiSanXuat WHERE MaNSX = N'" + ma + "'");
                
                txtSoluong.Text = row.Cells["SoLuong"].Value.ToString();
                txtDongianhap.Text = row.Cells["DonGiaNhap"].Value.ToString();
                txtDongiaban.Text = row.Cells["DonGiaBan"].Value.ToString();
                
                // Lấy tên file ảnh từ database và hiển thị
                string imageName = row.Cells["Anh"].Value.ToString();
                if (!string.IsNullOrEmpty(imageName))
                {
                    string imagePath = Path.Combine(Application.StartupPath, "Images", imageName);
                    if (File.Exists(imagePath))
                    {
                        // Giải phóng ảnh cũ nếu có
                        if (picAnh.Image != null)
                        {
                            picAnh.Image.Dispose();
                            picAnh.Image = null;
                        }

                        // Load ảnh mới
                        try
                        {
                            byte[] imageBytes = File.ReadAllBytes(imagePath);
                            using (var ms = new MemoryStream(imageBytes))
                            {
                                picAnh.Image = new Bitmap(ms);
                            }
                            txtAnh.Text = imageName;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Lỗi khi tải ảnh: " + ex.Message);
                            picAnh.Image = null;
                            txtAnh.Text = "";
                        }
                    }
                    else
                    {
                        picAnh.Image = null;
                        txtAnh.Text = imageName; // Vẫn giữ tên file trong txtAnh
                    }
                }
                else
                {
                    if (picAnh.Image != null)
                    {
                        picAnh.Image.Dispose();
                        picAnh.Image = null;
                    }
                    txtAnh.Text = "";
                }

                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                btnBoqua.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi hiển thị dữ liệu: " + ex.Message);
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (btnThem.Enabled == false) return; // Đang ở chế độ thêm mới, không load thông tin

            if (SanPham.Rows.Count == 0) return; // Không có dữ liệu

            if (dataGridView1.CurrentRow != null)
            {
                LoadProductInfo(dataGridView1.CurrentRow);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                LoadProductInfo(dataGridView1.CurrentRow);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoqua.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            ResetValues();
            txtMahang.Enabled = true;
            txtMahang.Focus();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            if (txtMahang.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã hàng", "Thông báo", MessageBoxButtons.OK,
MessageBoxIcon.Warning);
                txtMahang.Focus();
                return;
            }
            if (txtTenhang.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên hàng", "Thông báo", MessageBoxButtons.OK,
 MessageBoxIcon.Warning);
                txtTenhang.Focus();
                return;
            }
            if (cboMachatlieu.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập chất liệu", "Thông báo",
MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMachatlieu.Focus();
                return;
            }
            if (txtAnh.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải chọn ảnh minh họa cho hàng", "Thông báo",
MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAnh.Focus();
                return;
            }
            sql = "SELECT MaQuanAo FROM SanPham WHERE MaQuanAo=N'" + txtMahang.Text.Trim() + "'";
            if (function.CheckKey(sql))
            {
                MessageBox.Show("Mã hàng này đã có, bạn phải nhập mã khác", "Thông báo",
MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMahang.Focus();
                txtMahang.Text = "";
                return;
            }
            sql = "INSERT INTO SanPham (MaQuanAo, TenQuanAo, MaLoai, MaCo, MaChatLieu, MaMau, MaDoiTuong, MaMua, MaNSX, SoLuong, DonGiaNhap, DonGiaBan, Anh) " +
      "VALUES (N'" + txtMahang.Text.Trim() + "', " +
               "N'" + txtTenhang.Text.Trim() + "', " +
               "N'" + cboMaloai.SelectedValue.ToString() + "', " +
               "N'" + cboMaco.SelectedValue.ToString() + "', " +
               "N'" + cboMachatlieu.SelectedValue.ToString() + "', " +
               "N'" + cboMamau.SelectedValue.ToString() + "', " +
               "N'" + cboMadoituong.SelectedValue.ToString() + "', " +
               "N'" + cboMamua.SelectedValue.ToString() + "', " +
               "N'" + cboMaNSX.SelectedValue.ToString() + "', " +
               txtSoluong.Text.Trim() + ", " +
               txtDongianhap.Text + ", " +
               txtDongiaban.Text + ", " +
               "N'" + txtAnh.Text + "')";

            function.RunSQL(sql);
            Load_DataGridView();
            ResetValues();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnBoqua.Enabled = false;
            btnLuu.Enabled = false;
            txtMahang.Enabled = false;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql;
            if (SanPham.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK,
MessageBoxIcon.Information);
                return;
            }
            if (txtMahang.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo",
MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtTenhang.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên hàng", "Thông báo", MessageBoxButtons.OK,
MessageBoxIcon.Warning);
                txtTenhang.Focus();
                return;
            }
            if (cboMachatlieu.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập chất liệu", "Thông báo",
MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMachatlieu.Focus();
                return;
            }
            if (txtAnh.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải chọn ảnh minh họa cho hàng", "Thông báo",
MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAnh.Focus();
                return;
            }
            sql = "UPDATE SanPham SET " +
                "TenQuanAo = N'" + txtTenhang.Text.Trim() + "', " +
                "MaLoai = N'" + cboMaloai.SelectedValue.ToString() + "', " +
                "MaCo = N'" + cboMaco.SelectedValue.ToString() + "', " +
                "MaChatLieu = N'" + cboMachatlieu.SelectedValue.ToString() + "', " +
                "MaMau = N'" + cboMamau.SelectedValue.ToString() + "', " +
                "MaDoiTuong = N'" + cboMadoituong.SelectedValue.ToString() + "', " +
                "MaMua = N'" + cboMamua.SelectedValue.ToString() + "', " +
                "MaNSX = N'" + cboMaNSX.SelectedValue.ToString() + "', " +
                "SoLuong = " + txtSoluong.Text.Trim() + ", " +
                "DonGiaNhap = " + txtDongianhap.Text + ", " +
                "DonGiaBan = " + txtDongiaban.Text + ", " +
                "Anh = N'" + txtAnh.Text + "' " +
                "WHERE MaQuanAo = N'" + txtMahang.Text + "'";
            function.RunSQL(sql);
            Load_DataGridView();
            ResetValues();
            btnBoqua.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if (SanPham.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK,
MessageBoxIcon.Information);
                return;
            }
            if (txtMahang.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo",
MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo",
MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                sql = "DELETE SanPham WHERE MaQuanAo=N'" + txtMahang.Text + "'";
                function.RunDeleteSQL(sql);
                Load_DataGridView();
                ResetValues();
            }

        }

        private void btnBoqua_Click(object sender, EventArgs e)
        {
            ResetValues();
            btnBoqua.Enabled = false;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
            txtMahang.Enabled = false;

        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Image Files (*.jpg; *.jpeg; *.png; *.bmp)|*.jpg;*.jpeg;*.png;*.bmp";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string fileName = Path.GetFileName(openFile.FileName);
                    string destFile = Path.Combine(Application.StartupPath, "Images", fileName);
                    string imageDir = Path.Combine(Application.StartupPath, "Images");

                    // Giải phóng ảnh cũ
                    if (picAnh.Image != null)
                    {
                        picAnh.Image.Dispose();
                        picAnh.Image = null;
                    }

                    // Tạo thư mục nếu chưa tồn tại
                    if (!Directory.Exists(imageDir))
                    {
                        Directory.CreateDirectory(imageDir);
                    }

                    // Đọc file ảnh vào bộ nhớ
                    byte[] imageBytes = File.ReadAllBytes(openFile.FileName);

                    // Lưu file ảnh mới
                    File.WriteAllBytes(destFile, imageBytes);

                    // Load ảnh từ byte array
                    using (var ms = new MemoryStream(imageBytes))
                    {
                        picAnh.Image = new Bitmap(ms);
                    }

                    txtAnh.Text = fileName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xử lý file ảnh: " + ex.Message);
                }
            }
        }
       
        

        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            string sql;
            if ((txtMahang.Text == "") && (txtTenhang.Text == "") && (cboMachatlieu.Text == ""))
            {
                MessageBox.Show("Hãy nhập một điều kiện tìm kiếm!!!", "Yêu cầu ...",
MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            sql = "SELECT * FROM SanPham WHERE 1=1";

            if (txtMahang.Text != "")
                sql = sql + " AND MaQuanAo LIKE N'%" + txtMahang.Text + "%'";

            if (txtTenhang.Text != "")
                sql = sql + " AND TenQuanAo LIKE N'%" + txtTenhang.Text + "%'";

            if (cboMachatlieu.Text != "")
                sql = sql + " AND MaChatLieu LIKE N'%" + cboMachatlieu.SelectedValue + "%'";

            if (cboMaloai.Text != "")
                sql = sql + " AND MaLoai LIKE N'%" + cboMaloai.SelectedValue + "%'";

            if (cboMaco.Text != "")
                sql = sql + " AND MaCo LIKE N'%" + cboMaco.SelectedValue + "%'";

            if (cboMamau.Text != "")
                sql = sql + " AND MaMau LIKE N'%" + cboMamau.SelectedValue + "%'";

            if (cboMadoituong.Text != "")
                sql = sql + " AND MaDoiTuong LIKE N'%" + cboMadoituong.SelectedValue + "%'";

            if (cboMamua.Text != "")
                sql = sql + " AND MaMua LIKE N'%" + cboMamua.SelectedValue + "%'";

            if (cboMaNSX.Text != "")
                sql = sql + " AND MaNSX LIKE N'%" + cboMaNSX.SelectedValue + "%'";

            SanPham = function.GetDataToTable(sql);

            if (SanPham.Rows.Count == 0)
                MessageBox.Show("Không có bản ghi thỏa mãn điều kiện!!!", "Thông báo",
MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
                MessageBox.Show("Có " + SanPham.Rows.Count + " bản ghi thỏa mãn điều kiện!!!",
"Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            dataGridView1.DataSource = SanPham;
            ResetValues();
        }

        private void btnHienthi_Click(object sender, EventArgs e)
        {
            string sql;
            sql = "SELECT MaQuanAo, TenQuanAo, MaLoai, MaCo, MaChatLieu, MaMau, MaDoiTuong, MaMua, MaNSX, SoLuong, DonGiaNhap, DonGiaBan, Anh from SanPham";
            SanPham = function.GetDataToTable(sql);
            dataGridView1.DataSource = SanPham;

        }

        private void txtDongianhap_TextChanged(object sender, EventArgs e)
        {
            if (double.TryParse(txtDongianhap.Text, out double giaNhap))
            {
                txtDongiaban.Text = (giaNhap * 1.1).ToString("0.00");
            }
        }

        private void txtDongiaban_TextChanged(object sender, EventArgs e)
        {
            // Xử lý khi giá trị đơn giá bán thay đổi
            if (!double.TryParse(txtDongiaban.Text, out _))
            {
                // Nếu giá trị không phải là số, xóa nội dung
                txtDongiaban.Text = "";
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            
            this.Close();
        }

        private void FrmSanPham_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Giải phóng tài nguyên ảnh khi đóng form
            if (picAnh.Image != null)
            {
                picAnh.Image.Dispose();
                picAnh.Image = null;
            }
        }
    }
}
