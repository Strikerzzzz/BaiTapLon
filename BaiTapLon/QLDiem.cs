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
    public partial class QLDiem : Form
    {
        public QLDiem()
        {
            InitializeComponent();
        }
        Dictionary<string, string> dictMH = new Dictionary<string, string>();
        Dictionary<string, string> dictLD = new Dictionary<string, string>();
        Dictionary<string, string> dictSV = new Dictionary<string, string>();
        private void QLDiem_Load(object sender, EventArgs e)
        {
            LoadDatabase();
            DataTable dt;
            // Môn học
            dt = DataBase.GetData("SELECT MaMon, TenMon FROM MonHoc where TrangThai = 'Initialize'");
            if (dt != null && dt.Rows.Count > 0)
            {
                LoadListDB(dt, dictMH);
            }
            else
            {
                MessageBox.Show("Không có dữ liệu tên môn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            cboTenMon.DataSource = new BindingSource(dictMH, null);
            cboTenMon.DisplayMember = "Value";
            cboTenMon.ValueMember = "Key";
            // Loại điểm
            dt = DataBase.GetData("SELECT IDLoaiDiem, TenLoaiDiem FROM LoaiDiem where TrangThai = 'Initialize'");
            if (dt != null && dt.Rows.Count > 0)
            {
                LoadListDB(dt, dictLD);
            }
            else
            {
                MessageBox.Show("Không có dữ liệu tên môn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            cboTenLoaiDiem.DataSource = new BindingSource(dictLD, null);
            cboTenLoaiDiem.DisplayMember = "Value";
            cboTenLoaiDiem.ValueMember = "Key";
            // Sinh viên
            dt = DataBase.GetData("SELECT MaSV, CONCAT(HoDem, ' ', Ten,'-',MaSV) FROM SinhVien where TrangThai = 'Initialize'");
            if (dt != null && dt.Rows.Count > 0)
            {
                LoadListDB(dt, dictSV);
            }
            else
            {
                MessageBox.Show("Không có dữ liệu tên môn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            cboTenSV.DataSource = new BindingSource(dictSV, null);
            cboTenSV.DisplayMember = "Value";
            cboTenSV.ValueMember = "Key";
        }
        void LoadDatabase()
        {
            try
            {
                this.dataGridView1.DataSource = DataBase.GetData("SELECT IDDiem, CONCAT(sv.HoDem, ' ', sv.Ten,'-',sv.MaSV) AS TenSinhVien, mh.TenMon, ld.TenLoaiDiem, GiaTriDiem, LanThi FROM Diem d " +
                    "LEFT JOIN MonHoc mh on d.MaMon = mh.MaMon " +
                    "LEFT JOIN LoaiDiem ld on d.IDLoaiDiem = ld.IDLoaiDiem " +
                    "LEFT JOIN SinhVien sv on d.MaSV = sv.MaSV " +
                    "where d.TrangThai = 'Initialize'");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!Valid())
            {
                return;
            }
            try
            {
                string query = "INSERT INTO Diem (MaSV, MaMon, IDLoaiDiem, GiaTriDiem, LanThi, TrangThai) VALUES (@MaSV, @MaMon, @IDLoaiDiem, @GiaTriDiem, @LanThi, 'Initialize')";
                SqlParameter[] parameters = {
                    new SqlParameter("@MaSV", cboTenSV.SelectedValue?.ToString()),
                    new SqlParameter("@MaMon", cboTenMon.SelectedValue?.ToString()),
                    new SqlParameter("@IDLoaiDiem", cboTenLoaiDiem.SelectedValue?.ToString()),
                    new SqlParameter("@GiaTriDiem", txtGiaTriDiem.Text),
                    new SqlParameter("@LanThi", txtLanThi.Text),
                };

                bool result = new DataBase().UpdateData(query, parameters);

                if (result)
                {
                    MessageBox.Show("Thêm điểm thành công!");
                    LoadDatabase();
                }
                else
                {
                    MessageBox.Show("Thêm điểm thất bại.");
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
                string query = "UPDATE  Diem SET MaSV = @MaSV, MaMon = @MaMon, " +
                    "IDLoaiDiem = @IDLoaiDiem, GiaTriDiem = @GiaTriDiem, LanThi = @LanThi WHERE IDDiem = @IDDiem";
                SqlParameter[] parameters = {
                    new SqlParameter("@MaSV", cboTenSV.SelectedValue?.ToString()),
                    new SqlParameter("@MaMon", cboTenMon.SelectedValue?.ToString()),
                    new SqlParameter("@IDLoaiDiem", cboTenLoaiDiem.SelectedValue?.ToString()),
                    new SqlParameter("@GiaTriDiem", txtGiaTriDiem.Text),
                    new SqlParameter("@LanThi", txtLanThi.Text),
                    new SqlParameter("@IDDiem", txtID.Text),
                };
                bool result = new DataBase().UpdateData(query, parameters);
                if (result)
                {
                    MessageBox.Show("Sửa điểm thành công!");
                    LoadDatabase();
                }
                else
                {
                    MessageBox.Show("Sửa điểm thất bại.");
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
                string query = "UPDATE Diem SET TrangThai = 'Deleted' WHERE IDDiem = @IDDiem";
                SqlParameter[] parameters = {
                new SqlParameter("@IDDiem", txtID.Text)
            };
                bool result = new DataBase().UpdateData(query, parameters);
                if (result)
                {
                    MessageBox.Show("Xóa thành công!");
                    LoadDatabase();
                }
                else
                {
                    MessageBox.Show("Xóa thất bại.");
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

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        bool Valid()
        {
            if (string.IsNullOrEmpty(txtLanThi.Text) || !txtLanThi.Text.All(char.IsDigit))
            {
                MessageBox.Show("Vui lòng nhập lần thi hợp lệ.");
                return false;
            }
            if (string.IsNullOrEmpty(txtGiaTriDiem.Text) || !double.TryParse(txtGiaTriDiem.Text, out _))
            {
                MessageBox.Show("Vui lòng nhập giá trị điểm hợp lệ.");
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

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    txtID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    string ID, ten;
                    ten = dataGridView1.CurrentRow.Cells[1].Value?.ToString();
                    ID = dictSV.FirstOrDefault(x => x.Value == ten).Key;
                    if (!string.IsNullOrEmpty(ID))
                    {
                        cboTenSV.SelectedValue = ID;
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy mã sinh viên cho tên: " + ID);
                    }
                    ten = dataGridView1.CurrentRow.Cells[2].Value?.ToString();
                    ID = dictMH.FirstOrDefault(x => x.Value == ten).Key;
                    if (!string.IsNullOrEmpty(ID))
                    {
                        cboTenMon.SelectedValue = ID;
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy mã môn cho tên: " + ID);
                    }
                    ten = dataGridView1.CurrentRow.Cells[3].Value?.ToString();
                    ID = dictLD.FirstOrDefault(x => x.Value == ten).Key;
                    if (!string.IsNullOrEmpty(ID))
                    {
                        cboTenLoaiDiem.SelectedValue = ID;
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy mã loại điểms cho tên: " + ID);
                    }
                    txtGiaTriDiem.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                    txtLanThi.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi hiển thị thông tin môn học: " + ex.Message);
            }
        }
    }
}
