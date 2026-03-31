using LTWinforms_CuoiKy_Nhom8.BUS;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace LTWinforms_CuoiKy_Nhom8.GUI
{
    public partial class ucDangKy : UserControl
    {
        private readonly TaiKhoanBUS tk = new TaiKhoanBUS();
        private bool isUiApplied;

        private Label lblSubTitle;
        private Panel lineUser;
        private Panel linePass;
        private Panel lineConfirm;

        public ucDangKy()
        {
            InitializeComponent();
        }

        private void ucDangKy_Load(object sender, EventArgs e)
        {
            ApplyModernUi();
            txtUsername.Focus();
        }

        private void ApplyModernUi()
        {
            if (isUiApplied)
            {
                return;
            }

            BackColor = Color.FromArgb(221, 226, 243);

            pnlCard.BackColor = Color.FromArgb(241, 243, 249);
            pnlCard.BorderStyle = BorderStyle.None;
            ModernTheme.EnableAutoRounded(pnlCard, 24);

            EnsureVisualNodes();
            LayoutRegisterCard();

            pnlCard.SizeChanged += delegate { LayoutRegisterCard(); };

            isUiApplied = true;
        }

        private void EnsureVisualNodes()
        {
            if (lblSubTitle == null)
            {
                lblSubTitle = new Label();
                pnlCard.Controls.Add(lblSubTitle);
            }

            if (lineUser == null)
            {
                lineUser = new Panel();
                pnlCard.Controls.Add(lineUser);
            }

            if (linePass == null)
            {
                linePass = new Panel();
                pnlCard.Controls.Add(linePass);
            }

            if (lineConfirm == null)
            {
                lineConfirm = new Panel();
                pnlCard.Controls.Add(lineConfirm);
            }

            lblTitle.Text = "SPORTIFY!";
            lblTitle.Font = new Font("Segoe UI", 21F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(92, 100, 226);
            lblTitle.AutoSize = true;

            lblSubTitle.Text = "Đăng ký người dùng";
            lblSubTitle.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            lblSubTitle.ForeColor = Color.FromArgb(145, 153, 179);
            lblSubTitle.AutoSize = true;

            label1.Text = "Tên đăng nhập";
            label2.Text = "Mật khẩu";
            label3.Text = "Xác nhận mật khẩu";

            label1.ForeColor = Color.FromArgb(140, 146, 165);
            label2.ForeColor = Color.FromArgb(140, 146, 165);
            label3.ForeColor = Color.FromArgb(140, 146, 165);

            label1.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            label2.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            label3.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);

            txtUsername.BorderStyle = BorderStyle.None;
            txtPassword.BorderStyle = BorderStyle.None;
            txtConfirmPass.BorderStyle = BorderStyle.None;

            txtUsername.BackColor = pnlCard.BackColor;
            txtPassword.BackColor = pnlCard.BackColor;
            txtConfirmPass.BackColor = pnlCard.BackColor;

            txtUsername.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            txtPassword.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            txtConfirmPass.Font = new Font("Segoe UI", 10F, FontStyle.Regular);

            lineUser.BackColor = Color.FromArgb(198, 202, 215);
            linePass.BackColor = Color.FromArgb(198, 202, 215);
            lineConfirm.BackColor = Color.FromArgb(198, 202, 215);

            ModernTheme.StyleButton(btnDangKy, Color.FromArgb(39, 174, 245), Color.White);
            btnDangKy.Text = "Đăng ký";

            linkLabelDangNhap.Text = "Quay lại đăng nhập";
            linkLabelDangNhap.LinkColor = Color.FromArgb(145, 153, 179);
            linkLabelDangNhap.ActiveLinkColor = Color.FromArgb(92, 100, 226);
        }

        private void LayoutRegisterCard()
        {
            int x = 30;
            int w = pnlCard.Width - (x * 2);
            int y = 30;

            lblTitle.Location = new Point(x, y);
            lblSubTitle.Location = new Point(x, lblTitle.Bottom + 1);

            label1.Location = new Point(x, lblSubTitle.Bottom + 18);
            txtUsername.SetBounds(x, label1.Bottom + 3, w, 24);
            lineUser.SetBounds(x, txtUsername.Bottom + 2, w, 1);

            label2.Location = new Point(x, lineUser.Bottom + 8);
            txtPassword.SetBounds(x, label2.Bottom + 3, w, 24);
            linePass.SetBounds(x, txtPassword.Bottom + 2, w, 1);

            label3.Location = new Point(x, linePass.Bottom + 8);
            txtConfirmPass.SetBounds(x, label3.Bottom + 3, w, 24);
            lineConfirm.SetBounds(x, txtConfirmPass.Bottom + 2, w, 1);

            btnDangKy.SetBounds((pnlCard.Width - 96) / 2, lineConfirm.Bottom + 12, 96, 30);

            int linkBottomMargin = 28;
            linkLabelDangNhap.Location = new Point(
                (pnlCard.Width - linkLabelDangNhap.PreferredWidth) / 2,
                pnlCard.Height - linkBottomMargin);
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            string confirmPass = txtConfirmPass.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPass))
            {
                ModernMessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Cảnh báo", ModernMessageType.Warning);
                return;
            }

            if (password != confirmPass)
            {
                ModernMessageBox.Show("Mật khẩu nhập lại không khớp.", "Lỗi", ModernMessageType.Error);
                return;
            }

            string ketQua = tk.DangKyTaiKhoan(username, password);

            if (ketQua == "")
            {
                ModernMessageBox.Show("Đăng ký tài khoản thành công.", "Thành công", ModernMessageType.Success);
                frmMainContainer.Instance.LoadUserControl(new ucDangNhap());
            }
            else
            {
                ModernMessageBox.Show(ketQua, "Lỗi đăng ký", ModernMessageType.Error);
            }
        }

        private void linkLabelDangNhap_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmMainContainer.Instance.LoadUserControl(new ucDangNhap());
        }
    }
}
