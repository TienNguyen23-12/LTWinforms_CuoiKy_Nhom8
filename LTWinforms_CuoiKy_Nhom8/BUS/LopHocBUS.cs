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

        public object LayDanhSachLopHoc(string tuKhoa = "", string maHLV = "", DateTime? tuNgay = null, DateTime? denNgay = null)
        {
            var query = db.LopHocs.AsQueryable();

            if (!string.IsNullOrEmpty(tuKhoa))
            {
                tuKhoa = tuKhoa.ToLower();
                query = query.Where(x => x.TenLop.ToLower().Contains(tuKhoa));
            }

            if (!string.IsNullOrEmpty(maHLV))
            {
                if (maHLV == "NULL")
                {
                    query = query.Where(x => x.MaHLV == null || x.MaHLV == "");
                }
                else
                {
                    query = query.Where(x => x.MaHLV == maHLV);
                }
            }

            if (tuNgay.HasValue && denNgay.HasValue)
            {
                query = query.Where(x => x.NgayBatDau >= tuNgay.Value.Date && x.NgayBatDau <= denNgay.Value.Date);
            }

            var result = query.Select(x => new
            {
                x.MaLop,
                x.TenLop,
                TenHLV = x.HuanLuyenVien != null ? x.HuanLuyenVien.TenHLV : "Chưa có",
                x.ThoiGian,
                PhongTap = x.PhongTap1 != null ? x.PhongTap1.TenPhong : "Chưa xếp",
                x.GiaTien,
                x.SoLuongToiDa,
                x.SoBuoi,
                NgayBatDau = x.NgayBatDau,
                TrangThai = x.TrangThai 
            }).ToList();

            return result;
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
                lop.SoBuoi = lopSua.SoBuoi;
                lop.NgayBatDau = lopSua.NgayBatDau;
                lop.TrangThai = lopSua.TrangThai;

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
