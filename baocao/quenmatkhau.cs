using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace baocao
{
    public partial class quenmatkhau : Form
    {
        public quenmatkhau()
        {
            InitializeComponent();
        }

        private void quenmatkhau_Load(object sender, EventArgs e)
        {

        }

        private void btnlaymk_Click(object sender, EventArgs e)
        {
            function.Connect();

            string tenTK = txttendn.Text.Trim();
            string email = txtemail.Text.Trim();

            if (tenTK == "" || email == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra tài khoản và email
            string sql = "SELECT COUNT(*) FROM TaiKhoan WHERE TaiKhoan = @tk AND Email = @em";
            SqlCommand cmd = new SqlCommand(sql, function.conn);
            cmd.Parameters.AddWithValue("@tk", tenTK);
            cmd.Parameters.AddWithValue("@em", email);
            int count = (int)cmd.ExecuteScalar();

            if (count == 0)
            {
                MessageBox.Show("Không tìm thấy tài khoản hoặc email!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Tạo mật khẩu mới
            string newPass = TaoMatKhauMoi();

            // Cập nhật mật khẩu vào DB
            string updateSql = "UPDATE TaiKhoan SET Pass = @newPass WHERE TaiKhoan = @tk";
            SqlCommand updateCmd = new SqlCommand(updateSql, function.conn);
            updateCmd.Parameters.AddWithValue("@newPass", newPass);
            updateCmd.Parameters.AddWithValue("@tk", tenTK);
            updateCmd.ExecuteNonQuery();

            // Gửi email
            try
            {
                GuiEmail(email, "Khôi phục mật khẩu", $"Mật khẩu mới của bạn là: {newPass}");
                MessageBox.Show("Mật khẩu mới đã được gửi tới email của bạn!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Hide();
                dangnhap dangnhap = new dangnhap();
                dangnhap.FormClosed += (s, args) => this.Close(); // Khi form trang chủ đóng thì thoát luôn app
                dangnhap.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể gửi email: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private string TaoMatKhauMoi()
        {
            string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random rand = new Random();
            char[] result = new char[8];

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = chars[rand.Next(chars.Length)];
            }

            return new string(result);
        }

        private void GuiEmail(string toEmail, string subject, string body)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("huyenjuly2508@gmail.com"); // đổi thành email gửi
            mail.To.Add(toEmail);
            mail.Subject = subject;
            mail.Body = body;

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential("huyenjuly2508@gmail.com", "okho btkr zdde ywsy");
            smtp.EnableSsl = true;

            smtp.Send(mail);
        }
    }
}
