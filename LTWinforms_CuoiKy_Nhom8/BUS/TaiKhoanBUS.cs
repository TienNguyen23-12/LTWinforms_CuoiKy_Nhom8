using LTWinforms_CuoiKy_Nhom8.DAL;
using LTWinforms_CuoiKy_Nhom8.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTWinforms_CuoiKy_Nhom8.BUS
{
    public class TaiKhoanBUS
    {
        QLTTDataContext db = new QLTTDataContext();

        public string KiemTraDangNhap(string username, string password)
        {
            try
            {
                var user = db.TaiKhoans.SingleOrDefault(u => u.Username == username);

                if (user == null)
                {
                    return "Tài khoản không tồn tại";
                }

                if (user.IsActive == false)
                {
                    return "Tài khoản của bạn đã bị khóa. Vui lòng liên hệ Admin!";
                }

                if (!SecurityHelper.VerifyPassword(user.Password, password))
                {
                    user.FailedLogin += 1;

                    if (user.FailedLogin >= 5)
                    {
                        user.IsActive = false;
                        db.SubmitChanges();
                        return "Bạn đã nhập sai quá 5 lần. Tài khoản đã bị khóa tự động!";
                    }

                    db.SubmitChanges();
                    return $"Sai mật khẩu! Bạn còn {5 - user.FailedLogin} lần thử.";
                }

                user.FailedLogin = 0;
                user.LastLogin = DateTime.Now;
                db.SubmitChanges();

                Session.IdTaiKhoan = user.Id;
                Session.Username = user.Username;
                Session.Role = user.Role;

                return "";
            }
            catch (Exception ex)
            {
                return "Lỗi kết nối CSDL: " + ex.Message;
            }
        }

        public string DangKyTaiKhoan(string username, string password)
        {
            try
            {
                var checkUser = db.TaiKhoans.SingleOrDefault(u => u.Username == username);
                if (checkUser != null)
                {
                    return "Tài khoản này đã tồn tại! Vui lòng chọn tên khác.";
                }

                TaiKhoan tkMoi = new TaiKhoan();
                tkMoi.Username = username;
                tkMoi.Password = SecurityHelper.HashPassword(password);
                tkMoi.Role = 3; 
                tkMoi.IsActive = true;
                tkMoi.FailedLogin = 0;

                db.TaiKhoans.InsertOnSubmit(tkMoi);
                db.SubmitChanges(); 

                HoiVien hvMoi = new HoiVien();
                hvMoi.MaHoiVien = "HV" + tkMoi.Id.ToString("D4");
                hvMoi.IdTaiKhoan = tkMoi.Id; 
                hvMoi.HoTen = username; 
                hvMoi.SDT = ""; 
                hvMoi.NgayDangKy = DateTime.Now;
                hvMoi.IsActive = true;

                db.HoiViens.InsertOnSubmit(hvMoi);
                db.SubmitChanges(); 

                return "";
            }
            catch (Exception ex)
            {
                return "Lỗi hệ thống: " + ex.Message;
            }
        }

        public string DoiMatKhau(int idTaiKhoan, string passCu, string passMoi)
        {
            try
            {
                var tk = db.TaiKhoans.SingleOrDefault(x => x.Id == idTaiKhoan);
                if (tk == null)
                {
                    return "Lỗi: Không tìm thấy tài khoản!";
                }

                if (!SecurityHelper.VerifyPassword(tk.Password, passCu))
                {
                    return "Mật khẩu cũ không chính xác!";
                }

                tk.Password = SecurityHelper.HashPassword(passMoi);
                db.SubmitChanges();
                return ""; 
            }
            catch (Exception ex)
            {
                return "Lỗi hệ thống: " + ex.Message;
            }
        }

        public string QuenMatKhau(string username, string sdtXacThuc, out string matKhauMoi)
        {
            matKhauMoi = "";
            try
            {
                var tk = db.TaiKhoans.SingleOrDefault(x => x.Username == username);
                if (tk == null)
                {
                    return "Tài khoản này không tồn tại trong hệ thống!";
                }
                if (tk.IsActive == false)
                {
                    return "Tài khoản này đã bị khóa!";
                }

                bool xacThucThanhCong = false;

                if (tk.Role == 3) 
                {
                    var hv = db.HoiViens.SingleOrDefault(x => x.IdTaiKhoan == tk.Id);
                    if (hv != null && hv.SDT == sdtXacThuc)
                    {
                        xacThucThanhCong = true;
                    }
                }
                else if (tk.Role == 4) 
                {
                    var hlv = db.HuanLuyenViens.SingleOrDefault(x => x.IdTaiKhoan == tk.Id);
                    if (hlv != null && hlv.SDT == sdtXacThuc)
                    {
                        xacThucThanhCong = true;
                    }
                }
                else if (tk.Role == 2)
                {
                    var nv = db.NhanViens.SingleOrDefault(x => x.IdTaiKhoan == tk.Id);
                    if (nv != null && nv.SDT == sdtXacThuc)
                    {
                        xacThucThanhCong = true;
                    }
                }
                else
                {
                    return "Tài khoản Quản trị/Nhân viên vui lòng liên hệ trực tiếp Admin để cấp lại mật khẩu!";
                }

                if (xacThucThanhCong == false)
                {
                    return "Số điện thoại xác thực không trùng khớp với hồ sơ đăng ký!";
                }

                Random rnd = new Random();
                matKhauMoi = rnd.Next(100000, 999999).ToString();

                tk.Password = SecurityHelper.HashPassword(matKhauMoi);
                db.SubmitChanges();

                return ""; 
            }
            catch (Exception ex)
            {
                return "Lỗi hệ thống: " + ex.Message;
            }
        }

        public string CapNhatHoSoCaNhan(int idTaiKhoan, int role, string hoTenMoi, string sdtMoi, DateTime? ngaySinh, string gioiTinh)
        {
            try
            {
                if (role == 3) // Hội viên
                {
                    var hv = db.HoiViens.SingleOrDefault(x => x.IdTaiKhoan == idTaiKhoan);
                    if (hv != null)
                    {
                        hv.HoTen = hoTenMoi; 
                        hv.SDT = sdtMoi;
                        hv.NgaySinh = ngaySinh; 
                        hv.GioiTinh = gioiTinh;
                    }
                }
                else if (role == 4) // HLV
                {
                    var hlv = db.HuanLuyenViens.SingleOrDefault(x => x.IdTaiKhoan == idTaiKhoan);
                    if (hlv != null)
                    {
                        hlv.TenHLV = hoTenMoi; 
                        hlv.SDT = sdtMoi;
                        hlv.NgaySinh = ngaySinh; 
                        hlv.GioiTinh = gioiTinh;
                    }
                }
                else if (role == 2) // NV
                {
                    var nv = db.NhanViens.SingleOrDefault(x => x.IdTaiKhoan == idTaiKhoan);
                    if (nv != null)
                    {
                        nv.HoTen = hoTenMoi;
                        nv.SDT = sdtMoi;
                        nv.NgaySinh = ngaySinh;
                        nv.GioiTinh = gioiTinh;
                    }
                }
                else return "Tài khoản của bạn không hỗ trợ tính năng này!";

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
