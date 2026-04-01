using LTWinforms_CuoiKy_Nhom8.BUS;
using LTWinforms_CuoiKy_Nhom8.DTO;
using System;
using System.Drawing;
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
            dtpTuNgay.Enabled = false;
            dtpDenNgay.Enabled = false;

            LoadDuLieuGoi();
            LoadDuLieuLop();
            LoadComboHLV();
        }

        private void LoadDuLieuGoi()
        {
            dgvGoiTap.DataSource = hvBUS.LayDanhSachGoiTap();

            FormatGrid(dgvGoiTap, "MaGoi");

            if (dgvGoiTap.Columns.Contains("TenGoi")) dgvGoiTap.Columns["TenGoi"].HeaderText = "Tên Gói Tập";
            if (dgvGoiTap.Columns.Contains("GiaTien")) dgvGoiTap.Columns["GiaTien"].HeaderText = "Giá Tiền";
            if (dgvGoiTap.Columns.Contains("ThoiHan")) dgvGoiTap.Columns["ThoiHan"].HeaderText = "Thời Hạn";
            if (dgvGoiTap.Columns.Contains("PhongGym")) dgvGoiTap.Columns["PhongGym"].HeaderText = "Phòng Gym";
        }

        private void LoadDuLieuLop(string maHLV = "", DateTime? tuNgay = null, DateTime? denNgay = null)
        {
            dgvLopHoc.DataSource = lopBUS.LayDanhSachLopHoc("", maHLV, tuNgay, denNgay);
            FormatGrid(dgvLopHoc, "MaLop");
        }

        private void LoadLichSu()
        {
            dgvLichSu.DataSource = hvBUS.LayLichSuDangKy(Session.IdTaiKhoan);

            if (dgvLichSu.Columns.Count > 0)
            {
                if (dgvLichSu.Columns.Contains("MaLop")) dgvLichSu.Columns["MaLop"].Visible = false;
                if (dgvLichSu.Columns.Contains("IdDangKy")) dgvLichSu.Columns["IdDangKy"].Visible = false;

                if (dgvLichSu.Columns.Contains("SoTien"))
                    dgvLichSu.Columns["SoTien"].DefaultCellStyle.Format = "N0";
                if (dgvLichSu.Columns.Contains("NgayDky"))
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

            string kq = hvBUS.GuiPhanHoi(Session.IdTaiKhoan, noiDung);
            if (kq == "")
            {
                ModernMessageBox.Show("Cảm ơn góp ý của bạn!", "Thành công", ModernMessageType.Success);
                txtPhanHoi.Clear();
            }
        }

        private void btnDangKyGoi_Click(object sender, EventArgs e)
        {
            if (dgvGoiTap.CurrentRow != null)
            {
                string maGoi = dgvGoiTap.CurrentRow.Cells["MaGoi"].Value.ToString();
                decimal giaTien = Convert.ToDecimal(dgvGoiTap.CurrentRow.Cells["GiaTien"].Value);

                if (ModernMessageBox.Show("Xác nhận đăng ký online gói này?", "Xác nhận", MessageBoxButtons.YesNo, ModernMessageType.Question) == DialogResult.Yes)
                {
                    string kq = hvBUS.DangKyGoiOnline(Session.IdTaiKhoan, maGoi, giaTien);
                    if (kq == "")
                    {
                        ModernMessageBox.Show("Đã đặt chỗ thành công. Hãy ghé quầy thu ngân để thanh toán.", "Thông báo", ModernMessageType.Success);
                    }
                    else
                    {
                        ModernMessageBox.Show("Lỗi: " + kq, "Lỗi", ModernMessageType.Error);
                    }
                }
            }
        }

        private void btnDangKyLop_Click(object sender, EventArgs e)
        {
            if (dgvLopHoc.CurrentRow != null)
            {
                string maLop = dgvLopHoc.CurrentRow.Cells["MaLop"].Value.ToString();
                string tenLop = dgvLopHoc.CurrentRow.Cells["TenLop"].Value.ToString();

                if (ModernMessageBox.Show("Bạn muốn đăng ký học lớp [" + tenLop + "]?", "Xác nhận", MessageBoxButtons.YesNo, ModernMessageType.Question) == DialogResult.Yes)
                {
                    string kq = hvBUS.DangKyLopOnline(Session.IdTaiKhoan, maLop);
                    if (kq == "")
                    {
                        ModernMessageBox.Show("Đăng ký lớp thành công. Vui lòng đóng học phí tại quầy.", "Thông báo", ModernMessageType.Success);
                    }
                    else
                    {
                        ModernMessageBox.Show(kq, "Lỗi", ModernMessageType.Error);
                    }
                }
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tabName = tabControl1.SelectedTab.Name;

            switch (tabName)
            {
                case "tpGoiTap":
                    LoadDuLieuGoi();
                    break;

                case "tpLopHoc":
                    LoadDuLieuLop();
                    break;

                case "tpLichSu":
                    LoadLichSu();
                    break;

                default:
                    break;
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

        private void LoadComboHLV()
        {
            cboLocHLV.DataSource = lopBUS.LayDanhSachHLV();
            cboLocHLV.DisplayMember = "TenHLV";
            cboLocHLV.ValueMember = "MaHLV";
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            string maHLV = "";
            if (cboLocHLV.SelectedValue != null)
            {
                maHLV = cboLocHLV.SelectedValue.ToString();
            }

            DateTime? tuNgay = null;
            DateTime? denNgay = null;

            if (chkLocNgay.Checked)
            {
                tuNgay = dtpTuNgay.Value;
                denNgay = dtpDenNgay.Value;

                if (tuNgay > denNgay)
                {
                    ModernMessageBox.Show("Từ ngày không thể lớn hơn Đến ngày!", "Cảnh báo", ModernMessageType.Warning);
                    return;
                }
            }

            LoadDuLieuLop(maHLV, tuNgay, denNgay);
        }

        private void chkLocNgay_CheckedChanged(object sender, EventArgs e)
        {
            dtpTuNgay.Enabled = chkLocNgay.Checked;
            dtpDenNgay.Enabled = chkLocNgay.Checked;
        }

        private void btnHuyDangKy_Click(object sender, EventArgs e)
        {
            if (dgvLichSu.CurrentRow == null)
            {
                return;
            }

            var row = dgvLichSu.CurrentRow;

            if (!dgvLichSu.Columns.Contains("Loai") || row.Cells["Loai"].Value == null)
            {
                ModernMessageBox.Show("Vui lòng chọn một mục trong Lịch sử để hủy.", "Thông báo", ModernMessageType.Warning);
                return;
            }

            string loai = row.Cells["Loai"].Value.ToString();
            string ten = row.Cells["TenDichVu"]?.Value?.ToString() ?? "";
            int idDangKy = 0;

            if (dgvLichSu.Columns.Contains("IdDangKy") && row.Cells["IdDangKy"].Value != null)
            {
                int.TryParse(row.Cells["IdDangKy"].Value.ToString(), out idDangKy);
            }

            if (idDangKy <= 0)
            {
                ModernMessageBox.Show("Không thể xác định bản ghi để hủy.", "Lỗi", ModernMessageType.Error);
                return;
            }

            if (ModernMessageBox.Show("Bạn có chắc muốn hủy " + loai + " [" + ten + "]?", "Xác nhận", MessageBoxButtons.YesNo, ModernMessageType.Question) == DialogResult.Yes)
            {
                string kq = hvBUS.HuyDangKy(Session.IdTaiKhoan, loai, idDangKy);
                if (kq == "")
                {
                    ModernMessageBox.Show("Hủy thành công.", "Thông báo", ModernMessageType.Success);
                    LoadLichSu();
                }
                else
                {
                    ModernMessageBox.Show(kq, "Lỗi", ModernMessageType.Error);
                }
            }
        }
    }
}
