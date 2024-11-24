using BaiTapLon.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaiTapLon.View.TimKiem
{
    public partial class TKLopHocSinhVien : Form
    {
        private LopSinhVienController lopSinhVienController;
        public TKLopHocSinhVien()
        {
            InitializeComponent();
            lopSinhVienController = new LopSinhVienController();
        }

        private void TKLopHocSinhVien_Load(object sender, EventArgs e)
        {
            cboLuaChon.Items.AddRange(new string[]
           {
                "Chọn tiêu chí tìm kiếm",
                "Tên lớp",
                "Tên sinh viên",
                "Mã lớp học - sinh viên"
           });
            cboLuaChon.SelectedIndex = 0;
            LoadAllData();
        }
        private void LoadAllData()
        {
                try
                {
                    // Truy vấn lấy toàn bộ dữ liệu từ bảng LopHoc_SinhVien
                    string query = @"
            SELECT lhsv.ID, lh.TenLop, CONCAT(sv.HoDem, ' ', sv.Ten, '-', sv.MaSV) AS TenSinhVien
            FROM LopHoc_SinhVien lhsv
            LEFT JOIN LopHoc lh ON lh.MaLop = lhsv.MaLop
            LEFT JOIN SinhVien sv ON lhsv.MaSV = sv.MaSV";

                    DataTable dt = DataBase.GetData(query);

                    // Hiển thị dữ liệu lên DataGridView
                    dataGridView1.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (cboLuaChon.SelectedIndex == 0)
            {
                MessageBox.Show("Vui lòng chọn tiêu chí tìm kiếm.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string luaChon = cboLuaChon.Text; // Tiêu chí tìm kiếm
            string giaTri = txtGiaTri.Text.Trim(); // Giá trị tìm kiếm

            if (string.IsNullOrWhiteSpace(giaTri))
            {
                LoadAllData();
                return;
            }

            // Xây dựng câu lệnh SQL theo tiêu chí
            string query = @"
        SELECT lhsv.ID, lh.TenLop, CONCAT(sv.HoDem, ' ', sv.Ten, '-', sv.MaSV) AS TenSinhVien
        FROM LopHoc_SinhVien lhsv
        LEFT JOIN LopHoc lh ON lh.MaLop = lhsv.MaLop
        LEFT JOIN SinhVien sv ON lhsv.MaSV = sv.MaSV";

            switch (luaChon)
            {
                case "Tên lớp":
                    query += " WHERE lh.TenLop LIKE @SearchValue";
                    break;

                case "Tên sinh viên":
                    query += " WHERE CONCAT(sv.HoDem, ' ', sv.Ten, '-', sv.MaSV) LIKE @SearchValue";
                    break;

                case "Mã lớp học - sinh viên":
                    query += " WHERE CONVERT(lhsv.ID, NVARCHAR) LIKE @SearchValue";
                    break;

                default:
                    MessageBox.Show("Tiêu chí tìm kiếm không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
            }

            try
            {
                // Thực thi câu lệnh SQL với giá trị tìm kiếm
                DataTable dt = DataBase.GetData(query, new System.Data.SqlClient.SqlParameter[]
                {
            new System.Data.SqlClient.SqlParameter("@SearchValue", "%" + giaTri + "%")
                });

                // Hiển thị kết quả trên DataGridView
                dataGridView1.DataSource = dt;

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy kết quả phù hợp.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
