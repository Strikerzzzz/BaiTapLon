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
        public QLLoaiDiem()
        {
            InitializeComponent();
        }
        bool Valid()
        {
            if (string.IsNullOrEmpty(txtTenLoaiDiem.Text))
            {
                MessageBox.Show("Vui lòng nhập tên loại điểm.");
                return false;
            }
            if (string.IsNullOrEmpty(txtTiLe.Text) || !double.TryParse(txtTiLe.Text, out _))
            {
                MessageBox.Show("Vui lòng nhập tỉ lệ.");
                return false;
            }
            return true;
        }
        void LoadDatabase()
        {
            try
            {
                this.dataGridView1.DataSource = DataBase.GetData("SELECT IDLoaiDiem, TenLoaiDiem, Tile FROM LoaiDiem where TrangThai = 'Initialize'");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void QLLoaiDiem_Load(object sender, EventArgs e)
        {
            LoadDatabase();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!Valid())
            {
                return;
            }
            try
            {
                string query = "INSERT INTO LoaiDiem ( TenLoaiDiem, TiLe, TrangThai) VALUES ( @TenLoaiDiem, @TiLe, 'Initialize')";
                SqlParameter[] parameters = {
                    new SqlParameter("@TenLoaiDiem", txtTenLoaiDiem.Text),
                    new SqlParameter("@TiLe", txtTiLe.Text),
                };

                bool result = new DataBase().UpdateData(query, parameters);

                if (result)
                {
                    MessageBox.Show("Thêm môn học thành công!");
                    LoadDatabase();
                }
                else
                {
                    MessageBox.Show("Thêm môn học thất bại.");
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
            if (!Valid())
            {
                return;
            }
            try
            {
                string query = "UPDATE  LoaiDiem SET TenLoaiDiem = @TenLoaiDiem, TiLe = @Tile WHERE IDLoaiDiem = @IDLoaiDiem";
                SqlParameter[] parameters = {
                    new SqlParameter("@TenLoaiDiem", txtTenLoaiDiem.Text),
                    new SqlParameter("@TiLe", txtTiLe.Text),
                    new SqlParameter("@IDLoaiDiem", txtID.Text),
                };
                bool result = new DataBase().UpdateData(query, parameters);
                if (result)
                {
                    MessageBox.Show("Sửa môn học thành công!");
                    LoadDatabase();
                }
                else
                {
                    MessageBox.Show("Sửa môn học thất bại.");
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

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "UPDATE LoaiDiem SET TrangThai = 'Deleted' WHERE IDLoaiDiem = @ID";
                SqlParameter[] parameters = {
                new SqlParameter("@ID", txtID.Text)
            };
                bool result = new DataBase().UpdateData(query, parameters);
                if (result)
                {
                    MessageBox.Show("Xóa môn học thành công!");
                    LoadDatabase();
                }
                else
                {
                    MessageBox.Show("Xóa môn học thất bại.");
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
                if (dataGridView1.CurrentRow != null)
                {
                    txtID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    txtTenLoaiDiem.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    txtTiLe.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi hiển thị thông tin môn học: " + ex.Message);
            }
        }
    }
}
