using LTWinforms_CuoiKy_Nhom8.BUS;
using LTWinforms_CuoiKy_Nhom8.DTO;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace LTWinforms_CuoiKy_Nhom8.GUI
{
    public partial class ucDichVuHoiVien : UserControl
    {
        private readonly HoiVienBUS hvBUS = new HoiVienBUS();
        private readonly LopHocBUS lopBUS = new LopHocBUS();
        private bool isThemeApplied;
        private bool isLayoutHooked;

        public ucDichVuHoiVien()
        {
            InitializeComponent();
        }

        private void ucDichVuHoiVien_Load(object sender, EventArgs e)
        {
            ApplyTheme();

            dtpTuNgay.Enabled = false;
            dtpDenNgay.Enabled = false;

            LoadDuLieuGoi();
            LoadDuLieuLop();
            LoadComboHLV();

            if (!isLayoutHooked)
            {
                Resize += ucDichVuHoiVien_Resize;
                isLayoutHooked = true;
            }

            ApplyResponsiveLayout();
            BeginInvoke((Action)ApplyResponsiveLayout);
        }

        private void ucDichVuHoiVien_Resize(object sender, EventArgs e)
        {
            ApplyResponsiveLayout();
        }

        private void ApplyTheme()
        {
            if (isThemeApplied)
            {
                return;
            }

            BackColor = ModernTheme.PageBackground;
            ApplyRegularFont(this);

            tabControl1.Font = new Font("Segoe UI", 10F, FontStyle.Regular);

            tpGoiTap.UseVisualStyleBackColor = false;
            tpLopHoc.UseVisualStyleBackColor = false;
            tpLichSu.UseVisualStyleBackColor = false;
            tpGoiTap.BackColor = ModernTheme.PageBackground;
            tpLopHoc.BackColor = ModernTheme.PageBackground;
            tpLichSu.BackColor = ModernTheme.PageBackground;

            ModernTheme.StyleDataComboBox(cboLocHLV);
            chkLocNgay.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            chkLocNgay.ForeColor = ModernTheme.TextPrimary;

            dtpTuNgay.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            dtpDenNgay.Font = new Font("Segoe UI", 10F, FontStyle.Regular);

            StyleGridRegular(dgvGoiTap);
            StyleGridRegular(dgvLopHoc);
            StyleGridRegular(dgvLichSu);

            StyleModernButton(btnDangKyGoi, Color.FromArgb(46, 134, 222));
            StyleModernButton(btnDangKyLop, Color.FromArgb(46, 134, 222));
            StyleModernButton(btnLoc, Color.FromArgb(52, 73, 94));
            StyleModernButton(btnGuiPhanHoi, Color.FromArgb(46, 134, 222));

            // UI nút hủy
            StyleModernButton(btnHuyDangKy, Color.FromArgb(231, 76, 60));
            btnHuyDangKy.Text = "Hủy";

            txtPhanHoi.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            txtPhanHoi.BorderStyle = BorderStyle.FixedSingle;
            txtPhanHoi.BackColor = Color.White;
            txtPhanHoi.ForeColor = ModernTheme.TextPrimary;

            isThemeApplied = true;
        }

        private void ApplyRegularFont(Control root)
        {
            if (root == null)
            {
                return;
            }

            root.Font = new Font("Segoe UI", 10F, FontStyle.Regular);

            foreach (Control child in root.Controls)
            {
                ApplyRegularFont(child);
            }
        }

        private void StyleGridRegular(DataGridView dgv)
        {
            ModernTheme.StyleGrid(dgv);
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgv.RowHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
        }

        private void StyleModernButton(Button button, Color backColor)
        {
            ModernTheme.StyleButton(button, backColor, Color.White);
            button.Font = new Font("Segoe UI", 10F, FontStyle.Bold); // chữ in đậm
            button.Size = new Size(150, 40);
            button.TextAlign = ContentAlignment.MiddleCenter;
            button.Padding = new Padding(0);
        }

        private void ApplyResponsiveLayout()
        {
            int contentWidth = Math.Min(1160, ClientSize.Width - 24);
            if (contentWidth < 940)
            {
                contentWidth = 940;
            }

            int left = Math.Max(8, (ClientSize.Width - contentWidth) / 2);

            int top = 12;
            int tabHeight = Math.Max(360, ClientSize.Height / 2);
            tabControl1.SetBounds(left, top, contentWidth, tabHeight);

            Rectangle pageRect = tabControl1.DisplayRectangle;
            int pageWidth = Math.Max(760, pageRect.Width);
            int pageHeight = Math.Max(250, pageRect.Height);

            // ===== Tab Gói tập =====
            int goiGridWidth = Math.Max(700, pageWidth - 80);
            int goiGridLeft = (pageWidth - goiGridWidth) / 2;
            int goiGridTop = 16;
            int goiGridHeight = Math.Max(170, pageHeight - 120);
            dgvGoiTap.SetBounds(goiGridLeft, goiGridTop, goiGridWidth, goiGridHeight);

            int goiBtnLeft = (pageWidth - btnDangKyGoi.Width) / 2;
            btnDangKyGoi.SetBounds(goiBtnLeft, dgvGoiTap.Bottom + 10, btnDangKyGoi.Width, btnDangKyGoi.Height);

            // ===== Tab Lớp học =====
            int filterTop = 16;
            int comboW = 170;
            int dateW = 190;
            int gap = 12;
            int checkW = 130;

            int filterWidth = comboW + gap + dateW + gap + dateW + gap + checkW;
            int filterLeft = (pageWidth - filterWidth) / 2;

            cboLocHLV.SetBounds(filterLeft, filterTop, comboW, 32);
            dtpTuNgay.SetBounds(cboLocHLV.Right + gap, filterTop, dateW, 32);
            dtpDenNgay.SetBounds(dtpTuNgay.Right + gap, filterTop, dateW, 32);
            chkLocNgay.SetBounds(dtpDenNgay.Right + gap, filterTop + 6, checkW, 24);

            int locLeft = (pageWidth - btnLoc.Width) / 2;
            btnLoc.SetBounds(locLeft, dtpTuNgay.Bottom + 8, btnLoc.Width, btnLoc.Height);

            int lopGridWidth = Math.Max(700, pageWidth - 80);
            int lopGridLeft = (pageWidth - lopGridWidth) / 2;
            int lopGridTop = btnLoc.Bottom + 10;
            int lopGridHeight = Math.Max(145, pageHeight - lopGridTop - 58);
            dgvLopHoc.SetBounds(lopGridLeft, lopGridTop, lopGridWidth, lopGridHeight);

            int lopBtnLeft = (pageWidth - btnDangKyLop.Width) / 2;
            btnDangKyLop.SetBounds(lopBtnLeft, dgvLopHoc.Bottom + 8, btnDangKyLop.Width, btnDangKyLop.Height);

            // ===== Tab Lịch sử =====
            int lsGridWidth = Math.Max(700, pageWidth - 80);
            int lsGridLeft = (pageWidth - lsGridWidth) / 2;
            int lsGridTop = 16;
            int lsGridHeight = Math.Max(145, pageHeight - 86);
            dgvLichSu.SetBounds(lsGridLeft, lsGridTop, lsGridWidth, lsGridHeight);

            int huyLeft = (pageWidth - btnHuyDangKy.Width) / 2;
            btnHuyDangKy.SetBounds(huyLeft, dgvLichSu.Bottom + 8, btnHuyDangKy.Width, btnHuyDangKy.Height);

            // ===== Khu phản hồi =====
            int phTop = tabControl1.Bottom + 18;
            int phWidth = Math.Min(820, contentWidth - 80);
            int phLeft = left + (contentWidth - phWidth) / 2;
            txtPhanHoi.SetBounds(phLeft, phTop, phWidth, 110);

            int phBtnLeft = left + (contentWidth - btnGuiPhanHoi.Width) / 2;
            btnGuiPhanHoi.SetBounds(phBtnLeft, txtPhanHoi.Bottom + 12, btnGuiPhanHoi.Width, btnGuiPhanHoi.Height);
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

            if (dgvLopHoc.Columns.Count > 0)
            {
                if (dgvLopHoc.Columns.Contains("MaLop")) dgvLopHoc.Columns["MaLop"].HeaderText = "Mã lớp";
                if (dgvLopHoc.Columns.Contains("TenLop")) dgvLopHoc.Columns["TenLop"].HeaderText = "Tên lớp học";
                if (dgvLopHoc.Columns.Contains("TenHLV")) dgvLopHoc.Columns["TenHLV"].HeaderText = "Huấn luyện viên";
                if (dgvLopHoc.Columns.Contains("ThoiGian")) dgvLopHoc.Columns["ThoiGian"].HeaderText = "Thời gian";
                if (dgvLopHoc.Columns.Contains("PhongTap")) dgvLopHoc.Columns["PhongTap"].HeaderText = "Phòng tập";
                if (dgvLopHoc.Columns.Contains("GiaTien")) dgvLopHoc.Columns["GiaTien"].HeaderText = "Giá tiền (VNĐ)";
                if (dgvLopHoc.Columns.Contains("SoLuongToiDa")) dgvLopHoc.Columns["SoLuongToiDa"].HeaderText = "Sĩ số tối đa";
                if (dgvLopHoc.Columns.Contains("SoBuoi")) dgvLopHoc.Columns["SoBuoi"].HeaderText = "Số buổi";
                if (dgvLopHoc.Columns.Contains("NgayBatDau"))
                {
                    dgvLopHoc.Columns["NgayBatDau"].HeaderText = "Ngày bắt đầu";
                    dgvLopHoc.Columns["NgayBatDau"].DefaultCellStyle.Format = "dd/MM/yyyy";
                }
                if (dgvLopHoc.Columns.Contains("TrangThai")) dgvLopHoc.Columns["TrangThai"].HeaderText = "Trạng thái";
                if (dgvLopHoc.Columns.Contains("SiSo")) dgvLopHoc.Columns["SiSo"].HeaderText = "Sĩ số";
                if (dgvLopHoc.Columns.Contains("SlotCon")) dgvLopHoc.Columns["SlotCon"].HeaderText = "Chỗ còn lại";
            }
        }

        private void LoadLichSu()
        {
            dgvLichSu.DataSource = hvBUS.LayLichSuDangKy(Session.IdTaiKhoan);

            if (dgvLichSu.Columns.Count > 0)
            {
                if (dgvLichSu.Columns.Contains("MaLop")) dgvLichSu.Columns["MaLop"].Visible = false;
                if (dgvLichSu.Columns.Contains("IdDangKy")) dgvLichSu.Columns["IdDangKy"].Visible = false;

                if (dgvLichSu.Columns.Contains("TenLop")) dgvLichSu.Columns["TenLop"].HeaderText = "Tên lớp";
                if (dgvLichSu.Columns.Contains("TenGoi")) dgvLichSu.Columns["TenGoi"].HeaderText = "Tên gói tập";
                if (dgvLichSu.Columns.Contains("TenDichVu")) dgvLichSu.Columns["TenDichVu"].HeaderText = "Tên dịch vụ";
                if (dgvLichSu.Columns.Contains("LoaiDichVu")) dgvLichSu.Columns["LoaiDichVu"].HeaderText = "Loại dịch vụ";
                if (dgvLichSu.Columns.Contains("TrangThaiThanhToan")) dgvLichSu.Columns["TrangThaiThanhToan"].HeaderText = "Trạng thái thanh toán";
                if (dgvLichSu.Columns.Contains("NgayDky")) dgvLichSu.Columns["NgayDky"].HeaderText = "Ngày đăng ký";
                if (dgvLichSu.Columns.Contains("SoTien")) dgvLichSu.Columns["SoTien"].HeaderText = "Số tiền (VNĐ)";

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
            }
        }

        private void dgvLichSu_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvLichSu.Columns[e.ColumnIndex].Name == "TrangThai" && e.Value != null)
            {
                if (e.Value.ToString() == "Chờ thanh toán")
                {
                    e.CellStyle.ForeColor = Color.Red;
                }
                else
                {
                    e.CellStyle.ForeColor = Color.Green;
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
