using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LTWinforms_CuoiKy_Nhom8.GUI
{
    public partial class frmCR_BaoCao : Form
    {
        public frmCR_BaoCao()
        {
            InitializeComponent();

            var iconPath = Path.Combine(Application.StartupPath, "Assets", "sportify.ico");
            if (File.Exists(iconPath))
            {
                Icon = new Icon(iconPath);
            }
        }

        public void HienThiBaoCao(object rpt)
        {
            crvBaoCao.ReportSource = rpt;
            crvBaoCao.Refresh();
        }

        private void frmCR_BaoCao_Load(object sender, EventArgs e)
        {

        }
    }
}
