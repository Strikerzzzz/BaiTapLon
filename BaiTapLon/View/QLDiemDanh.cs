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
            try
            {
                cboTenLop.Items.Clear();
                cboTenLop.Enabled = false;

                dictML.Add("-1", "Chọn tên lớp ");
                DataTable dt = DataBase.GetData("SELECT MaLop, TenLop FROM LopHoc WHERE TrangThai = 'Initialize'");
                diemDanhController.LoadListDB(dt, dictML);

                cboTenLop.DataSource = new BindingSource(dictML, null);
                cboTenLop.DisplayMember = "Value";
                cboTenLop.ValueMember = "Key";
                cboTenLop.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách lớp: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            if (cboTenLop.SelectedValue == null || cboTenLop.SelectedValue.ToString() == "-1")
            {
                MessageBox.Show("Vui lòng chọn lớp.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboTenLop.Focus();
                return false;
            }

            if (cboTenSV.SelectedValue == null || cboTenSV.SelectedValue.ToString() == "-1")
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
            try
            {
                if (cboTenLop.SelectedIndex > 0)
                {
                    cboTenSV.Enabled = true;
                    cboTenSV.DataSource = null;

                    dictSV = new Dictionary<string, string> { { "-1", "Chọn tên sinh viên" } };
                    DataTable dt = diemDanhController.LoadSinhVienTheoLop(int.Parse(cboTenLop.SelectedValue.ToString()));
                    diemDanhController.LoadListDB(dt, dictSV);

                    cboTenSV.DataSource = new BindingSource(dictSV, null);
                    cboTenSV.DisplayMember = "Value";
                    cboTenSV.ValueMember = "Key";
                }
                else
                {
                    cboTenSV.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thay đổi lớp: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    txtID.Text = dataGridView1.CurrentRow.Cells[0].Value?.ToString();

                    // Lấy tên lớp và mã lớp từ hàng hiện tại của dataGridView
                    string tenLop = dataGridView1.CurrentRow.Cells[2].Value?.ToString();
                    string maLop = dictML.FirstOrDefault(x => x.Value == tenLop).Key;

                    // Nếu lớp học chưa được chọn, chọn lớp và tải sinh viên
                    if (cboTenLop.SelectedValue == null || cboTenLop.SelectedValue.ToString() != maLop)
                    {
                        cboTenLop.SelectedValue = maLop ?? "-1";
                        // Gọi hàm LoadSinhVienTheoLop để tải sinh viên theo lớp
                        cboTenLop_SelectedIndexChanged(null, null);
                    }

                    // Lấy tên sinh viên và mã sinh viên
                    string tenSV = dataGridView1.CurrentRow.Cells[1].Value?.ToString();
                    string maSV = dictSV.FirstOrDefault(x => x.Value == tenSV).Key;
                    cboTenSV.SelectedValue = maSV ?? "-1";

                    // Cài đặt giá trị cho các trường còn lại
                    dateTimePickerNgayDD.Value = DateTime.TryParse(dataGridView1.CurrentRow.Cells[3].Value?.ToString(), out var ngay) ? ngay : DateTime.Now;
                    cboTT.Text = dataGridView1.CurrentRow.Cells[4].Value?.ToString() ?? "Không";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi hiển thị thông tin điểm danh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



    }
}
