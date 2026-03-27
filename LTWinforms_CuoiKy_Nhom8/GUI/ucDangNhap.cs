using LTWinforms_CuoiKy_Nhom8.BUS;
using LTWinforms_CuoiKy_Nhom8.DTO;
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
    public partial class ucDangNhap : UserControl
    {
        TaiKhoanBUS tk = new TaiKhoanBUS();

        public ucDangNhap()
        {
            InitializeComponent();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Tên đăng nhập và Mật khẩu!", "Thông báo",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string ketQua = tk.KiemTraDangNhap(username, password);

            if (ketQua == "")
            {
                MessageBox.Show($"Đăng nhập thành công!\nXin chào: {Session.Username}\nQuyền của bạn là: Role {Session.Role}",
                                "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                frmMainContainer.Instance.LoadUserControl(new ucTrangChu());
                // TODO: Chút nữa chúng ta sẽ viết code mở MainForm và phân quyền ở đây
                // Mở MainForm...
                // this.Hide(); // Ẩn form đăng nhập đi
            }
            else
            {
                MessageBox.Show(ketQua, "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            frmQuenMatKhau f = new frmQuenMatKhau();
            f.ShowDialog();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult tb;
            tb = (MessageBox.Show("Bạn có muốn thoát không?", "Chú ý",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning));
            if (tb == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void ucDangNhap_Load(object sender, EventArgs e)
        {
            txtUsername.Focus();
        }
    }
}
