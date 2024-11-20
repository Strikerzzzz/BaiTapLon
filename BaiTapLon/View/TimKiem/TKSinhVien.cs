using System;
using System.Collections.Generic;
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

        private void TKSinhVien_Load(object sender, EventArgs e)
        {
            cboSinhVien.Items.Add("Vui lòng chọn lựa chọn của bạn");
            cboSinhVien.Items.Add("Mã sinh viên");
            cboSinhVien.Items.Add("Họ đệm");
            cboSinhVien.Items.Add("Tên");
            cboSinhVien.Items.Add("Giới tính");
            cboSinhVien.Items.Add("Địa chỉ");
            cboSinhVien.Items.Add("Khoá học");
            cboSinhVien.Items.Add("Tên chuyên ngành");
            cboSinhVien.SelectedIndex = 0;

            cboSinhVien.SelectedIndexChanged += cboSinhVien_SelectedIndexChanged;
            cboDuLieu.SelectedIndexChanged += cboDuLieu_SelectedIndexChanged_1;

            LoadSinhVienData();
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

        private void LoadDatabaseWithParams(string sql, SqlParameter[] parameters)
        {
            try
            {
                this.dataGridViewQLSV.DataSource = DataBase.GetData(sql, parameters);
            }
            catch (Exception ex)
            {
                
            }
        }

        private void btnTK_Click(object sender, EventArgs e)
        {
            string value = txtTK.Text.Trim();
            string columnName = cboSinhVien.SelectedItem?.ToString();
            string selectedValue = cboDuLieu.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(columnName) || columnName == "Vui lòng chọn lựa chọn của bạn")
            {
                MessageBox.Show("Vui lòng chọn cột để tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string selectedColumn = MapToDatabaseColumn(columnName);

            if (string.IsNullOrEmpty(value) && (selectedValue == "Vui lòng chọn dữ liệu" || string.IsNullOrEmpty(selectedValue)))
            {
                LoadSinhVienData();
            }
            else
            {
                if (!string.IsNullOrEmpty(value))
                {
                    string query = $@"SELECT MaSV, HoDem, Ten, Email, CCCD, SoDienThoai, GioiTinh, NgaySinh, DiaChi, cn.TenChuyenNganh, KhoaHoc 
                              FROM SinhVien sv 
                              LEFT JOIN ChuyenNganh cn ON cn.MaChuyenNganh = sv.MaChuyenNganh 
                              WHERE sv.TrangThai = 'Initialize' AND cn.TrangThai = 'Initialize' AND {selectedColumn} LIKE @value";

                    SqlParameter[] parameters = {
                new SqlParameter("@value", $"%{value}%")
            };

                    LoadDatabaseWithParams(query, parameters);
                }
            }
        }

        private void cboSinhVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            string columnName = cboSinhVien.SelectedItem?.ToString();

            if (!string.IsNullOrEmpty(columnName) && columnName != "Vui lòng chọn lựa chọn của bạn")
            {
                DataTable data = GetDistinctDataForComboBox(columnName);

                cboDuLieu.Items.Clear();
                cboDuLieu.Items.Add("Vui lòng chọn dữ liệu");

                foreach (DataRow row in data.Rows)
                {
                    cboDuLieu.Items.Add(row[0].ToString());
                }

                if (cboDuLieu.Items.Count > 0)
                {
                    cboDuLieu.SelectedIndex = 0;
                }
            }
            else
            {
                cboDuLieu.Items.Clear();
            }
        }
      
        private DataTable GetDistinctDataForComboBox(string columnName)
        {
            string sql = $@"SELECT DISTINCT {MapToDatabaseColumn(columnName)} 
                            FROM SinhVien sv 
                            LEFT JOIN ChuyenNganh cn ON cn.MaChuyenNganh = sv.MaChuyenNganh 
                            WHERE sv.TrangThai = 'Initialize' AND cn.TrangThai = 'Initialize'";
            return DataBase.GetData(sql, null);
        }

        private string MapToDatabaseColumn(string displayName)
        {
            switch (displayName)
            {
                case "Mã sinh viên": return "MaSV";
                case "Họ đệm": return "HoDem";
                case "Tên": return "Ten";
                case "Giới tính": return "GioiTinh";
                case "Địa chỉ": return "DiaChi";
                case "Khoá học": return "KhoaHoc";
                case "Tên chuyên ngành": return "TenChuyenNganh";
                default: throw new ArgumentException("Cột không hợp lệ!");
            }

        }

        private void cboDuLieu_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string selectedColumn = cboSinhVien.SelectedItem?.ToString();
            string selectedValue = cboDuLieu.SelectedItem?.ToString();

            if (!string.IsNullOrEmpty(selectedColumn) && !string.IsNullOrEmpty(selectedValue))
            {
                string columnName = MapToDatabaseColumn(selectedColumn);

                string query = $@"SELECT MaSV, HoDem, Ten, Email, CCCD, SoDienThoai, GioiTinh, NgaySinh, DiaChi, cn.TenChuyenNganh, KhoaHoc 
                          FROM SinhVien sv 
                          LEFT JOIN ChuyenNganh cn ON cn.MaChuyenNganh = sv.MaChuyenNganh 
                          WHERE sv.TrangThai = 'Initialize' AND cn.TrangThai = 'Initialize' AND {columnName} = @value";

                SqlParameter[] parameters = {
                    new SqlParameter("@value", selectedValue)
                };
                LoadDatabaseWithParams(query, parameters);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn đầy đủ thông tin để tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
