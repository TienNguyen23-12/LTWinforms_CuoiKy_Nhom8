using LTWinforms_CuoiKy_Nhom8.BUS;
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

            // 1. ĐỔ DỮ LIỆU CHI TIẾT VÀO BẢNG
            dgvThongKe.DataSource = tkBUS.ThongKeTheoGoiTap(tuNgay, denNgay);
            if (dgvThongKe.Columns.Count > 0)
            {
                dgvThongKe.Columns["TenDichVu"].HeaderText = "Tên Dịch Vụ / Lớp Học";
                dgvThongKe.Columns["SoLuongBan"].HeaderText = "Số Lượng Bán";
                dgvThongKe.Columns["TongDoanhThu"].HeaderText = "Tổng Tiền Thu (VNĐ)";
                dgvThongKe.Columns["TongDoanhThu"].DefaultCellStyle.Format = "N0";
                dgvThongKe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }

            // 2. TÍNH TOÁN 3 CON SỐ TỔNG QUAN ĐỂ HIỂN THỊ LÊN 3 CÁI LABEL
            var dataTaiChinh = (List<dynamic>)tkBUS.ThongKeTaiChinh(tuNgay, denNgay);

            if (dataTaiChinh.Count > 0)
            {
                var row = dataTaiChinh.First(); // Lấy dòng dữ liệu duy nhất

                // Gắn vào Label kèm format VNĐ
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
    }
}
