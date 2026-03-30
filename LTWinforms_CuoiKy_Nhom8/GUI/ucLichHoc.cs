using LTWinforms_CuoiKy_Nhom8.BUS;
using LTWinforms_CuoiKy_Nhom8.DTO;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace LTWinforms_CuoiKy_Nhom8.GUI
{
    public partial class ucLichHoc : UserControl
    {
        private readonly LichHocBUS lichBUS = new LichHocBUS();
        private bool isThemeApplied;
        private bool isLayoutHooked;

        private Panel pnlHeaderCard;
        private Label lblMoTa;
        private Label lblTongLop;

        public ucLichHoc()
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

            EnsureHeaderControls();

            ModernTheme.StyleLabel(lblTieuDe, true);
            lblTieuDe.Font = new Font("Segoe UI", 16F, FontStyle.Bold);

            ModernTheme.StyleLabel(lblMoTa);
            lblMoTa.Font = new Font("Segoe UI", 10F, FontStyle.Regular);

            lblTongLop.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTongLop.ForeColor = Color.White;
            lblTongLop.BackColor = Color.FromArgb(58, 129, 214);
            lblTongLop.TextAlign = ContentAlignment.MiddleCenter;

            ModernTheme.StyleCard(pnlHeaderCard);
            ModernTheme.StyleGrid(dgvLich);

            isThemeApplied = true;
        }

        private void EnsureHeaderControls()
        {
            if (pnlHeaderCard != null)
            {
                return;
            }

            pnlHeaderCard = new Panel();
            pnlHeaderCard.Name = "pnlHeaderCard";

            lblMoTa = new Label();
            lblMoTa.Name = "lblMoTa";
            lblMoTa.AutoSize = false;

            lblTongLop = new Label();
            lblTongLop.Name = "lblTongLop";
            lblTongLop.AutoSize = false;

            pnlHeaderCard.Controls.Add(lblTieuDe);
            pnlHeaderCard.Controls.Add(lblMoTa);
            pnlHeaderCard.Controls.Add(lblTongLop);

            Controls.Add(pnlHeaderCard);
            pnlHeaderCard.BringToFront();
        }

        private void ApplyResponsiveLayout()
        {
            int contentWidth = Math.Min(1120, ClientSize.Width - 40);
            int left = Math.Max(20, (ClientSize.Width - contentWidth) / 2);

            int top = 18;

            pnlHeaderCard.SetBounds(left, top, contentWidth, 96);

            lblTieuDe.AutoSize = true;
            lblTieuDe.Location = new Point(20, 14);

            lblMoTa.SetBounds(20, 48, contentWidth - 200, 26);

            int badgeWidth = 140;
            lblTongLop.SetBounds(contentWidth - badgeWidth - 18, 30, badgeWidth, 36);

            int gridTop = pnlHeaderCard.Bottom + 14;
            int gridHeight = Math.Max(240, ClientSize.Height - gridTop - 20);
            dgvLich.SetBounds(left, gridTop, contentWidth, gridHeight);
        }

        private void LoadData()
        {
            var data = lichBUS.LayLichTheoQuyen(Session.Role, Session.IdTaiKhoan);
            dgvLich.DataSource = data;

            if (dgvLich.Columns.Count > 0)
            {
                dgvLich.Columns["MaLop"].HeaderText = "Mã Lớp";
                dgvLich.Columns["TenLop"].HeaderText = "Tên Lớp Học";
                dgvLich.Columns["TenHLV"].HeaderText = "Giáo Viên / HLV";
                dgvLich.Columns["ThoiGian"].HeaderText = "Lịch Học (Thời Gian)";
                dgvLich.Columns["PhongTap"].HeaderText = "Phòng Tập";

                dgvLich.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                dgvLich.Columns["MaLop"].FillWeight = 14;
                dgvLich.Columns["TenLop"].FillWeight = 30;
                dgvLich.Columns["TenHLV"].FillWeight = 24;
                dgvLich.Columns["ThoiGian"].FillWeight = 22;
                dgvLich.Columns["PhongTap"].FillWeight = 10;
            }

            lblTongLop.Text = "Tổng: " + dgvLich.Rows.Count + " lớp";
        }

        private void ApplyTitleByRole()
        {
            if (Session.Role == 1 || Session.Role == 2)
            {
                lblTieuDe.Text = "LỊCH TOÀN TRUNG TÂM";
                lblMoTa.Text = "Theo dõi lịch dạy và lịch học của toàn bộ lớp trong hệ thống.";
            }
            else if (Session.Role == 4)
            {
                lblTieuDe.Text = "LỊCH DẠY CỦA BẠN";
                lblMoTa.Text = "Danh sách các buổi dạy được phân công cho bạn.";
            }
            else
            {
                lblTieuDe.Text = "THỜI KHÓA BIỂU CỦA TÔI";
                lblMoTa.Text = "Theo dõi lịch học cá nhân theo từng lớp đã đăng ký.";
            }
        }

        private void ucLichHoc_Load(object sender, EventArgs e)
        {
            ApplyTheme();
            ApplyTitleByRole();
            LoadData();

            if (!isLayoutHooked)
            {
                Resize += ucLichHoc_Resize;
                isLayoutHooked = true;
            }

            ApplyResponsiveLayout();
        }

        private void ucLichHoc_Resize(object sender, EventArgs e)
        {
            ApplyResponsiveLayout();
        }
    }
}
