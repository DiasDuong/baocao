using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace baocao
{
    public partial class FrmMainModern : Form
    {
        // Định nghĩa màu sắc chung
        private Color primaryColor = Color.White;    // Màu nền trắng
        private Color hoverColor = Color.FromArgb(255, 106, 0);      // Màu cam khi hover
        private Color selectedColor = Color.FromArgb(255, 106, 0);   // Màu cam khi được chọn
        private Color textColor = Color.FromArgb(51, 51, 51);        // Màu chữ đen đậm

        private Panel leftMenuPanel;
        private Panel contentPanel;
        private Label lblTitle;
        
        // Khai báo các panel menu
        private Panel pnlSanPham;
        private Panel pnlNhanVien;
        private Panel pnlKhachHang;
        private Panel pnlNhaCungCap;
        private Panel pnlHoaDonNhap;
        private Panel pnlDanhSachHoaDon;
        private Panel pnlTaoHoaDon;
        private Panel pnlHoaDonBan;
        private Panel pnlDanhSachHoaDonBan;
        private Panel pnlTaoHoaDonBan;
        private Panel pnlBaoCao;
        private Panel pnlKinhDoanh;
        private Panel pnlNhanVienBaoCao;
        private Panel pnlSanPhamBaoCao;

        private Panel currentSelectedPanel = null;

        public FrmMainModern()
        {
            InitializeComponent();
            InitializeCustomComponents();
        }

        private void InitializeCustomComponents()
        {
            // Khởi tạo main panels
            leftMenuPanel = new Panel
            {
                Dock = DockStyle.Left,
                Width = 280,
                BackColor = primaryColor,
                AutoScroll = true,
                Padding = new Padding(0, 10, 0, 0)  // Thêm padding top để tạo khoảng cách
            };

            contentPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(10)  // Thêm padding cho panel nội dung
            };

            // Thêm panel để tạo khoảng trống giữa menu và nội dung
            Panel spacerPanel = new Panel
            {
                Dock = DockStyle.Left,
                Width = 1,
                BackColor = Color.FromArgb(224, 224, 224)  // Màu xám nhạt cho đường phân cách
            };

            // Logo panel ở đầu menu trái
            Panel logoPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 70,
                BackColor = primaryColor,
                Padding = new Padding(10)
            };

            // Tạo một panel container cho logo và text
            Panel containerPanel = new Panel
            {
                Width = 220,
                Height = 70,
                BackColor = Color.Transparent,
                Location = new Point(10, 10)
            };

            Label nameLabel = new Label
            {
                Text = "Boutique.com",
                Font = new Font("Arial", 32, FontStyle.Bold),
                ForeColor = Color.FromArgb(255, 106, 0),
                Location = new Point(10, 10),
                AutoSize = true
            };

            Label dotComLabel = new Label
            {
               
            };

            containerPanel.Controls.Add(nameLabel);
            containerPanel.Controls.Add(dotComLabel);
            logoPanel.Controls.Add(containerPanel);
            leftMenuPanel.Controls.Add(logoPanel);

            // Thêm các menu items với khoảng cách mới
            int panelHeight = 45;  // Giảm chiều cao mỗi item
            int currentY = 100;    // Bắt đầu ngay sau logo panel

            CreateAndAddMenuPanel("Sản phẩm", currentY); currentY += panelHeight;
            CreateAndAddMenuPanel("Nhân viên", currentY); currentY += panelHeight;
            CreateAndAddMenuPanel("Khách hàng", currentY); currentY += panelHeight;
            CreateAndAddMenuPanel("Hóa đơn bán", currentY); currentY += panelHeight;
            CreateAndAddMenuPanel("Hóa đơn nhập", currentY); currentY += panelHeight;
            CreateAndAddMenuPanel("Tìm kiếm hóa đơn nhập", currentY); currentY += panelHeight; 
            CreateAndAddMenuPanel("Tìm kiếm hóa đơn bán", currentY); currentY += panelHeight;
            CreateAndAddMenuPanel("Tìm kiếm sản phẩm", currentY); currentY += panelHeight;
            CreateAndAddMenuPanel("Báo cáo kinh doanh", currentY); currentY += panelHeight;
            CreateAndAddMenuPanel("Báo cáo hiệu suất", currentY); currentY += panelHeight;
            CreateAndAddMenuPanel("Báo cáo doanh thu", currentY); currentY += panelHeight;
            CreateAndAddMenuPanel("Đăng xuất", currentY);

            // Thêm panels vào form
            this.Controls.Add(contentPanel);
            this.Controls.Add(spacerPanel);
            this.Controls.Add(leftMenuPanel);

            // Cài đặt form
            this.WindowState = FormWindowState.Maximized;
            this.MinimumSize = new Size(800, 600);
        }

        private Panel CreateAndAddMenuPanel(string text, int yPosition)
        {
            Panel menuPanel = new Panel
            {
                Size = new Size(260, 45),  // Tăng chiều cao lên một chút
                Location = new Point(10, yPosition),
                BackColor = primaryColor,
                Margin = new Padding(5),
                BorderStyle = BorderStyle.Fixed3D  // Thêm viền 3D
            };

            Label lblMenu = new Label
            {
                Text = text,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = textColor,
                AutoSize = false,  // Tắt AutoSize để có thể căn giữa
                Size = new Size(260, 45),  // Kích thước bằng với panel
                TextAlign = ContentAlignment.MiddleCenter,  // Căn giữa text
                Dock = DockStyle.Fill  // Lấp đầy panel
            };

            menuPanel.Controls.Add(lblMenu);
            
            // Thêm sự kiện click và hover
            menuPanel.Click += MenuPanel_Click;
            menuPanel.MouseEnter += (s, e) => {
                if (menuPanel != currentSelectedPanel)
                {
                    menuPanel.BackColor = Color.FromArgb(245, 245, 245);
                    lblMenu.ForeColor = hoverColor;
                }
            };
            menuPanel.MouseLeave += (s, e) => {
                if (menuPanel != currentSelectedPanel)
                {
                    menuPanel.BackColor = primaryColor;
                    lblMenu.ForeColor = textColor;
                }
            };

            // Làm cho label cũng nhận sự kiện click và hover
            lblMenu.Click += (s, e) => MenuPanel_Click(menuPanel, e);
            lblMenu.MouseEnter += (s, e) => {
                if (menuPanel != currentSelectedPanel)
                {
                    menuPanel.BackColor = Color.FromArgb(245, 245, 245);
                    lblMenu.ForeColor = hoverColor;
                }
            };
            lblMenu.MouseLeave += (s, e) => {
                if (menuPanel != currentSelectedPanel)
                {
                    menuPanel.BackColor = primaryColor;
                    lblMenu.ForeColor = textColor;
                }
            };

            leftMenuPanel.Controls.Add(menuPanel);
            return menuPanel;
        }

        [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
        );

        [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "DeleteObject")]
        private static extern bool DeleteObject(IntPtr hObject);

        private void MenuPanel_Click(object sender, EventArgs e)
        {
            Panel clickedPanel = sender as Panel;
            if (clickedPanel != null)
            {
                // Reset màu panel được chọn trước đó
                if (currentSelectedPanel != null)
                {
                    currentSelectedPanel.BackColor = primaryColor;
                    if (currentSelectedPanel.Controls.Count > 0 && currentSelectedPanel.Controls[0] is Label)
                    {
                        (currentSelectedPanel.Controls[0] as Label).ForeColor = textColor;
                    }
                }

                // Đặt màu cho panel mới được chọn
                currentSelectedPanel = clickedPanel;
                clickedPanel.BackColor = Color.FromArgb(245, 245, 245);  // Màu nền khi được chọn
                if (clickedPanel.Controls.Count > 0 && clickedPanel.Controls[0] is Label)
                {
                    (clickedPanel.Controls[0] as Label).ForeColor = selectedColor;
                }

                string menuText = (clickedPanel.Controls[0] as Label)?.Text;
                switch (menuText)
                {
                    case "Sản phẩm":
                        OpenForm(new FrmSanPham());
                        break;
                    case "Nhân viên":
                        OpenForm(new NhanVien());
                        break;
                    case "Khách hàng":
                        OpenForm(new KhachHang());
                        break;
                    case "Hóa đơn bán":
                        OpenForm(new Frmquanlyhoadonban());
                        break;
                    case "Hóa đơn nhập":
                        OpenForm(new HoaDonNhap());
                        break;
                    case "Báo cáo kinh doanh":
                        OpenForm(new Banhang());
                        break;
                    case "Báo cáo hiệu suất":
                        OpenForm(new Hieusuat());
                        break;
                    case "Báo cáo doanh thu":
                        OpenForm(new Doanhthu());
                        break;
                    case "Tìm kiếm hóa đơn nhập":
                        OpenForm(new TimkiemHDN());
                        break;
                    case "Tìm kiếm hóa đơn bán":
                        OpenForm(new TimKiemHDB());
                        break;
                    case "Tìm kiếm sản phẩm":
                        OpenForm(new Tksanpham());
                        break;
                    case "Đăng xuất":
                        if (MessageBox.Show("Bạn có muốn đăng xuất không?", "Thông báo", 
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            this.Close();
                        }
                        break;
                }
            }
        }

        private Form activeForm = null;
        private void OpenForm(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            contentPanel.Controls.Add(childForm);
            contentPanel.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void FrmMainModern_Load(object sender, EventArgs e)
        {
            function.Connect();
        }
    }
} 