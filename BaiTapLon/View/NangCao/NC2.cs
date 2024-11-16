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
using System.Windows.Forms.DataVisualization.Charting;

namespace BaiTapLon.View.NangCao
{
    public partial class NC2 : Form
    {
        public NC2()
        {
            InitializeComponent();
        }
        Dictionary<string, string> dictLM = new Dictionary<string, string>();
        private void NC2_Load(object sender, EventArgs e)
        {
            DataTable dt = DataBase.GetData("SELECT MaChuyenNganh, TenChuyenNganh FROM ChuyenNganh where TrangThai = 'Initialize'");
            dictLM.Add("-1", "------Chọn chuyên ngành------");
            if (dt != null && dt.Rows.Count > 0)
            {
                LoadListDB(dt, dictLM);
                LoadChartData();
            }
            else
            {
                MessageBox.Show("Không có dữ liệu loại môn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            comboBox.DataSource = new BindingSource(dictLM, null);
            comboBox.DisplayMember = "Value";
            comboBox.ValueMember = "Key";
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void LoadChartData()
        {
            try
            {
                // Truy vấn lấy số lượng sinh viên theo chuyên ngành
                string query = @"SELECT cn.TenChuyenNganh, COUNT(sv.MaSV) AS SoLuong
                         FROM ChuyenNganh cn
                         LEFT JOIN SinhVien sv ON cn.MaChuyenNganh = sv.MaChuyenNganh
                         WHERE sv.TrangThai = 'Initialize'
                         GROUP BY cn.TenChuyenNganh";

                DataTable dt = DataBase.GetData(query);

                if (dt != null && dt.Rows.Count > 0)
                {
                    // Xóa dữ liệu cũ trên Chart
                    chartChuyenNganh.Series.Clear();

                    // Tạo Series mới cho Chart
                    Series series = new Series("Số lượng");
                    series.ChartType = SeriesChartType.Column;
                    series.IsValueShownAsLabel = true;

                    // Thêm dữ liệu vào Chart
                    foreach (DataRow row in dt.Rows)
                    {
                        string tenChuyenNganh = row["TenChuyenNganh"].ToString();
                        int soLuong = Convert.ToInt32(row["SoLuong"]);
                        series.Points.AddXY(tenChuyenNganh, soLuong);
                    }

                    // Cấu hình Chart
                    chartChuyenNganh.Series.Add(series);
                    chartChuyenNganh.ChartAreas[0].AxisX.Title = "Chuyên Ngành";
                    chartChuyenNganh.ChartAreas[0].AxisY.Title = "Số Lượng Sinh Viên";
                    chartChuyenNganh.ChartAreas[0].AxisX.LabelStyle.Angle = 0; // 0 độ là hiển thị ngang
                    chartChuyenNganh.ChartAreas[0].AxisX.Interval = 1; // Hiển thị tất cả các nhãn
                    chartChuyenNganh.Titles.Clear();
                    chartChuyenNganh.Titles.Add("Thống kê số lượng sinh viên theo chuyên ngành");
                    // Tắt lưới ngang (trục Y)
                    chartChuyenNganh.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
                    chartChuyenNganh.ChartAreas[0].AxisY.MinorGrid.Enabled = false;

                    // Tắt lưới dọc (trục X)
                    chartChuyenNganh.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                    chartChuyenNganh.ChartAreas[0].AxisX.MinorGrid.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu để hiển thị biểu đồ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu biểu đồ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = ((KeyValuePair<string, string>)comboBox.SelectedItem).Key;

            if (selected != "-1")
            {
                string query = $@"SELECT MaSV, CONCAT(HoDem, ' ', Ten) AS TenSinhVien, NgaySinh, SoDienThoai FROM SinhVien mh where TrangThai = 'Initialize' AND MaChuyenNganh = @MaChuyenNganh";

                SqlParameter[] parameters = {
                    new SqlParameter("@MaChuyenNganh ", selected)
                };

                LoadDatabaseWithParams(query, parameters);
            }
            else
            {
                dgv.DataSource = null;
            }
        }
        void LoadDatabaseWithParams(string sql, SqlParameter[] parameters)
        {
            try
            {
                this.dgv.DataSource = DataBase.GetData(sql, parameters);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        void LoadListDB(DataTable dt, Dictionary<string, string> dict)
        {
            try
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        string ma = row[0].ToString();
                        string ten = row[1].ToString();

                        if (!dict.ContainsKey(ma))
                        {
                            dict.Add(ma, ten);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Dữ liệu không tồn tại hoặc rỗng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
