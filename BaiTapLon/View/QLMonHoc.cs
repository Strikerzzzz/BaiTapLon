using BaiTapLon.Controller;
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
            dataGridViewQLMonHoc.DataSource = MonHocController.GetAllMonHoc();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (Valid())
            {
                bool result = MonHocController.AddMonHoc(txtTenMon.Text, int.Parse(txtSoTinChi.Text), cbLm.SelectedValue.ToString(), int.Parse(txtTongSoBuoiHoc.Text));
                MessageBox.Show(result ? "Thêm thành công!" : "Thêm thất bại.");
                LoadDatabase();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (Valid())
            {
                bool result = MonHocController.UpdateMonHoc(txtMaMon.Text, txtTenMon.Text, int.Parse(txtSoTinChi.Text), cbLm.SelectedValue.ToString(), int.Parse(txtTongSoBuoiHoc.Text));
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
                txtMaMon.Text = dataGridViewQLMonHoc.CurrentRow.Cells[0].Value.ToString();
                txtTenMon.Text = dataGridViewQLMonHoc.CurrentRow.Cells[1].Value.ToString();
                txtSoTinChi.Text = dataGridViewQLMonHoc.CurrentRow.Cells[2].Value.ToString();
                cbLm.SelectedValue = dictLM.FirstOrDefault(x => x.Value == dataGridViewQLMonHoc.CurrentRow.Cells[3].Value.ToString()).Key;
                txtTongSoBuoiHoc.Text = dataGridViewQLMonHoc.CurrentRow.Cells[4].Value.ToString();
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
