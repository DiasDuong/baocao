using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace baocao
{
    public partial class Khachhang : Form
    {
        public Khachhang()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Khachhang_Load(object sender, EventArgs e)
        {
            try
            {
                loadDataToGridView();

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void loadDataToGridView()
        {

            string sql = "Select * from KhachHang";
            DataTable dt = function.LoadDataToTable(sql);
            dataGridViewKhachhang.DataSource = dt;
            if (dataGridViewKhachhang.Columns.Count >= 4)
            {
                dataGridViewKhachhang.Columns[0].HeaderText = "Mã khách hàng ";
                dataGridViewKhachhang.Columns[1].HeaderText = "Tên khách hàng ";
                dataGridViewKhachhang.Columns[2].HeaderText = "Địa chỉ";
                dataGridViewKhachhang.Columns[3].HeaderText = "Điện thoại";

            }

            dataGridViewKhachhang.AllowUserToAddRows = false;
        }
        private void clear()
        {
            txtMakhachhang.Enabled = true;
            txtTenkhachhang.Text = "";
            txtDiachi.Text = "";
            mskDienthoai.Text = "";
        }

        private void dataGridViewKhachhang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewKhachhang.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để hiển thị");
                return;
            }
            txtMakhachhang.Text = dataGridViewKhachhang.CurrentRow.Cells[0].Value.ToString();  // MaNV
            txtTenkhachhang.Text = dataGridViewKhachhang.CurrentRow.Cells[1].Value.ToString(); // TenNV
            txtDiachi.Text = dataGridViewKhachhang.CurrentRow.Cells[2].Value.ToString();
            mskDienthoai.Text = dataGridViewKhachhang.CurrentRow.Cells[3].Value.ToString();   // DienThoai

        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql = "INSERT INTO KhachHang(MaKhach, TenKhach, DiaChi, DienThoai) " +
             "VALUES ('" + txtMakhachhang.Text + "', N'" + txtTenkhachhang.Text + "', N'" + txtDiachi.Text + "', '" + mskDienthoai.Text + "')";

            SqlCommand sqlCommand = new SqlCommand(sql, function.conn);
            sqlCommand.ExecuteNonQuery();
            loadDataToGridView();
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnLuu.Enabled = false;
        }
        private bool CheckData()
        {
            if (string.IsNullOrWhiteSpace(txtMakhachhang.Text))
            {
                MessageBox.Show("Bạn chưa nhập mã khách hang!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMakhachhang.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtTenkhachhang.Text))
            {
                MessageBox.Show("Bạn chưa nhập tên khachhang!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenkhachhang.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDiachi.Text))
            {
                MessageBox.Show("Bạn chưa nhập địa chỉ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiachi.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(mskDienthoai.Text) || mskDienthoai.Text.Length < 10)
            {
                MessageBox.Show("Số điện thoại không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mskDienthoai.Focus();
                return false;
            }
            return true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (CheckData())
            {
                string MaKhach = txtMakhachhang.Text.Trim();
                string TenKhach = txtTenkhachhang.Text.Trim();
                string DiaChi = txtDiachi.Text.Trim();
                string DienThoai = mskDienthoai.Text.Trim();


                string sql = "INSERT INTO KhachHang (MaKhach, TenKhach, DiaChi, DienThoai) " +
               "VALUES (@MaKhach, @TenKhach, @DiaChi, @DienThoai)";


                // Tạo đối tượng SqlCommand
                SqlCommand command = new SqlCommand(sql, function.conn);

                // Thêm tham số vào câu lệnh SQL
                command.Parameters.AddWithValue("@MaKhach", txtMakhachhang.Text.Trim()); // Dùng txtMaNhanVien.Text cho mã nhân viên
                command.Parameters.AddWithValue("@TenKhach", txtTenkhachhang.Text.Trim());
                command.Parameters.AddWithValue("@DiaChi", DiaChi);
                command.Parameters.AddWithValue("@DienThoai", DienThoai);



                try
                {
                    // Mở kết nối và thực thi câu lệnh SQL
                    function.Connect();
                    command.ExecuteNonQuery();  // Thực thi câu lệnh INSERT

                    // Cập nhật lại DataGridView sau khi thêm dữ liệu thành công
                    string sql2 = "SELECT * FROM KhachHang";
                    SqlDataAdapter adapter = new SqlDataAdapter(sql2, function.conn);
                    DataTable NhanVien = new DataTable();
                    adapter.Fill(NhanVien);
                    dataGridViewKhachhang.DataSource = NhanVien;

                    // Hiển thị thông báo thêm nhân viên thành công
                    MessageBox.Show("Thêm nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    // Nếu có lỗi, hiển thị thông báo lỗi
                    MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {

                    function.Close();

                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMakhachhang.Text))
            {
                MessageBox.Show("Chọn nhân viên để xóa!", "Cảnh báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Xác nhận xóa nhân viên
            var kq = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa nhân viên '{txtTenkhachhang.Text}'?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (kq != DialogResult.Yes)
                return;

            // Câu lệnh SQL để xóa nhân viên
            string sql = "DELETE FROM NhanVien WHERE MaKhach = @MaKhach";

            using (var cmd = new SqlCommand(sql, function.conn))
            {
                cmd.Parameters.AddWithValue("@MaNV", txtMakhachhang.Text.Trim());

                try
                {
                    // Mở kết nối trước khi thực thi lệnh SQL
                    function.Connect();
                    int rows = cmd.ExecuteNonQuery(); // Thực thi lệnh xóa

                    // Kiểm tra nếu xóa thành công
                    if (rows > 0)
                    {
                        MessageBox.Show("Xóa thành công!", "Thông báo",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Cập nhật lại DataGridView sau khi xóa
                        string sql2 = "SELECT MaNV, TenNV, GioiTinh, NgaySinh, DienThoai, DiaChi, MaCV FROM NhanVien";
                        using (var adapter = new SqlDataAdapter(sql2, function.conn))
                        {
                            var dt = new DataTable();
                            adapter.Fill(dt);
                            dataGridViewKhachhang.DataSource = dt;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy nhân viên để xóa.", "Lỗi",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Lỗi khi xóa: " + ex.Message, "Lỗi",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {


                    function.Close();

                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenkhachhang.Text))
            {
                MessageBox.Show("Họ tên không được để trống!");
                txtTenkhachhang.Focus();
                return;
            }

            // Lấy dữ liệu từ các TextBox và CheckBox
            string MaKhach = txtMakhachhang.Text.Trim();
            string TenKhach = txtTenkhachhang.Text.Trim();
            string DiaChi = txtDiachi.Text.Trim();
            string DienThoai = mskDienthoai.Text.Trim();
            

            // Câu lệnh SQL UPDATE với tham số hóa để tránh SQL Injection
            string sql = "UPDATE KhachHang SET TenKhach = @TenKhach,  " +
                         "DiaChi = @DiaChi, DienThoai = @DienThoai,MaKhach = @MaKhach WHERE MaKhach = @MaKhach";

            using (SqlCommand cmd = new SqlCommand(sql, function.conn))
            {
                // Thêm tham số vào câu lệnh SQL
                cmd.Parameters.AddWithValue("@MaKhach", MaKhach);
                cmd.Parameters.AddWithValue("@TenKhach", TenKhach);
                cmd.Parameters.AddWithValue("@DiaChi", DiaChi);
                cmd.Parameters.AddWithValue("@DienThoai", DienThoai);
                try
                {
                    // Mở kết nối và thực thi câu lệnh SQL
                    function.Connect();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    // Kiểm tra số dòng bị ảnh hưởng
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Cập nhật nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // Cập nhật lại DataGridView sau khi thay đổi dữ liệu
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy nhân viên cần cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    // Nếu có lỗi xảy ra, hiển thị thông báo lỗi
                    MessageBox.Show("Lỗi khi cập nhật: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    function.Close(); // Đảm bảo luôn đóng kết nối
                }
            }

        }
        private void LoadData()
        {
            string sql = "SELECT MaKhach, TenKhach, DiaChi, DienThoai FROM KhachHang";
            using (SqlDataAdapter adapter = new SqlDataAdapter(sql, function.conn))
            {
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridViewKhachhang.DataSource = dt;
            }
        }

        private void btnBoqua_Click(object sender, EventArgs e)
        {
            btnBoqua.Enabled = false;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
            txtMakhachhang.Enabled = false;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

