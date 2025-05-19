using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Data;
using System.Linq;

namespace baocao
{
    public partial class Frmmainmodern2 : Form
    {
        private Color menuBackColor = Color.FromArgb(35, 97, 146); // Xanh biển
        private Color buttonColor = Color.FromArgb(65, 135, 200);  // Xanh dương nhạt
        private Color buttonTextColor = Color.White;

        private Panel leftMenuPanel;
        private Panel contentPanel;
        private Form activeForm;

        // Tạo class để vẽ nút bo tròn
        public class RoundButton : Button
        {
            private int radius = 15;
            public RoundButton()
            {
                this.FlatStyle = FlatStyle.Flat;
                this.FlatAppearance.BorderSize = 0;
                this.BackColor = Color.FromArgb(65, 135, 200);
                this.ForeColor = Color.White;
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);
                Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
                GraphicsPath graphPath = new GraphicsPath();
                graphPath.AddArc(rect.X, rect.Y, radius * 2, radius * 2, 180, 90);
                graphPath.AddArc(rect.X + rect.Width - radius * 2, rect.Y, radius * 2, radius * 2, 270, 90);
                graphPath.AddArc(rect.X + rect.Width - radius * 2, rect.Y + rect.Height - radius * 2, radius * 2, radius * 2, 0, 90);
                graphPath.AddArc(rect.X, rect.Y + rect.Height - radius * 2, radius * 2, radius * 2, 90, 90);
                this.Region = new Region(graphPath);
            }
        }

