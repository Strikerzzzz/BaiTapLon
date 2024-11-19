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
    public partial class QLLoaiDiem : Form
    {
        private readonly LoaiDiemController _controller;
        public QLLoaiDiem()
        {
            InitializeComponent();
            _controller = new LoaiDiemController();
        }

        private void QLLoaiDiem_Load(object sender, EventArgs e)
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
            var loaiDiem = new LoaiDiem
            {
                TenLoaiDiem = txtTenLoaiDiem.Text,
                TiLe = double.TryParse(txtTiLe.Text, out var tiLe) ? tiLe : 0
            };

            if (_controller.AddLoaiDiem(loaiDiem, out string message))
            {
                MessageBox.Show(message);
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
            var loaiDiem = new LoaiDiem
            {
                IDLoaiDiem = int.TryParse(txtID.Text, out var id) ? id : 0,
                TenLoaiDiem = txtTenLoaiDiem.Text,
                TiLe = double.TryParse(txtTiLe.Text, out var tiLe) ? tiLe : 0
            };

            if (_controller.UpdateLoaiDiem(loaiDiem, out string message))
            {
                MessageBox.Show(message);
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
            if (int.TryParse(txtID.Text, out var id))
            {
                if (_controller.DeleteLoaiDiem(id, out string message))
                {
                    MessageBox.Show(message);
                    LoadData();
                    Clear();
                }
                else
                {
                    MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn hàng cần xoá!!!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                txtID.Text = dataGridView1.CurrentRow.Cells[0].Value?.ToString();
                txtTenLoaiDiem.Text = dataGridView1.CurrentRow.Cells[1].Value?.ToString();
                txtTiLe.Text = dataGridView1.CurrentRow.Cells[2].Value?.ToString();
            }
        }
        public void Clear()
        {
            txtID.Text = "";
            txtTenLoaiDiem.Text = "";
            txtTiLe.Text = "";
        }
        private void btnNhapLai_Click(object sender, EventArgs e)
        {
            Clear();
        }
    }
}
