using LTWinforms_CuoiKy_Nhom8.BUS;
using LTWinforms_CuoiKy_Nhom8.DAL;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace LTWinforms_CuoiKy_Nhom8.GUI
{
    public partial class ucQuanLyPhongTap : UserControl
    {
        private readonly PhongTapBUS ptBUS = new PhongTapBUS();
        private readonly QLTTDataContext db = new QLTTDataContext();
        private bool isThemeApplied;
        private bool isLayoutHooked;
        private const string SearchPlaceholder = "Nhập từ khóa tìm kiếm";
        private bool isSearchPlaceholderHooked;

        public ucQuanLyPhongTap()
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
            StyleLabel(label7);

            StyleInput(txtMaPhong);
            StyleInput(txtTenPhong);
            StyleInput(txtTimKiem);
            StyleInput(cboPhuTrach);
            StyleInput(txtGhiChu);

            StylePrimaryButton(btnThem);
            StyleSecondaryButton(btnSua);
            StyleDangerButton(btnKhoa);
            StyleSecondaryButton(btnLamMoi);
            StylePrimaryButton(btnTimKiem);

            StyleGrid(dgvPhongTap);

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

            RichTextBox richTextBox = control as RichTextBox;
            if (richTextBox != null)
            {
                richTextBox.BorderStyle = BorderStyle.FixedSingle;
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

        private void ApplyResponsiveLayout()
        {
            int contentWidth = Math.Min(1100, ClientSize.Width - 80);
            if (contentWidth < 900)
            {
                contentWidth = 900;
            }

            int left = Math.Max(20, (ClientSize.Width - contentWidth) / 2);
            int top = 30;

            int leftColLabel = left + 20;
            int leftColInput = left + 130;
            int leftInputWidth = 260;

            int rightColLabel = left + 460;
            int rightColInput = rightColLabel + 90;
            int rightInputWidth = 270;

            int rowHeight = 42;

            label1.Left = leftColLabel;
            label1.Top = top + 4;
            txtMaPhong.Left = leftColInput;
            txtMaPhong.Top = top;
            txtMaPhong.Width = leftInputWidth;
            txtMaPhong.Height = 30;

            label4.Left = rightColLabel;
            label4.Top = top + 4;
            txtGhiChu.Left = rightColInput;
            txtGhiChu.Top = top;
            txtGhiChu.Width = rightInputWidth;
            txtGhiChu.Height = 96;

            top += rowHeight;
            label2.Left = leftColLabel;
            label2.Top = top + 4;
            txtTenPhong.Left = leftColInput;
            txtTenPhong.Top = top;
            txtTenPhong.Width = leftInputWidth;
            txtTenPhong.Height = 30;

            top += rowHeight;
            label3.Left = leftColLabel;
            label3.Top = top + 4;
            cboPhuTrach.Left = leftColInput;
            cboPhuTrach.Top = top;
            cboPhuTrach.Width = leftInputWidth;
            cboPhuTrach.DropDownWidth = leftInputWidth;
            cboPhuTrach.Height = 30;

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
            txtTimKiem.Height = 30;
            btnTimKiem.SetBounds(txtTimKiem.Right + 14, 0, searchBtnWidth, 34);

            int searchRowHeight = Math.Max(Math.Max(label7.Height, txtTimKiem.Height), btnTimKiem.Height);
            label7.Top = searchY + (searchRowHeight - label7.Height) / 2;
            txtTimKiem.Top = searchY + (searchRowHeight - txtTimKiem.Height) / 2;
            btnTimKiem.Top = searchY + (searchRowHeight - btnTimKiem.Height) / 2;

            // Grid
            int gridTop = Math.Max(txtTimKiem.Bottom, btnTimKiem.Bottom) + 10;
            int gridHeight = ClientSize.Height - gridTop - 24;
            if (gridHeight < 220)
            {
                gridHeight = 220;
            }

            dgvPhongTap.SetBounds(left, gridTop, contentWidth, gridHeight);
        }

        private void LoadNhanVienPhuTrach()
        {
            var listNhanVien = db.TaiKhoans.Where(x => x.Role == 2 && x.IsActive == true).ToList();
            listNhanVien.Insert(0, new TaiKhoan
            {
                Id = 0,
                Username = "-- Chưa phân công --"
            });

            cboPhuTrach.DataSource = listNhanVien;
            cboPhuTrach.DisplayMember = "Username";
            cboPhuTrach.ValueMember = "Id";
        }

        private void LoadData(string tuKhoa = "")
        {
            dgvPhongTap.DataSource = ptBUS.LayDanhSach(tuKhoa);

            if (dgvPhongTap.Columns.Count > 0)
            {
                dgvPhongTap.Columns["IdNguoiPhuTrach"].Visible = false;

                dgvPhongTap.Columns["MaPhong"].HeaderText = "Mã Phòng";
                dgvPhongTap.Columns["TenPhong"].HeaderText = "Tên Phòng";
                dgvPhongTap.Columns["NguoiPhuTrach"].HeaderText = "Người Phụ Trách";
                dgvPhongTap.Columns["GhiChu"].HeaderText = "Ghi Chú";
                dgvPhongTap.Columns["TrangThai"].HeaderText = "Trạng Thái";

                dgvPhongTap.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private void ucQuanLyPhongTap_Load(object sender, EventArgs e)
        {
            ApplyTheme();
            SetupSearchPlaceholder();
            LoadNhanVienPhuTrach();
            LoadData();

            if (!isLayoutHooked)
            {
                Resize += ucQuanLyPhongTap_Resize;
                isLayoutHooked = true;
            }

            ApplyResponsiveLayout();
            BeginInvoke(new Action(ApplyResponsiveLayout));
        }

        private void ucQuanLyPhongTap_Resize(object sender, EventArgs e)
        {
            ApplyResponsiveLayout();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaPhong.Text) || string.IsNullOrEmpty(txtTenPhong.Text))
            {
                ModernMessageBox.Show("Vui lòng nhập đủ Mã và Tên phòng!", "Cảnh báo", ModernMessageType.Warning);
                return;
            }

            int idChon = Convert.ToInt32(cboPhuTrach.SelectedValue);
            PhongTap pt = new PhongTap()
            {
                MaPhong = txtMaPhong.Text.Trim(),
                TenPhong = txtTenPhong.Text.Trim(),
                GhiChu = txtGhiChu.Text.Trim(),
                IdNguoiPhuTrach = idChon == 0 ? (int?)null : idChon,
                IsActive = true
            };

            string kq = ptBUS.ThemPhong(pt);
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
            if (string.IsNullOrEmpty(txtMaPhong.Text))
            {
                return;
            }

            int idChon = Convert.ToInt32(cboPhuTrach.SelectedValue);
            PhongTap pt = new PhongTap()
            {
                MaPhong = txtMaPhong.Text.Trim(),
                TenPhong = txtTenPhong.Text.Trim(),
                GhiChu = txtGhiChu.Text.Trim(),
                IdNguoiPhuTrach = idChon == 0 ? (int?)null : idChon
            };

            string kq = ptBUS.SuaPhong(pt);
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
            if (string.IsNullOrEmpty(txtMaPhong.Text))
            {
                return;
            }

            string kq = ptBUS.KhoaPhong(txtMaPhong.Text.Trim());
            if (kq == "")
            {
                ModernMessageBox.Show("Cập nhật trạng thái thành công!", "Thông báo", ModernMessageType.Success);
                btnLamMoi_Click(sender, e);
            }
            else
            {
                ModernMessageBox.Show(kq, "Lỗi", ModernMessageType.Error);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaPhong.Clear();
            txtTenPhong.Clear();
            txtGhiChu.Clear();
            SetSearchPlaceholder();

            cboPhuTrach.SelectedValue = 0;
            txtMaPhong.Enabled = true;
            btnKhoa.Text = "Khóa";

            LoadData();
            txtMaPhong.Focus();
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

        private void dgvPhongTap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvPhongTap.Rows[e.RowIndex];
                txtMaPhong.Text = row.Cells["MaPhong"].Value == null ? "" : row.Cells["MaPhong"].Value.ToString();
                txtTenPhong.Text = row.Cells["TenPhong"].Value == null ? "" : row.Cells["TenPhong"].Value.ToString();
                txtGhiChu.Text = row.Cells["GhiChu"].Value == null ? "" : row.Cells["GhiChu"].Value.ToString();

                if (row.Cells["IdNguoiPhuTrach"].Value != null)
                {
                    cboPhuTrach.SelectedValue = Convert.ToInt32(row.Cells["IdNguoiPhuTrach"].Value);
                }
                else
                {
                    cboPhuTrach.SelectedValue = 0;
                }

                txtMaPhong.Enabled = false;
                btnKhoa.Text = row.Cells["TrangThai"].Value != null && row.Cells["TrangThai"].Value.ToString() == "Tạm ngưng"
                    ? "Mở khóa"
                    : "Khóa";
            }
        }
    }
}
