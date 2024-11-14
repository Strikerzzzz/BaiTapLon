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

namespace BaiTapLon.View.NangCao
{
    public partial class NC6 : Form
    {
        public NC6()
        {
            InitializeComponent();
        }
        Dictionary<string, string> dictLM = new Dictionary<string, string>();
        private void NC6_Load(object sender, EventArgs e)
        {
            DataTable dt = DataBase.GetData("SELECT IDHocKy, CONCAT(TenHocKy,' ',Nam) as HocKy FROM HocKy where TrangThai = 'Initialize'");
            dictLM.Add("-1", "------Chọn học kỳ------");
            if (dt != null && dt.Rows.Count > 0)
            {
                LoadListDB(dt, dictLM);
            }
            else
            {
                MessageBox.Show("Không có dữ liệu học kỳ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            comboBox.DataSource = new BindingSource(dictLM, null);
            comboBox.DisplayMember = "Value";
            comboBox.ValueMember = "Key";
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedMonId = ((KeyValuePair<string, string>)comboBox.SelectedItem).Key;

            if (selectedMonId != "-1")
            {
                string query = $@"SELECT MaLop, TenLop, KhoaHoc as HocKy FROM LopHoc lh " +
                "where lh.TrangThai = 'Initialize' AND IDHocKy = @IDHocKy";

                SqlParameter[] parameters = {
                    new SqlParameter("@IDHocKy", selectedMonId)
                };

                LoadDatabaseWithParams(query, parameters);
            }
            else
            {
                dgv.DataSource = null;
            }
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
