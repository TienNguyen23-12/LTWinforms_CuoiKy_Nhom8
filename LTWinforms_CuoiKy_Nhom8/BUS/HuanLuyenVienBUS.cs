using LTWinforms_CuoiKy_Nhom8.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTWinforms_CuoiKy_Nhom8.BUS
{
    public class HuanLuyenVienBUS
    {
        QLTTDataContext db = new QLTTDataContext();

        public object XemBangLuong(int idTaiKhoan, out decimal tongLuong)
        {
            tongLuong = 0;
            var hlv = db.HuanLuyenViens.SingleOrDefault(x => x.IdTaiKhoan == idTaiKhoan);
            if (hlv == null)
            {
                return null;
            }

            decimal mucLuong = hlv.LuongCoBan ?? 0;

            var cacLop = db.LopHocs.Where(x => x.MaHLV == hlv.MaHLV && x.IsActive == true).ToList();

            tongLuong = cacLop.Count * mucLuong;

            return cacLop.Select(x => new {
                x.MaLop,
                x.TenLop,
                x.ThoiGian,
                TienCong = mucLuong 
            }).ToList();
        }
    }
}
