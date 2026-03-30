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
            var query = db.LopHocs.Where(x => x.IsActive == true);

            if (!string.IsNullOrWhiteSpace(tuKhoa))
            {
                query = query.Where(x =>
                    x.MaLop.Contains(tuKhoa) ||
                    x.TenLop.Contains(tuKhoa) ||
                    x.ThoiGian.Contains(tuKhoa));
            }

            if (!string.IsNullOrEmpty(maHLV) && maHLV != "ALL")
            {
                if (maHLV == "NONE" || maHLV == "NULL")
                {
                    query = query.Where(x => x.MaHLV == null || x.MaHLV == "");
                }
                else
                {
                    query = query.Where(x => x.MaHLV == maHLV);
                }
            }

            if (tuNgay.HasValue)
            {
                query = query.Where(x => x.NgayBatDau >= tuNgay.Value.Date);
            }

            if (denNgay.HasValue)
            {
                DateTime denNgayEnd = denNgay.Value.Date.AddDays(1).AddSeconds(-1);
                query = query.Where(x => x.NgayBatDau <= denNgayEnd);
            }

            return query.Select(x => new
            {
                x.MaLop,
                x.TenLop,
                x.ThoiGian,
                NgayBatDau = x.NgayBatDau,
                SoLuongToiDa = x.SoLuongToiDa,
                TrangThai = x.TrangThai,
                GiaTien = x.GiaTien,
                SoBuoi = x.SoBuoi,
                TenHLV = x.HuanLuyenVien != null ? x.HuanLuyenVien.TenHLV : "Chưa phân công",
                PhongTap = x.PhongTap1 != null ? x.PhongTap1.TenPhong : "N/A"
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

        public object LayDanhSachHLV()
        {
            var list = db.HuanLuyenViens.Where(x => x.IsActive == true).ToList();

            HuanLuyenVien hlvChuaPhanCong = new HuanLuyenVien();
            hlvChuaPhanCong.MaHLV = "NONE";
            hlvChuaPhanCong.TenHLV = "--- Chưa phân công ---";
            list.Insert(0, hlvChuaPhanCong);

            HuanLuyenVien hlvTatCa = new HuanLuyenVien();
            hlvTatCa.MaHLV = "ALL";
            hlvTatCa.TenHLV = "--- Tất cả HLV ---";
            list.Insert(0, hlvTatCa);

            return list;
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
