using LTWinforms_CuoiKy_Nhom8.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTWinforms_CuoiKy_Nhom8.BUS
{
    public class ThongKeBUS
    {
        QLTTDataContext db = new QLTTDataContext();

        public object ThongKeTheoGoiTap(DateTime tuNgay, DateTime denNgay)
        {
            DateTime denNgayEnd = denNgay.Date.AddDays(1).AddSeconds(-1);

            var hoaDonTrongKy = db.HoaDons
                                  .Where(x => x.TrangThai == "Đã thanh toán"
                                           && x.NgayThanhToan.HasValue
                                           && x.NgayThanhToan.Value >= tuNgay.Date
                                           && x.NgayThanhToan.Value <= denNgayEnd)
                                  .ToList();

            var query = hoaDonTrongKy
                        .GroupBy(x => x.GoiTap != null ? x.GoiTap.TenGoi : x.GhiChu)
                        .Select(g => new
                        {
                            TenDichVu = g.Key,
                            SoLuongBan = g.Count(),
                            TongDoanhThu = g.Sum(x => x.SoTien)
                        })
                        .OrderByDescending(x => x.TongDoanhThu)
                        .ToList();

            return query;
        }

        public decimal TinhTongDoanhThu(DateTime tuNgay, DateTime denNgay)
        {
            DateTime denNgayEnd = denNgay.Date.AddDays(1).AddSeconds(-1);

            return db.HoaDons
                     .Where(x => x.TrangThai == "Đã thanh toán"
                              && x.NgayThanhToan.HasValue
                              && x.NgayThanhToan.Value >= tuNgay.Date
                              && x.NgayThanhToan.Value <= denNgayEnd)
                     .Sum(x => (decimal?)x.SoTien) ?? 0;
        }

        public object ThongKeTaiChinh(DateTime tuNgay, DateTime denNgay)
        {
            DateTime denNgayEnd = denNgay.Date.AddDays(1).AddSeconds(-1);

            decimal tongDoanhThu = TinhTongDoanhThu(tuNgay, denNgay);

            var listChamCong = db.ChamCongs.Where(x => x.NgayCham.HasValue
                                                    && x.NgayCham.Value >= tuNgay.Date
                                                    && x.NgayCham.Value <= denNgayEnd
                                                    && x.TrangThai == "Có mặt").ToList();

            var listPhat = db.KyLuats.Where(x => x.NgayPhat.HasValue
                                              && x.NgayPhat.Value >= tuNgay.Date
                                              && x.NgayPhat.Value <= denNgayEnd).ToList();

            decimal tongChiLuong = 0;

            var listNV = db.NhanViens.ToList();
            foreach (var nv in listNV)
            {
                int soNgayLam = listChamCong.Count(c => c.IdTaiKhoan == nv.IdTaiKhoan);
                decimal luong1Ngay = (nv.Luong ?? 0) / 30; 
                decimal tienPhat = listPhat.Where(p => p.IdTaiKhoan == nv.IdTaiKhoan).Sum(p => (decimal?)p.SoTien) ?? 0;

                decimal luongThucLanh = (luong1Ngay * soNgayLam) - tienPhat;

                if (luongThucLanh > 0)
                {
                    tongChiLuong += luongThucLanh;
                }
            }

            var listHLV = db.HuanLuyenViens.ToList();
            foreach (var hlv in listHLV)
            {
                int soBuoiDay = listChamCong.Count(c => c.IdTaiKhoan == hlv.IdTaiKhoan);
                decimal tienPhat = listPhat.Where(p => p.IdTaiKhoan == hlv.IdTaiKhoan).Sum(p => (decimal?)p.SoTien) ?? 0;

                decimal luongThucLanh = ((hlv.LuongCoBan ?? 0) * soBuoiDay) - tienPhat;

                if (luongThucLanh > 0)
                {
                    tongChiLuong += luongThucLanh;
                }
            }

            decimal loiNhuan = tongDoanhThu - tongChiLuong;

            var result = new List<dynamic>
            {
                new {
                    Doanh_Thu = tongDoanhThu,
                    Tong_Chi_Luong = Math.Round(tongChiLuong, 0),
                    Loi_Nhuan_Thuc = Math.Round(loiNhuan, 0)
                }
            };

            return result;
        }
    }
}
