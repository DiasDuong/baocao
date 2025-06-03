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
    public partial class TimkiemHDN : Form
    {
        DataTable HoaDonNhap;
        public TimkiemHDN()
        {
            InitializeComponent();
        }

        private void TimkiemHDN_Load(object sender, EventArgs e)
        {
            Load_cboSoHDN(); 
            ResetValues();
            dataGridViewTimkiemHDN.DataSource = null;

        }
        private void Load_cboSoHDN()
        {
            string sql = "SELECT SoHDN FROM HoaDonNhap";
            DataTable table = function.GetDataToTable(sql);
            cboSoHDN.DataSource = table;
            cboSoHDN.DisplayMember = "SoHDN";
            cboSoHDN.ValueMember = "SoHDN";
            cboSoHDN.SelectedIndex = -1;
        }

        private void ResetValues()
        {
            foreach (Control ctl in this.Controls)
            {
                if (ctl is TextBox)
                    ctl.Text = "";
                else if (ctl is ComboBox)
                    ((ComboBox)ctl).SelectedIndex = -1;
            }
            cboSoHDN.Focus(); // thay vì txtSoHDN
        }

        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            string sql;
            if ((cboSoHDN.SelectedIndex == -1) && string.IsNullOrWhiteSpace(txtNgayNhap.Text) &&
    string.IsNullOrWhiteSpace(txtMaNV.Text) && string.IsNullOrWhiteSpace(txtMaNCC.Text) &&
    string.IsNullOrWhiteSpace(txtTongtien.Text))

            {
                MessageBox.Show("Hãy nhập một điều kiện tìm kiếm!!!", "Yêu cầu ...",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            sql = "SELECT * FROM HoaDonNhap WHERE 1=1";
            if (cboSoHDN.SelectedIndex != -1)
                sql += " AND SoHDN = N'" + cboSoHDN.SelectedValue.ToString() + "'";
            if (!string.IsNullOrWhiteSpace(txtNgayNhap.Text))
            {
                DateTime ngayNhap;
                if (DateTime.TryParse(txtNgayNhap.Text, out ngayNhap))
                {
                    sql += " AND CONVERT(date, Ngayban, 103) = '" + ngayNhap.ToString("yyyy-MM-dd") + "'";
                }
                else
                {
                    MessageBox.Show("Ngày nhập không hợp lệ! Định dạng đúng: dd/MM/yyyy", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNgayNhap.Focus();
                    return;
                }
            }

            if (!string.IsNullOrWhiteSpace(txtMaNV.Text))
                sql += " AND Manhanvien = N'" + txtMaNV.Text.Trim() + "'";
            if (!string.IsNullOrWhiteSpace(txtMaNCC.Text))
                sql += " AND Manhacungcap = N'" + txtMaNCC.Text.Trim() + "'";
            if (txtTongtien.Text != "")
                sql += " AND Tongtien <=" + txtTongtien.Text;

            HoaDonNhap = function.GetDataToTable(sql);

            if (HoaDonNhap == null || HoaDonNhap.Rows.Count == 0)
            {
                MessageBox.Show("Không tìm thấy hóa đơn nào thỏa mãn điều kiện tìm kiếm.");
                dataGridViewTimkiemHDN.DataSource = null;
            }
            else
            {
                Load_dataGridViewTimkiemHDN();
            }
        }
        private void Load_dataGridViewTimkiemHDN()
        {
            dataGridViewTimkiemHDN.DataSource = null;
            dataGridViewTimkiemHDN.Columns.Clear();

            dataGridViewTimkiemHDN.DataSource = HoaDonNhap;

            // Đặt tiêu đề cột và ẩn các cột dư
            string[] headers = { "Số HDN", "Mã nhân viên", "Ngày nhập", "Mã nhà cung cấp", "Tổng tiền" };
            for (int i = 0; i < dataGridViewTimkiemHDN.Columns.Count; i++)
            {
                if (i < headers.Length)
                {
                    dataGridViewTimkiemHDN.Columns[i].HeaderText = headers[i];
                    dataGridViewTimkiemHDN.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
                else
                {
                    // Ẩn các cột dư
                    dataGridViewTimkiemHDN.Columns[i].Visible = false;
                }
            }

            // Căn chỉnh
            dataGridViewTimkiemHDN.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewTimkiemHDN.AllowUserToAddRows = false;
            dataGridViewTimkiemHDN.EditMode = DataGridViewEditMode.EditProgrammatically;
            HienThiChiTietTuDongDauTien();

        }
        private void HienThiChiTietTuDongDauTien()
        {
            if (HoaDonNhap != null && HoaDonNhap.Rows.Count > 0)
            {
                DataRow row = HoaDonNhap.Rows[0];

                // Hiển thị Số HDN
                cboSoHDN.SelectedValue = row["SoHDN"].ToString();

                // Hiển thị Mã NV
                // Hiển thị Mã NV
                txtMaNV.Text = row["MaNV"].ToString();

                // Hiển thị Mã NCC
                txtMaNCC.Text = row["MaNCC"].ToString();


                // Hiển thị Ngày nhập (giả sử cột là Ngayban hoặc Ngaynhap)
                string colNgay = row.Table.Columns.Contains("NgayNhap") ? "NgayNhap" : "Ngayban";
                if (row.Table.Columns.Contains(colNgay) && row[colNgay] != DBNull.Value)
                    txtNgayNhap.Text = Convert.ToDateTime(row[colNgay]).ToString("dd/MM/yyyy");
                else
                    txtNgayNhap.Text = "";



                // Hiển thị Tổng tiền nếu có
                if (row.Table.Columns.Contains("Tongtien"))
                    txtTongtien.Text = row["Tongtien"].ToString();
            }
            else
            {
                // Nếu không có dữ liệu, reset các control
                cboSoHDN.SelectedIndex = -1;
                txtMaNV.Text = "";
                txtMaNCC.Text = "";
                txtNgayNhap.Text = "";
                txtTongtien.Text = "";
            }
        }


        private void dataGridViewTimkiemHDN_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnTimlai_Click(object sender, EventArgs e)
        {
            ResetValues();
            dataGridViewTimkiemHDN.DataSource = null;
        }
        private void txtTongtien_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= '0') && (e.KeyChar <= '9')) || (Convert.ToInt32(e.KeyChar) == 8))
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void dataGridViewTimkiemHDN_DoubleClick(object sender, EventArgs e)
        {

            string mahd;
            if (MessageBox.Show("Bạn có muốn hiển thị thông tin chi tiết?", "Xác nhận",MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                mahd = dataGridViewTimkiemHDN.CurrentRow.Cells["SoHDN"].Value.ToString();
                HoaDonNhap frm = new HoaDonNhap();
                frm.txtSoHD.Text = mahd;
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.ShowDialog();
            }

        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}
