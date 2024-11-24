using BaiTapLon.Controller;
using BaiTapLon.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaiTapLon.View.TimKiem
{
    public partial class TKChuyenNganh : Form
    {
        public TKChuyenNganh()
        {
            InitializeComponent();
        }

        private void TKChuyenNganh_Load(object sender, EventArgs e)
        {
            cboLuaChon.Items.AddRange(new string[] { "Lựa chọn giá trị", "Mã chuyên ngành", "Tên chuyên ngành" });
            cboLuaChon.SelectedIndex = 0; 
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                // Lấy toàn bộ danh sách chuyên ngành từ controller và hiển thị trên DataGridView
                List<ChuyenNganh> data = ChuyenNganhController.GetAllChuyenNganh();
                dataGridView1.DataSource = data;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTK_Click(object sender, EventArgs e)
        {
            if(cboLuaChon.SelectedIndex == 0)
            {
                MessageBox.Show("Vui lòng chọn tiêu chí tìm kiếm.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string luaChon = cboLuaChon.Text;
            string giaTri = txtGiaTri.Text.Trim();

            if (string.IsNullOrEmpty(giaTri))
            {
                LoadData();
                return;
            }

            try
            {
                // Lấy toàn bộ danh sách chuyên ngành từ controller
                List<ChuyenNganh> data = ChuyenNganhController.GetAllChuyenNganh();

                // Áp dụng bộ lọc danh sách dựa trên tiêu chí tìm kiếm
                List<ChuyenNganh> filteredData = new List<ChuyenNganh>();

                switch (luaChon)
                {
                    case "Mã chuyên ngành":
                        filteredData = data.Where(cn =>
                            cn.MaChuyenNganh.IndexOf(giaTri, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
                        break;

                    case "Tên chuyên ngành":
                        filteredData = data.Where(cn =>
                            cn.TenChuyenNganh.IndexOf(giaTri, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
                        break;

                    default:
                        MessageBox.Show("Tiêu chí tìm kiếm không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                }

                // Hiển thị dữ liệu đã lọc trên DataGridView
                dataGridView1.DataSource = filteredData;

                if (filteredData.Count == 0)
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
