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
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
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
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            bool result = MonHocController.DeleteMonHoc(txtMaMon.Text);
            MessageBox.Show(result ? "Xóa thành công!" : "Xóa thất bại.");
            LoadDatabase();
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
            if (string.IsNullOrEmpty(txtTenMon.Text) || !txtSoTinChi.Text.All(char.IsDigit) || !txtTongSoBuoiHoc.Text.All(char.IsDigit))
            {
                MessageBox.Show("Vui lòng nhập dữ liệu hợp lệ.");
                return false;
            }
            return true;
        }
    }
}
