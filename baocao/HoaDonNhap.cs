using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using COMExcel = Microsoft.Office.Interop.Excel;
using System.Xml.Linq;



namespace baocao
{
    public partial class HoaDonNhap : Form
    {
        DataTable tblCTHDN;
        public HoaDonNhap()
        {
            InitializeComponent();
            this.KeyPreview = true;

     
            txtGiamgia.KeyDown += new KeyEventHandler(txtGiamgia_KeyDown);
        }

        private void HoaDonNhap_Load(object sender, EventArgs e)
        {
            tblCTHDN = new DataTable();
            btnThem.Enabled = true;
            btnLuu.Visible = true;
            btnXoa.Visible = true;
            btnIn.Visible = true;
            txtSoHD.ReadOnly = false;
            txtSoHD.Enabled = true;
            txtTennhanvien.ReadOnly = true;
            txtTenNCC.ReadOnly = true;
            txtDiachi.ReadOnly = true;
            mskSodienthoai.ReadOnly = true;
            txtTenquanao.ReadOnly = true;
            txtDongianhap.ReadOnly = true;
            txtThanhtien.ReadOnly = true;
            txtTongtien.ReadOnly = true;
            txtGiamgia.Text = "0";
            txtTongtien.Text = "0";

            function.Connect();  // Mở kết nối CSDL
            LoadComboNhanVien();
            LoadComboNhaCungCap();
            LoadComboQuanAo();
            if (!string.IsNullOrEmpty(txtSoHD.Text))
            {
                Load_ThongtinHD();
                Load_DataGridViewHDN();
                btnXoa.Enabled = true;
                btnIn.Enabled = true;
            }



        }
        private void LoadComboNhanVien()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT MaNV, TenNV FROM NhanVien", function.conn);
            da.SelectCommand.CommandTimeout = 120;
            DataTable dt = new DataTable();
            da.Fill(dt);

            cboManhanvien.DataSource = dt;
            cboManhanvien.DisplayMember = "MaNV";   // ✅ Hiển thị Mã nhân viên
            cboManhanvien.ValueMember = "MaNV";     // ✅ Giá trị là Mã nhân viên

            cboManhanvien.SelectedIndex = -1;
        }

