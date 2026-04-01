using LTWinforms_CuoiKy_Nhom8.BUS;
using LTWinforms_CuoiKy_Nhom8.DTO;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace LTWinforms_CuoiKy_Nhom8.GUI
{
    public partial class ucDangKyDay : UserControl
    {
        private readonly LopHocBUS lopBUS = new LopHocBUS();
        private readonly TinNhanBUS tnBUS = new TinNhanBUS();
        private bool isThemeApplied;
        private bool isLayoutHooked;

        public ucDangKyDay()
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

            label1.AutoSize = false;
            label1.TextAlign = ContentAlignment.MiddleCenter;
            label1.Font = new Font("Segoe UI", 11F, FontStyle.Regular);
            label1.ForeColor = Color.FromArgb(37, 48, 66);

            ModernTheme.StyleGrid(dgvLopTrong);
            dgvLopTrong.DefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            dgvLopTrong.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            ModernTheme.StyleButton(btnDangKyDay, Color.FromArgb(46, 134, 222), Color.White);
            btnDangKyDay.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            isThemeApplied = true;
        }

        private void ApplyResponsiveLayout()
        {
            int contentWidth = Math.Min(1120, ClientSize.Width - 40);
            int left = Math.Max(12, (ClientSize.Width - contentWidth) / 2);

            int top = 18;
            label1.SetBounds(left, top, contentWidth, 30);

            int gridTop = label1.Bottom + 12;
            int gridHeight = ClientSize.Height - gridTop - 90;
            if (gridHeight < 220)
            {
                gridHeight = 220;
            }

            dgvLopTrong.SetBounds(left, gridTop, contentWidth, gridHeight);

            int buttonWidth = 160;
            int buttonHeight = 38;
            int buttonLeft = left + (contentWidth - buttonWidth) / 2;
            int buttonTop = dgvLopTrong.Bottom + 14;
            btnDangKyDay.SetBounds(buttonLeft, buttonTop, buttonWidth, buttonHeight);
        }

        private void LoadData()
        {
            dgvLopTrong.DataSource = lopBUS.LayDanhSachLopChuaCoHLV();

            if (dgvLopTrong.Columns.Count > 0)
            {
                dgvLopTrong.Columns["MaLop"].HeaderText = "Mã Lớp";
                dgvLopTrong.Columns["TenLop"].HeaderText = "Tên Lớp Học";
                dgvLopTrong.Columns["ThoiGian"].HeaderText = "Thời Gian";
                dgvLopTrong.Columns["PhongTap"].HeaderText = "Phòng Tập";
                dgvLopTrong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }

            dgvLopTrong.DefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            dgvLopTrong.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        }

        private void ucDangKyDay_Load(object sender, EventArgs e)
        {
            ApplyTheme();

            if (!isLayoutHooked)
            {
                Resize += ucDangKyDay_Resize;
                isLayoutHooked = true;
            }

            ApplyResponsiveLayout();
            LoadData();
        }

        private void ucDangKyDay_Resize(object sender, EventArgs e)
        {
            ApplyResponsiveLayout();
        }

        private void btnDangKyDay_Click(object sender, EventArgs e)
        {
            if (dgvLopTrong.CurrentRow != null)
            {
                string maLop = dgvLopTrong.CurrentRow.Cells["MaLop"].Value.ToString();
                string tenLop = dgvLopTrong.CurrentRow.Cells["TenLop"].Value.ToString();

                if (ModernMessageBox.Show("Bạn muốn gửi yêu cầu nhận dạy lớp " + tenLop + " tới Quản lý?", "Xác nhận", MessageBoxButtons.YesNo, ModernMessageType.Question) == DialogResult.Yes)
                {
                    string noiDungMsg = "Xin chào Quản lý, tôi muốn đăng ký nhận giảng dạy lớp " + tenLop + " (Mã: " + maLop + "). Vui lòng xem xét và duyệt cho tôi nhé!";
                    string kq = tnBUS.GuiTinNhan(Session.IdTaiKhoan, 1, noiDungMsg);

                    if (kq == "")
                    {
                        ModernMessageBox.Show("Yêu cầu của bạn đã được gửi trực tiếp đến hộp thư của Quản lý thành công. Vui lòng chờ phản hồi.", "Thành công", ModernMessageType.Success);
                    }
                    else
                    {
                        ModernMessageBox.Show(kq, "Lỗi gửi yêu cầu", ModernMessageType.Error);
                    }
                }
            }
            else
            {
                ModernMessageBox.Show("Vui lòng chọn 1 lớp trong danh sách để đăng ký.", "Nhắc nhở", ModernMessageType.Warning);
            }
        }
    }
}
