using BaiTapLon.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaiTapLon.Controller
{
    internal class DiemDanhController
    {
        public Dictionary<string, string> dictSV = new Dictionary<string, string>();
        public Dictionary<string, string> dictML = new Dictionary<string, string>();

        public DataTable LoadDiemDanhData()
        {
            return DataBase.GetData("SELECT dd.IDDiemDanh, CONCAT(sv.HoDem, ' ', sv.Ten, '-', sv.MaSV) AS TenSinhVien, lh.TenLop, dd.NgayDiemDanh, dd.TTDiemDanh " +
                                    "FROM DiemDanh dd " +
                                    "LEFT JOIN LopHoc lh ON lh.MaLop = dd.MaLop " +
                                    "LEFT JOIN SinhVien sv ON dd.MaSV = sv.MaSV " +
                                    "WHERE dd.TrangThai = 'Initialize'");
        }

        public void LoadListDB(DataTable dt, Dictionary<string, string> dict)
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
        }

        public bool AddDiemDanh(DiemDanh diemDanh)
        {
            string query = "INSERT INTO DiemDanh (MaSV, MaLop, NgayDiemDanh, TTDiemDanh, TrangThai) VALUES (@MaSV, @MaLop, @NgayDiemDanh, @TTDiemDanh, 'Initialize')";
            SqlParameter[] parameters = {
                new SqlParameter("@MaSV", diemDanh.MaSV),
                new SqlParameter("@MaLop", diemDanh.MaLop),
                new SqlParameter("@NgayDiemDanh", diemDanh.NgayDiemDanh),
                new SqlParameter("@TTDiemDanh", diemDanh.TTDiemDanh),
            };

            return new DataBase().UpdateData(query, parameters);
        }

        public bool UpdateDiemDanh(DiemDanh diemDanh)
        {
            string query = "UPDATE DiemDanh SET MaSV = @MaSV, MaLop = @MaLop, NgayDiemDanh = @NgayDiemDanh, TTDiemDanh = @TTDiemDanh WHERE IDDiemDanh = @IDDiemDanh";
            SqlParameter[] parameters = {
                new SqlParameter("@MaSV", diemDanh.MaSV),
                new SqlParameter("@MaLop", diemDanh.MaLop),
                new SqlParameter("@NgayDiemDanh", diemDanh.NgayDiemDanh),
                new SqlParameter("@TTDiemDanh", diemDanh.TTDiemDanh),
                new SqlParameter("@IDDiemDanh", diemDanh.IDDiemDanh),
            };

            return new DataBase().UpdateData(query, parameters);
        }

        public bool DeleteDiemDanh(int idDiemDanh)
        {
            string query = "UPDATE DiemDanh SET TrangThai = 'Deleted' WHERE IDDiemDanh = @IDDiemDanh";
            SqlParameter[] parameters = {
                new SqlParameter("@IDDiemDanh", idDiemDanh)
            };

            return new DataBase().UpdateData(query, parameters);
        }
        public DataTable LoadSinhVienTheoLop(int maLop)
        {
            string query = "SELECT sv.MaSV, CONCAT(sv.HoDem, ' ', sv.Ten, '-', sv.MaSV) AS TenSinhVien FROM LopHoc_SinhVien ls " +
                "LEFT JOIN SinhVien sv ON ls.MaSV = sv.MaSV  WHERE MaLop = @MaLop AND ls.TrangThai = 'Initialize'";
            SqlParameter[] parameters = {
                new SqlParameter("@MaLop", maLop)
            };

            return DataBase.GetData(query, parameters);
        }

    }
}
