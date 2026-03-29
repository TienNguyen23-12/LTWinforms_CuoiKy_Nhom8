using LTWinforms_CuoiKy_Nhom8.BUS;
using LTWinforms_CuoiKy_Nhom8.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LTWinforms_CuoiKy_Nhom8.GUI
{
    public partial class ucThongKeDoanhThu : UserControl
    {
        ThongKeBUS tkBUS = new ThongKeBUS();

        public ucThongKeDoanhThu()
        {
            InitializeComponent();
        }

        private void ucThongKeDoanhThu_Load(object sender, EventArgs e)
        {
            dtpTuNgay.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpDenNgay.Value = DateTime.Now;

            btnXemBaoCao_Click(sender, e);
        }

        private void btnXemBaoCao_Click(object sender, EventArgs e)
        {
            DateTime tuNgay = dtpTuNgay.Value.Date;
            DateTime denNgay = dtpDenNgay.Value.Date;

            dgvThongKe.DataSource = tkBUS.ThongKeTheoGoiTap(tuNgay, denNgay);
            if (dgvThongKe.Columns.Count > 0)
            {
                dgvThongKe.Columns["TenDichVu"].HeaderText = "Tên Dịch Vụ / Lớp Học";
                dgvThongKe.Columns["SoLuongBan"].HeaderText = "Số Lượng Bán";
                dgvThongKe.Columns["TongDoanhThu"].HeaderText = "Tổng Tiền Thu (VNĐ)";
                dgvThongKe.Columns["TongDoanhThu"].DefaultCellStyle.Format = "N0";
                dgvThongKe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }

            var dataTaiChinh = (List<dynamic>)tkBUS.ThongKeTaiChinh(tuNgay, denNgay);

            if (dataTaiChinh.Count > 0)
            {
                var row = dataTaiChinh.First(); 

                txtDoanhThu.Text = row.Doanh_Thu.ToString("N0") + " VNĐ";
                txtChiLuong.Text = row.Tong_Chi_Luong.ToString("N0") + " VNĐ";
                txtLoiNhuan.Text = row.Loi_Nhuan_Thuc.ToString("N0") + " VNĐ";
            }
            else
            {
                txtDoanhThu.Text = "0 VNĐ";
                txtChiLuong.Text = "0 VNĐ";
                txtLoiNhuan.Text = "0 VNĐ";
            }
        }

        
        private void btnIn_Click(object sender, EventArgs e)
        {
            frmCR_BaoCao frmBaoCao = new frmCR_BaoCao();
            DateTime tuNgay = dtpTuNgay.Value.Date;
            DateTime denNgay = dtpDenNgay.Value.Date.AddDays(1).AddSeconds(-1);

            try
            {
                using (QLTTDataContext db = new QLTTDataContext())
                {
                    // =========================================================
                    // 1. LẤY DỮ LIỆU TỔNG THU 
                    // =========================================================
                    // Bước 1: Kéo dữ liệu thô về RAM
                    var rawThu = db.HoaDons
                        .Where(hd => hd.NgayThanhToan >= tuNgay && hd.NgayThanhToan <= denNgay)
                        .ToList();

                    // Bước 2: Ép kiểu triệt để khử Nullable
                    var lstThu = rawThu.Select(hd => new
                    {
                        NgayThang = Convert.ToDateTime(hd.NgayThanhToan),
                        MaChungTu = hd.MaHoaDon.ToString(),
                        DienGiai = "Thu tiền gói tập (Mã gói: " + hd.MaGoi + ")",
                        LoaiGiaoDich = "1. TỔNG DOANH THU",
                        SoTien = Convert.ToDecimal(hd.SoTien)
                    }).ToList();

                    // =========================================================
                    // 2. LẤY DỮ LIỆU TỔNG CHI - NHÂN VIÊN
                    // =========================================================
                    var rawChiNV = (from cc in db.ChamCongs
                                    join nv in db.NhanViens on cc.IdTaiKhoan equals nv.IdTaiKhoan
                                    where cc.NgayCham >= tuNgay && cc.NgayCham <= denNgay
                                    select new { cc.NgayCham, cc.Id, nv.HoTen, nv.Luong }).ToList();

                    var lstChiNV = rawChiNV.Select(x => new
                    {
                        NgayThang = Convert.ToDateTime(x.NgayCham),
                        MaChungTu = "CC_" + x.Id.ToString(),
                        DienGiai = "Lương NV: " + x.HoTen,
                        LoaiGiaoDich = "2. TỔNG CHI LƯƠNG",
                        SoTien = Convert.ToDecimal(x.Luong)
                    }).ToList();

                    // =========================================================
                    // 3. LẤY DỮ LIỆU TỔNG CHI - HUẤN LUYỆN VIÊN
                    // =========================================================
                    var rawChiHLV = (from cc in db.ChamCongs
                                     join hlv in db.HuanLuyenViens on cc.IdTaiKhoan equals hlv.IdTaiKhoan
                                     where cc.NgayCham >= tuNgay && cc.NgayCham <= denNgay
                                     select new { cc.NgayCham, cc.Id, hlv.TenHLV, hlv.LuongCoBan }).ToList();

                    var lstChiHLV = rawChiHLV.Select(x => new
                    {
                        NgayThang = Convert.ToDateTime(x.NgayCham),
                        MaChungTu = "CC_" + x.Id.ToString(),
                        DienGiai = "Lương HLV: " + x.TenHLV,
                        LoaiGiaoDich = "2. TỔNG CHI LƯƠNG",
                        SoTien = Convert.ToDecimal(x.LuongCoBan)
                    }).ToList();

                    // =========================================================
                    // 4. GỘP CÁC DANH SÁCH & TÍNH TOÁN
                    // =========================================================
                    var lstBaoCao = lstThu.Concat(lstChiNV)
                                          .Concat(lstChiHLV)
                                          .OrderBy(x => x.LoaiGiaoDich)
                                          .ThenBy(x => x.NgayThang)
                                          .ToList();

                    if (lstBaoCao.Count == 0)
                    {
                        MessageBox.Show("Không có dữ liệu thu chi nào trong khoảng thời gian này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    // Khúc này không cần ?? 0 nữa vì SoTien đã được ép về decimal thuần túy
                    decimal tongThu = lstThu.Sum(x => x.SoTien);
                    decimal tongChi = lstChiNV.Sum(x => x.SoTien) + lstChiHLV.Sum(x => x.SoTien);
                    decimal loiNhuanRong = tongThu - tongChi;

                    txtDoanhThu.Text = tongThu.ToString("#,##0") + " VNĐ";
                    txtChiLuong.Text = tongChi.ToString("#,##0") + " VNĐ";
                    txtLoiNhuan.Text = loiNhuanRong.ToString("#,##0") + " VNĐ";

                    // =========================================================
                    // 5. HIỂN THỊ LÊN CRYSTAL REPORT QUA FORM MỚI
                    // =========================================================
                    rptDoanhThu rpt = new rptDoanhThu();
                    rpt.SetDataSource(lstBaoCao);

                    rpt.SetParameterValue("pTuNgay", tuNgay.ToString("dd/MM/yyyy"));
                    rpt.SetParameterValue("pDenNgay", dtpDenNgay.Value.Date.ToString("dd/MM/yyyy"));
                    rpt.SetParameterValue("pLoiNhuanRong", loiNhuanRong.ToString("#,##0") + " VNĐ");

                    frmBaoCao.HienThiBaoCao(rpt);
                    frmBaoCao.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi hệ thống: \n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
