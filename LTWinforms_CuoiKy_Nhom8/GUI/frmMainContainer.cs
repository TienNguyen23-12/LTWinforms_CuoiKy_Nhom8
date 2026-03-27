using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LTWinforms_CuoiKy_Nhom8.GUI
{
    public partial class frmMainContainer : Form
    {
        public static frmMainContainer Instance;

        public frmMainContainer()
        {
            InitializeComponent();
            Instance = this;
        }

        public void LoadUserControl(UserControl uc)
        {
            pnlContent.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(uc);
        }

        private void frmMainContainer_Load(object sender, EventArgs e)
        {
            LoadUserControl(new ucDangNhap());
        }
    }
}
