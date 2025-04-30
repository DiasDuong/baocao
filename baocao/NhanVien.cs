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

namespace baocao
{
    public partial class NhanVien : Form
    {
        public NhanVien()
        {
            InitializeComponent();
   
        }

        private void NhanVien_Load(object sender, EventArgs e)
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
            
            string sql = "Select * from NhanVien";
            DataTable dt = function.LoadDataToTable(sql);
            dataGridView.DataSource = dt;
            if (dataGridView.Columns.Count >= 7)
            {
                dataGridView.Columns[0].HeaderText = "Mã nhân viên";
                dataGridView.Columns[1].HeaderText = "Tên nhân viên";
                dataGridView.Columns[2].HeaderText = "Giới tính";
                dataGridView.Columns[3].HeaderText = "Ngày sinh";
                dataGridView.Columns[4].HeaderText = "Điện thoại";
                dataGridView.Columns[5].HeaderText = "Địa chỉ";
                dataGridView.Columns[6].HeaderText = "Mã chức vụ";
            }

            dataGridView.AllowUserToAddRows = false;
        }
        private void clear()
        {
            txtMaNhanVien.Enabled = true;
            txtMaNhanVien.Text = "";
            txtTennhanvien.Text = "";
            chkNam.Checked = false;
            chkNu.Checked = false;
            txtDiachi.Text = "";
            mskDienthoai.Text = "";
            mskNgaysinh.Text = "";
            txtMaCV.Text = "";
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để hiển thị");
                return;
            }
            txtMaNhanVien.Text = dataGridView.CurrentRow.Cells[0].Value.ToString();  // MaNV
            txtTennhanvien.Text = dataGridView.CurrentRow.Cells[1].Value.ToString(); // TenNV

            string GioiTinh = dataGridView.CurrentRow.Cells[2].Value.ToString();     // GioiTinh
            chkNam.Checked = (GioiTinh == "Nam");
            chkNu.Checked = !chkNam.Checked;

            mskNgaysinh.Text = dataGridView.CurrentRow.Cells[3].Value.ToString();    // NgaySinh
            mskDienthoai.Text = dataGridView.CurrentRow.Cells[4].Value.ToString();   // DienThoai
            txtDiachi.Text = dataGridView.CurrentRow.Cells[5].Value.ToString();      // DiaChi
            txtMaCV.Text = dataGridView.CurrentRow.Cells[6].Value.ToString();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (!CheckData())
                return;

            string GioiTinh = "";
            if (chkNam.Checked)
                GioiTinh = "Nam";
            else if (chkNu.Checked)
                GioiTinh = "Nữ";

            string sql = "INSERT INTO NhanVien (MaNV, TenNV, GioiTinh, DiaChi, DienThoai, NgaySinh, MaCV) " +
                         "VALUES ('" + txtMaNhanVien.Text + "', N'" + txtTennhanvien.Text + "', '" + GioiTinh + "', N'" +
                         txtDiachi.Text + "', '" + mskDienthoai.Text + "', '" +
                         function.getSQLdateFromText(mskNgaysinh.Text) + "', '" + txtMaCV.Text + "')";

