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

        private string MaMon { get; set; }
        private string TenMon { get; set; }
        private int SoTinChi { get; set; }
        private string IDLoaiMon { get; set; }
        private int TongSoBuoiHoc { get; set; }
    }
}
