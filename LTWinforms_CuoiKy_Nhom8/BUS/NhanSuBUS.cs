using LTWinforms_CuoiKy_Nhom8.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTWinforms_CuoiKy_Nhom8.BUS
{
    public class NhanSuBUS
    {
        QLTTDataContext db = new QLTTDataContext();

        public object LayDanhSachNhanSu(string tuKhoa = "")
        {
            var query = db.TaiKhoans.Where(x => x.Role == 2 || x.Role == 4).ToList();

            var result = query.Select(tk => new {
                IdTaiKhoan = tk.Id,
                Username = tk.Username,
                VaiTro = tk.Role == 2 ? "Nhân viên" : "Huấn luyện viên",
                HoTen = tk.Role == 2
                        ? (db.NhanViens.FirstOrDefault(n => n.IdTaiKhoan == tk.Id)?.HoTen ?? "Chưa cập nhật")
                        : (db.HuanLuyenViens.FirstOrDefault(h => h.IdTaiKhoan == tk.Id)?.TenHLV ?? "Chưa cập nhật")
            }).ToList();

            if (!string.IsNullOrEmpty(tuKhoa))
            {
                tuKhoa = tuKhoa.ToLower();
                result = result.Where(x => x.HoTen.ToLower().Contains(tuKhoa) || x.Username.ToLower().Contains(tuKhoa)).ToList();
            }

            return result.OrderBy(x => x.VaiTro).ThenBy(x => x.HoTen).ToList();
        }

        public string ChamCongHomNay(int idTaiKhoan, string trangThai)
        {
            try
            {
                var daCham = db.ChamCongs.FirstOrDefault(x => x.IdTaiKhoan == idTaiKhoan && x.NgayCham.Value.Date == DateTime.Now.Date);

                if (daCham != null)
                {
                    daCham.TrangThai = trangThai;
                }
                else
                {
                    ChamCong cc = new ChamCong();
                    cc.IdTaiKhoan = idTaiKhoan;
                    cc.NgayCham = DateTime.Now;
                    cc.TrangThai = trangThai;
                    db.ChamCongs.InsertOnSubmit(cc);
                }

                db.SubmitChanges();
                return "";
            }
            catch (Exception ex)
            {
                return "Lỗi chấm công: " + ex.Message;
            }
        }

        public string GhiNhanPhat(int idTaiKhoan, decimal soTien, string lyDo)
        {
            try
            {
                KyLuat kl = new KyLuat();
                kl.IdTaiKhoan = idTaiKhoan;
                kl.SoTien = soTien;
                kl.LyDo = lyDo;
                kl.NgayPhat = DateTime.Now;

                db.KyLuats.InsertOnSubmit(kl);
                db.SubmitChanges();
                return "";
            }
            catch (Exception ex)
            {
                return "Lỗi ghi nhận kỷ luật: " + ex.Message;
            }
        }

        private decimal GetCommissionRate()
        {
            return 0.10m;
        }

        public object TinhBangLuongChiTiet(DateTime tuNgay, DateTime denNgay)
        {
            DateTime start = tuNgay.Date;
            DateTime end = denNgay.Date.AddDays(1).AddSeconds(-1);

            var listChamCong = db.ChamCongs
                .Where(x => x.NgayCham >= start && x.NgayCham <= end && x.TrangThai == "Có mặt")
                .ToList();

            var listPhat = db.KyLuats
                .Where(x => x.NgayPhat >= start && x.NgayPhat <= end)
                .ToList();

            var result = new List<dynamic>();

            var listNV = db.NhanViens.ToList();
            foreach (var nv in listNV)
            {
                int soCong = listChamCong.Count(c => c.IdTaiKhoan == nv.IdTaiKhoan);
                decimal tienPhat = listPhat.Where(p => p.IdTaiKhoan == nv.IdTaiKhoan).Sum(p => (decimal?)p.SoTien) ?? 0;

                decimal luong1Ngay = (nv.Luong ?? 0) / 26m;
                decimal luongThucLanh = (luong1Ngay * soCong) - tienPhat;

                result.Add(new
                {
                    MaNhanSu = nv.MaNhanVien,
                    HoTen = nv.HoTen,
                    VaiTro = "Nhân viên",
                    SoNgayLam = soCong,
                    LuongCoBan = Math.Round(nv.Luong ?? 0, 0), 
                    Luong1Ngay = Math.Round(luong1Ngay, 0),
                    TienPhat = tienPhat,
                    Thuong = 0m,
                    ThucLanh = Math.Round(luongThucLanh, 0)
                });
            }

            var listHLV = db.HuanLuyenViens.ToList();
            foreach (var hlv in listHLV)
            {
                int soBuoi = listChamCong.Count(c => c.IdTaiKhoan == hlv.IdTaiKhoan);
                decimal tienPhat = listPhat.Where(p => p.IdTaiKhoan == hlv.IdTaiKhoan).Sum(p => (decimal?)p.SoTien) ?? 0;

                decimal luongCoBan = hlv.LuongCoBan ?? 0;
                decimal luongTheoCong = (luongCoBan / 26m) * soBuoi;

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

                result.Add(new
                {
                    MaNhanSu = hlv.MaHLV,
                    HoTen = hlv.TenHLV,
                    VaiTro = "Huấn luyện viên",
                    SoNgayLam = soBuoi,
                    LuongCoBan = Math.Round(luongCoBan, 0), // monthly basic salary for HLV
                    Luong1Ngay = Math.Round(luongTheoCong, 0),
                    TienPhat = tienPhat,
                    Thuong = Math.Round(commissionTotal, 0),
                    ThucLanh = Math.Round(tongLuong, 0)
                });
            }

            return result.OrderBy(x => x.VaiTro).ThenBy(x => x.HoTen).ToList();
        }

        public object TinhLuongCaNhan(int idTaiKhoan, int role, DateTime tuNgay, DateTime denNgay)
        {
            DateTime start = tuNgay.Date;
            DateTime end = denNgay.Date.AddDays(1).AddSeconds(-1);

            var listChamCong = db.ChamCongs
                .Where(x => x.NgayCham >= start && x.NgayCham <= end && x.TrangThai == "Có mặt" && x.IdTaiKhoan == idTaiKhoan)
                .ToList();

            var listPhat = db.KyLuats
                .Where(x => x.NgayPhat >= start && x.NgayPhat <= end && x.IdTaiKhoan == idTaiKhoan)
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

                    result.Add(new
                    {
                        MaNhanSu = nv.MaNhanVien,
                        HoTen = nv.HoTen,
                        VaiTro = "Nhân viên",
                        SoNgayLam = soBuoiCoMat,
                        Luong1Ngay = Math.Round(luong1Ngay,0),
                        LuongCoBan = Math.Round(nv.Luong ?? 0, 0),
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

                    result.Add(new
                    {
                        MaNhanSu = hlv.MaHLV,
                        HoTen = hlv.TenHLV,
                        VaiTro = "Huấn luyện viên",
                        SoNgayLam = soBuoiCoMat,
                        Luong1Ngay = Math.Round(luongTheoCong,0),
                        LuongCoBan = Math.Round(luongCoBan, 0),
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
