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

        private void quảnLýSinhViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QLSinhVien frm = new QLSinhVien();
            frm.MdiParent = this;
            frm.Show();
        }

        private void quảnLýLớpHọcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QLLopHoc frm = new QLLopHoc();
            frm.MdiParent = this;
            frm.Show();
        }

        private void quảnLýMônHọcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QLMonHoc frm = new QLMonHoc();
            frm.MdiParent = this;
            frm.Show();
        }

        private void quảnLýĐiểmDanhToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QLDiemDanh frm = new QLDiemDanh();
            frm.MdiParent = this;
            frm.Show();
        }

        private void quảnLýĐiểmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QLDiem frm = new QLDiem();
            frm.MdiParent = this;
            frm.Show();
        }

        private void quảnLýLoạiĐiểmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QLLoaiDiem frm = new QLLoaiDiem();
            frm.MdiParent = this;
            frm.Show();
        }

        private void quảnLýLớpSinhViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QLLopSinhVien frm = new QLLopSinhVien();
            frm.MdiParent = this;
            frm.Show();
        }

        private void chuyênNgànhToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QLChuyenNganh frm = new QLChuyenNganh();
            frm.MdiParent = this;
            frm.Show();
        }

        private void họcKỳToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QLHocKy frm = new QLHocKy();
            frm.MdiParent = this;
            frm.Show();
        }

        private void loạiMônToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QLLoaiMon frm = new QLLoaiMon();
            frm.MdiParent = this;
            frm.Show();
        }
    }
}
