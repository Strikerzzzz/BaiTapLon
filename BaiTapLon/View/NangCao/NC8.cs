using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace BaiTapLon.View.NangCao
{
    public partial class NC8 : Form
    {
        public NC8()
        {
            InitializeComponent();
        }

        private void NC8_Load(object sender, EventArgs e)
        {
            comboBox.Items.Add("Theo môn");
            comboBox.Items.Add("Theo kỳ");
            comboBox.Items.Add("Theo ngành");
            comboBox.Items.Add("Theo lớp");
            comboBox.SelectedIndex = 0;
            LoadChartData(GetQueryForSelectedComboBox());
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
            else if (comboBox.SelectedItem.ToString() == "Theo kỳ")
            {
                return @"WITH DiemTrungBinh AS (
                    SELECT 
                        d.MaSV,
                        CONCAT(h.TenHocKy, ' - ', h.Nam) AS HocKyNam, -- Gộp tên học kỳ và năm học
                        SUM(d.GiaTriDiem * (ld.TiLe / 100)) AS DiemTrungBinh
                    FROM 
                        Diem d
                    INNER JOIN 
                        LoaiDiem ld ON d.IDLoaiDiem = ld.IDLoaiDiem
                    INNER JOIN 
                        SinhVien sv ON d.MaSV = sv.MaSV
                    INNER JOIN 
                        LopHoc_SinhVien lhs ON sv.MaSV = lhs.MaSV
                    INNER JOIN 
                        LopHoc lh ON lhs.MaLop = lh.MaLop
                    INNER JOIN 
                        HocKy h ON lh.IDHocKy = h.IDHocKy -- Kết nối với bảng HocKy
                    WHERE 
                        d.TrangThai = 'Initialize' -- Điều kiện trạng thái điểm
                    GROUP BY 
                        d.MaSV, h.TenHocKy, h.Nam
                ),
                KetQua AS (
                    SELECT 
                        HocKyNam,
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
                    HocKyNam,
                    SUM(Pass) AS SoLuongPass,
                    SUM(Fail) AS SoLuongFail
                FROM 
                    KetQua
                GROUP BY 
                    HocKyNam
                ORDER BY 
                    HocKyNam;";
            }
            else if (comboBox.SelectedItem.ToString() == "Theo ngành")
            {
                return @"WITH DiemTrungBinhNganh AS (
                    SELECT 
                        d.MaSV,
                        sv.MaChuyenNganh,
                        cn.TenChuyenNganh,
                        SUM(d.GiaTriDiem * (ld.TiLe / 100)) AS DiemTrungBinh
                    FROM 
                        Diem d
                    INNER JOIN 
                        LoaiDiem ld ON d.IDLoaiDiem = ld.IDLoaiDiem
                    INNER JOIN 
                        SinhVien sv ON d.MaSV = sv.MaSV
                    INNER JOIN 
                        ChuyenNganh cn ON sv.MaChuyenNganh = cn.MaChuyenNganh
                    WHERE 
                        d.TrangThai = 'Initialize'
                    GROUP BY 
                        d.MaSV, sv.MaChuyenNganh, cn.TenChuyenNganh
                ),
                KetQuaNganh AS (
                    SELECT 
                        TenChuyenNganh,
                        CASE 
                            WHEN DiemTrungBinh >= 4.0 THEN 1
                            ELSE 0
                        END AS Pass,
                        CASE 
                            WHEN DiemTrungBinh < 4.0 THEN 1
                            ELSE 0
                        END AS Fail
                    FROM 
                        DiemTrungBinhNganh
                )
                SELECT 
                    TenChuyenNganh,
                    SUM(Pass) AS SoLuongPass,
                    SUM(Fail) AS SoLuongFail
                FROM 
                    KetQuaNganh
                GROUP BY 
                    TenChuyenNganh
                ORDER BY 
                    TenChuyenNganh;";
            }
            else if (comboBox.SelectedItem.ToString() == "Theo lớp")
            {
                return @"WITH DiemTrungBinh AS (
                    SELECT 
                        d.MaSV,
                        lh.MaLop,
                        lh.TenLop,
                        SUM(d.GiaTriDiem * (ld.TiLe / 100)) AS DiemTrungBinh
                    FROM 
                        Diem d
                    INNER JOIN 
                        LoaiDiem ld ON d.IDLoaiDiem = ld.IDLoaiDiem
                    INNER JOIN 
                        SinhVien sv ON d.MaSV = sv.MaSV
                    INNER JOIN 
                        LopHoc_SinhVien lhs ON sv.MaSV = lhs.MaSV
                    INNER JOIN 
                        LopHoc lh ON lhs.MaLop = lh.MaLop
                    WHERE 
                        d.TrangThai = 'Initialize'
                    GROUP BY 
                        d.MaSV, lh.MaLop, lh.TenLop
                ),
                KetQua AS (
                    SELECT 
                        TenLop,
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
                    TenLop,
                    SUM(Pass) AS SoLuongPass,
                    SUM(Fail) AS SoLuongFail
                FROM 
                    KetQua
                GROUP BY 
                    TenLop
                ORDER BY 
                    TenLop;";
            }
            else
            {
                return string.Empty;
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

                    // Tắt lưới ngang (trục Y) và lưới dọc (trục X)
                    chart.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
                    chart.ChartAreas[0].AxisY.MinorGrid.Enabled = false;
                    chart.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                    chart.ChartAreas[0].AxisX.MinorGrid.Enabled = false;

                    // Tạo Series mới cho biểu đồ
                    Series seriesPass = new Series("Pass");
                    Series seriesFail = new Series("Fail");

                    seriesPass.ChartType = SeriesChartType.Column;
                    seriesFail.ChartType = SeriesChartType.Column;

                    seriesPass.IsValueShownAsLabel = true; // Hiển thị giá trị trên cột
                    seriesFail.IsValueShownAsLabel = true;

                    // Phân loại dữ liệu dựa trên cột có trong bảng
                    string columnX = string.Empty;
                    string columnPass = string.Empty;
                    string columnFail = string.Empty;

                    if (dt.Columns.Contains("TenMon"))
                    {
                        columnX = "TenMon";
                        columnPass = "SoLuongPass";
                        columnFail = "SoLuongFail";
                    }
                    else if (dt.Columns.Contains("HocKyNam"))
                    {
                        columnX = "HocKyNam";
                        columnPass = "SoLuongPass";
                        columnFail = "SoLuongFail";
                    }
                    else if (dt.Columns.Contains("TenChuyenNganh"))
                    {
                        columnX = "TenChuyenNganh";
                        columnPass = "SoLuongPass";
                        columnFail = "SoLuongFail";
                    }
                    else if (dt.Columns.Contains("TenLop"))
                    {
                        columnX = "TenLop";
                        columnPass = "SoLuongPass";
                        columnFail = "SoLuongFail";
                    }

                    // Thêm dữ liệu vào các Series
                    foreach (DataRow row in dt.Rows)
                    {
                        string xValue = row[columnX].ToString();
                        int passValue = Convert.ToInt32(row[columnPass]);
                        int failValue = Convert.ToInt32(row[columnFail]);

                        seriesPass.Points.AddXY(xValue, passValue);
                        seriesFail.Points.AddXY(xValue, failValue);
                    }

                    // Thêm Series vào biểu đồ
                    chart.Series.Add(seriesPass);
                    chart.Series.Add(seriesFail);

                    // Cấu hình trục và tiêu đề biểu đồ
                    if (columnX == "TenMon")
                    {
                        chart.ChartAreas[0].AxisX.Title = "Môn học";
                    }
                    else if (columnX == "TenHocKy")
                    {
                        chart.ChartAreas[0].AxisX.Title = "Kỳ học";
                    }
                    else if (columnX == "TenChuyenNganh")
                    {
                        chart.ChartAreas[0].AxisX.Title = "Ngành học";
                    }
                    else if (columnX == "TenLop")
                    {
                        chart.ChartAreas[0].AxisX.Title = "Lớp học";
                    }
                    else
                    {
                        chart.ChartAreas[0].AxisX.Title = "Danh mục";
                    }

                    chart.ChartAreas[0].AxisY.Title = "Số lượng";
                    chart.ChartAreas[0].AxisX.Interval = 1; // Hiển thị tất cả các nhãn

                    // Thêm tiêu đề cho biểu đồ
                    chart.Titles.Clear();
                    chart.Titles.Add($"Thống kê theo {chart.ChartAreas[0].AxisX.Title.ToLower()}");
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
            LoadChartData(GetQueryForSelectedComboBox());
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
