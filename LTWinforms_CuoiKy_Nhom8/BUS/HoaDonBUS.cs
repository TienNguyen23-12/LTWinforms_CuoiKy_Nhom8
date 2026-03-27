using LTWinforms_CuoiKy_Nhom8.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTWinforms_CuoiKy_Nhom8.BUS
{
    public class HoaDonBUS
    {
        QLTTDataContext db = new QLTTDataContext();

        public object LayLichSuGiaoDich()
        {
            var query = from hd in db.HoaDons
                        join hv in db.HoiViens on hd.MaHoiVien equals hv.MaHoiVien
                        join nv in db.TaiKhoans on hd.IdNhanVien equals nv.Id
                        orderby hd.NgayThanhToan descending
                        select new
                        {
                            Mã_HĐ = hd.MaHoaDon,
                            Tên_Khách = hv.HoTen,
                            Dịch_Vụ = hd.GoiTap != null ? hd.GoiTap.TenGoi : hd.GhiChu,
                            Thu_Ngân = nv.Username,
                            SoTien = hd.SoTien,
                            NgayThanhToan = hd.NgayThanhToan
                        };

            return query.ToList();
        }

        public string ThanhToanGiaHan(string maHoiVien, string maGoi, int idNhanVien)
        {
            try
            {
                var hv = db.HoiViens.SingleOrDefault(x => x.MaHoiVien == maHoiVien);
                var goi = db.GoiTaps.SingleOrDefault(x => x.MaGoi == maGoi);

                if (hv == null || goi == null)
                {
                    return "Dữ liệu không hợp lệ!";
                }

                DateTime ngayBatDau = (hv.NgayHetHan.HasValue && hv.NgayHetHan.Value > DateTime.Now)
                                      ? hv.NgayHetHan.Value
                                      : DateTime.Now;

                hv.NgayHetHan = ngayBatDau.AddMonths(goi.ThoiHanThang.Value);

                HoaDon hd = new HoaDon();
                hd.MaHoiVien = maHoiVien;
                hd.MaGoi = maGoi;
                hd.IdNhanVien = idNhanVien;
                hd.SoTien = goi.GiaTien;
                hd.NgayThanhToan = DateTime.Now;
                hd.TrangThai = "Đã thanh toán";
                hd.GhiChu = "Đăng ký gói " + goi.TenGoi;

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
    }
}
