using LTWinforms_CuoiKy_Nhom8.BUS;
using LTWinforms_CuoiKy_Nhom8.DAL;
using LTWinforms_CuoiKy_Nhom8.DTO;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace LTWinforms_CuoiKy_Nhom8.GUI
{
    public partial class ucHoSoCaNhan : UserControl
    {
        private readonly TaiKhoanBUS tkBUS = new TaiKhoanBUS();
        private QLTTDataContext db = new QLTTDataContext();
        private bool isThemeApplied;
        private bool isLayoutHooked;

        public ucHoSoCaNhan()
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

            StyleHeaderLabel(lblXinChao);
            StyleLabel(lblQuyen);
            StyleLabel(lblThongTinThem);

            StyleGroup(groupBox1);
            StyleGroup(groupBox2);

            StyleLabel(label1);
            StyleLabel(label2);
            StyleLabel(label3);
            StyleLabel(label4);
            StyleLabel(label5);
            StyleLabel(label6);
            StyleLabel(label7);

            StyleInput(txtPassCu);
            StyleInput(txtPassMoi);
            StyleInput(txtXacNhanPass);
            StyleInput(txtHoTen);
            StyleInput(txtSDT);

            cboGioiTinh.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            cboGioiTinh.DropDownStyle = ComboBoxStyle.DropDownList;
            cboGioiTinh.FlatStyle = FlatStyle.Flat;

            dtpNgaySinh.Font = new Font("Segoe UI", 10F, FontStyle.Regular);

            StylePrimaryButton(btnLuuMatKhau);
            StylePrimaryButton(btnLuuHoSo);

            btnLuuMatKhau.Size = new Size(92, 36);
            btnLuuHoSo.Size = new Size(92, 36);

            isThemeApplied = true;
        }

        private void StyleHeaderLabel(Label label)
        {
            label.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label.ForeColor = Color.FromArgb(34, 49, 63);
        }

        private void StyleLabel(Label label)
        {
            label.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            label.ForeColor = Color.FromArgb(44, 62, 80);
        }

        private void StyleGroup(GroupBox group)
        {
            group.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            group.ForeColor = Color.FromArgb(44, 62, 80);
            group.BackColor = Color.FromArgb(245, 249, 255);
        }

        private void StyleInput(TextBox textBox)
        {
            ModernTheme.StyleInput(textBox);
        }

        private void StylePrimaryButton(Button button)
        {
            ModernTheme.StyleButton(button, Color.FromArgb(46, 134, 222), Color.White);
            button.Height = 34;
        }

        private void ApplyResponsiveLayout()
        {
            int contentWidth = Math.Min(1140, ClientSize.Width - 36);
            if (contentWidth < 940)
            {
                contentWidth = 940;
            }

            int left = Math.Max(12, (ClientSize.Width - contentWidth) / 2);
            int top = 20;

            int colGap = 24;
            int leftColWidth = 430;
            int rightLeft = left + leftColWidth + colGap;
            int rightColWidth = contentWidth - leftColWidth - colGap;

            // Cột trái: thông tin tài khoản
            lblXinChao.SetBounds(left, top, leftColWidth, 30);
            lblQuyen.SetBounds(left, lblXinChao.Bottom + 6, leftColWidth, 24);

            lblThongTinThem.MaximumSize = new Size(leftColWidth, 0);
            lblThongTinThem.AutoSize = true;
            lblThongTinThem.Left = left;
            lblThongTinThem.Top = lblQuyen.Bottom + 6;

            // Cột phải: Đổi mật khẩu ở trên
            groupBox1.SetBounds(rightLeft, top, rightColWidth, 210);

            // Cột phải: Đổi thông tin ở dưới
            if (groupBox2.Visible)
            {
                groupBox2.SetBounds(rightLeft, groupBox1.Bottom + 16, rightColWidth, 262);
            }
        }

        private void ucHoSoCaNhan_Load(object sender, EventArgs e)
        {
            ApplyTheme();

            db = new QLTTDataContext();

            lblXinChao.Text = "Tài khoản: " + Session.Username;
            lblThongTinThem.Text = "";

            bool hoTroCapNhatHoSo = Session.Role == 2 || Session.Role == 3 || Session.Role == 4;
            groupBox2.Visible = hoTroCapNhatHoSo;

            if (Session.Role == 1)
            {
                lblQuyen.Text = "Vai trò: Quản trị viên (Admin)";
                lblThongTinThem.Text = "ID Hệ thống: " + Session.IdTaiKhoan +
                                       "\nQuyền hạn: Toàn quyền kiểm soát hệ thống" +
                                       "\nTài khoản Admin không có hồ sơ cá nhân để cập nhật tại màn này.";
            }
            else if (Session.Role == 2)
            {
                lblQuyen.Text = "Vai trò: Nhân viên";

                var nv = db.NhanViens.FirstOrDefault(x => x.IdTaiKhoan == Session.IdTaiKhoan);
                if (nv != null)
                {
                    string luong = nv.Luong.HasValue ? nv.Luong.Value.ToString("N0") + " VNĐ" : "Chưa cập nhật";
                    string sdt = string.IsNullOrEmpty(nv.SDT) ? "Chưa cập nhật" : nv.SDT;

                    lblThongTinThem.Text = "Mã Nhân Viên: " + nv.MaNhanVien + "\n" +
                                           "Họ và tên: " + nv.HoTen + "\n" +
                                           "Số điện thoại: " + sdt + "\n" +
                                           "Lương: " + luong;

                    txtHoTen.Text = nv.HoTen;
                    txtSDT.Text = nv.SDT;
                    cboGioiTinh.Text = nv.GioiTinh;
                    if (nv.NgaySinh.HasValue)
                    {
                        dtpNgaySinh.Value = nv.NgaySinh.Value;
                    }
                }
                else
                {
                    lblThongTinThem.Text = "ID Hệ thống: " + Session.IdTaiKhoan +
                                           "\nChưa có thông tin hồ sơ nhân viên.";
                }
            }
            else if (Session.Role == 4)
            {
                lblQuyen.Text = "Vai trò: Huấn luyện viên";

                var hlv = db.HuanLuyenViens.FirstOrDefault(x => x.IdTaiKhoan == Session.IdTaiKhoan);
                if (hlv != null)
                {
                    string luong = hlv.LuongCoBan.HasValue ? hlv.LuongCoBan.Value.ToString("N0") + " VNĐ/Lớp" : "Chưa cập nhật";
                    string sdt = string.IsNullOrEmpty(hlv.SDT) ? "Chưa cập nhật" : hlv.SDT;

                    lblThongTinThem.Text = "Mã HLV: " + hlv.MaHLV + "\n" +
                                           "Họ và tên: " + hlv.TenHLV + "\n" +
                                           "Số điện thoại: " + sdt + "\n" +
                                           "Lương cơ bản: " + luong;

                    txtHoTen.Text = hlv.TenHLV;
                    txtSDT.Text = hlv.SDT;
                    cboGioiTinh.Text = hlv.GioiTinh;
                    if (hlv.NgaySinh.HasValue)
                    {
                        dtpNgaySinh.Value = hlv.NgaySinh.Value;
                    }
                }
                else
                {
                    lblThongTinThem.Text = "Chưa có hồ sơ huấn luyện viên để hiển thị.";
                }
            }
            else if (Session.Role == 3)
            {
                lblQuyen.Text = "Vai trò: Hội viên (Khách hàng)";

                var hoiVien = db.HoiViens.FirstOrDefault(x => x.IdTaiKhoan == Session.IdTaiKhoan);
                if (hoiVien != null)
                {
                    string hanThe = hoiVien.NgayHetHan.HasValue ? hoiVien.NgayHetHan.Value.ToString("dd/MM/yyyy") : "Chưa có thẻ / Thẻ chưa kích hoạt";
                    string ngayDK = hoiVien.NgayDangKy.HasValue ? hoiVien.NgayDangKy.Value.ToString("dd/MM/yyyy") : "Không xác định";
                    string sdt = string.IsNullOrEmpty(hoiVien.SDT) ? "Chưa cập nhật" : hoiVien.SDT;

                    lblThongTinThem.Text = "Mã Hội Viên: " + hoiVien.MaHoiVien + "\n" +
                                           "Họ và tên: " + hoiVien.HoTen + "\n" +
                                           "Số điện thoại: " + sdt + "\n" +
                                           "Ngày đăng ký: " + ngayDK + "\n" +
                                           "Ngày hết hạn thẻ: " + hanThe;

                    txtHoTen.Text = hoiVien.HoTen;
                    txtSDT.Text = hoiVien.SDT;
                    cboGioiTinh.Text = hoiVien.GioiTinh;
                    if (hoiVien.NgaySinh.HasValue)
                    {
                        dtpNgaySinh.Value = hoiVien.NgaySinh.Value;
                    }
                }
            }

            if (!isLayoutHooked)
            {
                Resize += ucHoSoCaNhan_Resize;
                isLayoutHooked = true;
            }

            ApplyResponsiveLayout();
            BeginInvoke((Action)ApplyResponsiveLayout);
        }

        private void ucHoSoCaNhan_Resize(object sender, EventArgs e)
        {
            ApplyResponsiveLayout();
        }

        private void btnLuuMatKhau_Click(object sender, EventArgs e)
        {
            string passCu = txtPassCu.Text.Trim();
            string passMoi = txtPassMoi.Text.Trim();
            string xacNhan = txtXacNhanPass.Text.Trim();

            if (string.IsNullOrEmpty(passCu) || string.IsNullOrEmpty(passMoi) || string.IsNullOrEmpty(xacNhan))
            {
                ModernMessageBox.Show("Vui lòng nhập đầy đủ thông tin mật khẩu!", "Cảnh báo", ModernMessageType.Warning);
                return;
            }

            if (passMoi != xacNhan)
            {
                ModernMessageBox.Show("Mật khẩu xác nhận không khớp!", "Lỗi", ModernMessageType.Error);
                return;
            }

            string kq = tkBUS.DoiMatKhau(Session.IdTaiKhoan, passCu, passMoi);
            if (kq == "")
            {
                ModernMessageBox.Show("Đổi mật khẩu thành công! Vui lòng ghi nhớ mật khẩu mới.", "Thông báo", ModernMessageType.Success);
                txtPassCu.Clear();
                txtPassMoi.Clear();
                txtXacNhanPass.Clear();
            }
            else
            {
                ModernMessageBox.Show(kq, "Lỗi", ModernMessageType.Error);
            }
        }

        private void btnLuuHoSo_Click(object sender, EventArgs e)
        {
            if (Session.Role != 2 && Session.Role != 3 && Session.Role != 4)
            {
                ModernMessageBox.Show("Tài khoản này không hỗ trợ cập nhật hồ sơ tại màn này.", "Thông báo", ModernMessageType.Info);
                return;
            }

            string kq = tkBUS.CapNhatHoSoCaNhan(
                Session.IdTaiKhoan,
                Session.Role,
                txtHoTen.Text.Trim(),
                txtSDT.Text.Trim(),
                dtpNgaySinh.Value,
                cboGioiTinh.Text);

            if (kq == "")
            {
                ModernMessageBox.Show("Cập nhật thông tin cá nhân thành công!", "Thông báo", ModernMessageType.Success);
                ucHoSoCaNhan_Load(sender, e);
            }
            else
            {
                ModernMessageBox.Show(kq, "Lỗi cập nhật", ModernMessageType.Error);
            }
        }
    }
}
