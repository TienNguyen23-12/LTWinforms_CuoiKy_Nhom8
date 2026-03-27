using LTWinforms_CuoiKy_Nhom8.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTWinforms_CuoiKy_Nhom8.BUS
{
    public class LichHocBUS
    {
        QLTTDataContext db = new QLTTDataContext();

        public object LayLichTheoQuyen(int role, int idTaiKhoan)
        {
            if (role == 1 || role == 2)
            {
                return db.LopHocs.Where(x => (bool)x.IsActive)
                                 .Select(x => new {
                                     x.MaLop,
                                     x.TenLop,
                                     TenHLV = x.HuanLuyenVien.TenHLV,
                                     x.ThoiGian,
                                     x.PhongTap
                                 }).ToList();
            }
            else if (role == 4)
            {
                var hlv = db.HuanLuyenViens.SingleOrDefault(x => x.IdTaiKhoan == idTaiKhoan);
                if (hlv != null)
                {
                    return db.LopHocs.Where(x => x.IsActive == true && x.MaHLV == hlv.MaHLV)
                                     .Select(x => new {
                                         x.MaLop,
                                         x.TenLop,
                                         TenHLV = "Tôi (" + hlv.TenHLV + ")",
                                         x.ThoiGian,
                                         x.PhongTap
                                     }).ToList();
                }
            }
            else if (role == 3)
            {
                var hv = db.HoiViens.SingleOrDefault(x => x.IdTaiKhoan == idTaiKhoan);
                if (hv != null)
                {
                    return db.DangKyLops.Where(x => x.MaHoiVien == hv.MaHoiVien && x.LopHoc.IsActive == true)
                                        .Select(x => new {
                                            x.LopHoc.MaLop,
                                            x.LopHoc.TenLop,
                                            TenHLV = x.LopHoc.HuanLuyenVien.TenHLV,
                                            x.LopHoc.ThoiGian,
                                            x.LopHoc.PhongTap
                                        }).ToList();
                }
            }

            return null; 
        }
    }
}
