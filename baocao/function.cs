using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace baocao
{
    public class function
    {
        public static SqlConnection conn;  //Khai báo đối tượng kết nối
        public static string ConnectionString =


"Data Source=DESKTOP-6PT6RNN;Initial Catalog=quanaonet;Integrated Security=True;Encrypt=False";


        public static void Connect()
        {
            try
            {
                if (conn == null)
                    conn = new SqlConnection(ConnectionString);

                if (conn.State == ConnectionState.Closed)
                    conn.Open();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi kết nối Database: " + ex.Message);
            }
        }
        public static void Disconnect()
        {
            if (conn != null && conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        public static DataTable GetDataToTable(string sql)
        {
            DataTable table = new DataTable();
            try
            {
                using (SqlConnection tempConn = new SqlConnection(ConnectionString))
                {
                    tempConn.Open();
                    SqlDataAdapter mydata = new SqlDataAdapter(sql, tempConn);
                    mydata.SelectCommand.CommandTimeout = 60;
                    mydata.Fill(table);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lấy dữ liệu: " + ex.Message);
            }
            return table;
        }

        // Kiểm tra ID của bản ghi
        public static bool CheckID(string query)
        {
            bool result = false;
            try
            {
                using (SqlConnection tempConn = new SqlConnection(ConnectionString))
                {
                    tempConn.Open();
                    SqlDataAdapter data = new SqlDataAdapter(query, tempConn);
                    DataTable table = new DataTable();
                    data.Fill(table);
                    if (table.Rows.Count > 0)
                    {
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kiểm tra ID: " + ex.Message);
            }
            return result;
        }

        // Thực thi query SQL
        public static void RunSQL(string sql)
        {
            try
            {
                using (SqlConnection tempConn = new SqlConnection(ConnectionString))
                {
                    tempConn.Open();
                    SqlCommand cmd = new SqlCommand(sql, tempConn);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thực thi SQL: " + ex.Message);
            }
        }

        // Thực thi query SQL (xóa)
        public static void RunDeleteSQL(string sql)
        {
            try
            {
                using (SqlConnection tempConn = new SqlConnection(ConnectionString))
                {
                    tempConn.Open();
                    SqlCommand cmd = new SqlCommand(sql, tempConn);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xóa dữ liệu: " + ex.Message);
            }
        }

        // Lấy dữ liệu từ một query SQL
        public static string GetFieldValues(string sql)
        {
            string result = "";
            try
            {
                using (SqlConnection tempConn = new SqlConnection(ConnectionString))
                {
                    tempConn.Open();
                    SqlCommand cmd = new SqlCommand(sql, tempConn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        result = reader.GetValue(0).ToString();
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi truy vấn: " + ex.Message);
            }
            return result;
        }

        // Đổ dữ liệu vào ComboBox
        public static void FillCombo(string query, System.Windows.Forms.ComboBox comboBox, string value, string name)
        {
            try
            {
                using (SqlConnection tempConn = new SqlConnection(ConnectionString))
                {
                    tempConn.Open();
                    SqlDataAdapter data = new SqlDataAdapter(query, tempConn);
                    DataTable table = new DataTable();
                    data.Fill(table);

                    if (!table.Columns.Contains(value))
                    {
                        MessageBox.Show("Không tìm thấy cột '" + value + "' trong dữ liệu.");
                        return;
                    }

                    if (!table.Columns.Contains(name))
                    {
                        MessageBox.Show("Không tìm thấy cột '" + name + "' trong dữ liệu.");
                        return;
                    }

                    comboBox.DataSource = table;
                    comboBox.ValueMember = value;
                    comboBox.DisplayMember = name;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi đổ dữ liệu vào ComboBox: " + ex.Message);
            }
        }

        public static void FillCombo1(string query, System.Windows.Forms.ComboBox comboBox, string value, string name)
        {
            try
            {
                using (SqlConnection tempConn = new SqlConnection(ConnectionString))
                {
                    tempConn.Open();
                    SqlDataAdapter data = new SqlDataAdapter(query, tempConn);
                    DataTable table = new DataTable();
                    data.Fill(table);

                    comboBox.DataSource = table;
                    comboBox.ValueMember = value;
                    comboBox.DisplayMember = value;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi đổ dữ liệu vào ComboBox: " + ex.Message);
            }
        }
        // Đổ dữ liệu vào combo box với định dạng mã + tên
        /*public static void FillCombo1(string query, ComboBox comboBox, string value, string displayExpression)
        {
            // Create a SqlDataAdapter and DataTable
            SqlDataAdapter data = new SqlDataAdapter(query, conn);
            DataTable table = new DataTable();
            data.Fill(table);

            // Add a new "DisplayMember" column to the DataTable
            table.Columns.Add("DisplayMember", typeof(string)).Expression = displayExpression;

            // Bind the DataTable to the combobox
            comboBox.DataSource = table;

            // Set the ValueMember and DisplayMember properties
            comboBox.ValueMember = value;
            comboBox.DisplayMember = "DisplayMember";

            // Set the selected index to -1
            comboBox.SelectedIndex = -1;
        }*/
        // Hàm này tương tự như hàm FillCombo nhưng có thêm khả năng định dạng văn bản hiển thị trong combobox.

        // Hàm kiểm tra dữ liệu nhập vào có phải là date không
        public static bool IsDate(string date)
        {
            string[] parts = date.Split('/');
            if ((Convert.ToInt32(parts[0]) >= 1) && (Convert.ToInt32(parts[0]) <= 31) && (Convert.ToInt32(parts[1]) >= 1) && (Convert.ToInt32(parts[1]) <= 12) && (Convert.ToInt32(parts[2]) >= 1900))
                return true;
            else
                return false;
        }

        // Chuyển đổi định dạng ngày tháng năm từ dd/MM/yyyy thành MM/dd/yyyy
        public static string ConvertDateTime(string date)
        {
            string[] parts = date.Split('/');
            string dateTime = String.Format("{0}/{1}/{2}", parts[1], parts[0], parts[2]);
            return dateTime;
        }

        // Chuyển đổi giá tiền từ định dạng số sang định dạng chữ
        public static string ConvertNumericToText(string number)
        {
            int length, digit;
            string result = "";
            string[] numText;

            // Xóa các dấu "," nếu có
            number = number.Replace(",", "");
            numText = "không;một;hai;ba;bốn;năm;sáu;bảy;tám;chín".Split(';');
            length = number.Length - 1; // Trừ 1 vì thứ tự đi từ 0

            for (int i = 0; i <= length; i++)
            {
                digit = Convert.ToInt32(number.Substring(i, 1));
                result = result + " " + numText[digit];

                if (length == i) // Chữ số cuối cùng không cần xét tiếp
                    break;

                switch ((length - i) % 9)
                {
                    case 0:
                        result = result + " tỷ";
                        if (number.Substring(i + 1, 3) == "000")
                            i += 3;
                        if (number.Substring(i + 1, 3) == "000")
                            i += 3;
                        if (number.Substring(i + 1, 3) == "000")
                            i += 3;
                        break;
                    case 6:
                        result = result + " triệu";
                        if (number.Substring(i + 1, 3) == "000")
                            i += 3;
                        if (number.Substring(i + 1, 3) == "000")
                            i += 3;
                        break;
                    case 3:
                        result = result + " nghìn";
                        if (number.Substring(i + 1, 3) == "000")
                            i += 3;
                        break;
                    default:
                        switch ((length - i) % 3)
                        {
                            case 2:
                                result = result + " trăm";
                                break;
                            case 1:
                                result = result + " mươi";
                                break;
                        }

                        break;
                }
            }

            // Loại bỏ trường hợp x00
            result = result.Replace("không mươi không ", "");
            result = result.Replace("không mươi không", "");

            // Loại bỏ trường hợp 00x
            result = result.Replace("không mươi ", "linh ");

            // Loại bỏ trường hợp x0, x>=2
            result = result.Replace("mươi không", "mươi");

            // Fix trường hợp 10
            result = result.Replace("một mươi", "mười");

            // Fix trường hợp x4, x>=2
            result = result.Replace("mươi bốn", "mươi tư");

            // Fix trường hợp x04
            result = result.Replace("linh bốn", "linh tư");

            // Fix trường hợp x5, x>=2
            result = result.Replace("mươi năm", "mươi lăm");

            // Fix trường hợp x1, x>=2
            result = result.Replace("mươi một", "mươi mốt");

            // Fix trường hợp x15
            result = result.Replace("mười năm", "mười lăm");

            // Bỏ ký tự space
            result = result.Trim();

            // Viết hoa ký tự đầu tiên
            result = char.ToUpper(result[0]) + result.Substring(1) + " đồng";

            return result;
        }

        // Tạo ID dựa theo ngày và giờ cho params bất kì
        public static string CreateKey(string param)
        {
            string key = param;

            string[] dateParts;
            dateParts = DateTime.Now.ToShortDateString().Split('/');
            string date = String.Format("{0}{1}{2}", dateParts[0], dateParts[1], dateParts[2]);
            key = key + date;

            string[] timeParts;
            timeParts = DateTime.Now.ToLongTimeString().Split(':');
            if (timeParts[2].Substring(3, 2) == "PM")
                timeParts[0] = ConvertTimeTo24(timeParts[0]);
            if (timeParts[2].Substring(3, 2) == "AM")
                if (timeParts[0].Length == 1)
                    timeParts[0] = "0" + timeParts[0];
            // Xóa ký tự trắng và PM hoặc AM
            timeParts[2] = timeParts[2].Remove(2, 3);
            string time;
            time = String.Format("{0}{1}{2}", timeParts[0], timeParts[1], timeParts[2]);
            key = key + "_" + time;

            return key;
        }

        // Chuyển đổi thời gian từ 12h sang 24h
        public static string ConvertTimeTo24(string hour)
        {
            string hour24 = "";
            switch (hour)
            {
                case "1":
                    hour24 = "13";
                    break;
                case "2":
                    hour24 = "14";
                    break;
                case "3":
                    hour24 = "15";
                    break;
                case "4":
                    hour24 = "16";
                    break;
                case "5":
                    hour24 = "17";
                    break;
                case "6":
                    hour24 = "18";
                    break;
                case "7":
                    hour24 = "19";
                    break;
                case "8":
                    hour24 = "20";
                    break;
                case "9":
                    hour24 = "21";
                    break;
                case "10":
                    hour24 = "22";
                    break;
                case "11":
                    hour24 = "23";
                    break;
                case "12":
                    hour24 = "0";
                    break;
            }

            return hour24;
        }
        public static void Close()
        {
            try
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi đóng kết nối: " + ex.Message);
            }
        }

        public static DataTable LoadDataToTable(string sql)
        {
            Connect(); // Đảm bảo kết nối đã được mở
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
            adapter.SelectCommand.CommandTimeout = 120;
            try
            {
                adapter.Fill(dt); // Lấy dữ liệu vào DataTable
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi truy vấn dữ liệu: " + ex.Message);
            }
            Close(); // Đảm bảo đóng kết nối sau khi hoàn tất
            return dt;
        }

        public static string getSQLdateFromText(string dateDDMMYYYY)
        {
            DateTime dt;
            if (DateTime.TryParseExact(dateDDMMYYYY, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
            {
                return dt.ToString("yyyy-MM-dd"); // đúng chuẩn SQL
            }
            else
            {
                MessageBox.Show("Ngày sinh không hợp lệ. Định dạng yêu cầu: dd/MM/yyyy");
                return "1900-01-01"; // giá trị mặc định khi lỗi
            }
        }

        public static string ChuyenSoSangChu(string sNumber)
        {
            // Làm sạch chuỗi: bỏ dấu phẩy, chấm thập phân, và phần sau dấu chấm
            if (sNumber.Contains("."))
                sNumber = sNumber.Substring(0, sNumber.IndexOf("."));

            sNumber = sNumber.Replace(",", "").Trim();

            if (string.IsNullOrEmpty(sNumber) || !sNumber.All(char.IsDigit))
                throw new FormatException("Chuỗi không phải là một số hợp lệ: " + sNumber);

            int mLen, mDigit;
            string mTemp = "";
            string[] mNumText = "không;một;hai;ba;bốn;năm;sáu;bảy;tám;chín".Split(';');

            mLen = sNumber.Length - 1;

            for (int i = 0; i <= mLen; i++)
            {
                mDigit = Convert.ToInt32(sNumber.Substring(i, 1));
                mTemp = mTemp + " " + mNumText[mDigit];
                if (mLen == i) break;

                switch ((mLen - i) % 9)
                {
                    case 0:
                        mTemp += " tỷ";
                        break;
                    case 6:
                        mTemp += " triệu";
                        break;
                    case 3:
                        mTemp += " nghìn";
                        break;
                    default:
                        switch ((mLen - i) % 3)
                        {
                            case 2: mTemp += " trăm"; break;
                            case 1: mTemp += " mươi"; break;
                        }
                        break;
                }
            }

            mTemp = mTemp.Replace("không mươi không", "");
            mTemp = mTemp.Replace("không mươi", "linh");
            mTemp = mTemp.Replace("mươi không", "mươi");
            mTemp = mTemp.Replace("một mươi", "mười");
            mTemp = mTemp.Replace("mươi bốn", "mươi tư");
            mTemp = mTemp.Replace("linh bốn", "linh tư");
            mTemp = mTemp.Replace("mươi năm", "mươi lăm");
            mTemp = mTemp.Replace("mươi một", "mươi mốt");
            mTemp = mTemp.Replace("mười năm", "mười lăm");

            mTemp = mTemp.Trim();
            mTemp = mTemp.Substring(0, 1).ToUpper() + mTemp.Substring(1) + " đồng";

            return mTemp;
        }


        public static bool IsKeyExists(string sql)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
              
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.CommandTimeout = 30;
                    conn.Open(); // Mở kết nối
                    SqlDataReader reader = cmd.ExecuteReader();
                    bool exists = reader.HasRows;
                    reader.Close(); // Đóng reader
                    return exists;
                }
            } // using sẽ
        }
        public static string GetFieldValue(string sql)
        {
            SqlCommand cmd = new SqlCommand(sql, conn);

            // Kiểm tra xem kết nối có đang mở không trước khi mở lại
            if (conn.State != System.Data.ConnectionState.Open)
            {
                conn.Open();  // Mở kết nối
            }

            // Thực thi câu lệnh SQL và lấy giá trị đầu tiên của bản ghi đầu tiên
            object result = cmd.ExecuteScalar();

            // Đảm bảo đóng kết nối sau khi thực hiện
            if (conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();  // Đóng kết nối
            }

            // Nếu không có giá trị, trả về chuỗi rỗng
            return result != null ? result.ToString() : string.Empty;
        }
        public static bool CheckKey(string sql)
        {
            bool exists = false;
            try
            {
                using (SqlConnection tempConn = new SqlConnection(ConnectionString))
                {
                    tempConn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, tempConn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            exists = reader.HasRows;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi kiểm tra: " + ex.Message, "Lỗi");
            }
            return exists;
        }
        public static void FillCombo2(string sql, System.Windows.Forms.ComboBox cbo, string ma, string ten)
        {
            try
            {
                using (SqlConnection tempConn = new SqlConnection(ConnectionString))
                {
                    tempConn.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(sql, tempConn))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        cbo.DataSource = dt;
                        cbo.ValueMember = ma;
                        cbo.DisplayMember = ten;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi đổ dữ liệu vào ComboBox: " + ex.Message);
            }
        }
        
        public static string TaoMaHoaDonMoi()
        {
            try
            {
                string sql = "SELECT TOP 1 SoHDB FROM HoaDonBan ORDER BY SoHDB DESC";
                string maHoaDonCuoi = GetFieldValues(sql);
                
                if (string.IsNullOrEmpty(maHoaDonCuoi))
                {
                    // Nếu chưa có hóa đơn nào
                    return "HDB0000001";
                }
                
                // Lấy phần số từ mã hóa đơn
                string phanSo = maHoaDonCuoi.Substring(3);
                int soThuTu = int.Parse(phanSo) + 1;
                
                // Format lại mã hóa đơn mới với độ dài cố định 7 chữ số
                return $"HDB{soThuTu:D7}";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tạo mã hóa đơn: " + ex.Message);
                return "HDB0000001";
            }
        }
        public static string MaHoaDonNhapMoi()
        {
            string prefix = "HDN";
            string sql = "SELECT TOP 1 SoHDN FROM HoaDonNhap ORDER BY CAST(SUBSTRING(SoHDN, 4, LEN(SoHDN)) AS INT) DESC";
            string maHDMoi = "";
            using (SqlConnection conn = new SqlConnection(function.ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        string maHDcu = result.ToString();
                        // Lấy số cuối cùng, giả sử mã dạng HDB0001, HDB0002...
                        int so = 1;
                        string soStr = maHDcu.Substring(prefix.Length);
                        if (int.TryParse(soStr, out so))
                        {
                            so++;
                        }
                        maHDMoi = prefix + so.ToString("D4");
                    }
                    else
                    {
                        maHDMoi = prefix + "0001";
                    }
                }
            }
            return maHDMoi;
        }
      
        
    }
}
