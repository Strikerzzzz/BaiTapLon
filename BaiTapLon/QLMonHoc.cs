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
        void Valid()
        {
            if (string.IsNullOrEmpty(txtTenMon.Text))
            {
                MessageBox.Show("Vui lòng nhập họ tên môn.");
                return;
            }
            if (string.IsNullOrEmpty(txtSoTinChi.Text)  || !txtSoTinChi.Text.All(char.IsDigit))
            {
                MessageBox.Show("Vui lòng nhập số tín chỉ hợp lệ .");
                return;
            }
            //if (string.IsNullOrEmpty(cbGioiTinh.Text))
            //{
            //    MessageBox.Show("Vui lòng chọn giới tính.");
            //    return;
            //}
            if (string.IsNullOrEmpty(txtTongSoBuoiHoc.Text) || !txtTongSoBuoiHoc.Text.All(char.IsDigit))
            {
                MessageBox.Show("Vui lòng nhập số tín chỉ hợp lệ .");
                return;
            }
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        void LoadDatabase()
        {
            try
            {
                this.dataGridViewQLMonHoc.DataSource = DataBase.GetData("SELECT MaMon, TenMon, SoTinChi, IDLoaiMon, TongSoBuoiHoc FROM MonHoc where TrangThai = 'Initialize'");
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
                string query = "INSERT INTO MonHoc (TenMon, SoTinChi,  TongSoBuoiHoc, TrangThai) VALUES (@TenMon, @SoTinChi, @TongSoBuoiHoc, @TrangThai)";
                SqlParameter[] parameters = {
                    new SqlParameter("@TenMon", txtTenMon.Text),
                    new SqlParameter("@SoTinChi", txtSoTinChi.Text),
                   // new SqlParameter("@LoaiMon",""),
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

        private void dataGridViewQLMonHoc_Click(object sender, EventArgs e)
        {
            try
            {
                txtMaMon.Text = dataGridViewQLMonHoc.CurrentRow.Cells[0].Value.ToString();
                txtTenMon.Text = dataGridViewQLMonHoc.CurrentRow.Cells[1].Value.ToString();
                txtSoTinChi.Text = dataGridViewQLMonHoc.CurrentRow.Cells[2].Value.ToString();
                txtLoaiMon.Text = dataGridViewQLMonHoc.CurrentRow.Cells[3].Value.ToString();
                txtTongSoBuoiHoc.Text = dataGridViewQLMonHoc.CurrentRow.Cells[4].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi hiển thị thông tin môn học: " + ex.Message);
            }
        }
    }
}