            SqlCommand sqlCommand = new SqlCommand(sql, function.conn);
            sqlCommand.ExecuteNonQuery();
            loadDataToGridView();
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnLuu.Enabled = false;
        }
        private bool CheckData()
        {
            if (string.IsNullOrWhiteSpace(txtMaNhanVien.Text))
            {
                MessageBox.Show("Bạn chưa nhập mã nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTennhanvien.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtTennhanvien.Text))
            {
                MessageBox.Show("Bạn chưa nhập tên nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTennhanvien.Focus();
                return false;
            }

            if (!chkNam.Checked && !chkNu.Checked)
            {
                MessageBox.Show("Bạn chưa chọn giới tính!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

            if (string.IsNullOrWhiteSpace(mskNgaysinh.Text) || mskNgaysinh.Text.Length != 10)
            {
                MessageBox.Show("Ngày sinh không hợp lệ (dd/mm/yyyy)!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mskNgaysinh.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtMaCV.Text))
            {
                MessageBox.Show("Bạn chưa nhập mã chức vụ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaCV.Focus();
                return false;
            }

            return true;

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (CheckData())
            {
                string MaNV = txtMaNhanVien.Text.Trim();
                string TenNV = txtTennhanvien.Text.Trim();
                string GioiTinh = chkNam.Checked ? "Nam" : "Nữ";
                string NgaySinh = function.getSQLdateFromText(mskNgaysinh.Text);
                string DienThoai = mskDienthoai.Text.Trim();
                string DiaChi = txtDiachi.Text.Trim();
                string MaCV = txtMaCV.Text.Trim();


                string sql = "INSERT INTO NhanVien (MaNV, TenNV, GioiTinh, NgaySinh, DienThoai, DiaChi, MaCV) " +
               "VALUES (@MaNhanVien, @TenNhanVien, @GioiTinh, @NgaySinh, @DienThoai, @DiaChi, @MaCV)";


                // Tạo đối tượng SqlCommand
                SqlCommand command = new SqlCommand(sql, function.conn);

                // Thêm tham số vào câu lệnh SQL
                command.Parameters.AddWithValue("@MaNhanVien", txtMaNhanVien.Text.Trim()); // Dùng txtMaNhanVien.Text cho mã nhân viên
                command.Parameters.AddWithValue("@TenNhanVien", TenNV);
                command.Parameters.AddWithValue("@GioiTinh", GioiTinh);
                command.Parameters.AddWithValue("@NgaySinh", NgaySinh);
                command.Parameters.AddWithValue("@DienThoai", DienThoai);
                command.Parameters.AddWithValue("@DiaChi", DiaChi);
                command.Parameters.AddWithValue("@MaCV", MaCV);

                try
                {
                    // Mở kết nối và thực thi câu lệnh SQL
                    function.Connect();
                    command.ExecuteNonQuery();  // Thực thi câu lệnh INSERT

                    // Cập nhật lại DataGridView sau khi thêm dữ liệu thành công
                    string sql2 = "SELECT * FROM NhanVien";
                    SqlDataAdapter adapter = new SqlDataAdapter(sql2, function.conn);
                    DataTable NhanVien = new DataTable();
                    adapter.Fill(NhanVien);
                    dataGridView.DataSource = NhanVien;

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
            if (string.IsNullOrWhiteSpace(txtMaNhanVien.Text))
            {
                MessageBox.Show("Chọn nhân viên để xóa!", "Cảnh báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Xác nhận xóa nhân viên
            var kq = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa nhân viên '{txtTennhanvien.Text}'?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (kq != DialogResult.Yes)
                return;

            // Câu lệnh SQL để xóa nhân viên
            string sql = "DELETE FROM NhanVien WHERE MaNV = @MaNV";

            using (var cmd = new SqlCommand(sql, function.conn))
            {
                cmd.Parameters.AddWithValue("@MaNV", txtMaNhanVien.Text.Trim());

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
                            dataGridView.DataSource = dt;
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
            if (string.IsNullOrWhiteSpace(txtTennhanvien.Text))
            {
                MessageBox.Show("Họ tên không được để trống!");
                txtTennhanvien.Focus();
                return;
            }

            string MaNV = txtMaNhanVien.Text.Trim();
            string TenNV = txtTennhanvien.Text.Trim();
            string GioiTinh = chkNam.Checked ? "Nam" : "Nữ";  // Kiểm tra giới tính Nam hay Nữ
            string NgaySinh = function.getSQLdateFromText(mskNgaysinh.Text);
            string DienThoai = mskDienthoai.Text.Trim();
            string DiaChi = txtDiachi.Text.Trim();
            string MaCV = txtMaCV.Text.Trim();

            // Câu lệnh SQL UPDATE với tham số hóa để tránh SQL Injection
            string sql = "UPDATE NhanVien SET TenNV = @TenNV, GioiTinh = @GioiTinh, NgaySinh = @NgaySinh, " +
                   "DienThoai = @DienThoai, DiaChi = @DiaChi, MaCV = @MaCV WHERE MaNV = @MaNV";

            using (SqlCommand cmd = new SqlCommand(sql, function.conn))
            {
                // Thêm tham số vào câu lệnh SQL
                cmd.Parameters.AddWithValue("@MaNV", MaNV);
                cmd.Parameters.AddWithValue("@TenNV", TenNV);
                cmd.Parameters.AddWithValue("@GioiTinh", GioiTinh);
                cmd.Parameters.AddWithValue("@NgaySinh", NgaySinh);
                cmd.Parameters.AddWithValue("@DienThoai", DienThoai);
                cmd.Parameters.AddWithValue("@DiaChi", DiaChi);
                cmd.Parameters.AddWithValue("@MaCV", MaCV);

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

                    function.Close();
                }
            }
        }
        private void LoadData()
        {
            string sql = "SELECT MaNV, TenNV, GioiTinh, NgaySinh, DienThoai, DiaChi, MaCV FROM NhanVien";
            using (SqlDataAdapter adapter = new SqlDataAdapter(sql, function.conn))
            {
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView.DataSource = dt;
            }
        }

        private void btnBoqua_Click(object sender, EventArgs e)
        {
            btnBoqua.Enabled = false;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
            txtMaNhanVien.Enabled = false;

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }


}
