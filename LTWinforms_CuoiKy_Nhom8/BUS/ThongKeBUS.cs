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

        private decimal GetCommissionRate() => 0.10m; // 10%

        public object ThongKeTheoGoiTap(DateTime tuNgay, DateTime denNgay)
        {
            DateTime denNgayEnd = denNgay.Date.AddDays(1).AddSeconds(-1);

            var hoaDonTrongKy = db.HoaDons
                                  .Where(x => x.TrangThai == "Đã thanh toán"
                                           && x.NgayThanhToan.HasValue
                                           && x.NgayThanhToan.Value >= tuNgay.Date
                                           && x.NgayThanhToan.Value <= denNgayEnd)
                                  .ToList();

            var queryGoi = hoaDonTrongKy
                        .GroupBy(x => x.GoiTap != null ? x.GoiTap.TenGoi : x.GhiChu)
                        .Select(g => new
                        {
                            TenDichVu = g.Key,
                            SoLuongBan = g.Count(),
                            TongDoanhThu = g.Sum(x => x.SoTien)
                        });

            var dangKyLopTrongKy = db.DangKyLops
                                     .Where(d => d.TrangThaiThanhToan == "Đã thanh toán"
                                              && d.NgayDangKy.HasValue
                                              && d.NgayDangKy.Value >= tuNgay.Date
                                              && d.NgayDangKy.Value <= denNgayEnd)
                                     .ToList();

            var queryLop = (from d in dangKyLopTrongKy
                            join l in db.LopHocs on d.MaLop equals l.MaLop
                            group l by l.TenLop into g
                            select new
                            {
                                TenDichVu = g.Key,
                                SoLuongBan = g.Count(),
                                TongDoanhThu = g.Sum(x => x.GiaTien ?? 0)
                            });

            var combined = queryGoi
                           .Concat(queryLop)
                           .OrderByDescending(x => x.TongDoanhThu)
                           .ToList();

            return combined;
        }

        public decimal TinhTongDoanhThu(DateTime tuNgay, DateTime denNgay)
        {
            DateTime denNgayEnd = denNgay.Date.AddDays(1).AddSeconds(-1);

            decimal thuHoaDon = db.HoaDons
                     .Where(x => x.TrangThai == "Đã thanh toán"
                              && x.NgayThanhToan.HasValue
                              && x.NgayThanhToan.Value >= tuNgay.Date
                              && x.NgayThanhToan.Value <= denNgayEnd)
                     .Sum(x => (decimal?)x.SoTien) ?? 0;

            decimal thuDangKyLop = (from d in db.DangKyLops
                                    join l in db.LopHocs on d.MaLop equals l.MaLop
                                    where d.TrangThaiThanhToan == "Đã thanh toán"
                                          && d.NgayDangKy.HasValue
                                          && d.NgayDangKy.Value >= tuNgay.Date
                                          && d.NgayDangKy.Value <= denNgayEnd
                                    select (decimal?)(l.GiaTien ?? 0)).Sum() ?? 0;

            return thuHoaDon + thuDangKyLop;
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
                decimal luong1Ngay = (nv.Luong ?? 0) / 26m;
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

                decimal luongCoBan = hlv.LuongCoBan ?? 0;
                decimal luongTheoCong = (luongCoBan / 26m) * soBuoiDay;

                decimal commissionRate = GetCommissionRate();
                decimal commissionTotal = 0m;

                var cacLop = db.LopHocs.Where(l => l.MaHLV == hlv.MaHLV && l.IsActive == true).ToList();
                foreach (var lop in cacLop)
                {
                    int soHocVien = db.DangKyLops.Count(dk =>
                        dk.MaLop == lop.MaLop &&
                        dk.NgayDangKy >= tuNgay.Date &&
                        dk.NgayDangKy <= denNgayEnd
                    );
                    decimal giaLop = lop.GiaTien ?? 0m;
                    commissionTotal += giaLop * soHocVien * commissionRate;
                }

                decimal luongThucLanh = luongTheoCong + commissionTotal - tienPhat;
                tongChiLuong += luongThucLanh;
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

        public object TinhLuongCaNhan(int idTaiKhoan, int role, DateTime tuNgay, DateTime denNgay)
        {
            DateTime start = tuNgay.Date;
            DateTime end = denNgay.Date.AddDays(1).AddSeconds(-1);

            var listChamCong = db.ChamCongs
                .Where(x => x.NgayCham.HasValue && x.NgayCham >= start && x.NgayCham <= end && x.TrangThai == "Có mặt" && x.IdTaiKhoan == idTaiKhoan)
                .ToList();

            var listPhat = db.KyLuats
                .Where(x => x.NgayPhat.HasValue && x.NgayPhat >= start && x.NgayPhat <= end && x.IdTaiKhoan == idTaiKhoan)
                .ToList();

            var result = new List<dynamic>();
            int soBuoiCoMat = listChamCong.Count;
            decimal tienPhat = listPhat.Sum(p => (decimal?)p.SoTien) ?? 0;

            if (role == 2)
            {
                var nv = db.NhanViens.FirstOrDefault(x => x.IdTaiKhoan == idTaiKhoan);
                if (nv != null)
                {
                    decimal luong1Ngay = (nv.Luong ?? 0) / 26m;
                    decimal luongThucLanh = (luong1Ngay * soBuoiCoMat) - tienPhat;
                    if (luongThucLanh < 0) luongThucLanh = 0;

                    result.Add(new
                    {
                        MaNhanSu = nv.MaNhanVien,
                        HoTen = nv.HoTen,
                        VaiTro = "Nhân viên",
                        SoNgayLam = soBuoiCoMat,
                        Luong1Ngay = Math.Round(luong1Ngay, 0),
                        TienPhat = tienPhat,
                        ThucLanh = Math.Round(luongThucLanh, 0)
                    });
                }
            }
            else if (role == 4)
            {
                var hlv = db.HuanLuyenViens.FirstOrDefault(x => x.IdTaiKhoan == idTaiKhoan);
                if (hlv != null)
                {
                    decimal luongCoBan = hlv.LuongCoBan ?? 0;
                    decimal luongTheoCong = (luongCoBan / 26m) * soBuoiCoMat;

                    decimal commissionRate = GetCommissionRate();
                    decimal commissionTotal = 0m;

                    var cacLop = db.LopHocs.Where(l => l.MaHLV == hlv.MaHLV && l.IsActive == true).ToList();
                    foreach (var lop in cacLop)
                    {
                        int soHocVien = db.DangKyLops.Count(dk =>
                            dk.MaLop == lop.MaLop &&
                            dk.NgayDangKy >= start &&
                            dk.NgayDangKy <= end
                        );
                        decimal giaLop = lop.GiaTien ?? 0m;
                        commissionTotal += giaLop * soHocVien * commissionRate;
                    }

                    decimal tongLuong = luongTheoCong + commissionTotal - tienPhat;
                    if (tongLuong < 0) tongLuong = 0;

                    result.Add(new
                    {
                        MaNhanSu = hlv.MaHLV,
                        HoTen = hlv.TenHLV,
                        VaiTro = "Huấn luyện viên",
                        SoNgayLam = soBuoiCoMat,
                        TienPhat = tienPhat,
                        Thuong = Math.Round(commissionTotal, 0),
                        ThucLanh = Math.Round(tongLuong, 0)
                    });
                }
            }

            return result;
        }
    }
}
