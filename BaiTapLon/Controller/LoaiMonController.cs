using BaiTapLon.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaiTapLon.Controller
{
    internal class LoaiMonController
    {
        public void LoadData(DataGridView gridView)
        {
            try
            {
                DataTable data = DataBase.GetData("SELECT IDLoaiMon, LoaiMon FROM LoaiMon WHERE TrangThai = 'Initialize'");
                gridView.DataSource = data;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public bool AddLoaiMon(LoaiMon loaiMon, out string message)
        {
            if (string.IsNullOrEmpty(loaiMon.LoaiMonName))
            {
                message = "Vui lòng nhập tên loại môn.";
                return false;
            }

            try
            {
                string query = "INSERT INTO LoaiMon (LoaiMon, TrangThai) VALUES (@LoaiMon, 'Initialize')";
                SqlParameter[] parameters = {
                    new SqlParameter("@LoaiMon", loaiMon.LoaiMonName)
                };
                bool result = new DataBase().UpdateData(query, parameters);

                message = result ? "Thêm loại môn thành công!" : "Thêm loại môn thất bại.";
                return result;
            }
            catch (Exception ex)
            {
                message = "Lỗi cơ sở dữ liệu: " + ex.Message;
                return false;
            }
        }

        public bool UpdateLoaiMon(LoaiMon loaiMon, out string message)
        {
            if (string.IsNullOrEmpty(loaiMon.LoaiMonName))
            {
                message = "Vui lòng nhập tên loại môn.";
                return false;
            }

            try
            {
                string query = "UPDATE LoaiMon SET LoaiMon = @LoaiMon WHERE IDLoaiMon = @IDLoaiMon";
                SqlParameter[] parameters = {
                    new SqlParameter("@LoaiMon", loaiMon.LoaiMonName),
                    new SqlParameter("@IDLoaiMon", loaiMon.IDLoaiMon)
                };
                bool result = new  DataBase().UpdateData(query, parameters);

                message = result ? "Cập nhật loại môn thành công!" : "Cập nhật loại môn thất bại.";
                return result;
            }
            catch (Exception ex)
            {
                message = "Lỗi cơ sở dữ liệu: " + ex.Message;
                return false;
            }
        }

        public bool DeleteLoaiMon(int id, out string message)
        {
            try
            {
                string query = "UPDATE LoaiMon SET TrangThai = 'Deleted' WHERE IDLoaiMon = @IDLoaiMon";
                SqlParameter[] parameters = { new SqlParameter("@IDLoaiMon", id) };
                bool result = new  DataBase().UpdateData(query, parameters);

                message = result ? "Xóa loại môn thành công!" : "Xóa loại môn thất bại.";
                return result;
            }
            catch (Exception ex)
            {
                message = "Lỗi cơ sở dữ liệu: " + ex.Message;
                return false;
            }
        }
    }
}
