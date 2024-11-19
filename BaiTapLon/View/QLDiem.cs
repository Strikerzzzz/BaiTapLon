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
    public partial class QLDiem : Form
    {
        private DiemController _controller = new DiemController();

        public QLDiem()
        {
            InitializeComponent();
        }

        private void QLDiem_Load(object sender, EventArgs e)
        {
            LoadComboBoxData();
            LoadDatabase();
        }

        private void LoadComboBoxData()
        {
            DataTable dt;

            // Môn học
            dt = DataBase.GetData("SELECT MaMon, TenMon FROM MonHoc WHERE TrangThai = 'Initialize'");
            _controller.LoadListDB(dt, _controller.dictMH);

            // Tạo danh sách có thứ tự, thêm mục mặc định lên đầu
            var listMH = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("0", "Chọn môn học")
            };
            listMH.AddRange(_controller.dictMH);

            cboTenMon.DataSource = new BindingSource(listMH, null);
            cboTenMon.DisplayMember = "Value";
            cboTenMon.ValueMember = "Key";

            // Loại điểm
            dt = DataBase.GetData("SELECT IDLoaiDiem, TenLoaiDiem FROM LoaiDiem WHERE TrangThai = 'Initialize'");
            _controller.LoadListDB(dt, _controller.dictLD);

            var listLD = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("0", "Chọn loại điểm")
            };
            listLD.AddRange(_controller.dictLD);

            cboTenLoaiDiem.DataSource = new BindingSource(listLD, null);
            cboTenLoaiDiem.DisplayMember = "Value";
            cboTenLoaiDiem.ValueMember = "Key";

            // Sinh viên
            dt = DataBase.GetData("SELECT MaSV, CONCAT(HoDem, ' ', Ten, '-', MaSV) AS TenDayDu FROM SinhVien WHERE TrangThai = 'Initialize'");
            _controller.LoadListDB(dt, _controller.dictSV);

            var listSV = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("0", "Chọn sinh viên")
            };
            listSV.AddRange(_controller.dictSV);

            cboTenSV.DataSource = new BindingSource(listSV, null);
            cboTenSV.DisplayMember = "Value";
            cboTenSV.ValueMember = "Key";
        }



        private void LoadDatabase()
        {
            this.dataGridView1.DataSource = _controller.LoadDiemData();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            // Gọi hàm kiểm tra dữ liệu
            if (!ValidateInput())
                return;

            Diem diem = new Diem
            {
                MaSV = cboTenSV.SelectedValue?.ToString(),
                MaMon = cboTenMon.SelectedValue?.ToString(),
                IDLoaiDiem = cboTenLoaiDiem.SelectedValue?.ToString(),
                GiaTriDiem = double.Parse(txtGiaTriDiem.Text),
                LanThi = int.Parse(txtLanThi.Text)
            };

            if (_controller.AddDiem(diem))
            {
                MessageBox.Show("Thêm điểm thành công!");
                LoadDatabase();
                Clear();
            }
            else
            {
                MessageBox.Show("Thêm điểm thất bại.");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtID.Text))
            {
                MessageBox.Show("Vui lòng chọn mã điểm cần sửa!!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!ValidateInput())
                return;

            Diem diem = new Diem
            {
                IDDiem = int.Parse(txtID.Text),
                MaSV = cboTenSV.SelectedValue?.ToString(),
                MaMon = cboTenMon.SelectedValue?.ToString(),
                IDLoaiDiem = cboTenLoaiDiem.SelectedValue?.ToString(),
                GiaTriDiem = double.Parse(txtGiaTriDiem.Text),
                LanThi = int.Parse(txtLanThi.Text)
            };

            if (_controller.UpdateDiem(diem))
            {
                MessageBox.Show("Sửa điểm thành công!");
                LoadDatabase();
                Clear();
            }
            else
            {
                MessageBox.Show("Sửa điểm thất bại.");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtID.Text))
            {
                MessageBox.Show("Vui lòng chọn mã điểm cần xoá!!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Hiển thị hộp thoại xác nhận trước khi xóa
            DialogResult confirmResult = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa điểm này không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            // Nếu người dùng chọn "Yes", thực hiện xóa
            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    int idDiem = int.Parse(txtID.Text);
                    if (_controller.DeleteDiem(idDiem))
                    {
                        MessageBox.Show("Xóa thành công!");
                        LoadDatabase();
                        Clear();
                    }
                    else
                    {
                        MessageBox.Show("Xóa thất bại.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            // Nếu người dùng chọn "No", hủy bỏ hành động xóa
            else
            {
                MessageBox.Show("Hủy bỏ xóa điểm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    // Lấy giá trị từ hàng hiện tại trong DataGridView
                    txtID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();

                    // Hiển thị thông tin Sinh viên
                    string tenSinhVien = dataGridView1.CurrentRow.Cells[1].Value?.ToString();
                    string maSV = _controller.dictSV.FirstOrDefault(x => x.Value == tenSinhVien).Key;
                    if (!string.IsNullOrEmpty(maSV))
                    {
                        cboTenSV.SelectedValue = maSV;
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy mã sinh viên cho tên: " + tenSinhVien);
                    }

                    // Hiển thị thông tin Môn học
                    string tenMon = dataGridView1.CurrentRow.Cells[2].Value?.ToString();
                    string maMon = _controller.dictMH.FirstOrDefault(x => x.Value == tenMon).Key;
                    if (!string.IsNullOrEmpty(maMon))
                    {
                        cboTenMon.SelectedValue = maMon;
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy mã môn học cho tên: " + tenMon);
                    }

                    // Hiển thị thông tin Loại điểm
                    string tenLoaiDiem = dataGridView1.CurrentRow.Cells[3].Value?.ToString();
                    string idLoaiDiem = _controller.dictLD.FirstOrDefault(x => x.Value == tenLoaiDiem).Key;
                    if (!string.IsNullOrEmpty(idLoaiDiem))
                    {
                        cboTenLoaiDiem.SelectedValue = idLoaiDiem;
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy mã loại điểm cho tên: " + tenLoaiDiem);
                    }

                    // Hiển thị Giá trị điểm và Lần thi
                    txtGiaTriDiem.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                    txtLanThi.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi hiển thị thông tin điểm: " + ex.Message);
            }
        }
        public void Clear()
        {
            txtID.Text = "";
            cboTenSV.SelectedIndex = 0;
            cboTenMon.SelectedIndex = 0;
            cboTenLoaiDiem.SelectedIndex = 0;
            txtGiaTriDiem.Text = "";
            txtLanThi.Text = "";
        }
        private void btnNhapLai_Click(object sender, EventArgs e)
        {
            Clear();
        }
        private bool ValidateInput()
        {
            // Kiểm tra ComboBox Sinh viên
            if (cboTenSV.SelectedValue == null || cboTenSV.SelectedValue.ToString() == "0")
            {
                MessageBox.Show("Vui lòng chọn sinh viên.");
                return false;
            }

            // Kiểm tra ComboBox Môn học
            if (cboTenMon.SelectedValue == null || cboTenMon.SelectedValue.ToString() == "0")
            {
                MessageBox.Show("Vui lòng chọn môn học.");
                return false;
            }

            // Kiểm tra ComboBox Loại điểm
            if (cboTenLoaiDiem.SelectedValue == null || cboTenLoaiDiem.SelectedValue.ToString() == "0")
            {
                MessageBox.Show("Vui lòng chọn loại điểm.");
                return false;
            }

            // Kiểm tra giá trị điểm (double)
            if (string.IsNullOrWhiteSpace(txtGiaTriDiem.Text) || !double.TryParse(txtGiaTriDiem.Text, out double giaTriDiem) || giaTriDiem < 0 || giaTriDiem > 10)
            {
                MessageBox.Show("Giá trị điểm không hợp lệ. Vui lòng nhập số từ 0 đến 10.");
                return false;
            }

            // Kiểm tra lần thi (int)
            if (string.IsNullOrWhiteSpace(txtLanThi.Text) || !int.TryParse(txtLanThi.Text, out int lanThi) || lanThi <= 0)
            {
                MessageBox.Show("Lần thi không hợp lệ. Vui lòng nhập số nguyên lớn hơn 0.");
                return false;
            }

            return true;
        }

    }
}
