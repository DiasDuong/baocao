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
            ResetValues();
            dataGridViewTimkiemHDN.DataSource = null;
            ResetValues();

        }
        private void ResetValues()
        {
            foreach (Control ctl in this.Controls)
            {
                if (ctl is TextBox || ctl is ComboBox)
                    ctl.Text = "";
            }
            txtSoHDN.Focus();
        }

        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            string sql;
            if ((txtSoHDN.Text == "") && (txtThang.Text == "") && (txtNam.Text == "") &&
                (txtMaNV.Text == "") && (txtMaNCC.Text == "") &&
                (txtTongtien.Text == ""))
            {
                MessageBox.Show("Hãy nhập một điều kiện tìm kiếm!!!", "Yêu cầu ...",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            sql = "SELECT * FROM HoaDonNhap WHERE 1=1";
            if (txtSoHDN.Text != "")
                sql += " AND SoHDN = N'" + txtSoHDN.Text + "'";
            if (txtThang.Text != "")
                sql += " AND MONTH(Ngayban) =" + txtThang.Text;
            if (txtNam.Text != "")
                sql += " AND YEAR(Ngayban) =" + txtNam.Text;
            if (txtMaNV.Text != "")
                sql += " AND Manhanvien Like N'%" + txtMaNV.Text + "%'";
            if (txtMaNCC.Text != "")
                sql += " AND Manhacungcap Like N'%" + txtMaNCC.Text + "%'";
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
    }
}
