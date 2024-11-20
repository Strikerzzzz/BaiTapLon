using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaiTapLon.View.TimKiem
{
    public partial class TKMonHoc : Form
    {
        public TKMonHoc()
        {
            InitializeComponent();
        }
        Dictionary<string, string> dictLM = new Dictionary<string, string>();
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TKMonHoc_Load(object sender, EventArgs e)
        {
            comboBox.Items.Add("Mã môn");
            comboBox.Items.Add("Tên môn");
            comboBox.Items.Add("Số tín chỉ");
            comboBox.Items.Add("Loại môn");
            comboBox.Items.Add("Tổng số buổi học");
            comboBox.SelectedIndex = 0;
        }

        private void btnTK_Click(object sender, EventArgs e)
        {
            string value = txtTK.Text;
            string selected = "MaMon";
            switch (comboBox.SelectedItem.ToString())
            {
                case "Mã môn":
                    selected = "MaMon";
                    break;
                case "Tên môn":
                    selected = "TenMon";
                    break;
                case "Số tín chỉ":
                    selected = "SoTinChi";
                    break;
                case "Loại môn":
                    selected = "lm.LoaiMon";
                    break;
                case "Tổng số buổi học":
                    selected = "TongSoBuoiHoc";
                    break;
            }

            if (!String.IsNullOrEmpty(value))
            {
                string query = $@"SELECT MaMon, TenMon, SoTinChi, lm.LoaiMon, TongSoBuoiHoc FROM MonHoc mh
                LEFT JOIN LoaiMon lm on mh.IDLoaiMon = lm.IDLoaiMon
                where mh.TrangThai = 'Initialize' AND lm.TrangThai = 'Initialize' AND  {selected} LIKE @value";

                SqlParameter[] parameters = {
                    new SqlParameter("@value", $"%{value}%") 
                };

                LoadDatabaseWithParams(query, parameters);
            }
            else
            {
                MessageBox.Show("Vui lòng nhập giá trị!!!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }
        void LoadDatabaseWithParams(string sql, SqlParameter[] parameters)
        {
            try
            {
                this.dgv.DataSource = DataBase.GetData(sql, parameters);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }
    }
}
