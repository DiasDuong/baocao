using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace baocao
{
    public partial class dangky : Form
    {
        public dangky()
        {
            InitializeComponent();
        }

        private void dangky_Load(object sender, EventArgs e)
        {
            
        }

        private void btndangky_Click(object sender, EventArgs e)
        {
            // ✅ Kiểm tra định dạng email
            string email = txtmail.Text.Trim();
            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Email không đúng định dạng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtmail.Focus();
                return;
            }
            // Kiểm tra tính hợp lệ của dữ liệu
            if (txtmail.Text == "")
            {
                MessageBox.Show("Vui lòng nhập địa chỉ email!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtmail.Focus();
                return;
            }

            if (txttendn.Text == "")
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txttendn.Focus();
                return;
            }

            if (txtmk.Text == "")
            {
                MessageBox.Show("Vui lòng nhập mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtmk.Focus();
                return;
            }

            if (txtnhaplaimk.Text == "")
            {
                MessageBox.Show("Vui lòng nhập lại mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtnhaplaimk.Focus();
                return;
            }

            if (txtmk.Text != txtnhaplaimk.Text)
            {
                MessageBox.Show("Mật khẩu không khớp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtnhaplaimk.Text = "";
                txtnhaplaimk.Focus();
                return;
            }
            function.Connect();

            // Kiểm tra xem tên đăng nhập đã tồn tại chưa
            string sqlCheckExist = "SELECT COUNT(*) FROM TaiKhoan WHERE TaiKhoan = N'" + txttendn.Text + "'";
            int count = Convert.ToInt32(function.GetFieldValues(sqlCheckExist));
            if (count > 0)
            {
                MessageBox.Show("Tên đăng nhập đã tồn tại. Vui lòng chọn tên đăng nhập khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txttendn.Text = "";
                txttendn.Focus();
                return;
            }

            // Thêm thông tin tài khoản vào database
            string sqlInsert = "INSERT INTO TaiKhoan (Email, TaiKhoan, Pass) VALUES " +
                               "(N'" + txtmail.Text + "', N'" + txttendn.Text + "', N'" + txtmk.Text + "')";
            function.RunSQL(sqlInsert);
            MessageBox.Show("Đăng ký tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            txttendn.Text = "";
            txtmail.Text = "";
            txtmk.Text = "";
            txtnhaplaimk.Text = "";
            this.Close();
        }

        private void linkdangnhap_Click(object sender, EventArgs e)
        {
           
            dangnhap loginForm = new dangnhap(); 
            loginForm.Show(); 

            // Đóng form đăng ký
           this.Close();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
