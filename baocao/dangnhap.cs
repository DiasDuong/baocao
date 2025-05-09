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
    public partial class dangnhap : Form
    {
        public dangnhap()
        {
            InitializeComponent();
        }

        private void dangnhap_Load(object sender, EventArgs e)
        {

        }
        public static class DatabaseHelper
        {
            private static string connectionString = @"Data Source=DESKTOP-6PT6RNN;Initial Catalog=quanaonet;Integrated Security=True;Encrypt=False";

            public static SqlConnection GetConnection()
            {
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                return conn;
            }
        }
        private void btndn_Click(object sender, EventArgs e)
        {
            string username = txttendn.Text.Trim();
            string password = txtmk.Text.Trim();

            if (username == "" || password == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }

            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                string query = "SELECT COUNT(*) FROM TaiKhoan WHERE TaiKhoan = @tk AND Pass = @pw"; 
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@tk", username);
                    cmd.Parameters.AddWithValue("@pw", password);

                    int count = (int)cmd.ExecuteScalar();
                    if (count > 0)
                    {
                        MessageBox.Show("Đăng nhập thành công!");
                        // Chuyển sang form chính
                    }
                    else
                    {
                        MessageBox.Show("Tên đăng nhập hoặc mật khẩu sai!");
                    }
                }
            }

        }

        private void linkLabel1_Click(object sender, EventArgs e)
        {
            dangky dangKyForm = new dangky();
            dangKyForm.Show();
        }
    }

}
