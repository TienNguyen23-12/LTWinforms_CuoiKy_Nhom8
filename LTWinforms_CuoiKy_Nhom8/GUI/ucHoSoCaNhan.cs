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

            BackColor = Color.White;

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

            isThemeApplied = true;
        }

        private void StyleHeaderLabel(Label label)
        {
            label.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            label.ForeColor = Color.FromArgb(34, 49, 63);
        }

        private void StyleLabel(Label label)
        {
            label.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            label.ForeColor = Color.FromArgb(44, 62, 80);
        }

        private void StyleGroup(GroupBox group)
        {
            group.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            group.ForeColor = Color.FromArgb(44, 62, 80);
            group.BackColor = Color.FromArgb(245, 249, 255);
        }

        private void StyleInput(TextBox textBox)
        {
            textBox.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            textBox.ForeColor = Color.FromArgb(44, 62, 80);
            textBox.BackColor = Color.White;
            textBox.BorderStyle = BorderStyle.FixedSingle;
        }

        private void StylePrimaryButton(Button button)
        {
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.BackColor = Color.FromArgb(46, 134, 222);
            button.ForeColor = Color.White;
            button.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            button.Cursor = Cursors.Hand;
            button.Height = 34;
        }

        private void ApplyResponsiveLayout()
        {
            int contentWidth = Math.Min(1100, ClientSize.Width - 40);
            int left = Math.Max(20, (ClientSize.Width - contentWidth) / 2);

            int top = 20;

            lblXinChao.SetBounds(left, top, 420, 28);
            lblQuyen.SetBounds(left, lblXinChao.Bottom + 6, 420, 24);

            lblThongTinThem.MaximumSize = new Size(420, 0);
            lblThongTinThem.AutoSize = true;
            lblThongTinThem.Left = left;
            lblThongTinThem.Top = lblQuyen.Bottom + 6;

            int rightLeft = left + 460;
            int rightWidth = Math.Min(500, contentWidth - 460);

            groupBox2.SetBounds(rightLeft, top, rightWidth, 240);

            int passTop = Math.Max(lblThongTinThem.Bottom + 18, groupBox2.Bottom + 18);
            groupBox1.SetBounds(left, passTop, Math.Min(560, contentWidth), 185);

            if (!groupBox2.Visible)
            {
                groupBox1.SetBounds(left, lblThongTinThem.Bottom + 18, Math.Min(560, contentWidth), 185);
            }
        }

        private void ucHoSoCaNhan_Load(object sender, EventArgs e)
        {
            ApplyTheme();

            db = new QLTTDataContext();

            lblXinChao.Text = "Tài khoản: " + Session.Username;
            lblThongTinThem.Text = "";

            if (Session.Role == 1) // ADMIN
            {
                lblQuyen.Text = "Vai trò: Quản trị viên (Admin)";
                lblThongTinThem.Text = "ID Hệ thống: " + Session.IdTaiKhoan +
                                       "\nQuyền hạn: Toàn quyền kiểm soát hệ thống";

                groupBox2.Visible = false;
            }
            else if (Session.Role == 2) // NHÂN VIÊN
            {
                lblQuyen.Text = "Vai trò: Nhân viên";
                groupBox2.Visible = true;

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
                                           "\nChưa có thông tin hồ sơ nhân viên. Vui lòng cập nhật ở ô bên cạnh.";
                }
            }
            else if (Session.Role == 4) // HUẤN LUYỆN VIÊN
            {
                lblQuyen.Text = "Vai trò: Huấn luyện viên";
                groupBox2.Visible = true;

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
            }
            else if (Session.Role == 3) // HỘI VIÊN
            {
                lblQuyen.Text = "Vai trò: Hội viên (Khách hàng)";
                groupBox2.Visible = true;

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
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin mật khẩu!", "Cảnh báo");
                return;
            }

            if (passMoi != xacNhan)
            {
                MessageBox.Show("Mật khẩu xác nhận không khớp!", "Lỗi");
                return;
            }

            string kq = tkBUS.DoiMatKhau(Session.IdTaiKhoan, passCu, passMoi);
            if (kq == "")
            {
                MessageBox.Show("Đổi mật khẩu thành công! Vui lòng ghi nhớ mật khẩu mới.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPassCu.Clear();
                txtPassMoi.Clear();
                txtXacNhanPass.Clear();
            }
            else
            {
                MessageBox.Show(kq, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLuuHoSo_Click(object sender, EventArgs e)
        {
            string kq = tkBUS.CapNhatHoSoCaNhan(
                Session.IdTaiKhoan,
                Session.Role,
                txtHoTen.Text.Trim(),
                txtSDT.Text.Trim(),
                dtpNgaySinh.Value,
                cboGioiTinh.Text);

            if (kq == "")
            {
                MessageBox.Show("Cập nhật thông tin cá nhân thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ucHoSoCaNhan_Load(sender, e);
            }
            else
            {
                MessageBox.Show(kq, "Lỗi cập nhật", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
