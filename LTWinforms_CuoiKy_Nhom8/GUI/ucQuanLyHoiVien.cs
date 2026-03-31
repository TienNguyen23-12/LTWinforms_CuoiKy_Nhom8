using LTWinforms_CuoiKy_Nhom8.BUS;
using LTWinforms_CuoiKy_Nhom8.DAL;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace LTWinforms_CuoiKy_Nhom8.GUI
{
    public partial class ucQuanLyHoiVien : UserControl
    {
        private readonly HoiVienBUS hvBUS = new HoiVienBUS();
        private bool isThemeApplied;
        private bool isLayoutHooked;

        public ucQuanLyHoiVien()
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

            StyleInput(txtMaHoiVien);
            StyleInput(txtHoTen);
            StyleInput(txtSDT);
            StyleInput(txtTimKiem);
            StyleInput(cboGioiTinh);
            StyleInput(dtpNgaySinh);

            StylePrimaryButton(btnThem);
            StyleSecondaryButton(btnSua);
            StyleDangerButton(btnKhoa);
            StyleSecondaryButton(btnLamMoi);
            StylePrimaryButton(btnTimKiem);

            StyleGrid(dgvHoiVien);

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

            DateTimePicker dateTimePicker = control as DateTimePicker;
            if (dateTimePicker != null)
            {
                dateTimePicker.CalendarForeColor = Color.FromArgb(44, 62, 80);
            }
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
            int contentWidth = Math.Min(1080, ClientSize.Width - 80);
            if (contentWidth < 860)
            {
                contentWidth = 860;
            }

            int left = Math.Max(20, (ClientSize.Width - contentWidth) / 2);
            int top = 30;

            int leftColLabel = left + 30;
            int leftColInput = left + 150;

            int rightColLabel = left + (contentWidth / 2) + 10;
            int rightColInput = rightColLabel + 95;

            int inputWidth = 230;
            int rowHeight = 42;

            label1.Left = leftColLabel;
            label1.Top = top + 4;
            txtMaHoiVien.Left = leftColInput;
            txtMaHoiVien.Top = top;
            txtMaHoiVien.Width = inputWidth;

            label4.Left = rightColLabel;
            label4.Top = top + 4;
            cboGioiTinh.Left = rightColInput;
            cboGioiTinh.Top = top;
            cboGioiTinh.Width = inputWidth;

            top += rowHeight;
            label2.Left = leftColLabel;
            label2.Top = top + 4;
            txtHoTen.Left = leftColInput;
            txtHoTen.Top = top;
            txtHoTen.Width = inputWidth;

            label5.Left = rightColLabel;
            label5.Top = top + 4;
            dtpNgaySinh.Left = rightColInput;
            dtpNgaySinh.Top = top;
            dtpNgaySinh.Width = inputWidth;

            top += rowHeight;
            label3.Left = leftColLabel;
            label3.Top = top + 4;
            txtSDT.Left = leftColInput;
            txtSDT.Top = top;
            txtSDT.Width = inputWidth;

            // Buttons row
            int btnY = top + 56;
            int btnWidth = 130;
            int btnSpacing = 14;
            int totalBtnWidth = (btnWidth * 4) + (btnSpacing * 3);
            int btnStart = left + (contentWidth - totalBtnWidth) / 2;

            btnThem.SetBounds(btnStart, btnY, btnWidth, 34);
            btnSua.SetBounds(btnThem.Right + btnSpacing, btnY, btnWidth, 34);
            btnKhoa.SetBounds(btnSua.Right + btnSpacing, btnY, btnWidth, 34);
            btnLamMoi.SetBounds(btnKhoa.Right + btnSpacing, btnY, btnWidth, 34);

            // Search row
            int searchY = btnThem.Bottom + 18;
            int searchInputWidth = 280;
            int searchBtnWidth = 120;

            int searchTotal = 70 + 10 + searchInputWidth + 14 + searchBtnWidth;
            int searchStart = left + (contentWidth - searchTotal) / 2;

            label6.Left = searchStart;
            label6.Top = searchY + 8;

            txtTimKiem.Left = label6.Right + 10;
            txtTimKiem.Top = searchY;
            txtTimKiem.Width = searchInputWidth;

            btnTimKiem.SetBounds(txtTimKiem.Right + 14, searchY, searchBtnWidth, 34);

            // Grid
            int gridTop = txtTimKiem.Bottom + 18;
            int gridHeight = ClientSize.Height - gridTop - 24;
            if (gridHeight < 220)
            {
                gridHeight = 220;
            }

            dgvHoiVien.SetBounds(left, gridTop, contentWidth, gridHeight);
        }

        private void LoadData(string tuKhoa = "")
        {
            dgvHoiVien.DataSource = hvBUS.LayDanhSachHoiVien(tuKhoa);

            if (dgvHoiVien.Columns.Count > 0)
            {
                dgvHoiVien.Columns["MaHoiVien"].HeaderText = "Mã hội viên";
                dgvHoiVien.Columns["HoTen"].HeaderText = "Họ tên";
                dgvHoiVien.Columns["GioiTinh"].HeaderText = "Giới tính";
                dgvHoiVien.Columns["NgaySinh"].HeaderText = "Ngày sinh";
                dgvHoiVien.Columns["SDT"].HeaderText = "Số điện thoại";
                dgvHoiVien.Columns["NgayDangKy"].HeaderText = "Ngày đăng ký";
                dgvHoiVien.Columns["NgayHetHan"].HeaderText = "Ngày hết hạn thẻ";

                dgvHoiVien.Columns["HoTen"].Width = 150;
                dgvHoiVien.Columns["NgayDangKy"].DefaultCellStyle.Format = "dd/MM/yyyy";
                dgvHoiVien.Columns["NgayHetHan"].DefaultCellStyle.Format = "dd/MM/yyyy";

                dgvHoiVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private void ucQuanLyHoiVien_Load(object sender, EventArgs e)
        {
            ApplyTheme();
            LoadData();

            if (!isLayoutHooked)
            {
                Resize += ucQuanLyHoiVien_Resize;
                isLayoutHooked = true;
            }

            ApplyResponsiveLayout();
        }

        private void ucQuanLyHoiVien_Resize(object sender, EventArgs e)
        {
            ApplyResponsiveLayout();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaHoiVien.Clear();
            txtHoTen.Clear();
            txtSDT.Clear();
            cboGioiTinh.SelectedIndex = -1;
            dtpNgaySinh.Value = DateTime.Now;
            txtTimKiem.Clear();

            txtMaHoiVien.Enabled = true;
            txtMaHoiVien.Focus();
            LoadData();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaHoiVien.Text) || string.IsNullOrEmpty(txtHoTen.Text))
            {
                ModernMessageBox.Show("Vui lòng nhập đủ Mã và Họ Tên!", "Cảnh báo", ModernMessageType.Warning);
                return;
            }

            HoiVien hv = new HoiVien()
            {
                MaHoiVien = txtMaHoiVien.Text.Trim(),
                HoTen = txtHoTen.Text.Trim(),
                GioiTinh = cboGioiTinh.Text,
                SDT = txtSDT.Text.Trim(),
                NgaySinh = dtpNgaySinh.Value,
                IsActive = true,
                NgayDangKy = DateTime.Now
            };

            string kq = hvBUS.ThemHoiVien(hv);
            if (kq == "")
            {
                ModernMessageBox.Show("Thêm hội viên thành công!", "Thông báo", ModernMessageType.Success);
                LoadData();
                btnLamMoi_Click(sender, e);
            }
            else
            {
                ModernMessageBox.Show(kq, "Lỗi", ModernMessageType.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            HoiVien hv = new HoiVien()
            {
                MaHoiVien = txtMaHoiVien.Text.Trim(),
                HoTen = txtHoTen.Text.Trim(),
                GioiTinh = cboGioiTinh.Text,
                SDT = txtSDT.Text.Trim(),
                NgaySinh = dtpNgaySinh.Value
            };

            string kq = hvBUS.SuaHoiVien(hv);
            if (kq == "")
            {
                ModernMessageBox.Show("Cập nhật thành công!", "Thông báo", ModernMessageType.Success);
                LoadData();
                btnLamMoi_Click(sender, e);
            }
            else
            {
                ModernMessageBox.Show(kq, "Lỗi", ModernMessageType.Error);
            }
        }

        private void btnKhoa_Click(object sender, EventArgs e)
        {
            if (ModernMessageBox.Show("Bạn có chắc muốn khóa hội viên này?", "Xác nhận", MessageBoxButtons.YesNo, ModernMessageType.Question) == DialogResult.Yes)
            {
                string kq = hvBUS.KhoaHoiVien(txtMaHoiVien.Text.Trim());
                if (kq == "")
                {
                    ModernMessageBox.Show("Đã khóa hội viên.", "Thông báo", ModernMessageType.Success);
                    LoadData();
                    btnLamMoi_Click(sender, e);
                }
                else
                {
                    ModernMessageBox.Show(kq, "Lỗi", ModernMessageType.Error);
                }
            }
        }

        private void dgvHoiVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvHoiVien.Rows[e.RowIndex];
                txtMaHoiVien.Text = row.Cells["MaHoiVien"].Value == null ? "" : row.Cells["MaHoiVien"].Value.ToString();
                txtHoTen.Text = row.Cells["HoTen"].Value == null ? "" : row.Cells["HoTen"].Value.ToString();
                cboGioiTinh.Text = row.Cells["GioiTinh"].Value == null ? "" : row.Cells["GioiTinh"].Value.ToString();
                txtSDT.Text = row.Cells["SDT"].Value == null ? "" : row.Cells["SDT"].Value.ToString();

                if (row.Cells["NgaySinh"].Value != null)
                {
                    dtpNgaySinh.Value = Convert.ToDateTime(row.Cells["NgaySinh"].Value);
                }

                txtMaHoiVien.Enabled = false;
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            LoadData(txtTimKiem.Text.Trim());
        }
    }
}
