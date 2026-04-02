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
                SetHeaderText(dgvBangLuong, "Mã nhân sự", "MaNhanSu", "Ma_NS");
                SetHeaderText(dgvBangLuong, "Họ và tên", "HoTen", "Ho_Ten");
                SetHeaderText(dgvBangLuong, "Vai trò", "VaiTro", "Vai_Tro");
                SetHeaderText(dgvBangLuong, "Số công / buổi", "SoNgayLam", "SoNgay_Lam", "So_Cong");
                SetHeaderText(dgvBangLuong, "Lương theo công (VNĐ)", "Luong1Ngay", "Luong_1_Ngay");
                SetHeaderText(dgvBangLuong, "Lương cơ bản (VNĐ)", "LuongCoBan", "Luong_Co_Ban");
                SetHeaderText(dgvBangLuong, "Thưởng (VNĐ)", "Thuong", "Thuong_VND");
                SetHeaderText(dgvBangLuong, "Bị phạt (VNĐ)", "TienPhat", "Tien_Phat");
                SetHeaderText(dgvBangLuong, "Thực lãnh (VNĐ)", "ThucLanh", "Thuc_Lanh");

                FormatCurrencyColumn(dgvBangLuong, "Luong1Ngay", "Luong_1_Ngay");
                FormatCurrencyColumn(dgvBangLuong, "LuongCoBan", "Luong_Co_Ban");
                FormatCurrencyColumn(dgvBangLuong, "Thuong", "Thuong_VND");
                FormatCurrencyColumn(dgvBangLuong, "TienPhat", "Tien_Phat");
                FormatCurrencyColumn(dgvBangLuong, "ThucLanh", "Thuc_Lanh");

                DataGridViewColumn colTienPhat = GetColumn(dgvBangLuong, "TienPhat", "Tien_Phat");
                if (colTienPhat != null)
                {
                    colTienPhat.DefaultCellStyle.ForeColor = Color.Red;
                }

                DataGridViewColumn colThucLanh = GetColumn(dgvBangLuong, "ThucLanh", "Thuc_Lanh");
                if (colThucLanh != null)
                {
                    colThucLanh.DefaultCellStyle.ForeColor = Color.Green;
                    colThucLanh.DefaultCellStyle.Font = new Font(dgvBangLuong.Font, FontStyle.Regular);
                }

                NormalizeHeaderUnderscoreToSpace(dgvBangLuong);
                dgvBangLuong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvBangLuong.RowHeadersVisible = false;
            }

            TinhTongLuong();
        }

        private static DataGridViewColumn GetColumn(DataGridView grid, params string[] names)
        {
            foreach (string name in names)
            {
                if (grid.Columns.Contains(name))
                {
                    return grid.Columns[name];
                }
            }

            return null;
        }

        private static void SetHeaderText(DataGridView grid, string headerText, params string[] names)
        {
            DataGridViewColumn column = GetColumn(grid, names);
            if (column != null)
            {
                column.HeaderText = headerText;
            }
        }

        private static void FormatCurrencyColumn(DataGridView grid, params string[] names)
        {
            DataGridViewColumn column = GetColumn(grid, names);
            if (column != null)
            {
                column.DefaultCellStyle.Format = "N0";
            }
        }

        private static void NormalizeHeaderUnderscoreToSpace(DataGridView grid)
        {
            foreach (DataGridViewColumn column in grid.Columns)
            {
                if (!string.IsNullOrEmpty(column.HeaderText) && column.HeaderText.Contains("_"))
                {
                    column.HeaderText = column.HeaderText.Replace("_", " ");
                }
            }
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
            txtTongLuong.Font = new Font("Segoe UI", 11F, FontStyle.Bold);

            if (tongTien > 0)
            {
                txtTongLuong.ForeColor = Color.FromArgb(39, 174, 96); // xanh
            }
            else if (tongTien < 0)
            {
                txtTongLuong.ForeColor = Color.FromArgb(231, 76, 60); // đỏ
            }
            else
            {
                txtTongLuong.ForeColor = ModernTheme.TextPrimary; // = 0
            }
        }

        private void btnXuat_Click(object sender, EventArgs e)
        {
            DateTime tuNgay = dtpTuNgay.Value.Date;
            DateTime denNgay = dtpDenNgay.Value.Date.AddDays(1).AddSeconds(-1);

            // BƯỚC 2: BẮT DỮ LIỆU TỪ CLASS SESSION DTO CỦA BẠN
            // Tự động lấy chính xác ID và Role của người đang thao tác trên phần mềm
            int currentIdTaiKhoan = Session.IdTaiKhoan;
            int currentRole = Session.Role;

            // Chốt chặn an toàn: Nếu ID = 0 tức là chưa đăng nhập hoặc lỗi phiên bản
            if (currentIdTaiKhoan == 0)
            {
                MessageBox.Show("Không tìm thấy thông tin đăng nhập. Vui lòng đăng xuất và đăng nhập lại!", "Lỗi bảo mật", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // BƯỚC 3: GỌI TẦNG BUS ĐỂ TÍNH TOÁN
                NhanSuBUS nhanSuBUS = new NhanSuBUS();
                IEnumerable<dynamic> rawData = (IEnumerable<dynamic>)nhanSuBUS.TinhLuongCaNhan(
                                                                            currentIdTaiKhoan,
                                                                            currentRole,
                                                                            tuNgay,
                                                                            denNgay);

                // BƯỚC 4: NẮN KHUÔN DỮ LIỆU ĐỂ ĐẨY VÀO CRYSTAL REPORTS
                var lstPhieuLuong = rawData.Select(x => new
                {
                    MaNS = (string)x.MaNhanSu,
                    HoTen = (string)x.HoTen,
                    VaiTro = (string)x.VaiTro,
                    SoCong = (int)x.SoNgayLam,
                    BiPhat = (decimal)x.TienPhat,
                    Luong1Ngay = currentRole == 2 ? (decimal)x.Luong1Ngay : 0m,
                    Thuong = currentRole == 4 ? (decimal)x.Thuong : 0m,
                    ThucLanh = (decimal)x.ThucLanh
                }).ToList();

                // BƯỚC 5: KIỂM TRA RỖNG
                if (lstPhieuLuong.Count == 0)
                {
                    MessageBox.Show("Bạn không có dữ liệu công trong khoảng thời gian này để in phiếu lương!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // BƯỚC 6: XUẤT RA GIAO DIỆN IN
                frmCR_BaoCao frmBaoCao = new frmCR_BaoCao();

                // Nhớ đổi tên 'rptPhieuLuongCaNhan' thành tên file .rpt thực tế của bạn
                rptPhieuLuongCaNhan rpt = new rptPhieuLuongCaNhan();
                rpt.SetDataSource(lstPhieuLuong);

                rpt.SetParameterValue("pTuNgay", tuNgay.ToString("dd/MM/yyyy"));
                rpt.SetParameterValue("pDenNgay", dtpDenNgay.Value.Date.ToString("dd/MM/yyyy"));

                frmBaoCao.HienThiBaoCao(rpt);
                frmBaoCao.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi hệ thống khi in phiếu lương: \n" + ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
