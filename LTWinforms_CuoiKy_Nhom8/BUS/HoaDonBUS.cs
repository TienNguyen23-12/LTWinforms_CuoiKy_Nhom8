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
            return db.HoaDons.OrderByDescending(x => x.NgayThanhToan)
                             .Select(x => new {
                                 x.MaHoaDon,
                                 TenKhach = x.HoiVien.HoTen,
                                 TenGoi = x.GoiTap.TenGoi,
                                 x.SoTien,
                                 x.NgayThanhToan,
                                 NguoiThu = x.TaiKhoan.Username 
                             }).ToList();
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

            return db.HoaDons.Where(x => x.MaHoiVien == hv.MaHoiVien)
                             .OrderByDescending(x => x.NgayThanhToan) 
                             .Select(x => new {
                                 x.MaHoaDon,
                                 TenGoi = x.GoiTap.TenGoi,
                                 x.SoTien,
                                 x.NgayThanhToan,
                                 x.TrangThai
                             }).ToList();
        }
    }
}
