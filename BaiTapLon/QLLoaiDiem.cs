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
    public partial class QLLoaiDiem : Form
    {
        Dictionary<string, string> dictLM = new Dictionary<string, string>();

        public QLLoaiDiem()
        {
            InitializeComponent();
        }
        bool Valid()
        {
            if (string.IsNullOrEmpty(txtTenLoaiDiem.Text))
            {
                MessageBox.Show("Vui lòng nhập tên loại điểm.");
                return false;
            }
            if (string.IsNullOrEmpty(txtTiLe.Text) || !txtTiLe.Text.All(char.IsDigit))
            {
                MessageBox.Show("Vui lòng nhập tỉ lệs.");
                return false;
            }
            return true;
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
        void LoadDatabase()
        {
            try
            {
                this.dataGridView1.DataSource = DataBase.GetData("SELECT IDLoaiDiem, mh.TenMon, TenLoaiDiem,Concat(TiLe,'%') as Tile FROM LoaiDiem ld LEFT JOIN MonHoc mh on ld.MaMon= mh.MaMon where ld.TrangThai = 'Initialize'");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void QLLoaiDiem_Load(object sender, EventArgs e)
        {
            LoadDatabase();
            DataTable dt = DataBase.GetData("SELECT MaMon, TenMon FROM MonHoc where TrangThai = 'Initialize'");
            if (dt != null && dt.Rows.Count > 0)
            {
                LoadListDB(dt, dictLM);
            }
            else
            {
                MessageBox.Show("Không có dữ liệu loại môn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            cbTenMon.DataSource = new BindingSource(dictLM, null);
            cbTenMon.DisplayMember = "Value";
            cbTenMon.ValueMember = "Key";
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!Valid())
            {
                return;
            }
            try
            {
                string query = "INSERT INTO LoaiDiem (MaMon, TenLoaiDiem, TiLe, TrangThai) VALUES (@MaMon, @TenLoaiDiem, @TiLe, 'Initialize')";
                SqlParameter[] parameters = {
                    new SqlParameter("@MaMon", cbTenMon.SelectedValue?.ToString()),
                    new SqlParameter("@TenLoaiDiem", txtTenLoaiDiem.Text),
                    new SqlParameter("@TiLe", txtTiLe.Text),
                };

                bool result = new DataBase().UpdateData(query, parameters);

                if (result)
                {
                    MessageBox.Show("Thêm môn học thành công!");
                    LoadDatabase();
                }
                else
                {
                    MessageBox.Show("Thêm môn học thất bại.");
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

        private void btnSua_Click(object sender, EventArgs e)
        {

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "UPDATE LoaiDiem SET TrangThai = 'Deleted' WHERE IDLoaiDiem = @ID";
                SqlParameter[] parameters = {
                new SqlParameter("@ID", txtID.Text)
            };
                bool result = new DataBase().UpdateData(query, parameters);
                if (result)
                {
                    MessageBox.Show("Xóa môn học thành công!");
                    LoadDatabase();
                }
                else
                {
                    MessageBox.Show("Xóa môn học thất bại.");
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

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    txtID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    string TenMon = dataGridView1.CurrentRow.Cells[1].Value?.ToString();
                    string ID = dictLM.FirstOrDefault(x => x.Value == TenMon).Key;

                    if (!string.IsNullOrEmpty(ID))
                    {
                        cbTenMon.SelectedValue = ID;
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy mã môn cho tên: " + ID);
                    }
                    txtTenLoaiDiem.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                    txtTiLe.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi hiển thị thông tin môn học: " + ex.Message);
            }
        }
    }
}
