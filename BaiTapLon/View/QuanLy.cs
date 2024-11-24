using BaiTapLon.View.NangCao;
using BaiTapLon.View.TimKiem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaiTapLon
{
    public partial class QuanLy : Form
    {
        public QuanLy()
        {
            InitializeComponent();
        }
        private void CloseAllChildForms()
        {
            foreach (Form child in this.MdiChildren)
            {
                child.Close();
            }
        }
        private void quảnLýSinhViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseAllChildForms();
            QLSinhVien frm = new QLSinhVien();
            frm.MdiParent = this;
            frm.Show();
        }

        private void quảnLýLớpHọcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseAllChildForms();
            QLLopHoc frm = new QLLopHoc();
            frm.MdiParent = this;
            frm.Show();
        }

        private void quảnLýMônHọcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseAllChildForms();
            QLMonHoc frm = new QLMonHoc();
            frm.MdiParent = this;
            frm.Show();
        }

        private void quảnLýĐiểmDanhToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseAllChildForms();
            QLDiemDanh frm = new QLDiemDanh();
            frm.MdiParent = this;
            frm.Show();
        }

        private void quảnLýĐiểmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseAllChildForms();
            QLDiem frm = new QLDiem();
            frm.MdiParent = this;
            frm.Show();
        }

        private void quảnLýLoạiĐiểmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseAllChildForms();
            QLLoaiDiem frm = new QLLoaiDiem();
            frm.MdiParent = this;
            frm.Show();
        }

        private void quảnLýLớpSinhViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseAllChildForms();
            QLLopSinhVien frm = new QLLopSinhVien();
            frm.MdiParent = this;
            frm.Show();
        }

        private void chuyênNgànhToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseAllChildForms();
            QLChuyenNganh frm = new QLChuyenNganh();
            frm.MdiParent = this;
            frm.Show();
        }

        private void họcKỳToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseAllChildForms();
            QLHocKy frm = new QLHocKy();
            frm.MdiParent = this;
            frm.Show();
        }

        private void loạiMônToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseAllChildForms();
            QLLoaiMon frm = new QLLoaiMon();
            frm.MdiParent = this;
            frm.Show();
        }
        private void thungfToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseAllChildForms();
            ThungRac frm = new ThungRac();
            frm.MdiParent = this;
            frm.Show();
        }

        private void mônHọcThuộcLoạiMônToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseAllChildForms();
            NC4 frm = new NC4();
            frm.MdiParent = this;
            frm.Show();
        }

        private void lớpTheoMônHọcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseAllChildForms();
            NC5 frm = new NC5();
            frm.MdiParent = this;
            frm.Show();
        }

        private void lớpTheoHọcKỳToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseAllChildForms();
            NC6 frm = new NC6();
            frm.MdiParent = this;
            frm.Show();
        }

        private void danhSáchĐãĐangHọcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseAllChildForms();
            NC1 frm = new NC1();
            frm.MdiParent = this;
            frm.Show();
        }

        private void sinhViênTheoChuyênGànhToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseAllChildForms();
            NC2 frm = new NC2();
            frm.MdiParent = this;
            frm.Show();
        }

        private void sinhViênPassTrượtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseAllChildForms();
            NC7 frm = new NC7();
            frm.MdiParent = this;
            frm.Show();
        }

        private void sinhViênPassTrượt8ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseAllChildForms();
            NC8 frm = new NC8();
            frm.MdiParent = this;
            frm.Show();
        }

        private void QuanLy_Load(object sender, EventArgs e)
        {

        }

        private void tk1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseAllChildForms();
            TKSinhVien frm = new TKSinhVien();
            frm.MdiParent = this;
            frm.Show();
        }

        private void tk2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseAllChildForms();
            TKLopHoc frm = new TKLopHoc();
            frm.MdiParent = this;
            frm.Show();
        }

        private void tk3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseAllChildForms();
            TKMonHoc  frm = new TKMonHoc();
            frm.MdiParent = this;
            frm.Show();
        }


        private void danhSáchĐiểmTheoHọcKỳCủaSinhViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseAllChildForms();
            NC9 frm = new NC9();
            frm.MdiParent = this;
            frm.Show();
        }

        private void họcKỳToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CloseAllChildForms();
            TKHocKy frm = new TKHocKy();
            frm.MdiParent = this;
            frm.Show();
        }

        private void loạiMônToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CloseAllChildForms();
            TKLoaiMon frm = new TKLoaiMon();
            frm.MdiParent = this;
            frm.Show();
        }

        private void chuyênNgànhToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CloseAllChildForms();
            TKChuyenNganh frm = new TKChuyenNganh();
            frm.MdiParent = this;
            frm.Show();
        }

        private void điểmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseAllChildForms();
            TKDiem frm = new TKDiem();
            frm.MdiParent = this;
            frm.Show();
        }

        private void điểmDanhToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseAllChildForms();
            TKDiemDanh frm = new TKDiemDanh();
            frm.MdiParent = this;
            frm.Show();
        }

        private void lớpHọcSinhViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseAllChildForms();
            TKLopHocSinhVien frm = new TKLopHocSinhVien();
            frm.MdiParent = this;
            frm.Show();
        }

        private void loạiĐiểmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseAllChildForms();
            TKLoaiDiem frm = new TKLoaiDiem();
            frm.MdiParent = this;
            frm.Show();
        }
    }
}
