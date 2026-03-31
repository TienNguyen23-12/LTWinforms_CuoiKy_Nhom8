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
    public partial class ucDangKyDay : UserControl
    {
        LopHocBUS lopBUS = new LopHocBUS();
        TinNhanBUS tnBUS = new TinNhanBUS();

        public ucDangKyDay()
        {
            InitializeComponent();
        }

        private void LoadData()
        {
            dgvLopTrong.DataSource = lopBUS.LayDanhSachLopChuaCoHLV();
            if (dgvLopTrong.Columns.Count > 0)
            {
                dgvLopTrong.Columns["MaLop"].HeaderText = "Mã Lớp";
                dgvLopTrong.Columns["TenLop"].HeaderText = "Tên Lớp Học";
                dgvLopTrong.Columns["ThoiGian"].HeaderText = "Thời Gian";
                dgvLopTrong.Columns["PhongTap"].HeaderText = "Phòng Tập";
                dgvLopTrong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private void ucDangKyDay_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnDangKyDay_Click(object sender, EventArgs e)
        {
            if (dgvLopTrong.CurrentRow != null)
            {
                string maLop = dgvLopTrong.CurrentRow.Cells["MaLop"].Value.ToString();
                string tenLop = dgvLopTrong.CurrentRow.Cells["TenLop"].Value.ToString();

                if (ModernMessageBox.Show("Bạn muốn gửi yêu cầu nhận dạy lớp " + tenLop + " tới Quản lý?", "Xác nhận", MessageBoxButtons.YesNo, ModernMessageType.Question) == DialogResult.Yes)
                {
                    string noiDungMsg = "Xin chào Quản lý, tôi muốn đăng ký nhận giảng dạy lớp " + tenLop + " (Mã: " + maLop + "). Vui lòng xem xét và duyệt cho tôi nhé!";
                    string kq = tnBUS.GuiTinNhan(Session.IdTaiKhoan, 1, noiDungMsg);

                    if (kq == "")
                    {
                        ModernMessageBox.Show("Yêu cầu của bạn đã được gửi trực tiếp đến hộp thư của Quản lý thành công. Vui lòng chờ phản hồi.", "Thành công", ModernMessageType.Success);
                    }
                    else
                    {
                        ModernMessageBox.Show(kq, "Lỗi gửi yêu cầu", ModernMessageType.Error);
                    }
                }
            }
            else
            {
                ModernMessageBox.Show("Vui lòng chọn 1 lớp trong danh sách để đăng ký.", "Nhắc nhở", ModernMessageType.Warning);
            }
        }
    }
}
