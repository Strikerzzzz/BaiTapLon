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
    internal class SinhVienController
    {
        public static List<SinhVien> GetAllSinhVien()
        {
            string query = @"SELECT MaSV, HoDem, Ten, Email, CCCD, SoDienThoai, GioiTinh, NgaySinh, DiaChi, 
                     sv.MaChuyenNganh, cn.TenChuyenNganh, KhoaHoc 
                     FROM SinhVien sv 
                     LEFT JOIN ChuyenNganh cn ON cn.MaChuyenNganh = sv.MaChuyenNganh 
                     WHERE sv.TrangThai = 'Initialize'";
            DataTable dt = DataBase.GetData(query);
            List<SinhVien> list = new List<SinhVien>();

            foreach (DataRow row in dt.Rows)
            {
                SinhVien sinhVien = new SinhVien
                {
                    MaSV = Convert.ToInt32(row["MaSV"]),
                    HoDem = row["HoDem"].ToString(),
                    Ten = row["Ten"].ToString(),
                    Email = row["Email"].ToString(),
                    CCCD = row["CCCD"].ToString(),
                    SoDienThoai = row["SoDienThoai"].ToString(),
                    GioiTinh = row["GioiTinh"].ToString(),
                    NgaySinh = Convert.ToDateTime(row["NgaySinh"]),
                    DiaChi = row["DiaChi"].ToString(),
                    MaChuyenNganh = Convert.ToInt32(row["MaChuyenNganh"]), 
                    KhoaHoc = row["KhoaHoc"].ToString()
                };
                list.Add(sinhVien);
            }

            return list;
        }
        public static bool AddSinhVien(SinhVien sinhVien)
        {
            string query = "INSERT INTO SinhVien (HoDem, Ten, Email, CCCD, SoDienThoai, GioiTinh, NgaySinh, DiaChi, MaChuyenNganh, KhoaHoc, TrangThai) " +
                           "VALUES (@HoDem, @Ten, @Email, @CCCD, @SoDienThoai, @GioiTinh, @NgaySinh, @DiaChi, @MaChuyenNganh, @KhoaHoc, 'Initialize')";
            SqlParameter[] parameters = {
                new SqlParameter("@HoDem", sinhVien.HoDem),
                new SqlParameter("@Ten", sinhVien.Ten),
                new SqlParameter("@Email", sinhVien.Email),
                new SqlParameter("@CCCD", sinhVien.CCCD),
                new SqlParameter("@SoDienThoai", sinhVien.SoDienThoai),
                new SqlParameter("@GioiTinh", sinhVien.GioiTinh),
                new SqlParameter("@NgaySinh", sinhVien.NgaySinh),
                new SqlParameter("@DiaChi", sinhVien.DiaChi),
                new SqlParameter("@MaChuyenNganh", sinhVien.MaChuyenNganh),
                new SqlParameter("@KhoaHoc", sinhVien.KhoaHoc)
            };
            return new DataBase().UpdateData(query, parameters);
        }
        public static bool UpdateSinhVien(SinhVien sinhVien)
        {
            string query = "UPDATE SinhVien SET HoDem = @HoDem, Ten = @Ten, Email = @Email, CCCD = @CCCD, SoDienThoai = @SoDienThoai, " +
                           "GioiTinh = @GioiTinh, NgaySinh = @NgaySinh, DiaChi = @DiaChi, MaChuyenNganh = @MaChuyenNganh, KhoaHoc = @KhoaHoc " +
                           "WHERE MaSV = @MaSV";
            SqlParameter[] parameters = {
                new SqlParameter("@HoDem", sinhVien.HoDem),
                new SqlParameter("@Ten", sinhVien.Ten),
                new SqlParameter("@Email", sinhVien.Email),
                new SqlParameter("@CCCD", sinhVien.CCCD),
                new SqlParameter("@SoDienThoai", sinhVien.SoDienThoai),
                new SqlParameter("@GioiTinh", sinhVien.GioiTinh),
                new SqlParameter("@NgaySinh", sinhVien.NgaySinh),
                new SqlParameter("@DiaChi", sinhVien.DiaChi),
                new SqlParameter("@MaChuyenNganh", sinhVien.MaChuyenNganh),
                new SqlParameter("@KhoaHoc", sinhVien.KhoaHoc),
                new SqlParameter("@MaSV", sinhVien.MaSV)
            };
            return new DataBase().UpdateData(query, parameters);
        }
        public static bool DeleteSinhVien(string maSV)
        {
            string query = "UPDATE SinhVien SET TrangThai = 'Deleted' WHERE MaSV = @MaSV";
            SqlParameter[] parameters = {
                new SqlParameter("@MaSV", maSV)
            };
            return new DataBase().UpdateData(query, parameters);
        }
    }
}
