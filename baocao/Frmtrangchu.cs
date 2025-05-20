using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Access;

namespace baocao
{
    public partial class Frmtrangchu: System.Windows.Forms.Form
    {
        public Frmtrangchu()
        {
            InitializeComponent();

        }

        private void Frmtrangchu_Load(object sender, EventArgs e)
        {
            function.Connect();
        }

        private void sảnPhẩmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSanPham a = new FrmSanPham();
            a.Show();
        }

        private void nhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NhanVien a = new NhanVien();
            a.Show();
        }

        private void kháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KhachHang a = new KhachHang();
            a.Show();
        }

        private void hóaĐơnBánHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frmquanlyhoadonban a = new Frmquanlyhoadonban();
            a.Show();
        }

        private void hóaĐơnNhậpHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HoaDonNhap a = new HoaDonNhap();
            a.Show();
        }

        private void tìmKiếmToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            TimkiemHDN a = new TimkiemHDN();
            a.Show();
        }

        private void báoCáoHiệuSuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hieusuat a = new Hieusuat();
            a.Show();
        }

        private void báoCáoBánToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Banhang a = new Banhang();
            a.Show();
        }

        private void báoCáoDoanhThuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Doanhthu a = new Doanhthu();
            a.Show();
        
    }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn đăng xuất không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void tìmKiếmHóaĐơnBánToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void tìmKiếmSảnPhẩmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Tksanpham a = new Tksanpham();
            a.Show();
        }
    }
 }

