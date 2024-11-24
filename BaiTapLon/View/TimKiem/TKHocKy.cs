using BaiTapLon.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using BaiTapLon.Model;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaiTapLon.View.TimKiem
{
    public partial class TKHocKy : Form
    {
        private HocKyController hocKyController;
        public TKHocKy()
        {
            InitializeComponent();
            hocKyController = new HocKyController();
        }
           private void TKHocKy_Load(object sender, EventArgs e)
        {
            cboLuaChon.Items.AddRange(new string[] { "Mã học kỳ", "Tên học kỳ", "Năm" });
            cboLuaChon.SelectedIndex = 0; // Chọn mặc định là "Mã học kỳ"

            // Tải dữ liệu ban đầu (toàn bộ học kỳ)
            LoadDatabase();
        }
        private void LoadDatabase()
        {
            try
            {
                // Lấy toàn bộ dữ liệu và hiển thị trên DataGridView
                dataGridView1.DataSource = hocKyController.GetAllHocKy();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTK_Click(object sender, EventArgs e)
        {
            if (cboLuaChon.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn tiêu chí tìm kiếm.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string luaChon = cboLuaChon.Text;
            string giaTri = txtGiaTri.Text.Trim();

            if (string.IsNullOrEmpty(giaTri))
            {
                LoadDatabase();
                return;
            }

            try
            {
                // Lấy toàn bộ dữ liệu từ bảng
                DataTable dt = hocKyController.GetAllHocKy();
                DataView dv = dt.DefaultView;

                // Lọc dữ liệu theo tiêu chí
                switch (luaChon)
                {
                    case "Mã học kỳ":
                        dv.RowFilter = $"CONVERT(IDHocKy, 'System.String') LIKE '%{giaTri}%'";
                        break;

                    case "Tên học kỳ":
                        dv.RowFilter = $"TenHocKy LIKE '%{giaTri}%'";
                        break;

                    case "Năm":
                        dv.RowFilter = $"CONVERT(Nam, 'System.String') LIKE '%{giaTri}%'";
                        break;

                    default:
                        MessageBox.Show("Tiêu chí tìm kiếm không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                }

                // Hiển thị dữ liệu sau khi lọc
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
