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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaiTapLon
{
    public partial class QLHocKy : Form
    {
        private HocKyController hocKyController;
        private string[] years = Enumerable.Range(2000, DateTime.Now.Year - 2000 + 1)
                                           .Select(year => "Năm " + year)
                                           .ToArray();

        public QLHocKy()
        {
            InitializeComponent();
            hocKyController = new HocKyController();
        }

        private void LoadDatabase()
        {
            try
            {
                dataGridView1.DataSource = hocKyController.GetAllHocKy();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        private void QLHocKy_Load(object sender, EventArgs e)
        {
            cboTenHocKy.Items.AddRange(new string[] { "Chọn tên học kỳ", "Học kỳ 1", "Học kỳ 2", "Học kỳ phụ" });
            cboTenHocKy.SelectedIndex = 0;
            cboNam.DataSource = new BindingSource(years, null);
            LoadDatabase();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (cboTenHocKy.SelectedIndex == 0)
            {
                MessageBox.Show("Vui lòng chọn thông tin về tên học kỳ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int nam = int.Parse(cboNam.Text.Replace("Năm ", ""));
            var hocKy = new HocKy { TenHocKy = cboTenHocKy.Text, Nam = nam };

            if (hocKyController.AddHocKy(hocKy))
            {
                MessageBox.Show("Thêm học kỳ thành công!");
                LoadDatabase();
                ClearFields();
            }
            else
            {
                MessageBox.Show("Thêm học kỳ thất bại.");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtID.Text))
            {
                MessageBox.Show("Vui lòng chọn mã học kỳ cần sửa!!!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!ValidateFields()) return;

            int nam = int.Parse(cboNam.Text.Replace("Năm ", ""));
            var hocKy = new HocKy
            {
                IDHocKy = int.Parse(txtID.Text),
                TenHocKy = cboTenHocKy.Text,
                Nam = nam
            };

            if (hocKyController.UpdateHocKy(hocKy))
            {
                MessageBox.Show("Sửa học kỳ thành công!");
                LoadDatabase();
                ClearFields();
            }
            else
            {
                MessageBox.Show("Sửa học kỳ thất bại.");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtID.Text))
            {
                MessageBox.Show("Vui lòng chọn mã học kỳ cần xóa!!!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                int idHocKy = int.Parse(txtID.Text);

                if (hocKyController.DeleteHocKy(idHocKy))
                {
                    MessageBox.Show("Xóa học kỳ thành công!");
                    LoadDatabase();
                    ClearFields();
                }
                else
                {
                    MessageBox.Show("Xóa học kỳ thất bại.");
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Mã học kỳ không hợp lệ. Vui lòng chọn học kỳ hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            txtID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            cboTenHocKy.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            cboNam.Text = "Năm " + dataGridView1.CurrentRow.Cells[2].Value.ToString();
        }

        private void ClearFields()
        {
            txtID.Text = "";
            cboTenHocKy.SelectedIndex = 0;
            cboNam.SelectedIndex = 0;
        }
        
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private bool ValidateFields()
        {
            if (cboTenHocKy.SelectedIndex == 0)
            {
                MessageBox.Show("Vui lòng chọn thông tin về tên học kỳ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            int selectedYear = int.Parse(cboNam.Text.Replace("Năm ", ""));
            if (selectedYear > DateTime.Now.Year)
            {
                MessageBox.Show("Năm không được lớn hơn năm hiện tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
    }
}
