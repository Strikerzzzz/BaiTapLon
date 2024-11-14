using BaiTapLon.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaiTapLon.Controller
{
    internal class LoaiDiemController
    {
        // Lấy tất cả LoaiDiem từ cơ sở dữ liệu
        public List<LoaiDiem> GetAll()
        {
            var result = new List<LoaiDiem>();
            string query = "SELECT IDLoaiDiem, TenLoaiDiem, TiLe FROM LoaiDiem WHERE TrangThai = 'Initialize'";

            try
            {
                using (var dt = DataBase.GetData(query))
                {
                    foreach (System.Data.DataRow row in dt.Rows)
                    {
                        result.Add(new LoaiDiem
                        {
                            IDLoaiDiem = Convert.ToInt32(row["IDLoaiDiem"]),
                            TenLoaiDiem = row["TenLoaiDiem"].ToString(),
                            TiLe = Convert.ToDouble(row["TiLe"]),
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return result;
        }

        // Load dữ liệu vào DataGridView
        public void LoadData(DataGridView gridView)
        {
            gridView.DataSource = GetAll();
        }

        // Thêm mới LoaiDiem
        public bool AddLoaiDiem(LoaiDiem loaiDiem, out string message)
        {
            if (loaiDiem == null || string.IsNullOrEmpty(loaiDiem.TenLoaiDiem) || loaiDiem.TiLe <= 0)
            {
                message = "Vui lòng nhập thông tin hợp lệ.";
                return false;
            }

            string query = "INSERT INTO LoaiDiem (TenLoaiDiem, TiLe, TrangThai) VALUES (@TenLoaiDiem, @TiLe, 'Initialize')";
            SqlParameter[] parameters = {
                new SqlParameter("@TenLoaiDiem", loaiDiem.TenLoaiDiem),
                new SqlParameter("@TiLe", loaiDiem.TiLe)
            };

            try
            {
                if (new DataBase().UpdateData(query, parameters))
                {
                    message = "Thêm thành công.";
                    return true;
                }
            }
            catch (Exception ex)
            {
                message = "Lỗi khi thêm loại điểm: " + ex.Message;
            }

            message = "Thêm thất bại.";
            return false;
        }

        // Cập nhật LoaiDiem
        public bool UpdateLoaiDiem(LoaiDiem loaiDiem, out string message)
        {
            if (loaiDiem == null || loaiDiem.IDLoaiDiem <= 0 || string.IsNullOrEmpty(loaiDiem.TenLoaiDiem) || loaiDiem.TiLe <= 0)
            {
                message = "Vui lòng nhập thông tin hợp lệ.";
                return false;
            }

            string query = "UPDATE LoaiDiem SET TenLoaiDiem = @TenLoaiDiem, TiLe = @TiLe WHERE IDLoaiDiem = @IDLoaiDiem";
            SqlParameter[] parameters = {
                new SqlParameter("@TenLoaiDiem", loaiDiem.TenLoaiDiem),
                new SqlParameter("@TiLe", loaiDiem.TiLe),
                new SqlParameter("@IDLoaiDiem", loaiDiem.IDLoaiDiem)
            };

            try
            {
                if (new DataBase().UpdateData(query, parameters))
                {
                    message = "Cập nhật thành công.";
                    return true;
                }
            }
            catch (Exception ex)
            {
                message = "Lỗi khi cập nhật loại điểm: " + ex.Message;
            }

            message = "Cập nhật thất bại.";
            return false;
        }

        // Xóa LoaiDiem (Chuyển trạng thái thành Deleted)
        public bool DeleteLoaiDiem(int id, out string message)
        {
            if (id <= 0)
            {
                message = "ID không hợp lệ.";
                return false;
            }

            string query = "UPDATE LoaiDiem SET TrangThai = 'Deleted' WHERE IDLoaiDiem = @ID";
            SqlParameter[] parameters = {
                new SqlParameter("@ID", id)
            };

            try
            {
                if (new DataBase().UpdateData(query, parameters))
                {
                    message = "Xóa thành công.";
                    return true;
                }
            }
            catch (Exception ex)
            {
                message = "Lỗi khi xóa loại điểm: " + ex.Message;
            }

            message = "Xóa thất bại.";
            return false;
        }
    }
}
