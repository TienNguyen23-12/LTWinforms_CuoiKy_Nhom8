using LTWinforms_CuoiKy_Nhom8.BUS;
using LTWinforms_CuoiKy_Nhom8.DAL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace LTWinforms_CuoiKy_Nhom8.GUI
{
    public partial class ucThongKeDoanhThu : UserControl
    {
        private readonly ThongKeBUS tkBUS = new ThongKeBUS();
        private bool isThemeApplied;
        private bool isLayoutHooked;

        public ucThongKeDoanhThu()
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

            ModernTheme.StyleLabel(label1);
            ModernTheme.StyleLabel(label2);

            ModernTheme.StyleLabel(label3);
            ModernTheme.StyleLabel(label6);
            ModernTheme.StyleLabel(label8);

            label3.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            label6.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            label8.Font = new Font("Segoe UI", 11F, FontStyle.Bold);

            dtpTuNgay.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            dtpDenNgay.Font = new Font("Segoe UI", 10F, FontStyle.Regular);

            ModernTheme.StyleInput(txtDoanhThu);
            ModernTheme.StyleInput(txtChiLuong);
            ModernTheme.StyleInput(txtLoiNhuan);

            txtDoanhThu.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            txtChiLuong.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            txtLoiNhuan.Font = new Font("Segoe UI", 11F, FontStyle.Bold);

            ModernTheme.StyleCard(panel1);
            ModernTheme.StyleCard(panel2);
            ModernTheme.StyleCard(panel3);

            ModernTheme.StyleButton(btnXemBaoCao, Color.FromArgb(58, 129, 214), Color.White);
            ModernTheme.StyleButton(btnIn, Color.FromArgb(53, 73, 95), Color.White);

            ModernTheme.StyleGrid(dgvThongKe);

            isThemeApplied = true;
        }

        private void StyleLabel(Label label)
        {
            label.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            label.ForeColor = Color.FromArgb(44, 62, 80);
        }

        private void StyleDateInput(DateTimePicker picker)
        {
            picker.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            picker.CalendarForeColor = Color.FromArgb(44, 62, 80);
        }

        private void StyleSummaryBox(TextBox textBox)
        {
            textBox.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            textBox.ForeColor = Color.FromArgb(35, 47, 62);
            textBox.BackColor = Color.White;
            textBox.BorderStyle = BorderStyle.FixedSingle;
            textBox.ReadOnly = true;
        }

        private void StyleCard(Panel panel)
        {
            panel.BackColor = Color.FromArgb(245, 249, 255);
            panel.BorderStyle = BorderStyle.FixedSingle;
        }

        private void StylePrimaryButton(Button button)
        {
            StyleButton(button, Color.FromArgb(46, 134, 222), Color.White);
        }

        private void StyleSecondaryButton(Button button)
        {
            StyleButton(button, Color.FromArgb(52, 73, 94), Color.White);
        }

        private void StyleButton(Button button, Color backColor, Color foreColor)
        {
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.FlatAppearance.MouseOverBackColor = ControlPaint.Light(backColor, 0.1f);
            button.FlatAppearance.MouseDownBackColor = ControlPaint.Dark(backColor, 0.1f);
            button.BackColor = backColor;
            button.ForeColor = foreColor;
            button.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            button.Cursor = Cursors.Hand;
            button.Height = 34;
        }

        private void StyleGrid(DataGridView grid)
        {
            grid.BorderStyle = BorderStyle.None;
            grid.BackgroundColor = Color.White;
            grid.EnableHeadersVisualStyles = false;
            grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(46, 134, 222);
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            grid.ColumnHeadersHeight = 38;

            grid.RowTemplate.Height = 28;
            grid.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 249, 255);
            grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(186, 222, 250);
            grid.DefaultCellStyle.SelectionForeColor = Color.FromArgb(25, 42, 58);

            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.MultiSelect = false;
            grid.ReadOnly = true;
            grid.AllowUserToAddRows = false;
            grid.AllowUserToResizeRows = false;
            grid.RowHeadersVisible = false;
        }

        private void ApplyResponsiveLayout()
        {
            int contentWidth = Math.Min(1100, ClientSize.Width - 40);
            int left = Math.Max(20, (ClientSize.Width - contentWidth) / 2);

            int top = 24;

            label1.SetBounds(left, top + 8, 70, 24);
            dtpTuNgay.SetBounds(left + 72, top, 170, 30);

            label2.SetBounds(left + 282, top + 8, 80, 24);
            dtpDenNgay.SetBounds(left + 364, top, 170, 30);

            btnXemBaoCao.SetBounds(left + 556, top - 2, 140, 38);
            btnIn.SetBounds(btnXemBaoCao.Right + 12, top - 2, 140, 38);

            int cardTop = top + 54;
            int gap = 12;
            int cardWidth = (contentWidth - (gap * 2)) / 3;
            int cardHeight = 104;

            panel1.SetBounds(left, cardTop, cardWidth, cardHeight);
            panel2.SetBounds(panel1.Right + gap, cardTop, cardWidth, cardHeight);
            panel3.SetBounds(panel2.Right + gap, cardTop, cardWidth, cardHeight);

            AlignSummaryCard(panel1, label3, txtDoanhThu);
            AlignSummaryCard(panel2, label6, txtChiLuong);
            AlignSummaryCard(panel3, label8, txtLoiNhuan);

            int gridTop = panel1.Bottom + 16;
            int gridHeight = Math.Max(220, ClientSize.Height - gridTop - 20);
            dgvThongKe.SetBounds(left, gridTop, contentWidth, gridHeight);
        }

        private void UpdateBaoCao(DateTime tuNgay, DateTime denNgay)
        {
            dgvThongKe.DataSource = tkBUS.ThongKeTheoGoiTap(tuNgay, denNgay);
            if (dgvThongKe.Columns.Count > 0)
            {
                dgvThongKe.Columns["TenDichVu"].HeaderText = "Tên Dịch Vụ / Lớp Học";
                dgvThongKe.Columns["SoLuongBan"].HeaderText = "Số Lượng Bán";
                dgvThongKe.Columns["TongDoanhThu"].HeaderText = "Tổng Tiền Thu (VNĐ)";
                dgvThongKe.Columns["TongDoanhThu"].DefaultCellStyle.Format = "N0";
                dgvThongKe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }

            decimal doanhThu = 0;
            decimal chiLuong = 0;
            decimal loiNhuan = 0;

            IEnumerable dataTaiChinh = tkBUS.ThongKeTaiChinh(tuNgay, denNgay) as IEnumerable;
            if (dataTaiChinh != null)
            {
                foreach (object obj in dataTaiChinh)
                {
                    dynamic row = obj;
                    doanhThu = Convert.ToDecimal(row.Doanh_Thu);
                    chiLuong = Convert.ToDecimal(row.Tong_Chi_Luong);
                    loiNhuan = Convert.ToDecimal(row.Loi_Nhuan_Thuc);
                    break;
                }
            }

            txtDoanhThu.Text = doanhThu.ToString("N0") + " VNĐ";
            txtChiLuong.Text = chiLuong.ToString("N0") + " VNĐ";
            txtLoiNhuan.Text = loiNhuan.ToString("N0") + " VNĐ";
        }

        private void ucThongKeDoanhThu_Load(object sender, EventArgs e)
        {
            ApplyTheme();

            dtpTuNgay.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpDenNgay.Value = DateTime.Now;

            if (!isLayoutHooked)
            {
                Resize += ucThongKeDoanhThu_Resize;
                isLayoutHooked = true;
            }

            ApplyResponsiveLayout();
            btnXemBaoCao_Click(sender, e);
        }

        private void ucThongKeDoanhThu_Resize(object sender, EventArgs e)
        {
            ApplyResponsiveLayout();
        }

        private void btnXemBaoCao_Click(object sender, EventArgs e)
        {
            DateTime tuNgay = dtpTuNgay.Value.Date;
            DateTime denNgay = dtpDenNgay.Value.Date;

            if (tuNgay > denNgay)
            {
                MessageBox.Show("Từ ngày không được lớn hơn Đến ngày!", "Cảnh báo");
                return;
            }

            UpdateBaoCao(tuNgay, denNgay);
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
                    // BƯỚC 1: LẤY DỮ LIỆU TỔNG THU (Từ Database)
                    var rawThu = db.HoaDons
                        .Where(hd => hd.NgayThanhToan >= tuNgay && hd.NgayThanhToan <= denNgay)
                        .ToList();

                    var lstThu = rawThu.Select(hd => new
                    {
                        NgayThang = Convert.ToDateTime(hd.NgayThanhToan),
                        MaChungTu = hd.MaHoaDon.ToString(),
                        // Tích hợp logic xử lý Ghi chú thông minh
                        DienGiai = string.IsNullOrEmpty(hd.GhiChu)
                                   ? "Thu tiền gói tập (Mã gói: " + hd.MaGoi + ")"
                                   : hd.GhiChu,
                        LoaiGiaoDich = "1. TỔNG DOANH THU",
                        SoTien = Convert.ToDecimal(hd.SoTien)
                    }).ToList();

                    // BƯỚC 2: LẤY DỮ LIỆU TỔNG CHI (Gọi từ tầng BUS)
                    NhanSuBUS nhanSuBUS = new NhanSuBUS();
                    IEnumerable<dynamic> rawChi = (IEnumerable<dynamic>)nhanSuBUS.TinhBangLuongChiTiet(tuNgay, denNgay);

                    // Thêm các lệnh ép kiểu (string) và (decimal) để đồng bộ cấu trúc với lstThu
                    var lstChi = rawChi.Select(x => new
                    {
                        NgayThang = denNgay.Date,

                        // Ép kiểu cứng về string
                        MaChungTu = (string)("L_" + x.MaNhanSu.ToString()),

                        // Ép kiểu cứng về string
                        DienGiai = (string)("Thanh toán lương " + x.VaiTro.ToString().ToLower() + ": " + x.HoTen),

                        LoaiGiaoDich = "2. TỔNG CHI LƯƠNG",

                        // Ép kiểu cứng về decimal
                        SoTien = (decimal)x.ThucLanh

                    }).ToList();

                    // BƯỚC 3: GỘP DỮ LIỆU VÀ SẮP XẾP
                    var lstBaoCao = lstThu.Concat(lstChi)
                                          .OrderBy(x => x.LoaiGiaoDich)
                                          .ThenBy(x => x.NgayThang)
                                          .ToList();

                    if (lstBaoCao.Count == 0)
                    {
                        MessageBox.Show("Không có dữ liệu thu chi nào trong khoảng thời gian này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    // BƯỚC 4: TÍNH TOÁN LỢI NHUẬN RÒNG ĐỂ ĐẨY LÊN GIAO DIỆN
                    decimal tongThu = lstThu.Sum(x => x.SoTien);
                    decimal tongChi = lstChi.Sum(x => x.SoTien);
                    decimal loiNhuanRong = tongThu - tongChi;

                    txtDoanhThu.Text = tongThu.ToString("#,##0") + " VNĐ";
                    txtChiLuong.Text = tongChi.ToString("#,##0") + " VNĐ";
                    txtLoiNhuan.Text = loiNhuanRong.ToString("#,##0") + " VNĐ";

                    // BƯỚC 5: ĐẨY DỮ LIỆU VÀO CRYSTAL REPORTS
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
                MessageBox.Show("Đã xảy ra lỗi hệ thống khi in báo cáo: \n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AlignSummaryCard(Panel panel, Label titleLabel, TextBox valueTextBox)
        {
            titleLabel.AutoSize = false;
            titleLabel.TextAlign = ContentAlignment.MiddleCenter;
            titleLabel.SetBounds(10, 12, panel.Width - 20, 26);

            valueTextBox.BorderStyle = BorderStyle.FixedSingle;
            valueTextBox.SetBounds(18, 48, panel.Width - 36, 32);
        }
    }
}
