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
    public partial class QLLopHoc : Form
    {
        Dictionary<string, string> dictHK = new Dictionary<string, string>();
        Dictionary<string, string> dictMH = new Dictionary<string, string>();
        public QLLopHoc()
        {
            InitializeComponent();
        }

        void LoadDatabase()
        {
            try
            {
                this.dataGridView1.DataSource = DataBase.GetData("SELECT lh.MaLop, lh.TenLop, lh.KhoaHoc, lh.SoSVMax, mh.TenMon,concat(hk.TenHocKy, N' - Năm ',hk.Nam) as hocky  FROM LopHoc lh LEFT JOIN MonHoc mh ON lh.MaMon = mh.MaMon LEFT JOIN HocKy hk ON lh.IDHocKy = hk.IDHocKy WHERE lh.TrangThai = 'Initialize'");
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
        private void QLLopHoc_Load(object sender, EventArgs e)
        {
            cboKhoaHoc.Items.Add("Chọn khóa học");
            for (int i = 1; i <= 19; i++)
            {
                cboKhoaHoc.Items.Add("Khóa " + i);
            }
            cboKhoaHoc.SelectedIndex = 0;

            LoadDatabase();

            //Học kỳ 
            dictHK.Add("-1", "Chọn tên học kỳ ");
            cboMaHocKy.Items.Add("Chọn tên học kỳ");
            DataTable dt = DataBase.GetData("SELECT IDHocKy, CONCAT(TenHocKy, N' - Năm ', Nam) AS HocKy FROM HocKy where TrangThai = 'Initialize'");
            if (dt != null && dt.Rows.Count > 0)
            {
                LoadListDB(dt, dictHK);
            }
            else
            {
                MessageBox.Show("Không có dữ liệu loại môn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            cboMaHocKy.DataSource = new BindingSource(dictHK, null);
            cboMaHocKy.DisplayMember = "Value";
            cboMaHocKy.ValueMember = "Key";

            //Môn học
            dictMH.Add("-1", "Chọn tên môn học ");
            cboMaMon.Items.Add("Chọn tên môn học");
            DataTable dtMh = DataBase.GetData("SELECT MaMon, TenMon FROM MonHoc where TrangThai = 'Initialize'");
            if (dtMh != null && dtMh.Rows.Count > 0)
            {
                LoadListDB(dtMh, dictMH);
            }
            else
            {
                MessageBox.Show("Không có dữ liệu loại môn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            cboMaMon.DataSource = new BindingSource(dictMH, null);
            cboMaMon.DisplayMember = "Value";
            cboMaMon.ValueMember = "Key";
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
           
            try
            {
                if (!ValidateFields())
                {
                    return;
                }

                string query = "INSERT INTO LopHoc (TenLop, KhoaHoc, SoSVMax, MaMon, TrangThai, IDHocKy) VALUES (@TenLop, @KhoaHoc, @SoSVMax, @MaMon, 'Initialize', @IDHocKy)";
                SqlParameter[] parameters = {
                    new SqlParameter("@TenLop", txtTenlop.Text),
                    new SqlParameter("@KhoaHoc", cboKhoaHoc.Text),
                    new SqlParameter("@SoSVMax", txtSinhvienmax.Text),
                    new SqlParameter("@MaMon", cboMaMon.SelectedValue?.ToString()),
                    new SqlParameter("@IDHocKy", cboMaHocKy.SelectedValue?.ToString())
                };

                bool result = new DataBase().UpdateData(query, parameters);

                if (result)
                {
                    MessageBox.Show("Thêm lớp học thành công!");
                    LoadDatabase();
                    ClearFields();
                }
                else
                {
                    MessageBox.Show("Thêm lớp học thất bại.");
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
            try
            {
                if (!ValidateFields())
                {
                    return;
                }

                string query = "UPDATE  LopHoc SET TenLop = @TenLop, KhoaHoc = @KhoaHoc, SoSVMax = @SoSVMax, MaMon = @MaMon, IDHocKy = @IDHocKy WHERE MaLop = @MaLop";
                SqlParameter[] parameters = {
                    new SqlParameter("@TenLop", txtTenlop.Text),
                    new SqlParameter("@KhoaHoc", cboKhoaHoc.Text),
                    new SqlParameter("@SoSVMax", txtSinhvienmax.Text),
                    new SqlParameter("@MaMon", cboMaMon.SelectedValue?.ToString()),
                    new SqlParameter("@IDHocKy", cboMaHocKy.SelectedValue?.ToString()),
                    new SqlParameter("@MaLop", txtMalop.Text),
                };
                bool result = new DataBase().UpdateData(query, parameters);
                if (result)
                {
                    MessageBox.Show("Sửa lớp học thành công!");
                    LoadDatabase();
                    ClearFields();
                }
                else
                {
                    MessageBox.Show("Sửa lớp học thất bại.");
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
                    txtMalop.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    txtTenlop.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    cboKhoaHoc.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                    txtSinhvienmax.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();

                    cboMaHocKy.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();

                    string MaMon = dataGridView1.CurrentRow.Cells[4].Value?.ToString();
                    string ID = dictMH.FirstOrDefault(x => x.Value == MaMon).Key;

                    if (!string.IsNullOrEmpty(ID))
                    {
                        cboMaMon.SelectedValue = ID;
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy mã môn cho tên: " + ID);
                    }

                    string MaHocKy = dataGridView1.CurrentRow.Cells[5].Value?.ToString();
                    string IDMaHocKy = dictHK.FirstOrDefault(x => x.Value == MaHocKy).Key;

                    if (!string.IsNullOrEmpty(IDMaHocKy))
                    {
                        cboMaHocKy.SelectedValue = IDMaHocKy;
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy mã học kỳ cho tên: " + ID);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi hiển thị thông tin lớp học: " + ex.Message);
            }
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "UPDATE LopHoc SET TrangThai = @TrangThai WHERE MaLop = @MaLop";
                SqlParameter[] parameters = {
                    new SqlParameter("@MaLop", txtMalop.Text),
                    new SqlParameter("@TrangThai", "Deleted"),
                };
                bool result = new DataBase().UpdateData(query, parameters);
                if (result)
                {
                    MessageBox.Show("Xóa lớp học thành công!");
                    LoadDatabase();
                }
                else
                {
                    MessageBox.Show("Xóa lớp học thất bại.");
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

        private void ClearFields()
        {
            txtMalop.Text = string.Empty;
            txtTenlop.Text = string.Empty;
            txtSinhvienmax.Text = string.Empty;
            cboMaHocKy.SelectedIndex = 0;
            cboKhoaHoc.SelectedIndex = 0;
            cboMaMon.SelectedIndex = 0;
        }
        private bool ValidateFields()
        {
            if (string.IsNullOrWhiteSpace(txtTenlop.Text))
            {
                MessageBox.Show("Vui lòng nhập Tên lớp.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenlop.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtSinhvienmax.Text))
            {
                MessageBox.Show("Vui lòng nhập Số sinh viên tối đa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSinhvienmax.Focus();
                return false;
            }
            if (cboMaHocKy.SelectedIndex == 0)
            {
                MessageBox.Show("Vui lòng chọn Học kỳ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMaHocKy.Focus();
                return false;
            }
            if (cboKhoaHoc.SelectedIndex == 0)
            {
                MessageBox.Show("Vui lòng chọn Khóa học.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboKhoaHoc.Focus();
                return false;
            }
            if (cboMaMon.SelectedIndex == 0)
            {
                MessageBox.Show("Vui lòng chọn Mã môn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMaMon.Focus();
                return false;
            }

            return true;
        }
    }
}
