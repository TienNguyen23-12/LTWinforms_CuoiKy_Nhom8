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
    public partial class ucDangKy : UserControl
    {
        TaiKhoanBUS tk = new TaiKhoanBUS();

        public ucDangKy()
        {
            InitializeComponent();
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            string confirmPass = txtConfirmPass.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPass))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Cảnh báo",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (password != confirmPass)
            {
                MessageBox.Show("Mật khẩu nhập lại không khớp!", "Lỗi",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string ketQua = tk.DangKyTaiKhoan(username, password);

            if (ketQua == "")
            {
                MessageBox.Show("Đăng ký tài khoản thành công! Bạn có thể đăng nhập ngay bây giờ.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmMainContainer.Instance.LoadUserControl(new ucDangNhap());
            }
            else
            {
                MessageBox.Show(ketQua, "Lỗi đăng ký",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linkLabelDangNhap_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmMainContainer.Instance.LoadUserControl(new ucDangNhap());
        }

        private void ucDangKy_Load(object sender, EventArgs e)
        {
            txtUsername.Focus();
        }
    }
}
