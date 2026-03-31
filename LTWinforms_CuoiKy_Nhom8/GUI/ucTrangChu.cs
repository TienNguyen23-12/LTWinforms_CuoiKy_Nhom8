using LTWinforms_CuoiKy_Nhom8.DTO;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace LTWinforms_CuoiKy_Nhom8.GUI
{
    public partial class ucTrangChu : UserControl
    {
        private Button currentActiveButton;
        private PictureBox picBrandLogo;

        public ucTrangChu()
        {
            InitializeComponent();
        }

        public void TaiChucNang(UserControl uc)
        {
            pnlNoiDungChinh.SuspendLayout();
            pnlNoiDungChinh.Controls.Clear();

            Panel pnlWrapper = new Panel();
            pnlWrapper.Dock = DockStyle.Fill;
            pnlWrapper.Padding = new Padding(14);
            pnlWrapper.BackColor = Color.FromArgb(238, 242, 247);

            Panel pnlSurface = new Panel();
            pnlSurface.Dock = DockStyle.Fill;
            pnlSurface.BackColor = Color.White;
            pnlSurface.BorderStyle = BorderStyle.FixedSingle;

            uc.Dock = DockStyle.Fill;
            pnlSurface.Controls.Add(uc);
            pnlWrapper.Controls.Add(pnlSurface);

            pnlNoiDungChinh.Controls.Add(pnlWrapper);
            pnlNoiDungChinh.ResumeLayout();
        }

        private void OpenModule(UserControl uc, Button menuButton, string pageTitle)
        {
            SetActiveMenuButton(menuButton);
            lblModuleTitle.Text = pageTitle;
            TaiChucNang(uc);

            if (frmMainContainer.Instance != null)
            {
                frmMainContainer.Instance.Text = "SPORTIFY - " + pageTitle;
            }
        }

        private void SetActiveMenuButton(Button activeButton)
        {
            if (currentActiveButton != null)
            {
                ApplyDefaultMenuButtonColor(currentActiveButton);
            }

            currentActiveButton = activeButton;

            if (currentActiveButton != null)
            {
                if (currentActiveButton == btnDangXuat)
                {
                    currentActiveButton.BackColor = Color.FromArgb(220, 73, 73);
                    currentActiveButton.ForeColor = Color.White;
                }
                else
                {
                    currentActiveButton.BackColor = Color.FromArgb(72, 157, 242);
                    currentActiveButton.ForeColor = Color.White;
                }
            }
        }

        private void ApplyDefaultMenuButtonColor(Button button)
        {
            if (button == btnDangXuat)
            {
                button.BackColor = Color.FromArgb(190, 58, 58);
                button.ForeColor = Color.White;
                button.FlatAppearance.MouseOverBackColor = Color.FromArgb(214, 78, 78);
                button.FlatAppearance.MouseDownBackColor = Color.FromArgb(166, 46, 46);
            }
            else
            {
                button.BackColor = Color.FromArgb(58, 82, 112);
                button.ForeColor = Color.Gainsboro;
                button.FlatAppearance.MouseOverBackColor = Color.FromArgb(66, 96, 132);
                button.FlatAppearance.MouseDownBackColor = Color.FromArgb(72, 157, 242);
            }
        }

        private void ApplyMenuButtonStyle(Button button)
        {
            button.Height = 40;
            button.Margin = new Padding(10, 6, 10, 0);
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;

            button.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            button.Cursor = Cursors.Hand;
            button.AutoEllipsis = true;
            button.UseVisualStyleBackColor = false;

            AlignSidebarButtonContent(button);
            ApplyDefaultMenuButtonColor(button);

            button.SizeChanged -= MenuButton_SizeChanged;
            button.SizeChanged += MenuButton_SizeChanged;
            ApplyRoundedButtonRegion(button, 8);
        }

        private void AlignSidebarButtonContent(Button button)
        {
            button.TextAlign = ContentAlignment.MiddleLeft;
            button.ImageAlign = ContentAlignment.MiddleLeft;
            button.TextImageRelation = TextImageRelation.ImageBeforeText;
            button.Padding = new Padding(14, 0, 8, 0);
        }

        private void ApplyMenuStyle()
        {
            ApplyMenuButtonStyle(btnQuanTriHeThong);
            ApplyMenuButtonStyle(btnChamCong);
            ApplyMenuButtonStyle(btnThongKeDoanhThu);
            ApplyMenuButtonStyle(btnQuanLySanPham);
            ApplyMenuButtonStyle(btnQuanLyHoiVien);
            ApplyMenuButtonStyle(btnQuanLyGoiTap);
            ApplyMenuButtonStyle(btnQuanLyLopHoc);
            ApplyMenuButtonStyle(btnBanVeThuNgan);
            ApplyMenuButtonStyle(btnTinNhan);
            ApplyMenuButtonStyle(btnDangKyDichVu);
            ApplyMenuButtonStyle(btnLichSuGiaoDich);
            ApplyMenuButtonStyle(btnLuongThuong);
            ApplyMenuButtonStyle(btnLichHoc);
            ApplyMenuButtonStyle(btnQuanLyPhongTap);
            ApplyMenuButtonStyle(btnHoSoCaNhan);
            ApplyMenuButtonStyle(btnDangXuat);
        }

        private void ConfigureMenuLayout()
        {
            flowMenuMain.WrapContents = false;
            flowMenuMain.FlowDirection = FlowDirection.TopDown;
            flowMenuMain.AutoScroll = true;
            flowMenuMain.Padding = new Padding(0, 12, 0, 8);
            flowMenuMain.HorizontalScroll.Enabled = false;
            flowMenuMain.HorizontalScroll.Visible = false;

            flowMenuBottom.WrapContents = false;
            flowMenuBottom.FlowDirection = FlowDirection.TopDown;
            flowMenuBottom.AutoScroll = false;
            flowMenuBottom.Padding = new Padding(0, 8, 0, 8);

            // Không BringToFront để tránh layout chồng khi dock
            // flowMenuBottom.BringToFront();
            // panelMenuTop.BringToFront();

            flowMenuMain.SizeChanged += MenuArea_SizeChanged;
            flowMenuBottom.SizeChanged += MenuArea_SizeChanged;
            pnlThanhMenu.SizeChanged += MenuArea_SizeChanged;

            UpdateBottomMenuHeight();
            FixFirstVisibleMainButtonMargin();
            ResizeSidebarButtons();
        }

        private void MenuArea_SizeChanged(object sender, EventArgs e)
        {
            UpdateBottomMenuHeight();
            FixFirstVisibleMainButtonMargin();
            ResizeSidebarButtons();
        }

        private void ResizeSidebarButtons()
        {
            UpdateBottomMenuHeight();

            int mainWidth = Math.Max(150, flowMenuMain.ClientSize.Width - 24);
            int bottomWidth = Math.Max(150, flowMenuBottom.ClientSize.Width - 24);

            ResizeButton(btnQuanTriHeThong, mainWidth);
            ResizeButton(btnChamCong, mainWidth);
            ResizeButton(btnThongKeDoanhThu, mainWidth);
            ResizeButton(btnQuanLySanPham, mainWidth);
            ResizeButton(btnQuanLyHoiVien, mainWidth);
            ResizeButton(btnQuanLyGoiTap, mainWidth);
            ResizeButton(btnQuanLyLopHoc, mainWidth);
            ResizeButton(btnBanVeThuNgan, mainWidth);
            ResizeButton(btnTinNhan, mainWidth);
            ResizeButton(btnDangKyDichVu, mainWidth);
            ResizeButton(btnLichSuGiaoDich, mainWidth);
            ResizeButton(btnLuongThuong, mainWidth);

            ResizeButton(btnLichHoc, bottomWidth);
            ResizeButton(btnQuanLyPhongTap, bottomWidth);
            ResizeButton(btnHoSoCaNhan, bottomWidth);
            ResizeButton(btnDangXuat, bottomWidth);
        }

        private void ResizeButton(Button button, int width)
        {
            button.Width = width;
        }

        private void LoadWelcomeScreen()
        {
            pnlNoiDungChinh.Controls.Clear();

            Panel pnlWrapper = new Panel();
            pnlWrapper.Dock = DockStyle.Fill;
            pnlWrapper.Padding = new Padding(14);
            pnlWrapper.BackColor = Color.FromArgb(238, 242, 247);

            Panel pnlSurface = new Panel();
            pnlSurface.Dock = DockStyle.Fill;
            pnlSurface.BackColor = Color.White;
            pnlSurface.BorderStyle = BorderStyle.FixedSingle;

            Label lblWelcome = new Label();
            lblWelcome.Dock = DockStyle.Fill;
            lblWelcome.TextAlign = ContentAlignment.MiddleCenter;
            lblWelcome.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblWelcome.ForeColor = Color.FromArgb(44, 62, 80);
            lblWelcome.Text = "Chào mừng đến với SPORTIFY\r\nHãy chọn chức năng từ thanh bên trái.";

            pnlSurface.Controls.Add(lblWelcome);
            pnlWrapper.Controls.Add(pnlSurface);
            pnlNoiDungChinh.Controls.Add(pnlWrapper);

            lblModuleTitle.Text = "Tổng quan";
            SetActiveMenuButton(null);
        }

        private void ucTrangChu_Load(object sender, EventArgs e)
        {
            lblXinChao.Text = "Xin chào: " + Session.Username;

            ApplySidebarTheme();
            ApplyMenuStyle();
            ApplyMenuIcons();
            ConfigureMenuLayout();

            btnQuanTriHeThong.Visible = false;
            btnThongKeDoanhThu.Visible = false;
            btnQuanLyHoiVien.Visible = false;
            btnQuanLyGoiTap.Visible = false;
            btnBanVeThuNgan.Visible = false;
            btnTinNhan.Visible = false;
            btnDangKyDichVu.Visible = false;
            btnLichSuGiaoDich.Visible = false;
            btnLuongThuong.Visible = false;
            btnHoSoCaNhan.Visible = true;
            btnLichHoc.Visible = false;
            btnQuanLyLopHoc.Visible = false;
            btnQuanLyPhongTap.Visible = false;
            btnChamCong.Visible = false;
            btnQuanLySanPham.Visible = false;

            if (Session.Role == 1)
            {
                btnLichHoc.Text = "Lịch toàn Trung tâm";
                btnQuanTriHeThong.Visible = true;
                btnThongKeDoanhThu.Visible = true;
                btnQuanLyHoiVien.Visible = true;
                btnQuanLyGoiTap.Visible = true;
                btnBanVeThuNgan.Visible = true;
                btnTinNhan.Visible = true;
                btnLichHoc.Visible = true;
                btnQuanLyLopHoc.Visible = true;
                btnQuanLyPhongTap.Visible = true;
                btnChamCong.Visible = true;
                btnQuanLySanPham.Visible = true;
            }
            else if (Session.Role == 2)
            {
                btnLichHoc.Text = "Lịch toàn Trung tâm";
                btnQuanLyHoiVien.Visible = true;
                btnBanVeThuNgan.Visible = true;
                btnTinNhan.Visible = true;
                btnLichHoc.Visible = true;
                btnLuongThuong.Visible = true;
                btnQuanLySanPham.Visible = true;
            }
            else if (Session.Role == 3)
            {
                btnLichHoc.Text = "Lịch học của tôi";
                btnDangKyDichVu.Text = "Đăng ký lớp";
                btnLichHoc.Visible = true;
                btnDangKyDichVu.Visible = true;
                btnLichSuGiaoDich.Visible = true;
                btnTinNhan.Visible = true;
            }
            else if (Session.Role == 4)
            {
                btnLichHoc.Text = "Lịch dạy & Điểm danh";
                btnDangKyDichVu.Text = "Đăng ký nhận lớp";
                btnLichHoc.Visible = true;
                btnDangKyDichVu.Visible = true;
                btnLuongThuong.Visible = true;
                btnTinNhan.Visible = true;
            }

            UpdateBottomMenuHeight();
            ResizeSidebarButtons();
            LoadWelcomeScreen();
        }

        private void btnQuanTriHeThong_Click(object sender, EventArgs e)
        {
            OpenModule(new ucQuanTriHeThong(), btnQuanTriHeThong, "Quản trị hệ thống");
        }

        private void btnQuanLyHoiVien_Click(object sender, EventArgs e)
        {
            OpenModule(new ucQuanLyHoiVien(), btnQuanLyHoiVien, "Quản lý hội viên");
        }

        private void btnQuanLyGoiTap_Click(object sender, EventArgs e)
        {
            OpenModule(new ucQuanLyGoiTap(), btnQuanLyGoiTap, "Quản lý gói tập");
        }

        private void btnBanVeThuNgan_Click(object sender, EventArgs e)
        {
            OpenModule(new ucBanVeThuNgan(), btnBanVeThuNgan, "Bán vé - Thu ngân");
        }

        private void btnHoSoCaNhan_Click(object sender, EventArgs e)
        {
            OpenModule(new ucHoSoCaNhan(), btnHoSoCaNhan, "Hồ sơ cá nhân");
        }

        private void btnThongKeDoanhThu_Click(object sender, EventArgs e)
        {
            OpenModule(new ucThongKeDoanhThu(), btnThongKeDoanhThu, "Thống kê doanh thu");
        }

        private void btnQuanLyLopHoc_Click(object sender, EventArgs e)
        {
            OpenModule(new ucQuanLyLopHoc(), btnQuanLyLopHoc, "Quản lý lớp học");
        }

        private void btnTinNhan_Click(object sender, EventArgs e)
        {
            OpenModule(new ucTinNhan(), btnTinNhan, "Tin nhắn");
        }

        private void btnDangKyDichVu_Click(object sender, EventArgs e)
        {
            if (Session.Role == 3)
            {
                OpenModule(new ucDichVuHoiVien(), btnDangKyDichVu, "Đăng ký lớp");
            }
            else if (Session.Role == 4)
            {
                OpenModule(new ucDangKyDay(), btnDangKyDichVu, "Đăng ký nhận lớp");
            }
        }

        private void btnLichSuGiaoDich_Click(object sender, EventArgs e)
        {
            OpenModule(new ucLichSuGiaoDich(), btnLichSuGiaoDich, "Lịch sử giao dịch");
        }

        private void btnLuongThuong_Click(object sender, EventArgs e)
        {
            OpenModule(new ucLuongThuong(), btnLuongThuong, "Lương thưởng");
        }

        private void btnLichHoc_Click(object sender, EventArgs e)
        {
            OpenModule(new ucLichHoc(), btnLichHoc, btnLichHoc.Text);
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            DialogResult xacNhan = ModernMessageBox.Show(
                "Bạn có chắc chắn muốn đăng xuất khỏi hệ thống?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                ModernMessageType.Question);

            if (xacNhan == DialogResult.Yes)
            {
                Session.IdTaiKhoan = 0;
                Session.Username = null;
                Session.Role = 0;

                frmMainContainer.Instance.LoadUserControl(new ucDangNhap());
            }
        }

        private void btnQuanLyPhongTap_Click(object sender, EventArgs e)
        {
            OpenModule(new ucQuanLyPhongTap(), btnQuanLyPhongTap, "Quản lý phòng tập");
        }

        private void btnChamCong_Click(object sender, EventArgs e)
        {
            OpenModule(new ucQuanLyNhanSu(), btnChamCong, "Chấm công");
        }

        private void btnQuanLySanPham_Click(object sender, EventArgs e)
        {
            OpenModule(new ucQuanLySanPham(), btnQuanLySanPham, "Quản lý sản phẩm");
        }

        private void lblXinChao_Click(object sender, EventArgs e)
        {
        }

        private int CountVisibleButtons(params Button[] buttons)
        {
            int count = 0;
            foreach (Button button in buttons)
            {
                if (button.Visible)
                {
                    count++;
                }
            }

            return count;
        }

        private void UpdateBottomMenuHeight()
        {
            int visibleCount = CountVisibleButtons(btnLichHoc, btnQuanLyPhongTap, btnHoSoCaNhan, btnDangXuat);

            if (visibleCount == 0)
            {
                flowMenuBottom.Height = 0;
                return;
            }

            int buttonHeight = 38;
            int topMargin = 6;
            int verticalPadding = 16; // flowMenuBottom.Padding.Top + Bottom
            int reserve = 8;

            int requiredHeight = (visibleCount * buttonHeight) + (visibleCount * topMargin) + verticalPadding + reserve;
            flowMenuBottom.Height = requiredHeight;
        }

        private void FixFirstVisibleMainButtonMargin()
        {
            bool foundFirstVisible = false;

            foreach (Control control in flowMenuMain.Controls)
            {
                Button button = control as Button;
                if (button == null)
                {
                    continue;
                }

                if (!button.Visible)
                {
                    continue;
                }

                if (!foundFirstVisible)
                {
                    button.Margin = new Padding(10, 12, 10, 0); // nút đầu có top margin lớn hơn
                    foundFirstVisible = true;
                }
                else
                {
                    button.Margin = new Padding(10, 6, 10, 0);
                }
            }
        }

        private void ApplyMenuIcons()
        {
            ApplyMenuIcon(btnQuanTriHeThong, "quantri");
            ApplyMenuIcon(btnChamCong, "chamcong");
            ApplyMenuIcon(btnThongKeDoanhThu, "thongke");
            ApplyMenuIcon(btnQuanLySanPham, "sanpham");
            ApplyMenuIcon(btnQuanLyHoiVien, "hoivien");
            ApplyMenuIcon(btnQuanLyGoiTap, "goitap");
            ApplyMenuIcon(btnQuanLyLopHoc, "lophoc");
            ApplyMenuIcon(btnBanVeThuNgan, "banve");
            ApplyMenuIcon(btnTinNhan, "tinnhan");
            ApplyMenuIcon(btnDangKyDichVu, "dichvu");
            ApplyMenuIcon(btnLichSuGiaoDich, "giaodich");
            ApplyMenuIcon(btnLuongThuong, "luongthuong");
            ApplyMenuIcon(btnLichHoc, "lichhoc");
            ApplyMenuIcon(btnQuanLyPhongTap, "phongtap");
            ApplyMenuIcon(btnHoSoCaNhan, "hoso");
            ApplyMenuIcon(btnDangXuat, "dangxuat");
        }

        private void ApplyMenuIcon(Button button, string key)
        {
            if (button.Image != null)
            {
                button.Image.Dispose();
                button.Image = null;
            }

            using (Bitmap source = GetMenuIconSource(key))
            {
                if (source != null)
                {
                    button.Image = CreateWhiteMenuIcon(source, 16, 8);
                }
            }

            AlignSidebarButtonContent(button);
        }

        private Bitmap GetMenuIconSource(string key)
        {
            // Ưu tiên file thật trong Assets/MenuIcons trước
            Bitmap fromAssets = GetMenuIconFromAssets(key);
            if (fromAssets != null)
            {
                return fromAssets;
            }

            // Fallback Resources nếu không tìm thấy file
            return GetMenuIconFromResources(key);
        }

        private Bitmap GetMenuIconFromResources(string key)
        {
            // Ưu tiên strongly-typed Resources
            Bitmap bmp = null;

            if (key == "quantri") bmp = global::LTWinforms_CuoiKy_Nhom8.Properties.Resources.quantri;
            else if (key == "chamcong") bmp = global::LTWinforms_CuoiKy_Nhom8.Properties.Resources.chamcong;
            else if (key == "thongke") bmp = global::LTWinforms_CuoiKy_Nhom8.Properties.Resources.thongke;
            else if (key == "sanpham") bmp = global::LTWinforms_CuoiKy_Nhom8.Properties.Resources.sanpham;
            else if (key == "hoivien") bmp = global::LTWinforms_CuoiKy_Nhom8.Properties.Resources.hoivien;
            else if (key == "goitap") bmp = global::LTWinforms_CuoiKy_Nhom8.Properties.Resources.goitap;
            else if (key == "lophoc") bmp = global::LTWinforms_CuoiKy_Nhom8.Properties.Resources.lophoc;
            else if (key == "banve") bmp = global::LTWinforms_CuoiKy_Nhom8.Properties.Resources.banve;
            else if (key == "tinnhan") bmp = global::LTWinforms_CuoiKy_Nhom8.Properties.Resources.tinnhan;
            else if (key == "dichvu") bmp = global::LTWinforms_CuoiKy_Nhom8.Properties.Resources.dichvu;
            else if (key == "giaodich") bmp = global::LTWinforms_CuoiKy_Nhom8.Properties.Resources.giaodich;
            else if (key == "luongthuong") bmp = global::LTWinforms_CuoiKy_Nhom8.Properties.Resources.luongthuong;
            else if (key == "lichhoc") bmp = global::LTWinforms_CuoiKy_Nhom8.Properties.Resources.lichhoc;
            else if (key == "phongtap") bmp = global::LTWinforms_CuoiKy_Nhom8.Properties.Resources.phongtap;
            else if (key == "hoso") bmp = global::LTWinforms_CuoiKy_Nhom8.Properties.Resources.hoso;
            else if (key == "dangxuat") bmp = global::LTWinforms_CuoiKy_Nhom8.Properties.Resources.dangxuat;

            return bmp != null ? new Bitmap(bmp) : null;
        }

        private Bitmap GetMenuIconFromAssets(string key)
        {
            // Runtime thường ở ...\bin\Debug hoặc ...\bin\Release
            string[] candidateFolders =
            {
                Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\..\Assets\MenuIcons")),
                Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\Assets\MenuIcons")),
                Path.GetFullPath(Path.Combine(Application.StartupPath, @"Assets\MenuIcons"))
            };

            foreach (string folder in candidateFolders)
            {
                if (!Directory.Exists(folder))
                {
                    continue;
                }

                string filePath = Path.Combine(folder, key + ".png");
                if (!File.Exists(filePath))
                {
                    continue;
                }

                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (Image img = Image.FromStream(fs))
                {
                    return new Bitmap(img);
                }
            }

            return null;
        }

        private Bitmap CreateWhiteMenuIcon(Bitmap source, int iconSize, int gap)
        {
            Bitmap resized = new Bitmap(source, new Size(iconSize, iconSize));
            Bitmap whiteIcon = new Bitmap(iconSize + gap, iconSize);

            for (int y = 0; y < iconSize; y++)
            {
                for (int x = 0; x < iconSize; x++)
                {
                    Color px = resized.GetPixel(x, y);
                    whiteIcon.SetPixel(x, y, Color.FromArgb(px.A, Color.White));
                }
            }

            resized.Dispose();
            return whiteIcon;
        }

        private void ApplySidebarTheme()
        {
            pnlThanhMenu.BackColor = Color.FromArgb(46, 68, 96);
            panelMenuTop.BackColor = Color.FromArgb(66, 96, 132);

            flowMenuMain.BackColor = pnlThanhMenu.BackColor;
            flowMenuBottom.BackColor = pnlThanhMenu.BackColor;

            lblBrand.Text = "SPORTIFY";
            lblBrand.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblBrand.ForeColor = Color.White;
            lblBrand.Location = new Point(22, 12);

            lblXinChao.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblXinChao.ForeColor = Color.FromArgb(232, 238, 246);
            lblXinChao.Location = new Point(22, 50);

            // Bỏ icon logo cạnh SPORTIFY:
            // Không gọi EnsureBrandLogo()
        }

        private void EnsureBrandLogo()
        {
            if (picBrandLogo == null)
            {
                picBrandLogo = new PictureBox();
                picBrandLogo.Name = "picBrandLogo";
                picBrandLogo.Size = new Size(28, 28);
                picBrandLogo.Location = new Point(22, 10);
                picBrandLogo.SizeMode = PictureBoxSizeMode.Zoom;
                picBrandLogo.BackColor = Color.Transparent;
                panelMenuTop.Controls.Add(picBrandLogo);
            }

            if (picBrandLogo.Image != null)
            {
                picBrandLogo.Image.Dispose();
                picBrandLogo.Image = null;
            }

            Bitmap logoBitmap = null;

            object logoResource = Properties.Resources.ResourceManager.GetObject("logo");
            if (logoResource is Bitmap bmp)
            {
                logoBitmap = new Bitmap(bmp, new Size(24, 24));
            }
            else if (frmMainContainer.Instance != null && frmMainContainer.Instance.Icon != null)
            {
                logoBitmap = new Bitmap(frmMainContainer.Instance.Icon.ToBitmap(), new Size(24, 24));
            }

            picBrandLogo.Image = logoBitmap;
            picBrandLogo.BringToFront();
            lblBrand.BringToFront();
            lblXinChao.BringToFront();
        }

        private void MenuButton_SizeChanged(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                ModernTheme.ApplyRoundedButtons(button, 8);
            }
        }

        private void ApplyRoundedButtonRegion(Button button, int radius)
        {
            ModernTheme.ApplyRoundedButtons(button, radius);
        }
    }
}