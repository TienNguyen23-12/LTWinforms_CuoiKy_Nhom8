using LTWinforms_CuoiKy_Nhom8.BUS;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace LTWinforms_CuoiKy_Nhom8.GUI
{
    public partial class frmQuenMatKhau : Form
    {
        private readonly TaiKhoanBUS tkBUS = new TaiKhoanBUS();

        private Label lblSubTitle;
        private Panel lineUser;
        private Panel linePhone;

        public frmQuenMatKhau()
        {
            InitializeComponent();
            ApplyAppIcon();
            ApplyModernUi();
        }

        private void ApplyAppIcon()
        {
            try
            {
                string iconPath = Path.Combine(Application.StartupPath, "Assets", "sportify.ico");
                if (File.Exists(iconPath))
                {
                    Icon = new Icon(iconPath);
                }
            }
            catch
            {
            }
        }

        private void ApplyModernUi()
        {
            BackColor = Color.FromArgb(221, 226, 243);

            pnlCard.BackColor = Color.FromArgb(241, 243, 249);
            pnlCard.BorderStyle = BorderStyle.None;
            ModernTheme.EnableAutoRounded(pnlCard, 24);

            EnsureVisualNodes();
            LayoutForgotCard();

            pnlCard.SizeChanged += delegate { LayoutForgotCard(); };
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

            if (linePhone == null)
            {
                linePhone = new Panel();
                pnlCard.Controls.Add(linePhone);
            }

            lblTitle.Text = "Quên mật khẩu";
            lblTitle.Font = new Font("Segoe UI", 19F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(92, 100, 226);
            lblTitle.AutoSize = true;

            lblSubTitle.Text = "Khôi phục tài khoản";
            lblSubTitle.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            lblSubTitle.ForeColor = Color.FromArgb(145, 153, 179);
            lblSubTitle.AutoSize = true;

            label1.Text = "Email";
            label2.Text = "Số điện thoại";
            label1.ForeColor = Color.FromArgb(140, 146, 165);
            label2.ForeColor = Color.FromArgb(140, 146, 165);
            label1.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            label2.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);

            txtUsername.BorderStyle = BorderStyle.None;
            txtSoDienThoai.BorderStyle = BorderStyle.None;
            txtUsername.BackColor = pnlCard.BackColor;
            txtSoDienThoai.BackColor = pnlCard.BackColor;
            txtUsername.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            txtSoDienThoai.Font = new Font("Segoe UI", 10F, FontStyle.Regular);

            lineUser.BackColor = Color.FromArgb(198, 202, 215);
            linePhone.BackColor = Color.FromArgb(198, 202, 215);

            ModernTheme.StyleButton(btnLayLaiMatKhau, Color.FromArgb(39, 174, 245), Color.White);
            btnLayLaiMatKhau.Text = "Gửi";
        }

        private void LayoutForgotCard()
        {
            int left = 28;
            int width = pnlCard.Width - (left * 2);
            int y = 30;

            int inputHeight = txtUsername.PreferredHeight + 2;
            int buttonWidth = 90;
            int buttonHeight = 30;
            int bottomPadding = 16;

            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(left, y);
            y = lblTitle.Bottom + 2;

            lblSubTitle.AutoSize = true;
            lblSubTitle.Location = new Point(left, y);
            y = lblSubTitle.Bottom + 12;

            label1.AutoSize = true;
            label1.Location = new Point(left, y);
            y = label1.Bottom + 2;

            txtUsername.SetBounds(left, y, width, inputHeight);
            y = txtUsername.Bottom + 1;

            lineUser.SetBounds(left, y, width, 1);
            y = lineUser.Bottom + 8;

            label2.AutoSize = true;
            label2.Location = new Point(left, y);
            y = label2.Bottom + 2;

            txtSoDienThoai.SetBounds(left, y, width, inputHeight);
            y = txtSoDienThoai.Bottom + 1;

            linePhone.SetBounds(left, y, width, 1);

            int buttonY = linePhone.Bottom + 12;
            int maxButtonY = pnlCard.Height - bottomPadding - buttonHeight;
            if (buttonY > maxButtonY)
            {
                buttonY = maxButtonY;
            }

            btnLayLaiMatKhau.SetBounds((pnlCard.Width - buttonWidth) / 2, buttonY, buttonWidth, buttonHeight);
        }

        private void btnLayLaiMatKhau_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string sdt = txtSoDienThoai.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(sdt))
            {
                ModernMessageBox.Show("Vui lòng nhập đầy đủ tên đăng nhập và số điện thoại.", "Cảnh báo", ModernMessageType.Warning);
                return;
            }

            string passMoi;
            string kq = tkBUS.QuenMatKhau(username, sdt, out passMoi);

            if (kq == "")
            {
                ModernMessageBox.Show("Xác thực thành công.\nMật khẩu mới: " + passMoi, "Thông báo", ModernMessageType.Success);
                Close();
            }
            else
            {
                ModernMessageBox.Show(kq, "Lỗi bảo mật", ModernMessageType.Error);
            }
        }
    }
}
