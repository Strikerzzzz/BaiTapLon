using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaiTapLon.Model
{
    internal class MonHoc
    {
        public MonHoc(string maMon, string tenMon, int soTinChi, string iDLoaiMon, int tongSoBuoiHoc)
        {
            MaMon = maMon;
            TenMon = tenMon;
            SoTinChi = soTinChi;
            IDLoaiMon = iDLoaiMon;
            TongSoBuoiHoc = tongSoBuoiHoc;
        }
        public MonHoc() { }
        public string MaMon { get; set; }
        public string TenMon { get; set; }
        public int SoTinChi { get; set; }
        public string IDLoaiMon { get; set; }
        public int TongSoBuoiHoc { get; set; }
    }
}
