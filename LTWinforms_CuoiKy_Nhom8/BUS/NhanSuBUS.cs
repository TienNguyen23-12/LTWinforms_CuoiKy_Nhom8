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
        public object TinhBangLuongChiTiet(DateTime tuNgay, DateTime denNgay)
        {
            var listChamCong = db.ChamCongs.Where(x => x.NgayCham.Value.Date >= tuNgay.Date && x.NgayCham.Value.Date <= denNgay.Date && x.TrangThai == "Có mặt").ToList();
            var listPhat = db.KyLuats.Where(x => x.NgayPhat.Value.Date >= tuNgay.Date && x.NgayPhat.Value.Date <= denNgay.Date).ToList();

            var result = new List<dynamic>();

            var listNV = db.NhanViens.ToList();
            foreach (var nv in listNV)
            {
                int soCong = listChamCong.Count(c => c.IdTaiKhoan == nv.IdTaiKhoan);
                decimal tienPhat = listPhat.Where(p => p.IdTaiKhoan == nv.IdTaiKhoan).Sum(p => (decimal?)p.SoTien) ?? 0;

                decimal luong1Ngay = (nv.Luong ?? 0) / 30; 
                decimal luongThucLanh = (luong1Ngay * soCong) - tienPhat;

                if (luongThucLanh < 0) luongThucLanh = 0;

                result.Add(new
                {
                    MaNhanSu = nv.MaNhanVien,
                    HoTen = nv.HoTen,
                    VaiTro = "Nhân viên",
                    SoNgayLam = soCong,
                    TienPhat = tienPhat,
                    ThucLanh = Math.Round(luongThucLanh, 0)
                });
            }

            var listHLV = db.HuanLuyenViens.ToList();
            foreach (var hlv in listHLV)
            {
                int soBuoi = listChamCong.Count(c => c.IdTaiKhoan == hlv.IdTaiKhoan);
                decimal tienPhat = listPhat.Where(p => p.IdTaiKhoan == hlv.IdTaiKhoan).Sum(p => (decimal?)p.SoTien) ?? 0;

                decimal luongThucLanh = ((hlv.LuongCoBan ?? 0) * soBuoi) - tienPhat;

                if (luongThucLanh < 0) luongThucLanh = 0;

                result.Add(new
                {
                    MaNhanSu = hlv.MaHLV,
                    HoTen = hlv.TenHLV,
                    VaiTro = "Huấn luyện viên",
                    SoNgayLam = soBuoi, 
                    TienPhat = tienPhat,
                    ThucLanh = Math.Round(luongThucLanh, 0)
                });
            }

            return result.OrderBy(x => x.VaiTro).ThenBy(x => x.HoTen).ToList();
        }

        public object TinhLuongCaNhan(int idTaiKhoan, int role, DateTime tuNgay, DateTime denNgay)
        {
            var listChamCong = db.ChamCongs.Where(x => x.NgayCham.Value.Date >= tuNgay.Date && x.NgayCham.Value.Date <= denNgay.Date && x.TrangThai == "Có mặt" && x.IdTaiKhoan == idTaiKhoan).ToList();
            var listPhat = db.KyLuats.Where(x => x.NgayPhat.Value.Date >= tuNgay.Date && x.NgayPhat.Value.Date <= denNgay.Date && x.IdTaiKhoan == idTaiKhoan).ToList();

            var result = new List<dynamic>();
            int soBuoiCoMat = listChamCong.Count;
            decimal tienPhat = listPhat.Sum(p => (decimal?)p.SoTien) ?? 0;

            if (role == 2) 
            {
                var nv = db.NhanViens.FirstOrDefault(x => x.IdTaiKhoan == idTaiKhoan);
                if (nv != null)
                {
                    decimal luong1Ngay = (nv.Luong ?? 0) / 30;
                    decimal luongThucLanh = (luong1Ngay * soBuoiCoMat) - tienPhat;
                    if (luongThucLanh < 0) luongThucLanh = 0;

                    result.Add(new
                    {
                        MaNhanSu = nv.MaNhanVien,
                        HoTen = nv.HoTen,
                        VaiTro = "Nhân viên",
                        SoNgayLam = soBuoiCoMat,
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
                    decimal luongThucLanh = ((hlv.LuongCoBan ?? 0) * soBuoiCoMat) - tienPhat;
                    if (luongThucLanh < 0) luongThucLanh = 0;

                    result.Add(new
                    {
                        MaNhanSu = hlv.MaHLV,
                        HoTen = hlv.TenHLV,
                        VaiTro = "Huấn luyện viên",
                        SoNgayLam = soBuoiCoMat,
                        TienPhat = tienPhat,
                        ThucLanh = Math.Round(luongThucLanh, 0)
                    });
                }
            }

            return result;
        }
    }
}
