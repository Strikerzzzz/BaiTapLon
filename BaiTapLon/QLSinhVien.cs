using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaiTapLon
{
    public partial class QLSinhVien : Form
    {
        public QLSinhVien()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        void LoadDatabase()
        {
            try
            {
                this.dataGridViewQLSV.DataSource = DataBase.GetData("SELECT * FROM SinhVien where TrangThai = 'Initialize'");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }
        private void QLSinhVien_Load(object sender, EventArgs e)
        {
            cboGioiTinh.Items.Add("Chọn giới tính");
            cboGioiTinh.Items.Add("Nam");
            cboGioiTinh.Items.Add("Nữ");
            cboGioiTinh.SelectedIndex = 0;

            for (int i = 1; i <= 19; i++)
            {
                cboKhoaHoc.Items.Add("Khóa " + i);
            }
            cboKhoaHoc.Items.Insert(0, "Chọn khóa học");
            cboKhoaHoc.SelectedIndex = 0;

            cboMaChuyenNganh.Items.Add("Chọn mã chuyên ngành"); 
            cboMaChuyenNganh.SelectedIndex = 0;

            LoadDatabase();
        }
      
        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateFields()) 
                {
                    return; 
                }

                string query = "INSERT INTO SinhVien (HoDem, Ten, Email, CCCD, SoDienThoai, GioiTinh, NgaySinh, DiaChi, KhoaHoc, TrangThai)" +
                    " VALUES (@HoDem, @Ten, @Email, @CCCD, @SoDienThoai, @GioiTinh, @NgaySinh, @DiaChi, @KhoaHoc, @TrangThai)";
                SqlParameter[] parameters = {
                    new SqlParameter("@HoDem", txtHoDem.Text),
                    new SqlParameter("@Ten", txtTen.Text),
                    new SqlParameter("@Email", txtEmail.Text),
                    new SqlParameter("@CCCD", txtSoCCCD.Text),
                    new SqlParameter("@SoDienThoai", txtSDT.Text),
                    new SqlParameter("@GioiTinh", cboGioiTinh.Text),
                    new SqlParameter("@NgaySinh", dateTimePickerNS.Value),
                    new SqlParameter("@DiaChi", txtDiaChi.Text),
                    //new SqlParameter("@MaChuyenNganh", cboMaChuyenNganh.Text),
                    new SqlParameter("@KhoaHoc", cboKhoaHoc.Text),
                    new SqlParameter("@TrangThai", "Initialize"),
                };

                bool result = new DataBase().UpdateData(query, parameters);

                if (result)
                {
                    MessageBox.Show("Thêm sinh viên thành công!");
                    LoadDatabase();
                    ClearFields();
                }
                else
                {
                    MessageBox.Show("Thêm sinh viên thất bại.");
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi cơ sở dữ liệu: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateFields())
                {
                    return;
                }
                string query = "UPDATE  SinhVien SET HoDem = @HoDem, Ten = @Ten, Email = @Email, CCCD = @CCCD, SoDienThoai = @SoDienThoai, GioiTinh = @GioiTinh, NgaySinh = @NgaySinh, DiaChi = @DiaChi, KhoaHoc = @KhoaHoc WHERE MaSV = @MaSV";
                SqlParameter[] parameters = {
                    new SqlParameter("@HoDem", txtHoDem.Text),
                    new SqlParameter("@Ten", txtTen.Text),
                    new SqlParameter("@Email", txtEmail.Text),
                    new SqlParameter("@CCCD", txtSoCCCD.Text),
                    new SqlParameter("@SoDienThoai", txtSDT.Text),
                    new SqlParameter("@GioiTinh", cboGioiTinh.Text),
                    new SqlParameter("@NgaySinh", dateTimePickerNS.Value),
                    new SqlParameter("@DiaChi", txtDiaChi.Text),
                    //new SqlParameter("@MaChuyenNganh", cboMaChuyenNganh.Text),
                    new SqlParameter("@KhoaHoc", cboKhoaHoc.Text),
                    new SqlParameter("@TrangThai", "Initialize"),
                    new SqlParameter("@MaSV", txtMaSV.Text),
                };
                bool result = new DataBase().UpdateData(query, parameters);
                if (result)
                {
                    MessageBox.Show("Sửa sinh viên thành công!");
                    LoadDatabase();
                    ClearFields();
                }
                else
                {
                    MessageBox.Show("Sửa sinh viên thất bại.");
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi cơ sở dữ liệu: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }

        private void dataGridViewQLSV_Click(object sender, EventArgs e)
        {
            try
            {
                txtMaSV.Text = dataGridViewQLSV.CurrentRow.Cells[0].Value.ToString();
                txtHoDem.Text = dataGridViewQLSV.CurrentRow.Cells[1].Value.ToString();
                txtTen.Text = dataGridViewQLSV.CurrentRow.Cells[2].Value.ToString();
                txtEmail.Text = dataGridViewQLSV.CurrentRow.Cells[3].Value.ToString();
                txtSoCCCD.Text = dataGridViewQLSV.CurrentRow.Cells[4].Value.ToString();
                txtSDT.Text = dataGridViewQLSV.CurrentRow.Cells[5].Value.ToString();
                cboGioiTinh.Text = dataGridViewQLSV.CurrentRow.Cells[6].Value.ToString();
                dateTimePickerNS.Text = dataGridViewQLSV.CurrentRow.Cells[7].Value.ToString();
                txtDiaChi.Text = dataGridViewQLSV.CurrentRow.Cells[8].Value.ToString();
                cboMaChuyenNganh.Text = dataGridViewQLSV.CurrentRow.Cells[9].Value.ToString();
                cboKhoaHoc.Text = dataGridViewQLSV.CurrentRow.Cells[10].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi hiển thị thông tin sinh viên: " + ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewQLSV.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn sinh viên cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; 
                }
                string maSV = dataGridViewQLSV.SelectedRows[0].Cells["MaSV"].Value.ToString();
                string query = "UPDATE SinhVien SET TrangThai = @TrangThai WHERE MaSV = @MaSV";
                SqlParameter[] parameters = {
                    new SqlParameter("@MaSV", maSV),
                    new SqlParameter("@TrangThai", "Deleted"),
                };
                bool result = new DataBase().UpdateData(query, parameters);

                if (result)
                {
                    MessageBox.Show("Xóa sinh viên thành công!");
                    LoadDatabase(); 
                    ClearFields(); 
                }
                else
                {
                    MessageBox.Show("Xóa sinh viên thất bại.");
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi cơ sở dữ liệu: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }

        private void ClearFields()
        {
            txtHoDem.Text = string.Empty;
            txtTen.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtSoCCCD.Text = string.Empty;
            txtSDT.Text = string.Empty;
            txtDiaChi.Text = string.Empty;
            txtMaSV.Text = string.Empty;
            cboGioiTinh.SelectedIndex = 0; 
            cboKhoaHoc.SelectedIndex = 0; 
            cboMaChuyenNganh.SelectedIndex = 0; 
            dateTimePickerNS.Value = DateTime.Now;
        }
        private bool ValidateFields()
        {
            if (string.IsNullOrEmpty(txtHoDem.Text) || string.IsNullOrEmpty(txtTen.Text))
            {
                MessageBox.Show("Họ và tên không được để trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (!IsValidEmail(txtEmail.Text))
            {
                MessageBox.Show("Email không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrEmpty(txtSoCCCD.Text) || txtSoCCCD.Text.Length != 12)
            {
                MessageBox.Show("Số CCCD không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrEmpty(txtSDT.Text) || !Regex.IsMatch(txtSDT.Text, @"^\d{10}$"))
            {
                MessageBox.Show("Số điện thoại không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrEmpty(txtDiaChi.Text))
            {
                MessageBox.Show("Địa chỉ không được để trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            //if (cboGioiTinh.SelectedIndex == 0 || cboKhoaHoc.SelectedIndex == 0 || cboMaChuyenNganh.SelectedIndex == 0)
            //{
            //    MessageBox.Show("Vui lòng chọn đầy đủ thông tin về giới tính, khóa học và chuyên ngành.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return false;
            //}
            if (dateTimePickerNS.Value >= DateTime.Now)
            {
                MessageBox.Show("Ngày sinh không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

    }
}
