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
    public partial class dangnhap : Form
    {
        public dangnhap()
        {
            InitializeComponent();
        }

        private void dangnhap_Load(object sender, EventArgs e)
        {

        }

        private void btndn_Click(object sender, EventArgs e)
        {
            // Gọi kết nối CSDL
            function.Connect();

            // Kiểm tra ô nhập
            if (txttendn.Text.Trim() == "" || txtmk.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Câu truy vấn kiểm tra tài khoản
            string sql = "SELECT COUNT(*) FROM TaiKhoan WHERE TaiKhoan = N'" + txttendn.Text + "' AND Pass = N'" + txtmk.Text + "'";
            int result = Convert.ToInt32(function.GetFieldValues(sql));

            if (result > 0)
            {
                MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                
            }
            else
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu sai!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtmk.Clear();
                txtmk.Focus();
            }
        }

        private void linkLabel1_Click(object sender, EventArgs e)
        {
            dangky dangKyForm = new dangky();
            dangKyForm.Show();
        }
    }
}
