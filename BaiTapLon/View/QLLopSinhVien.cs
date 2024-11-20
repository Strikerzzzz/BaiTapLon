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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaiTapLon
{
    public partial class QLLopSinhVien : Form
    {
        private Dictionary<int, string> dictLop = new Dictionary<int, string>();
        private Dictionary<int, string> dictSinhVien = new Dictionary<int, string>();

        public QLLopSinhVien()
        {
            InitializeComponent();
        }

        private void QLLopSinhVien_Load(object sender, EventArgs e)
        {
            LoadLopSinhVienData();
            LoadLopComboBox();
            LoadSinhVienComboBox();
        }

        private void LoadLopSinhVienData()
        {
            DataTable dtLopSinhVien = DataBase.GetData(@"SELECT lhsv.ID, lh.TenLop, CONCAT(sv.HoDem, ' ', sv.Ten,'-',sv.MaSV) AS TenSinhVien 
            FROM LopHoc_SinhVien lhsv 
            LEFT JOIN LopHoc lh ON lh.MaLop = lhsv.MaLop 
            LEFT JOIN SinhVien sv ON lhsv.MaSV = sv.MaSV 
            WHERE lhsv.TrangThai = 'Initialize' AND sv.TrangThai = 'Initialize' AND lh.TrangThai = 'Initialize'");
            dataGridView1.DataSource = dtLopSinhVien;
        }

        private void LoadLopComboBox()
        {
            dictLop = new Dictionary<int, string> { { 0, "Chọn lớp" } };
            DataTable dt = DataBase.GetData("SELECT MaLop, TenLop FROM LopHoc where TrangThai = 'Initialize'");

            foreach (DataRow row in dt.Rows)
            {
                int maLop = Convert.ToInt32(row["MaLop"]);
                string tenLop = row["TenLop"].ToString();
                dictLop.Add(maLop, tenLop);
            }

            cboTenLop.DataSource = new BindingSource(dictLop, null);
            cboTenLop.DisplayMember = "Value";
            cboTenLop.ValueMember = "Key";
        }

        private void LoadSinhVienComboBox()
        {
            dictSinhVien = new Dictionary<int, string> { { 0, "Chọn sinh viên" } };

            DataTable dt = DataBase.GetData("SELECT MaSV, CONCAT(HoDem, ' ', Ten, '-', MaSV) AS HoTen FROM SinhVien WHERE TrangThai = 'Initialize'");

            foreach (DataRow row in dt.Rows)
            {
                int maSV = Convert.ToInt32(row["MaSV"]);
                string hoTen = row["HoTen"].ToString();
                dictSinhVien.Add(maSV, hoTen);
            }

            cboTenSV.DataSource = new BindingSource(dictSinhVien, null);
            cboTenSV.DisplayMember = "Value";
            cboTenSV.ValueMember = "Key";
        }


        private void btnThem_Click(object sender, EventArgs e)
        {
            if (ValidateFields())
            {
                var lopSinhVien = new LopSinhVien
                {
                    MaLop = Convert.ToInt32(cboTenLop.SelectedValue),
                    MaSV = Convert.ToInt32(cboTenSV.SelectedValue)
                };

                bool result = LopSinhVienController.AddLopHocSinhVien(lopSinhVien);
                MessageBox.Show(result ? "Thêm thành công!" : "Thêm thất bại.");
                LoadLopSinhVienData();
                ClearFields();
            }
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtID.Text))
            {
                MessageBox.Show("Vui lòng chọn mã lớp học_sinh viên cần sửa!!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (ValidateFields())
            {
                var lopSinhVien = new LopSinhVien
                {
                    ID = Convert.ToInt32(txtID.Text),
                    MaLop = Convert.ToInt32(cboTenLop.SelectedValue),
                    MaSV = Convert.ToInt32(cboTenSV.SelectedValue)
                };

                bool result = LopSinhVienController.UpdateLopHocSinhVien(lopSinhVien);
                MessageBox.Show(result ? "Sửa thành công!" : "Sửa thất bại.");
                LoadLopSinhVienData();
                ClearFields();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem mã lớp học_sinh viên có được chọn không
            if (string.IsNullOrWhiteSpace(txtID.Text))
            {
                MessageBox.Show("Vui lòng chọn mã lớp học_sinh viên cần xoá!!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Hiển thị hộp thoại xác nhận trước khi xóa
            DialogResult confirmResult = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa lớp học_sinh viên này không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            // Kiểm tra nếu người dùng chọn Yes
            if (confirmResult == DialogResult.Yes)
            {
                // Thực hiện xóa
                bool result = LopSinhVienController.DeleteLopHocSinhVien(Convert.ToInt32(txtID.Text));
                MessageBox.Show(result ? "Xóa thành công!" : "Xóa thất bại.");

                // Tải lại dữ liệu và xóa các trường thông tin
                LoadLopSinhVienData();
                ClearFields();
            }
        }


        private void dataGridView1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                txtID.Text = dataGridView1.CurrentRow.Cells["ID"].Value.ToString();
                string tenLop = dataGridView1.CurrentRow.Cells[1].Value?.ToString();
                int maLop = dictLop.FirstOrDefault(x => x.Value == tenLop).Key;

                if (maLop != 0)
                {
                    cboTenLop.SelectedValue = maLop;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy mã lớp cho tên: " + tenLop);
                }
                string tenSinhVien = dataGridView1.CurrentRow.Cells[2].Value?.ToString();
                int maSinhVien = dictSinhVien.FirstOrDefault(x => x.Value == tenSinhVien).Key;

                if (maSinhVien != 0)
                {
                    cboTenSV.SelectedValue = maSinhVien;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy mã sinh viên cho tên: " + tenSinhVien);
                }
            }
        }


        private void ClearFields()
        {
            txtID.Text = string.Empty;
            cboTenLop.SelectedIndex = 0;
            cboTenSV.SelectedIndex = 0;
        }

        private bool ValidateFields()
        {
            if (cboTenLop.SelectedIndex == 0)
            {
                MessageBox.Show("Vui lòng chọn lớp.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (cboTenSV.SelectedIndex == 0)
            {
                MessageBox.Show("Vui lòng chọn sinh viên.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void btnNhapLai_Click(object sender, EventArgs e)
        {
            ClearFields();
        }
    }
}
