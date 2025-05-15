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
    public partial class doimatkhau : Form
    {
        public doimatkhau()
        {
            InitializeComponent();
        }

        private void doimatkhau_Load(object sender, EventArgs e)
        {

        }

        private void btndoimk_Click(object sender, EventArgs e)
        {
            string tenDangNhap = txttendn.Text.Trim();
            string matKhauCu = txtmkcu.Text.Trim();
            string matKhauMoi = txtmkmoi.Text.Trim();
            string xacNhanMK = txtmkmoii.Text.Trim();

            if (tenDangNhap == "" || matKhauCu == "" || matKhauMoi == "" || xacNhanMK == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (matKhauMoi != xacNhanMK)
            {
                MessageBox.Show("Mật khẩu mới và xác nhận không khớp!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            function.Connect();

            string sqlCheck = "SELECT COUNT(*) FROM TaiKhoan WHERE TaiKhoan = @TenDN AND Pass = @MKcu";
            SqlCommand cmdCheck = new SqlCommand(sqlCheck, function.conn);
            cmdCheck.Parameters.AddWithValue("@TenDN", tenDangNhap);
            cmdCheck.Parameters.AddWithValue("@MKcu", matKhauCu);

            int count = (int)cmdCheck.ExecuteScalar();
            if (count == 0)
            {
                MessageBox.Show("Mật khẩu hiện tại không đúng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                function.Disconnect();
                return;
            }

            string sqlUpdate = "UPDATE TaiKhoan SET Pass = @MKmoi WHERE TaiKhoan = @TenDN";
            SqlCommand cmdUpdate = new SqlCommand(sqlUpdate, function.conn);
            cmdUpdate.Parameters.AddWithValue("@MKmoi", matKhauMoi);
            cmdUpdate.Parameters.AddWithValue("@TenDN", tenDangNhap);

            int rows = cmdUpdate.ExecuteNonQuery();
            function.Disconnect();

            if (rows > 0)
            {
                MessageBox.Show("Đổi mật khẩu thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Đổi mật khẩu thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
