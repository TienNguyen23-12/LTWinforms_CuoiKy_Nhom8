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
    public partial class ucDangKyLop : UserControl
    {
        DangKyLopBUS dkBUS = new DangKyLopBUS();
        string maHoiVienDangLogin = "";

        public ucDangKyLop()
        {
            InitializeComponent();
        }

        private void LoadData(string tuKhoa = "")
        {
            dgvLopChuaDangKy.DataSource = dkBUS.LayCacLopChuaDangKy(maHoiVienDangLogin, tuKhoa);

            dgvLopDaDangKy.DataSource = dkBUS.LayCacLopDaDangKy(maHoiVienDangLogin);

            if (dgvLopChuaDangKy.Columns.Count > 0)
            {
                dgvLopChuaDangKy.Columns["MaLop"].HeaderText = "Mã Lớp";
                dgvLopChuaDangKy.Columns["TenLop"].HeaderText = "Tên Lớp";
                dgvLopChuaDangKy.Columns["TenHLV"].HeaderText = "Huấn Luyện Viên";
                dgvLopChuaDangKy.Columns["ThoiGian"].HeaderText = "Thời Gian";
                dgvLopChuaDangKy.Columns["PhongTap"].HeaderText = "Phòng Tập";
                dgvLopChuaDangKy.Columns["SiSo"].HeaderText = "Sĩ Số";
                dgvLopChuaDangKy.Columns["SlotCon"].HeaderText = "Slot Còn Lại";

                dgvLopChuaDangKy.Columns["SlotCon"].DefaultCellStyle.ForeColor = Color.Red;
                dgvLopChuaDangKy.Columns["SlotCon"].DefaultCellStyle.Font = new Font(dgvLopChuaDangKy.Font, FontStyle.Bold);
            }

            if (dgvLopDaDangKy.Columns.Count > 0)
            {
                dgvLopDaDangKy.Columns["Id"].Visible = false; 
                dgvLopDaDangKy.Columns["MaLop"].HeaderText = "Mã Lớp";
                dgvLopDaDangKy.Columns["TenLop"].HeaderText = "Tên Lớp";
                dgvLopDaDangKy.Columns["TenHLV"].HeaderText = "Huấn Luyện Viên";
                dgvLopDaDangKy.Columns["ThoiGian"].HeaderText = "Thời Gian";
                dgvLopDaDangKy.Columns["NgayDangKy"].HeaderText = "Ngày Ghi Danh";
                dgvLopDaDangKy.Columns["NgayDangKy"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            }
        }

        private void ucDangKyLop_Load(object sender, EventArgs e)
        {
            maHoiVienDangLogin = dkBUS.LayMaHoiVien(Session.IdTaiKhoan);

            if (string.IsNullOrEmpty(maHoiVienDangLogin))
            {
                MessageBox.Show("Tài khoản của bạn chưa được liên kết với hồ sơ Hội viên nào!", "Lỗi");
                return;
            }

            LoadData();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            LoadData(txtTimKiem.Text.Trim());
        }

        private void btnHuyDangKy_Click(object sender, EventArgs e)
        {
            if (dgvLopDaDangKy.CurrentRow != null)
            {
                int idDangKy = Convert.ToInt32(dgvLopDaDangKy.CurrentRow.Cells["Id"].Value);
                string tenLop = dgvLopDaDangKy.CurrentRow.Cells["TenLop"].Value.ToString();

                if (MessageBox.Show($"Bạn có chắc chắn muốn rút khỏi {tenLop}?", "Cảnh báo", 
                                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    string kq = dkBUS.HuyDangKy(idDangKy);
                    if (kq == "")
                    {
                        MessageBox.Show("Đã hủy đăng ký thành công.");
                        LoadData(); 
                    }
                    else
                    {
                        MessageBox.Show(kq, "Lỗi");
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn 1 lớp ở bảng dưới để hủy!", "Nhắc nhở");
            }
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            if (dgvLopChuaDangKy.CurrentRow != null)
            {
                string maLop = dgvLopChuaDangKy.CurrentRow.Cells["MaLop"].Value.ToString();
                string tenLop = dgvLopChuaDangKy.CurrentRow.Cells["TenLop"].Value.ToString();

                string slotCon = dgvLopChuaDangKy.CurrentRow.Cells["SlotCon"].Value.ToString();
                if (slotCon == "Đã đầy")
                {
                    MessageBox.Show($"Rất tiếc! Lớp {tenLop} đã hết chỗ. Vui lòng chọn lớp khác nhé!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                decimal giaTien = 0;
                if (dgvLopChuaDangKy.CurrentRow.Cells["GiaTien"].Value != null)
                {
                    giaTien = Convert.ToDecimal(dgvLopChuaDangKy.CurrentRow.Cells["GiaTien"].Value);
                }

                string thongBao = $"Lớp học [{tenLop}] có học phí là: {giaTien.ToString("N0")} VNĐ.\n\nBạn có chắc chắn muốn đăng ký và thanh toán ngay?";

                if (MessageBox.Show(thongBao, "Xác nhận thanh toán", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string kq = dkBUS.DangKy(maHoiVienDangLogin, maLop);
                    if (kq == "")
                    {
                        MessageBox.Show("Thanh toán và Ghi danh thành công!", "Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show(kq, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn 1 lớp ở bảng trên để đăng ký!", "Nhắc nhở");
            }
        }
    }
}
