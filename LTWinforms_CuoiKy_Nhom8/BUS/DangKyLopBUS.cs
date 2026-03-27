using LTWinforms_CuoiKy_Nhom8.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTWinforms_CuoiKy_Nhom8.BUS
{
    public class DangKyLopBUS
    {
        QLTTDataContext db = new QLTTDataContext();

        public string LayMaHoiVien(int idTaiKhoan)
        {
            var hv = db.HoiViens.SingleOrDefault(x => x.IdTaiKhoan == idTaiKhoan);
            return hv != null ? hv.MaHoiVien : "";
        }

        public object LayCacLopChuaDangKy(string maHoiVien, string tuKhoa = "")
        {
            var cacLopDaHoc = db.DangKyLops.Where(d => d.MaHoiVien == maHoiVien).Select(d => d.MaLop).ToList();

            var query = db.LopHocs.Where(l => l.IsActive == true && !cacLopDaHoc.Contains(l.MaLop));

            if (!string.IsNullOrEmpty(tuKhoa))
            {
                query = query.Where(x => x.TenLop.Contains(tuKhoa));
            }

            return query.Select(x => new { 
                            x.MaLop, 
                            x.TenLop, 
                            TenHLV = x.HuanLuyenVien.TenHLV, 
                            x.ThoiGian, 
                            x.PhongTap,
                            GiaTien = x.GiaTien 
                        }).ToList();
        }

        public object LayCacLopDaDangKy(string maHoiVien)
        {
            var query = from dk in db.DangKyLops
                        join lop in db.LopHocs on dk.MaLop equals lop.MaLop
                        where dk.MaHoiVien == maHoiVien
                        select new
                        {
                            Id = dk.Id, 
                            MaLop = lop.MaLop,
                            TenLop = lop.TenLop,
                            TenHLV = lop.HuanLuyenVien != null ? lop.HuanLuyenVien.TenHLV : "Đang chờ phân công",
                            ThoiGian = lop.ThoiGian,
                            NgayDangKy = dk.NgayDangKy,
                            TrangThai = dk.TrangThaiThanhToan
                        };
            return query.ToList();
        }

        public string DangKy(string maHoiVien, string maLop)
        {
            try
            {
                DangKyLop dk = new DangKyLop();
                dk.MaHoiVien = maHoiVien;
                dk.MaLop = maLop;
                dk.NgayDangKy = DateTime.Now;
                dk.TrangThaiThanhToan = "Chờ thanh toán";

                db.DangKyLops.InsertOnSubmit(dk);
                db.SubmitChanges();
                return "";
            }
            catch (Exception ex)
            {
                return "Lỗi đăng ký: " + ex.Message;
            }
        }

        public string HuyDangKy(int idDangKy)
        {
            try
            {
                var dk = db.DangKyLops.SingleOrDefault(x => x.Id == idDangKy);
                if (dk != null)
                {
                    db.DangKyLops.DeleteOnSubmit(dk);
                    db.SubmitChanges();
                }
                return "";
            }
            catch (Exception ex)
            {
                return "Lỗi hủy lớp: " + ex.Message;
            }
        }

        public object LayTatCaDangKy_ChoAdmin()
        {
            var query = from dk in db.DangKyLops
                        join lop in db.LopHocs on dk.MaLop equals lop.MaLop
                        join hv in db.HoiViens on dk.MaHoiVien equals hv.MaHoiVien
                        select new
                        {
                            Id = dk.Id,
                            MaHoiVien = hv.MaHoiVien,
                            TenHoiVien = hv.HoTen,
                            TenLop = lop.TenLop,
                            GiaTien = lop.GiaTien,
                            NgayDangKy = dk.NgayDangKy,
                            TrangThai = dk.TrangThaiThanhToan
                        };
            return query.OrderByDescending(x => x.TrangThai == "Chờ thanh toán").ToList();
        }

        public string XacNhanThuTien(int idDangKy, int idNhanVienThuTien)
        {
            try
            {
                var dk = db.DangKyLops.SingleOrDefault(x => x.Id == idDangKy);
                if (dk == null)
                {
                    return "Không tìm thấy giao dịch này!";
                }

                dk.TrangThaiThanhToan = "Đã thanh toán";

                var lop = db.LopHocs.SingleOrDefault(x => x.MaLop == dk.MaLop);

                HoaDon hdMoi = new HoaDon();
                hdMoi.MaHoiVien = dk.MaHoiVien;
                hdMoi.IdNhanVien = idNhanVienThuTien;
                hdMoi.SoTien = lop != null ? (lop.GiaTien ?? 0) : 0;
                hdMoi.NgayThanhToan = DateTime.Now;
                hdMoi.MaGoi = null;
                hdMoi.GhiChu = "Thanh toán lớp học: " + dk.MaLop;

                db.HoaDons.InsertOnSubmit(hdMoi); 

                db.SubmitChanges();
                return "";
            }
            catch (Exception ex)
            {
                return "Lỗi xác nhận: " + ex.Message;
            }
        }
    }
}
