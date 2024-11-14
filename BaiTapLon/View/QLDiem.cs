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
    public partial class QLDiem : Form
    {
        private DiemController _controller = new DiemController();

        public QLDiem()
        {
            InitializeComponent();
        }

        private void QLDiem_Load(object sender, EventArgs e)
        {
            LoadComboBoxData();
            LoadDatabase();
        }

        private void LoadComboBoxData()
        {
            DataTable dt;
            // Môn học
            dt = DataBase.GetData("SELECT MaMon, TenMon FROM MonHoc where TrangThai = 'Initialize'");
            _controller.LoadListDB(dt, _controller.dictMH);
            cboTenMon.DataSource = new BindingSource(_controller.dictMH, null);
            cboTenMon.DisplayMember = "Value";
            cboTenMon.ValueMember = "Key";
            // Loại điểm
            dt = DataBase.GetData("SELECT IDLoaiDiem, TenLoaiDiem FROM LoaiDiem where TrangThai = 'Initialize'");
            _controller.LoadListDB(dt, _controller.dictLD);
            cboTenLoaiDiem.DataSource = new BindingSource(_controller.dictLD, null);
            cboTenLoaiDiem.DisplayMember = "Value";
            cboTenLoaiDiem.ValueMember = "Key";
            // Sinh viên
            dt = DataBase.GetData("SELECT MaSV, CONCAT(HoDem, ' ', Ten,'-',MaSV) FROM SinhVien where TrangThai = 'Initialize'");
            _controller.LoadListDB(dt, _controller.dictSV);
            cboTenSV.DataSource = new BindingSource(_controller.dictSV, null);
            cboTenSV.DisplayMember = "Value";
            cboTenSV.ValueMember = "Key";
        }

        private void LoadDatabase()
        {
            this.dataGridView1.DataSource = _controller.LoadDiemData();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            Diem diem = new Diem
            {
                MaSV = cboTenSV.SelectedValue?.ToString(),
                MaMon = cboTenMon.SelectedValue?.ToString(),
                IDLoaiDiem = cboTenLoaiDiem.SelectedValue?.ToString(),
                GiaTriDiem = double.Parse(txtGiaTriDiem.Text),
                LanThi = int.Parse(txtLanThi.Text)
            };

            if (_controller.AddDiem(diem))
            {
                MessageBox.Show("Thêm điểm thành công!");
                LoadDatabase();
            }
            else
            {
                MessageBox.Show("Thêm điểm thất bại.");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            Diem diem = new Diem
            {
                IDDiem = int.Parse(txtID.Text),
                MaSV = cboTenSV.SelectedValue?.ToString(),
                MaMon = cboTenMon.SelectedValue?.ToString(),
                IDLoaiDiem = cboTenLoaiDiem.SelectedValue?.ToString(),
                GiaTriDiem = double.Parse(txtGiaTriDiem.Text),
                LanThi = int.Parse(txtLanThi.Text)
            };

            if (_controller.UpdateDiem(diem))
            {
                MessageBox.Show("Sửa điểm thành công!");
                LoadDatabase();
            }
            else
            {
                MessageBox.Show("Sửa điểm thất bại.");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            int idDiem = int.Parse(txtID.Text);
            if (_controller.DeleteDiem(idDiem))
            {
                MessageBox.Show("Xóa thành công!");
                LoadDatabase();
            }
            else
            {
                MessageBox.Show("Xóa thất bại.");
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void dataGridView1_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    // Lấy giá trị từ hàng hiện tại trong DataGridView
                    txtID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();

                    // Hiển thị thông tin Sinh viên
                    string tenSinhVien = dataGridView1.CurrentRow.Cells[1].Value?.ToString();
                    string maSV = _controller.dictSV.FirstOrDefault(x => x.Value == tenSinhVien).Key;
                    if (!string.IsNullOrEmpty(maSV))
                    {
                        cboTenSV.SelectedValue = maSV;
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy mã sinh viên cho tên: " + tenSinhVien);
                    }

                    // Hiển thị thông tin Môn học
                    string tenMon = dataGridView1.CurrentRow.Cells[2].Value?.ToString();
                    string maMon = _controller.dictMH.FirstOrDefault(x => x.Value == tenMon).Key;
                    if (!string.IsNullOrEmpty(maMon))
                    {
                        cboTenMon.SelectedValue = maMon;
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy mã môn học cho tên: " + tenMon);
                    }

                    // Hiển thị thông tin Loại điểm
                    string tenLoaiDiem = dataGridView1.CurrentRow.Cells[3].Value?.ToString();
                    string idLoaiDiem = _controller.dictLD.FirstOrDefault(x => x.Value == tenLoaiDiem).Key;
                    if (!string.IsNullOrEmpty(idLoaiDiem))
                    {
                        cboTenLoaiDiem.SelectedValue = idLoaiDiem;
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy mã loại điểm cho tên: " + tenLoaiDiem);
                    }

                    // Hiển thị Giá trị điểm và Lần thi
                    txtGiaTriDiem.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                    txtLanThi.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi hiển thị thông tin điểm: " + ex.Message);
            }
        }

    }
}
