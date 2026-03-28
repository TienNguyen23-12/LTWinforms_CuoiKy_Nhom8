using LTWinforms_CuoiKy_Nhom8.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTWinforms_CuoiKy_Nhom8.BUS
{
    public class TinNhanBUS
    {
        QLTTDataContext db = new QLTTDataContext();

        public object LayDanhSachLienHe(int roleDangLogin)
        {
            var danhSach = new List<dynamic>();

            if (roleDangLogin == 3 || roleDangLogin == 4)
            {
                danhSach.Add(new { Id = 1, TenHienThi = "🏢 Trung tâm hỗ trợ (Admin)" });
            }

            var listTaiKhoan = db.TaiKhoans.ToList();

            foreach (var tk in listTaiKhoan)
            {
                if (tk.Role == 1 || tk.Role == 2)
                {
                    continue;
                }

                if (roleDangLogin == 3 && tk.Role == 3)
                {
                    continue;
                }
                if (roleDangLogin == 4 && tk.Role == 4)
                {
                    continue;
                }

                string tenHienThi = tk.Username;

                if (tk.Role == 3) // HỘI VIÊN
                {
                    var hv = db.HoiViens.FirstOrDefault(x => x.IdTaiKhoan == tk.Id);
                    tenHienThi = hv != null && !string.IsNullOrEmpty(hv.HoTen) ? "👤 Khách: " + hv.HoTen : "👤 Khách: " + tk.Username;
                }
                else if (tk.Role == 4) // HUẤN LUYỆN VIÊN
                {
                    var hlv = db.HuanLuyenViens.FirstOrDefault(x => x.IdTaiKhoan == tk.Id);
                    tenHienThi = hlv != null && !string.IsNullOrEmpty(hlv.TenHLV) ? "💪 HLV: " + hlv.TenHLV : "💪 HLV: " + tk.Username;
                }

                danhSach.Add(new { Id = tk.Id, TenHienThi = tenHienThi });
            }

            return danhSach;
        }

        public object LayLichSuChat(int idNguoiDangLogin, int idKhachHang)
        {
            var query = db.TinNhans.Where(x =>
                            (x.IdSender == idNguoiDangLogin && x.IdReceiver == idKhachHang) ||
                            (x.IdSender == idKhachHang && x.IdReceiver == idNguoiDangLogin)
                        ).OrderBy(x => x.ThoiGian).ToList();

            var result = new List<object>();

            foreach (var t in query)
            {
                string tenHienThi = "";
                bool laToi = false;

                if (t.IdSender == idNguoiDangLogin)
                {
                    tenHienThi = "Tôi";
                    laToi = true;
                }
                else
                {
                    var tkGui = db.TaiKhoans.SingleOrDefault(x => x.Id == t.IdSender);
                    if (tkGui != null)
                    {
                        if (tkGui.Role == 1 || tkGui.Role == 2)
                        {
                            tenHienThi = "Trung tâm"; 
                        }
                        else if (tkGui.Role == 3)
                        {
                            var hv = db.HoiViens.FirstOrDefault(x => x.IdTaiKhoan == tkGui.Id);
                            tenHienThi = hv != null ? hv.HoTen : "Khách hàng"; 
                        }
                        else if (tkGui.Role == 4)
                        {
                            var hlv = db.HuanLuyenViens.FirstOrDefault(x => x.IdTaiKhoan == tkGui.Id);
                            tenHienThi = hlv != null ? hlv.TenHLV : "Huấn luyện viên"; 
                        }
                    }
                }

                result.Add(new
                {
                    Id = t.Id,
                    NoiDung = t.NoiDung,
                    ThoiGian = t.ThoiGian,
                    TenHienThi = tenHienThi,
                    LaToi = laToi 
                });
            }

            return result;
        }

        public string GuiTinNhan(int idNguoiGui, int idNguoiNhan, string noiDung)
        {
            try
            {
                TinNhan tn = new TinNhan();
                tn.IdSender = idNguoiGui;
                tn.IdReceiver = idNguoiNhan; 
                tn.NoiDung = noiDung;
                tn.ThoiGian = DateTime.Now;

                db.TinNhans.InsertOnSubmit(tn);
                db.SubmitChanges();
                return "";
            }
            catch (Exception ex)
            {
                return "Lỗi gửi tin nhắn: " + ex.Message;
            }
        }

        public string SuaTinNhan(int idTinNhan, string noiDungMoi)
        {
            try
            {
                var tn = db.TinNhans.SingleOrDefault(x => x.Id == idTinNhan);
                if (tn == null)
                {
                    return "Không tìm thấy tin nhắn!";
                }

                tn.NoiDung = noiDungMoi;
                db.SubmitChanges();
                return "";
            }
            catch (Exception ex) 
            { 
                return "Lỗi: " + ex.Message; 
            }
        }

        public string XoaTinNhan(int idTinNhan)
        {
            try
            {
                var tn = db.TinNhans.SingleOrDefault(x => x.Id == idTinNhan);
                if (tn == null)
                {
                    return "Không tìm thấy tin nhắn!";
                }

                db.TinNhans.DeleteOnSubmit(tn); 
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
