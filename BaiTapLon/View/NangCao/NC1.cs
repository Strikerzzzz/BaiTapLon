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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BaiTapLon.View.NangCao
{
    public partial class NC1 : Form
    {
        public NC1()
        {
            InitializeComponent();
        }
        Dictionary<string, string> dictLM = new Dictionary<string, string>();

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void NC1_Load(object sender, EventArgs e)
        {
            DataTable dt = DataBase.GetData("SELECT sv.MaSV, CONCAT(sv.MaSV, ' - ',sv.HoDem,' ',sv.Ten) as HoTen FROM SinhVien sv where TrangThai = 'Initialize'");
            dictLM.Add("-1", "------Chọn sinh viên------");
            if (dt != null && dt.Rows.Count > 0)
            {
                LoadListDB(dt, dictLM);
            }
            else
            {
                MessageBox.Show("Không có dữ liệu sinh viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            cboSinhVien.DataSource = new BindingSource(dictLM, null);
            cboSinhVien.DisplayMember = "Value";
            cboSinhVien.ValueMember = "Key";
        }
        void LoadListDB(DataTable dt, Dictionary<string, string> dict)
        {
            try
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
                else
                {
                    MessageBox.Show("Dữ liệu không tồn tại hoặc rỗng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void cboSinhVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedMonId = ((KeyValuePair<string, string>)cboSinhVien.SelectedItem).Key;

            if (selectedMonId != "-1")
            {
                string query = $@"SELECT lhsv.MaLop, lh.TenLop,lh.KhoaHoc, mh.MaMon, mh.TenMon, CONCAT(hk.TenHocKy, ' - ',hk.Nam) as HocKy FROM LopHoc_SinhVien lhsv " +

                "LEFT JOIN LopHoc lh ON lh.MaLop = lhsv.MaLop " +
                "LEFT JOIN MonHoc mh ON mh.MaMon = lh.MaMon " +
                "LEFT JOIN HocKy hk ON lh.IDHocKy = hk.IDHocKy " +
                "where lh.TrangThai = 'Initialize' AND MaSV = @MaSV";

                SqlParameter[] parameters = {
                    new SqlParameter("@MaSV", selectedMonId)
                };

                LoadDatabaseWithParams(query, parameters);
            }
            else
            {
                dataGridView1.DataSource = null;
            }
        }
        void LoadDatabaseWithParams(string sql, SqlParameter[] parameters)
        {
            try
            {
                this.dataGridView1.DataSource = DataBase.GetData(sql, parameters);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

       
    }
}
