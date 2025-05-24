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
        }

        private void btndangky_Click(object sender, EventArgs e)
        {
            // Kiểm tra tính hợp lệ của dữ liệu
            if (txtmail.Text == "")
            {
                MessageBox.Show("Vui lòng nhập địa chỉ email!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtmail.Focus();
                return;
            }

            string email = txtmail.Text.Trim();
            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Email không đúng định dạng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

            // Kiểm tra tên đăng nhập đã tồn tại
            string sqlCheckExist = "SELECT COUNT(*) FROM TaiKhoan WHERE TaiKhoan = N'" + txttendn.Text + "'";
            int count = Convert.ToInt32(function.GetFieldValues(sqlCheckExist));
            if (count > 0)
            {
                MessageBox.Show("Tên đăng nhập đã tồn tại. Vui lòng chọn tên đăng nhập khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txttendn.Text = "";
                txttendn.Focus();
                return;
            }

            // Thêm tài khoản vào database
            string sqlInsert = "INSERT INTO TaiKhoan (Email, TaiKhoan, Pass) VALUES " +
                               "(N'" + txtmail.Text + "', N'" + txttendn.Text + "', N'" + txtmk.Text + "')";
            function.RunSQL(sqlInsert);
            MessageBox.Show("Đăng ký tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Gửi email xác nhận
            SendConfirmationEmail(txtmail.Text.Trim(), txttendn.Text.Trim());

            txttendn.Text = "";
            txtmail.Text = "";
            txtmk.Text = "";
            txtnhaplaimk.Text = "";
            this.Close();
        }

        private void SendConfirmationEmail(string toEmail, string username)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("huyenjuly2508@gmail.com"); // 📌 Thay bằng email của bạn
                mail.To.Add(toEmail);
                mail.Subject = "Xác nhận đăng ký thành công";
                mail.Body = $"Chào {username},\n\nBạn đã đăng ký tài khoản thành công tại Borcelle Fashion Store.\n\nXin cảm ơn!";
                mail.IsBodyHtml = false;

                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential("huyenjuly2508@gmail.com", "okho btkr zdde ywsy"); // 📌 Mật khẩu ứng dụng
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
    }
}
