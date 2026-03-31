using LTWinforms_CuoiKy_Nhom8.BUS;
using LTWinforms_CuoiKy_Nhom8.DAL;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace LTWinforms_CuoiKy_Nhom8.GUI
{
    public partial class ucQuanLyLopHoc : UserControl
    {
        private readonly LopHocBUS lopBUS = new LopHocBUS();
        private readonly QLTTDataContext db = new QLTTDataContext();
        private bool isThemeApplied;
        private bool isLayoutHooked;

        public ucQuanLyLopHoc()
        {
            InitializeComponent();
        }

        private void ApplyTheme()
        {
            if (isThemeApplied)
            {
                return;
            }

            BackColor = Color.White;

            StyleLabel(label1);
            StyleLabel(label2);
            StyleLabel(label3);
            StyleLabel(label4);
            StyleLabel(label5);
            StyleLabel(label6);
            StyleLabel(label7);
            StyleLabel(label8);
            StyleLabel(label9);
            StyleLabel(label10);
            StyleLabel(label11);
            StyleLabel(label12);
            StyleLabel(label13);
            StyleLabel(label14);

            StyleInput(txtMaLop);
            StyleInput(txtTenLop);
            StyleInput(txtThoiGian);
            StyleInput(txtGiaTien);
            StyleInput(txtSoLuongToiDa);
            StyleInput(txtSoBuoi);
            StyleInput(txtTimKiem);

            StyleInput(cboHLV);
            StyleInput(cboPhongTap);
            StyleInput(cboTrangThai);
            StyleInput(cboLocHLV);

            StyleInput(dtpNgayBatDau);
            StyleInput(dtpLocTuNgay);
            StyleInput(dtpLocDenNgay);

            StylePrimaryButton(btnThem);
            StyleSecondaryButton(btnSua);
            StyleDangerButton(btnKhoa);
            StyleSecondaryButton(btnLamMoi);
            StylePrimaryButton(btnTimKiem);

            StyleGrid(dgvLopHoc);

            isThemeApplied = true;
        }

        private void StyleLabel(Label label)
        {
            label.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            label.ForeColor = Color.FromArgb(44, 62, 80);
        }

        private void StyleInput(Control control)
        {
            ModernTheme.StyleInput(control);
        }

        private void StylePrimaryButton(Button button)
        {
            StyleButton(button, Color.FromArgb(46, 134, 222), Color.White);
        }

        private void StyleSecondaryButton(Button button)
        {
            StyleButton(button, Color.FromArgb(52, 73, 94), Color.White);
        }

        private void StyleDangerButton(Button button)
        {
            StyleButton(button, Color.FromArgb(211, 84, 0), Color.White);
        }

        private void StyleButton(Button button, Color backColor, Color foreColor)
        {
            ModernTheme.StyleButton(button, backColor, foreColor);
            button.Height = 34;
        }

        private void StyleGrid(DataGridView grid)
        {
            grid.BorderStyle = BorderStyle.None;
            grid.BackgroundColor = Color.White;
            grid.EnableHeadersVisualStyles = false;
            grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(46, 134, 222);
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            grid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            grid.ColumnHeadersHeight = 38;

            grid.RowTemplate.Height = 28;
            grid.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 249, 255);

            grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(186, 222, 250);
            grid.DefaultCellStyle.SelectionForeColor = Color.FromArgb(25, 42, 58);
            grid.RowHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(186, 222, 250);
            grid.RowHeadersDefaultCellStyle.SelectionForeColor = Color.FromArgb(25, 42, 58);

            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.MultiSelect = false;
            grid.ReadOnly = true;
            grid.AllowUserToAddRows = false;
            grid.AllowUserToResizeRows = false;
        }

        private void ApplyResponsiveLayout()
        {
            int contentWidth = Math.Min(1120, ClientSize.Width - 80);
            if (contentWidth < 960)
            {
                contentWidth = 960;
            }

            int left = Math.Max(20, (ClientSize.Width - contentWidth) / 2);
            int top = 26;

            int leftLabel = left + 20;
            int leftInput = left + 145;

            int rightLabel = left + 520;
            int rightInput = rightLabel + 130;

            int inputWidth = 250;
            int rowHeight = 42;

            // Row 1
            label1.Left = leftLabel;
            label1.Top = top + 4;
            txtMaLop.Left = leftInput;
            txtMaLop.Top = top;
            txtMaLop.Width = inputWidth;

            label6.Left = rightLabel;
            label6.Top = top + 4;
            cboPhongTap.Left = rightInput;
            cboPhongTap.Top = top;
            cboPhongTap.Width = inputWidth;

            // Row 2
            top += rowHeight;
            label2.Left = leftLabel;
            label2.Top = top + 4;
            txtTenLop.Left = leftInput;
            txtTenLop.Top = top;
            txtTenLop.Width = inputWidth;

            label4.Left = rightLabel;
            label4.Top = top + 4;
            cboHLV.Left = rightInput;
            cboHLV.Top = top;
            cboHLV.Width = inputWidth;

            // Row 3
            top += rowHeight;
            label3.Left = leftLabel;
            label3.Top = top + 4;
            txtThoiGian.Left = leftInput;
            txtThoiGian.Top = top;
            txtThoiGian.Width = inputWidth;

            label5.Left = rightLabel;
            label5.Top = top + 4;
            txtGiaTien.Left = rightInput;
            txtGiaTien.Top = top;
            txtGiaTien.Width = inputWidth;

            // Row 4
            top += rowHeight;
            label8.Left = leftLabel;
            label8.Top = top + 4;
            txtSoLuongToiDa.Left = leftInput;
            txtSoLuongToiDa.Top = top;
            txtSoLuongToiDa.Width = inputWidth;

            label9.Left = rightLabel;
            label9.Top = top + 4;
            txtSoBuoi.Left = rightInput;
            txtSoBuoi.Top = top;
            txtSoBuoi.Width = inputWidth;

            // Row 5
            top += rowHeight;
            label10.Left = leftLabel;
            label10.Top = top + 4;
            dtpNgayBatDau.Left = leftInput;
            dtpNgayBatDau.Top = top;
            dtpNgayBatDau.Width = inputWidth;

            label11.Left = rightLabel;
            label11.Top = top + 4;
            cboTrangThai.Left = rightInput;
            cboTrangThai.Top = top;
            cboTrangThai.Width = inputWidth;

            // Buttons row
            int btnY = top + 52;
            int btnWidth = 130;
            int btnSpacing = 14;
            int totalBtnWidth = (btnWidth * 4) + (btnSpacing * 3);
            int btnStart = left + (contentWidth - totalBtnWidth) / 2;

            btnThem.SetBounds(btnStart, btnY, btnWidth, 34);
            btnSua.SetBounds(btnThem.Right + btnSpacing, btnY, btnWidth, 34);
            btnKhoa.SetBounds(btnSua.Right + btnSpacing, btnY, btnWidth, 34);
            btnLamMoi.SetBounds(btnKhoa.Right + btnSpacing, btnY, btnWidth, 34);

            // Search row
            int searchY = btnThem.Bottom + 16;
            int searchLabelWidth = 70;
            int searchInputWidth = 260;
            int searchBtnWidth = 120;

            int searchTotal = searchLabelWidth + 10 + searchInputWidth + 14 + searchBtnWidth;
            int searchStart = left + 10;

            label7.Left = searchStart;
            label7.Top = searchY + 8;

            txtTimKiem.Left = label7.Right + 10;
            txtTimKiem.Top = searchY;
            txtTimKiem.Width = searchInputWidth;

            btnTimKiem.SetBounds(txtTimKiem.Right + 14, searchY, searchBtnWidth, 34);

            label12.Left = btnTimKiem.Right + 25;
            label12.Top = searchY + 8;
            cboLocHLV.Left = label12.Right + 10;
            cboLocHLV.Top = searchY;
            cboLocHLV.Width = 220;

            // Date filter row
            int filterY = txtTimKiem.Bottom + 12;

            label14.Left = searchStart;
            label14.Top = filterY + 6;
            dtpLocTuNgay.Left = label14.Right + 10;
            dtpLocTuNgay.Top = filterY;
            dtpLocTuNgay.Width = 180;

            label13.Left = dtpLocTuNgay.Right + 22;
            label13.Top = filterY + 6;
            dtpLocDenNgay.Left = label13.Right + 10;
            dtpLocDenNgay.Top = filterY;
            dtpLocDenNgay.Width = 180;

            // Grid
            int gridTop = dtpLocTuNgay.Bottom + 14;
            int gridHeight = ClientSize.Height - gridTop - 24;
            if (gridHeight < 220)
            {
                gridHeight = 220;
            }

            dgvLopHoc.SetBounds(left, gridTop, contentWidth, gridHeight);
        }

        private void LoadData(string tuKhoa = "", string maHLV = "", DateTime? tuNgay = null, DateTime? denNgay = null)
        {
            dgvLopHoc.DataSource = lopBUS.LayDanhSachLopHoc(tuKhoa, maHLV, tuNgay, denNgay);

            if (dgvLopHoc.Columns.Count > 0)
            {
                if (dgvLopHoc.Columns.Contains("MaPhongTap"))
                {
                    dgvLopHoc.Columns["MaPhongTap"].Visible = false;
                }

                if (dgvLopHoc.Columns.Contains("MaLop")) dgvLopHoc.Columns["MaLop"].HeaderText = "Mã Lớp";
                if (dgvLopHoc.Columns.Contains("TenLop")) dgvLopHoc.Columns["TenLop"].HeaderText = "Tên Lớp Học";
                if (dgvLopHoc.Columns.Contains("TenHLV")) dgvLopHoc.Columns["TenHLV"].HeaderText = "Huấn Luyện Viên";
                if (dgvLopHoc.Columns.Contains("ThoiGian")) dgvLopHoc.Columns["ThoiGian"].HeaderText = "Thời Gian";
                if (dgvLopHoc.Columns.Contains("PhongTap")) dgvLopHoc.Columns["PhongTap"].HeaderText = "Phòng Tập";

                if (dgvLopHoc.Columns.Contains("GiaTien"))
                {
                    dgvLopHoc.Columns["GiaTien"].HeaderText = "Giá Tiền";
                    dgvLopHoc.Columns["GiaTien"].DefaultCellStyle.Format = "N0";
                }

                if (dgvLopHoc.Columns.Contains("SoLuongToiDa")) dgvLopHoc.Columns["SoLuongToiDa"].HeaderText = "Sĩ Số Tối Đa";
                if (dgvLopHoc.Columns.Contains("SoBuoi")) dgvLopHoc.Columns["SoBuoi"].HeaderText = "Số Buổi";
                if (dgvLopHoc.Columns.Contains("NgayBatDau"))
                {
                    dgvLopHoc.Columns["NgayBatDau"].HeaderText = "Ngày Bắt Đầu";
                    dgvLopHoc.Columns["NgayBatDau"].DefaultCellStyle.Format = "dd/MM/yyyy";
                }
                if (dgvLopHoc.Columns.Contains("TrangThai")) dgvLopHoc.Columns["TrangThai"].HeaderText = "Trạng Thái";

                dgvLopHoc.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private void ucQuanLyLopHoc_Load(object sender, EventArgs e)
        {
            ApplyTheme();

            var listHLV = db.HuanLuyenViens.Where(x => x.IsActive == true).ToList();
            listHLV.Insert(0, new HuanLuyenVien { MaHLV = "", TenHLV = "-- Chưa phân công --" });
            cboHLV.DataSource = listHLV;
            cboHLV.DisplayMember = "TenHLV";
            cboHLV.ValueMember = "MaHLV";

            var listPhong = db.PhongTaps.Where(p => p.IsActive == true).ToList();
            listPhong.Insert(0, new PhongTap { MaPhong = "", TenPhong = "-- Chọn phòng tập --" });
            cboPhongTap.DataSource = listPhong;
            cboPhongTap.DisplayMember = "TenPhong";
            cboPhongTap.ValueMember = "MaPhong";

            var listLocHLV = db.HuanLuyenViens.Where(x => x.IsActive == true).ToList();
            listLocHLV.Insert(0, new HuanLuyenVien { MaHLV = "NONE", TenHLV = "-- Lớp chưa phân công HLV --" });
            listLocHLV.Insert(0, new HuanLuyenVien { MaHLV = "ALL", TenHLV = "-- Tất cả HLV --" });

            cboLocHLV.DataSource = listLocHLV;
            cboLocHLV.DisplayMember = "TenHLV";
            cboLocHLV.ValueMember = "MaHLV";

            if (cboTrangThai.Items.Count > 0)
            {
                cboTrangThai.SelectedIndex = 0;
            }

            txtTimKiem_Leave(sender, e);
            txtTimKiem.Enter += txtTimKiem_EnterTextBox;

            LoadData();

            if (!isLayoutHooked)
            {
                Resize += ucQuanLyLopHoc_Resize;
                isLayoutHooked = true;
            }

            ApplyResponsiveLayout();
        }

        private void ucQuanLyLopHoc_Resize(object sender, EventArgs e)
        {
            ApplyResponsiveLayout();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaLop.Clear();
            txtTenLop.Clear();
            txtThoiGian.Clear();
            txtGiaTien.Clear();
            txtSoLuongToiDa.Clear();
            txtSoBuoi.Clear();

            txtMaLop.Enabled = true;
            txtMaLop.Focus();

            cboHLV.SelectedIndex = 0;
            cboPhongTap.SelectedIndex = 0;
            if (cboTrangThai.Items.Count > 0)
            {
                cboTrangThai.SelectedIndex = 0;
            }

            dtpNgayBatDau.Value = DateTime.Now;

            cboLocHLV.SelectedIndex = 0;
            dtpLocTuNgay.Checked = false;
            dtpLocDenNgay.Checked = false;

            txtTimKiem.Text = "";
            txtTimKiem_Leave(sender, e);

            LoadData();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaLop.Text) || string.IsNullOrEmpty(txtTenLop.Text))
            {
                ModernMessageBox.Show("Vui lòng nhập đủ Mã lớp và Tên lớp!", "Cảnh báo", ModernMessageType.Warning);
                return;
            }

            decimal giaTien = 0;
            if (!string.IsNullOrEmpty(txtGiaTien.Text) && !decimal.TryParse(txtGiaTien.Text.Replace(",", ""), out giaTien))
            {
                ModernMessageBox.Show("Giá tiền phải là số hợp lệ!", "Lỗi", ModernMessageType.Error);
                return;
            }

            int siSo = 1;
            if (!string.IsNullOrEmpty(txtSoLuongToiDa.Text) && !int.TryParse(txtSoLuongToiDa.Text, out siSo))
            {
                ModernMessageBox.Show("Sĩ số tối đa phải là số nguyên!", "Lỗi", ModernMessageType.Error);
                return;
            }

            int soBuoi = 0;
            if (!string.IsNullOrEmpty(txtSoBuoi.Text) && !int.TryParse(txtSoBuoi.Text, out soBuoi))
            {
                ModernMessageBox.Show("Số buổi học phải là số nguyên!", "Lỗi", ModernMessageType.Error);
                return;
            }

            string hlvChon = cboHLV.SelectedValue == null ? null : cboHLV.SelectedValue.ToString();
            string phongChon = cboPhongTap.SelectedValue == null ? null : cboPhongTap.SelectedValue.ToString();

            LopHoc lop = new LopHoc()
            {
                MaLop = txtMaLop.Text.Trim(),
                TenLop = txtTenLop.Text.Trim(),
                MaHLV = string.IsNullOrEmpty(hlvChon) ? null : hlvChon,
                ThoiGian = txtThoiGian.Text.Trim(),
                PhongTap = string.IsNullOrEmpty(phongChon) ? null : phongChon,
                GiaTien = giaTien,
                SoLuongToiDa = siSo,
                SoBuoi = soBuoi,
                NgayBatDau = dtpNgayBatDau.Value.Date,
                TrangThai = cboTrangThai.SelectedItem == null ? "Chuẩn bị" : cboTrangThai.SelectedItem.ToString(),
                IsActive = true
            };

            string kq = lopBUS.ThemLop(lop);
            if (kq == "")
            {
                ModernMessageBox.Show("Thêm thành công!", "Thông báo", ModernMessageType.Success);
                btnLamMoi_Click(sender, e);
            }
            else
            {
                ModernMessageBox.Show(kq, "Lỗi", ModernMessageType.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaLop.Text))
            {
                return;
            }

            decimal giaTien = 0;
            if (!string.IsNullOrEmpty(txtGiaTien.Text) && !decimal.TryParse(txtGiaTien.Text.Replace(",", ""), out giaTien))
            {
                return;
            }

            int siSo = 1;
            if (!string.IsNullOrEmpty(txtSoLuongToiDa.Text) && !int.TryParse(txtSoLuongToiDa.Text, out siSo))
            {
                return;
            }

            int soBuoi = 0;
            if (!string.IsNullOrEmpty(txtSoBuoi.Text) && !int.TryParse(txtSoBuoi.Text, out soBuoi))
            {
                return;
            }

            string hlvChon = cboHLV.SelectedValue == null ? null : cboHLV.SelectedValue.ToString();
            string phongChon = cboPhongTap.SelectedValue == null ? null : cboPhongTap.SelectedValue.ToString();

            LopHoc lop = new LopHoc()
            {
                MaLop = txtMaLop.Text.Trim(),
                TenLop = txtTenLop.Text.Trim(),
                MaHLV = string.IsNullOrEmpty(hlvChon) ? null : hlvChon,
                ThoiGian = txtThoiGian.Text.Trim(),
                GiaTien = giaTien,
                SoLuongToiDa = siSo,
                SoBuoi = soBuoi,
                PhongTap = string.IsNullOrEmpty(phongChon) ? null : phongChon,
                NgayBatDau = dtpNgayBatDau.Value.Date,
                TrangThai = cboTrangThai.SelectedItem == null ? "Chuẩn bị" : cboTrangThai.SelectedItem.ToString()
            };

            string kq = lopBUS.SuaLop(lop);
            if (kq == "")
            {
                ModernMessageBox.Show("Sửa thành công!", "Thông báo", ModernMessageType.Success);
                btnLamMoi_Click(sender, e);
            }
            else
            {
                ModernMessageBox.Show(kq, "Lỗi", ModernMessageType.Error);
            }
        }

        private void btnKhoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaLop.Text))
            {
                return;
            }

            if (ModernMessageBox.Show("Cập nhật trạng thái lớp học này?", "Xác nhận", MessageBoxButtons.YesNo, ModernMessageType.Question) == DialogResult.Yes)
            {
                string kq = lopBUS.KhoaLop(txtMaLop.Text.Trim());
                if (kq == "")
                {
                    ModernMessageBox.Show("Đã cập nhật!", "Thông báo", ModernMessageType.Success);
                    btnLamMoi_Click(sender, e);
                }
                else
                {
                    ModernMessageBox.Show(kq, "Lỗi", ModernMessageType.Error);
                }
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string tuKhoa = txtTimKiem.Text == "Nhập từ khóa tìm kiếm..." ? "" : txtTimKiem.Text.Trim();
            string locHLV = cboLocHLV.SelectedValue == null ? "" : cboLocHLV.SelectedValue.ToString();

            DateTime? tuNgay = dtpLocTuNgay.Checked ? (DateTime?)dtpLocTuNgay.Value.Date : null;
            DateTime? denNgay = dtpLocDenNgay.Checked ? (DateTime?)dtpLocDenNgay.Value.Date : null;

            LoadData(tuKhoa, locHLV, tuNgay, denNgay);
        }

        private void dgvLopHoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvLopHoc.Rows[e.RowIndex];

                txtMaLop.Text = row.Cells["MaLop"].Value == null ? "" : row.Cells["MaLop"].Value.ToString();
                txtTenLop.Text = row.Cells["TenLop"].Value == null ? "" : row.Cells["TenLop"].Value.ToString();
                txtThoiGian.Text = row.Cells["ThoiGian"].Value == null ? "" : row.Cells["ThoiGian"].Value.ToString();

                if (row.Cells["GiaTien"].Value != null)
                {
                    txtGiaTien.Text = Convert.ToDecimal(row.Cells["GiaTien"].Value).ToString("0");
                }
                else
                {
                    txtGiaTien.Text = "0";
                }

                txtSoLuongToiDa.Text = row.Cells["SoLuongToiDa"].Value == null ? "1" : row.Cells["SoLuongToiDa"].Value.ToString();
                txtSoBuoi.Text = row.Cells["SoBuoi"].Value == null ? "0" : row.Cells["SoBuoi"].Value.ToString();

                if (row.Cells["NgayBatDau"].Value != null)
                {
                    dtpNgayBatDau.Value = Convert.ToDateTime(row.Cells["NgayBatDau"].Value);
                }

                if (row.Cells["TrangThai"].Value != null)
                {
                    cboTrangThai.Text = row.Cells["TrangThai"].Value.ToString();
                }

                string tenHLV = row.Cells["TenHLV"].Value == null ? "" : row.Cells["TenHLV"].Value.ToString();
                int idxHLV = cboHLV.FindStringExact(tenHLV);
                cboHLV.SelectedIndex = idxHLV >= 0 ? idxHLV : 0;

                string tenPhong = row.Cells["PhongTap"].Value == null ? "" : row.Cells["PhongTap"].Value.ToString();
                int idxPhong = cboPhongTap.FindStringExact(tenPhong);
                cboPhongTap.SelectedIndex = idxPhong >= 0 ? idxPhong : 0;

                txtMaLop.Enabled = false;
            }
        }

        private void txtTimKiem_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTimKiem.Text))
            {
                txtTimKiem.Text = "Nhập từ khóa tìm kiếm...";
                txtTimKiem.ForeColor = Color.Gray;
            }
        }

        private void btnTimKiem_Enter(object sender, EventArgs e)
        {
            if (txtTimKiem.Text == "Nhập từ khóa tìm kiếm...")
            {
                txtTimKiem.Text = "";
                txtTimKiem.ForeColor = Color.Black;
            }
        }

        private void txtTimKiem_EnterTextBox(object sender, EventArgs e)
        {
            if (txtTimKiem.Text == "Nhập từ khóa tìm kiếm...")
            {
                txtTimKiem.Text = "";
                txtTimKiem.ForeColor = Color.Black;
            }
        }
    }
}
