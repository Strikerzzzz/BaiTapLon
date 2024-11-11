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
    public partial class TRLoaiMon : Form
    {
        public TRLoaiMon()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TRLoaiMon_Load(object sender, EventArgs e)
        {
            LoadDatabase();
        }
        void LoadDatabase()
        {
            try
            {
                this.dataGridView1.DataSource = DataBase.GetData("SELECT IDLoaiMon,LoaiMon FROM LoaiMon where TrangThai = 'Deleted'");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            if (string.IsNullOrEmpty(id))
            {
                MessageBox.Show("Vui lòng chọn loại môn.");
                return;
            }
            try
            {
                string query = "UPDATE LoaiMon SET TrangThai = 'Initialize' WHERE IDLoaiMon = @IDLoaiMon";
                SqlParameter[] parameters = {
                    new SqlParameter("@IDLoaiMon", id)
                };
                bool result = new DataBase().UpdateData(query, parameters);
                if (result)
                {
                    MessageBox.Show("Khôi phục thành công!");
                    LoadDatabase();
                }
                else
                {
                    MessageBox.Show("Khôi phục thất bại.");
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi cơ sở dữ liệu: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }
    }
}
