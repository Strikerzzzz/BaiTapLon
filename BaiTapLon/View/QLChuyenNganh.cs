﻿using BaiTapLon.Controller;
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

            ChuyenNganh chuyenNganh = new ChuyenNganh
            {
                TenChuyenNganh = txtTenCN.Text
            };

            bool result = ChuyenNganhController.AddChuyenNganh(chuyenNganh);
            MessageBox.Show(result ? "Thêm chuyên ngành thành công!" : "Thêm chuyên ngành thất bại.");
            if (result) LoadDatabase();
            ClearFields();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaCN.Text) || string.IsNullOrEmpty(txtTenCN.Text))
            {
                MessageBox.Show("Vui lòng nhập mã và tên chuyên ngành.");
                return;
            }

            ChuyenNganh chuyenNganh = new ChuyenNganh
            {
                MaChuyenNganh = txtMaCN.Text,
                TenChuyenNganh = txtTenCN.Text
            };

            bool result = ChuyenNganhController.UpdateChuyenNganh(chuyenNganh);
            MessageBox.Show(result ? "Sửa chuyên ngành thành công!" : "Sửa chuyên ngành thất bại.");
            if (result) LoadDatabase();
            ClearFields();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaCN.Text))
            {
                MessageBox.Show("Vui lòng chọn chuyên ngành cần xóa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DialogResult confirmResult = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa chuyên ngành này không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );
            if (confirmResult == DialogResult.Yes)
            {
                bool result = ChuyenNganhController.DeleteChuyenNganh(txtMaCN.Text);
                MessageBox.Show(result ? "Xóa chuyên ngành thành công!" : "Xóa chuyên ngành thất bại.");
                if (result) LoadDatabase(); ClearFields();
            }
            else
            {
                MessageBox.Show("Hủy bỏ xóa chuyên ngành.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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

        private void button1_Click(object sender, EventArgs e)
        {
            ClearFields();
        }
    }
}
