using LTWinforms_CuoiKy_Nhom8.BUS;
using LTWinforms_CuoiKy_Nhom8.DAL;
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
    public partial class ucHoSoCaNhan : UserControl
    {
        TaiKhoanBUS tkBUS = new TaiKhoanBUS();
        QLTTDataContext db = new QLTTDataContext();

        public ucHoSoCaNhan()
        {
            InitializeComponent();
        }

        private void ucHoSoCaNhan_Load(object sender, EventArgs e)
        {
            lblXinChao.Text = "Tài khoản: " + Session.Username;
            lblThongTinThem.Text = "";

            if (Session.Role == 1) // ADMIN
            {
                lblQuyen.Text = "Vai trò: Quản trị viên (Admin)";
                lblThongTinThem.Text = $"ID Hệ thống: {Session.IdTaiKhoan}\nQuyền hạn: Toàn quyền kiểm soát hệ thống";

                groupBox2.Visible = false;
            }
            else if (Session.Role == 2) // NHÂN VIÊN
            {
                lblQuyen.Text = "Vai trò: Nhân viên Lễ tân";
                lblThongTinThem.Text = $"ID Hệ thống: {Session.IdTaiKhoan}\nQuyền hạn: Quản lý khách hàng, Hóa đơn, Lớp học";

                groupBox2.Visible = false;
            }
            else if (Session.Role == 4) // HUẤN LUYỆN VIÊN
            {
                lblQuyen.Text = "Vai trò: Huấn luyện viên";

                var hlv = db.HuanLuyenViens.FirstOrDefault(x => x.IdTaiKhoan == Session.IdTaiKhoan);
                if (hlv != null)
                {
                    string luong = hlv.LuongCoBan.HasValue ? hlv.LuongCoBan.Value.ToString("N0") + " VNĐ/Lớp" : "Chưa cập nhật";
                    string sdt = string.IsNullOrEmpty(hlv.SDT) ? "Chưa cập nhật" : hlv.SDT;

                    lblThongTinThem.Text = $"Mã HLV: {hlv.MaHLV}\n" +
                                           $"Họ và tên: {hlv.TenHLV}\n" +
                                           $"Số điện thoại: {sdt}\n" +
                                           $"Lương cơ bản: {luong}";

                    txtHoTen.Text = hlv.TenHLV;
                    txtSDT.Text = hlv.SDT;
                    cboGioiTinh.Text = hlv.GioiTinh;
                    if (hlv.NgaySinh.HasValue) dtpNgaySinh.Value = hlv.NgaySinh.Value;
                }
            }
            else if (Session.Role == 3) // HỘI VIÊN
            {
                lblQuyen.Text = "Vai trò: Hội viên (Khách hàng)";

                var hoiVien = db.HoiViens.FirstOrDefault(x => x.IdTaiKhoan == Session.IdTaiKhoan);
                if (hoiVien != null)
                {
                    string hanThe = hoiVien.NgayHetHan.HasValue ? hoiVien.NgayHetHan.Value.ToString("dd/MM/yyyy") : "Chưa có thẻ / Thẻ chưa kích hoạt";
                    string ngayDK = hoiVien.NgayDangKy.HasValue ? hoiVien.NgayDangKy.Value.ToString("dd/MM/yyyy") : "Không xác định";
                    string sdt = string.IsNullOrEmpty(hoiVien.SDT) ? "Chưa cập nhật" : hoiVien.SDT;

                    lblThongTinThem.Text = $"Mã Hội Viên: {hoiVien.MaHoiVien}\n" +
                                           $"Họ và tên: {hoiVien.HoTen}\n" +
                                           $"Số điện thoại: {sdt}\n" +
                                           $"Ngày đăng ký: {ngayDK}\n" +
                                           $"Ngày hết hạn thẻ: {hanThe}";

                    txtHoTen.Text = hoiVien.HoTen;
                    txtSDT.Text = hoiVien.SDT;
                    cboGioiTinh.Text = hoiVien.GioiTinh;
                    if (hoiVien.NgaySinh.HasValue) dtpNgaySinh.Value = hoiVien.NgaySinh.Value;
                }
            }
        }

        private void btnLuuMatKhau_Click(object sender, EventArgs e)
        {
            string passCu = txtPassCu.Text.Trim();
            string passMoi = txtPassMoi.Text.Trim();
            string xacNhan = txtXacNhanPass.Text.Trim();

            if (string.IsNullOrEmpty(passCu) || string.IsNullOrEmpty(passMoi) || string.IsNullOrEmpty(xacNhan))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin mật khẩu!", "Cảnh báo");
                return;
            }

            if (passMoi != xacNhan)
            {
                MessageBox.Show("Mật khẩu xác nhận không khớp!", "Lỗi");
                return;
            }

            string kq = tkBUS.DoiMatKhau(Session.IdTaiKhoan, passCu, passMoi);
            if (kq == "")
            {
                MessageBox.Show("Đổi mật khẩu thành công! Vui lòng ghi nhớ mật khẩu mới.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPassCu.Clear();
                txtPassMoi.Clear();
                txtXacNhanPass.Clear();
            }
            else
            {
                MessageBox.Show(kq, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLuuHoSo_Click(object sender, EventArgs e)
        {
            string kq = tkBUS.CapNhatHoSoCaNhan(Session.IdTaiKhoan, Session.Role, txtHoTen.Text.Trim(), txtSDT.Text.Trim(), dtpNgaySinh.Value, cboGioiTinh.Text);

            if (kq == "")
            {
                MessageBox.Show("Cập nhật thông tin cá nhân thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ucHoSoCaNhan_Load(sender, e);
            }
            else
            {
                MessageBox.Show(kq, "Lỗi cập nhật", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
