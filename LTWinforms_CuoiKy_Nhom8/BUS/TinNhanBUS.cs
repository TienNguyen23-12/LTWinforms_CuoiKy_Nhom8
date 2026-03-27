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

        public object LayDanhSachLienHe()
        {
            return db.TaiKhoans.Where(x => x.Role == 3 || x.Role == 4)
                               .Select(x => new { 
                                   x.Id, 
                                   TenHienThi = x.Username 
                               }).ToList();
        }

        public object LayLichSuChat(int idKhachHang)
        {
            var query = db.TinNhans.Where(x => x.IdSender == idKhachHang || x.IdReceiver == idKhachHang)
                                   .OrderBy(x => x.ThoiGian)
                                   .ToList();
            return query;
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
