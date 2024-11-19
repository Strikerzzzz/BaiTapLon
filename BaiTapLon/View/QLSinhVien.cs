using BaiTapLon.Controller;
using BaiTapLon.Model;
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
        private Dictionary<int, string> dictLM = new Dictionary<int, string>();  // Dùng int cho MaChuyenNganh

        public QLSinhVien()
        {
            InitializeComponent();
        }

        private void QLSinhVien_Load(object sender, EventArgs e)
        {
            LoadSinhVienData();
            LoadChuyenNganhComboBox();
            LoadKhoaHocComboBox();
            LoadGioiTinhComboBox();
            LoadDiaChiComboBox();  
        }

        private void LoadSinhVienData()
        {
            DataTable dtSinhVien = DataBase.GetData(@"SELECT MaSV, HoDem, Ten, Email, CCCD, SoDienThoai, GioiTinh, NgaySinh, DiaChi, 
                                             cn.TenChuyenNganh, KhoaHoc 
                                             FROM SinhVien sv 
                                             LEFT JOIN ChuyenNganh cn ON cn.MaChuyenNganh = sv.MaChuyenNganh 
                                             WHERE sv.TrangThai = 'Initialize'");

            dataGridViewQLSV.DataSource = dtSinhVien;
        }

        private void LoadChuyenNganhComboBox()
        {
            dictLM = new Dictionary<int, string>();
            dictLM.Add(0, "Chọn chuyên ngành"); 
            DataTable dt = DataBase.GetData("SELECT MaChuyenNganh, TenChuyenNganh FROM ChuyenNganh WHERE TrangThai = 'Initialize'");

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    int maChuyenNganh = Convert.ToInt32(row["MaChuyenNganh"]);
                    string tenChuyenNganh = row["TenChuyenNganh"].ToString();
                    dictLM.Add(maChuyenNganh, tenChuyenNganh);
                }
                cboMaChuyenNganh.DataSource = new BindingSource(dictLM, null);
                cboMaChuyenNganh.DisplayMember = "Value";
                cboMaChuyenNganh.ValueMember = "Key";
            }
        }
        private void LoadKhoaHocComboBox()
        {
            cboKhoaHoc.Items.Add("Chọn khóa học");
            for (int i = 1; i <= 19; i++)
            {
                cboKhoaHoc.Items.Add("Khóa " + i);
            }
            cboKhoaHoc.SelectedIndex = 0;
        }

        private void LoadGioiTinhComboBox()
        {
            cboGioiTinh.Items.Add("Chọn giới tính");
            cboGioiTinh.Items.Add("Nam");
            cboGioiTinh.Items.Add("Nữ");
            cboGioiTinh.SelectedIndex = 0;
        }
        private void LoadDiaChiComboBox()
        {
            List<string> diaChiList = new List<string>
            {
                 "An Giang", "Bà Rịa-Vũng Tàu", "Bắc Giang", "Bắc Kạn", "Bến Tre", "Bình Định",
                "Bình Dương", "Bình Phước", "Bình Thuận", "Cà Mau", "Cao Bằng", "Cần Thơ",
                "Cao Bằng", "Đà Nẵng", "Đắk Lắk", "Đắk Nông", "Điện Biên", "Đồng Nai",
                "Đồng Tháp", "Hà Giang", "Hà Nam", "Hà Nội", "Hà Tĩnh", "Hải Dương",
                "Hải Phòng", "Hòa Bình", "Hồ Chí Minh", "Hậu Giang", "Hưng Yên", "Khánh Hòa",
                "Kiên Giang", "Kon Tum", "Lai Châu", "Lâm Đồng", "Lạng Sơn", "Lào Cai",
                "Long An", "Nam Định", "Nghệ An", "Ninh Bình", "Ninh Thuận", "Phú Thọ",
                "Phú Yên", "Quảng Bình", "Quảng Nam", "Quảng Ngãi", "Quảng Ninh",
                "Quảng Trị", "Sóc Trăng", "Sơn La", "Tây Ninh", "Thái Bình", "Thái Nguyên",
                "Thanh Hóa", "Thừa Thiên-Huế", "Tiền Giang", "Trà Vinh", "Tuyên Quang",
                "Vĩnh Long", "Vĩnh Phúc", "Yên Bái"
            };

            cboDiaChi.Items.Add("Chọn địa chỉ");
            foreach (var diaChi in diaChiList)
            {
                cboDiaChi.Items.Add(diaChi);
            }
            cboDiaChi.SelectedIndex = 0;  
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (ValidateFields())
            {
                var sinhVien = new SinhVien
                {
                    HoDem = txtHoDem.Text,
                    Ten = txtTen.Text,
                    Email = txtEmail.Text,
                    CCCD = txtSoCCCD.Text,
                    SoDienThoai = txtSDT.Text,
                    GioiTinh = cboGioiTinh.Text,
                    NgaySinh = dateTimePickerNS.Value,
                    DiaChi = cboDiaChi.Text,  
                    MaChuyenNganh = Convert.ToInt32(cboMaChuyenNganh.SelectedValue), 
                    KhoaHoc = cboKhoaHoc.Text
                };

                bool result = SinhVienController.AddSinhVien(sinhVien);
                MessageBox.Show(result ? "Thêm thành công!" : "Thêm thất bại.");
                LoadSinhVienData();
                ClearFields();
            }
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaSV.Text))
            {
                MessageBox.Show("Vui lòng chọn mã sinh viên cần sửa!!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (ValidateFields())
            {
                var sinhVien = new SinhVien
                {
                    MaSV = Convert.ToInt32(txtMaSV.Text),  
                    HoDem = txtHoDem.Text,
                    Ten = txtTen.Text,
                    Email = txtEmail.Text,
                    CCCD = txtSoCCCD.Text,
                    SoDienThoai = txtSDT.Text,
                    GioiTinh = cboGioiTinh.Text,
                    NgaySinh = dateTimePickerNS.Value,
                    DiaChi = cboDiaChi.Text,  
                    MaChuyenNganh = Convert.ToInt32(cboMaChuyenNganh.SelectedValue),  
                    KhoaHoc = cboKhoaHoc.Text
                };

                bool result = SinhVienController.UpdateSinhVien(sinhVien);
                MessageBox.Show(result ? "Sửa thành công!" : "Sửa thất bại.");
                LoadSinhVienData();
                ClearFields();
            }
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaSV.Text))
            {
                MessageBox.Show("Vui lòng chọn mã sinh viên cần xoá!!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DialogResult confirmResult = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa sinh viên này không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );
            if (confirmResult == DialogResult.Yes)
            {
                bool result = SinhVienController.DeleteSinhVien(txtMaSV.Text);
                MessageBox.Show(result ? "Xóa thành công!" : "Xóa thất bại.");
                if (result)
                {
                    LoadSinhVienData();
                    ClearFields();
                }
            }
            else
            {
                MessageBox.Show("Hủy bỏ xóa sinh viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void dataGridViewQLSV_Click(object sender, EventArgs e)
        {
            if (dataGridViewQLSV.CurrentRow != null)
            {
                txtMaSV.Text = dataGridViewQLSV.CurrentRow.Cells["MaSV"].Value.ToString();
                txtHoDem.Text = dataGridViewQLSV.CurrentRow.Cells["HoDem"].Value.ToString();
                txtTen.Text = dataGridViewQLSV.CurrentRow.Cells["Ten"].Value.ToString();
                txtEmail.Text = dataGridViewQLSV.CurrentRow.Cells["Email"].Value.ToString();
                txtSoCCCD.Text = dataGridViewQLSV.CurrentRow.Cells["CCCD"].Value.ToString();
                txtSDT.Text = dataGridViewQLSV.CurrentRow.Cells["SoDienThoai"].Value.ToString();
                cboGioiTinh.Text = dataGridViewQLSV.CurrentRow.Cells["GioiTinh"].Value.ToString();
                dateTimePickerNS.Text = dataGridViewQLSV.CurrentRow.Cells["NgaySinh"].Value.ToString();
                cboDiaChi.Text = dataGridViewQLSV.CurrentRow.Cells["DiaChi"].Value.ToString();
                cboKhoaHoc.Text = dataGridViewQLSV.CurrentRow.Cells["KhoaHoc"].Value.ToString();

                string tenChuyenNganh = dataGridViewQLSV.CurrentRow.Cells["TenChuyenNganh"].Value.ToString();

                int maChuyenNganh = dictLM.FirstOrDefault(x => x.Value == tenChuyenNganh).Key;

                cboMaChuyenNganh.SelectedValue = maChuyenNganh;
            }
        }
        private void ClearFields()
        {
            txtHoDem.Text = string.Empty;
            txtTen.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtSoCCCD.Text = string.Empty;
            txtSDT.Text = string.Empty;
            txtMaSV.Text = string.Empty;
            cboDiaChi.SelectedIndex = 0;
            cboGioiTinh.SelectedIndex = 0;
            cboKhoaHoc.SelectedIndex = 0;
            cboMaChuyenNganh.SelectedIndex = 0;
            dateTimePickerNS.Value = DateTime.Now;
        }
        private bool ValidateFields()
        {
            if (string.IsNullOrEmpty(txtHoDem.Text))
            {
                MessageBox.Show("Họ đệm không được để trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtHoDem.Focus();
                return false;
            }
            if ( string.IsNullOrEmpty(txtTen.Text))
            {
                MessageBox.Show("Tên không được để trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTen.Focus();
                return false;
            }
            if (!IsValidEmail(txtEmail.Text))
            {
                MessageBox.Show("Email không hợp lệ", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEmail.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtSoCCCD.Text) || txtSoCCCD.Text.Length != 12)
            {
                MessageBox.Show("Số CCCD không hợp lệ!Không được để trống và độ dài bằng 12!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSoCCCD.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtSDT.Text) || !Regex.IsMatch(txtSDT.Text, @"^\d{10}$"))
            {
                MessageBox.Show("Số điện thoại không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSDT.Focus();
                return false;
            }
            if (cboDiaChi.SelectedIndex == 0)
            {
                MessageBox.Show("Vui lòng chọn đầy đủ thông tin về địa chỉ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cboDiaChi.Focus();
                return false;
            }
            if (cboGioiTinh.SelectedIndex == 0)
            {
                MessageBox.Show("Vui lòng chọn đầy đủ thông tin về giới tính.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cboGioiTinh.Focus();
                return false;
            }
            if (cboKhoaHoc.SelectedIndex == 0)
            {
                MessageBox.Show("Vui lòng chọn đầy đủ thông tin về khóa học", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cboKhoaHoc.Focus();
                return false;
            }
            if (cboMaChuyenNganh.SelectedIndex == 0)
            {
                MessageBox.Show("Vui lòng chọn đầy đủ thông tin về chuyên ngành.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cboMaChuyenNganh.Focus();
                return false;
            }
            if (dateTimePickerNS.Value > DateTime.Now)
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

        private void btnNhapLai_Click(object sender, EventArgs e)
        {
            ClearFields();
        }
    }

}

