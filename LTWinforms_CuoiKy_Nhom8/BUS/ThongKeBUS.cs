using LTWinforms_CuoiKy_Nhom8.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTWinforms_CuoiKy_Nhom8.BUS
{
    public class ThongKeBUS
    {
        QLTTDataContext db = new QLTTDataContext();

        public object ThongKeTheoGoiTap(DateTime tuNgay, DateTime denNgay)
        {
            var query = db.HoaDons
                          .Where(x => x.NgayThanhToan.Value.Date >= tuNgay.Date && x.NgayThanhToan.Value.Date <= denNgay.Date)
                          .GroupBy(x => x.GoiTap.TenGoi)
                          .Select(g => new
                          {
                              TenGoi = g.Key,
                              SoLuongBan = g.Count(), 
                              TongDoanhThu = g.Sum(x => x.SoTien) 
                          }).OrderByDescending(x => x.TongDoanhThu).ToList();

            return query;
        }

        public decimal TinhTongDoanhThu(DateTime tuNgay, DateTime denNgay)
        {
            var query = db.HoaDons.Where(x => x.NgayThanhToan.Value.Date >= tuNgay.Date && x.NgayThanhToan.Value.Date <= denNgay.Date);

            if (query.Any()) 
            {
                return query.Sum(x => x.SoTien);
            }
            return 0;
        }
    }
}
