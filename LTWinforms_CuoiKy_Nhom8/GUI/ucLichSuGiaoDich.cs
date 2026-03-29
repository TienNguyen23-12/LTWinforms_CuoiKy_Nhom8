using LTWinforms_CuoiKy_Nhom8.BUS;
using LTWinforms_CuoiKy_Nhom8.DAL;
using LTWinforms_CuoiKy_Nhom8.DTO;
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
    public partial class ucLichSuGiaoDich : UserControl
    {
        HoaDonBUS hdBUS = new HoaDonBUS();
        public ucLichSuGiaoDich()
        {
            InitializeComponent();
        }

        private void ucLichSuGiaoDich_Load(object sender, EventArgs e)
        {
            dgvLichSu.DataSource = hdBUS.LayLichSuCuaKhachHang(Session.IdTaiKhoan);

            if (dgvLichSu.Columns.Count > 0)
            {
                dgvLichSu.Columns["MaHoaDon"].HeaderText = "Mã Hóa Đơn";
                dgvLichSu.Columns["TenGoi"].HeaderText = "Dịch Vụ / Lớp Học";
                dgvLichSu.Columns["SoTien"].HeaderText = "Số Tiền (VNĐ)";
                dgvLichSu.Columns["NgayThanhToan"].HeaderText = "Ngày Giao Dịch";
                dgvLichSu.Columns["TrangThai"].HeaderText = "Trạng Thái";

                dgvLichSu.Columns["SoTien"].DefaultCellStyle.Format = "N0";
                dgvLichSu.Columns["NgayThanhToan"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                dgvLichSu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private void btnXuat_Click(object sender, EventArgs e)
        {
            DateTime tuNgay = dtpTuNgay.Value.Date;
            DateTime denNgay = dtpDenNgay.Value.Date.AddDays(1).AddSeconds(-1);

            int idTaiKhoanHienTai = Session.IdTaiKhoan;

            try
            {
                using (QLTTDataContext db = new QLTTDataContext())
                {
                    var hoiVien = db.HoiViens.FirstOrDefault(hv => hv.IdTaiKhoan == idTaiKhoanHienTai);

                    if (hoiVien == null)
                    {
                        MessageBox.Show("Lỗi: Tài khoản này chưa được liên kết với hồ sơ Hội viên nào!", "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    string maHoiVienCuaToi = hoiVien.MaHoiVien;
                    string tenKhachHang = hoiVien.HoTen; // Chữ HoTen này có thể đổi lại tùy theo tên cột thực tế trong DB của bạn

                    var rawLichSu = (from hd in db.HoaDons
                                     where hd.NgayThanhToan >= tuNgay
                                        && hd.NgayThanhToan <= denNgay
                                        && hd.MaHoiVien == maHoiVienCuaToi

                                     // Cú pháp Left Join trong LINQ
                                     join gt in db.GoiTaps on hd.MaGoi equals gt.MaGoi into bangGoiTap
                                     from khopGoiTap in bangGoiTap.DefaultIfEmpty() // Đảm bảo lấy cả hóa đơn không có gói tập

                                     select new
                                     {
                                         NgayThanhToan = hd.NgayThanhToan,
                                         MaHoaDon = hd.MaHoaDon,
                                         // Nếu tìm thấy gói tập thì lấy TenGoi, nếu không thì lấy GhiChu của hóa đơn
                                         NoiDung = khopGoiTap != null ? "Thanh toán gói: " + khopGoiTap.TenGoi : hd.GhiChu,
                                         SoTien = hd.SoTien
                                     }).ToList();

                    if (rawLichSu.Count == 0)
                    {
                        MessageBox.Show("Bạn không có giao dịch thanh toán nào trong khoảng thời gian này!");
                        return;
                    }

                    // =========================================================
                    // BƯỚC 3: ÉP KIỂU TRIỆT ĐỂ CHO CRYSTAL REPORT
                    // =========================================================
                    var lstLichSu = rawLichSu.Select(x => new
                    {
                        NgayGiaoDich = Convert.ToDateTime(x.NgayThanhToan),
                        MaHoaDon = x.MaHoaDon.ToString(),
                        NoiDung = x.NoiDung, // Truyền trực tiếp cột NoiDung đã xử lý ở Bước 2 xuống đây
                        SoTien = Convert.ToDecimal(x.SoTien)
                    }).OrderByDescending(x => x.NgayGiaoDich).ToList();

                    rptLichSuGiaoDich rpt = new rptLichSuGiaoDich();
                    rpt.SetDataSource(lstLichSu);

                    rpt.SetParameterValue("pTuNgay", tuNgay.ToString("dd/MM/yyyy"));
                    rpt.SetParameterValue("pDenNgay", dtpDenNgay.Value.Date.ToString("dd/MM/yyyy"));
                    rpt.SetParameterValue("pTenNguoiDung", tenKhachHang);

                    frmCR_BaoCao frmBaoCao = new frmCR_BaoCao();
                    frmBaoCao.HienThiBaoCao(rpt);
                    frmBaoCao.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: \n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
