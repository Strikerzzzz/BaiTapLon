using BaiTapLon.Controller;
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

namespace BaiTapLon.View.TimKiem
{
    public partial class TKDiemDanh : Form
    {
        private DiemDanhController diemDanhController;
        public TKDiemDanh()
        {
            InitializeComponent();
            diemDanhController = new DiemDanhController();
        }

        private void TKDiemDanh_Load(object sender, EventArgs e)
        {
            cboLuaChon.Items.AddRange(new string[] { "Lựa chọn giá trị","ID điểm danh", "Tên sinh viên", "Tên lớp", "Ngày điểm danh", "Trạng thái điểm danh" });
            cboLuaChon.SelectedIndex = 0; // Mặc định chọn tiêu chí đầu tiên

            // Load toàn bộ dữ liệu vào DataGridView
            LoadDatabase();
        }
        private void LoadDatabase()
        {
            try
            {
                // Lấy dữ liệu từ controller và hiển thị lên DataGridView
                dataGridView1.DataSource = diemDanhController.LoadDiemDanhData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (cboLuaChon.SelectedIndex == 0)
            {
                MessageBox.Show("Vui lòng chọn tiêu chí tìm kiếm.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string luaChon = cboLuaChon.Text; // Tiêu chí tìm kiếm
            string giaTri = txtGiaTri.Text.Trim(); // Giá trị nhập vào

            if (string.IsNullOrEmpty(giaTri))
            {
                // Nếu không nhập giá trị, load toàn bộ dữ liệu
                LoadDatabase();
                return;
            }

            try
            {
                // Lấy toàn bộ dữ liệu từ controller
                DataTable dt = diemDanhController.LoadDiemDanhData();
                DataView dv = dt.DefaultView;

                // Áp dụng bộ lọc theo tiêu chí tìm kiếm
                switch (luaChon)
                {
                    case "ID điểm danh":
                        dv.RowFilter = $"CONVERT(IDDiemDanh, 'System.String') LIKE '%{giaTri}%'";
                        break;

                    case "Tên sinh viên":
                        dv.RowFilter = $"TenSinhVien LIKE '%{giaTri}%'";
                        break;

                    case "Tên lớp":
                        dv.RowFilter = $"TenLop LIKE '%{giaTri}%'";
                        break;

                    case "Ngày điểm danh":
                        if (DateTime.TryParse(giaTri, out DateTime ngay))
                        {
                            dv.RowFilter = $"NgayDiemDanh = #{ngay:yyyy-MM-dd}#";
                        }
                        else
                        {
                            MessageBox.Show("Vui lòng nhập ngày hợp lệ (định dạng yyyy-MM-dd).", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        break;

                    case "Trạng thái điểm danh":
                        dv.RowFilter = $"TTDiemDanh LIKE '%{giaTri}%'";
                        break;

                    default:
                        MessageBox.Show("Tiêu chí tìm kiếm không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                }

                // Hiển thị kết quả tìm kiếm trên DataGridView
                dataGridView1.DataSource = dv.ToTable();
                if (dv.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy kết quả phù hợp.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
    }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
