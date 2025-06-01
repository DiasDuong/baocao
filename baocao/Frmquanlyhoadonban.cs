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
using COMExcel = Microsoft.Office.Interop.Excel;

namespace baocao
{
    public partial class Frmquanlyhoadonban : Form
    {
        DataTable ChiTietHDBan;
        public Frmquanlyhoadonban()
        {
            InitializeComponent();
        }

        private string FormatDate(string date)
        {
            try
            {
                // Chuyển đổi từ dd/MM/yyyy sang yyyy-MM-dd (định dạng SQL Server)
                DateTime dt = DateTime.ParseExact(date.Trim(), "M/d/yyyy", null);
                return dt.ToString("yyyy-MM-dd");
            }
            catch
            {
                MessageBox.Show("Ngày không hợp lệ. Vui lòng nhập theo định dạng MM/dd/yyyy", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        private bool KiemTraMatHangTonTai(string maHoaDon, string maQuanAo)
        {
            try
            {
                string sql = "SELECT SoLuong FROM ChiTietHDBan WHERE SoHDB=@SoHDB AND MaQuanAo=@MaQuanAo";
                using (SqlConnection tempConn = new SqlConnection(function.ConnectionString))
                {
                    tempConn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, tempConn))
                    {
                        cmd.Parameters.AddWithValue("@SoHDB", maHoaDon);
                        cmd.Parameters.AddWithValue("@MaQuanAo", maQuanAo);
                        object result = cmd.ExecuteScalar();
                        return (result != null && result != DBNull.Value);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kiểm tra mặt hàng: " + ex.Message);
                return false;
            }
        }

        private void CapNhatSoLuongTonTai(string maHoaDon, string maQuanAo, decimal soLuongThem)
        {
            try
            {
                // Lấy số lượng hiện tại trong chi tiết hóa đơn
                decimal soLuongHienTai = 0;
                string sql = "SELECT SoLuong FROM ChiTietHDBan WHERE SoHDB=@SoHDB AND MaQuanAo=@MaQuanAo";
                using (SqlConnection tempConn = new SqlConnection(function.ConnectionString))
                {
                    tempConn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, tempConn))
                    {
                        cmd.Parameters.AddWithValue("@SoHDB", maHoaDon);
                        cmd.Parameters.AddWithValue("@MaQuanAo", maQuanAo);
                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            soLuongHienTai = Convert.ToDecimal(result);
                        }
                    }
                }

                // Lấy số lượng trong kho
                decimal soLuongTrongKho = 0;
                sql = "SELECT SoLuong FROM SanPham WHERE MaQuanAo=@MaQuanAo";
                using (SqlConnection tempConn = new SqlConnection(function.ConnectionString))
                {
                    tempConn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, tempConn))
                    {
                        cmd.Parameters.AddWithValue("@MaQuanAo", maQuanAo);
                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            soLuongTrongKho = Convert.ToDecimal(result);
                        }
                    }
                }

                // Kiểm tra nếu đủ số lượng để thêm
                if (soLuongThem > soLuongTrongKho)
                {
                    MessageBox.Show($"Số lượng mặt hàng này chỉ còn {soLuongTrongKho}", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Cập nhật số lượng trong chi tiết hóa đơn
                decimal soLuongMoi = soLuongHienTai + soLuongThem;
                decimal thanhTien = soLuongThem * Convert.ToDecimal(txtDongiaban.Text);
                decimal giamGia = Convert.ToDecimal(txtGiamgia.Text);
                thanhTien = thanhTien - (thanhTien * giamGia / 100);

                sql = "UPDATE ChiTietHDBan SET SoLuong=@SoLuong, ThanhTien=ThanhTien+@ThanhTien WHERE SoHDB=@SoHDB AND MaQuanAo=@MaQuanAo";
                using (SqlConnection tempConn = new SqlConnection(function.ConnectionString))
                {
                    tempConn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, tempConn))
                    {
                        cmd.Parameters.AddWithValue("@SoLuong", soLuongMoi);
                        cmd.Parameters.AddWithValue("@ThanhTien", thanhTien);
                        cmd.Parameters.AddWithValue("@SoHDB", maHoaDon);
                        cmd.Parameters.AddWithValue("@MaQuanAo", maQuanAo);
                        cmd.ExecuteNonQuery();
                    }
                }

