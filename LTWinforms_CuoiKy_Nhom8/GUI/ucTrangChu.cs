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
    public partial class ucTrangChu : UserControl
    {
        public ucTrangChu()
        {
            InitializeComponent();
        }

        public void TaiChucNang(UserControl uc)
        {
            pnlNoiDungChinh.Controls.Clear();
            uc.Dock = DockStyle.Fill;        
            pnlNoiDungChinh.Controls.Add(uc);
        }

        private void ucTrangChu_Load(object sender, EventArgs e)
        {
            lblXinChao.Text = "Xin chào: " + Session.Username;

            btnQuanTriHeThong.Visible = true;
            btnThongKeDoanhThu.Visible = true;
            btnQuanLyHoiVien.Visible = true;
            btnQuanLyGoiTap.Visible = true;
            btnBanVeThuNgan.Visible = true;
            btnTinNhan.Visible = true;
            btnDangKyDichVu.Visible = true;
            btnLichSuGiaoDich.Visible = true;
            btnLuongThuong.Visible = true;
            btnHoSoCaNhan.Visible = true;
            btnLichHoc.Visible = true;
            btnQuanLyLopHoc.Visible = true;

            if (Session.Role == 3) // HỘI VIÊN
            {
                btnLichHoc.Text = "Lịch học của tôi";

                btnDangKyDichVu.Text = "Đăng ký lớp";

                btnQuanTriHeThong.Visible = false;
                btnThongKeDoanhThu.Visible = false;
                btnQuanLyHoiVien.Visible = false;
                btnQuanLyGoiTap.Visible = false;
                btnBanVeThuNgan.Visible = false;
                btnLuongThuong.Visible = false;
                btnQuanLyLopHoc.Visible = false;
                btnQuanLyPhongTap.Visible = false;
            }
            else if (Session.Role == 4) // HUẤN LUYỆN VIÊN
            {
                btnLichHoc.Text = "Lịch dạy & Điểm danh";

                btnDangKyDichVu.Text = "Đăng ký nhận lớp"; 
                btnDangKyDichVu.Visible = true; 

                btnQuanTriHeThong.Visible = false;
                btnThongKeDoanhThu.Visible = false;
                btnQuanLyHoiVien.Visible = false;
                btnQuanLyGoiTap.Visible = false;
                btnBanVeThuNgan.Visible = false;
                btnLichSuGiaoDich.Visible = false;
                btnQuanLyLopHoc.Visible = false;
                btnQuanLyPhongTap.Visible = false;
            }
            else if (Session.Role == 2) // NHÂN VIÊN
            {
                btnLichHoc.Text = "Lịch toàn Trung tâm"; 

                btnQuanTriHeThong.Visible = false;
                btnThongKeDoanhThu.Visible = false;
                btnDangKyDichVu.Visible = false;
                btnLichSuGiaoDich.Visible = false;
                btnLuongThuong.Visible = false;
            }
            else if (Session.Role == 1) // ADMIN
            {
                btnLichHoc.Text = "Lịch toàn Trung tâm"; 

                btnDangKyDichVu.Visible = false;
                btnLichSuGiaoDich.Visible = false;
                btnLuongThuong.Visible = false;
            }
        }

        private void btnQuanTriHeThong_Click(object sender, EventArgs e)
        {
            TaiChucNang(new ucQuanTriHeThong());
        }

        private void btnQuanLyHoiVien_Click(object sender, EventArgs e)
        {
            TaiChucNang(new ucQuanLyHoiVien());
        }

        private void btnQuanLyGoiTap_Click(object sender, EventArgs e)
        {
            TaiChucNang(new ucQuanLyGoiTap());
        }

        private void btnBanVeThuNgan_Click(object sender, EventArgs e)
        {
            TaiChucNang(new ucBanVeThuNgan());
        }

        private void btnHoSoCaNhan_Click(object sender, EventArgs e)
        {
            TaiChucNang(new ucHoSoCaNhan());
        }

        private void btnThongKeDoanhThu_Click(object sender, EventArgs e)
        {
            TaiChucNang(new ucThongKeDoanhThu());
        }

        private void btnQuanLyLopHoc_Click(object sender, EventArgs e)
        {
            TaiChucNang(new ucQuanLyLopHoc());
        }

        private void btnTinNhan_Click(object sender, EventArgs e)
        {
            TaiChucNang(new ucTinNhan());
        }

        private void btnDangKyDichVu_Click(object sender, EventArgs e)
        {
            if (Session.Role == 3) 
            {
                TaiChucNang(new ucDichVuHoiVien());
            }
            else if (Session.Role == 4) 
            {
                TaiChucNang(new ucDangKyDay());
            }
        }

        private void btnLichSuGiaoDich_Click(object sender, EventArgs e)
        {
            TaiChucNang(new ucLichSuGiaoDich());
        }

        private void btnLuongThuong_Click(object sender, EventArgs e)
        {
            TaiChucNang(new ucLuongThuong());
        }

        private void btnLichHoc_Click(object sender, EventArgs e)
        {
            TaiChucNang(new ucLichHoc());
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            DialogResult xacNhan = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất khỏi hệ thống?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

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
            TaiChucNang(new ucQuanLyPhongTap());
        }

        private void btnChamCong_Click(object sender, EventArgs e)
        {
            TaiChucNang(new ucQuanLyNhanSu());
        }

        private void btnQuanLySanPham_Click(object sender, EventArgs e)
        {
            TaiChucNang(new ucQuanLySanPham());
        }
    }
}
