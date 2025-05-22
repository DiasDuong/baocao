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
    public partial class KhachHang : Form
    {
        public KhachHang()
        {
            InitializeComponent();
        }

        private void KhachHang_Load(object sender, EventArgs e)
        {
            try
            {
                loadDataToGridView();
                txtMakhachhang.ReadOnly = true;

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            cboTimkiem.Items.Add("Mã khách hàng");
            cboTimkiem.Items.Add("Tên khách hàng ");
            cboTimkiem.SelectedIndex = 0;
        }
        private void loadDataToGridView()
        {
            string sql = "SELECT * FROM KhachHang";
            DataTable dt = function.LoadDataToTable(sql);
            dataGridViewKhachhang.DataSource = dt;

            // Đặt tiêu đề và tự động co giãn cột cho vừa khung
            if (dataGridViewKhachhang.Columns.Count >= 4)
            {
                dataGridViewKhachhang.Columns[0].HeaderText = "Mã khách hàng";
                dataGridViewKhachhang.Columns[1].HeaderText = "Tên khách hàng";
                dataGridViewKhachhang.Columns[2].HeaderText = "Địa chỉ";
                dataGridViewKhachhang.Columns[3].HeaderText = "Điện thoại";
            }

            // Tự động co giãn cột cho vừa khung DataGridView
            dataGridViewKhachhang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridViewKhachhang.AllowUserToAddRows = false;
            dataGridViewKhachhang.EditMode = DataGridViewEditMode.EditProgrammatically;
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
            txtMakhachhang.ReadOnly = true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtMakhachhang.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMakhachhang.Focus();
                return;
            }
            if (txtTenkhachhang.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenkhachhang.Focus();
                return;
            }
            if (txtDiachi.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiachi.Focus();
                return;
            }
            if (mskDienthoai.Text == "(   )     -")
            {
                MessageBox.Show("Bạn phải nhập điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mskDienthoai.Focus();
                return;
            }
      

            string sql = "INSERT INTO KhachHang(MaKhach, TenKhach, DiaChi, DienThoai) " +
            "VALUES ('" + txtMakhachhang.Text + "', N'" + txtTenkhachhang.Text + "', N'" + txtDiachi.Text + "', '" + mskDienthoai.Text + "')";

            try
{
    // Sửa ở đây: mở kết nối nếu đang đóng
    if (function.conn.State == ConnectionState.Closed)
    {
        function.conn.Open();
    }

    SqlCommand sqlCommand = new SqlCommand(sql, function.conn);
    sqlCommand.ExecuteNonQuery();

    loadDataToGridView();
    btnSua.Enabled = true;
    btnXoa.Enabled = true;
    btnLuu.Enabled = false;
}
catch (Exception ex)
{
    MessageBox.Show("Lỗi khi lưu dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
}
finally
{
    // Tuỳ chọn: đóng kết nối nếu bạn không cần giữ kết nối lâu dài
    if (function.conn.State == ConnectionState.Open)
    {
        function.conn.Close();
    }
}
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

            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoqua.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            ResetValues();
            txtMakhachhang.Enabled = true;
            txtMakhachhang.Focus();
            txtMakhachhang.ReadOnly = false;
        }
        private void ResetValues()
        {
            txtMakhachhang.Text = "";
            txtTenkhachhang.Text = "";
            txtDiachi.Text = "";
            mskDienthoai.Text = "";
        }


        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMakhachhang.Text))
            {
                MessageBox.Show("Chọn khách hàng để xóa!", "Cảnh báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            var kq = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa khách hàng '{txtTenkhachhang.Text}'?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (kq != DialogResult.Yes)
                return;


            string sql = "DELETE FROM KhachHang WHERE MaKhach = @MaKhach";

            using (var cmd = new SqlCommand(sql, function.conn))
            {
                cmd.Parameters.AddWithValue("@MaKhach", txtMakhachhang.Text.Trim());

                try
                {

                    function.Connect();
                    int rows = cmd.ExecuteNonQuery(); // Thực thi lệnh xóa

                    // Kiểm tra nếu xóa thành công
                    if (rows > 0)
                    {
                        MessageBox.Show("Xóa thành công!", "Thông báo",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Cập nhật lại DataGridView sau khi xóa
                        string sql2 = "SELECT MaKhach, TenKhach,DiaChi, DienThoai FROM KhachHang";
                        using (var adapter = new SqlDataAdapter(sql2, function.conn))
                        {
                            var dt = new DataTable();
                            adapter.Fill(dt);
                            dataGridViewKhachhang.DataSource = dt;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy khách hàng để xóa.", "Lỗi",
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
            string ma = txtMakhachhang.Text.Trim();
            string ten = txtTenkhachhang.Text.Trim();
            string dienthoai = mskDienthoai.Text.Trim();
            string diachi = txtDiachi.Text.Trim();



            if (ten == "")
            {
                MessageBox.Show("Nhập tên khách hang !");
                return;
            }

            string sql = $"UPDATE KhachHang SET TenKhach = N'{ten}',DiaChi = N'{diachi}', DienThoai = '{dienthoai}' WHERE MaKhach= '{ma}'";

            SqlCommand cmd = new SqlCommand(sql, function.conn);
            try
            {
                function.Connect();
                cmd.ExecuteNonQuery();
                loadDataToGridView();
                MessageBox.Show("Cập nhật thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                function.conn.Close();
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
            DialogResult kq;
            kq = MessageBox.Show("Bạn có muốn thoát không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (kq == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            if (cboTimkiem.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn kiểu tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string searchType = cboTimkiem.SelectedItem.ToString();
            string searchText = txtTimkiem.Text.Trim();

            if (string.IsNullOrEmpty(searchText))
            {
                MessageBox.Show("Vui lòng nhập từ khóa tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = "";
            if (searchType == "Mã khách hàng")
            {
                query = "SELECT * FROM KhachHang WHERE MaKhach = @SearchText";
            }
            else if (searchType == "Tên khách hàng ")
            {
                query = "SELECT * FROM KhachHang WHERE TenKhach LIKE @SearchText";
                searchText = "%" + searchText + "%";
            }

            try
            {
                string sqlQuery = query.Replace("@SearchText", "'" + searchText + "'");
                DataTable dt = function.GetDataToTable(sqlQuery);

                if (dt != null && dt.Rows.Count > 0)
                {
                    dataGridViewKhachhang.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThoat_Click_1(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}


