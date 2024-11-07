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
        void LoadDatabase()
        {
            try
            {
                this.dataGridView1.DataSource = DataBase.GetData("SELECT * FROM ChuyenNganh where TrangThai = 'Initialize'");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        private void QLChuyenNganh_Load(object sender, EventArgs e)
        {
            LoadDatabase();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTenCN.Text))
            {
                MessageBox.Show("Vui lòng nhập tên chuyên ngành.");
                return;
            }
            try
            {
                string query = "INSERT INTO ChuyenNganh (TenChuyenNganh, TrangThai) VALUES (@TenChuyenNganh, 'Initialize')";
                SqlParameter[] parameters = {
                    new SqlParameter("@TenChuyenNganh", txtTenCN.Text)
                };

                bool result = new DataBase().UpdateData(query, parameters);

                if (result)
                {
                    MessageBox.Show("Thêm chuyên ngành thành công!");
                    LoadDatabase();
                    ClearFields();
                }
                else
                {
                    MessageBox.Show("Thêm chuyên ngành thất bại.");
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
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn chuyên ngành cần sửa.");
                return;
            }
            if (string.IsNullOrEmpty(txtTenCN.Text))
            {
                MessageBox.Show("Vui lòng nhập tên chuyên ngành.");
                return;
            }
            try
            {
                string query = "UPDATE  ChuyenNganh SET TenChuyenNganh = @TenChuyenNganh WHERE MaChuyenNganh = @MaChuyenNganh";
                SqlParameter[] parameters = {
                    new SqlParameter("@TenChuyenNganh", txtTenCN.Text),
                    new SqlParameter("@MaChuyenNganh", txtMaCN.Text),
                };
                bool result = new DataBase().UpdateData(query, parameters);
                if (result)
                {
                    MessageBox.Show("Sửa chuyên ngành thành công!");
                    LoadDatabase();
                    ClearFields();
                }
                else
                {
                    MessageBox.Show("Sửa chuyên ngành thất bại.");
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
                txtMaCN.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                txtTenCN.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi hiển thị thông tin tên chuyên ngành: " + ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtMaCN.Text))
            {
                MessageBox.Show("Vui lòng chọn chuyên ngành cần xoá.");
                return;
            }
            try
            {
                string query = "UPDATE ChuyenNganh SET TrangThai = 'Deleted' WHERE MaChuyenNganh = @MaChuyenNganh";
                SqlParameter[] parameters = {
                    new SqlParameter("@MaChuyenNganh", txtMaCN.Text)
                };
                bool result = new DataBase().UpdateData(query, parameters);
                if (result)
                {
                    MessageBox.Show("Xóa chuyên ngành thành công!");
                    LoadDatabase();
                }
                else
                {
                    MessageBox.Show("Xóa chuyên ngành thất bại.");
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

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ClearFields()
        {
            txtMaCN.Text = string.Empty;
            txtTenCN.Text = string.Empty;
           
        }
    }
}
