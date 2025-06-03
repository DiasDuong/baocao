using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace baocao
{
    public partial class Tksanpham : Form
    {
        public Tksanpham()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
       "Bạn có chắc muốn thoát không?",
       "Xác nhận thoát",
       MessageBoxButtons.YesNo,
       MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Close(); // Đóng form hiện tại  
                              // Hoặc Application.Exit(); để thoát toàn ứng dụng  
            }
        }
        DataTable tblSP;
        private void Load_dgridSanpham()
        {
            string sql;
            sql = @"SELECT SanPham.MaQuanAo, SanPham.TenQuanAo, TheLoai.TenLoai, 
       Co.TenCo, ChatLieu.TenChatLieu, 
       Mau.TenMau, DoiTuong.TenDoiTuong, Mua.TenMua, NoiSanXuat.TenNSX, 
       SanPham.SoLuong, SanPham.DonGiaNhap, SanPham.DonGiaBan
FROM SanPham
JOIN TheLoai ON SanPham.MaLoai = TheLoai.MaLoai
JOIN Co ON SanPham.MaCo = Co.MaCo
JOIN ChatLieu ON SanPham.MaChatLieu = ChatLieu.MaChatLieu
JOIN Mau ON SanPham.MaMau = Mau.MaMau
JOIN DoiTuong ON SanPham.MaDoiTuong = DoiTuong.MaDoiTuong
JOIN Mua ON SanPham.MaMua = Mua.MaMua
JOIN NoiSanXuat ON SanPham.MaNSX = NoiSanXuat.MaNSX;";
            tblSP = function.GetDataToTable(sql);
            dgridSanpham.DataSource = tblSP;
            dgridSanpham.Columns[0].HeaderText = "Mã sản phẩm ";
            dgridSanpham.Columns[1].HeaderText = "Tên sản phẩm";
            dgridSanpham.Columns[2].HeaderText = "Loại";
            dgridSanpham.Columns[3].HeaderText = "Kích cỡ";
            dgridSanpham.Columns[4].HeaderText = "Chất liệu";
            dgridSanpham.Columns[5].HeaderText = "Màu sắc";
            dgridSanpham.Columns[6].HeaderText = "Đối tượng";
            dgridSanpham.Columns[7].HeaderText = "Mùa";
            dgridSanpham.Columns[8].HeaderText = "Nơi sản xuất";
            dgridSanpham.Columns[9].HeaderText = "Số lượng";
            dgridSanpham.Columns[10].HeaderText = "Đơn giá nhập";
            dgridSanpham.Columns[11].HeaderText = "Đơn giá bán";
            dgridSanpham.Columns[0].Width = 100;
            dgridSanpham.Columns[1].Width = 100;
            dgridSanpham.Columns[2].Width = 60;
            dgridSanpham.Columns[3].Width = 80;
            dgridSanpham.Columns[4].Width = 80;
            dgridSanpham.Columns[5].Width = 80;
            dgridSanpham.Columns[6].Width = 80;
            dgridSanpham.Columns[7].Width = 60;
            dgridSanpham.Columns[8].Width = 100;
            dgridSanpham.Columns[9].Width = 80;
            dgridSanpham.Columns[10].Width = 100;
            dgridSanpham.Columns[11].Width = 90;
            dgridSanpham.AllowUserToAddRows = false;
            dgridSanpham.EditMode = DataGridViewEditMode.EditProgrammatically;

        }
        private void Tksanpham_Load(object sender, EventArgs e)
        {
            function.Connect();
            Load_dgridSanpham();

            function.FillCombo("SELECT MaQuanAo, TenQuanAo FROM SanPham", cboMasanpham, "TenQuanAo", "MaQuanAo");
            cboMasanpham.SelectedIndex = -1; // mặc định chưa chọn

            
            function.FillCombo("SELECT MaLoai, TenLoai FROM TheLoai", cboMaloai, "MaLoai", "TenLoai");
            function.FillCombo("SELECT MaCo, TenCo FROM Co", cboMaco, "MaCo", "TenCo");
            function.FillCombo("SELECT MaChatLieu, TenChatLieu FROM ChatLieu", cboMachatlieu, "MaChatLieu", "TenChatLieu");
            function.FillCombo("SELECT MaMau, TenMau FROM Mau", cboMamau, "MaMau", "TenMau");
            function.FillCombo("SELECT MaDoiTuong, TenDoiTuong FROM DoiTuong", cboMadoituong, "MaDoiTuong", "TenDoiTuong");
            function.FillCombo("SELECT MaMua, TenMua FROM Mua", cboMamua, "MaMua", "TenMua");
            function.FillCombo("SELECT MaNSX, TenNSX FROM NoiSanXuat", cboMaNSX, "MaNSX", "TenNSX");
            cboMaloai.SelectedIndex = -1;
            cboMaco.SelectedIndex = -1;
            cboMachatlieu.SelectedIndex = -1;
            cboMamau.SelectedIndex = -1;
            cboMadoituong.SelectedIndex = -1;
            cboMamua.SelectedIndex = -1;
            cboMaNSX.SelectedIndex = -1;

            ResetValues();
        }
        private void ResetValues()
        {
            cboMasanpham.Text = "";
            txtTensanpham.Text = "";
            cboMasanpham.SelectedIndex = -1;
            cboMaloai.Text = "";
            cboMaco.Text = "";
            cboMachatlieu.Text = "";
            cboMamau.Text = "";
            cboMadoituong.Text = "";
            cboMamua.Text = "";
            cboMaNSX.Text = "";

            cboMaloai.SelectedIndex = -1;
            cboMaco.SelectedIndex = -1;
            cboMachatlieu.SelectedIndex = -1;
            cboMamau.SelectedIndex = -1;
            cboMadoituong.SelectedIndex = -1;
            cboMamua.SelectedIndex = -1;
            cboMaNSX.SelectedIndex = -1;
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            string sql;
            if ((cboMasanpham.Text == "") && (txtTensanpham.Text == "") && (cboMaloai.Text == "")
                && (cboMaco.Text == "")
                && (cboMachatlieu.Text == "")
                && (cboMamau.Text == "")
                && (cboMadoituong.Text == "")
                && (cboMamua.Text == "")
                && (cboMaNSX.Text == ""))
            {
                MessageBox.Show("Hãy nhập một điều kiện tìm kiếm!!!", "Yêu cầu ...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            sql = @"SELECT SanPham.MaQuanAo, SanPham.TenQuanAo, TheLoai.TenLoai, Co.TenCo, 
                  ChatLieu.TenChatLieu, Mau.TenMau, DoiTuong.TenDoiTuong, Mua.TenMua, NoiSanXuat.TenNSX,
                  SanPham.SoLuong, SanPham.DonGiaBan, SanPham.DonGiaNhap
            FROM SanPham
            JOIN TheLoai ON SanPham.MaLoai = TheLoai.MaLoai
            JOIN Co ON SanPham.MaCo = Co.MaCo
            JOIN ChatLieu ON SanPham.MaChatLieu = ChatLieu.MaChatLieu
            JOIN Mau ON SanPham.MaMau = Mau.MaMau
            JOIN DoiTuong ON SanPham.MaDoiTuong = DoiTuong.MaDoiTuong
            JOIN Mua ON SanPham.MaMua = Mua.MaMua
            JOIN NoiSanXuat ON SanPham.MaNSX = NoiSanXuat.MaNSX
            WHERE 1=1";

            // Sửa điều kiện tìm theo mã thành dùng cboMasanpham.SelectedItem (hoặc .Text):
            if (cboMasanpham.Text != "")
                sql += " AND SanPham.MaQuanAo Like N'%" + cboMasanpham.Text + "%'";

            if (txtTensanpham.Text != "")
                sql += " AND SanPham.TenQuanAo Like N'%" + txtTensanpham.Text + "%'";
            if (cboMaloai.Text != "")
                sql += " AND TheLoai.MaLoai Like N'%" + cboMaloai.SelectedValue + "%'";
            if (cboMaco.Text != "")
                sql += " AND Co.MaCo Like N'%" + cboMaco.SelectedValue + "%'";
            if (cboMachatlieu.Text != "")
                sql += " AND ChatLieu.MaChatLieu Like N'%" + cboMachatlieu.SelectedValue + "%'";
            if (cboMamau.Text != "")
                sql += " AND Mau.MaMau Like N'%" + cboMamau.SelectedValue + "%'";
            if (cboMadoituong.Text != "")
                sql += " AND DoiTuong.MaDoiTuong Like N'%" + cboMadoituong.SelectedValue + "%'";
            if (cboMamua.Text != "")
                sql += " AND Mua.MaMua Like N'%" + cboMamua.SelectedValue + "%'";
            if (cboMaNSX.Text != "")
                sql += " AND NoiSanXuat.MaNSX Like N'%" + cboMaNSX.SelectedValue + "%'";

            tblSP = function.GetDataToTable(sql);
            if (tblSP.Rows.Count == 0)
                MessageBox.Show("Không có bản ghi thỏa mãn điều kiện!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
                MessageBox.Show("Có " + tblSP.Rows.Count + " bản ghi thỏa mãn điều kiện!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            dgridSanpham.DataSource = tblSP;
            ResetValues();
        }

        private void btnHienthi_Click(object sender, EventArgs e)
        {
            Load_dgridSanpham();
            ResetValues();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMasanpham.SelectedItem != null)
            {
                // cboMasanpham.SelectedValue sẽ trả về TenQuanAo (theo cách FillCombo bạn khai báo)
                txtTensanpham.Text = cboMasanpham.SelectedValue.ToString();
            }
            else
            {
                txtTensanpham.Text = "";
            }
        }
    }
}
