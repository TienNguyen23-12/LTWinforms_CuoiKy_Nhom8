using LTWinforms_CuoiKy_Nhom8.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTWinforms_CuoiKy_Nhom8.BUS
{
    public class PhongTapBUS
    {
        QLTTDataContext db = new QLTTDataContext();

        public object LayDanhSach(string tuKhoa = "")
        {
            var query = from pt in db.PhongTaps
                        join tk in db.TaiKhoans on pt.IdNguoiPhuTrach equals tk.Id into tkGroup
                        from t in tkGroup.DefaultIfEmpty()
                        where pt.TenPhong.Contains(tuKhoa) || pt.MaPhong.Contains(tuKhoa) 
                        select new
                        {
                            pt.MaPhong,
                            pt.TenPhong,
                            NguoiPhuTrach = t != null ? t.Username : "-- Chưa phân công --",
                            pt.GhiChu,
                            TrangThai = pt.IsActive == true ? "Hoạt động" : "Tạm ngưng",
                            IdNguoiPhuTrach = pt.IdNguoiPhuTrach
                        };
            return query.ToList();
        }

        public string ThemPhong(PhongTap pt)
        {
            try
            {
                var check = db.PhongTaps.SingleOrDefault(x => x.MaPhong == pt.MaPhong);
                if (check != null)
                {
                    return "Mã phòng đã tồn tại!";
                }
                db.PhongTaps.InsertOnSubmit(pt); db.SubmitChanges(); return "";
            }
            catch (Exception ex) 
            { 
                return "Lỗi: " + ex.Message; 
            }
        }

        public string SuaPhong(PhongTap pt)
        {
            try
            {
                var ptCu = db.PhongTaps.SingleOrDefault(x => x.MaPhong == pt.MaPhong);
                if (ptCu == null)
                {
                    return "Không tìm thấy phòng!";
                }
                ptCu.TenPhong = pt.TenPhong;
                ptCu.GhiChu = pt.GhiChu;
                ptCu.IdNguoiPhuTrach = pt.IdNguoiPhuTrach; 
                db.SubmitChanges(); return "";
            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }
        }

        public string KhoaPhong(string maPhong)
        {
            try
            {
                var pt = db.PhongTaps.SingleOrDefault(x => x.MaPhong == maPhong);
                if (pt == null)
                {
                    return "Không tìm thấy phòng này!";
                }

                pt.IsActive = !pt.IsActive; 
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
