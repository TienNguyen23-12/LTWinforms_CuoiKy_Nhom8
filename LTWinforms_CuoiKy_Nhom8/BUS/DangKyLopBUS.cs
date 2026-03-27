using LTWinforms_CuoiKy_Nhom8.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTWinforms_CuoiKy_Nhom8.BUS
{
    public class DangKyLopBUS
    {
        QLTTDataContext db = new QLTTDataContext();

        public string LayMaHoiVien(int idTaiKhoan)
        {
            var hv = db.HoiViens.SingleOrDefault(x => x.IdTaiKhoan == idTaiKhoan);
            return hv != null ? hv.MaHoiVien : "";
        }

        public object LayCacLopChuaDangKy(string maHoiVien, string tuKhoa = "")
        {
            var cacLopDaHoc = db.DangKyLops.Where(d => d.MaHoiVien == maHoiVien).Select(d => d.MaLop).ToList();

            var query = db.LopHocs.Where(l => l.IsActive == true && !cacLopDaHoc.Contains(l.MaLop));

            if (!string.IsNullOrEmpty(tuKhoa))
            {
                query = query.Where(x => x.TenLop.Contains(tuKhoa));
            }

            return query.Select(x => new { 
                            x.MaLop, 
                            x.TenLop, 
                            TenHLV = x.HuanLuyenVien.TenHLV, 
                            x.ThoiGian, 
                            x.PhongTap 
                        }).ToList();
        }

        public object LayCacLopDaDangKy(string maHoiVien)
        {
            return db.DangKyLops.Where(d => d.MaHoiVien == maHoiVien)
                                .Select(x => new {
                                    x.Id,
                                    x.LopHoc.MaLop,
                                    x.LopHoc.TenLop,
                                    TenHLV = x.LopHoc.HuanLuyenVien.TenHLV,
                                    x.LopHoc.ThoiGian,
                                    x.NgayDangKy
                                }).ToList();
        }

        public string DangKy(string maHoiVien, string maLop)
        {
            try
            {
                DangKyLop dk = new DangKyLop();
                dk.MaHoiVien = maHoiVien;
                dk.MaLop = maLop;
                dk.NgayDangKy = DateTime.Now;

                db.DangKyLops.InsertOnSubmit(dk);
                db.SubmitChanges();
                return "";
            }
            catch (Exception ex)
            {
                return "Lỗi đăng ký: " + ex.Message;
            }
        }

        public string HuyDangKy(int idDangKy)
        {
            try
            {
                var dk = db.DangKyLops.SingleOrDefault(x => x.Id == idDangKy);
                if (dk != null)
                {
                    db.DangKyLops.DeleteOnSubmit(dk);
                    db.SubmitChanges();
                }
                return "";
            }
            catch (Exception ex) 
            { 
                return "Lỗi hủy lớp: " + ex.Message; 
            }
        }
    }
}
