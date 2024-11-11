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
    public partial class ThungRac : Form
    {
        public ThungRac()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void PopulateComboBox()
        {
            // Thêm dòng đầu tiên
            comboBox.Items.Add("-----Chọn loại cần khôi phục-----");

            // Danh sách các bảng
            string[] tables = {
                "ChuyenNganh",
                "SinhVien",
                "DiemDanh",
                "LopHoc_SinhVien",
                "LoaiDiem",
                "Diem",
                "LopHoc",
                "MonHoc",
                "HocKy",
                "LoaiMon"
            };

            // Thêm các bảng vào ComboBox
            comboBox.Items.AddRange(tables);

            // Thiết lập dòng đầu tiên được chọn mặc định
            comboBox.SelectedIndex = 0;
        }
        private void TRLoaiMon_Load(object sender, EventArgs e)
        {
            PopulateComboBox();
        }
        void LoadDatabase(string sql)
        {
            try
            {
                this.dgv.DataSource = DataBase.GetData(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            // Lấy tên bảng được chọn từ ComboBox
            string selectedTable = comboBox.SelectedItem.ToString();

            // Kiểm tra nếu người dùng chưa chọn một bảng hợp lệ
            if (selectedTable == "-----Chọn loại cần khôi phục-----")
            {
                MessageBox.Show("Vui lòng chọn bảng cần khôi phục.");
                return;
            }

            // Kiểm tra nếu người dùng chưa chọn dòng nào trong DataGridView
            if (dgv.CurrentRow == null || dgv.CurrentRow.Cells[0].Value == null)
            {
                MessageBox.Show("Vui lòng chọn một dòng để khôi phục.");
                return;
            }

            // Lấy ID của bản ghi cần khôi phục
            string id = dgv.CurrentRow.Cells[0].Value.ToString();

            try
            {
                // Tạo câu truy vấn SQL động để cập nhật trạng thái của bản ghi
                string query = $"UPDATE {selectedTable} SET TrangThai = 'Initialize' WHERE {dgv.Columns[0].Name} = @ID";

                // Thiết lập tham số cho câu truy vấn
                SqlParameter[] parameters = {
                    new SqlParameter("@ID", id)
                };

                // Thực thi câu truy vấn
                bool result = new DataBase().UpdateData(query, parameters);
                if (result)
                {
                    MessageBox.Show("Khôi phục thành công!");
                    // Tải lại dữ liệu sau khi khôi phục
                    LoadDatabase($"SELECT * FROM {selectedTable} WHERE TrangThai = 'Deleted'");
                }
                else
                {
                    MessageBox.Show("Khôi phục thất bại.");
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


        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedTable = comboBox.SelectedItem.ToString();
            if (selectedTable != "-----Chọn loại cần khôi phục-----")
            {
                string query = $"SELECT * FROM {selectedTable} WHERE TrangThai = 'Deleted'";
                LoadDatabase(query);
            }
            else
            {
                dgv.DataSource = null;
            }
        }
    }
}
