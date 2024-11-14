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

namespace BaiTapLon
{
    public partial class NC5 : Form
    {
        public NC5()
        {
            InitializeComponent();
        }
        Dictionary<string, string> dictLM = new Dictionary<string, string>();
        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedLoaiMonId = ((KeyValuePair<string, string>)comboBox.SelectedItem).Key;

            if (selectedLoaiMonId != "-1")
            {
                string query = $@"SELECT MaMon, TenMon, SoTinChi, TongSoBuoiHoc FROM MonHoc mh where TrangThai = 'Initialize' AND IDLoaiMon = @LoaiMonID";

                SqlParameter[] parameters = {
                    new SqlParameter("@LoaiMonID", selectedLoaiMonId)
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
        private void NC5_Load(object sender, EventArgs e)
        {
            DataTable dt = DataBase.GetData("SELECT IDLoaiMon, LoaiMon FROM LoaiMon where TrangThai = 'Initialize'");
            dictLM.Add("-1", "------Chọn loại môn------");
            if (dt != null && dt.Rows.Count > 0)
            {
                LoadListDB(dt, dictLM);
            }
            else
            {
                MessageBox.Show("Không có dữ liệu loại môn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            comboBox.DataSource = new BindingSource(dictLM, null);
            comboBox.DisplayMember = "Value";
            comboBox.ValueMember = "Key";
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
