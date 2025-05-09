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
            txtMaNhanvien.ReadOnly = true;
        }

        private void NhanVien_Load(object sender, EventArgs e)
        {
            try
            {
                mskngaysinh.Mask = "00/00/0000"; 
                mskngaysinh.ValidatingType = typeof(DateTime);
                loadDataToGridView();
                LoadCongViecToComboBox();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            cboTimkiem.Items.Add("Mã nhân viên");
            cboTimkiem.Items.Add("Tên nhân viên");
            cboTimkiem.SelectedIndex = 0;
        }
        private void LoadCongViecToComboBox()
        {
            string sql = "SELECT MaCV FROM CongViec";
            DataTable dt = function.LoadDataToTable(sql);
            comboMaCV.DataSource = dt;
            comboMaCV.DisplayMember = "MaCV";
            comboMaCV.ValueMember = "MaCV";
            comboMaCV.SelectedIndex = -1;
        }
        private void loadDataToGridView()
        {
            string sql = "SELECT * FROM NhanVien";
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
                dataGridView.Columns[6].HeaderText = "Mã công việc";
            }

            dataGridView.AllowUserToAddRows = false;
        }
        private void clear()
        {
            txtMaNhanvien.Enabled = true;
            txtMaNhanvien.Text = "";
            txtTennhanvien.Text = "";
            radionam.Checked = false;
            radionu.Checked = false;
            txtDiachi.Text = "";
            mskdienthoai.Text = "";
            mskngaysinh.Text = "";
            comboMaCV.SelectedIndex = -1;
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView.Rows.Count == 0) return;

            txtMaNhanvien.Text = dataGridView.CurrentRow.Cells[0].Value.ToString();
            txtTennhanvien.Text = dataGridView.CurrentRow.Cells[1].Value.ToString();
            string gioiTinh = dataGridView.CurrentRow.Cells[2].Value.ToString();
            radionam.Checked = gioiTinh == "Nam";
            radionu.Checked = gioiTinh == "Nữ";
            DateTime ngaySinh;
            if (DateTime.TryParse(dataGridView.CurrentRow.Cells[3].Value.ToString(), out ngaySinh))
            {
                mskngaysinh.Text = ngaySinh.ToString("dd/MM/yyyy");
            }
            else
            {
                mskngaysinh.Text = "";
            }
            mskdienthoai.Text = dataGridView.CurrentRow.Cells[4].Value.ToString();
            txtDiachi.Text = dataGridView.CurrentRow.Cells[5].Value.ToString();
            comboMaCV.SelectedValue = dataGridView.CurrentRow.Cells[6].Value.ToString();
            txtMaNhanvien.ReadOnly = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoqua.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            ResetValues();
            txtMaNhanvien.Enabled = true;
            txtMaNhanvien.Focus();
      


        }

        private void ResetValues()
        {
            txtMaNhanvien.Text = "";
            txtTennhanvien.Text = "";
            radionam.Checked = false;   
            radionu.Checked = false;    
            txtDiachi.Text = "";
            mskngaysinh.Text = "";
            mskdienthoai.Text = "";
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtMaNhanvien.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaNhanvien.Focus();
                return;
            }
            if (txtTennhanvien.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTennhanvien.Focus();
                return;
            }
            if (txtDiachi.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiachi.Focus();
                return;
            }
            if (mskdienthoai.Text == "(   )     -")
            {
                MessageBox.Show("Bạn phải nhập điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mskdienthoai.Focus();
                return;
            }
             string sql = "SELECT MaNV FROM NhanVien WHERE MaNV=N'" + txtMaNhanvien.Text.Trim() + "'";
            if (function.CheckKey(sql))
            {
                MessageBox.Show("Mã nhân viên này đã có, bạn phải nhập mã khác", "Thông báo",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaNhanvien.Focus();
                txtMaNhanvien.Text = "";
                return;
            }
             sql = "INSERT INTO NhanVien (MaNV, TenNV, GioiTinh, NgaySinh, DienThoai, DiaChi, MaCV) " +
                "VALUES (N'" + txtMaNhanvien.Text.Trim() + "', N'" + txtTennhanvien.Text.Trim() + "', N'" +
                (radionam.Checked ? "Nam" : "Nữ") + "', '" + function.getSQLdateFromText(mskngaysinh.Text) + "', '" +
                mskdienthoai.Text.Trim() + "', N'" + txtDiachi.Text.Trim() + "', '" + comboMaCV.SelectedValue.ToString() + "')";
            function.RunSQL(sql);
            loadDataToGridView();
            ResetValues();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnBoqua.Enabled = false;
            btnLuu.Enabled = false;
            txtMaNhanvien.Enabled = false;

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaNhanvien.Text))
            {
                MessageBox.Show("Chọn nhân viên để xóa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show($"Bạn có chắc chắn muốn xóa nhân viên '{txtTennhanvien.Text}'?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            string sql = "DELETE FROM NhanVien WHERE MaNV = @MaNV";
            using (SqlCommand cmd = new SqlCommand(sql, function.conn))
            {
                cmd.Parameters.AddWithValue("@MaNV", txtMaNhanvien.Text.Trim());

                try
                {
                    function.Connect();
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show("Xóa thành công!", "Thông báo");
                        LoadData();
                    }
                    else
                        MessageBox.Show("Không tìm thấy nhân viên để xóa.", "Lỗi");
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Lỗi khi xóa: " + ex.Message, "Lỗi");
                }
                finally
                {
                    function.Close();
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string ma = txtMaNhanvien.Text.Trim();
            string ten = txtTennhanvien.Text.Trim();
            string dienthoai = mskdienthoai.Text.Trim();
            string ngaysinh = mskngaysinh.Text.Trim();
            string diachi = txtDiachi.Text.Trim();
            string MaCV = comboMaCV.SelectedValue.ToString();
            string gioiTinh = radionam.Checked ? "Nam" : "Nữ";


            if (ten == "")
            {
                MessageBox.Show("Nhập tên nhân viên !");
                return;
            }
          
            string sql = $"UPDATE NhanVien SET TenNV = N'{ten}', GioiTinh = N'{gioiTinh}', NgaySinh = '{ngaysinh}', DienThoai = '{dienthoai}', DiaChi = N'{diachi}', MaCV = '{MaCV}' WHERE MaNV = '{ma}'";

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
            string sql = "SELECT * FROM NhanVien";
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
            txtMaNhanvien.Enabled = false;
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
        private bool CheckData()
        {
            if (string.IsNullOrWhiteSpace(txtMaNhanvien.Text))
            {
                MessageBox.Show("Bạn chưa nhập mã nhân viên!");
                txtMaNhanvien.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtTennhanvien.Text))
            {
                MessageBox.Show("Bạn chưa nhập tên nhân viên!");
                txtTennhanvien.Focus();
                return false;
            }
            if (!radionam.Checked && !radionu.Checked)
            {
                MessageBox.Show("Bạn chưa chọn giới tính!");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtDiachi.Text))
            {
                MessageBox.Show("Bạn chưa nhập địa chỉ!");
                txtDiachi.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(mskdienthoai.Text) || mskdienthoai.Text.Length < 10)
            {
                MessageBox.Show("Số điện thoại không hợp lệ!");
                mskdienthoai.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(mskngaysinh.Text) || mskngaysinh.Text.Length != 10)
            {
                MessageBox.Show("Ngày sinh không hợp lệ (dd/mm/yyyy)!");
                mskngaysinh.Focus();
                return false;
            }
            if (comboMaCV.SelectedIndex == -1)
            {
                MessageBox.Show("Bạn chưa chọn mã chức vụ!");
                comboMaCV.Focus();
                return false;
            }

            return true;
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
            if (searchType == "Mã nhân viên")
            {
                query = "SELECT * FROM NhanVien WHERE MaNV = @SearchText";
            }
            else if (searchType == "Tên nhân viên")
            {
                query = "SELECT * FROM NhanVien WHERE TenNV LIKE @SearchText";
                searchText = "%" + searchText + "%"; 
            }

            try
            {
                string sqlQuery = query.Replace("@SearchText", "'" + searchText + "'");
                DataTable dt = function.GetDataToTable(sqlQuery);

                if (dt != null && dt.Rows.Count > 0)
                {
                    dataGridView.DataSource = dt;
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
    }
}
