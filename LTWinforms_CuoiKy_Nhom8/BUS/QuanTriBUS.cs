using LTWinforms_CuoiKy_Nhom8.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTWinforms_CuoiKy_Nhom8.BUS
{
    public class QuanTriBUS
    {
        QLTTDataContext db = new QLTTDataContext();

        public object LayDanhSachTaiKhoan()
        {
            var query = from tk in db.TaiKhoans
                        where tk.Role != 1 
                        join hlv in db.HuanLuyenViens on tk.Id equals hlv.IdTaiKhoan into hlvGroup
                        from h in hlvGroup.DefaultIfEmpty()
                        join hv in db.HoiViens on tk.Id equals hv.IdTaiKhoan into hvGroup
                        from v in hvGroup.DefaultIfEmpty()

                        join nv in db.NhanViens on tk.Id equals nv.IdTaiKhoan into nvGroup
                        from n in nvGroup.DefaultIfEmpty()

                        select new
                        {
                            tk.Id,
                            tk.Username,
                            VaiTro = tk.Role == 2 ? "Nhân viên" : (tk.Role == 4 ? "Huấn Luyện Viên" : "Hội Viên"),

                            TenHienThi = h != null ? h.TenHLV : (n != null ? n.HoTen : (v != null ? v.HoTen : "N/A")),

                            LuongCoBan = h != null ? h.LuongCoBan : (n != null ? n.Luong : 0),

                            TrangThai = tk.IsActive == true ? "Hoạt động" : "Bị khóa"
                        };
            return query.ToList();
        }

        public string CapNhatLuong(int idTaiKhoan, decimal luongMoi)
        {
            try
            {
                var tk = db.TaiKhoans.SingleOrDefault(x => x.Id == idTaiKhoan);
                if (tk == null)
                {
                    return "Không tìm thấy tài khoản!";
                }

                bool updated = false;

                if (tk.Role == 4) // Huấn luyện viên
                {
                    var hlv = db.HuanLuyenViens.SingleOrDefault(x => x.IdTaiKhoan == idTaiKhoan);
                    if (hlv != null)
                    {
                        hlv.LuongCoBan = luongMoi;
                        updated = true;
                    }
                    else
                    {
                        return "Không tìm thấy hồ sơ Huấn luyện viên cho tài khoản này.";
                    }
                }
                else if (tk.Role == 2) // Nhân viên
                {
                    var nv = db.NhanViens.SingleOrDefault(x => x.IdTaiKhoan == idTaiKhoan);
                    if (nv != null)
                    {
                        nv.Luong = luongMoi;
                        updated = true;
                    }
                    else
                    {
                        return "Không tìm thấy hồ sơ Nhân viên cho tài khoản này.";
                    }
                }
                else
                {
                    return "Tài khoản không phải Nhân viên hoặc Huấn luyện viên.";
                }

                if (updated)
                {
                    db.SubmitChanges();
                    return "";
                }

                return "Không có thay đổi nào được thực hiện.";
            }
            catch (Exception ex) 
            { 
                return "Lỗi: " + ex.Message; 
            }
        }

        public string DatLaiMatKhau(int idTaiKhoan)
        {
            try
            {
                var tk = db.TaiKhoans.SingleOrDefault(x => x.Id == idTaiKhoan);
                if (tk == null)
                {
                    return "Không tìm thấy tài khoản!";
                }

                tk.Password = "123456";
                db.SubmitChanges();
                return "";
            }
            catch (Exception ex) 
            { 
                return "Lỗi: " + ex.Message; 
            }
        }

        public string KhoaMoTaiKhoan(int idTaiKhoan)
        {
            try
            {
                var tk = db.TaiKhoans.SingleOrDefault(x => x.Id == idTaiKhoan);
                if (tk == null)
                {
                    return "Không tìm thấy tài khoản!";
                }

                tk.IsActive = !tk.IsActive;
                db.SubmitChanges();
                return "";
            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }
        }

        public string CapNhatThongTin(int idTaiKhoan, string username, string vaiTroStr, string tenHienThi)
        {
            try
            {
                var tk = db.TaiKhoans.SingleOrDefault(x => x.Id == idTaiKhoan);
                if (tk == null)
                {
                    return "Không tìm thấy tài khoản!";
                }

                tk.Username = username;

                int roleMoi = 2; 
                if (vaiTroStr == "Hội Viên")
                {
                    roleMoi = 3;
                }
                else if (vaiTroStr == "Huấn Luyện Viên")
                {
                    roleMoi = 4;
                }

                tk.Role = roleMoi;

                if (roleMoi == 4)
                {
                    var hlv = db.HuanLuyenViens.SingleOrDefault(x => x.IdTaiKhoan == idTaiKhoan);
                    if (hlv != null)
                    {
                        hlv.TenHLV = tenHienThi;
                    }
                    else
                    {
                        HuanLuyenVien hlvMoi = new HuanLuyenVien();
                        hlvMoi.MaHLV = "HLV" + idTaiKhoan.ToString("D4");
                        hlvMoi.IdTaiKhoan = idTaiKhoan;
                        hlvMoi.TenHLV = string.IsNullOrEmpty(tenHienThi) ? username : tenHienThi;
                        hlvMoi.IsActive = true;
                        db.HuanLuyenViens.InsertOnSubmit(hlvMoi);
                    }
                }
                else if (roleMoi == 3)
                {
                    var hv = db.HoiViens.SingleOrDefault(x => x.IdTaiKhoan == idTaiKhoan);
                    if (hv != null)
                    {
                        hv.HoTen = tenHienThi;
                    }
                    else
                    {
                        HoiVien hvMoi = new HoiVien();
                        hvMoi.MaHoiVien = "HV" + idTaiKhoan.ToString("D4");
                        hvMoi.IdTaiKhoan = idTaiKhoan;
                        hvMoi.HoTen = string.IsNullOrEmpty(tenHienThi) ? username : tenHienThi;
                        hvMoi.NgayDangKy = DateTime.Now;
                        hvMoi.IsActive = true;
                        db.HoiViens.InsertOnSubmit(hvMoi);
                    }
                }
                else if (roleMoi == 2)
                {
                    var nv = db.NhanViens.SingleOrDefault(x => x.IdTaiKhoan == idTaiKhoan);
                    if (nv != null)
                    {
                        nv.HoTen = tenHienThi;
                    }
                    else
                    {
                        NhanVien nvMoi = new NhanVien();
                        nvMoi.MaNhanVien = "NV" + idTaiKhoan.ToString("D4");
                        nvMoi.IdTaiKhoan = idTaiKhoan;
                        nvMoi.HoTen = string.IsNullOrEmpty(tenHienThi) ? username : tenHienThi;
                        nvMoi.Luong = 0;
                        nvMoi.IsActive = true;
                        db.NhanViens.InsertOnSubmit(nvMoi);
                    }
                }

                db.SubmitChanges();
                return "";
            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }
        }
    }
}
