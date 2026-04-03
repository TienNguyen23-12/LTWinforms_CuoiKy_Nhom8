using LTWinforms_CuoiKy_Nhom8.DAL;
using System;
using System.Linq;

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
                TaiKhoan tkMoi = new TaiKhoan();
                tkMoi.Username = hvMoi.MaHoiVien; 
                tkMoi.Password = SecurityHelper.HashPassword("123456");       
                tkMoi.Role = 3;
                tkMoi.IsActive = true;
                tkMoi.FailedLogin = 0;

                db.TaiKhoans.InsertOnSubmit(tkMoi);
                db.SubmitChanges(); 

                hvMoi.IdTaiKhoan = tkMoi.Id;
                hvMoi.IsActive = true;
                hvMoi.NgayDangKy = DateTime.Now;

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
                if (hv == null)
                {
                    return "Không tìm thấy hội viên cần xóa!";
                }

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
                if (hv == null)
                {
                    return "Lỗi: Không tìm thấy hồ sơ hội viên!";
                }

                if (db.DangKyLops.Any(d => d.MaHoiVien == hv.MaHoiVien && d.MaLop == maLop))
                {
                    return "Bạn đã đăng ký lớp học này rồi!";
                }

                var lop = db.LopHocs.SingleOrDefault(l => l.MaLop == maLop && l.IsActive == true);
                if (lop == null)
                {
                    return "Lỗi: Lớp học không tồn tại!";
                }

                int soNguoiHienTai = db.DangKyLops.Count(x => x.MaLop == maLop);
                int maxSoLuong = lop.SoLuongToiDa ?? 0;

                if (maxSoLuong > 0 && soNguoiHienTai >= maxSoLuong)
                {
                    return $"Rất tiếc! Lớp {lop.TenLop} đã đầy ({maxSoLuong}/{maxSoLuong}). Vui lòng chọn lớp khác.";
                }

                var dk = new DangKyLop
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
            if (hv == null)
            {
                return null;
            }

            var hoaDons = db.HoaDons
                .Where(x => x.MaHoiVien == hv.MaHoiVien)
                .Select(x => new
                {
                    x.MaHoaDon,
                    x.MaGoi,
                    TenGoi = x.GoiTap != null ? x.GoiTap.TenGoi : null,
                    x.GhiChu,
                    x.SoTien,
                    x.NgayThanhToan,
                    TrangThai = x.TrangThai ?? "Đã thanh toán"
                }).ToList();

            var dsGoi = hoaDons.Select(x =>
            {
                string loai;
                string tenDichVu;
                string maLop = "";

                if (!string.IsNullOrEmpty(x.MaGoi))
                {
                    loai = "Gói Tập";
                    tenDichVu = x.TenGoi ?? x.GhiChu;
                }
                else if (!string.IsNullOrEmpty(x.GhiChu) && x.GhiChu.StartsWith("Thanh toán lớp học: "))
                {
                    maLop = x.GhiChu.Substring("Thanh toán lớp học: ".Length);
                    var lop = db.LopHocs.SingleOrDefault(l => l.MaLop == maLop);
                    loai = "Lớp Học";
                    tenDichVu = lop != null ? lop.TenLop : ("Lớp " + maLop);
                }
                else
                {
                    loai = "Khác";
                    tenDichVu = x.GhiChu;
                }

                return new
                {
                    Loai = loai,
                    TenDichVu = tenDichVu,
                    MaLop = maLop,
                    IdDangKy = x.MaHoaDon,
                    SoTien = x.SoTien,
                    NgayDky = x.NgayThanhToan,
                    TrangThai = x.TrangThai
                };
            }).ToList();

            var dsLop = (from dk in db.DangKyLops
                         join lop in db.LopHocs on dk.MaLop equals lop.MaLop
                         where dk.MaHoiVien == hv.MaHoiVien && (dk.TrangThaiThanhToan ?? "") != "Đã thanh toán"
                         select new
                         {
                             Loai = "Lớp Học",
                             TenDichVu = lop.TenLop,
                             MaLop = dk.MaLop,
                             IdDangKy = dk.Id,
                             SoTien = lop.GiaTien ?? 0,
                             NgayDky = dk.NgayDangKy,
                             TrangThai = dk.TrangThaiThanhToan
                         }).ToList();

            return dsGoi.Concat(dsLop).OrderByDescending(x => x.NgayDky).ToList();
        }

        public object LayTatCaPhanHoi()
        {
            return (from ph in db.PhanHois
                    join tk in db.TaiKhoans on ph.IdTaiKhoan equals tk.Id
                    orderby ph.NgayGui descending
                    select new
                    {
                        Mã_PH = ph.Id,
                        Người_Gửi = tk.Username,
                        Nội_Dung = ph.NoiDung,
                        Ngày_Gửi = ph.NgayGui,
                        Trạng_Thái = ph.TrangThai
                    }).ToList();
        }

        public bool DaXuLyPhanHoi(int idPH)
        {
            var ph = db.PhanHois.SingleOrDefault(x => x.Id == idPH);
            if (ph != null)
            {
                ph.TrangThai = "Đã xử lý";
                db.SubmitChanges();
                return true;
            }
            return false;
        }

        public string HuyDangKy(int idTaiKhoan, string loai, int idDangKy)
        {
            try
            {
                var hv = db.HoiViens.SingleOrDefault(x => x.IdTaiKhoan == idTaiKhoan);
                if (hv == null)
                {
                    return "Lỗi: Không tìm thấy hồ sơ hội viên!";
                }

                if (string.IsNullOrEmpty(loai))
                {
                    return "Loại đăng ký không hợp lệ!";
                }

                if (loai == "Lớp Học")
                {
                    var dk = db.DangKyLops.SingleOrDefault(d => d.Id == idDangKy && d.MaHoiVien == hv.MaHoiVien);
                    if (dk == null)
                    {
                        return "Không tìm thấy đăng ký lớp này!";
                    }

                    if (!string.IsNullOrEmpty(dk.TrangThaiThanhToan) && dk.TrangThaiThanhToan == "Đã thanh toán")
                    {
                        return "Không thể hủy đăng ký đã thanh toán!";
                    }

                    db.DangKyLops.DeleteOnSubmit(dk);
                    db.SubmitChanges();
                    return "";
                }
                else if (loai == "Gói Tập")
                {
                    var hd = db.HoaDons.SingleOrDefault(h => h.MaHoaDon == idDangKy && h.MaHoiVien == hv.MaHoiVien);
                    if (hd == null)
                    {
                        return "Không tìm thấy giao dịch gói này!";
                    }

                    if (!string.IsNullOrEmpty(hd.TrangThai) && hd.TrangThai != "Chờ thanh toán")
                    {
                        return "Không thể hủy giao dịch đã thanh toán hoặc đã duyệt!";
                    }

                    db.HoaDons.DeleteOnSubmit(hd);
                    db.SubmitChanges();
                    return "";
                }
                else
                {
                    return "Loại đăng ký không được hỗ trợ!";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
