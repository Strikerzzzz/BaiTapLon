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
    public partial class QLMonHoc : Form
    {
        public QLMonHoc()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        void LoadDatabase()
        {
            try
            {
                this.dataGridViewQLMonHoc.DataSource = DataBase.GetData("SELECT * FROM MonHoc where TrangThai = 'Initialize'");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        private void QLMonHoc_Load(object sender, EventArgs e)
        {
            LoadDatabase();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "INSERT INTO MonHoc (TenMon, SoTinChi, LoaiMon, TongSoBuoiHoc, TrangThai) VALUES (@TenMon, @SoTinChi, @LoaiMon, @TongSoBuoiHoc, @TrangThai)";
                SqlParameter[] parameters = {
                    new SqlParameter("@TenMon", txtTenMon.Text),
                    new SqlParameter("@SoTinChi", txtSoTinChi.Text),
                    new SqlParameter("@LoaiMon", txtLoaiMon.Text),
                    new SqlParameter("@TongSoBuoiHoc", txtTongSoBuoiHoc.Text),
                    new SqlParameter("@TrangThai", "Initialize"),
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
            try
            {
                string query = "UPDATE  MonHoc SET TenMon = @TenMon, SoTinChi = @SoTinChi, LoaiMon = @LoaiMon, TongSoBuoiHoc = @TongSoBuoiHoc WHERE MaMon = @MaMon";
                SqlParameter[] parameters = {
                    new SqlParameter("@TenMon", txtTenMon.Text),
                    new SqlParameter("@SoTinChi", txtSoTinChi.Text),
                    new SqlParameter("@LoaiMon", txtLoaiMon.Text),
                    new SqlParameter("@TongSoBuoiHoc", txtTongSoBuoiHoc.Text),
                    new SqlParameter("@MaMon", txtMaMon.Text),
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
                string query = "UPDATE MonHoc SET TrangThai = @TrangThai WHERE MaMon = @MaMon";
                SqlParameter[] parameters = {
                new SqlParameter("@MaMon", txtMaMon.Text),
                new SqlParameter("@TrangThai", "Deleted"),
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
    }
}
