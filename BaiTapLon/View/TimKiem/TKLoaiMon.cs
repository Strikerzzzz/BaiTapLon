using BaiTapLon.Controller;
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
    public partial class TKLoaiMon : Form
    {
        private readonly LoaiMonController _controller;
        public TKLoaiMon()
        {
            InitializeComponent();
            _controller = new LoaiMonController();
        }

        private void TKLoaiMon_Load(object sender, EventArgs e)
        {
            cboLuaChon.Items.AddRange(new string[] { "Lựa chọn giá trị", "Mã loại môn", "Loại môn" });
            cboLuaChon.SelectedIndex = 0; // Mặc định chọn "Mã loại môn"

            // Tải toàn bộ dữ liệu ban đầu vào DataGridView
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                // Lấy toàn bộ dữ liệu loại môn và hiển thị trên DataGridView
                DataTable data = DataBase.GetData("SELECT IDLoaiMon, LoaiMon FROM LoaiMon WHERE TrangThai = 'Initialize'");
                dataGridView1.DataSource = data;
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

            string luaChon = cboLuaChon.Text;
            string giaTri = txtGiaTri.Text.Trim();

            if (string.IsNullOrEmpty(giaTri))
            {
                LoadData(); 
                return;
            }

            try
            {
                // Lấy toàn bộ dữ liệu ban đầu
                DataTable data = DataBase.GetData("SELECT IDLoaiMon, LoaiMon FROM LoaiMon WHERE TrangThai = 'Initialize'");
                DataView dv = data.DefaultView;

                // Áp dụng bộ lọc dữ liệu dựa trên tiêu chí
                switch (luaChon)
                {
                    case "Mã loại môn":
                        dv.RowFilter = $"CONVERT(IDLoaiMon, 'System.String') LIKE '%{giaTri}%'";
                        break;

                    case "Loại môn":
                        dv.RowFilter = $"LoaiMon LIKE '%{giaTri}%'";
                        break;

                    default:
                        MessageBox.Show("Tiêu chí tìm kiếm không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                }

                // Hiển thị dữ liệu đã lọc trên DataGridView
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
