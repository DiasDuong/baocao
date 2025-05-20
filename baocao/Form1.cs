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
    public partial class Form1: Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private Form currentFormChild;
        private void OpenChildForm(Form childForm)
        {
            if (currentFormChild != null)
            {
                currentFormChild.Close();
            }
            currentFormChild = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panel_body.Controls.Add(childForm);
            panel_body.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnsanpham_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmSanPham());
         
        }

        private void btnkhachhang_Click(object sender, EventArgs e)
        {
            OpenChildForm(new KhachHang());
        }

        private void btnnhanvien_Click(object sender, EventArgs e)
        {
            OpenChildForm(new NhanVien());
        }

        private void btnhoadonban_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Frmquanlyhoadonban());
        }

        private void btnhoadonnhap_Click(object sender, EventArgs e)
        {
            OpenChildForm(new HoaDonNhap());
        }

        private void btntimkiemhoadonban_Click(object sender, EventArgs e)
        {
            OpenChildForm(new TimkiemHDN());
        }

        private void btntimkiemsanpham_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Tksanpham());
        }

        private void btnbaocaobanhang_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Banhang());
        }

        private void btnbaocaodoanhthu_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Doanhthu());
        }

        private void btnbaocaohieusuat_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Hieusuat());
        }

        private void btnthoat_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
