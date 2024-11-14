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
    internal class HocKyController
    {
        public DataTable GetAllHocKy()
        {
            return DataBase.GetData("SELECT IDHocKy, TenHocKy, Nam FROM HocKy WHERE TrangThai = 'Initialize'");
        }

        public bool AddHocKy(HocKy hocKy)
        {
            string query = "INSERT INTO HocKy (TenHocKy, Nam, TrangThai) VALUES (@TenHocKy, @Nam, 'Initialize')";
            SqlParameter[] parameters = {
                new SqlParameter("@TenHocKy", hocKy.TenHocKy),
                new SqlParameter("@Nam", hocKy.Nam)
            };
            return new DataBase().UpdateData(query, parameters);
        }

        public bool UpdateHocKy(HocKy hocKy)
        {
            string query = "UPDATE HocKy SET TenHocKy = @TenHocKy, Nam = @Nam WHERE IDHocKy = @IDHocKy";
            SqlParameter[] parameters = {
                new SqlParameter("@IDHocKy", hocKy.IDHocKy),
                new SqlParameter("@TenHocKy", hocKy.TenHocKy),
                new SqlParameter("@Nam", hocKy.Nam)
            };
            return new DataBase().UpdateData(query, parameters);
        }

        public bool DeleteHocKy(int idHocKy)
        {
            string query = "UPDATE HocKy SET TrangThai = 'Deleted' WHERE IDHocKy = @IDHocKy";
            SqlParameter[] parameters = { new SqlParameter("@IDHocKy", idHocKy) };
            return new DataBase().UpdateData(query, parameters);
        }
    }
}
