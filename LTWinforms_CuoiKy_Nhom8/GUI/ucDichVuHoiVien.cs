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
    public partial class ucDichVuHoiVien : UserControl
    {
        HoiVienBUS hvBUS = new HoiVienBUS();
        LopHocBUS lopBUS = new LopHocBUS();

        public ucDichVuHoiVien()
        {
            InitializeComponent();
        }

        private void ucDichVuHoiVien_Load(object sender, EventArgs e)
        {
            LoadDuLieuGoi();
            LoadDuLieuLop();
        }

        private void LoadDuLieuGoi()
        {
            // Tiến nhớ check tên hàm trong HoiVienBUS nhé (ví dụ: LayDanhSachGoi)
            dgvGoiTap.DataSource = hvBUS.LayDanhSachGoiTap();

            FormatGrid(dgvGoiTap, "MaGoi");

            // Đổi tiêu đề cột cho khách dễ nhìn
            if (dgvGoiTap.Columns.Contains("TenGoi")) dgvGoiTap.Columns["TenGoi"].HeaderText = "Tên Gói Tập";
            if (dgvGoiTap.Columns.Contains("GiaTien")) dgvGoiTap.Columns["GiaTien"].HeaderText = "Giá Tiền";
            if (dgvGoiTap.Columns.Contains("ThoiHan")) dgvGoiTap.Columns["ThoiHan"].HeaderText = "Thời Hạn";
            if (dgvGoiTap.Columns.Contains("PhongGym")) dgvGoiTap.Columns["PhongGym"].HeaderText = "Phòng Gym";
        }

        private void LoadDuLieuLop()
        {
            // Lấy các lớp đang ở trạng thái "Chuẩn bị" để khách đăng ký
            dgvLopHoc.DataSource = lopBUS.LayDanhSachLopHoc("", "", null, null);
            FormatGrid(dgvLopHoc, "MaLop");
        }

        private void LoadLichSu()
        {
            dgvLichSu.DataSource = hvBUS.LayLichSuDangKy(Session.IdTaiKhoan);

            if (dgvLichSu.Columns.Count > 0)
            {
                dgvLichSu.Columns["SoTien"].DefaultCellStyle.Format = "N0";
                dgvLichSu.Columns["NgayDky"].DefaultCellStyle.Format = "dd/MM/yyyy";
                dgvLichSu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private void FormatGrid(DataGridView dgv, string idColumn)
        {
            if (dgv.Columns.Count > 0)
            {
                if (dgv.Columns.Contains(idColumn)) dgv.Columns[idColumn].Visible = false;
                if (dgv.Columns.Contains("GiaTien")) dgv.Columns["GiaTien"].DefaultCellStyle.Format = "N0";

                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgv.RowHeadersVisible = false;
            }
        }

        private void btnGuiPhanHoi_Click(object sender, EventArgs e)
        {
            string noiDung = txtPhanHoi.Text.Trim();
            if (string.IsNullOrEmpty(noiDung))
            {
                return;
            }

            // GỌI HÀM TRONG HOIVIENBUS CỦA TIẾN
            string kq = hvBUS.GuiPhanHoi(Session.IdTaiKhoan, noiDung);
            if (kq == "")
            {
                MessageBox.Show("Cảm ơn góp ý của bạn!", "Thành công");
                txtPhanHoi.Clear();
            }
        }

        private void btnDangKyGoi_Click(object sender, EventArgs e)
        {
            if (dgvGoiTap.CurrentRow != null)
            {
                string maGoi = dgvGoiTap.CurrentRow.Cells["MaGoi"].Value.ToString();
                decimal giaTien = Convert.ToDecimal(dgvGoiTap.CurrentRow.Cells["GiaTien"].Value);

                if (MessageBox.Show("Xác nhận đăng ký Online gói này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    // GỌI HÀM TRONG HOIVIENBUS CỦA TIẾN
                    string kq = hvBUS.DangKyGoiOnline(Session.IdTaiKhoan, maGoi, giaTien);
                    if (kq == "")
                        MessageBox.Show("Đã đặt chỗ thành công! Hãy ghé quầy thu ngân để thanh toán.", "Thông báo");
                    else
                        MessageBox.Show("Lỗi: " + kq);
                }
            }
        }

        private void btnDangKyLop_Click(object sender, EventArgs e)
        {
            if (dgvLopHoc.CurrentRow != null)
            {
                string maLop = dgvLopHoc.CurrentRow.Cells["MaLop"].Value.ToString();
                string tenLop = dgvLopHoc.CurrentRow.Cells["TenLop"].Value.ToString();

                if (MessageBox.Show($"Bạn muốn đăng ký học lớp [{tenLop}]?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    // GỌI HÀM TRONG HOIVIENBUS CỦA TIẾN
                    string kq = hvBUS.DangKyLopOnline(Session.IdTaiKhoan, maLop);
                    if (kq == "")
                        MessageBox.Show("Đăng ký lớp thành công! Vui lòng đóng học phí tại quầy.", "Thông báo");
                    else
                        MessageBox.Show(kq);
                }
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Text == "Lịch sử của tôi")
            {
                LoadLichSu();
            }
        }

        private void dgvLichSu_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvLichSu.Columns[e.ColumnIndex].Name == "TrangThai" && e.Value != null)
            {
                if (e.Value.ToString() == "Chờ thanh toán")
                {
                    e.CellStyle.ForeColor = Color.Red; // Chưa đóng tiền: Màu Đỏ
                }
                else
                {
                    e.CellStyle.ForeColor = Color.Green; // Đã xong: Màu Xanh
                }
            }
        }
    }
}
