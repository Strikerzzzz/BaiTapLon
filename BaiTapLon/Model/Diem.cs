using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaiTapLon.Model
{
    internal class Diem
    {
        public Diem(int idDiem, string maSV, string maMon, string idLoaiDiem, double giaTriDiem, int lanThi)
        {
            IDDiem = idDiem;
            MaSV = maSV;
            MaMon = maMon;
            IDLoaiDiem = idLoaiDiem;
            GiaTriDiem = giaTriDiem;
            LanThi = lanThi;
        }

        public Diem() { }

        public int IDDiem { get; set; }
        public string MaSV { get; set; }
        public string MaMon { get; set; }
        public string IDLoaiDiem { get; set; }
        public double GiaTriDiem { get; set; }
        public int LanThi { get; set; }
    }
}
