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
    public partial class QLMonHoc : Form
    {
        Dictionary<string, string> dictLM = new Dictionary<string, string>();
        public QLMonHoc()
        {
            InitializeComponent();
        }
        bool Valid()
        {
            if (string.IsNullOrEmpty(txtTenMon.Text))
            {
                MessageBox.Show("Vui lòng nhập họ tên môn.");
                return false;
            }
            if (string.IsNullOrEmpty(txtSoTinChi.Text)  || !txtSoTinChi.Text.All(char.IsDigit))
            {
                MessageBox.Show("Vui lòng nhập số tín chỉ hợp lệ .");
                return false;
            }
            if (string.IsNullOrEmpty(txtTongSoBuoiHoc.Text) || !txtTongSoBuoiHoc.Text.All(char.IsDigit))
            {
                MessageBox.Show("Vui lòng nhập tổng số buổi học hợp lệ .");
                return false;
            }
            return true;
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        void LoadDatabase()
        {
            try
            {
                this.dataGridViewQLMonHoc.DataSource = DataBase.GetData("SELECT MaMon, TenMon, SoTinChi, lm.LoaiMon , TongSoBuoiHoc FROM MonHoc mh LEFT JOIN LoaiMon lm on lm.IDLoaiMon = mh.IDLoaiMon where mh.TrangThai = 'Initialize'");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        private void QLMonHoc_Load(object sender, EventArgs e)
        {
            LoadDatabase();
            DataTable dt = DataBase.GetData("SELECT IDLoaiMon, LoaiMon FROM LoaiMon where TrangThai = 'Initialize'");
            if (dt != null && dt.Rows.Count > 0)
            {
                LoadListDB(dt, dictLM);
            }
            else
            {
                MessageBox.Show("Không có dữ liệu loại môn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            cbLm.DataSource = new BindingSource(dictLM, null);
            cbLm.DisplayMember = "Value";
            cbLm.ValueMember = "Key";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!Valid())
            {
                return;
            }
            try
            {
                string query = "INSERT INTO MonHoc (TenMon, SoTinChi, IDLoaiMon, TongSoBuoiHoc, TrangThai) VALUES (@TenMon, @SoTinChi, @ID, @TongSoBuoiHoc, 'Initialize')";
                SqlParameter[] parameters = {
                    new SqlParameter("@TenMon", txtTenMon.Text),
                    new SqlParameter("@SoTinChi", txtSoTinChi.Text),
                    new SqlParameter("@TongSoBuoiHoc", txtTongSoBuoiHoc.Text),
                    new SqlParameter("@ID", cbLm.SelectedValue?.ToString())
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
            if (!Valid())
            {
                return;
            }
            try
            {
                string query = "UPDATE  MonHoc SET TenMon = @TenMon, SoTinChi = @SoTinChi, IDLoaiMon = @IDLoaiMon, TongSoBuoiHoc = @TongSoBuoiHoc WHERE MaMon = @MaMon";
                SqlParameter[] parameters = {
                    new SqlParameter("@TenMon", txtTenMon.Text),
                    new SqlParameter("@SoTinChi", txtSoTinChi.Text),
                   new SqlParameter("@IDLoaiMon", cbLm.SelectedValue?.ToString()),
                    new SqlParameter("@TongSoBuoiHoc", txtTongSoBuoiHoc.Text),
                    new SqlParameter("@MaMon", txtMaMon.Text),
                };
                bool result = new DataBase().UpdateData(query, parameters);
                if (result)
                {
                    MessageBox.Show("Sửa môn học thành công!");
                    LoadDatabase();
                }
                else
                {
                    MessageBox.Show("Sửa môn học thất bại.");
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

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "UPDATE MonHoc SET TrangThai = @TrangThai WHERE MaMon = @MaMon";
                SqlParameter[] parameters = {
                new SqlParameter("@MaMon", txtMaMon.Text),
                new SqlParameter("@TrangThai", "Deleted"),
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

        private void dataGridViewQLMonHoc_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewQLMonHoc.CurrentRow != null)
                {
                    txtMaMon.Text = dataGridViewQLMonHoc.CurrentRow.Cells[0].Value.ToString();
                    txtTenMon.Text = dataGridViewQLMonHoc.CurrentRow.Cells[1].Value.ToString();
                    txtSoTinChi.Text = dataGridViewQLMonHoc.CurrentRow.Cells[2].Value.ToString();

                    string LoaiMon = dataGridViewQLMonHoc.CurrentRow.Cells[3].Value?.ToString();
                    string ID = dictLM.FirstOrDefault(x => x.Value == LoaiMon).Key;

                    if (!string.IsNullOrEmpty(ID))
                    {
                        cbLm.SelectedValue = ID;
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy mã mặt hàng cho tên: " + ID);
                    }

                    txtTongSoBuoiHoc.Text = dataGridViewQLMonHoc.CurrentRow.Cells[4].Value.ToString();
                }
                    
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi hiển thị thông tin môn học: " + ex.Message);
            }
        }
    }
}
