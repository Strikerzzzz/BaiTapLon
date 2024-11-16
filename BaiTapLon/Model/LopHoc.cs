using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaiTapLon.Model
{
    internal class LopHoc
    {
        public int MaLop { get; set; }
        public string TenLop { get; set; }
        public string KhoaHoc { get; set; }
        public int SoSVMax { get; set; }
        public int? MaMon { get; set; }
        public int? IDHocKy { get; set; }
        
        public DateTime NgayKetThuc {  get; set; }

    }
}
