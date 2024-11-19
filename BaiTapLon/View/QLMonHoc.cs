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
    public partial class QLMonHoc : Form
    {
        Dictionary<string, string> dictLM = new Dictionary<string, string>();

        public QLMonHoc()
        {
            InitializeComponent();
        }

        private void QLMonHoc_Load(object sender, EventArgs e)
        {
            LoadDatabase();

            DataTable dt = DataBase.GetData("SELECT IDLoaiMon, LoaiMon FROM LoaiMon WHERE TrangThai = 'Initialize'");
            if (dt != null && dt.Rows.Count > 0)
            {
                // Thêm tùy chọn "Chọn loại môn" lên đầu
                dictLM.Add("0", "Chọn loại môn");
                foreach (DataRow row in dt.Rows)
                {
                    dictLM.Add(row["IDLoaiMon"].ToString(), row["LoaiMon"].ToString());
                }
                cbLm.DataSource = new BindingSource(dictLM, null);
                cbLm.DisplayMember = "Value";
                cbLm.ValueMember = "Key";
            }
        }

        void LoadDatabase()
        {
            // Chuyển đổi dữ liệu từ List<MonHoc> sang DataTable để gán cho DataSource của DataGridView
            List<MonHoc> monHocs = MonHocController.GetAllMonHoc();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("MaMon");
            dataTable.Columns.Add("TenMon");
            dataTable.Columns.Add("SoTinChi");
            dataTable.Columns.Add("LoaiMon");
            dataTable.Columns.Add("TongSoBuoiHoc");

            foreach (var monHoc in monHocs)
            {
                dataTable.Rows.Add(monHoc.MaMon, monHoc.TenMon, monHoc.SoTinChi, monHoc.IDLoaiMon, monHoc.TongSoBuoiHoc);
            }

            dataGridViewQLMonHoc.DataSource = dataTable;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (Valid())
            {
                var monHoc = new MonHoc
                {
                    TenMon = txtTenMon.Text,
                    SoTinChi = int.Parse(txtSoTinChi.Text),
                    IDLoaiMon = cbLm.SelectedValue.ToString(),
                    TongSoBuoiHoc = int.Parse(txtTongSoBuoiHoc.Text)
                };

                bool result = MonHocController.AddMonHoc(monHoc);
                MessageBox.Show(result ? "Thêm thành công!" : "Thêm thất bại.");
                LoadDatabase();
                Clear();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaMon.Text))
            {
                MessageBox.Show("Vui lòng chọn mã môn cần sửa!!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (Valid())
            {
                var monHoc = new MonHoc
                {
                    MaMon = txtMaMon.Text,
                    TenMon = txtTenMon.Text,
                    SoTinChi = int.Parse(txtSoTinChi.Text),
                    IDLoaiMon = cbLm.SelectedValue.ToString(),
                    TongSoBuoiHoc = int.Parse(txtTongSoBuoiHoc.Text)
                };

                bool result = MonHocController.UpdateMonHoc(monHoc);
                MessageBox.Show(result ? "Sửa thành công!" : "Sửa thất bại.");
                LoadDatabase();
                Clear();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaMon.Text))
            {
                MessageBox.Show("Vui lòng chọn mã môn cần xoá!!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Hiển thị hộp thoại xác nhận
            DialogResult confirmResult = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa môn học này không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            // Kiểm tra người dùng chọn Yes hay No
            if (confirmResult == DialogResult.Yes)
            {
                bool result = MonHocController.DeleteMonHoc(txtMaMon.Text);
                MessageBox.Show(result ? "Xóa thành công!" : "Xóa thất bại.");
                LoadDatabase();
                Clear();
            }
        }


        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridViewQLMonHoc_Click(object sender, EventArgs e)
        {
            if (dataGridViewQLMonHoc.CurrentRow != null)
            {
                txtMaMon.Text = dataGridViewQLMonHoc.CurrentRow.Cells["MaMon"].Value.ToString();
                txtTenMon.Text = dataGridViewQLMonHoc.CurrentRow.Cells["TenMon"].Value.ToString();
                txtSoTinChi.Text = dataGridViewQLMonHoc.CurrentRow.Cells["SoTinChi"].Value.ToString();
                cbLm.SelectedValue = dictLM.FirstOrDefault(x => x.Value == dataGridViewQLMonHoc.CurrentRow.Cells["LoaiMon"].Value.ToString()).Key;
                txtTongSoBuoiHoc.Text = dataGridViewQLMonHoc.CurrentRow.Cells["TongSoBuoiHoc"].Value.ToString();
            }
        }

        private bool Valid()
        {
            // Kiểm tra trường Tên môn
            if (string.IsNullOrWhiteSpace(txtTenMon.Text))
            {
                MessageBox.Show("Tên môn không được để trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTenMon.Focus();
                return false;
            }

            // Kiểm tra trường Số tín chỉ
            if (string.IsNullOrWhiteSpace(txtSoTinChi.Text))
            {
                MessageBox.Show("Số tín chỉ không được để trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSoTinChi.Focus();
                return false;
            }
            if (!int.TryParse(txtSoTinChi.Text, out int soTinChi) || soTinChi <= 0)
            {
                MessageBox.Show("Số tín chỉ phải là một số nguyên dương.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSoTinChi.Focus();
                return false;
            }

            // Kiểm tra trường Loại môn
            if (cbLm.SelectedValue == null || cbLm.SelectedValue.ToString() == "0")
            {
                MessageBox.Show("Vui lòng chọn loại môn.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbLm.Focus();
                return false;
            }

            // Kiểm tra trường Tổng số buổi học
            if (string.IsNullOrWhiteSpace(txtTongSoBuoiHoc.Text))
            {
                MessageBox.Show("Tổng số buổi học không được để trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTongSoBuoiHoc.Focus();
                return false;
            }
            if (!int.TryParse(txtTongSoBuoiHoc.Text, out int tongSoBuoiHoc) || tongSoBuoiHoc <= 0)
            {
                MessageBox.Show("Tổng số buổi học phải là một số nguyên dương.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTongSoBuoiHoc.Focus();
                return false;
            }

            // Nếu tất cả kiểm tra đều hợp lệ
            return true;
        }


        public void Clear()
        {
            txtTenMon.Text = "";
            txtMaMon.Text = "";
            txtSoTinChi.Text = "";
            txtTongSoBuoiHoc.Text = "";
            cbLm.SelectedIndex = 0;
        }
        private void btnNhapLai_Click(object sender, EventArgs e)
        {
            Clear();
        }
    }
}