        private void LoadComboNhaCungCap()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT MaNCC, TenNCC FROM NhaCungCap", function.conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cboMaNCC.DataSource = dt;
            cboMaNCC.DisplayMember = "MaNCC";   // ✅ Hiển thị mã nhà cung cấp
            cboMaNCC.ValueMember = "MaNCC";     // ✅ Giá trị cũng là mã
            cboMaNCC.SelectedIndex = -1;
        }

        private void LoadComboQuanAo()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT MaQuanAo, TenQuanAo FROM SanPham", function.conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.SelectCommand.CommandTimeout = 120; // hoặc 180 nếu cần
            cboMaquanao.DataSource = dt;
            cboMaquanao.DisplayMember = "MaQuanAo";
            cboMaquanao.ValueMember = "MaQuanAo";
            cboMaquanao.SelectedIndex = -1;
        }

        private void Load_DataGridViewHDN()
        {
            try
            {
                string sql = "SELECT a.MaQuanAo, b.TenQuanAo, a.SoLuong, a.GiamGia, b.DonGiaNhap, a.ThanhTien " +
                             "FROM ChiTietHDNhap AS a, SanPham AS b " +
                             "WHERE a.SoHDN = N'" + txtSoHD.Text + "' AND a.MaQuanAo = b.MaQuanAo";

                tblCTHDN = function.GetDataToTable(sql);

                // Nếu không có dữ liệu, tạo DataTable rỗng với các cột cần thiết
                if (tblCTHDN == null || tblCTHDN.Rows.Count == 0)
                {
                    tblCTHDN = new DataTable();
                    tblCTHDN.Columns.Add("MaQuanAo", typeof(string));
                    tblCTHDN.Columns.Add("TenQuanAo", typeof(string));
                    tblCTHDN.Columns.Add("SoLuong", typeof(int));
                    tblCTHDN.Columns.Add("GiamGia", typeof(double));
                    tblCTHDN.Columns.Add("DonGiaNhap", typeof(double));
                    tblCTHDN.Columns.Add("ThanhTien", typeof(double));
                }

                dataGridViewHDN.DataSource = tblCTHDN;

                // Đặt tên cột hiển thị
                dataGridViewHDN.Columns["MaQuanAo"].HeaderText = "Mã quần áo";
                dataGridViewHDN.Columns["TenQuanAo"].HeaderText = "Tên quần áo";
                dataGridViewHDN.Columns["SoLuong"].HeaderText = "Số lượng";
                dataGridViewHDN.Columns["GiamGia"].HeaderText = "Giảm giá (%)";
                dataGridViewHDN.Columns["DonGiaNhap"].HeaderText = "Đơn giá nhập";
                dataGridViewHDN.Columns["ThanhTien"].HeaderText = "Thành tiền";


                dataGridViewHDN.AllowUserToAddRows = false;
                dataGridViewHDN.EditMode = DataGridViewEditMode.EditProgrammatically;
                dataGridViewHDN.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

      
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        
        }
        private void Load_ThongtinHD()
        {
            string str;
            str = "SELECT NgayNhap FROM HoaDonNhap WHERE SoHDN = N'" + txtSoHD.Text + "'";
            txtNgaynhap.Text = function.ConvertDateTime(function.GetFieldValues(str));
            str = "SELECT MaNV FROM HoaDonNhap WHERE SoHDN = N'" + txtSoHD.Text + "'";
            cboManhanvien.Text = function.GetFieldValues(str);
            str = "SELECT MaNCC FROM HoaDonNhap WHERE SoHDN = N'" + txtSoHD.Text + "'";
            cboMaNCC.Text = function.GetFieldValues(str);
            str = "SELECT TongTien FROM HoaDonNhap WHERE SoHDN = N'" + txtSoHD.Text + "'";
            txtTongtien.Text = function.GetFieldValues(str);
            string tongTienText = txtTongtien.Text.Replace(",", "").Trim();

            if (decimal.TryParse(tongTienText, out decimal tongTien))
            {
                lblbangchu.Text = "Bằng chữ: " + function.ChuyenSoSangChu(((long)tongTien).ToString());
            }
            else
            {
                lblbangchu.Text = "Bằng chữ: Không hợp lệ";
            }
        }


        private void btnThem_Click(object sender, EventArgs e)
        {
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;
            btnIn.Enabled = false;
            btnThem.Enabled = false;
            ResetValues();
            txtSoHD.Text = function.MaHoaDonNhapMoi();
            Load_DataGridViewHDN();

        }
        private void ResetValues()
        {
            txtSoHD.Text = "";
            txtNgaynhap.Text = DateTime.Now.ToShortDateString();
            cboManhanvien.Text = "";
            cboMaNCC.Text = "";
            txtTongtien.Text = "0";
            lblbangchu.Text = "Bằng chữ: ";
            cboMaquanao.Text = "";
            txtSoluong.Text = "";
            txtGiamgia.Text = "0";
            txtThanhtien.Text = "0";

            cboManhanvien.SelectedIndex = -1;
            txtTennhanvien.Text = "";

            cboMaNCC.SelectedIndex = -1;
            txtTenNCC.Text = "";
            mskSodienthoai.Text = "";
            txtDiachi.Text = "";

            cboMaquanao.SelectedIndex = -1;
            txtTenquanao.Text = "";
            txtSoluong.Text = "";
            txtGiamgia.Text = "0";
            txtDongianhap.Text = "0";
            txtThanhtien.Text = "0";
            txtNgaynhap.Text = DateTime.Today.ToString("dd/MM/yyyy");

        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            double slCu, slMoi, tong, tongMoi;

            // 1. Kiểm tra xem hóa đơn đã tồn tại chưa
            sql = "SELECT SoHDN FROM HoaDonNhap WHERE SoHDN = N'" + txtSoHD.Text + "'";
            if (!function.IsKeyExists(sql))
            {
                // Hóa đơn chưa có => thêm mới
                if (txtNgaynhap.Text.Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập ngày nhập", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNgaynhap.Focus();
                    return;
                }
                if (cboManhanvien.Text.Length == 0)
                {
                    MessageBox.Show("Bạn phải chọn nhân viên", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboManhanvien.Focus();
                    return;
                }
                if (cboMaNCC.Text.Length == 0)
                {
                    MessageBox.Show("Bạn phải chọn nhà cung cấp", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboMaNCC.Focus();
                    return;
                }

                sql = "INSERT INTO HoaDonNhap(SoHDN, NgayNhap, MaNV, MaNCC, TongTien) VALUES(N'" +
                    txtSoHD.Text.Trim() + "', '" +
                    function.ConvertDateTime(txtNgaynhap.Text.Trim()) + "', N'" +
                    cboManhanvien.SelectedValue + "', N'" +
                    cboMaNCC.SelectedValue + "', 0)";
                function.RunSQL(sql);
            }

            // 2. Kiểm tra thông tin mặt hàng
            if (cboMaquanao.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải chọn mã sản phẩm", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMaquanao.Focus();
                return;
            }

            if ((txtSoluong.Text.Trim().Length == 0) || (txtSoluong.Text == "0"))
            {
                MessageBox.Show("Bạn phải nhập số lượng", "Thông báo", MessageBoxButtons.OK,
MessageBoxIcon.Warning);
                txtSoluong.Text = "";
                txtSoluong.Focus();
                return;
            }
            if (txtGiamgia.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập giảm giá", "Thông báo", MessageBoxButtons.OK,
MessageBoxIcon.Warning);
                txtGiamgia.Focus();
                return;
            }


            sql = "SELECT * FROM ChiTietHDNhap WHERE SoHDN = N'" + txtSoHD.Text + "' AND MaQuanAo = N'" + cboMaquanao.SelectedValue + "'";
            if (function.IsKeyExists(sql))
            {
                DialogResult result = MessageBox.Show("Sản phẩm này đã có trong hóa đơn.\nBạn có muốn cập nhật số lượng không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Lấy số lượng cũ
                    sql = "SELECT SoLuong FROM ChiTietHDNhap WHERE SoHDN = N'" + txtSoHD.Text + "' AND MaQuanAo = N'" + cboMaquanao.SelectedValue + "'";
                    double soLuongCu = Convert.ToDouble(function.GetFieldValue(sql));

                    double soLuongMoi = soLuongCu + Convert.ToDouble(txtSoluong.Text);
                    double donGia = Convert.ToDouble(txtDongianhap.Text);
                    double giamGia = Convert.ToDouble(txtGiamgia.Text);
                    double thanhTienMoi = soLuongMoi * donGia * (100 - giamGia) / 100;

                    // Cập nhật lại số lượng và thành tiền trong bảng ChiTietHDNhap
                    sql = "UPDATE ChiTietHDNhap SET SoLuong = " + soLuongMoi +
                          ", ThanhTien = " + thanhTienMoi +
                          " WHERE SoHDN = N'" + txtSoHD.Text + "' AND MaQuanAo = N'" + cboMaquanao.SelectedValue + "'";
                    function.RunSQL(sql);

                    // Cập nhật lại tồn kho
                    sql = "SELECT SoLuong FROM SanPham WHERE MaQuanAo = N'" + cboMaquanao.SelectedValue + "'";
                     slCu = Convert.ToDouble(function.GetFieldValue(sql));
                     slMoi = slCu + Convert.ToDouble(txtSoluong.Text); // chỉ cộng phần mới thêm
                    sql = "UPDATE SanPham SET SoLuong = " + slMoi + " WHERE MaQuanAo = N'" + cboMaquanao.SelectedValue + "'";
                    function.RunSQL(sql);

                    // Cập nhật tổng tiền
                    sql = "SELECT TongTien FROM HoaDonNhap WHERE SoHDN = N'" + txtSoHD.Text + "'";
                     tong = Convert.ToDouble(function.GetFieldValue(sql));
                    double thanhTienThem = Convert.ToDouble(txtSoluong.Text) * donGia * (100 - giamGia) / 100;
                    tongMoi = tong + thanhTienThem;

                    sql = "UPDATE HoaDonNhap SET TongTien = " + tongMoi + " WHERE SoHDN = N'" + txtSoHD.Text + "'";
                    function.RunSQL(sql);

                    txtTongtien.Text = tongMoi.ToString("N0");
                    lblbangchu.Text = "Bằng chữ: " + function.ChuyenSoSangChu(((long)tongMoi).ToString());

                    Load_DataGridViewHDN();
                    ResetValuesHang();
                    btnXoa.Enabled = true;
                    btnThem.Enabled = true;
                    btnIn.Enabled = true;

                    return;
                }
                else
                {
                    // Không cập nhật, chỉ reset và thoát
                    ResetValuesHang();
                    cboMaquanao.Focus();
                    return;
                }
            }
            // 4. Lưu chi tiết hóa đơn nhập
            double SoLuong = Convert.ToDouble(txtSoluong.Text);
            double DonGiaNhap = Convert.ToDouble(txtDongianhap.Text);
            double GiamGia = Convert.ToDouble(txtGiamgia.Text);
            double ThanhTien = SoLuong * DonGiaNhap * (100 - GiamGia) / 100;

            sql = "INSERT INTO ChiTietHDNhap(SoHDN, MaQuanAo, SoLuong, DonGia, GiamGia, ThanhTien) VALUES(N'" +
                txtSoHD.Text.Trim() + "', N'" +
                cboMaquanao.SelectedValue + "', " +
                SoLuong + ", " + DonGiaNhap + ", " + GiamGia + ", " + ThanhTien + ")";
            function.RunSQL(sql);
            Load_DataGridViewHDN(); // Gọi lại để hiển thị danh sách sản phẩm mới

            // 5. Cập nhật tồn kho (tăng lên)
            sql = "SELECT SoLuong FROM SanPham WHERE MaQuanAo = N'" + cboMaquanao.SelectedValue + "'";
            slCu = Convert.ToDouble(function.GetFieldValue(sql));
            slMoi = slCu + SoLuong;
            sql = "UPDATE SanPham SET SoLuong = " + slMoi + " WHERE MaQuanAo = N'" + cboMaquanao.SelectedValue + "'";
            function.RunSQL(sql);

            // 6. Cập nhật tổng tiền của hóa đơn
            sql = "SELECT TongTien FROM HoaDonNhap WHERE SoHDN = N'" + txtSoHD.Text + "'";
            tong = Convert.ToDouble(function.GetFieldValue(sql));
            tongMoi = tong + ThanhTien;
            sql = "UPDATE HoaDonNhap SET TongTien = " + tongMoi + " WHERE SoHDN = N'" + txtSoHD.Text + "'";
            function.RunSQL(sql);

            txtTongtien.Text = tongMoi.ToString("N0");
            lblbangchu.Text = "Bằng chữ: " + function.ChuyenSoSangChu(((long)tongMoi).ToString());
            ResetValuesHang();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnIn.Enabled = true;
        }
        private void ResetValuesHang()
        {
            cboMaquanao.Text = "";
            txtSoluong.Text = "";
            txtGiamgia.Text = "0";
            txtThanhtien.Text = "0";
        }



        private void dataGridViewHDN_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string MaQuanAo;
            Double ThanhTien;
            if (tblCTHDN.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK,
MessageBoxIcon.Information);
                return;
            }
            if ((MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo",
MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
            {
                //Xóa hàng và cập nhật lại số lượng hàng 
                MaQuanAo = dataGridViewHDN.CurrentRow.Cells["MaQuanAo"].Value.ToString();
                DelHang(txtSoHD.Text, MaQuanAo);
                // Cập nhật lại tổng tiền cho hóa đơn bán
                ThanhTien = Convert.ToDouble(dataGridViewHDN.CurrentRow.
Cells["Thanhtien"].Value.ToString());
                DelUpdateTongTien(txtSoHD.Text, ThanhTien);
                Load_DataGridViewHDN();

            }
        }
        private void DelHang(string SoHDN, string MaQuanAo)
        {
            double s, sl, SLcon;
            string sql;

            // Sử dụng txtSoHD.Text để thay thế SoHDN nếu đây là nơi chứa giá trị mã hóa đơn
            SoHDN = txtSoHD.Text.Trim();

            // Chuỗi SQL đã sửa, nối chuỗi đúng cách
            sql = "SELECT SoLuong FROM ChiTietHDNhap WHERE SoHDN = N'" + SoHDN + "' AND MaQuanAo = N'" + MaQuanAo + "'";
            s = Convert.ToDouble(function.GetFieldValues(sql));

            // Câu lệnh xóa chi tiết hóa đơn nhập
            sql = "DELETE ChiTietHDNhap WHERE SoHDN = N'" + SoHDN + "' AND MaQuanAo = N'" + MaQuanAo + "'";
            function.RunSQL(sql);

            // Cập nhật lại số lượng cho các mặt hàng
            sql = "SELECT SoLuong FROM SanPham WHERE MaQuanAo = N'" + MaQuanAo + "'";
            sl = Convert.ToDouble(function.GetFieldValues(sql));

            SLcon = sl + s;

            // Cập nhật số lượng trong bảng tblHang
            sql = "UPDATE SanPham SET SoLuong = " + SLcon + " WHERE MaQuanAo = N'" + MaQuanAo + "'";
            function.RunSQL(sql);  // Thực thi câu lệnh SQL
        }
        private void DelUpdateTongTien(string SoHDN, double ThanhTien)
        {
            Double Tong, Tongmoi;
            string sql;
            sql = "SELECT TongTien FROM HoaDonNhap WHERE SoHDN = N'" + SoHDN + "'";
            Tong = Convert.ToDouble(function.GetFieldValues(sql));
            Tongmoi = Tong - ThanhTien;
            sql = "UPDATE HoaDonNhap SET TongTien =" + Tongmoi + " WHERE SoHDN = N'" +
SoHDN + "'";
            function.RunSQL(sql);
            txtTongtien.Text = Tongmoi.ToString();
            lblbangchu.Text = "Bằng chữ: " + function.ChuyenSoSangChu(Tongmoi.ToString());
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo",
           MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    if (function.conn.State == ConnectionState.Closed)
                        function.conn.Open();

                    string[] MaQuanAo = new string[20];
                    int n = 0;

                    string sql = "SELECT MaQuanAo FROM ChiTietHDNhap WHERE SoHDN = @SoHDN";
                    SqlCommand cmd = new SqlCommand(sql, function.conn);
                    cmd.Parameters.AddWithValue("@SoHDN", txtSoHD.Text.Trim());

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        MaQuanAo[n] = reader.GetString(0);
                        n++;
                    }
                    reader.Close();

                    // Xóa chi tiết từng mặt hàng của hóa đơn
                    for (int i = 0; i < n; i++)
                    {
                        DelHang(txtSoHD.Text.Trim(), MaQuanAo[i]);
                    }

                    // Xóa hóa đơn
                    sql = "DELETE FROM HoaDonNhap WHERE SoHDN = @SoHDN";
                    SqlCommand cmdDelete = new SqlCommand(sql, function.conn);
                    cmdDelete.Parameters.AddWithValue("@SoHDN", txtSoHD.Text.Trim());
                    cmdDelete.ExecuteNonQuery();

                    ResetValues();
                    Load_DataGridViewHDN();
                    btnXoa.Enabled = false;
                    btnIn.Enabled = false;

                    MessageBox.Show("Đã xóa hóa đơn và các mặt hàng liên quan.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa hóa đơn: " + ex.Message);
                }
                finally
                {
                    if (function.conn.State == ConnectionState.Open)
                        function.conn.Close();
                }
            }
        }
        private void cboManhanvien_TextChanged(object sender, EventArgs e)
        {


        }

        private void cboMaNCC_TextChanged(object sender, EventArgs e)
        {

        }

        private void cboMaquanao_TextChanged(object sender, EventArgs e)
        {


        }
        private void txtSoluong_TextChanged(object sender, EventArgs e)
        {
            CalculateThanhTien();
        }
        private void txtGiamgia_TextChanged(object sender, EventArgs e)
        {
            CalculateThanhTien();

        }
        private void txtDongianhap_TextChanged(object sender, EventArgs e)
        {
            CalculateThanhTien();

        }

        private void ThemSanPhamVaoDataGridView()
        {
            // Lấy dữ liệu từ các TextBox và ComboBox
            try
            {
                // Tạo bảng nếu chưa có hoặc chưa có cột
                if (tblCTHDN == null || tblCTHDN.Columns.Count == 0)
                {
                    tblCTHDN = new DataTable();
                    tblCTHDN.Columns.Add("MaQuanAo", typeof(string));
                    tblCTHDN.Columns.Add("TenQuanAo", typeof(string));
                    tblCTHDN.Columns.Add("SoLuong", typeof(int));
                    tblCTHDN.Columns.Add("GiamGia", typeof(double));
                    tblCTHDN.Columns.Add("DonGiaNhap", typeof(double));
                    tblCTHDN.Columns.Add("ThanhTien", typeof(double));
                }

                // Lấy dữ liệu từ các control nhập liệu
                string MaQuanAo = cboMaquanao.SelectedValue.ToString();
                string TenQuanAo = txtTenquanao.Text;
                int SoLuong = int.Parse(txtSoluong.Text);
                double GiamGia = double.Parse(txtGiamgia.Text);
                double DonGiaNhap = double.Parse(txtDongianhap.Text);

                // Tính thành tiền
                double ThanhTien = SoLuong * DonGiaNhap * (1 - GiamGia / 100.0);

                // Tạo dòng mới và gán dữ liệu
                DataRow row = tblCTHDN.NewRow();
                row["MaQuanAo"] = MaQuanAo;
                row["TenQuanAo"] = TenQuanAo;
                row["SoLuong"] = SoLuong;
                row["GiamGia"] = GiamGia;
                row["DonGiaNhap"] = DonGiaNhap;
                row["ThanhTien"] = ThanhTien;

                // Thêm dòng vào bảng
                tblCTHDN.Rows.Add(row);

                // Cập nhật tổng tiền nếu cần
                CapNhatTongTien();

                // Xoá dữ liệu sau khi thêm
                XoaOThongTinNhap();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }

        }
        private void XoaOThongTinNhap()
        {
            cboMaquanao.SelectedIndex = -1;
            txtTenquanao.Clear();
            txtSoluong.Clear();
            txtDongianhap.Clear();
            txtGiamgia.Clear();
        }
        private void CapNhatTongTien()
        {
            double TongTien = 0;

            // Duyệt qua từng dòng trong DataGridView để tính tổng tiền
            foreach (DataGridViewRow row in dataGridViewHDN.Rows)
            {
                if (row.Cells["ThanhTien"] != null && row.Cells["ThanhTien"].Value != null)
                {
                    // Cộng thêm giá trị thanh tiền của mỗi sản phẩm vào tổng tiền
                    TongTien += Convert.ToDouble(row.Cells["ThanhTien"].Value);
                }
            }

            // Hiển thị tổng tiền lên Label (hoặc TextBox)
            lblTongtien.Text = TongTien.ToString("C"); // Định dạng theo kiểu tiền tệ
        }

        private void txtGiamgia_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ThemSanPhamVaoDataGridView();  // Gọi hàm thêm sản phẩm vào DataGridView
            }
        }




        private void CalculateThanhTien()
        {
            double sl, dg, gg, tt;
            if (string.IsNullOrWhiteSpace(txtSoluong.Text) ||
                string.IsNullOrWhiteSpace(txtDongianhap.Text) ||
                string.IsNullOrWhiteSpace(txtGiamgia.Text))
            {
                txtThanhtien.Text = "";
                return;
            }

            if (double.TryParse(txtSoluong.Text, out sl) &&
                double.TryParse(txtDongianhap.Text, out dg) &&
                double.TryParse(txtGiamgia.Text, out gg))
            {
                if (gg < 0) gg = 0;
                if (gg > 100) gg = 100;

                tt = sl * dg * (100 - gg) / 100;
                txtThanhtien.Text = tt.ToString("N0");
            }
            else
            {
                txtThanhtien.Text = "";
            }
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            COMExcel.Application exApp = new COMExcel.Application();
            COMExcel.Workbook exBook; //Trong 1 chương trình Excel có nhiều Workbook
            COMExcel.Worksheet exSheet; //Trong 1 Workbook có nhiều Worksheet
            COMExcel.Range exRange;
            string sql;
            int hang = 0, cot = 0;
            DataTable tblThongtinHD, tblThongtinHang;
            exBook = exApp.Workbooks.Add(COMExcel.XlWBATemplate.xlWBATWorksheet);
            exSheet = (COMExcel.Worksheet)exBook.Worksheets[1];
            // Định dạng chung
            exRange = (COMExcel.Range)exSheet.Cells[1, 1];
            exRange.Range["A1:B3"].Font.Size = 10;
            exRange.Range["A1:B3"].Font.Name = "Times new roman";
            exRange.Range["A1:B3"].Font.Bold = true;
            exRange.Range["A1:B3"].Font.ColorIndex = 5; //Màu xanh da trời
            exRange.Range["A1:A1"].ColumnWidth = 7;
            exRange.Range["B1:B1"].ColumnWidth = 15;
            exRange.Range["A1:B1"].MergeCells = true;
            exRange.Range["A1:B1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A1:B1"].Value = "Công ty A";
            exRange.Range["A2:B2"].MergeCells = true;
            exRange.Range["A2:B2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A2:B2"].Value = "Hà Nội";
            exRange.Range["A3:B3"].MergeCells = true;
            exRange.Range["A3:B3"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A3:B3"].Value = "Điện thoại: 0123456789";
            exRange.Range["C2:E2"].Font.Size = 16;
            exRange.Range["C2:E2"].Font.Name = "Times new roman";
            exRange.Range["C2:E2"].Font.Bold = true;
            exRange.Range["C2:E2"].Font.ColorIndex = 3; //Màu đỏ
            exRange.Range["C2:E2"].MergeCells = true;
            exRange.Range["C2:E2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["C2:E2"].Value = "HÓA ĐƠN NHẬP";
            // Biểu diễn thông tin chung của hóa đơn bána
            sql = "SELECT a.SoHDN, a.NgayNhap, a.TongTien, b.TenNCC, b.DiaChi, b.DienThoai, c.TenNV " +
                    "FROM HoaDonNhap AS a " +
                    "JOIN NhaCungCap AS b ON a.MaNCC = b.MaNCC " +
                    "JOIN NhanVien AS c ON a.MaNV = c.MaNV " +
                    "WHERE a.SoHDN = N'" + txtSoHD.Text + "'";
            tblThongtinHD = function.GetDataToTable(sql);
            exRange.Range["B6:C9"].Font.Size = 12;
            exRange.Range["B6:C9"].Font.Name = "Times new roman";
            exRange.Range["B6:B6"].Value = "Mã hóa đơn:";
            exRange.Range["C6:E6"].MergeCells = true;
            if (tblThongtinHD != null && tblThongtinHD.Rows.Count > 0)
            {
                exRange.Range["C6:E6"].Value = tblThongtinHD.Rows[0][0].ToString();
            }
            else
            {
                MessageBox.Show("Không có thông tin hóa đơn để in!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // hoặc xử lý tiếp theo tùy logic
            }

            exRange.Range["B7:B7"].Value = "Khách hàng:";
            exRange.Range["C7:E7"].MergeCells = true;
            exRange.Range["C7:E7"].Value = tblThongtinHD.Rows[0][3].ToString();
            exRange.Range["B8:B8"].Value = "Địa chỉ:";
            exRange.Range["C8:E8"].MergeCells = true;
            exRange.Range["C8:E8"].Value = tblThongtinHD.Rows[0][4].ToString();
            exRange.Range["B9:B9"].Value = "Điện thoại:";
            exRange.Range["C9:E9"].MergeCells = true;
            exRange.Range["C9:E9"].Value = tblThongtinHD.Rows[0][5].ToString();
            //Lấy thông tin các mặt hàng
            sql = "SELECT b.TenQuanAo, a.SoLuong, b.DonGiaNhap, a.GiamGia, a.ThanhTien " +
               "FROM ChiTietHDNhap AS a JOIN SanPham AS b ON a.MaQuanAo = b.MaQuanAo " +
               "WHERE a.SoHDN = N'" + txtSoHD.Text + "'";
            tblThongtinHang = function.GetDataToTable(sql);

            //Tạo dòng tiêu đề bảng
            exRange.Range["A11:F11"].Font.Bold = true;
            exRange.Range["A11:F11"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["C11:F11"].ColumnWidth = 12;
            exRange.Range["A11:A11"].Value = "STT";
            exRange.Range["B11:B11"].Value = "Tên hàng";
            exRange.Range["C11:C11"].Value = "Số lượng";
            exRange.Range["D11:D11"].Value = "Đơn giá";
            exRange.Range["E11:E11"].Value = "Giảm giá";
            exRange.Range["F11:F11"].Value = "Thành tiền";
            for (hang = 0; hang <= tblThongtinHang.Rows.Count - 1; hang++)
            {
                //Điền số thứ tự vào cột 1 từ dòng 12
                exSheet.Cells[hang + 12, 1] = hang + 1;
                for (cot = 0; cot < tblThongtinHang.Columns.Count; cot++)
                {
                    exSheet.Cells[hang + 12, cot + 2] = tblThongtinHang.Rows[hang][cot].ToString();
                }
            }
            COMExcel.Range exRangeTotal = (COMExcel.Range)exSheet.Cells[hang + 14, tblThongtinHang.Columns.Count];
            exRange.Font.Bold = true;
            exRange.Value2 = "Tổng tiền:";
            exRange = (COMExcel.Range)exSheet.Cells[hang + 14, tblThongtinHang.Columns.Count + 1];
            exRange.Font.Bold = true;
            exRange.Value2 = tblThongtinHD.Rows[0][2].ToString();

            // Bằng chữ
            exRange = (COMExcel.Range)exSheet.Cells[hang + 15, 1];
            exRange.MergeCells = true;  // Merge cells here
            exRange.Font.Bold = true;
            exRange.Font.Italic = true;
            exRange.HorizontalAlignment = COMExcel.XlHAlign.xlHAlignRight;
            exRange.Value = "Bằng chữ: " + function.ChuyenSoSangChu(tblThongtinHD.Rows[0][2].ToString());
            exRange.Range["A1:F1"].MergeCells = true;
            exRange.Range["A1:F1"].Font.Bold = true;
            exRange.Range["A1:F1"].Font.Italic = true;
            exRange.Range["A1:F1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignRight;
            exRange.Range["A1:F1"].Value = "Bằng chữ: " + function.ChuyenSoSangChu
 (tblThongtinHD.Rows[0][2].ToString());
            exRange = (COMExcel.Range)exSheet.Cells[hang + 17, 4];
            exRange.Range["A1:C1"].MergeCells = true;
            exRange.Range["A1:C1"].Font.Italic = true;
            exRange.Range["A1:C1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            DateTime d = Convert.ToDateTime(tblThongtinHD.Rows[0][1]);
            exRange.Range["A1:C1"].Value = "Hà Nội, ngày " + d.Day + " tháng " + d.Month + " năm " + d.Year;
            exRange.Range["A2:C2"].MergeCells = true;
            exRange.Range["A2:C2"].Font.Italic = true;
            exRange.Range["A2:C2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A2:C2"].Value = "Nhân viên bán hàng";
            exRange.Range["A6:C6"].MergeCells = true;
            exRange.Range["A6:C6"].Font.Italic = true;
            exRange.Range["A6:C6"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A6:C6"].Value = tblThongtinHD.Rows[0][6];
            exSheet.Name = "Hóa đơn nhập";
            exApp.Visible = true;
            System.Runtime.InteropServices.Marshal.ReleaseComObject(exSheet);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(exBook);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(exApp);
            exSheet = null;
            exBook = null;
            exApp = null;
            GC.Collect(); // Dọn rác


        }

        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            if (cboSoHD.Text == "")
            {
                MessageBox.Show("Bạn phải chọn một mã hóa đơn để tìm", "Thông báo",
MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboSoHD.Focus();
                return;
            }
            txtSoHD.Text = cboSoHD.Text;
            Load_ThongtinHD();
            Load_DataGridViewHDN();
            btnXoa.Enabled = true;
            btnLuu.Enabled = true;
            btnIn.Enabled = true;
            cboSoHD.SelectedIndex = -1;

        }

        private void cboSoHD_DropDown(object sender, EventArgs e)
        {
            string sql = "SELECT SoHDN FROM HoaDonNhap";
            DataTable dt = function.GetDataToTable(sql);

            if (dt != null && dt.Rows.Count > 0)
            {
                cboSoHD.DataSource = dt;
                cboSoHD.DisplayMember = "SoHDN";  // Cột hiển thị trong ComboBox
                cboSoHD.ValueMember = "SoHDN";    // Cột chứa giá trị của item
            }
            else
            {
                MessageBox.Show("Không có hóa đơn nào.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void HoaDonNhap_FormClosing(object sender, FormClosingEventArgs e)
        {
            ResetValues();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {

            this.Close();
        }

        private void cboManhanvien_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void cboMaNCC_SelectedValueChanged(object sender, EventArgs e)
        {

        }


        private void cboMaquanao_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void cboManhanvien_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboManhanvien.SelectedIndex != -1 && cboManhanvien.SelectedValue != null)
            {
                string maNV = cboManhanvien.SelectedValue.ToString();
                string sql = "SELECT TenNV FROM NhanVien WHERE MaNV = @MaNV";
                SqlCommand cmd = new SqlCommand(sql, function.conn);
                cmd.Parameters.AddWithValue("@MaNV", maNV);

                try
                {
                    if (function.conn.State == ConnectionState.Closed)
                        function.conn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        txtTennhanvien.Text = reader["TenNV"].ToString();
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi lấy tên nhân viên: " + ex.Message);
                }
                finally
                {
                    if (function.conn.State == ConnectionState.Open)
                        function.conn.Close();
                }
            }
        }

        private void cboMaquanao_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMaquanao.SelectedIndex != -1 && cboMaquanao.SelectedValue != null)
            {
                string maQA = cboMaquanao.SelectedValue.ToString();
                string sql = "SELECT TenQuanAo, DonGiaNhap FROM SanPham WHERE MaQuanAo = @MaQuanAo";

                if (function.conn.State == ConnectionState.Closed)
                {
                    function.conn.Open();
                }

                using (SqlCommand cmd = new SqlCommand(sql, function.conn))
                {
                    cmd.Parameters.AddWithValue("@MaQuanAo", maQA);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtTenquanao.Text = reader["TenQuanAo"].ToString();
                            txtDongianhap.Text = reader["DonGiaNhap"].ToString();
                        }
                    }
                }
                // Luôn gọi hàm tính thành tiền sau khi cập nhật đơn giá
                CalculateThanhTien();
            }
        }

        private void cboMaNCC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMaNCC.SelectedIndex != -1 && cboMaNCC.SelectedValue != null)
            {
                string maNCC = cboMaNCC.SelectedValue.ToString();
                string sql = "SELECT TenNCC, DiaChi, DienThoai FROM NhaCungCap WHERE MaNCC = @MaNCC";

                try
                {
                    if (function.conn.State != ConnectionState.Open)
                        function.conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, function.conn);
                    cmd.Parameters.AddWithValue("@MaNCC", maNCC);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        txtTenNCC.Text = reader["TenNCC"].ToString();
                        txtDiachi.Text = reader["DiaChi"].ToString();
                        mskSodienthoai.Text = reader["DienThoai"].ToString();
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi lấy thông tin nhà cung cấp: " + ex.Message);
                }
                finally
                {
                    if (function.conn.State == ConnectionState.Open)
                        function.conn.Close();
                }
            }
        }
        // Đặt phương thức này trong cùng class mà bạn muốn sử dụng
        private string GenerateSoHDN()
        {
            // Tiền tố của mã hóa đơn
            string prefix = "HDN";

            // Lấy ngày hiện tại theo định dạng yyyyMMdd
            string datePart = DateTime.Now.ToString("yyyyMMdd");

            // Lấy số tự động (số hóa đơn trong ngày), ví dụ lấy số tiếp theo từ cơ sở dữ liệu
            int nextOrderNumber = GetNextOrderNumber(datePart);

            // Kết hợp thành mã hóa đơn: "HDN" + "yyyyMMdd" + số tự động
            string soHDN = prefix + datePart + nextOrderNumber.ToString("D4"); // Đảm bảo số tự động có 4 chữ số

            return soHDN;
        }

        private int GetNextOrderNumber(string datePart)
        {
            int nextOrderNumber = 1;

            string sql = "SELECT COUNT(*) FROM HoaDonNhap WHERE SoHDN LIKE @DatePart";

            // Đảm bảo kết nối được mở
            if (function.conn == null || function.conn.State == ConnectionState.Closed)
            {
                function.Connect();
            }

            using (SqlCommand cmd = new SqlCommand(sql, function.conn))
            {
                cmd.Parameters.AddWithValue("@DatePart", datePart + "%");

                try
                {
                    object result = cmd.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int count))
                    {
                        nextOrderNumber = count + 1;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi lấy số tự động: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return nextOrderNumber;
        }
        private void EnableButtons(bool enabled)
        {
            btnThem.Enabled = enabled;
            btnXoa.Enabled = enabled;
            btnIn.Enabled = enabled;
        }

        private void btnBoqua_Click(object sender, EventArgs e)
        {
            try
            {
                ResetValues();                  

                btnBoqua.Enabled = false;  
                btnLuu.Enabled = false;     
                btnXoa.Enabled = true;      
                btnThem.Enabled = true;     
                btnIn.Enabled = true;       

                txtSoHD.Enabled = false;
                Load_DataGridViewHDN();        
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thực hiện bỏ qua: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        

        private void dataGridViewHDN_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void HoaDonNhap_Layout(object sender, LayoutEventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}

       