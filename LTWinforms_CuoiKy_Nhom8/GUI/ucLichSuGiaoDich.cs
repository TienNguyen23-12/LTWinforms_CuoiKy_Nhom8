using LTWinforms_CuoiKy_Nhom8.BUS;
using LTWinforms_CuoiKy_Nhom8.DAL;
using LTWinforms_CuoiKy_Nhom8.DTO;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace LTWinforms_CuoiKy_Nhom8.GUI
{
    public partial class ucLichSuGiaoDich : UserControl
    {
        private readonly HoaDonBUS hdBUS = new HoaDonBUS();
        private bool isThemeApplied;
        private bool isLayoutHooked;

        public ucLichSuGiaoDich()
        {
            InitializeComponent();
        }

        private void ucLichSuGiaoDich_Load(object sender, EventArgs e)
        {
            ApplyTheme();

            dtpTuNgay.CustomFormat = "dd/MM/yyyy";
            dtpTuNgay.Format = DateTimePickerFormat.Custom;
            dtpDenNgay.CustomFormat = "dd/MM/yyyy";
            dtpDenNgay.Format = DateTimePickerFormat.Custom;

            dtpTuNgay.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpDenNgay.Value = DateTime.Now;

            if (!isLayoutHooked)
            {
                Resize += ucLichSuGiaoDich_Resize;
                isLayoutHooked = true;
            }

            ApplyResponsiveLayout();
            BeginInvoke((Action)ApplyResponsiveLayout);

            LoadLichSu();
        }

        private void ucLichSuGiaoDich_Resize(object sender, EventArgs e)
        {
            ApplyResponsiveLayout();
        }

        private void ApplyTheme()
        {
            if (isThemeApplied)
            {
                return;
            }

            BackColor = ModernTheme.PageBackground;

            label1.AutoSize = false;
            label1.TextAlign = ContentAlignment.MiddleCenter;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label1.ForeColor = Color.FromArgb(37, 48, 66);

            groupBox1.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            groupBox1.ForeColor = Color.FromArgb(44, 62, 80);
            groupBox1.BackColor = Color.White;

            label2.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            label3.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            label2.ForeColor = Color.FromArgb(44, 62, 80);
            label3.ForeColor = Color.FromArgb(44, 62, 80);

            dtpTuNgay.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            dtpDenNgay.Font = new Font("Segoe UI", 10F, FontStyle.Regular);

            ModernTheme.StyleButton(btnXuat, Color.FromArgb(46, 134, 222), Color.White);
            btnXuat.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnXuat.Size = new Size(120, 38);

            ModernTheme.StyleGrid(dgvLichSu);
            dgvLichSu.DefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            dgvLichSu.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvLichSu.RowHeadersVisible = false;
            dgvLichSu.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLichSu.MultiSelect = false;
            dgvLichSu.ReadOnly = true;

            isThemeApplied = true;
        }

        private void ApplyResponsiveLayout()
        {
            int contentWidth = Math.Min(1160, ClientSize.Width - 30);
            if (contentWidth < 920)
            {
                contentWidth = 920;
            }

            int left = Math.Max(10, (ClientSize.Width - contentWidth) / 2);

            int top = 18;
            label1.SetBounds(left, top, contentWidth, 32);

            int gridTop = label1.Bottom + 12;
            int gridHeight = ClientSize.Height - gridTop - 170;
            if (gridHeight < 260)
            {
                gridHeight = 260;
            }

            dgvLichSu.SetBounds(left, gridTop, contentWidth, gridHeight);

            int groupTop = dgvLichSu.Bottom + 14;
            int groupHeight = 110;
            groupBox1.SetBounds(left, groupTop, contentWidth, groupHeight);

            int y = 42;
            int leftPadding = 24;
            int labelWidth = 70;
            int pickerWidth = 210;
            int gap = 14;

            label2.SetBounds(leftPadding, y + 6, labelWidth, 24);
            dtpTuNgay.SetBounds(label2.Right + 8, y, pickerWidth, 30);

            label3.SetBounds(dtpTuNgay.Right + gap + 20, y + 6, labelWidth, 24);
            dtpDenNgay.SetBounds(label3.Right + 8, y, pickerWidth, 30);

            int btnLeft = groupBox1.Width - btnXuat.Width - 24;
            btnXuat.SetBounds(btnLeft, y - 2, btnXuat.Width, btnXuat.Height);
        }

        private void LoadLichSu()
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
                        ModernMessageBox.Show("Lỗi: Tài khoản này chưa được liên kết với hồ sơ Hội viên nào!", "Lỗi hệ thống", ModernMessageType.Error);
                        return;
                    }

                    string maHoiVienCuaToi = hoiVien.MaHoiVien;
                    string tenKhachHang = hoiVien.HoTen;

                    var rawLichSu = (from hd in db.HoaDons
                                     where hd.NgayThanhToan >= tuNgay
                                        && hd.NgayThanhToan <= denNgay
                                        && hd.MaHoiVien == maHoiVienCuaToi
                                     join gt in db.GoiTaps on hd.MaGoi equals gt.MaGoi into bangGoiTap
                                     from khopGoiTap in bangGoiTap.DefaultIfEmpty()
                                     select new
                                     {
                                         NgayThanhToan = hd.NgayThanhToan,
                                         MaHoaDon = hd.MaHoaDon,
                                         NoiDung = khopGoiTap != null ? "Thanh toán gói: " + khopGoiTap.TenGoi : hd.GhiChu,
                                         SoTien = hd.SoTien
                                     }).ToList();

                    if (rawLichSu.Count == 0)
                    {
                        ModernMessageBox.Show("Bạn không có giao dịch thanh toán nào trong khoảng thời gian này!", "Thông báo", ModernMessageType.Info);
                        return;
                    }

                    var lstLichSu = rawLichSu.Select(x => new
                    {
                        NgayGiaoDich = Convert.ToDateTime(x.NgayThanhToan),
                        MaHoaDon = x.MaHoaDon.ToString(),
                        NoiDung = x.NoiDung,
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
                ModernMessageBox.Show("Đã xảy ra lỗi: \n" + ex.Message, "Lỗi", ModernMessageType.Error);
            }
        }
    }
}