        public Frmmainmodern2()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
            InitMainForm();
            AddCloseButton(); // Thêm nút đóng góc phải
        }

        private void AddCloseButton()
        {
            Button closeButton = new Button
            {
                Text = "✕",
                Size = new Size(45, 30),
                Location = new Point(this.Width - 45, 0),
                BackColor = Color.Transparent,
                ForeColor = Color.FromArgb(35, 97, 146),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Arial", 12, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            closeButton.FlatAppearance.BorderSize = 0;

            // Hiệu ứng hover
            closeButton.MouseEnter += (s, e) => {
                closeButton.BackColor = Color.FromArgb(192, 0, 0);
                closeButton.ForeColor = Color.White;
            };
            closeButton.MouseLeave += (s, e) => {
                closeButton.BackColor = Color.Transparent;
                closeButton.ForeColor = Color.FromArgb(35, 97, 146);
            };

            // Sự kiện click
            closeButton.Click += (s, e) => {
                if (MessageBox.Show("Bạn có muốn thoát không?", "Thông báo", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Application.Exit();
                }
            };

            this.Controls.Add(closeButton);
            closeButton.BringToFront();
        }

        private void Frmmainmodern2_Load(object sender, EventArgs e)
        {
            // Kết nối database khi form load
            function.Connect();
        }

        private void InitMainForm()
        {
            // Khởi tạo panel chứa nội dung
            contentPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(240, 245, 249)
            };
            this.Controls.Add(contentPanel);

            // Khởi tạo menu
            InitSidebarMenu();
        }

        private void InitSidebarMenu()
        {
            // Panel chứa menu trái
            leftMenuPanel = new Panel
            {
                Size = new Size(200, this.Height),
                BackColor = menuBackColor,
                Dock = DockStyle.Left,
                AutoScroll = true
            };
            this.Controls.Add(leftMenuPanel);

            int y = 20;

            // Logo
            Label logoLabel = new Label
            {
                Text = "QUẢN LÝ BÁN HÀNG",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(20, y),
                AutoSize = true
            };
            leftMenuPanel.Controls.Add(logoLabel);
            y += 50;

            // Quản lý chung
            CreateSectionLabel("Quản lý chung", ref y);
            CreateMenuButton("Sản phẩm", "FrmSanPham", ref y);
            CreateMenuButton("Nhân viên", "NhanVien", ref y);
            CreateMenuButton("Khách hàng", "KhachHang", ref y);

            // Hóa đơn
            CreateSectionLabel("Quản lý hóa đơn", ref y);
            CreateMenuButton("Hóa đơn bán", "Frmquanlyhoadonban", ref y);
            CreateMenuButton("Hóa đơn nhập", "HoaDonNhap", ref y);

            // Tìm kiếm
            CreateSectionLabel("Tìm kiếm", ref y);
            CreateMenuButton("Tìm kiếm HĐ bán", "TimKiemHDB", ref y);
            CreateMenuButton("Tìm kiếm HĐ nhập", "TimkiemHDN", ref y);
            CreateMenuButton("Tìm kiếm sản phẩm", "Tksanpham", ref y);

            // Báo cáo
            CreateSectionLabel("Báo cáo", ref y);
            CreateMenuButton("Doanh thu", "Doanhthu", ref y);
            CreateMenuButton("Hiệu suất", "Hieusuat", ref y);
            CreateMenuButton("Bán hàng", "Banhang", ref y);

            // Nút Thoát cuối
            RoundButton exitButton = new RoundButton
            {
                Text = "Thoát",
                Size = new Size(160, 35),
                Location = new Point(20, this.Height - 80),
                BackColor = Color.FromArgb(35, 97, 146),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left
            };

            // Hiệu ứng hover
            exitButton.MouseEnter += (s, e) => {
                exitButton.BackColor = Color.FromArgb(65, 135, 200);
            };
            exitButton.MouseLeave += (s, e) => {
                exitButton.BackColor = Color.FromArgb(35, 97, 146);
            };

            exitButton.Click += (s, e) => {
                if (MessageBox.Show("Bạn có muốn thoát không?", "Thông báo", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Application.Exit();
                }
            };

            leftMenuPanel.Controls.Add(exitButton);
        }

        private void CreateSectionLabel(string text, ref int y)
        {
            Label sectionLabel = new Label
            {
                Text = text,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(20, y),
                AutoSize = true
            };
            leftMenuPanel.Controls.Add(sectionLabel);
            y += 25;
        }

        private void CreateMenuButton(string text, string formName, ref int y)
        {
            RoundButton menuButton = new RoundButton
            {
                Text = text,
                Size = new Size(160, 35),
                Location = new Point(20, y),
                BackColor = buttonColor,
                ForeColor = buttonTextColor,
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                Tag = formName
            };

            // Hover hiệu ứng
            menuButton.MouseEnter += (s, e) => menuButton.BackColor = Color.FromArgb(90, 160, 230);
            menuButton.MouseLeave += (s, e) => menuButton.BackColor = buttonColor;

            // Click event
            menuButton.Click += (s, e) => OpenChildForm(formName);

            leftMenuPanel.Controls.Add(menuButton);
            y += 40;
        }

        private void OpenChildForm(string formName)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }

            Form childForm = null;
            switch (formName)
            {
                case "FrmSanPham":
                    childForm = new FrmSanPham();
                    break;
                case "NhanVien":
                    childForm = new NhanVien();
                    break;
                case "KhachHang":
                    childForm = new KhachHang();
                    break;
                case "Frmquanlyhoadonban":
                    childForm = new Frmquanlyhoadonban();
                    break;
                case "HoaDonNhap":
                    childForm = new HoaDonNhap();
                    break;
                case "TimKiemHDB":
                    childForm = new TimKiemHDB();
                    break;
                case "TimkiemHDN":
                    childForm = new TimkiemHDN();
                    break;
                case "Tksanpham":
                    childForm = new Tksanpham();
                    break;
                case "Doanhthu":
                    childForm = new Doanhthu();
                    break;
                case "Hieusuat":
                    childForm = new Hieusuat();
                    break;
                case "Banhang":
                    childForm = new Banhang();
                    break;
            }

            if (childForm != null)
            {
                activeForm = childForm;
                childForm.TopLevel = false;
                childForm.FormBorderStyle = FormBorderStyle.None;
                childForm.Dock = DockStyle.Fill;
                contentPanel.Controls.Add(childForm);
                contentPanel.Tag = childForm;
                childForm.BringToFront();
                childForm.Show();
            }
        }
    }
}
