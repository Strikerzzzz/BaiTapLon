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
    public partial class QLDiemDanh : Form
    {
        Dictionary<string, string> dictSV;
        Dictionary<string, string> dictML = new Dictionary<string, string>();
        private DiemDanhController diemDanhController = new DiemDanhController();

        public QLDiemDanh()
        {
            InitializeComponent();
        }

        private void QLDiemDanh_Load(object sender, EventArgs e)
        {
            LoadDatabase();
            cboTT.Items.Add("Có");
            cboTT.Items.Add("Không");
            cboTT.SelectedIndex = 0;
            LoadClasses();
        }

        private void LoadDatabase()
        {
            try
            {
                dataGridView1.DataSource = diemDanhController.LoadDiemDanhData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadClasses()
        {
            dictML.Add("-1", "Chọn tên lớp ");
            cboTenLop.Items.Add("Chọn tên lớp");
            DataTable dt = DataBase.GetData("SELECT MaLop, TenLop FROM LopHoc where TrangThai = 'Initialize'");
            diemDanhController.LoadListDB(dt, diemDanhController.dictML);

            cboTenLop.DataSource = new BindingSource(diemDanhController.dictML, null);
            cboTenLop.DisplayMember = "Value";
            cboTenLop.ValueMember = "Key";
            cboTenSV.Enabled = false;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!ValidateFields()) return;

            DiemDanh diemDanh = new DiemDanh
            {
                MaSV = int.Parse(cboTenSV.SelectedValue.ToString()),
                MaLop = int.Parse(cboTenLop.SelectedValue.ToString()),
                NgayDiemDanh = dateTimePickerNgayDD.Value,
                TTDiemDanh = cboTT.Text
            };

            bool result = diemDanhController.AddDiemDanh(diemDanh);
            MessageBox.Show(result ? "Thêm điểm danh thành công!" : "Thêm điểm danh thất bại.");
            LoadDatabase();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (!ValidateFields()) return;

            DiemDanh diemDanh = new DiemDanh
            {
                IDDiemDanh = int.Parse(txtID.Text),
                MaSV = int.Parse(cboTenSV.SelectedValue.ToString()),
                MaLop = int.Parse(cboTenLop.SelectedValue.ToString()),
                NgayDiemDanh = dateTimePickerNgayDD.Value,
                TTDiemDanh = cboTT.Text
            };

            bool result = diemDanhController.UpdateDiemDanh(diemDanh);
            MessageBox.Show(result ? "Sửa điểm danh thành công!" : "Sửa điểm danh thất bại.");
            LoadDatabase();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtID.Text);
            bool result = diemDanhController.DeleteDiemDanh(id);
            MessageBox.Show(result ? "Xóa thành công!" : "Xóa thất bại.");
            LoadDatabase();
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private bool ValidateFields()
        {
            if (cboTenLop.SelectedIndex == 0)
            {
                MessageBox.Show("Vui lòng chọn lớp.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboTenLop.Focus();
                return false;
            }
            if (cboTenSV.SelectedIndex == 0)
            {
                MessageBox.Show("Vui lòng chọn sinh viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboTenSV.Focus();
                return false;
            }
            if (dateTimePickerNgayDD.Value > DateTime.Now)
            {
                MessageBox.Show("Ngày điểm danh không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }
        private void cboTenLop_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTenLop.SelectedIndex != 0)
            {
                cboTenSV.Enabled = true;
                // Xoá dữ liệu cũ
                cboTenSV.DataSource = null;
                cboTenSV.Items.Clear();

                // Tạo mới Dictionary cho sinh viên
                dictSV = new Dictionary<string, string>();
                dictSV.Add("-1", "Chọn tên sinh viên");

                // Truy vấn dữ liệu sinh viên
                DataTable dt1 = DataBase.GetData("SELECT sv.MaSV, CONCAT(sv.HoDem, ' ', sv.Ten, '-', sv.MaSV) " +
                    "FROM LopHoc_SinhVien ls " +
                    "JOIN SinhVien sv ON sv.MaSV = ls.MaSV " +
                    $"WHERE ls.MaLop = '{cboTenLop.SelectedValue.ToString()}' AND ls.TrangThai = 'Initialize'");

                // Kiểm tra dữ liệu và nạp vào Dictionary
                if (dt1 != null && dt1.Rows.Count > 0)
                {
                    foreach (DataRow row in dt1.Rows)
                    {
                        string maSV = row["MaSV"].ToString();
                        string tenSV = row[1].ToString();

                        // Kiểm tra và tránh trùng lặp
                        if (!dictSV.ContainsKey(maSV))
                        {
                            dictSV.Add(maSV, tenSV);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu tên sinh viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                // Thiết lập DataSource cho ComboBox
                cboTenSV.DataSource = new BindingSource(dictSV, null);
                cboTenSV.DisplayMember = "Value";
                cboTenSV.ValueMember = "Key";
            }
        }
        private void dataGridView1_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    txtID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    string ID, ten;
                    ten = dataGridView1.CurrentRow.Cells[1].Value?.ToString();
                    ID = dictSV.FirstOrDefault(x => x.Value == ten).Key;
                    if (!string.IsNullOrEmpty(ID))
                    {
                        cboTenSV.SelectedValue = ID;
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy mã sinh viên cho tên: " + ID);
                    }
                    ten = dataGridView1.CurrentRow.Cells[2].Value?.ToString();
                    ID = dictML.FirstOrDefault(x => x.Value == ten).Key;
                    if (!string.IsNullOrEmpty(ID))
                    {
                        cboTenLop.SelectedValue = ID;
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy mã lớp cho tên: " + ID);
                    }

                    dateTimePickerNgayDD.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                    cboTT.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi hiển thị thông tin điểm danh: " + ex.Message);
            }
        }

    }
}
