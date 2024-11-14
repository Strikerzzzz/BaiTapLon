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
        public QLLoaiMon()
        {
            InitializeComponent();
        }

        private void QLLoaiMon_Load(object sender, EventArgs e)
        {
            LoadDatabase();
        }
        void LoadDatabase()
        {
            try
            {
                this.dataGridView1.DataSource = DataBase.GetData("SELECT IDLoaiMon,LoaiMon FROM LoaiMon where TrangThai = 'Initialize'");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtLoaiMon.Text))
            {
                MessageBox.Show("Vui lòng nhập loại môn.");
                return;
            }
            try
            {
                string query = "INSERT INTO LoaiMon (LoaiMon, TrangThai) VALUES (@LoaiMon, 'Initialize')";
                SqlParameter[] parameters = {
                    new SqlParameter("@LoaiMon", txtLoaiMon.Text)
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
            if (string.IsNullOrEmpty(txtLoaiMon.Text))
            {
                MessageBox.Show("Vui lòng nhập loại môn.");
                return;
            }
            try
            {
                string query = "UPDATE  LoaiMon SET LoaiMon = @LoaiMon WHERE IDLoaiMon = @IDLoaiMon";
                SqlParameter[] parameters = {
                    new SqlParameter("@LoaiMon", txtLoaiMon.Text),
                    new SqlParameter("@IDLoaiMon", txtID.Text),
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
                string query = "UPDATE LoaiMon SET TrangThai = 'Deleted' WHERE IDLoaiMon = @IDLoaiMon";
                SqlParameter[] parameters = {
                    new SqlParameter("@IDLoaiMon", txtID.Text)
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
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            try
            {
                txtID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                txtLoaiMon.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi hiển thị thông tin loại môn: " + ex.Message);
            }
        }
    }

        
}
