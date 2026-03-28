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

        public string DangKyGoiOnline(int idTaiKhoan, string maGoi, decimal giaTien)
        {
            try
            {
                var hv = db.HoiViens.SingleOrDefault(x => x.IdTaiKhoan == idTaiKhoan);
                if (hv == null)
                {
                    return "Lỗi: Không tìm thấy hồ sơ hội viên!";
                }

                HoaDon hd = new HoaDon
                {
                    MaHoiVien = hv.MaHoiVien,
                    MaGoi = maGoi,
                    SoTien = giaTien,
                    NgayThanhToan = DateTime.Now,
                    TrangThai = "Chờ thanh toán", 
                    GhiChu = "Đăng ký Online qua Portal"
                };
                db.HoaDons.InsertOnSubmit(hd);
                db.SubmitChanges();
                return "";
            }
            catch (Exception ex) 
            { 
                return ex.Message; 
            }
        }

        public string DangKyLopOnline(int idTaiKhoan, string maLop)
        {
            try
            {
                var hv = db.HoiViens.SingleOrDefault(x => x.IdTaiKhoan == idTaiKhoan);
                if (db.DangKyLops.Any(d => d.MaHoiVien == hv.MaHoiVien && d.MaLop == maLop))
                {
                    return "Bạn đã đăng ký lớp học này rồi!";

                }

                DangKyLop dk = new DangKyLop
                {
                    MaHoiVien = hv.MaHoiVien,
                    MaLop = maLop,
                    NgayDangKy = DateTime.Now,
                    TrangThaiThanhToan = "Chờ thanh toán"
                };
                db.DangKyLops.InsertOnSubmit(dk);
                db.SubmitChanges();
                return "";
            }
            catch (Exception ex) 
            { 
                return ex.Message; 
            }
        }

        public string GuiPhanHoi(int idTaiKhoan, string noiDung)
        {
            try
            {
                PhanHoi ph = new PhanHoi
                {
                    IdTaiKhoan = idTaiKhoan,
                    NoiDung = noiDung,
                    NgayGui = DateTime.Now,
                    TrangThai = "Chưa đọc"
                };
                db.PhanHois.InsertOnSubmit(ph);
                db.SubmitChanges();
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public int TongHoiVien()
        {
            return db.HoiViens.Count(x => x.IsActive == true);
        }

        public bool KiemTraHanThe(string maHoiVien)
        {
            var hv = db.HoiViens.SingleOrDefault(x => x.MaHoiVien == maHoiVien);
            if (hv != null && hv.NgayHetHan >= DateTime.Now)
            {
                return true;
            }
            return false;
        }
        public object LayDanhSachGoiTap()
        {
            return db.GoiTaps.Where(x => x.IsActive == true).Select(x => new
            {
                x.MaGoi,
                x.TenGoi,
                x.GiaTien,
                ThoiHan = x.ThoiHanThang + " tháng",
                PhongGym = (x.KemPhongTap == true) ? "Có kèm" : "Không kèm"
            }).ToList();
        }

        public object LayLichSuDangKy(int idTaiKhoan)
        {
            var hv = db.HoiViens.SingleOrDefault(x => x.IdTaiKhoan == idTaiKhoan);
            if (hv == null) return null;

            // 1. Lấy lịch sử Gói tập từ bảng HoaDon
            var dsGoi = db.HoaDons.Where(x => x.MaHoiVien == hv.MaHoiVien)
                .Select(x => new {
                    Loai = "Gói Tập",
                    TenDichVu = x.GoiTap != null ? x.GoiTap.TenGoi : x.GhiChu,
                    SoTien = x.SoTien,
                    NgayDky = x.NgayThanhToan,
                    TrangThai = x.TrangThai ?? "Đã thanh toán"
                }).ToList();

            // 2. Lấy lịch sử Lớp học từ bảng DangKyLop
            var dsLop = (from dk in db.DangKyLops
                         join lop in db.LopHocs on dk.MaLop equals lop.MaLop
                         where dk.MaHoiVien == hv.MaHoiVien
                         select new
                         {
                             Loai = "Lớp Học",
                             TenDichVu = lop.TenLop,
                             SoTien = lop.GiaTien ?? 0,
                             NgayDky = dk.NgayDangKy,
                             TrangThai = dk.TrangThaiThanhToan
                         }).ToList();

            // Gộp 2 danh sách lại và sắp xếp theo ngày mới nhất
            return dsGoi.Concat(dsLop).OrderByDescending(x => x.NgayDky).ToList();
        }
    }
}
