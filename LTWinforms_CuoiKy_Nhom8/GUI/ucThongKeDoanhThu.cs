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
            DateTime tuNgay = dtpTuNgay.Value;
            DateTime denNgay = dtpDenNgay.Value;

            dgvThongKe.DataSource = tkBUS.ThongKeTheoGoiTap(tuNgay, denNgay);

            if (dgvThongKe.Columns.Count > 0)
            {
                dgvThongKe.Columns["TenDichVu"].HeaderText = "Tên Dịch Vụ / Lớp Học";
                dgvThongKe.Columns["SoLuongBan"].HeaderText = "Số Lượng Bán Được";
                dgvThongKe.Columns["TongDoanhThu"].HeaderText = "Tổng Doanh Thu (VNĐ)";

                dgvThongKe.Columns["TongDoanhThu"].DefaultCellStyle.Format = "N0";

                dgvThongKe.Columns["TenDichVu"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            decimal tongTien = tkBUS.TinhTongDoanhThu(tuNgay, denNgay);
            txtTongDoanhThu.Text = tongTien.ToString("N0") + " VNĐ";
        }
    }
}
