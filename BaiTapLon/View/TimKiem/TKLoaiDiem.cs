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
    public partial class TKLoaiDiem : Form
    {
        private readonly LoaiDiemController _controller;
        public TKLoaiDiem()
        {
            InitializeComponent();
            _controller = new LoaiDiemController();
        }

        private void TKLoaiDiem_Load(object sender, EventArgs e)
        {
            cboLuaChon.Items.AddRange(new string[]
            {
                "Chọn Tiêu chí",
                "ID loại điểm",
                "Tên loại điểm",
                "Tỉ lệ"
            });
            cboLuaChon.SelectedIndex = 0;

            // Tải toàn bộ dữ liệu ban đầu
            LoadAllData();
        }
        private void LoadAllData()
        {
            try
            {
                var dt = DataBase.GetData("SELECT IDLoaiDiem, TenLoaiDiem, TiLe FROM LoaiDiem WHERE TrangThai = 'Initialize'");
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (cboLuaChon.SelectedIndex == 0)
            {
                MessageBox.Show("Vui lòng chọn tiêu chí tìm kiếm.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // Lấy tiêu chí và giá trị tìm kiếm
            string luaChon = cboLuaChon.Text;
            string giaTri = txtGiaTri.Text.Trim();

            // Kiểm tra nếu không nhập giá trị thì hiển thị toàn bộ dữ liệu
            if (string.IsNullOrEmpty(giaTri))
            {
                LoadAllData();
                return;
            }

            // Câu truy vấn cơ bản
            string query = "SELECT IDLoaiDiem, TenLoaiDiem, TiLe FROM LoaiDiem WHERE TrangThai = 'Initialize'";

            // Thêm điều kiện tìm kiếm theo tiêu chí
            switch (luaChon)
            {
                case "ID loại điểm":
                    query += " AND IDLoaiDiem = @SearchValue";
                    break;

                case "Tên loại điểm":
                    query += " AND TenLoaiDiem LIKE @SearchValue";
                    break;

                case "Tỉ lệ":
                    query += " AND TiLe LIKE @SearchValue";
                    break;

                default:
                    MessageBox.Show("Tiêu chí tìm kiếm không hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
            }

            try
            {
                // Tạo tham số tìm kiếm
                SqlParameter[] parameters;

                if (luaChon == "ID loại điểm")
                {
                    // Tìm kiếm chính xác theo ID
                    parameters = new SqlParameter[]
                    {
                        new SqlParameter("@SearchValue", giaTri)
                    };
                }
                else
                {
                    // Tìm kiếm tương đối (LIKE)
                    parameters = new SqlParameter[]
                    {
                        new SqlParameter("@SearchValue", "%" + giaTri + "%")
                    };
                }

                // Thực hiện truy vấn với tham số tìm kiếm
                var dt = DataBase.GetData(query, parameters);

                // Hiển thị dữ liệu lên DataGridView
                dataGridView1.DataSource = dt;

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy kết quả phù hợp.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
    }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
