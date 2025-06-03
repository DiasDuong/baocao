using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;


namespace baocao
{
    public partial class Doanhthu : Form
    {
        public Doanhthu()
        {
            InitializeComponent();
        }

        private void ClearDataGridView()
        {
            dataGridView.DataSource = null;
            dataGridView.Rows.Clear();
            dataGridView.Columns.Clear();
        }
        private void ExportExcelPreview()
        {
            try
            {
                Excel.Application application = new Excel.Application();
                application.Workbooks.Add(Type.Missing);

                Excel.Worksheet worksheet = (Excel.Worksheet)application.ActiveSheet;
                worksheet.Name = "Báo Cáo Doanh Thu";

                decimal totalRevenue = 0;
                for (int i = 0; i < dataGridView.Rows.Count; i++)
                {
                    totalRevenue += Convert.ToDecimal(dataGridView.Rows[i].Cells[3].Value);
                }

                worksheet.Cells[1, 1] = "BÁO CÁO DOANH THU";
                worksheet.Cells[2, 1] = "Ngày lập báo cáo: " + DateTime.Now.ToString("dd/MM/yyyy");
                worksheet.Cells[3, 1] = "Tổng doanh thu: " + totalRevenue.ToString("C0");

                Excel.Range titleRange = worksheet.Range["A1:C1"];
                titleRange.Merge();
                titleRange.Font.Size = 16;
                titleRange.Font.Bold = true;
                titleRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                for (int i = 0; i < dataGridView.Columns.Count; i++)
                {
                    worksheet.Cells[5, i + 1] = dataGridView.Columns[i].HeaderText;
                    worksheet.Cells[5, i + 1].Font.Bold = true;
                    worksheet.Cells[5, i + 1].Interior.Color = ColorTranslator.ToOle(Color.LightGray);
                }

                for (int i = 0; i < dataGridView.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridView.Columns.Count; j++)
                    {
                        worksheet.Cells[i + 6, j + 1] = dataGridView.Rows[i].Cells[j].Value;
                    }
                }

                Excel.Range revenueRange = worksheet.Range["D6:D" + (dataGridView.Rows.Count + 5)];
                revenueRange.NumberFormat = "#,##0";
                application.Columns.AutoFit();

                // Không lưu, chỉ hiển thị Excel để xem
                application.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tạo báo cáo: " + ex.Message);
            }
        }
   

        private void Validate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == (char)Keys.Back)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
          ExportExcelPreview();

        }

        private void rdoKhoang_CheckedChanged_1(object sender, EventArgs e)
        {
            mskNgay.Enabled = false;
            mskNgay.Text = "";
            mskTu.Enabled = true;
            mskDen.Enabled = true;
            txtdoanhthu.Text = "";
            ClearDataGridView();
        }

        private void rdoNgay_CheckedChanged(object sender, EventArgs e)
        {
            mskNgay.Enabled = true;
            mskTu.Enabled = false;
            mskDen.Enabled = false;
            mskTu.Text = "";
            mskDen.Text = "";
            txtdoanhthu.Text = "";
            ClearDataGridView();
        }

        private void Doanhthu_Load(object sender, EventArgs e)
        {
            btnXem.Enabled = true;
            btnIn.Enabled = true;
            btnThoat.Enabled = true;
            txtdoanhthu.Enabled = true;
            rdoNgay.Checked = false;
            rdoKhoang.Checked = false;
            dataGridView.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 7.8F, FontStyle.Regular);
        }

        private void btnXem_Click_1(object sender, EventArgs e)
        {

            string connString = "Data Source=DESKTOP-36UK9PH\\LOCALHOST;Initial Catalog=qlcuahangquanao;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

            string sql = "";

            if (rdoNgay.Checked)
            {
                DateTime selectedDate;
                if (DateTime.TryParse(mskNgay.Text, out selectedDate))
                {
                    sql = $"SELECT sp.MaQuanAo, sp.TenQuanAo, SUM(ct.SoLuong) AS soluongbanra, SUM(ct.ThanhTien) AS doanhthu " +
                      $"FROM HoaDonBan hdb " +
                      $"JOIN ChiTietHDBan ct ON hdb.SoHDB = ct.SoHDB " +
                      $"JOIN SanPham sp ON sp.MaQuanAo = ct.MaQuanAo " +
                      $"WHERE hdb.NgayBan = '{selectedDate.ToString("yyyy-MM-dd")}' " +
                      $"GROUP BY sp.MaQuanAo, sp.TenQuanAo, hdb.NgayBan " +
                      $"ORDER BY hdb.NgayBan, sp.MaQuanAo";

                }
                else
                {
                    MessageBox.Show("Ngày không hợp lệ, bạn vui lòng nhập lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else if (rdoKhoang.Checked)
            {
                mskNgay.Enabled = false;
                DateTime fromDate, toDate;
                if (DateTime.TryParse(mskTu.Text, out fromDate) && DateTime.TryParse(mskDen.Text, out toDate))
                {


                    sql = $"SELECT sp.MaQuanAo,sp.TenQuanAo,SUM(ct.SoLuong) AS soluongbanra,SUM(ct.ThanhTien) AS doanhthu  FROM  HoaDonban hdb  " +
                    $"JOIN    ChiTietHDBan ct ON hdb.SoHDB = ct.SoHDB " +
                    $"JOIN    SanPham sp ON sp.MaQuanAo = ct.MaQuanAo   " +
                    $"WHERE    hdb.NgayBan BETWEEN '{fromDate.ToString("yyyy-MM-dd")}' AND '{toDate.ToString("yyyy-MM-dd")}'   " +
                    $"GROUP BY    sp.MaQuanAo, sp.TenQuanAo  ORDER BY  sp.MaQuanAo";
                }
                else
                {
                    MessageBox.Show("Ngày không hợp lệ, bạn vui lòng nhập lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một tùy chọn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //Đổ dữ liệu vào DataGridView
            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable table = new DataTable();
                adapter.Fill(table);
                dataGridView.DataSource = table;
            dataGridView.DataSource = table;
                dataGridView.Columns[0].HeaderText = "Mã quần áo";
                dataGridView.Columns[1].HeaderText = "Tên quần áo";
                dataGridView.Columns[2].HeaderText = "Số lượng bán ra";
                dataGridView.Columns[3].HeaderText = "Doanh thu";
                dataGridView.Columns[0].Width = 180;
                dataGridView.Columns[1].Width = 300;
                dataGridView.Columns[2].Width = 180;
                dataGridView.Columns[3].Width = 200;

                decimal totalRevenue = 0;
                foreach (DataRow row in table.Rows)
                {
                    totalRevenue += Convert.ToDecimal(row["doanhthu"]);
                }

                // Hiển thị tổng doanh thu
                txtdoanhthu.Enabled = true;
                NumberFormatInfo nfi = new CultureInfo("vi-VN", false).NumberFormat;
                nfi.CurrencySymbol = "₫";
                txtdoanhthu.Text = totalRevenue.ToString("C", nfi);

            }

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
     "Bạn có chắc muốn thoát không?",
     "Xác nhận thoát",
     MessageBoxButtons.YesNo,
     MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Close(); // Đóng form hiện tại  
                              // Hoặc Application.Exit(); để thoát toàn ứng dụng  
            }
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }

}
