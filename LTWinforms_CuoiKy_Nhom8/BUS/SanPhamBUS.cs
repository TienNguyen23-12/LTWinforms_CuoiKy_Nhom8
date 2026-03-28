using LTWinforms_CuoiKy_Nhom8.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTWinforms_CuoiKy_Nhom8.BUS
{
    public class SanPhamBUS
    {
        QLTTDataContext db = new QLTTDataContext();

        public object LayDanhSachSanPhamAdmin()
        {
            return db.SanPhams.Select(x => new {
                x.MaSP,
                x.TenSP,
                x.GiaTien,
                TrangThai = x.IsActive == true ? "Đang bán" : "Ngưng bán"
            }).ToList();
        }

        public string LuuSanPham(int maSP, string tenSP, decimal giaTien, bool isActive)
        {
            try
            {
                if (maSP == 0) 
                {
                    SanPham sp = new SanPham
                    {
                        TenSP = tenSP,
                        GiaTien = giaTien,
                        IsActive = isActive
                    };
                    db.SanPhams.InsertOnSubmit(sp);
                }
                else 
                {
                    var sp = db.SanPhams.SingleOrDefault(x => x.MaSP == maSP);
                    if (sp != null)
                    {
                        sp.TenSP = tenSP;
                        sp.GiaTien = giaTien;
                        sp.IsActive = isActive;
                    }
                }
                db.SubmitChanges();
                return "";
            }
            catch (Exception ex) 
            { 
                return ex.Message; 
            }
        }

        public string CapNhatSanPham(int maSP, string tenSP, decimal giaTien, bool isActive)
        {
            try
            {
                var sp = db.SanPhams.SingleOrDefault(x => x.MaSP == maSP);
                if (sp != null)
                {
                    sp.TenSP = tenSP;
                    sp.GiaTien = giaTien;
                    sp.IsActive = isActive;
                    db.SubmitChanges();
                    return "";
                }
                return "Không tìm thấy sản phẩm để sửa!";
            }
            catch (Exception ex) 
            { 
                return ex.Message; 
            }
        }
    }
}
