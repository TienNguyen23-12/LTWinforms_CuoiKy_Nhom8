using LTWinforms_CuoiKy_Nhom8.BUS;
using LTWinforms_CuoiKy_Nhom8.DTO;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace LTWinforms_CuoiKy_Nhom8.GUI
{
    public partial class ucDangNhap : UserControl
    {
        private readonly TaiKhoanBUS tk = new TaiKhoanBUS();
        private bool isUiApplied;

        private Panel pnlLogo;
        private Label lblLogo;
        private Label lblSubTitle;
        private Panel lineUser;
        private Panel linePass;

        public ucDangNhap()
        {
            InitializeComponent();
        }

        private void ucDangNhap_Load(object sender, EventArgs e)
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
            LayoutLoginCard();

            pnlCard.SizeChanged += delegate { LayoutLoginCard(); };

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

            lblTitle.Text = "Welcome!";
            lblTitle.Font = new Font("Segoe UI", 21F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(92, 100, 226);
            lblTitle.AutoSize = true;

            lblSubTitle.Text = "Đăng nhập người dùng";
            lblSubTitle.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            lblSubTitle.ForeColor = Color.FromArgb(145, 153, 179);
            lblSubTitle.AutoSize = true;

            label1.Text = "Tên đăng nhập";
            label2.Text = "Mật khẩu";
            label1.ForeColor = Color.FromArgb(140, 146, 165);
            label2.ForeColor = Color.FromArgb(140, 146, 165);
            label1.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            label2.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);

            txtUsername.BorderStyle = BorderStyle.None;
            txtPassword.BorderStyle = BorderStyle.None;
            txtUsername.BackColor = pnlCard.BackColor;
            txtPassword.BackColor = pnlCard.BackColor;
            txtUsername.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            txtPassword.Font = new Font("Segoe UI", 10F, FontStyle.Regular);

            lineUser.BackColor = Color.FromArgb(198, 202, 215);
            linePass.BackColor = Color.FromArgb(198, 202, 215);

            ModernTheme.StyleButton(btnDangNhap, Color.FromArgb(39, 174, 245), Color.White);
            btnDangNhap.Text = "Đăng nhập";

            btnThoat.Visible = false;

            linkQuenMatKhau.Text = "Quên mật khẩu";
            linkLabelDangKy.Text = "Đăng ký";
            linkQuenMatKhau.LinkColor = Color.FromArgb(145, 153, 179);
            linkLabelDangKy.LinkColor = Color.FromArgb(145, 153, 179);
            linkQuenMatKhau.ActiveLinkColor = Color.FromArgb(92, 100, 226);
            linkLabelDangKy.ActiveLinkColor = Color.FromArgb(92, 100, 226);
        }

        private void ApplyBrandLogo()
        {
            Image logo = LoadBrandLogo(40, 40);
            if (logo != null)
            {
                if (pnlLogo.BackgroundImage != null)
                {
                    pnlLogo.BackgroundImage.Dispose();
                }

                pnlLogo.BackgroundImage = logo;
                pnlLogo.BackgroundImageLayout = ImageLayout.Center;
                lblLogo.Visible = false;
            }
            else
            {
                lblLogo.Visible = true;
                lblLogo.Text = "Logo";
            }
        }

        private Image LoadBrandLogo(int w, int h)
        {
            object res = Properties.Resources.ResourceManager.GetObject("logo");
            Bitmap bmp = res as Bitmap;
            if (bmp != null)
            {
                return new Bitmap(bmp, new Size(w, h));
            }

            string[] paths =
            {
                Path.Combine(Application.StartupPath, "Assets", "logo.ico"),
                Path.Combine(Application.StartupPath, "Resources", "logo.ico")
            };

            foreach (string path in paths)
            {
                if (File.Exists(path))
                {
                    using (Icon ico = new Icon(path))
                    {
                        return new Bitmap(ico.ToBitmap(), new Size(w, h));
                    }
                }
            }

            return null;
        }

        private void LayoutLoginCard()
        {
            int x = 28;
            int w = pnlCard.Width - (x * 2);
            int y = 30;

            lblTitle.Location = new Point(x, y);
            lblSubTitle.Location = new Point(x, lblTitle.Bottom + 1);

            label1.Location = new Point(x, lblSubTitle.Bottom + 30);
            txtUsername.SetBounds(x, label1.Bottom + 3, w, 24);
            lineUser.SetBounds(x, txtUsername.Bottom + 2, w, 1);

            label2.Location = new Point(x, lineUser.Bottom + 12);
            txtPassword.SetBounds(x, label2.Bottom + 3, w, 24);
            linePass.SetBounds(x, txtPassword.Bottom + 2, w, 1);

            btnDangNhap.SetBounds((pnlCard.Width - 90) / 2, linePass.Bottom + 16, 90, 30);

            linkQuenMatKhau.Location = new Point(x, pnlCard.Height - 28);
            linkLabelDangKy.Location = new Point(pnlCard.Width - 28 - linkLabelDangKy.PreferredWidth, pnlCard.Height - 28);
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ModernMessageBox.Show("Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu.", "Thông báo", ModernMessageType.Warning);
                return;
            }

            string ketQua = tk.KiemTraDangNhap(username, password);

            if (ketQua == "")
            {
                ModernMessageBox.Show("Đăng nhập thành công.", "Thành công", ModernMessageType.Success);
                frmMainContainer.Instance.LoadUserControl(new ucTrangChu());
            }
            else
            {
                ModernMessageBox.Show(ketQua, "Lỗi đăng nhập", ModernMessageType.Error);
                txtPassword.Clear();
                txtPassword.Focus();
            }
        }

        private void linkLabelDangKy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmMainContainer.Instance.LoadUserControl(new ucDangKy());
        }

        private void linkQuenMatKhau_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (frmQuenMatKhau f = new frmQuenMatKhau())
            {
                f.ShowDialog();
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult tb = ModernMessageBox.Show("Bạn có muốn thoát không?", "Xác nhận", MessageBoxButtons.YesNo, ModernMessageType.Question);
            if (tb == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
