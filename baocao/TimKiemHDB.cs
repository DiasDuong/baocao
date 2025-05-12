using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace baocao
{
    public partial class TimKiemHDB : Form
    {
        DataTable HoaDonBan;
        public TimKiemHDB()
        {
            InitializeComponent();
        }

        private void TimKiemHDB_Load(object sender, EventArgs e)
        {
            ResetValues();
            dataGridViewTimkiemHDB.DataSource = null;
            ResetValues();

        }
        private void ResetValues()
        {
            foreach (Control ctl in this.Controls)
            {
                if (ctl is TextBox || ctl is ComboBox)
                    ctl.Text = "";
            }
            txtSoHDB.Focus();
        }

        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            string sql;
            if ((txtSoHDB.Text == "") && (txtThang.Text == "") && (txtNam.Text == "") &&
               (txtMaNV.Text == "") && (txtMaKH.Text == "") &&
               (txtTongtien.Text == ""))
            {
                MessageBox.Show("Hãy nhập một điều kiện tìm kiếm!!!", "Yeu cau ...",
    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            sql = "SELECT * FROM HoaDonBan WHERE 1=1";
            if (txtSoHDB.Text != "")
                sql = sql + " AND SoHDN Like N'%" + txtSoHDB.Text + "%'";
            if (txtThang.Text != "")
                sql = sql + " AND MONTH(Ngayban) =" + txtThang.Text;
            if (txtNam.Text != "")
                sql = sql + " AND YEAR(Ngayban) =" + txtNam.Text;
            if (txtMaNV.Text != "")
                sql = sql + " AND Manhanvien Like N'%" + txtMaNV.Text + "%'";
            if (txtMaKH.Text != "")
                sql = sql + " AND Makhach Like N'%" + txtMaKH.Text + "%'";
            if (txtTongtien.Text != "")
                sql = sql + " AND Tongtien <=" + txtTongtien.Text;
            HoaDonBan = function.GetDataToTable(sql);
            if (HoaDonBan != null && HoaDonBan.Rows.Count == 0)
            {
                MessageBox.Show("Không tìm thấy hóa đơn nào thỏa mãn điều kiện tìm kiếm.");
            }
            else
            {
                dataGridViewTimkiemHDB.DataSource = HoaDonBan;
            }
        }
        private void Load_dataGridViewTimkiemHDB()
        {
            dataGridViewTimkiemHDB.Columns[0].HeaderText = "Số HDB";
            dataGridViewTimkiemHDB.Columns[1].HeaderText = "Mã nhân viên";
            dataGridViewTimkiemHDB.Columns[2].HeaderText = "Ngày bán";
            dataGridViewTimkiemHDB.Columns[3].HeaderText = "Mã khách";
            dataGridViewTimkiemHDB.Columns[4].HeaderText = "Tổng tiền";

            dataGridViewTimkiemHDB.Columns[0].Width = 150;
            dataGridViewTimkiemHDB.Columns[1].Width = 100;
            dataGridViewTimkiemHDB.Columns[2].Width = 80;
            dataGridViewTimkiemHDB.Columns[3].Width = 80;
            dataGridViewTimkiemHDB.Columns[4].Width = 80;
            dataGridViewTimkiemHDB.AllowUserToAddRows = false;
            dataGridViewTimkiemHDB.EditMode = DataGridViewEditMode.EditProgrammatically;
        }
        private void dataGridViewTimkiemHDB_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnTimlai_Click(object sender, EventArgs e)
        {
            ResetValues();
            dataGridViewTimkiemHDB.DataSource = null;
        }

        private void txtTongtien_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (((e.KeyChar >= '0') && (e.KeyChar <= '9')) || (Convert.ToInt32(e.KeyChar) == 8))
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void dataGridViewTimkiemHDB_DoubleClick(object sender, EventArgs e)
        {
            string mahd;
            if (MessageBox.Show("Bạn có muốn hiển thị thông tin chi tiết?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                mahd = dataGridViewTimkiemHDB.CurrentRow.Cells["SoHDB"].Value.ToString();
                Frmquanlyhoadonban frm = new Frmquanlyhoadonban();
                frm.txtMaHDBan.Text = mahd;
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
