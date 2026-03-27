using LTWinforms_CuoiKy_Nhom8.BUS;
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
    public partial class frmQuenMatKhau : Form
    {
        TaiKhoanBUS tkBUS = new TaiKhoanBUS();
        public frmQuenMatKhau()
        {
            InitializeComponent();
        }

        private void btnLayLaiMatKhau_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string sdt = txtSoDienThoai.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(sdt))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Username và Số điện thoại!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string passMoi = "";
            string kq = tkBUS.QuenMatKhau(username, sdt, out passMoi);

            if (kq == "")
            {
                MessageBox.Show($"Xác thực thành công!\n\nMật khẩu mới của bạn là: {passMoi}\n\nVui lòng đăng nhập và đổi lại mật khẩu nhé!",
                                "Thông báo hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close(); 
            }
            else
            {
                MessageBox.Show(kq, "Lỗi bảo mật", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
