using LTWinforms_CuoiKy_Nhom8.BUS;
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
    public partial class ucLuongThuong : UserControl
    {
        private readonly NhanSuBUS nsBUS = new NhanSuBUS();
        private bool isThemeApplied;
        private bool isLayoutHooked;

        public ucLuongThuong()
        {
            InitializeComponent();
        }

        private void ApplyTheme()
        {
            if (isThemeApplied)
            {
                return;
            }

            BackColor = ModernTheme.PageBackground;

            Font normalFont = new Font("Segoe UI", 10F, FontStyle.Regular);

            Font = normalFont;
            label1.Font = normalFont;
            label2.Font = normalFont;
            label3.Font = normalFont;

            label1.ForeColor = ModernTheme.TextPrimary;
            label2.ForeColor = ModernTheme.TextPrimary;
            label3.ForeColor = ModernTheme.TextPrimary;

            ModernTheme.StyleInput(dtpTuNgay);
            ModernTheme.StyleInput(dtpDenNgay);

            ModernTheme.StyleButton(btnXemLuong, Color.FromArgb(46, 134, 222), Color.White);
            ModernTheme.StyleButton(btnXuat, Color.FromArgb(52, 73, 94), Color.White);

            ModernTheme.StyleGrid(dgvBangLuong);
            dgvBangLuong.DefaultCellStyle.Font = normalFont;
            dgvBangLuong.DefaultCellStyle.ForeColor = ModernTheme.TextPrimary;
            dgvBangLuong.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            txtTongLuong.ReadOnly = true;
            txtTongLuong.BorderStyle = BorderStyle.FixedSingle;
            txtTongLuong.BackColor = Color.White;
            txtTongLuong.ForeColor = Color.FromArgb(39, 174, 96);
            txtTongLuong.Font = new Font("Segoe UI", 11F, FontStyle.Regular);
            txtTongLuong.TextAlign = HorizontalAlignment.Right;

            isThemeApplied = true;
        }

        private void ApplyResponsiveLayout()
        {
            int contentWidth = Math.Min(1120, ClientSize.Width - 40);
            int left = Math.Max(12, (ClientSize.Width - contentWidth) / 2);

            // Row bộ lọc ngày (căn giữa)
            int filterY = 24;
            int labelWidth = 72;
            int dateWidth = 220;
            int betweenLabelDate = 8;
            int betweenGroups = 46;

            int filterRowWidth = (labelWidth + betweenLabelDate + dateWidth) * 2 + betweenGroups;
            int filterLeft = left + (contentWidth - filterRowWidth) / 2;

            label3.SetBounds(filterLeft, filterY + 5, labelWidth, 24);
            dtpTuNgay.SetBounds(label3.Right + betweenLabelDate, filterY, dateWidth, 30);

            label1.SetBounds(dtpTuNgay.Right + betweenGroups, filterY + 5, labelWidth, 24);
            dtpDenNgay.SetBounds(label1.Right + betweenLabelDate, filterY, dateWidth, 30);

            // Row button (căn giữa)
            int buttonY = dtpTuNgay.Bottom + 14;
            int btnWidth = 180;
            int btnHeight = 36;
            int btnGap = 14;
            int buttonRowWidth = btnWidth * 2 + btnGap;
            int buttonLeft = left + (contentWidth - buttonRowWidth) / 2;

            btnXemLuong.SetBounds(buttonLeft, buttonY, btnWidth, btnHeight);
            btnXuat.SetBounds(btnXemLuong.Right + btnGap, buttonY, btnWidth, btnHeight);

            // Grid
            int gridTop = btnXemLuong.Bottom + 20;
            int gridHeight = ClientSize.Height - gridTop - 110;
            if (gridHeight < 250)
            {
                gridHeight = 250;
            }

            dgvBangLuong.SetBounds(left, gridTop, contentWidth, gridHeight);

            // Row tổng lương (căn giữa)
            int totalY = dgvBangLuong.Bottom + 18;
            int totalLabelWidth = 110;
            int totalTextWidth = 260;
            int totalGap = 10;
            int totalRowWidth = totalLabelWidth + totalGap + totalTextWidth;
            int totalLeft = left + (contentWidth - totalRowWidth) / 2;

            label2.SetBounds(totalLeft, totalY + 4, totalLabelWidth, 24);
            txtTongLuong.SetBounds(label2.Right + totalGap, totalY, totalTextWidth, 34);
        }

        private void ucLuongThuong_Load(object sender, EventArgs e)
        {
            ApplyTheme();

            dtpTuNgay.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpDenNgay.Value = DateTime.Now;

            if (!isLayoutHooked)
            {
                Resize += ucLuongThuong_Resize;
                isLayoutHooked = true;
            }

            ApplyResponsiveLayout();
            btnXemLuong_Click(sender, e);
        }

        private void ucLuongThuong_Resize(object sender, EventArgs e)
        {
            ApplyResponsiveLayout();
        }

        private void btnXemLuong_Click(object sender, EventArgs e)
        {
            DateTime tuNgay = dtpTuNgay.Value.Date;
            DateTime denNgay = dtpDenNgay.Value.Date;

            if (Session.Role == 1) 
            {
                dgvBangLuong.DataSource = nsBUS.TinhBangLuongChiTiet(tuNgay, denNgay);
            }
            else 
            {
                dgvBangLuong.DataSource = nsBUS.TinhLuongCaNhan(Session.IdTaiKhoan, Session.Role, tuNgay, denNgay);
            }

            if (dgvBangLuong.Columns.Count > 0)
            {
                dgvBangLuong.Columns["MaNhanSu"].HeaderText = "Mã NS";
                dgvBangLuong.Columns["HoTen"].HeaderText = "Họ và Tên";
                dgvBangLuong.Columns["VaiTro"].HeaderText = "Vai Trò";
                dgvBangLuong.Columns["SoNgayLam"].HeaderText = "Số Công / Buổi";

                dgvBangLuong.Columns["TienPhat"].HeaderText = "Bị Phạt (VNĐ)";
                dgvBangLuong.Columns["TienPhat"].DefaultCellStyle.Format = "N0";
                dgvBangLuong.Columns["TienPhat"].DefaultCellStyle.ForeColor = Color.Red;

                dgvBangLuong.Columns["ThucLanh"].HeaderText = "Thực Lãnh (VNĐ)";
                dgvBangLuong.Columns["ThucLanh"].DefaultCellStyle.Format = "N0";
                dgvBangLuong.Columns["ThucLanh"].DefaultCellStyle.ForeColor = Color.Green;
                dgvBangLuong.Columns["ThucLanh"].DefaultCellStyle.Font = new Font(dgvBangLuong.Font, FontStyle.Regular);

                dgvBangLuong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvBangLuong.RowHeadersVisible = false;
            }

            TinhTongLuong();
        }

        private void TinhTongLuong()
        {
            decimal tongTien = 0;

            foreach (DataGridViewRow row in dgvBangLuong.Rows)
            {
                if (row.Cells["ThucLanh"].Value != null)
                {
                    tongTien += Convert.ToDecimal(row.Cells["ThucLanh"].Value);
                }
            }

            txtTongLuong.Text = tongTien.ToString("N0") + " VNĐ";
            txtTongLuong.ForeColor = Color.FromArgb(39, 174, 96);
        }

        private void btnXuat_Click(object sender, EventArgs e)
        {
            if (dgvBangLuong.Rows.Count == 0 || dgvBangLuong.Rows[0].Cells[0].Value == null)
            {
                MessageBox.Show("Chưa có dữ liệu lương để xuất phiếu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DataGridViewRow row = dgvBangLuong.Rows[0];

                var lstPhieuLuong = new[]
                {
            new
            {
                MaNS = row.Cells[0].Value.ToString(),
                HoTen = row.Cells[1].Value.ToString(),
                VaiTro = row.Cells[2].Value.ToString(),
                
                SoCong = Convert.ToDouble(row.Cells[3].Value?.ToString() ?? "0"),
                BiPhat = Convert.ToDecimal(row.Cells[4].Value?.ToString().Replace(",", "").Replace(".", "") ?? "0"),
                ThucLanh = Convert.ToDecimal(row.Cells[5].Value?.ToString().Replace(",", "").Replace(".", "") ?? "0")
            }
        }.ToList();

                rptPhieuLuongCaNhan rpt = new rptPhieuLuongCaNhan();
                rpt.SetDataSource(lstPhieuLuong);

                rpt.SetParameterValue("pTuNgay", dtpTuNgay.Value.ToString("dd/MM/yyyy"));
                rpt.SetParameterValue("pDenNgay", dtpDenNgay.Value.ToString("dd/MM/yyyy"));

                frmCR_BaoCao frmBaoCao = new frmCR_BaoCao();
                frmBaoCao.HienThiBaoCao(rpt);
                frmBaoCao.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xuất phiếu lương: \n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
