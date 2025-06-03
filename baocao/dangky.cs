using System;
using System.Data;
using System.Net;
using System.Net.Mail;
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
            txtmk.PasswordChar = '●';
            txtnhaplaimk.PasswordChar = '●';
        }

        private void btndangky_Click(object sender, EventArgs e)
        {
            // Lấy dữ liệu từ form
            string email = txtmail.Text.Trim();
            string tenDangNhap = txttendn.Text.Trim();
            string matKhau = txtmk.Text.Trim();
            string nhapLaiMatKhau = txtnhaplaimk.Text.Trim();

            // ==== Kiểm tra hợp lệ ====
            if (email == "")
            {
                MessageBox.Show("Vui lòng nhập địa chỉ email!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtmail.Focus();
                return;
            }

            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Email không đúng định dạng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtmail.Focus();
                return;
            }

            if (tenDangNhap == "")
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txttendn.Focus();
                return;
            }

            if (matKhau == "")
            {
                MessageBox.Show("Vui lòng nhập mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtmk.Focus();
                return;
            }

            if (nhapLaiMatKhau == "")
            {
                MessageBox.Show("Vui lòng nhập lại mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtnhaplaimk.Focus();
                return;
            }

            if (matKhau != nhapLaiMatKhau)
            {
                MessageBox.Show("Mật khẩu không khớp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtnhaplaimk.Clear();
                txtnhaplaimk.Focus();
                return;
            }

            function.Connect();

            // ==== Kiểm tra trùng email (PRIMARY KEY) ====
            string sqlCheckEmail = $"SELECT COUNT(*) FROM TaiKhoan WHERE Email = N'{email}'";
            int emailCount = Convert.ToInt32(function.GetFieldValues(sqlCheckEmail));
            if (emailCount > 0)
            {
                MessageBox.Show("Email này đã được sử dụng. Vui lòng nhập email khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtmail.Focus();
                return;
            }

            // ==== Kiểm tra trùng tên đăng nhập ====
            string sqlCheckUser = $"SELECT COUNT(*) FROM TaiKhoan WHERE TaiKhoan = N'{tenDangNhap}'";
            int userCount = Convert.ToInt32(function.GetFieldValues(sqlCheckUser));
            if (userCount > 0)
            {
                MessageBox.Show("Tên đăng nhập đã tồn tại. Vui lòng chọn tên khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txttendn.Focus();
                return;
            }

            // ==== Thêm tài khoản vào CSDL ====
            string sqlInsert = $"INSERT INTO TaiKhoan (Email, TaiKhoan, Pass) VALUES (N'{email}', N'{tenDangNhap}', N'{matKhau}')";
            try
            {
                function.RunSQL(sqlInsert);
                MessageBox.Show("Đăng ký tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // ==== Gửi email xác nhận ====
                SendConfirmationEmail(email, tenDangNhap);

                // Reset form
                txtmail.Clear();
                txttendn.Clear();
                txtmk.Clear();
                txtnhaplaimk.Clear();
 // hoặc mở form đăng nhập
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm tài khoản: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SendConfirmationEmail(string toEmail, string username)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("huyenjuly2508@gmail.com");
                mail.To.Add(toEmail);
                mail.Subject = "Xác nhận đăng ký thành công";
                mail.Body = $"Chào {username},\n\nBạn đã đăng ký tài khoản thành công tại Borcelle Fashion Store.\n\nXin cảm ơn!";
                mail.IsBodyHtml = false;

                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential("huyenjuly2508@gmail.com", "okho btkr zdde ywsy"); // Mật khẩu ứng dụng Gmail
                smtp.EnableSsl = true;

                smtp.Send(mail);
                MessageBox.Show("Email xác nhận đã được gửi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SmtpException ex)
            {
                MessageBox.Show("Lỗi gửi email: " + ex.Message, "Lỗi Email", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khác khi gửi email: " + ex.Message, "Lỗi Email", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linkdangnhap_Click(object sender, EventArgs e)
        {
            dangnhap loginForm = new dangnhap();
            loginForm.Show();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = checkBox1.Checked;
            txtmk.PasswordChar = isChecked ? '\0' : '●';
            txtnhaplaimk.PasswordChar = isChecked ? '\0' : '●';
        }
    }
}
