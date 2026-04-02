using LTWinforms_CuoiKy_Nhom8.BUS;
using LTWinforms_CuoiKy_Nhom8.DAL;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace LTWinforms_CuoiKy_Nhom8.GUI
{
    public partial class ucQuanLyGoiTap : UserControl
    {
        private readonly GoiTapBUS gtBUS = new GoiTapBUS();
        private bool isThemeApplied;
        private bool isLayoutHooked;
        private const string SearchPlaceholder = "Nhập từ khóa tìm kiếm";
        private bool isSearchPlaceholderHooked;

        public ucQuanLyGoiTap()
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
            StyleLabel(label6);
            StyleLabel(label7);

            StyleInput(txtMaGoi);
            StyleInput(txtTenGoi);
            StyleInput(txtThoiHan);
            StyleInput(txtGiaTien);
            StyleInput(txtTimKiem);

            StylePrimaryButton(btnThem);
            StyleSecondaryButton(btnSua);
            StyleDangerButton(btnKhoa);
            StyleSecondaryButton(btnLamMoi);
            StylePrimaryButton(btnTimKiem);

            StyleGrid(dgvGoiTap);

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
            txtMaGoi.Left = leftColInput;
            txtMaGoi.Top = top;
            txtMaGoi.Width = inputWidth;

            label6.Left = rightColLabel;
            label6.Top = top + 4;
            txtGiaTien.Left = rightColInput;
            txtGiaTien.Top = top;
            txtGiaTien.Width = inputWidth;

            top += rowHeight;
            label2.Left = leftColLabel;
            label2.Top = top + 4;
            txtTenGoi.Left = leftColInput;
            txtTenGoi.Top = top;
            txtTenGoi.Width = inputWidth;

            top += rowHeight;
            label3.Left = leftColLabel;
            label3.Top = top + 4;
            txtThoiHan.Left = leftColInput;
            txtThoiHan.Top = top;
            txtThoiHan.Width = inputWidth;

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
            int searchY = btnThem.Bottom + 16;
            int searchInputWidth = 280;
            int searchBtnWidth = 120;
            int searchLabelWidth = label7.PreferredWidth;

            int searchTotal = searchLabelWidth + 10 + searchInputWidth + 14 + searchBtnWidth;
            int searchStart = left + (contentWidth - searchTotal) / 2;

            label7.Left = searchStart;
            txtTimKiem.Left = label7.Right + 10;
            txtTimKiem.Width = searchInputWidth;
            btnTimKiem.SetBounds(txtTimKiem.Right + 14, 0, searchBtnWidth, 34);

            int searchRowHeight = Math.Max(Math.Max(label7.Height, txtTimKiem.Height), btnTimKiem.Height);
            label7.Top = searchY + (searchRowHeight - label7.Height) / 2;
            txtTimKiem.Top = searchY + (searchRowHeight - txtTimKiem.Height) / 2;
            btnTimKiem.Top = searchY + (searchRowHeight - btnTimKiem.Height) / 2;

            // Grid (cách hàng search 1 khoảng nhỏ)
            int gridTop = Math.Max(txtTimKiem.Bottom, btnTimKiem.Bottom) + 10;
            int gridHeight = ClientSize.Height - gridTop - 24;
            if (gridHeight < 220)
            {
                gridHeight = 220;
            }

            dgvGoiTap.SetBounds(left, gridTop, contentWidth, gridHeight);
        }

        private void LoadData(string tuKhoa = "")
        {
            dgvGoiTap.DataSource = gtBUS.LayDanhSachGoiTap(tuKhoa);

            if (dgvGoiTap.Columns.Count > 0)
            {
                dgvGoiTap.Columns["MaGoi"].HeaderText = "Mã gói";
                dgvGoiTap.Columns["TenGoi"].HeaderText = "Tên gói tập";
                dgvGoiTap.Columns["ThoiHanThang"].HeaderText = "Thời hạn (Tháng)";
                dgvGoiTap.Columns["GiaTien"].HeaderText = "Giá tiền (VNĐ)";

                dgvGoiTap.Columns["GiaTien"].DefaultCellStyle.Format = "N0";
                dgvGoiTap.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private void ucQuanLyGoiTap_Load(object sender, EventArgs e)
        {
            ApplyTheme();
            SetupSearchPlaceholder();
            LoadData();

            if (!isLayoutHooked)
            {
                Resize += ucQuanLyGoiTap_Resize;
                isLayoutHooked = true;
            }

            ApplyResponsiveLayout();
        }

        private void ucQuanLyGoiTap_Resize(object sender, EventArgs e)
        {
            ApplyResponsiveLayout();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaGoi.Text.Trim()) || string.IsNullOrEmpty(txtTenGoi.Text.Trim()) || string.IsNullOrEmpty(txtThoiHan.Text.Trim()) || string.IsNullOrEmpty(txtGiaTien.Text.Trim()))
{
    ModernMessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Cảnh báo", ModernMessageType.Warning);
    return;
}

int thoiHan;
decimal giaTien;

if (!int.TryParse(txtThoiHan.Text.Trim(), out thoiHan))
{
    ModernMessageBox.Show("Thời hạn phải là số nguyên.", "Lỗi", ModernMessageType.Error);
    return;
}

if (!decimal.TryParse(txtGiaTien.Text.Trim(), out giaTien))
{
    ModernMessageBox.Show("Giá tiền không hợp lệ.", "Lỗi", ModernMessageType.Error);
    return;
}

GoiTap gt = new GoiTap()
{
    MaGoi = txtMaGoi.Text.Trim(),
    TenGoi = txtTenGoi.Text.Trim(),
    ThoiHanThang = thoiHan,
    GiaTien = giaTien,
    IsActive = true
};

string kq = gtBUS.ThemGoiTap(gt);
if (kq == "")
{
    ModernMessageBox.Show("Thêm thành công.", "Thành công", ModernMessageType.Success);
    btnLamMoi_Click(sender, e);
}
else
{
    ModernMessageBox.Show(kq, "Lỗi", ModernMessageType.Error);
}
}

private void btnSua_Click(object sender, EventArgs e)
{
    int thoiHan;
    decimal giaTien;

    if (!int.TryParse(txtThoiHan.Text.Trim(), out thoiHan))
    {
        ModernMessageBox.Show("Thời hạn phải là số nguyên.", "Lỗi", ModernMessageType.Error);
        return;
    }

    if (!decimal.TryParse(txtGiaTien.Text.Trim(), out giaTien))
    {
        ModernMessageBox.Show("Giá tiền không hợp lệ.", "Lỗi", ModernMessageType.Error);
        return;
    }

    GoiTap gt = new GoiTap()
    {
        MaGoi = txtMaGoi.Text.Trim(),
        TenGoi = txtTenGoi.Text.Trim(),
        ThoiHanThang = thoiHan,
        GiaTien = giaTien
    };

    string kq = gtBUS.SuaGoiTap(gt);
    if (kq == "")
    {
        ModernMessageBox.Show("Sửa thành công.", "Thành công", ModernMessageType.Success);
        btnLamMoi_Click(sender, e);
    }
    else
    {
        ModernMessageBox.Show(kq, "Lỗi", ModernMessageType.Error);
    }
}

private void btnKhoa_Click(object sender, EventArgs e)
{
    if (ModernMessageBox.Show("Khóa gói tập này?", "Xác nhận", MessageBoxButtons.YesNo, ModernMessageType.Question) == DialogResult.Yes)
    {
        string kq = gtBUS.KhoaGoiTap(txtMaGoi.Text.Trim());
        if (kq == "")
        {
            ModernMessageBox.Show("Đã khóa.", "Thành công", ModernMessageType.Success);
            btnLamMoi_Click(sender, e);
        }
        else
        {
            ModernMessageBox.Show(kq, "Lỗi", ModernMessageType.Error);
        }
    }
}

private void btnLamMoi_Click(object sender, EventArgs e)
{
    txtMaGoi.Clear();
    txtTenGoi.Clear();
    txtThoiHan.Clear();
    txtGiaTien.Clear();
    SetSearchPlaceholder();

    txtMaGoi.Enabled = true;
    txtMaGoi.Focus();
    LoadData();
}

private void btnTimKiem_Click(object sender, EventArgs e)
{
    string tuKhoa = txtTimKiem.Text.Trim();
    if (tuKhoa == SearchPlaceholder)
    {
        tuKhoa = "";
    }

    LoadData(tuKhoa);
}

private void dgvGoiTap_CellClick(object sender, DataGridViewCellEventArgs e)
{
    if (e.RowIndex >= 0)
    {
        DataGridViewRow row = dgvGoiTap.Rows[e.RowIndex];
        txtMaGoi.Text = row.Cells["MaGoi"].Value == null ? "" : row.Cells["MaGoi"].Value.ToString();
        txtTenGoi.Text = row.Cells["TenGoi"].Value == null ? "" : row.Cells["TenGoi"].Value.ToString();
        txtThoiHan.Text = row.Cells["ThoiHanThang"].Value == null ? "" : row.Cells["ThoiHanThang"].Value.ToString();

        if (row.Cells["GiaTien"].Value != null)
        {
            txtGiaTien.Text = Convert.ToDecimal(row.Cells["GiaTien"].Value).ToString("0");
        }
        else
        {
            txtGiaTien.Text = "";
        }

        txtMaGoi.Enabled = false;
    }
}

private void SetupSearchPlaceholder()
{
    if (isSearchPlaceholderHooked)
    {
        return;
    }

    txtTimKiem.Enter += txtTimKiem_Enter;
    txtTimKiem.Leave += txtTimKiem_Leave;
    isSearchPlaceholderHooked = true;

    SetSearchPlaceholder();
}

private void SetSearchPlaceholder()
{
    txtTimKiem.Text = SearchPlaceholder;
    txtTimKiem.ForeColor = Color.Gray;
}

private void txtTimKiem_Enter(object sender, EventArgs e)
{
    if (txtTimKiem.Text == SearchPlaceholder)
    {
        txtTimKiem.Text = "";
        txtTimKiem.ForeColor = Color.FromArgb(44, 62, 80);
    }
}

private void txtTimKiem_Leave(object sender, EventArgs e)
{
    if (string.IsNullOrWhiteSpace(txtTimKiem.Text))
    {
        SetSearchPlaceholder();
    }
}
    }
}
