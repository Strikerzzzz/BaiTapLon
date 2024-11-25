using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BaiTapLon.View.TimKiem
{
    public partial class TKLopHoc : Form
    {
        public TKLopHoc()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoadDatabaseWithParams(string sql, SqlParameter[] parameters)
        {
            try
            {
                this.dataGridView1.DataSource = DataBase.GetData(sql, parameters);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadDatabase()
        {
            try
            {
                dataGridView1.DataSource = DataBase.GetData("SELECT lh.MaLop, lh.TenLop, lh.KhoaHoc, lh.SoSVMax, mh.TenMon, CONCAT(hk.TenHocKy, N' - Năm ', hk.Nam) AS hocky, lh.NgayKetThuc " +
                                                            "FROM LopHoc lh LEFT JOIN MonHoc mh ON lh.MaMon = mh.MaMon LEFT JOIN HocKy hk ON lh.IDHocKy = hk.IDHocKy WHERE lh.TrangThai = 'Initialize' AND mh.TrangThai = 'Initialize' AND hk.TrangThai = 'Initialize'");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TKLopHoc_Load(object sender, EventArgs e)
        {
            cboLopHoc.Items.Add("Vui lòng chọn lựa chọn của bạn");
            cboLopHoc.Items.Add("Mã lớp");
            cboLopHoc.Items.Add("Tên lớp");
            cboLopHoc.Items.Add("Khóa học");
            cboLopHoc.Items.Add("Số SV tối đa");
            cboLopHoc.Items.Add("Tên môn");
            cboLopHoc.Items.Add("Học kỳ");
            cboLopHoc.Items.Add("Ngày kết thúc");
            cboLopHoc.SelectedIndex = 0;
            LoadDatabase();
        }

        private void btnTK_Click(object sender, EventArgs e)
        {
            if (cboLopHoc.SelectedIndex == 0)
            {
                MessageBox.Show("Vui lòng chọn tiêu chí tìm kiếm.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string value = txtTK.Text.Trim();
            string selected = "";

            if (cboLopHoc.SelectedItem?.ToString() == "Vui lòng chọn lựa chọn của bạn")
            {
                MessageBox.Show("Vui lòng chọn cột để tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            switch (cboLopHoc.SelectedItem?.ToString())
            {
                case "Mã lớp": selected = "lh.MaLop"; break;
                case "Tên lớp": selected = "lh.TenLop"; break;
                case "Khóa học": selected = "lh.KhoaHoc"; break;
                case "Số SV tối đa": selected = "lh.SoSVMax"; break;
                case "Tên môn": selected = "mh.TenMon"; break;
                case "Học kỳ": selected = "hk.TenHocKy"; break;
                case "Ngày kết thúc": selected = "lh.NgayKetThuc"; break;
                default:
                    MessageBox.Show("Vui lòng chọn cột để tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
            }

            if (!string.IsNullOrEmpty(value))
            {
                string query = $@"SELECT lh.MaLop, lh.TenLop, lh.KhoaHoc, lh.SoSVMax, mh.TenMon, 
                                         CONCAT(hk.TenHocKy, N' - Năm ', hk.Nam) AS HocKy, lh.NgayKetThuc
                                  FROM LopHoc lh
                                  LEFT JOIN MonHoc mh ON lh.MaMon = mh.MaMon
                                  LEFT JOIN HocKy hk ON lh.IDHocKy = hk.IDHocKy
                                  WHERE lh.TrangThai = 'Initialize' 
                                    AND mh.TrangThai = 'Initialize' 
                                    AND hk.TrangThai = 'Initialize' 
                                    AND {selected} LIKE @value";

                SqlParameter[] parameters = {
                    new SqlParameter("@value", $"%{value}%")
                };

                LoadDatabaseWithParams(query, parameters);
            }
            else
            {
                MessageBox.Show("Vui lòng nhập giá trị tìm kiếm!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
