using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BaiTapLon.View.NangCao
{
    public partial class NC9 : Form
    {
        public NC9()
        {
            InitializeComponent();
        }

        private void LoadHocKy()
        {
            string query = "SELECT IDHocKy, CONCAT(TenHocKy, N' - Năm ', Nam) AS HocKy FROM HocKy WHERE TrangThai = 'Initialize'";
            DataTable dtHocKy = DataBase.GetData(query);

            if (dtHocKy != null && dtHocKy.Rows.Count > 0)
            {
                cboHocKy.DataSource = dtHocKy;
                cboHocKy.DisplayMember = "HocKy"; 
                cboHocKy.ValueMember = "IDHocKy"; 
                cboHocKy.SelectedIndex = -1;
            }
            else
            {
                MessageBox.Show("Không có học kỳ nào được khởi tạo.", "Thông báo");
            }
        }

        private void NC9_Load(object sender, EventArgs e)
        {
            LoadHocKy();
        }

        private void LoadSinhVien(int hocKyId)
        {
            string query = @"
                SELECT DISTINCT sv.MaSV, CONCAT(sv.MaSV, ' - ', sv.HoDem, ' ', sv.Ten) AS HoTen
                FROM LopHoc lh
                JOIN LopHoc_SinhVien lhsv ON lh.MaLop = lhsv.MaLop
                JOIN SinhVien sv ON lhsv.MaSV = sv.MaSV
                WHERE lh.IDHocKy = @HocKyId AND sv.TrangThai = 'Initialize'";

            SqlParameter[] parameters = { new SqlParameter("@HocKyId", hocKyId) };
            DataTable dtSinhVien = DataBase.GetData(query, parameters);

            if (dtSinhVien != null && dtSinhVien.Rows.Count > 0)
            {
                cboSinhVien.DataSource = dtSinhVien;
                cboSinhVien.DisplayMember = "HoTen";
                cboSinhVien.ValueMember = "MaSV";
                cboSinhVien.SelectedIndex = -1;
            }
            else
            {
                MessageBox.Show("Không có sinh viên trong học kỳ này.", "Thông báo");
                cboSinhVien.DataSource = null; 
            }
        }

        private void cboHocKy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboHocKy.SelectedValue != null && !(cboHocKy.SelectedValue is DataRowView))
            {
                int selectedHocKyId = (int)cboHocKy.SelectedValue; 

                Console.WriteLine($"HocKyId được chọn: {selectedHocKyId}"); 

                LoadSinhVien(selectedHocKyId); 
            }
            else
            {
                Console.WriteLine("Không có học kỳ hợp lệ được chọn.");
            }
        }
      
        private void LoadChiTietSinhVien(int maSinhVien)
        {
            string query = @"
                            SELECT 
                                mh.MaMon, 
                                mh.TenMon, 
                                
                                -- Tính điểm thường kỳ
                                ISNULL((SELECT SUM(d.GiaTriDiem * ld.TiLe) * 1.0 / NULLIF(SUM(ld.TiLe), 0)
                                        FROM Diem d
                                        JOIN LoaiDiem ld ON d.IDLoaiDiem = ld.IDLoaiDiem
                                        WHERE d.MaSV = @MaSV 
                                        AND d.MaMon = mh.MaMon
                                        AND ld.TenLoaiDiem NOT LIKE '%Thi%'
                                        AND d.TrangThai = 'Initialize'
                                        AND ld.TrangThai = 'Initialize'), 0) AS DiemThuongKy,
                                -- Tính điểm cuối kỳ
                                ISNULL((SELECT MAX(d.GiaTriDiem)
                                        FROM Diem d
                                        JOIN LoaiDiem ld ON d.IDLoaiDiem = ld.IDLoaiDiem
                                        WHERE d.MaSV = @MaSV 
                                        AND d.MaMon = mh.MaMon
                                        AND ld.TenLoaiDiem LIKE '%Thi%'
                                        AND d.TrangThai = 'Initialize'
                                        AND ld.TrangThai = 'Initialize'), 0) AS DiemCuoiKy,
                                (
                                    (
                                        -- Điểm thường kỳ (40%)
                                        ISNULL((SELECT SUM(d.GiaTriDiem * ld.TiLe) * 1.0 / NULLIF(SUM(ld.TiLe), 0)
                                                FROM Diem d
                                                JOIN LoaiDiem ld ON d.IDLoaiDiem = ld.IDLoaiDiem
                                                WHERE d.MaSV = @MaSV 
                                                AND d.MaMon = mh.MaMon
                                                AND ld.TenLoaiDiem NOT LIKE '%Thi%'
                                                AND d.TrangThai = 'Initialize'
                                                AND ld.TrangThai = 'Initialize'), 0) * 0.4
                                    ) +
                                    (
                                        -- Điểm cuối kỳ (60%)
                                        ISNULL((SELECT MAX(d.GiaTriDiem)
                                                FROM Diem d
                                                JOIN LoaiDiem ld ON d.IDLoaiDiem = ld.IDLoaiDiem
                                                WHERE d.MaSV = @MaSV 
                                                AND d.MaMon = mh.MaMon
                                                AND ld.TenLoaiDiem LIKE '%Thi%'
                                                AND d.TrangThai = 'Initialize'
                                                AND ld.TrangThai = 'Initialize'), 0) * 0.6
                                    )
                                ) AS DiemTrungBinh,

                                 -- Điểm trung bình hệ 4
                                CASE 
                                    WHEN (
                                        (
                                            ISNULL((SELECT SUM(d.GiaTriDiem * ld.TiLe) * 1.0 / NULLIF(SUM(ld.TiLe), 0)
                                                    FROM Diem d
                                                    JOIN LoaiDiem ld ON d.IDLoaiDiem = ld.IDLoaiDiem
                                                    WHERE d.MaSV = @MaSV 
                                                    AND d.MaMon = mh.MaMon
                                                    AND ld.TenLoaiDiem NOT LIKE '%Thi%'
                                                    AND d.TrangThai = 'Initialize'
                                                    AND ld.TrangThai = 'Initialize'), 0) * 0.4
                                        ) +
                                        (
                                            ISNULL((SELECT MAX(d.GiaTriDiem)
                                                    FROM Diem d
                                                    JOIN LoaiDiem ld ON d.IDLoaiDiem = ld.IDLoaiDiem
                                                    WHERE d.MaSV = @MaSV 
                                                    AND d.MaMon = mh.MaMon
                                                    AND ld.TenLoaiDiem LIKE '%Thi%'
                                                    AND d.TrangThai = 'Initialize'
                                                    AND ld.TrangThai = 'Initialize'), 0) * 0.6
                                        )
                                    ) >= 8.5 THEN 4.0
                                    WHEN (
                                        (
                                            ISNULL((SELECT SUM(d.GiaTriDiem * ld.TiLe) * 1.0 / NULLIF(SUM(ld.TiLe), 0)
                                                    FROM Diem d
                                                    JOIN LoaiDiem ld ON d.IDLoaiDiem = ld.IDLoaiDiem
                                                    WHERE d.MaSV = @MaSV 
                                                    AND d.MaMon = mh.MaMon
                                                    AND ld.TenLoaiDiem NOT LIKE '%Thi%'
                                                    AND d.TrangThai = 'Initialize'
                                                    AND ld.TrangThai = 'Initialize'), 0) * 0.4
                                        ) +
                                        (
                                            ISNULL((SELECT MAX(d.GiaTriDiem)
                                                    FROM Diem d
                                                    JOIN LoaiDiem ld ON d.IDLoaiDiem = ld.IDLoaiDiem
                                                    WHERE d.MaSV = @MaSV 
                                                    AND d.MaMon = mh.MaMon
                                                    AND ld.TenLoaiDiem LIKE '%Thi%'
                                                    AND d.TrangThai = 'Initialize'
                                                    AND ld.TrangThai = 'Initialize'), 0) * 0.6
                                        )
                                    ) BETWEEN 8.0 AND 8.4 THEN 3.5
                                    WHEN (
                                        (
                                            ISNULL((SELECT SUM(d.GiaTriDiem * ld.TiLe) * 1.0 / NULLIF(SUM(ld.TiLe), 0)
                                                    FROM Diem d
                                                    JOIN LoaiDiem ld ON d.IDLoaiDiem = ld.IDLoaiDiem
                                                    WHERE d.MaSV = @MaSV 
                                                    AND d.MaMon = mh.MaMon
                                                    AND ld.TenLoaiDiem NOT LIKE '%Thi%'
                                                    AND d.TrangThai = 'Initialize'
                                                    AND ld.TrangThai = 'Initialize'), 0) * 0.4
                                        ) +
                                        (
                                            ISNULL((SELECT MAX(d.GiaTriDiem)
                                                    FROM Diem d
                                                    JOIN LoaiDiem ld ON d.IDLoaiDiem = ld.IDLoaiDiem
                                                    WHERE d.MaSV = @MaSV 
                                                    AND d.MaMon = mh.MaMon
                                                    AND ld.TenLoaiDiem LIKE '%Thi%'
                                                    AND d.TrangThai = 'Initialize'
                                                    AND ld.TrangThai = 'Initialize'), 0) * 0.6
                                        )
                                    ) BETWEEN 7.0 AND 7.9 THEN 3.0
                                    WHEN (
                                        (
                                            ISNULL((SELECT SUM(d.GiaTriDiem * ld.TiLe) * 1.0 / NULLIF(SUM(ld.TiLe), 0)
                                                    FROM Diem d
                                                    JOIN LoaiDiem ld ON d.IDLoaiDiem = ld.IDLoaiDiem
                                                    WHERE d.MaSV = @MaSV 
                                                    AND d.MaMon = mh.MaMon
                                                    AND ld.TenLoaiDiem NOT LIKE '%Thi%'
                                                    AND d.TrangThai = 'Initialize'
                                                    AND ld.TrangThai = 'Initialize'), 0) * 0.4
                                        ) +
                                        (
                                            ISNULL((SELECT MAX(d.GiaTriDiem)
                                                    FROM Diem d
                                                    JOIN LoaiDiem ld ON d.IDLoaiDiem = ld.IDLoaiDiem
                                                    WHERE d.MaSV = @MaSV 
                                                    AND d.MaMon = mh.MaMon
                                                    AND ld.TenLoaiDiem LIKE '%Thi%'
                                                    AND d.TrangThai = 'Initialize'
                                                    AND ld.TrangThai = 'Initialize'), 0) * 0.6
                                        )
                                    ) BETWEEN 6.5 AND 6.9 THEN 2.5
                                    WHEN (
                                        (
                                            ISNULL((SELECT SUM(d.GiaTriDiem * ld.TiLe) * 1.0 / NULLIF(SUM(ld.TiLe), 0)
                                                    FROM Diem d
                                                    JOIN LoaiDiem ld ON d.IDLoaiDiem = ld.IDLoaiDiem
                                                    WHERE d.MaSV = @MaSV 
                                                    AND d.MaMon = mh.MaMon
                                                    AND ld.TenLoaiDiem NOT LIKE '%Thi%'
                                                    AND d.TrangThai = 'Initialize'
                                                    AND ld.TrangThai = 'Initialize'), 0) * 0.4
                                        ) +
                                        (
                                            ISNULL((SELECT MAX(d.GiaTriDiem)
                                                    FROM Diem d
                                                    JOIN LoaiDiem ld ON d.IDLoaiDiem = ld.IDLoaiDiem
                                                    WHERE d.MaSV = @MaSV 
                                                    AND d.MaMon = mh.MaMon
                                                    AND ld.TenLoaiDiem LIKE '%Thi%'
                                                    AND d.TrangThai = 'Initialize'
                                                    AND ld.TrangThai = 'Initialize'), 0) * 0.6
                                        )
                                    ) BETWEEN 5.5 AND 6.4 THEN 2.0
                                    WHEN (
                                        (
                                            ISNULL((SELECT SUM(d.GiaTriDiem * ld.TiLe) * 1.0 / NULLIF(SUM(ld.TiLe), 0)
                                                    FROM Diem d
                                                    JOIN LoaiDiem ld ON d.IDLoaiDiem = ld.IDLoaiDiem
                                                    WHERE d.MaSV = @MaSV 
                                                    AND d.MaMon = mh.MaMon
                                                    AND ld.TenLoaiDiem NOT LIKE '%Thi%'
                                                    AND d.TrangThai = 'Initialize'
                                                    AND ld.TrangThai = 'Initialize'), 0) * 0.4
                                        ) +
                                        (
                                            ISNULL((SELECT MAX(d.GiaTriDiem)
                                                    FROM Diem d
                                                    JOIN LoaiDiem ld ON d.IDLoaiDiem = ld.IDLoaiDiem
                                                    WHERE d.MaSV = @MaSV 
                                                    AND d.MaMon = mh.MaMon
                                                    AND ld.TenLoaiDiem LIKE '%Thi%'
                                                    AND d.TrangThai = 'Initialize'
                                                    AND ld.TrangThai = 'Initialize'), 0) * 0.6
                                        )
                                    ) BETWEEN 5 AND 5.4 THEN 1.5
                                    WHEN (
                                        (
                                            ISNULL((SELECT SUM(d.GiaTriDiem * ld.TiLe) * 1.0 / NULLIF(SUM(ld.TiLe), 0)
                                                    FROM Diem d
                                                    JOIN LoaiDiem ld ON d.IDLoaiDiem = ld.IDLoaiDiem
                                                    WHERE d.MaSV = @MaSV 
                                                    AND d.MaMon = mh.MaMon
                                                    AND ld.TenLoaiDiem NOT LIKE '%Thi%'
                                                    AND d.TrangThai = 'Initialize'
                                                    AND ld.TrangThai = 'Initialize'), 0) * 0.4
                                        ) +
                                        (
                                            ISNULL((SELECT MAX(d.GiaTriDiem)
                                                    FROM Diem d
                                                    JOIN LoaiDiem ld ON d.IDLoaiDiem = ld.IDLoaiDiem
                                                    WHERE d.MaSV = @MaSV 
                                                    AND d.MaMon = mh.MaMon
                                                    AND ld.TenLoaiDiem LIKE '%Thi%'
                                                    AND d.TrangThai = 'Initialize'
                                                    AND ld.TrangThai = 'Initialize'), 0) * 0.6
                                        )
                                    ) BETWEEN 4.0 AND 4.9 THEN 1.0
                                    ELSE 0.0
                                END AS DiemHe4,


                                -- Điểm chữ
                                CASE 
                                    WHEN (
                                        (
                                            ISNULL((SELECT SUM(d.GiaTriDiem * ld.TiLe) * 1.0 / NULLIF(SUM(ld.TiLe), 0)
                                                    FROM Diem d
                                                    JOIN LoaiDiem ld ON d.IDLoaiDiem = ld.IDLoaiDiem
                                                    WHERE d.MaSV = @MaSV 
                                                    AND d.MaMon = mh.MaMon
                                                    AND ld.TenLoaiDiem NOT LIKE '%Thi%'
                                                    AND d.TrangThai = 'Initialize'
                                                    AND ld.TrangThai = 'Initialize'), 0) * 0.4
                                        ) +
                                        (
                                            ISNULL((SELECT MAX(d.GiaTriDiem)
                                                    FROM Diem d
                                                    JOIN LoaiDiem ld ON d.IDLoaiDiem = ld.IDLoaiDiem
                                                    WHERE d.MaSV = @MaSV 
                                                    AND d.MaMon = mh.MaMon
                                                    AND ld.TenLoaiDiem LIKE '%Thi%'
                                                    AND d.TrangThai = 'Initialize'
                                                    AND ld.TrangThai = 'Initialize'), 0) * 0.6
                                        )
                                    ) >= 8.5 THEN 'A'
                                    WHEN (
                                        (
                                            ISNULL((SELECT SUM(d.GiaTriDiem * ld.TiLe) * 1.0 / NULLIF(SUM(ld.TiLe), 0)
                                                    FROM Diem d
                                                    JOIN LoaiDiem ld ON d.IDLoaiDiem = ld.IDLoaiDiem
                                                    WHERE d.MaSV = @MaSV 
                                                    AND d.MaMon = mh.MaMon
                                                    AND ld.TenLoaiDiem NOT LIKE '%Thi%'
                                                    AND d.TrangThai = 'Initialize'
                                                    AND ld.TrangThai = 'Initialize'), 0) * 0.4
                                        ) +
                                        (
                                            ISNULL((SELECT MAX(d.GiaTriDiem)
                                                    FROM Diem d
                                                    JOIN LoaiDiem ld ON d.IDLoaiDiem = ld.IDLoaiDiem
                                                    WHERE d.MaSV = @MaSV 
                                                    AND d.MaMon = mh.MaMon
                                                    AND ld.TenLoaiDiem LIKE '%Thi%'
                                                    AND d.TrangThai = 'Initialize'
                                                    AND ld.TrangThai = 'Initialize'), 0) * 0.6
                                        )
                                    ) BETWEEN 8.0 AND 8.4 THEN 'B+'
                                    WHEN (
                                        (
                                            ISNULL((SELECT SUM(d.GiaTriDiem * ld.TiLe) * 1.0 / NULLIF(SUM(ld.TiLe), 0)
                                                    FROM Diem d
                                                    JOIN LoaiDiem ld ON d.IDLoaiDiem = ld.IDLoaiDiem
                                                    WHERE d.MaSV = @MaSV 
                                                    AND d.MaMon = mh.MaMon
                                                    AND ld.TenLoaiDiem NOT LIKE '%Thi%'
                                                    AND d.TrangThai = 'Initialize'
                                                    AND ld.TrangThai = 'Initialize'), 0) * 0.4
                                        ) +
                                        (
                                            ISNULL((SELECT MAX(d.GiaTriDiem)
                                                    FROM Diem d
                                                    JOIN LoaiDiem ld ON d.IDLoaiDiem = ld.IDLoaiDiem
                                                    WHERE d.MaSV = @MaSV 
                                                    AND d.MaMon = mh.MaMon
                                                    AND ld.TenLoaiDiem LIKE '%Thi%'
                                                    AND d.TrangThai = 'Initialize'
                                                    AND ld.TrangThai = 'Initialize'), 0) * 0.6
                                        )
                                    ) BETWEEN 7.0 AND 7.9 THEN 'B'
                                    WHEN (
                                        (
                                            ISNULL((SELECT SUM(d.GiaTriDiem * ld.TiLe) * 1.0 / NULLIF(SUM(ld.TiLe), 0)
                                                    FROM Diem d
                                                    JOIN LoaiDiem ld ON d.IDLoaiDiem = ld.IDLoaiDiem
                                                    WHERE d.MaSV = @MaSV 
                                                    AND d.MaMon = mh.MaMon
                                                    AND ld.TenLoaiDiem NOT LIKE '%Thi%'
                                                    AND d.TrangThai = 'Initialize'
                                                    AND ld.TrangThai = 'Initialize'), 0) * 0.4
                                        ) +
                                        (
                                            ISNULL((SELECT MAX(d.GiaTriDiem)
                                                    FROM Diem d
                                                    JOIN LoaiDiem ld ON d.IDLoaiDiem = ld.IDLoaiDiem
                                                    WHERE d.MaSV = @MaSV 
                                                    AND d.MaMon = mh.MaMon
                                                    AND ld.TenLoaiDiem LIKE '%Thi%'
                                                    AND d.TrangThai = 'Initialize'
                                                    AND ld.TrangThai = 'Initialize'), 0) * 0.6
                                        )
                                    ) BETWEEN 6.5 AND 6.9 THEN 'C+'
                                    WHEN (
                                        (
                                            ISNULL((SELECT SUM(d.GiaTriDiem * ld.TiLe) * 1.0 / NULLIF(SUM(ld.TiLe), 0)
                                                    FROM Diem d
                                                    JOIN LoaiDiem ld ON d.IDLoaiDiem = ld.IDLoaiDiem
                                                    WHERE d.MaSV = @MaSV 
                                                    AND d.MaMon = mh.MaMon
                                                    AND ld.TenLoaiDiem NOT LIKE '%Thi%'
                                                    AND d.TrangThai = 'Initialize'
                                                    AND ld.TrangThai = 'Initialize'), 0) * 0.4
                                        ) +
                                        (
                                            ISNULL((SELECT MAX(d.GiaTriDiem)
                                                    FROM Diem d
                                                    JOIN LoaiDiem ld ON d.IDLoaiDiem = ld.IDLoaiDiem
                                                    WHERE d.MaSV = @MaSV 
                                                    AND d.MaMon = mh.MaMon
                                                    AND ld.TenLoaiDiem LIKE '%Thi%'
                                                    AND d.TrangThai = 'Initialize'
                                                    AND ld.TrangThai = 'Initialize'), 0) * 0.6
                                        )
                                    ) BETWEEN 5.5 AND 6.4 THEN 'C'
                                    WHEN (
                                        (
                                            ISNULL((SELECT SUM(d.GiaTriDiem * ld.TiLe) * 1.0 / NULLIF(SUM(ld.TiLe), 0)
                                                    FROM Diem d
                                                    JOIN LoaiDiem ld ON d.IDLoaiDiem = ld.IDLoaiDiem
                                                    WHERE d.MaSV = @MaSV 
                                                    AND d.MaMon = mh.MaMon
                                                    AND ld.TenLoaiDiem NOT LIKE '%Thi%'
                                                    AND d.TrangThai = 'Initialize'
                                                    AND ld.TrangThai = 'Initialize'), 0) * 0.4
                                        ) +
                                        (
                                            ISNULL((SELECT MAX(d.GiaTriDiem)
                                                    FROM Diem d
                                                    JOIN LoaiDiem ld ON d.IDLoaiDiem = ld.IDLoaiDiem
                                                    WHERE d.MaSV = @MaSV 
                                                    AND d.MaMon = mh.MaMon
                                                    AND ld.TenLoaiDiem LIKE '%Thi%'
                                                    AND d.TrangThai = 'Initialize'
                                                    AND ld.TrangThai = 'Initialize'), 0) * 0.6
                                        )
                                    ) BETWEEN 5 AND 5.4 THEN 'D+'
                                    WHEN (
                                        (
                                            ISNULL((SELECT SUM(d.GiaTriDiem * ld.TiLe) * 1.0 / NULLIF(SUM(ld.TiLe), 0)
                                                    FROM Diem d
                                                    JOIN LoaiDiem ld ON d.IDLoaiDiem = ld.IDLoaiDiem
                                                    WHERE d.MaSV = @MaSV 
                                                    AND d.MaMon = mh.MaMon
                                                    AND ld.TenLoaiDiem NOT LIKE '%Thi%'
                                                    AND d.TrangThai = 'Initialize'
                                                    AND ld.TrangThai = 'Initialize'), 0) * 0.4
                                        ) +
                                        (
                                            ISNULL((SELECT MAX(d.GiaTriDiem)
                                                    FROM Diem d
                                                    JOIN LoaiDiem ld ON d.IDLoaiDiem = ld.IDLoaiDiem
                                                    WHERE d.MaSV = @MaSV 
                                                    AND d.MaMon = mh.MaMon
                                                    AND ld.TenLoaiDiem LIKE '%Thi%'
                                                    AND d.TrangThai = 'Initialize'
                                                    AND ld.TrangThai = 'Initialize'), 0) * 0.6
                                        )
                                    ) BETWEEN 4.0 AND 4.9 THEN 'D'
                                    ELSE 'F'
                                END AS DiemChu,
                               -- Xếp loại
                                CASE 
                                    WHEN (
                                        (
                                            -- Điểm thường kỳ (40%)
                                            ISNULL((SELECT SUM(d.GiaTriDiem * ld.TiLe) * 1.0 / NULLIF(SUM(ld.TiLe), 0)
                                                    FROM Diem d
                                                    JOIN LoaiDiem ld ON d.IDLoaiDiem = ld.IDLoaiDiem
                                                    WHERE d.MaSV = @MaSV 
                                                    AND d.MaMon = mh.MaMon
                                                    AND ld.TenLoaiDiem NOT LIKE '%Thi%'
                                                    AND d.TrangThai = 'Initialize'
                                                    AND ld.TrangThai = 'Initialize'), 0) * 0.4
                                        ) +
                                        (
                                            -- Điểm cuối kỳ (60%)
                                            ISNULL((SELECT MAX(d.GiaTriDiem)
                                                    FROM Diem d
                                                    JOIN LoaiDiem ld ON d.IDLoaiDiem = ld.IDLoaiDiem
                                                    WHERE d.MaSV = @MaSV 
                                                    AND d.MaMon = mh.MaMon
                                                    AND ld.TenLoaiDiem LIKE '%Thi%'
                                                    AND d.TrangThai = 'Initialize'
                                                    AND ld.TrangThai = 'Initialize'), 0) * 0.6
                                        )
                                    ) >= 8.5 THEN N'Giỏi'
                                    WHEN (
                                        (
                                            -- Điểm thường kỳ (40%)
                                            ISNULL((SELECT SUM(d.GiaTriDiem * ld.TiLe) * 1.0 / NULLIF(SUM(ld.TiLe), 0)
                                                    FROM Diem d
                                                    JOIN LoaiDiem ld ON d.IDLoaiDiem = ld.IDLoaiDiem
                                                    WHERE d.MaSV = @MaSV 
                                                    AND d.MaMon = mh.MaMon
                                                    AND ld.TenLoaiDiem NOT LIKE '%Thi%'
                                                    AND d.TrangThai = 'Initialize'
                                                    AND ld.TrangThai = 'Initialize'), 0) * 0.4
                                        ) +
                                        (
                                            -- Điểm cuối kỳ (60%)
                                            ISNULL((SELECT MAX(d.GiaTriDiem)
                                                    FROM Diem d
                                                    JOIN LoaiDiem ld ON d.IDLoaiDiem = ld.IDLoaiDiem
                                                    WHERE d.MaSV = @MaSV 
                                                    AND d.MaMon = mh.MaMon
                                                    AND ld.TenLoaiDiem LIKE '%Thi%'
                                                    AND d.TrangThai = 'Initialize'
                                                    AND ld.TrangThai = 'Initialize'), 0) * 0.6
                                        )
                                    ) BETWEEN 8.0 AND 8.4 THEN N'Khá Giỏi'
                                    WHEN (
                                        (
                                            -- Điểm thường kỳ (40%)
                                            ISNULL((SELECT SUM(d.GiaTriDiem * ld.TiLe) * 1.0 / NULLIF(SUM(ld.TiLe), 0)
                                                    FROM Diem d
                                                    JOIN LoaiDiem ld ON d.IDLoaiDiem = ld.IDLoaiDiem
                                                    WHERE d.MaSV = @MaSV 
                                                    AND d.MaMon = mh.MaMon
                                                    AND ld.TenLoaiDiem NOT LIKE '%Thi%'
                                                    AND d.TrangThai = 'Initialize'
                                                    AND ld.TrangThai = 'Initialize'), 0) * 0.4
                                        ) +
                                        (
                                            -- Điểm cuối kỳ (60%)
                                            ISNULL((SELECT MAX(d.GiaTriDiem)
                                                    FROM Diem d
                                                    JOIN LoaiDiem ld ON d.IDLoaiDiem = ld.IDLoaiDiem
                                                    WHERE d.MaSV = @MaSV 
                                                    AND d.MaMon = mh.MaMon
                                                    AND ld.TenLoaiDiem LIKE '%Thi%'
                                                    AND d.TrangThai = 'Initialize'
                                                    AND ld.TrangThai = 'Initialize'), 0) * 0.6
                                        )
                                    ) BETWEEN 7.0 AND 7.9 THEN N'Khá'
                                    WHEN (
                                        (
                                            -- Điểm thường kỳ (40%)
                                            ISNULL((SELECT SUM(d.GiaTriDiem * ld.TiLe) * 1.0 / NULLIF(SUM(ld.TiLe), 0)
                                                    FROM Diem d
                                                    JOIN LoaiDiem ld ON d.IDLoaiDiem = ld.IDLoaiDiem
                                                    WHERE d.MaSV = @MaSV 
                                                    AND d.MaMon = mh.MaMon
                                                    AND ld.TenLoaiDiem NOT LIKE '%Thi%'
                                                    AND d.TrangThai = 'Initialize'
                                                    AND ld.TrangThai = 'Initialize'), 0) * 0.4
                                        ) +
                                        (
                                            -- Điểm cuối kỳ (60%)
                                            ISNULL((SELECT MAX(d.GiaTriDiem)
                                                    FROM Diem d
                                                    JOIN LoaiDiem ld ON d.IDLoaiDiem = ld.IDLoaiDiem
                                                    WHERE d.MaSV = @MaSV 
                                                    AND d.MaMon = mh.MaMon
                                                    AND ld.TenLoaiDiem LIKE '%Thi%'
                                                    AND d.TrangThai = 'Initialize'
                                                    AND ld.TrangThai = 'Initialize'), 0) * 0.6
                                        )
                                    ) BETWEEN 6.5 AND 6.9 THEN N'Trung Bình Khá'
                                    WHEN (
                                        (
                                            -- Điểm thường kỳ (40%)
                                            ISNULL((SELECT SUM(d.GiaTriDiem * ld.TiLe) * 1.0 / NULLIF(SUM(ld.TiLe), 0)
                                                    FROM Diem d
                                                    JOIN LoaiDiem ld ON d.IDLoaiDiem = ld.IDLoaiDiem
                                                    WHERE d.MaSV = @MaSV 
                                                    AND d.MaMon = mh.MaMon
                                                    AND ld.TenLoaiDiem NOT LIKE '%Thi%'
                                                    AND d.TrangThai = 'Initialize'
                                                    AND ld.TrangThai = 'Initialize'), 0) * 0.4
                                        ) +
                                        (
                                            -- Điểm cuối kỳ (60%)
                                            ISNULL((SELECT MAX(d.GiaTriDiem)
                                                    FROM Diem d
                                                    JOIN LoaiDiem ld ON d.IDLoaiDiem = ld.IDLoaiDiem
                                                    WHERE d.MaSV = @MaSV 
                                                    AND d.MaMon = mh.MaMon
                                                    AND ld.TenLoaiDiem LIKE '%Thi%'
                                                    AND d.TrangThai = 'Initialize'
                                                    AND ld.TrangThai = 'Initialize'), 0) * 0.6
                                        )
                                    ) BETWEEN 5.5 AND 6.4 THEN N'Trung Bình'
                                    WHEN (
                                        (
                                            -- Điểm thường kỳ (40%)
                                            ISNULL((SELECT SUM(d.GiaTriDiem * ld.TiLe) * 1.0 / NULLIF(SUM(ld.TiLe), 0)
                                                    FROM Diem d
                                                    JOIN LoaiDiem ld ON d.IDLoaiDiem = ld.IDLoaiDiem
                                                    WHERE d.MaSV = @MaSV 
                                                    AND d.MaMon = mh.MaMon
                                                    AND ld.TenLoaiDiem NOT LIKE '%Thi%'
                                                    AND d.TrangThai = 'Initialize'
                                                    AND ld.TrangThai = 'Initialize'), 0) * 0.4
                                        ) +
                                        (
                                            -- Điểm cuối kỳ (60%)
                                            ISNULL((SELECT MAX(d.GiaTriDiem)
                                                    FROM Diem d
                                                    JOIN LoaiDiem ld ON d.IDLoaiDiem = ld.IDLoaiDiem
                                                    WHERE d.MaSV = @MaSV 
                                                    AND d.MaMon = mh.MaMon
                                                    AND ld.TenLoaiDiem LIKE '%Thi%'
                                                    AND d.TrangThai = 'Initialize'
                                                    AND ld.TrangThai = 'Initialize'), 0) * 0.6
                                        )
                                    ) BETWEEN 5.0 AND 5.4 THEN N'Trung bình Yếu'
                                    WHEN (
                                        (
                                            -- Điểm thường kỳ (40%)
                                            ISNULL((SELECT SUM(d.GiaTriDiem * ld.TiLe) * 1.0 / NULLIF(SUM(ld.TiLe), 0)
                                                    FROM Diem d
                                                    JOIN LoaiDiem ld ON d.IDLoaiDiem = ld.IDLoaiDiem
                                                    WHERE d.MaSV = @MaSV 
                                                    AND d.MaMon = mh.MaMon
                                                    AND ld.TenLoaiDiem NOT LIKE '%Thi%'
                                                    AND d.TrangThai = 'Initialize'
                                                    AND ld.TrangThai = 'Initialize'), 0) * 0.4
                                        ) +
                                        (
                                            -- Điểm cuối kỳ (60%)
                                            ISNULL((SELECT MAX(d.GiaTriDiem)
                                                    FROM Diem d
                                                    JOIN LoaiDiem ld ON d.IDLoaiDiem = ld.IDLoaiDiem
                                                    WHERE d.MaSV = @MaSV 
                                                    AND d.MaMon = mh.MaMon
                                                    AND ld.TenLoaiDiem LIKE '%Thi%'
                                                    AND d.TrangThai = 'Initialize'
                                                    AND ld.TrangThai = 'Initialize'), 0) * 0.6
                                        )
                                    ) BETWEEN 4.0 AND 4.9 THEN N'Yếu'
                                    ELSE N'Kém (Không Đạt)'
                                END AS XepLoai

                            FROM 
                                MonHoc mh
                            JOIN 
                                LoaiMon lm ON mh.IDLoaiMon = lm.IDLoaiMon
                            WHERE 
                                mh.TrangThai = 'Initialize'
                                AND lm.TrangThai = 'Initialize'
                                -- Bỏ qua các môn có ĐiểmTrungBình = 0
                                        AND (
                                            (
                                                ISNULL((SELECT SUM(d.GiaTriDiem * ld.TiLe) * 1.0 / NULLIF(SUM(ld.TiLe), 0)
                                                        FROM Diem d
                                                        JOIN LoaiDiem ld ON d.IDLoaiDiem = ld.IDLoaiDiem
                                                        WHERE d.MaSV = @MaSV 
                                                        AND d.MaMon = mh.MaMon
                                                        AND ld.TenLoaiDiem NOT LIKE '%Thi%'
                                                        AND d.TrangThai = 'Initialize'
                                                        AND ld.TrangThai = 'Initialize'), 0) * 0.4
                                            ) +
                                            (
                                                ISNULL((SELECT MAX(d.GiaTriDiem)
                                                        FROM Diem d
                                                        JOIN LoaiDiem ld ON d.IDLoaiDiem = ld.IDLoaiDiem
                                                        WHERE d.MaSV = @MaSV 
                                                        AND d.MaMon = mh.MaMon
                                                        AND ld.TenLoaiDiem LIKE '%Thi%'
                                                        AND d.TrangThai = 'Initialize'
                                                        AND ld.TrangThai = 'Initialize'), 0) * 0.6
                                            )
                                        ) > 0
                            ORDER BY 
                                mh.TenMon, lm.LoaiMon;";

            SqlParameter[] parameters = { new SqlParameter("@MaSV", maSinhVien) };
            DataTable dtChiTiet = DataBase.GetData(query, parameters);

            if (dtChiTiet != null && dtChiTiet.Rows.Count > 0)
            {
                //dataGridView1.DataSource = null;
                dataGridView1.DataSource = dtChiTiet;
            }
            else
            {
                MessageBox.Show("Không có dữ liệu chi tiết sinh viên.", "Thông báo");
                dataGridView1.DataSource = null;
            }
        }

        private void cboSinhVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSinhVien.SelectedValue != null && int.TryParse(cboSinhVien.SelectedValue.ToString(), out int selectedSinhVienId))
            {
                int hocKyId = (int)cboHocKy.SelectedValue; 
                Console.WriteLine($"Sinh viên được chọn: {selectedSinhVienId}, Học kỳ: {hocKyId}");

                LoadChiTietSinhVien(selectedSinhVienId); 
            }
            else
            {
                Console.WriteLine("Không có sinh viên hợp lệ được chọn.");
            }
        }
    }
}
