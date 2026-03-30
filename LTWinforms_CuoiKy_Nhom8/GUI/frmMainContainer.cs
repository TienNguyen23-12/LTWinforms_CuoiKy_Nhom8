using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace LTWinforms_CuoiKy_Nhom8.GUI
{
    public partial class frmMainContainer : Form
    {
        public static frmMainContainer Instance;

        private readonly Size authWindowSize = new Size(760, 520);
        private readonly Font appUiFont = new Font("Segoe UI", 10F, FontStyle.Bold);


        public frmMainContainer()
        {
            InitializeComponent();
            Instance = this;

            var iconPath = Path.Combine(Application.StartupPath, "Assets", "sportify.ico");
            if (File.Exists(iconPath))
            {
                this.Icon = new Icon(iconPath);
            }

            ApplyGlobalFont(this);
        }

        public void LoadUserControl(UserControl uc)
        {
            ConfigureWindow(uc);

            ApplyGlobalFont(uc);

            pnlContent.SuspendLayout();
            pnlContent.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(uc);
            pnlContent.ResumeLayout();
        }

        private void ConfigureWindow(UserControl uc)
        {
            if (uc is ucDangNhap)
            {
                SetAuthWindow("SPORTIFY - Đăng nhập");
                return;
            }

            if (uc is ucDangKy)
            {
                SetAuthWindow("SPORTIFY - Đăng ký");
                return;
            }

            SetMainWindow();
        }

        private void SetAuthWindow(string title)
        {
            if (WindowState == FormWindowState.Maximized)
            {
                WindowState = FormWindowState.Normal;
            }

            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = true;
            StartPosition = FormStartPosition.CenterScreen;
            MinimumSize = authWindowSize;
            MaximumSize = authWindowSize;
            Size = authWindowSize;
            Text = title;
        }

        private void SetMainWindow()
        {
            FormBorderStyle = FormBorderStyle.Sizable;
            MaximizeBox = true;
            MinimizeBox = true;
            MinimumSize = new Size(1024, 640);
            MaximumSize = Size.Empty;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "SPORTIFY - Hệ thống quản lý";
            WindowState = FormWindowState.Maximized;
        }

        private void ApplyGlobalFont(Control root)
        {
            if (root == null)
            {
                return;
            }

            root.Font = appUiFont;

            if (root is DataGridView dgv)
            {
                dgv.DefaultCellStyle.Font = appUiFont;
                dgv.ColumnHeadersDefaultCellStyle.Font = appUiFont;
                dgv.RowHeadersDefaultCellStyle.Font = appUiFont;
            }

            foreach (Control child in root.Controls)
            {
                ApplyGlobalFont(child);
            }
        }

        private void frmMainContainer_Load(object sender, EventArgs e)
        {
            LoadUserControl(new ucDangNhap());
        }
    }
}
