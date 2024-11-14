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
    public partial class QLChuyenNganh : Form
    {
        public QLChuyenNganh()
        {
            InitializeComponent();
        }

        private void QLChuyenNganh_Load(object sender, EventArgs e)
        {
            LoadDatabase();
        }

        void LoadDatabase()
        {
            try
            {
                dataGridView1.DataSource = ChuyenNganhController.GetAllChuyenNganh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTenCN.Text))
            {
                MessageBox.Show("Vui lòng nhập tên chuyên ngành.");
                return;
            }

            bool result = ChuyenNganhController.AddChuyenNganh(txtTenCN.Text);
            MessageBox.Show(result ? "Thêm chuyên ngành thành công!" : "Thêm chuyên ngành thất bại.");
            if (result) LoadDatabase();
            ClearFields();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaCN.Text))
            {
                MessageBox.Show("Vui lòng chọn chuyên ngành cần sửa.");
                return;
            }

            bool result = ChuyenNganhController.UpdateChuyenNganh(txtMaCN.Text, txtTenCN.Text);
            MessageBox.Show(result ? "Sửa chuyên ngành thành công!" : "Sửa chuyên ngành thất bại.");
            if (result) LoadDatabase();
            ClearFields();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaCN.Text))
            {
                MessageBox.Show("Vui lòng chọn chuyên ngành cần xóa.");
                return;
            }

            bool result = ChuyenNganhController.DeleteChuyenNganh(txtMaCN.Text);
            MessageBox.Show(result ? "Xóa chuyên ngành thành công!" : "Xóa chuyên ngành thất bại.");
            if (result) LoadDatabase();
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            try
            {
                txtMaCN.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                txtTenCN.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi hiển thị thông tin chuyên ngành: " + ex.Message);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ClearFields()
        {
            txtMaCN.Clear();
            txtTenCN.Clear();
        }
    }
}
