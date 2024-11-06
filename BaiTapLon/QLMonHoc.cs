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
                this.dataGridViewQLMonHoc.DataSource = DataBase.GetData("SELECT * FROM MonHoc");
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
                MessageBox.Show("Thêm nhân viên thành công!");
                LoadDatabase();
            }
            else
            {
                MessageBox.Show("Thêm nhân viên thất bại.");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            //if (lsvNhanVien.SelectedItems.Count > 0)
            //{
            //    ListViewItem selectedItem = lsvNhanVien.SelectedItems[0];
            //    DataTable dt = DataBase.GetData("SELECT * FROM nhanvien");
            //    int maNhanVien = Convert.ToInt32(dt.Rows[selectedItem.Index]["MaNhanVien"]);

            //    string query = "UPDATE nhanvien SET Hotennhanvien = @Hotennhanvien, NgaySinh = @NgaySinh, DiaChi = @DiaChi, DienThoai = @DienThoai WHERE MaNhanVien = @MaNhanVien";
            //    SqlParameter[] parameters = {
            //        new SqlParameter("@Hotennhanvien", txtHoTen.Text),
            //        new SqlParameter("@NgaySinh", dateTimePickerNgaySinh.Value),
            //        new SqlParameter("@DiaChi", txtDiaChi.Text),
            //        new SqlParameter("@DienThoai", txtDienThoai.Text),
            //        new SqlParameter("@MaNhanVien", maNhanVien)
            //    };

            //    bool result = new DataBase().UpdateData(query, parameters);
            //    if (result)
            //    {
            //        MessageBox.Show("Cập nhật nhân viên thành công!");
            //        LoadListView();
            //    }
            //    else
            //    {
            //        MessageBox.Show("Cập nhật nhân viên thất bại.");
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("Vui lòng chọn nhân viên để sửa.");
            //}
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            //if (lsvNhanVien.SelectedItems.Count > 0 && DialogResult.Yes == MessageBox.Show("Bạn có chắc là muốn xóa?", "Xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            //{
            //    ListViewItem selectedItem = lsvNhanVien.SelectedItems[0];
            //    DataTable dt = DataBase.GetData("SELECT * FROM nhanvien");
            //    int maNhanVien = Convert.ToInt32(dt.Rows[selectedItem.Index]["MaNhanVien"]);

            //    string query = "DELETE FROM nhanvien WHERE MaNhanVien = @MaNhanVien";
            //    SqlParameter[] parameters = {
            //        new SqlParameter("@MaNhanVien", maNhanVien)
            //    };

            //    bool result = new DataBase().DeleteData(query, parameters);
            //    if (result)
            //    {
            //        MessageBox.Show("Xóa nhân viên thành công!");
            //        LoadListView();
            //    }
            //    else
            //    {
            //        MessageBox.Show("Xóa nhân viên thất bại.");
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("Vui lòng chọn nhân viên để xóa.");
            //}
        }
    }
}
