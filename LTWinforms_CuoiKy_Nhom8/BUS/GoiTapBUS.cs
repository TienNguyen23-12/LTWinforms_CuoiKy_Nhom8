using LTWinforms_CuoiKy_Nhom8.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTWinforms_CuoiKy_Nhom8.BUS
{
    public class GoiTapBUS
    {
        QLTTDataContext db = new QLTTDataContext();

        public object LayDanhSachGoiTap(string tuKhoa = "")
        {
            var query = db.GoiTaps.Where(x => x.IsActive == true);

            if (!string.IsNullOrEmpty(tuKhoa))
            {
                query = query.Where(x => x.TenGoi.Contains(tuKhoa) || x.MaGoi.Contains(tuKhoa));
            }

            return query.Select(x => new
            {
                x.MaGoi,
                x.TenGoi,
                x.ThoiHanThang,
                x.GiaTien
            }).ToList();
        }

        public string ThemGoiTap(GoiTap gtMoi)
        {
            try
            {
                var check = db.GoiTaps.SingleOrDefault(x => x.MaGoi == gtMoi.MaGoi);
                if (check != null)
                {
                    return "Mã gói tập đã tồn tại!";
                }

                db.GoiTaps.InsertOnSubmit(gtMoi);
                db.SubmitChanges();
                return "";
            }
            catch (Exception ex)
            {
                return "Lỗi thêm: " + ex.Message;
            }
        }

        public string SuaGoiTap(GoiTap gtSua)
        {
            try
            {
                var gt = db.GoiTaps.SingleOrDefault(x => x.MaGoi == gtSua.MaGoi);
                if (gt == null)
                {
                    return "Không tìm thấy gói tập!";
                }

                gt.TenGoi = gtSua.TenGoi;
                gt.ThoiHanThang = gtSua.ThoiHanThang;
                gt.GiaTien = gtSua.GiaTien;

                db.SubmitChanges();
                return "";
            }
            catch (Exception ex)
            {
                return "Lỗi sửa: " + ex.Message;
            }
        }

        public string KhoaGoiTap(string maGoi)
        {
            try
            {
                var gt = db.GoiTaps.SingleOrDefault(x => x.MaGoi == maGoi);
                if (gt == null) return "Không tìm thấy gói tập!";

                gt.IsActive = false;
                db.SubmitChanges();
                return "";
            }
            catch (Exception ex)
            {
                return "Lỗi khóa: " + ex.Message;
            }
        }
    }
}
