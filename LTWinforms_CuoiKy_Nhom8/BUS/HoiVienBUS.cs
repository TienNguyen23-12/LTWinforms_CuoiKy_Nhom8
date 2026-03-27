using LTWinforms_CuoiKy_Nhom8.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTWinforms_CuoiKy_Nhom8.BUS
{
    public class HoiVienBUS
    {
        QLTTDataContext db = new QLTTDataContext();

        public object LayDanhSachHoiVien(string tuKhoa = "")
        {
            var query = db.HoiViens.Where(x => x.IsActive == true);

            if (!string.IsNullOrEmpty(tuKhoa))
            {
                query = query.Where(x => x.HoTen.Contains(tuKhoa) || x.SDT.Contains(tuKhoa) || x.MaHoiVien.Contains(tuKhoa));
            }

            return query.Select(x => new
            {
                x.MaHoiVien,
                x.HoTen,
                x.GioiTinh,
                x.NgaySinh,
                x.SDT,
                x.NgayDangKy,
                x.NgayHetHan
            }).ToList();
        }
        public string ThemHoiVien(HoiVien hvMoi)
        {
            try
            {
                var check = db.HoiViens.SingleOrDefault(x => x.MaHoiVien == hvMoi.MaHoiVien);
                if (check != null)
                {
                    return "Mã hội viên này đã tồn tại!";
                }

                db.HoiViens.InsertOnSubmit(hvMoi);
                db.SubmitChanges();
                return "";
            }
            catch (Exception ex)
            {
                return "Lỗi thêm Hội viên: " + ex.Message;
            }
        }

        public string SuaHoiVien(HoiVien hvSua)
        {
            try
            {
                var hv = db.HoiViens.SingleOrDefault(x => x.MaHoiVien == hvSua.MaHoiVien);
                if (hv == null)
                {
                    return "Không tìm thấy hội viên cần sửa!";
                }

                hv.HoTen = hvSua.HoTen;
                hv.GioiTinh = hvSua.GioiTinh;
                hv.NgaySinh = hvSua.NgaySinh;
                hv.SDT = hvSua.SDT;

                db.SubmitChanges();
                return "";
            }
            catch (Exception ex)
            {
                return "Lỗi sửa Hội viên: " + ex.Message;
            }
        }

        public string KhoaHoiVien(string maHoiVien)
        {
            try
            {
                var hv = db.HoiViens.SingleOrDefault(x => x.MaHoiVien == maHoiVien);
                if (hv == null) return "Không tìm thấy hội viên cần xóa!";

                hv.IsActive = false; 
                db.SubmitChanges();
                return "";
            }
            catch (Exception ex)
            {
                return "Lỗi xóa Hội viên: " + ex.Message;
            }
        }
    }
}
