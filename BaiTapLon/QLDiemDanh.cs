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
    public partial class QLDiemDanh : Form
    {
        Dictionary<string, string> dictSV = new Dictionary<string, string>();
        Dictionary<string, string> dictML = new Dictionary<string, string>();
        public QLDiemDanh()
        {
            InitializeComponent();
        }
        void LoadDatabase()
        {
            try
            {
                this.dataGridView1.DataSource = DataBase.GetData("SELECT dd.IDDiemDanh, CONCAT(sv.HoDem, ' ', sv.Ten,'-',sv.MaSV) AS TenSinhVien, lh.TenLop, dd.NgayDiemDanh, dd.TTDiemDanh FROM DiemDanh dd LEFT JOIN LopHoc lh ON lh.MaLop = dd.MaLop LEFT JOIN SinhVien sv ON dd.MaSV = sv.MaSV WHERE dd.TrangThai = 'Initialize'");
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
        private void QLDiemDanh_Load(object sender, EventArgs e)
        {
            LoadDatabase();
            cboTT.Items.Add("Chọn thông tin điểm danh");
            cboTT.Items.Add("Có");
            cboTT.Items.Add("Không");
            cboTT.SelectedIndex = 0;

            //Lớp
            dictML.Add("-1", "Chọn tên lớp ");
            cboTenLop.Items.Add("Chọn tên lớp");
            DataTable dt = DataBase.GetData("SELECT MaLop, TenLop FROM LopHoc where TrangThai = 'Initialize'");
            if (dt != null && dt.Rows.Count > 0)
            {
                LoadListDB(dt, dictML);
            }
            else
            {
                MessageBox.Show("Không có dữ liệu lớp học.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            cboTenLop.DataSource = new BindingSource(dictML, null);
            cboTenLop.DisplayMember = "Value";
            cboTenLop.ValueMember = "Key";

            // Sinh viên
            dictSV.Add("-1", "Chọn tên sinh viên ");
            cboTenSV.Items.Add("Chọn tên sinh viên");
            DataTable dt1 = DataBase.GetData("SELECT MaSV, CONCAT(HoDem, ' ', Ten,'-',MaSV) FROM SinhVien where TrangThai = 'Initialize'");
            if (dt != null && dt.Rows.Count > 0)
            {
                LoadListDB(dt1, dictSV);
            }
            else
            {
                MessageBox.Show("Không có dữ liệu tên môn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            cboTenSV.DataSource = new BindingSource(dictSV, null);
            cboTenSV.DisplayMember = "Value";
            cboTenSV.ValueMember = "Key";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!ValidateFields())
            {
                return;
            }
            try
            {
                string query = "INSERT INTO DiemDanh (MaSV, MaLop, NgayDiemDanh, TTDiemDanh, TrangThai) VALUES (@MaSV, @MaLop, @NgayDiemDanh, @TTDiemDanh, 'Initialize')";
                SqlParameter[] parameters = {
                    new SqlParameter("@MaSV", cboTenSV.SelectedValue?.ToString()),
                    new SqlParameter("@MaLop", cboTenLop.SelectedValue?.ToString()),
                    new SqlParameter("@NgayDiemDanh", dateTimePickerNgayDD.Value),
                    new SqlParameter("@TTDiemDanh", cboTT.Text),
                };

                bool result = new DataBase().UpdateData(query, parameters);

                if (result)
                {
                    MessageBox.Show("Thêm điểm danh thành công!");
                    LoadDatabase();
                    ClearFields();
                }
                else
                {
                    MessageBox.Show("Thêm điểm danh thất bại.");
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
            if (!ValidateFields())
            {
                return;
            }
            try
            {
                string query = "UPDATE DiemDanh SET MaSV = @MaSV, MaLop = @MaLop, NgayDiemDanh = @NgayDiemDanh, TTDiemDanh = @TTDiemDanh WHERE IDDiemDanh = @IDDiemDanh";
                SqlParameter[] parameters = {
                    new SqlParameter("@MaSV", cboTenSV.SelectedValue?.ToString()),
                    new SqlParameter("@MaLop", cboTenLop.SelectedValue?.ToString()),
                     new SqlParameter("@NgayDiemDanh", dateTimePickerNgayDD.Value),
                    new SqlParameter("@TTDiemDanh", cboTT.Text),
                    new SqlParameter("@IDDiemDanh", txtID.Text),
                };
                bool result = new DataBase().UpdateData(query, parameters);
                if (result)
                {
                    MessageBox.Show("Sửa điểm danh thành công!");
                    LoadDatabase();
                    ClearFields();
                }
                else
                {
                    MessageBox.Show("Sửa điểm danh thất bại.");
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
                string query = "UPDATE DiemDanh SET TrangThai = 'Deleted' WHERE IDDiemDanh = @IDDiemDanh";
                SqlParameter[] parameters = {
                new SqlParameter("@IDDiemDanh", txtID.Text)
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
                    ID = dictML.FirstOrDefault(x => x.Value == ten).Key;
                    if (!string.IsNullOrEmpty(ID))
                    {
                        cboTenLop.SelectedValue = ID;
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy mã lớp cho tên: " + ID);
                    }
                    
                    dateTimePickerNgayDD.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                    cboTT.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi hiển thị thông tin điểm danh: " + ex.Message);
            }
        }
        private void ClearFields()
        {
            txtID.Text = string.Empty;
            cboTenSV.SelectedIndex = 0;
            cboTenLop.SelectedIndex = 0;
            cboTT.SelectedIndex = 0;
            dateTimePickerNgayDD.Value = DateTime.Now;
        }
        private bool ValidateFields()
        {
            if (cboTenLop.SelectedIndex == 0)
            {
                MessageBox.Show("Vui lòng chọn lớp.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboTenLop.Focus();
                return false;
            }
            if (cboTenSV.SelectedIndex == 0)
            {
                MessageBox.Show("Vui lòng chọn sinh viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboTenSV.Focus();
                return false;
            }
            if (dateTimePickerNgayDD.Value > DateTime.Now)
            {
                MessageBox.Show("Ngày điểm danh không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (cboTT.SelectedIndex == 0)
            {
                MessageBox.Show("Vui lòng chọn thông tin điểm danh.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cboTT.Focus();
                return false;
            }
            
            return true;
        }

    }
}
