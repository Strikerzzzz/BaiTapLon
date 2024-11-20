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
    internal class DiemController
    {
        public Dictionary<string, string> dictMH = new Dictionary<string, string>();
        public Dictionary<string, string> dictLD = new Dictionary<string, string>();
        public Dictionary<string, string> dictSV = new Dictionary<string, string>();

        public DataTable LoadDiemData()
        {
            return DataBase.GetData("SELECT IDDiem, CONCAT(sv.HoDem, ' ', sv.Ten,'-',sv.MaSV) AS TenSinhVien, mh.TenMon, ld.TenLoaiDiem, GiaTriDiem, LanThi FROM Diem d " +
                                    "LEFT JOIN MonHoc mh on d.MaMon = mh.MaMon " +
                                    "LEFT JOIN LoaiDiem ld on d.IDLoaiDiem = ld.IDLoaiDiem " +
                                    "LEFT JOIN SinhVien sv on d.MaSV = sv.MaSV " +
                                    "where d.TrangThai = 'Initialize' AND mh.TrangThai = 'Initialize' AND ld.TrangThai = 'Initialize' AND sv.TrangThai = 'Initialize'");
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

        public bool AddDiem(Diem diem)
        {
            string query = "INSERT INTO Diem (MaSV, MaMon, IDLoaiDiem, GiaTriDiem, LanThi, TrangThai) VALUES (@MaSV, @MaMon, @IDLoaiDiem, @GiaTriDiem, @LanThi, 'Initialize')";
            SqlParameter[] parameters = {
                new SqlParameter("@MaSV", diem.MaSV),
                new SqlParameter("@MaMon", diem.MaMon),
                new SqlParameter("@IDLoaiDiem", diem.IDLoaiDiem),
                new SqlParameter("@GiaTriDiem", diem.GiaTriDiem),
                new SqlParameter("@LanThi", diem.LanThi),
            };

            return new DataBase().UpdateData(query, parameters);
        }

        public bool UpdateDiem(Diem diem)
        {
            string query = "UPDATE Diem SET MaSV = @MaSV, MaMon = @MaMon, IDLoaiDiem = @IDLoaiDiem, GiaTriDiem = @GiaTriDiem, LanThi = @LanThi WHERE IDDiem = @IDDiem";
            SqlParameter[] parameters = {
                new SqlParameter("@MaSV", diem.MaSV),
                new SqlParameter("@MaMon", diem.MaMon),
                new SqlParameter("@IDLoaiDiem", diem.IDLoaiDiem),
                new SqlParameter("@GiaTriDiem", diem.GiaTriDiem),
                new SqlParameter("@LanThi", diem.LanThi),
                new SqlParameter("@IDDiem", diem.IDDiem),
            };

            return new DataBase().UpdateData(query, parameters);
        }

        public bool DeleteDiem(int idDiem)
        {
            string query = "UPDATE Diem SET TrangThai = 'Deleted' WHERE IDDiem = @IDDiem";
            SqlParameter[] parameters = {
                new SqlParameter("@IDDiem", idDiem)
            };

            return new DataBase().UpdateData(query, parameters);
        }
    }
}
