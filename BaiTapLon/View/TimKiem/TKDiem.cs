using System;
using BaiTapLon.Controller;
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
    public partial class TKDiem : Form
    {
        private DiemController diemController;
        public TKDiem()
        {
            InitializeComponent();
            diemController = new DiemController();

        }
        private void TKDiem_Load(object sender, EventArgs e)
        {
            cboLuaChon.Items.AddRange(new string[] { "Lựa chọn giá trị","ID điểm", "Tên sinh viên", "Tên môn", "Tên loại điểm", "Giá trị điểm", "Lần thi" });
            cboLuaChon.SelectedIndex = 0; // Default selection

            // Load the full "Diem" data into the DataGridView
            LoadDatabase();
        }

        private void LoadDatabase()
        {
            try
            {
                // Retrieve and bind all data to the DataGridView
                dataGridView1.DataSource = diemController.LoadDiemData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnTK_Click(object sender, EventArgs e)
        {
            if (cboLuaChon.SelectedIndex == 0)
            {
                MessageBox.Show("Vui lòng chọn tiêu chí tìm kiếm.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string luaChon = cboLuaChon.Text;
            string giaTri = txtGiaTri.Text.Trim();

            if (string.IsNullOrEmpty(giaTri))
            {
                // Reload all data if the search value is empty
                LoadDatabase();
                return;
            }

            try
            {
                // Load all data
                DataTable dt = diemController.LoadDiemData();
                DataView dv = dt.DefaultView;

                // Apply filters based on the selected criteria
                switch (luaChon)
                {
                    case "ID điểm":
                        dv.RowFilter = $"CONVERT(IDDiem, 'System.String') LIKE '%{giaTri}%'";
                        break;

                    case "Tên sinh viên":
                        dv.RowFilter = $"TenSinhVien LIKE '%{giaTri}%'";
                        break;

                    case "Tên môn":
                        dv.RowFilter = $"TenMon LIKE '%{giaTri}%'";
                        break;

                    case "Tên loại điểm":
                        dv.RowFilter = $"TenLoaiDiem LIKE '%{giaTri}%'";
                        break;

                    case "Giá trị điểm":
                        dv.RowFilter = $"CONVERT(GiaTriDiem, 'System.String') LIKE '%{giaTri}%'";
                        break;

                    case "Lần thi":
                        dv.RowFilter = $"CONVERT(LanThi, 'System.String') LIKE '%{giaTri}%'";
                        break;

                    default:
                        MessageBox.Show("Tiêu chí tìm kiếm không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                }

                // Update the DataGridView
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
    }
}