                // Cập nhật số lượng trong kho
                sql = "UPDATE SanPham SET SoLuong=SoLuong-@SoLuong WHERE MaQuanAo=@MaQuanAo";
                using (SqlConnection tempConn = new SqlConnection(function.ConnectionString))
                {
                    tempConn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, tempConn))
                    {
                        cmd.Parameters.AddWithValue("@SoLuong", soLuongThem);
                        cmd.Parameters.AddWithValue("@MaQuanAo", maQuanAo);
                        cmd.ExecuteNonQuery();
                    }
                }

                // Cập nhật tổng tiền trong hóa đơn
                sql = "UPDATE HoaDonBan SET TongTien=TongTien+@ThanhTien WHERE SoHDB=@SoHDB";
                using (SqlConnection tempConn = new SqlConnection(function.ConnectionString))
                {
                    tempConn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, tempConn))
                    {
                        cmd.Parameters.AddWithValue("@ThanhTien", thanhTien);
                        cmd.Parameters.AddWithValue("@SoHDB", maHoaDon);
                        cmd.ExecuteNonQuery();
                    }
                }

                // Cập nhật hiển thị
                Load_DataGridViewChitiet();
                
                // Lấy và hiển thị tổng tiền mới
                sql = "SELECT TongTien FROM HoaDonBan WHERE SoHDB=@SoHDB";
                using (SqlConnection tempConn = new SqlConnection(function.ConnectionString))
                {
                    tempConn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, tempConn))
                    {
                        cmd.Parameters.AddWithValue("@SoHDB", maHoaDon);
                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            decimal tongTien = Convert.ToDecimal(result);
                            txtTongtien.Text = tongTien.ToString();
                            lblBangchu.Text = "Bằng chữ: " + function.ChuyenSoSangChu(tongTien.ToString());
                        }
                    }
                }

                ResetValuesHang();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật số lượng: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra và định dạng ngày
                string ngayBan = FormatDate(txtNgayban.Text);
                if (ngayBan == null)
                {
                    txtNgayban.Focus();
                    return;
                }

                // Kiểm tra các trường bắt buộc
                if (string.IsNullOrWhiteSpace(cboMahang.Text))
                {
                    MessageBox.Show("Bạn phải chọn mặt hàng", "Thông báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboMahang.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtSoluong.Text) || txtSoluong.Text == "0")
                {
                    MessageBox.Show("Bạn phải nhập số lượng", "Thông báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSoluong.Text = "";
                    txtSoluong.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtGiamgia.Text))
                {
                    txtGiamgia.Text = "0";
                }

                string maHoaDon = txtMaHDBan.Text.Trim();
                string maQuanAo = cboMahang.SelectedValue?.ToString();
                decimal soLuongMua = Convert.ToDecimal(txtSoluong.Text);

                // Kiểm tra hóa đơn đã tồn tại chưa
                bool hoaDonTonTai = false;
                string sql = "SELECT COUNT(*) FROM HoaDonBan WHERE SoHDB = @SoHDB";
                using (SqlConnection tempConn = new SqlConnection(function.ConnectionString))
                {
                    tempConn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, tempConn))
                    {
                        cmd.Parameters.AddWithValue("@SoHDB", maHoaDon);
                        int count = (int)cmd.ExecuteScalar();
                        hoaDonTonTai = (count > 0);
                    }
                }

                // Nếu hóa đơn chưa tồn tại, tạo mới hóa đơn
                if (!hoaDonTonTai)
                {
                    // Kiểm tra các trường bắt buộc cho hóa đơn mới
                    if (string.IsNullOrWhiteSpace(txtNgayban.Text))
                    {
                        MessageBox.Show("Bạn phải nhập ngày bán", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtNgayban.Focus();
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(cboManhanvien.Text))
                    {
                        MessageBox.Show("Bạn phải nhập nhân viên", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cboManhanvien.Focus();
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(cboMakhach.Text))
                    {
                        MessageBox.Show("Bạn phải nhập khách hàng", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cboMakhach.Focus();
                        return;
                    }

                    // Tạo hóa đơn mới
                    sql = "INSERT INTO HoaDonBan(SoHDB, NgayBan, MaNV, MaKhach, TongTien) VALUES (@SoHDB, @NgayBan, @MaNV, @MaKhach, 0)";
                    using (SqlConnection tempConn = new SqlConnection(function.ConnectionString))
                    {
                        tempConn.Open();
                        using (SqlCommand cmd = new SqlCommand(sql, tempConn))
                        {
                            cmd.Parameters.AddWithValue("@SoHDB", maHoaDon);
                            cmd.Parameters.AddWithValue("@NgayBan", ngayBan);
                            cmd.Parameters.AddWithValue("@MaNV", cboManhanvien.SelectedValue?.ToString());
                            cmd.Parameters.AddWithValue("@MaKhach", cboMakhach.SelectedValue?.ToString());
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                // Kiểm tra mặt hàng đã tồn tại trong hóa đơn chưa
                if (KiemTraMatHangTonTai(maHoaDon, maQuanAo))
                {
                    if (MessageBox.Show("Mặt hàng này đã có trong hóa đơn. Bạn có muốn cập nhật số lượng không?",
                        "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        CapNhatSoLuongTonTai(maHoaDon, maQuanAo, soLuongMua);
                    }
                    return;
                }

                // Kiểm tra số lượng tồn kho
                decimal soLuongTonKho = 0;
                sql = "SELECT SoLuong FROM SanPham WHERE MaQuanAo = @MaQuanAo";
                using (SqlConnection tempConn = new SqlConnection(function.ConnectionString))
                {
                    tempConn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, tempConn))
                    {
                        cmd.Parameters.AddWithValue("@MaQuanAo", maQuanAo);
                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            soLuongTonKho = Convert.ToDecimal(result);
                        }
                    }
                }

                if (soLuongMua > soLuongTonKho)
                {
                    MessageBox.Show($"Số lượng mặt hàng này chỉ còn {soLuongTonKho}", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSoluong.Text = "";
                    txtSoluong.Focus();
                    return;
                }

                // Thêm chi tiết hóa đơn
                decimal thanhTien = soLuongMua * Convert.ToDecimal(txtDongiaban.Text);
                decimal giamGia = Convert.ToDecimal(txtGiamgia.Text);
                thanhTien = thanhTien - (thanhTien * giamGia / 100);

                sql = "INSERT INTO ChiTietHDBan(SoHDB, MaQuanAo, SoLuong, GiamGia, ThanhTien) VALUES (@SoHDB, @MaQuanAo, @SoLuong, @GiamGia, @ThanhTien)";
                using (SqlConnection tempConn = new SqlConnection(function.ConnectionString))
                {
                    tempConn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, tempConn))
                    {
                        cmd.Parameters.AddWithValue("@SoHDB", maHoaDon);
                        cmd.Parameters.AddWithValue("@MaQuanAo", maQuanAo);
                        cmd.Parameters.AddWithValue("@SoLuong", soLuongMua);
                        cmd.Parameters.AddWithValue("@GiamGia", giamGia);
                        cmd.Parameters.AddWithValue("@ThanhTien", thanhTien);
                        cmd.ExecuteNonQuery();
                    }
                }

                // Cập nhật số lượng tồn kho
                sql = "UPDATE SanPham SET SoLuong = SoLuong - @SoLuong WHERE MaQuanAo = @MaQuanAo";
                using (SqlConnection tempConn = new SqlConnection(function.ConnectionString))
                {
                    tempConn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, tempConn))
                    {
                        cmd.Parameters.AddWithValue("@SoLuong", soLuongMua);
                        cmd.Parameters.AddWithValue("@MaQuanAo", maQuanAo);
                        cmd.ExecuteNonQuery();
                    }
                }

                // Cập nhật tổng tiền hóa đơn
                sql = "UPDATE HoaDonBan SET TongTien = TongTien + @ThanhTien WHERE SoHDB = @SoHDB";
                using (SqlConnection tempConn = new SqlConnection(function.ConnectionString))
                {
                    tempConn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, tempConn))
                    {
                        cmd.Parameters.AddWithValue("@ThanhTien", thanhTien);
                        cmd.Parameters.AddWithValue("@SoHDB", maHoaDon);
                        cmd.ExecuteNonQuery();
                    }
                }

                // Cập nhật hiển thị
                Load_DataGridViewChitiet();
                
                // Lấy và hiển thị tổng tiền mới
                sql = "SELECT TongTien FROM HoaDonBan WHERE SoHDB = @SoHDB";
                using (SqlConnection tempConn = new SqlConnection(function.ConnectionString))
                {
                    tempConn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, tempConn))
                    {
                        cmd.Parameters.AddWithValue("@SoHDB", maHoaDon);
                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            decimal tongTien = Convert.ToDecimal(result);
                            txtTongtien.Text = tongTien.ToString();
                            lblBangchu.Text = "Bằng chữ: " + function.ChuyenSoSangChu(tongTien.ToString());
                        }
                    }
                }

                // Reset các controls
                ResetValuesHang();
                btnXoa.Enabled = true;
                btnThemmoi.Enabled = true;
                btnInhoadon.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu hóa đơn: " + ex.Message, "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Khởi động chương trình Excel
            COMExcel.Application exApp = new COMExcel.Application();
            COMExcel.Workbook exBook;
            COMExcel.Worksheet exSheet;
            COMExcel.Range exRange;
            string sql;
            int hang = 0, cot = 0;
            DataTable tblThongtinHD, tblThongtinHang;
            exBook = exApp.Workbooks.Add(COMExcel.XlWBATemplate.xlWBATWorksheet);
            exSheet = exBook.Worksheets[1];
            
            // Định dạng chung
            exRange = exSheet.Cells[1, 1];
            exRange.Range["A1:B3"].Font.Size = 10;
            exRange.Range["A1:B3"].Font.Name = "Times new roman";
            exRange.Range["A1:B3"].Font.Bold = true;
            exRange.Range["A1:B3"].Font.ColorIndex = 5; //Màu xanh da trời
            exRange.Range["A1:A1"].ColumnWidth = 7;
            exRange.Range["B1:B1"].ColumnWidth = 15;
            exRange.Range["A1:B1"].MergeCells = true;
            exRange.Range["A1:B1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A1:B1"].Value = "Shop DTM by ThaoMy";
            exRange.Range["A2:B2"].MergeCells = true;
            exRange.Range["A2:B2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A2:B2"].Value = "Đống Đa - Hà Nội";
            exRange.Range["A3:B3"].MergeCells = true;
            exRange.Range["A3:B3"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A3:B3"].Value = "Điện thoại: (04)37562222";
            exRange.Range["C2:E2"].Font.Size = 16;
            exRange.Range["C2:E2"].Font.Name = "Times new roman";
            exRange.Range["C2:E2"].Font.Bold = true;
            exRange.Range["C2:E2"].Font.ColorIndex = 3; //Màu đỏ
            exRange.Range["C2:E2"].MergeCells = true;
            exRange.Range["C2:E2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["C2:E2"].Value = "HÓA ĐƠN BÁN";

            // Biểu diễn thông tin chung của hóa đơn bán
            sql = @"SELECT a.SoHDB, a.NgayBan, a.TongTien, b.TenKhach, b.DiaChi, b.DienThoai, c.TenNV 
                    FROM HoaDonBan AS a 
                    INNER JOIN KhachHang AS b ON a.MaKhach = b.MaKhach 
                    INNER JOIN NhanVien AS c ON a.MaNV = c.MaNV 
                    WHERE a.SoHDB = @SoHDB";

            using (SqlConnection tempConn = new SqlConnection(function.ConnectionString))
            {
                tempConn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, tempConn))
                {
                    cmd.Parameters.AddWithValue("@SoHDB", txtMaHDBan.Text);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        tblThongtinHD = new DataTable();
                        da.Fill(tblThongtinHD);
                    }
                }
            }

            exRange.Range["B6:C9"].Font.Size = 12;
            exRange.Range["B6:C9"].Font.Name = "Times new roman";
            exRange.Range["B6:B6"].Value = "Mã hóa đơn:";
            exRange.Range["C6:E6"].MergeCells = true;
            exRange.Range["C6:E6"].Value = tblThongtinHD.Rows[0][0].ToString();
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
            sql = @"SELECT b.MaQuanAo, b.TenQuanAo, a.SoLuong, b.DonGiaBan, a.GiamGia, a.ThanhTien 
                    FROM ChiTietHDBan AS a 
                    INNER JOIN SanPham AS b ON a.MaQuanAo = b.MaQuanAo 
                    WHERE a.SoHDB = @SoHDB";

            using (SqlConnection tempConn = new SqlConnection(function.ConnectionString))
            {
                tempConn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, tempConn))
                {
                    cmd.Parameters.AddWithValue("@SoHDB", txtMaHDBan.Text);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        tblThongtinHang = new DataTable();
                        da.Fill(tblThongtinHang);
                    }
                }
            }

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

            for (hang = 0; hang < tblThongtinHang.Rows.Count; hang++)
            {
                // Điền số thứ tự
                exSheet.Cells[hang + 12, 1] = hang + 1;
                
                // Điền tên hàng
                exSheet.Cells[hang + 12, 2] = tblThongtinHang.Rows[hang][1].ToString();
                
                // Điền số lượng
                exSheet.Cells[hang + 12, 3] = tblThongtinHang.Rows[hang][2].ToString();
                
                // Điền đơn giá
                exSheet.Cells[hang + 12, 4] = tblThongtinHang.Rows[hang][3].ToString();
                
                // Điền giảm giá
                exSheet.Cells[hang + 12, 5] = tblThongtinHang.Rows[hang][4].ToString();
                
                // Điền thành tiền
                exSheet.Cells[hang + 12, 6] = tblThongtinHang.Rows[hang][5].ToString();
            }

            // Định dạng tổng tiền
            exRange = exSheet.Cells[hang + 14, 1];
            exRange.Range["A1:F1"].MergeCells = true;
            exRange.Range["A1:F1"].Font.Bold = true;
            exRange.Range["A1:F1"].Font.Italic = true;
            exRange.Range["A1:F1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignRight;
            exRange.Range["A1:F1"].Value = "Tổng tiền: " + tblThongtinHD.Rows[0][2].ToString();

            exRange = exSheet.Cells[hang + 15, 1];
            exRange.Range["A1:F1"].MergeCells = true;
            exRange.Range["A1:F1"].Font.Bold = true;
            exRange.Range["A1:F1"].Font.Italic = true;
            exRange.Range["A1:F1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignRight;
            exRange.Range["A1:F1"].Value = "Bằng chữ: " + function.ChuyenSoSangChu(tblThongtinHD.Rows[0][2].ToString());

            // Định dạng phần cuối
            exRange = exSheet.Cells[hang + 17, 4];
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

            exSheet.Name = "Hóa đơn bán";
            exApp.Visible = true;
        }

        private void Frmquanlyhoadonban_Load(object sender, EventArgs e)
        {
            btnThemmoi.Enabled = true;
            btnLuu.Enabled = false;
            btnXoa.Enabled = false;
            btnInhoadon.Enabled = false;
            txtMaHDBan.ReadOnly = true;
            txtTennhanvien.ReadOnly = true;
            txtTenkhach.ReadOnly = true;
            txtDiachi.ReadOnly = true;
            txtDienthoai.ReadOnly = true;
            txtTenhang.ReadOnly = true;
            txtDongiaban.ReadOnly = true;
            txtThanhtien.ReadOnly = true;
            txtTongtien.ReadOnly = true;
            txtGiamgia.Text = "0";
            txtTongtien.Text = "0";
            function.FillCombo2("SELECT MaNV, MaNV FROM NhanVien", cboManhanvien, "MaNV", "MaNV");
            cboManhanvien.SelectedIndex = -1;
            function.FillCombo2("SELECT MaKhach, MaKhach FROM KhachHang", cboMakhach, "MaKhach", "MaKhach");
            cboMakhach.SelectedIndex = -1;
            function.FillCombo2("SELECT MaQuanAo, MaQuanAo FROM SanPham", cboMahang, "MaQuanAo", "MaQuanAo");
            cboMahang.SelectedIndex = -1;
            //Hiển thị thông tin của một hóa đơn được gọi từ form tìm kiếm
            if (txtMaHDBan.Text != "")
            {
                Load_ThongtinHD();
                btnXoa.Enabled = true;
                btnInhoadon.Enabled = true;
            }
            Load_DataGridViewChitiet();
        }
        private void Load_DataGridViewChitiet()
        {
            try
            {
                string sql = @"SELECT a.MaQuanAo, a.MaQuanAo AS TenQuanAo, a.SoLuong, b.DonGiaBan, a.GiamGia, a.ThanhTien 
                              FROM ChiTietHDBan AS a 
                              INNER JOIN SanPham AS b ON a.MaQuanAo = b.MaQuanAo 
                              WHERE a.SoHDB = @SoHDB";

                using (SqlConnection tempConn = new SqlConnection(function.ConnectionString))
                {
                    tempConn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, tempConn))
                    {
                        cmd.Parameters.AddWithValue("@SoHDB", txtMaHDBan.Text);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            ChiTietHDBan = new DataTable();
                            da.Fill(ChiTietHDBan);
                        }
                    }
                }

                DataGridViewChitiet.DataSource = ChiTietHDBan;
                DataGridViewChitiet.Columns[0].HeaderText = "Mã quần áo";
                DataGridViewChitiet.Columns[1].HeaderText = "Mã quần áo";
                DataGridViewChitiet.Columns[2].HeaderText = "Số lượng";
                DataGridViewChitiet.Columns[3].HeaderText = "Đơn giá";
                DataGridViewChitiet.Columns[4].HeaderText = "Giảm giá %";
                DataGridViewChitiet.Columns[5].HeaderText = "Thành tiền";
                DataGridViewChitiet.Columns[0].Width = 80;
                DataGridViewChitiet.Columns[1].Width = 130;
                DataGridViewChitiet.Columns[2].Width = 80;
                DataGridViewChitiet.Columns[3].Width = 90;
                DataGridViewChitiet.Columns[4].Width = 90;
                DataGridViewChitiet.Columns[5].Width = 90;
                DataGridViewChitiet.AllowUserToAddRows = false;
                DataGridViewChitiet.EditMode = DataGridViewEditMode.EditProgrammatically;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load dữ liệu: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Load_ThongtinHD()
        {
            string str;
            try
            {
                str = "SELECT NgayBan FROM HoaDonBan WHERE SoHDB = @SoHDB";
                using (SqlConnection tempConn = new SqlConnection(function.ConnectionString))
                {
                    tempConn.Open();
                    using (SqlCommand cmd = new SqlCommand(str, tempConn))
                    {
                        cmd.Parameters.AddWithValue("@SoHDB", txtMaHDBan.Text);
                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            txtNgayban.Text = function.ConvertDateTime(result.ToString());
                        }
                    }
                }

                str = "SELECT MaNV FROM HoaDonBan WHERE SoHDB = @SoHDB";
                using (SqlConnection tempConn = new SqlConnection(function.ConnectionString))
                {
                    tempConn.Open();
                    using (SqlCommand cmd = new SqlCommand(str, tempConn))
                    {
                        cmd.Parameters.AddWithValue("@SoHDB", txtMaHDBan.Text);
                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            cboManhanvien.SelectedValue = result.ToString();
                        }
                    }
                }

                str = "SELECT MaKhach FROM HoaDonBan WHERE SoHDB = @SoHDB";
                using (SqlConnection tempConn = new SqlConnection(function.ConnectionString))
                {
                    tempConn.Open();
                    using (SqlCommand cmd = new SqlCommand(str, tempConn))
                    {
                        cmd.Parameters.AddWithValue("@SoHDB", txtMaHDBan.Text);
                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            cboMakhach.SelectedValue = result.ToString();
                        }
                    }
                }

                str = "SELECT TongTien FROM HoaDonBan WHERE SoHDB = @SoHDB";
                using (SqlConnection tempConn = new SqlConnection(function.ConnectionString))
                {
                    tempConn.Open();
                    using (SqlCommand cmd = new SqlCommand(str, tempConn))
                    {
                        cmd.Parameters.AddWithValue("@SoHDB", txtMaHDBan.Text);
                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            txtTongtien.Text = result.ToString();
                            lblBangchu.Text = "Bằng chữ: " + function.ChuyenSoSangChu(result.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load thông tin hóa đơn: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThemmoi_Click(object sender, EventArgs e)
        {
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;
            btnInhoadon.Enabled = false;
            btnThemmoi.Enabled = false;
            ResetValues();
            txtMaHDBan.Text = function.TaoMaHoaDonMoi();
            Load_DataGridViewChitiet();
        }
        private void ResetValues()
        {
            txtMaHDBan.Text = " ";
            txtNgayban.Text = DateTime.Now.ToShortDateString();
            cboManhanvien.Text = "";
            cboMakhach.Text = "";
            txtTongtien.Text = "0";
            lblBangchu.Text = "Bằng chữ: ";
            cboMahang.Text = "";
            txtSoluong.Text = "";
            txtGiamgia.Text = "0";
            txtThanhtien.Text = "0";
            txtTennhanvien.Text = "";
            txtTenkhach.Text = "";
            txtDiachi.Text = "";
            txtDienthoai.Text = "";
        }
        private void ResetValuesHang()
        {
            cboMahang.Text = "";
            txtSoluong.Text = "";
            txtGiamgia.Text = "0";
            txtThanhtien.Text = "0";
        }

        private void DataGridViewChitiet_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string mahang;
            Double Thanhtien;
            if (ChiTietHDBan.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK,
MessageBoxIcon.Information);
                return;
            }
            if ((MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo",
MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
            {
                //Xóa hàng và cập nhật lại số lượng hàng 
                mahang = DataGridViewChitiet.CurrentRow.Cells["MaQuanAo"].Value.ToString();
                DelHang(txtMaHDBan.Text, mahang);
                // Cập nhật lại tổng tiền cho hóa đơn bán
                Thanhtien = Convert.ToDouble(DataGridViewChitiet.CurrentRow.
Cells["ThanhTien"].Value.ToString());
                DelUpdateTongtien(txtMaHDBan.Text, Thanhtien);
                Load_DataGridViewChitiet();

            }

        }
        private void DelHang(string Mahoadon, string Mahang)
        {
            try
            {
                decimal soLuongTrongHD = 0;
                decimal soLuongHienTai = 0;

                // Lấy số lượng trong hóa đơn
                string sql = "SELECT SoLuong FROM ChiTietHDBan WHERE SoHDB = @SoHDB AND MaQuanAo = @MaQuanAo";
                using (SqlConnection tempConn = new SqlConnection(function.ConnectionString))
                {
                    tempConn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, tempConn))
                    {
                        cmd.Parameters.AddWithValue("@SoHDB", Mahoadon);
                        cmd.Parameters.AddWithValue("@MaQuanAo", Mahang);
                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            soLuongTrongHD = Convert.ToDecimal(result);
                        }
                    }
                }

                // Lấy số lượng hiện tại của sản phẩm
                sql = "SELECT SoLuong FROM SanPham WHERE MaQuanAo = @MaQuanAo";
                using (SqlConnection tempConn = new SqlConnection(function.ConnectionString))
                {
                    tempConn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, tempConn))
                    {
                        cmd.Parameters.AddWithValue("@MaQuanAo", Mahang);
                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            soLuongHienTai = Convert.ToDecimal(result);
                        }
                    }
                }

                // Xóa chi tiết hóa đơn
                sql = "DELETE ChiTietHDBan WHERE SoHDB = @SoHDB AND MaQuanAo = @MaQuanAo";
                using (SqlConnection tempConn = new SqlConnection(function.ConnectionString))
                {
                    tempConn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, tempConn))
                    {
                        cmd.Parameters.AddWithValue("@SoHDB", Mahoadon);
                        cmd.Parameters.AddWithValue("@MaQuanAo", Mahang);
                        cmd.ExecuteNonQuery();
                    }
                }

                // Cập nhật lại số lượng sản phẩm
                decimal soLuongMoi = soLuongHienTai + soLuongTrongHD;
                sql = "UPDATE SanPham SET SoLuong = @SoLuong WHERE MaQuanAo = @MaQuanAo";
                using (SqlConnection tempConn = new SqlConnection(function.ConnectionString))
                {
                    tempConn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, tempConn))
                    {
                        cmd.Parameters.AddWithValue("@SoLuong", soLuongMoi);
                        cmd.Parameters.AddWithValue("@MaQuanAo", Mahang);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xóa mặt hàng: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DelUpdateTongtien(string Mahoadon, double Thanhtien)
        {
            Double Tong, Tongmoi;
            string sql;
            sql = "SELECT TongTien FROM HoaDonBan WHERE SoHDB = N'" + Mahoadon + "'";
            Tong = Convert.ToDouble(function.GetFieldValues(sql));
            Tongmoi = Tong - Thanhtien;
            sql = "UPDATE HoaDonBan SET TongTien =" + Tongmoi + " WHERE SoHDB = N'" +
Mahoadon + "'";
            function.RunSQL(sql);
            txtTongtien.Text = Tongmoi.ToString();
            lblBangchu.Text = "Bằng chữ: " + function.ChuyenSoSangChu(Tongmoi.ToString());
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    string[] Mahang = new string[20];
                    string sql;
                    int n = 0;

                    // Lấy danh sách mã hàng cần xóa
                    sql = "SELECT MaQuanAo FROM ChiTietHDBan WHERE SoHDB = @SoHDB";
                    using (SqlConnection tempConn = new SqlConnection(function.ConnectionString))
                    {
                        tempConn.Open();
                        using (SqlCommand cmd = new SqlCommand(sql, tempConn))
                        {
                            cmd.Parameters.AddWithValue("@SoHDB", txtMaHDBan.Text);
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    Mahang[n] = reader.GetString(0);
                                    n++;
                                }
                            }
                        }
                    }

                    // Xóa từng mặt hàng trong hóa đơn
                    for (int i = 0; i < n; i++)
                    {
                        DelHang(txtMaHDBan.Text, Mahang[i]);
                    }

                    // Xóa hóa đơn
                    sql = "DELETE HoaDonBan WHERE SoHDB = @SoHDB";
                    using (SqlConnection tempConn = new SqlConnection(function.ConnectionString))
                    {
                        tempConn.Open();
                        using (SqlCommand cmd = new SqlCommand(sql, tempConn))
                        {
                            cmd.Parameters.AddWithValue("@SoHDB", txtMaHDBan.Text);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    ResetValues();
                    Load_DataGridViewChitiet();
                    btnXoa.Enabled = false;
                    btnInhoadon.Enabled = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa hóa đơn: " + ex.Message, "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void cboManhanvien_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cboManhanvien_TextChanged(object sender, EventArgs e)
        {
            string str;
            if (cboManhanvien.Text == "")
                txtTennhanvien.Text = "";
            // Khi kich chon Ma nhan vien thi ten nhan vien se tu dong hien ra
            str = "Select TenNV from NhanVien where MaNV =N'" +
cboManhanvien.SelectedValue + "'";
            txtTennhanvien.Text = function.GetFieldValues(str);

        }

        private void cboMakhach_TextChanged(object sender, EventArgs e)
        {
            string str;
            if (cboMakhach.Text == "")
            {
                txtTenkhach.Text = "";
                txtDiachi.Text = "";
                txtDienthoai.Text = "";
            }
            //Khi kich chon Ma khach thi ten khach, dia chi, dien thoai se tu dong hien ra
            str = "Select TenKhach from KhachHang where MaKhach = N'" + cboMakhach.SelectedValue
+ "'";
            txtTenkhach.Text = function.GetFieldValues(str);
            str = "Select DiaChi from KhachHang where MaKhach = N'" + cboMakhach.SelectedValue
+ "'";
            txtDiachi.Text = function.GetFieldValues(str);
            str = "Select DienThoai from KhachHang where MaKhach= N'" + cboMakhach.SelectedValue
  + "'";
            txtDienthoai.Text = function.GetFieldValues(str);

        }

        private void cboMahang_TextChanged(object sender, EventArgs e)
        {
            string str;
            if (cboMahang.Text == "")
            {
                txtTenhang.Text = "";
                txtDongiaban.Text = "";
            }
            // Khi kich chon Ma hang thi ten hang va gia ban se tu dong hien ra
            str = "SELECT TenQuanAo FROM SanPham WHERE MaQuanAo =N'" + cboMahang.SelectedValue
+ "'";
            txtTenhang.Text = function.GetFieldValues(str);
            str = "SELECT DonGiaBan FROM SanPham WHERE MaQuanAo =N'" + cboMahang.SelectedValue
+ "'";
            txtDongiaban.Text = function.GetFieldValues(str);

        }

        private void txtSoluong_TextChanged(object sender, EventArgs e)
        {
            //Khi thay doi So luong, Giam gia thi Thanh tien tu dong cap nhat lai gia tri
            double tt, sl, dg, gg;
            if (txtSoluong.Text == "")
                sl = 0;
            else
                sl = Convert.ToDouble(txtSoluong.Text);
            if (txtGiamgia.Text == "")
                gg = 0;
            else
                gg = Convert.ToDouble(txtGiamgia.Text);
            if (txtDongiaban.Text == "")
                dg = 0;
            else
                dg = Convert.ToDouble(txtDongiaban.Text);
            tt = sl * dg - sl * dg * gg / 100;
            txtThanhtien.Text = tt.ToString();

        }

        private void txtGiamgia_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            if (cboMaHDBan.Text == "")
            {
                MessageBox.Show("Bạn phải chọn một mã hóa đơn để tìm", "Thông báo",
MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMaHDBan.Focus();
                return;
            }
            txtMaHDBan.Text = cboMaHDBan.Text;
            Load_ThongtinHD();
            Load_DataGridViewChitiet();
            btnXoa.Enabled = true;
            btnLuu.Enabled = true;
            btnInhoadon.Enabled = true;
            cboMaHDBan.SelectedIndex = -1;

        }

        private void txtSoluong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= '0') && (e.KeyChar <= '9')) || (Convert.ToInt32(e.KeyChar) == 8))
                e.Handled = false;
            else
                e.Handled = true;

        }

        private void cboMaHDBan_DropDown(object sender, EventArgs e)
        {
            function.FillCombo2("SELECT SoHDB FROM HoaDonBan", cboMaHDBan, "SoHDB", "SoHDB");
            cboMaHDBan.SelectedIndex = -1;
        }

        private void Frmquanlyhoadonban_FormClosing(object sender, FormClosingEventArgs e)
        {
            ResetValues();

        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void DataGridViewChitiet_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ChiTietHDBan == null || ChiTietHDBan.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (e.RowIndex >= 0 && DataGridViewChitiet.CurrentRow != null)
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string mahang = DataGridViewChitiet.CurrentRow.Cells["MaQuanAo"].Value.ToString();
                    double Thanhtien = Convert.ToDouble(DataGridViewChitiet.CurrentRow.Cells["ThanhTien"].Value.ToString());
                    DelHang(txtMaHDBan.Text, mahang);
                    DelUpdateTongtien(txtMaHDBan.Text, Thanhtien);
                    Load_DataGridViewChitiet();
                }
            }
        }
    }
}