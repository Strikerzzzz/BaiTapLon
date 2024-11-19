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
    public partial class QLLoaiMon : Form
    {
        private readonly LoaiMonController _controller;

        public QLLoaiMon()
        {
            InitializeComponent();
            _controller = new LoaiMonController();
        }

        private void QLLoaiMon_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            _controller.LoadData(dataGridView1);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            var loaiMon = new LoaiMon
            {
                LoaiMonName = txtLoaiMon.Text
            };

            if (_controller.AddLoaiMon(loaiMon, out string message))
            {
                MessageBox.Show(message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                Clear();
            }
            else
            {
                MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            var loaiMon = new LoaiMon
            {
                IDLoaiMon = int.TryParse(txtID.Text, out int id) ? id : 0,
                LoaiMonName = txtLoaiMon.Text
            };

            if (_controller.UpdateLoaiMon(loaiMon, out string message))
            {
                MessageBox.Show(message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                Clear();
            }
            else
            {
                MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            // Kiểm tra nếu ID hợp lệ
            if (int.TryParse(txtID.Text, out int id))
            {
                // Hiển thị hộp thoại xác nhận trước khi xóa
                DialogResult confirmResult = MessageBox.Show(
                    "Bạn có chắc chắn muốn xóa loại môn này không?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                // Nếu người dùng chọn "Yes", thực hiện xóa
                if (confirmResult == DialogResult.Yes)
                {
                    if (_controller.DeleteLoaiMon(id, out string message))
                    {
                        MessageBox.Show(message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                        Clear();
                    }
                    else
                    {
                        MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                // Nếu người dùng chọn "No", không thực hiện xóa
                else
                {
                    MessageBox.Show("Hủy bỏ xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Chọn hàng cần xoá!!.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void dataGridView1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                txtID.Text = dataGridView1.CurrentRow.Cells[0].Value?.ToString();
                txtLoaiMon.Text = dataGridView1.CurrentRow.Cells[1].Value?.ToString();
            }
        }
        public void Clear()
        {
            txtID.Text = "";
            txtLoaiMon.Text = "";
        }
        private void btnNhapLai_Click(object sender, EventArgs e)
        {
            Clear();
        }
    }        
}
