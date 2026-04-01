using LTWinforms_CuoiKy_Nhom8.DAL;
using System;
using System.Linq;

namespace LTWinforms_CuoiKy_Nhom8.BUS
{
    public class HoaDonBUS
    {
        QLTTDataContext db = new QLTTDataContext();

        public object LayLichSuGiaoDich()
        {
            var query = from hd in db.HoaDons
                        join hv in db.HoiViens on hd.MaHoiVien equals hv.MaHoiVien
                        join nv in db.TaiKhoans on hd.IdNhanVien equals nv.Id into nvGroup
                        from nv in nvGroup.DefaultIfEmpty()
                        orderby hd.MaHoaDon descending
                        select new
                        {
                            Mã_HĐ = hd.MaHoaDon,
                            Tên_Khách = hv.HoTen,
                            Dịch_Vụ = hd.GoiTap != null ? hd.GoiTap.TenGoi : hd.GhiChu,
                            Thu_Ngân = nv != null ? nv.Username : "--- Chờ duyệt ---",
                            SoTien = hd.SoTien,
                            NgayThanhToan = hd.NgayThanhToan,
                            Trạng_Thái = hd.TrangThai ?? "Đã thanh toán"
                        };

            return query.ToList();
        }

        public string ThanhToanGiaHan(string maHoiVien, string maGoi, int idNhanVien)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(maHoiVien) || string.IsNullOrWhiteSpace(maGoi))
                {
                    return "Dữ liệu không hợp lệ!";
                }

                var hv = db.HoiViens.SingleOrDefault(x => x.MaHoiVien == maHoiVien);
                var goi = db.GoiTaps.SingleOrDefault(x => x.MaGoi == maGoi);

                if (hv == null || goi == null)
                {
                    return "Dữ liệu không hợp lệ!";
                }

                int thoiHan = goi.ThoiHanThang ?? 0;
                DateTime ngayBatDau = (hv.NgayHetHan.HasValue && hv.NgayHetHan.Value > DateTime.Now)
                                      ? hv.NgayHetHan.Value
                                      : DateTime.Now;

                hv.NgayHetHan = ngayBatDau.AddMonths(thoiHan);

                HoaDon hd = new HoaDon
                {
                    MaHoiVien = maHoiVien,
                    MaGoi = maGoi,
                    IdNhanVien = idNhanVien,
                    SoTien = goi.GiaTien,
                    NgayThanhToan = DateTime.Now,
                    TrangThai = "Đã thanh toán",
                    GhiChu = "Đăng ký gói " + goi.TenGoi
                };

                db.HoaDons.InsertOnSubmit(hd);
                db.SubmitChanges();

                return "";
            }
            catch (Exception ex)
            {
                return "Lỗi thanh toán: " + ex.Message;
            }
        }

        public object LayLichSuCuaKhachHang(int idTaiKhoan)
        {
            var hv = db.HoiViens.SingleOrDefault(x => x.IdTaiKhoan == idTaiKhoan);
            if (hv == null)
            {
                return null;
            }

            var listDaThanhToan = db.HoaDons.Where(x => x.MaHoiVien == hv.MaHoiVien)
                             .Select(x => new {
                                 MaHoaDon = x.MaHoaDon.ToString(),
                                 TenGoi = x.GoiTap != null ? x.GoiTap.TenGoi : x.GhiChu,
                                 SoTien = x.SoTien,
                                 NgayThanhToan = x.NgayThanhToan,
                                 TrangThai = x.TrangThai ?? "Đã thanh toán"
                             }).ToList();

            var listDangNo = (from dk in db.DangKyLops
                              join lop in db.LopHocs on dk.MaLop equals lop.MaLop
                              where dk.MaHoiVien == hv.MaHoiVien && dk.TrangThaiThanhToan == "Chờ thanh toán"
                              select new
                              {
                                  MaHoaDon = "---",
                                  TenGoi = "Chưa đóng học phí: " + lop.TenLop,
                                  SoTien = lop.GiaTien ?? 0,
                                  NgayThanhToan = dk.NgayDangKy,
                                  TrangThai = dk.TrangThaiThanhToan
                              }).ToList();

            var lichSuTongHop = listDaThanhToan.Concat(listDangNo)
                                               .OrderByDescending(x => x.NgayThanhToan)
                                               .ToList();

            return lichSuTongHop;
        }

        public string DuyetHoaDonOnline(int maHD, int idNhanVienThuTien)
        {
            try
            {
                var hd = db.HoaDons.SingleOrDefault(x => x.MaHoaDon == maHD);
                if (hd == null)
                {
                    return "Không tìm thấy hóa đơn!";
                }

                if (hd.TrangThai == "Đã thanh toán")
                {
                    return "Hóa đơn này đã được thanh toán trước đó!";
                }

                hd.TrangThai = "Đã thanh toán";
                hd.NgayThanhToan = DateTime.Now;
                hd.IdNhanVien = idNhanVienThuTien;

                if (!string.IsNullOrEmpty(hd.MaGoi))
                {
                    var hv = db.HoiViens.SingleOrDefault(x => x.MaHoiVien == hd.MaHoiVien);
                    var goi = db.GoiTaps.SingleOrDefault(x => x.MaGoi == hd.MaGoi);

                    if (hv != null && goi != null)
                    {
                        int thoiHan = goi.ThoiHanThang ?? 0;
                        DateTime ngayBatDau = (hv.NgayHetHan.HasValue && hv.NgayHetHan.Value > DateTime.Now)
                                              ? hv.NgayHetHan.Value
                                              : DateTime.Now;
                        hv.NgayHetHan = ngayBatDau.AddMonths(thoiHan);
                    }
                }

                db.SubmitChanges();
                return "";
            }
            catch (Exception ex)
            {
                return "Lỗi khi duyệt hóa đơn: " + ex.Message;
            }
        }

        public string BanSanPhamTaiQuay(string maHoiVien, string tenSP, decimal giaTien, int idNhanVien)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(maHoiVien))
                {
                    return "Mã hội viên không hợp lệ!";
                }

                HoaDon hd = new HoaDon
                {
                    MaHoiVien = maHoiVien,
                    MaGoi = null,
                    SoTien = giaTien,
                    NgayThanhToan = DateTime.Now,
                    IdNhanVien = idNhanVien,
                    TrangThai = "Đã thanh toán",
                    GhiChu = "Mua SP: " + (tenSP ?? string.Empty)
                };

                db.HoaDons.InsertOnSubmit(hd);
                db.SubmitChanges();

                return "";
            }
            catch (Exception ex)
            {
                return "Lỗi khi lưu Database: " + ex.Message;
            }
        }
    }
}
