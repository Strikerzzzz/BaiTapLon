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
        private string[] years = Enumerable.Range(2000, 1001).Select(year => "Năm " + year).ToArray();

        public QLHocKy()
        {
            InitializeComponent();
        }
        void LoadDatabase()
        {
            try
            {
                this.dataGridView1.DataSource = DataBase.GetData("SELECT IDHocKy, TenHocKy, Nam FROM HocKy where TrangThai = 'Initialize'");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        private void QLHocKy_Load(object sender, EventArgs e)
        {
            cboTenHocKy.Items.Add("Chọn tên học kỳ");
            cboTenHocKy.Items.Add("Học kỳ 1");
            cboTenHocKy.Items.Add("Học kỳ 2");
            cboTenHocKy.Items.Add("Học kỳ phụ");
            cboTenHocKy.SelectedIndex = 0;
           
            cboNam.DataSource = new BindingSource(this.years, null);
            LoadDatabase();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboTenHocKy.SelectedIndex == 0)
                {
                    MessageBox.Show("Vui lòng chọn thông tin về tên học kỳ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                int nam;
                string yearString = cboNam.Text.Replace("Năm ", ""); 

                if (!int.TryParse(yearString, out nam))
                {
                    MessageBox.Show("Năm không hợp lệ, vui lòng chọn một năm hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string query = "INSERT INTO HocKy (TenHocKy, Nam, TrangThai) VALUES (@TenHocKy,@Nam, 'Initialize')";
                SqlParameter[] parameters = {
                    new SqlParameter("@TenHocKy", cboTenHocKy.Text),
                    new SqlParameter("@Nam",nam)
                };

                bool result = new DataBase().UpdateData(query, parameters);

                if (result)
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
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi cơ sở dữ liệu: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateFields())
                {
                    return;
                }
                int nam;
                string yearString = cboNam.Text.Replace("Năm ", "");

                if (!int.TryParse(yearString, out nam))
                {
                    MessageBox.Show("Năm không hợp lệ, vui lòng chọn một năm hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                string query = "UPDATE  HocKy SET TenHocKy = @TenHocKy, Nam = @Nam WHERE IDHocKy = @IDHocKy";
                SqlParameter[] parameters = {
                    new SqlParameter("@TenHocKy", cboTenHocKy.Text),
                    new SqlParameter("@Nam", nam),
                    new SqlParameter("@IDHocKy", txtID.Text),
                };
                bool result = new DataBase().UpdateData(query, parameters);
                if (result)
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
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi cơ sở dữ liệu: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }

        private void ClearFields()
        {
            txtID.Text = "";
            cboTenHocKy.SelectedIndex = 0;
            cboNam.SelectedIndex = 0;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "UPDATE HocKy SET TrangThai = 'Deleted' WHERE IDHocKy = @IDHocKy";
                SqlParameter[] parameters = {
                    new SqlParameter("@IDHocKy", txtID.Text)
                };
                bool result = new DataBase().UpdateData(query, parameters);
                if (result)
                {
                    MessageBox.Show("Xóa học kỳ thành công!");
                    LoadDatabase();
                }
                else
                {
                    MessageBox.Show("Xóa học kỳ thất bại.");
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi cơ sở dữ liệu: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            try
            {
                txtID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                cboTenHocKy.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                cboNam.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi hiển thị thông tin học kỳ: " + ex.Message);
            }
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

            int currentYear = DateTime.Now.Year;
            string selectedYearStr = cboNam.SelectedItem.ToString();
            int selectedYear = int.Parse(selectedYearStr.Replace("Năm ", ""));
            if (selectedYear > currentYear)
            {
                MessageBox.Show($"Năm không được lớn hơn năm hiện tại ({currentYear}).", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
    }
}
