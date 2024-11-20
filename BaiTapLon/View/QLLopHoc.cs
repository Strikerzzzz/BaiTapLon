using BaiTapLon.Controller;
using BaiTapLon.Model;
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
        Dictionary<string, string> dictHK = new Dictionary<string, string> { { "-1", "Chọn tên học kỳ" } };
        Dictionary<string, string> dictMH = new Dictionary<string, string> { { "-1", "Chọn tên môn học" } };

        public QLLopHoc()
        {
            InitializeComponent();
        }

        private void QLLopHoc_Load(object sender, EventArgs e)
        {
            cboKhoaHoc.Items.Add("Chọn khóa học");
            for (int i = 1; i <= 19; i++)
                cboKhoaHoc.Items.Add("Khóa " + i);
            cboKhoaHoc.SelectedIndex = 0;

            LoadDatabase();
            LoadComboBoxData("SELECT IDHocKy, CONCAT(TenHocKy, N' - Năm ', Nam) AS HocKy FROM HocKy WHERE TrangThai = 'Initialize'", dictHK, cboMaHocKy);
            LoadComboBoxData("SELECT MaMon, TenMon FROM MonHoc WHERE TrangThai = 'Initialize'", dictMH, cboMaMon);
        }

        private void LoadDatabase()
        {
            try
            {
                dataGridView1.DataSource = DataBase.GetData("SELECT lh.MaLop, lh.TenLop, lh.KhoaHoc, lh.SoSVMax, mh.TenMon, CONCAT(hk.TenHocKy, N' - Năm ', hk.Nam) AS hocky, lh.NgayKetThuc " +
                                                            "FROM LopHoc lh LEFT JOIN MonHoc mh ON lh.MaMon = mh.MaMon LEFT JOIN HocKy hk ON lh.IDHocKy = hk.IDHocKy WHERE lh.TrangThai = 'Initialize' AND mh.TrangThai = 'Initialize' AND hk.TrangThai = 'Initialize'");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadComboBoxData(string query, Dictionary<string, string> dictionary, ComboBox comboBox)
        {
            DataTable dt = DataBase.GetData(query);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    string key = row[0].ToString();
                    string value = row[1].ToString();
                    if (!dictionary.ContainsKey(key))
                        dictionary.Add(key, value);
                }
                comboBox.DataSource = new BindingSource(dictionary, null);
                comboBox.DisplayMember = "Value";
                comboBox.ValueMember = "Key";
            }
            else
            {
                MessageBox.Show($"Không có dữ liệu cho {comboBox.Name}.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!ValidateFields()) return;

            string query = "INSERT INTO LopHoc (TenLop, KhoaHoc, SoSVMax, MaMon, TrangThai, IDHocKy, NgayKetThuc) VALUES (@TenLop, @KhoaHoc, @SoSVMax, @MaMon, 'Initialize', @IDHocKy, @NgayKetThuc)";
            SqlParameter[] parameters = {
                new SqlParameter("@TenLop", txtTenlop.Text),
                new SqlParameter("@KhoaHoc", cboKhoaHoc.Text),
                new SqlParameter("@SoSVMax", txtSinhvienmax.Text),
                new SqlParameter("@MaMon", cboMaMon.SelectedValue.ToString()),
                new SqlParameter("@IDHocKy", cboMaHocKy.SelectedValue.ToString()),
                new SqlParameter("@NgayKetThuc", dateTimePickerKetThuc.Value)
            };

            ExecuteDatabaseCommand(query, parameters, "Thêm lớp học thành công!", "Thêm lớp học thất bại.");
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMalop.Text))
            {
                MessageBox.Show("Vui lòng chọn mã lớp cần sửa!!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!ValidateFields()) return;

            string query = "UPDATE LopHoc SET TenLop = @TenLop, KhoaHoc = @KhoaHoc, SoSVMax = @SoSVMax, MaMon = @MaMon, IDHocKy = @IDHocKy, NgayKetThuc = @NgayKetThuc WHERE MaLop = @MaLop";
            SqlParameter[] parameters = {
                new SqlParameter("@TenLop", txtTenlop.Text),
                new SqlParameter("@KhoaHoc", cboKhoaHoc.Text),
                new SqlParameter("@SoSVMax", txtSinhvienmax.Text),
                new SqlParameter("@MaMon", cboMaMon.SelectedValue.ToString()),
                new SqlParameter("@IDHocKy", cboMaHocKy.SelectedValue.ToString()),
                new SqlParameter("@MaLop", txtMalop.Text),
                new SqlParameter("@NgayKetThuc", dateTimePickerKetThuc.Value)
            };

            ExecuteDatabaseCommand(query, parameters, "Sửa lớp học thành công!", "Sửa lớp học thất bại.");
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMalop.Text))
            {
                MessageBox.Show("Vui lòng chọn mã lớp cần xoá!!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Hiển thị hộp thoại xác nhận trước khi xóa
            DialogResult confirmResult = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa lớp học này không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            // Nếu người dùng chọn "Yes", thực hiện xóa
            if (confirmResult == DialogResult.Yes)
            {
                string query = "UPDATE LopHoc SET TrangThai = 'Deleted' WHERE MaLop = @MaLop";
                SqlParameter[] parameters = { new SqlParameter("@MaLop", txtMalop.Text) };

                ExecuteDatabaseCommand(query, parameters, "Xóa lớp học thành công!", "Xóa lớp học thất bại.");
            }
            // Nếu người dùng chọn "No", không thực hiện hành động gì
            else
            {
                MessageBox.Show("Hủy bỏ xóa.");
            }
        }


        private void ExecuteDatabaseCommand(string query, SqlParameter[] parameters, string successMessage, string failureMessage)
        {
            try
            {
                bool result = new DataBase().UpdateData(query, parameters);
                if (result)
                {
                    MessageBox.Show(successMessage);
                    LoadDatabase();
                    ClearFields();
                }
                else
                {
                    MessageBox.Show(failureMessage);
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
            if (dataGridView1.CurrentRow != null)
            {
                txtMalop.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                txtTenlop.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                cboKhoaHoc.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                txtSinhvienmax.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                
                cboMaMon.SelectedValue = dictMH.FirstOrDefault(x => x.Value == dataGridView1.CurrentRow.Cells[4].Value.ToString()).Key;
                cboMaHocKy.SelectedValue = dictHK.FirstOrDefault(x => x.Value == dataGridView1.CurrentRow.Cells[5].Value.ToString()).Key;
                if (DateTime.TryParse(dataGridView1.CurrentRow.Cells[6].Value.ToString(), out DateTime ketThucDate))
                {
                    dateTimePickerKetThuc.Value = ketThucDate;
                }
                else
                {
                    MessageBox.Show("Ngày không hợp lệ, vui lòng kiểm tra dữ liệu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
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
            if (cboKhoaHoc.SelectedIndex == 0)
            {
                MessageBox.Show("Vui lòng chọn Khóa học.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboKhoaHoc.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtSinhvienmax.Text))
            {
                MessageBox.Show("Vui lòng nhập Số sinh viên tối đa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSinhvienmax.Focus();
                return false;
            }
            if (cboMaMon.SelectedIndex == 0)
            {
                MessageBox.Show("Vui lòng chọn Mã môn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMaMon.Focus();
                return false;
            }
            if (cboMaHocKy.SelectedIndex == 0)
            {
                MessageBox.Show("Vui lòng chọn Học kỳ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMaHocKy.Focus();
                return false;
            }
            return true;
        }

        private void btnNhapLai_Click(object sender, EventArgs e)
        {
            txtMalop.Text = string.Empty;
            txtSinhvienmax.Text = string.Empty;
            txtTenlop.Text = string.Empty;
            
            cboKhoaHoc.SelectedIndex = 0;
            cboMaHocKy.SelectedIndex = 0;
            cboMaMon.SelectedIndex = 0;
           
            dateTimePickerKetThuc.Value = DateTime.Now;
        }
    }
}
