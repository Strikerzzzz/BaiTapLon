using System;
using System.Data;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace BaiTapLon.View.NangCao
{
    public partial class NC7 : Form
    {
        public NC7()
        {
            InitializeComponent();
        }

        private void NC7_Load(object sender, EventArgs e)
        {
            comboBox.Items.Add("Theo môn");
            comboBox.Items.Add("Theo điểm danh");
            comboBox.SelectedIndex = 0; // Đặt mục mặc định
            LoadChartData(GetQueryForSelectedComboBox());
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private string GetQueryForSelectedComboBox()
        {
            if (comboBox.SelectedItem.ToString() == "Theo môn")
            {
                return @"WITH DiemTrungBinh AS (
                            SELECT 
                                d.MaSV,
                                d.MaMon,
                                mh.TenMon,
                                SUM(d.GiaTriDiem * (ld.TiLe / 100)) AS DiemTrungBinh
                            FROM 
                                Diem d
                            INNER JOIN 
                                LoaiDiem ld ON d.IDLoaiDiem = ld.IDLoaiDiem
                            INNER JOIN 
                                MonHoc mh ON d.MaMon = mh.MaMon
                            WHERE 
                                d.TrangThai = 'Initialize'
                            GROUP BY 
                                d.MaSV, d.MaMon, mh.TenMon
                        ),
                        KetQua AS (
                            SELECT 
                                TenMon,
                                CASE 
                                    WHEN DiemTrungBinh >= 4.0 THEN 1
                                    ELSE 0
                                END AS Pass,
                                CASE 
                                    WHEN DiemTrungBinh < 4.0 THEN 1
                                    ELSE 0
                                END AS Fail
                            FROM 
                                DiemTrungBinh
                        )
                        SELECT 
                            TenMon,
                            SUM(Pass) AS SoLuongPass,
                            SUM(Fail) AS SoLuongFail
                        FROM 
                            KetQua
                        GROUP BY 
                            TenMon
                        ORDER BY 
                            TenMon;";
            }
            else
            {
                return @"SELECT 
                            lh.TenLop,
                            COUNT(CASE WHEN SoBuoiVang > 0.5 * TongSoBuoi THEN 1 END) AS SoLuongTruot,
                            COUNT(CASE WHEN SoBuoiVang <= 0.5 * TongSoBuoi THEN 1 END) AS SoLuongQua
                        FROM (
                            SELECT 
                                dd.MaLop,
                                dd.MaSV,
                                COUNT(CASE WHEN dd.TTDiemDanh = N'không' THEN 1 END) AS SoBuoiVang,
                                COUNT(*) AS TongSoBuoi
                            FROM 
                                DiemDanh dd
                            GROUP BY 
                                dd.MaLop, dd.MaSV
                        ) AS ThongKeDiemDanh
                        JOIN 
                            LopHoc lh ON ThongKeDiemDanh.MaLop = lh.MaLop
                        GROUP BY 
                            lh.TenLop;";
            }
        }

        private void LoadChartData(string query)
        {
            try
            {
                // Lấy dữ liệu từ cơ sở dữ liệu
                DataTable dt = DataBase.GetData(query);

                if (dt != null && dt.Rows.Count > 0)
                {
                    // Xóa dữ liệu cũ trên Chart
                    chart.Series.Clear();

                    // Tạo Series mới cho Chart
                    // Tắt lưới ngang (trục Y)
                    chart.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
                    chart.ChartAreas[0].AxisY.MinorGrid.Enabled = false;

                    // Tắt lưới dọc (trục X)
                    chart.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                    chart.ChartAreas[0].AxisX.MinorGrid.Enabled = false;
                    Series seriesPass = new Series("Pass");
                    Series seriesFail = new Series("Fail");

                    seriesPass.ChartType = SeriesChartType.Column;
                    seriesFail.ChartType = SeriesChartType.Column;

                    seriesPass.IsValueShownAsLabel = true;
                    seriesFail.IsValueShownAsLabel = true;

                    foreach (DataRow row in dt.Rows)
                    {
                        if (query.Contains("TenMon")) // Nếu là query theo môn
                        {
                            string tenMon = row["TenMon"].ToString();
                            int soLuongPass = Convert.ToInt32(row["SoLuongPass"]);
                            int soLuongFail = Convert.ToInt32(row["SoLuongFail"]);

                            seriesPass.Points.AddXY(tenMon, soLuongPass);
                            seriesFail.Points.AddXY(tenMon, soLuongFail);
                        }
                        else // Nếu là query theo điểm danh
                        {
                            string tenLop = row["TenLop"].ToString();
                            int soLuongQua = Convert.ToInt32(row["SoLuongQua"]);
                            int soLuongTruot = Convert.ToInt32(row["SoLuongTruot"]);

                            seriesPass.Points.AddXY(tenLop, soLuongQua);
                            seriesFail.Points.AddXY(tenLop, soLuongTruot);
                        }
                    }

                    // Thêm Series vào Chart
                    chart.Series.Add(seriesPass);
                    chart.Series.Add(seriesFail);

                    // Cấu hình trục và tiêu đề
                    chart.ChartAreas[0].AxisX.Title = query.Contains("TenMon") ? "Môn học" : "Lớp học";
                    chart.ChartAreas[0].AxisY.Title = "Số lượng";
                    chart.ChartAreas[0].AxisX.Interval = 1; // Hiển thị tất cả các nhãn
                    chart.Titles.Clear();
                    chart.Titles.Add(query.Contains("TenMon")
                        ? "Thống kê theo môn học"
                        : "Thống kê điểm danh theo lớp");
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
            // Gọi lại LoadChartData với câu truy vấn tương ứng
            LoadChartData(GetQueryForSelectedComboBox());
        }
    }
}
