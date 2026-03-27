    using LTWinforms_CuoiKy_Nhom8.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTWinforms_CuoiKy_Nhom8.BUS
{
    public class LopHocBUS
    {
        QLTTDataContext db = new QLTTDataContext();

        public object LayDanhSachLopHoc(string tuKhoa = "")
        {
            var query = db.LopHocs.Where(x => x.IsActive == true);

            if (!string.IsNullOrEmpty(tuKhoa))
            {
                query = query.Where(x => x.TenLop.Contains(tuKhoa) || x.MaLop.Contains(tuKhoa));
            }

            return query.Select(x => new
            {
                x.MaLop,
                x.TenLop,
                TenHLV = x.HuanLuyenVien.TenHLV,
                x.ThoiGian,
                x.PhongTap,
                x.GiaTien,
                x.SoLuongToiDa
            }).ToList();
        }

        public string ThemLop(LopHoc lopMoi)
        {
            try
            {
                var check = db.LopHocs.SingleOrDefault(x => x.MaLop == lopMoi.MaLop);
                if (check != null)
                {
                    return "Mã lớp học đã tồn tại!";
                }

                db.LopHocs.InsertOnSubmit(lopMoi);
                db.SubmitChanges();
                return "";
            }
            catch (Exception ex)
            {
                return "Lỗi thêm lớp: " + ex.Message;
            }
        }

        public string SuaLop(LopHoc lopSua)
        {
            try
            {
                var lop = db.LopHocs.SingleOrDefault(x => x.MaLop == lopSua.MaLop);
                if (lop == null)
                {
                    return "Không tìm thấy lớp học!";
                }

                lop.TenLop = lopSua.TenLop;
                lop.MaHLV = lopSua.MaHLV;
                lop.ThoiGian = lopSua.ThoiGian;
                lop.PhongTap = lopSua.PhongTap;
                lop.GiaTien = lopSua.GiaTien;
                lop.SoLuongToiDa = lopSua.SoLuongToiDa;

                db.SubmitChanges();
                return "";
            }
            catch (Exception ex)
            {
                return "Lỗi sửa lớp: " + ex.Message;
            }
        }

        public string KhoaLop(string maLop)
        {
            try
            {
                var lop = db.LopHocs.SingleOrDefault(x => x.MaLop == maLop);
                if (lop == null)
                {
                    return "Không tìm thấy lớp học!";
                }

                lop.IsActive = false;
                db.SubmitChanges();
                return "";
            }
            catch (Exception ex)
            {
                return "Lỗi khóa lớp: " + ex.Message;
            }
        }

        public object LayDanhSachLopChuaCoHLV()
        {
            var query = from lop in db.LopHocs
                        where lop.MaHLV == null || lop.MaHLV == ""
                        select new
                        {
                            lop.MaLop,
                            lop.TenLop,
                            lop.ThoiGian,
                            PhongTap = lop.PhongTap1 != null ? lop.PhongTap1.TenPhong : lop.PhongTap
                        };
            return query.ToList();
        }
    }
}
