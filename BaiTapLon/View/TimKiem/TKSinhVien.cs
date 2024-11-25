using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BaiTapLon.View.TimKiem
{
    public partial class TKSinhVien : Form
    {
        public TKSinhVien()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void LoadSinhVienData()
        {
            DataTable dtSinhVien = DataBase.GetData(@"SELECT MaSV, HoDem, Ten, Email, CCCD, SoDienThoai, GioiTinh, NgaySinh, DiaChi, 
                                             cn.TenChuyenNganh, KhoaHoc 
                                             FROM SinhVien sv 
                                             LEFT JOIN ChuyenNganh cn ON cn.MaChuyenNganh = sv.MaChuyenNganh 
                                             WHERE sv.TrangThai = 'Initialize' AND cn.TrangThai = 'Initialize'");

            dataGridViewQLSV.DataSource = dtSinhVien;
        }
        private void TKSinhVien_Load(object sender, EventArgs e)
        {
            cboSinhVien.Items.Add("Vui lòng chọn lựa chọn của bạn");
            cboSinhVien.Items.Add("Mã sinh viên");
            cboSinhVien.Items.Add("Họ đệm");
            cboSinhVien.Items.Add("Tên");
            cboSinhVien.Items.Add("Email");
            cboSinhVien.Items.Add("CCCD");
            cboSinhVien.Items.Add("Số điện thoại");
            cboSinhVien.Items.Add("Giới tính");
            cboSinhVien.Items.Add("Ngày sinh");
            cboSinhVien.Items.Add("Địa chỉ");
            cboSinhVien.Items.Add("Khóa học");
            cboSinhVien.Items.Add("Tên chuyên ngành");
            cboSinhVien.SelectedIndex = 0;
            LoadSinhVienData();
        }

        private void btnTK_Click(object sender, EventArgs e)
        {
            if (cboSinhVien.SelectedIndex == 0)
            {
                MessageBox.Show("Vui lòng chọn tiêu chí tìm kiếm.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string value = txtTK.Text.Trim(); 
            string selected = "";

            if (cboSinhVien.SelectedItem?.ToString() == "Vui lòng chọn lựa chọn của bạn")
            {
                MessageBox.Show("Vui lòng chọn cột để tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            switch (cboSinhVien.SelectedItem?.ToString())
            {
                case "Mã sinh viên":
                    selected = "MaSV";
                    break;
                case "Họ đệm":
                    selected = "HoDem"; 
                    break;
                case "Tên": 
                    selected = "Ten"; 
                    break;
                case "Email": 
                    selected = "Email";
                    break;
                case "CCCD": 
                    selected = "CCCD";
                    break;
                case "Số điện thoại":
                    selected = "SoDienThoai";
                    break;
                case "Giới tính":
                    selected = "GioiTinh";
                    break;
                case "Ngày sinh":
                    selected = "NgaySinh";
                    break;
                case "Địa chỉ":
                    selected = "DiaChi";
                    break;
                case "Khóa học":
                    selected = "KhoaHoc";
                    break;
                case "Tên chuyên ngành": 
                    selected = "cn.TenChuyenNganh";
                    break;
                default:
                    MessageBox.Show("Vui lòng chọn cột để tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
            }

            if (!string.IsNullOrEmpty(value))
            {
                string query = $@"SELECT MaSV, HoDem, Ten, Email, CCCD, SoDienThoai, GioiTinh, NgaySinh, DiaChi, 
                                 cn.TenChuyenNganh, KhoaHoc 
                                 FROM SinhVien sv
                                 LEFT JOIN ChuyenNganh cn ON sv.MaChuyenNganh = cn.MaChuyenNganh
                                 WHERE sv.TrangThai = 'Initialize' AND cn.TrangThai = 'Initialize' 
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

        private void LoadDatabaseWithParams(string sql, SqlParameter[] parameters)
        {
            try
            {
                this.dataGridViewQLSV.DataSource = DataBase.GetData(sql, parameters);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
