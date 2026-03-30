using LTWinforms_CuoiKy_Nhom8.BUS;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace LTWinforms_CuoiKy_Nhom8.GUI
{
    public partial class ucQuanTriHeThong : UserControl
    {
        private readonly QuanTriBUS qtBUS = new QuanTriBUS();
        private int idDangChon = 0;
        private bool isThemeApplied;
        private bool isLayoutHooked;

        public ucQuanTriHeThong()
        {
            InitializeComponent();
        }

        private void ApplyTheme()
        {
            if (isThemeApplied)
            {
                return;
            }

            BackColor = ModernTheme.PageBackground;

            ModernTheme.StyleLabel(label1, true);
            ModernTheme.StyleLabel(label2);
            ModernTheme.StyleLabel(label3);
            ModernTheme.StyleLabel(label4);
            ModernTheme.StyleLabel(label5);

            ModernTheme.StyleInput(txtUsername);
            ModernTheme.StyleInput(txtTenHienThi);
            ModernTheme.StyleInput(txtLuongCoBan);
            ModernTheme.StyleInput(cboVaiTro);

            ModernTheme.StyleButton(btnLuuThongTin, Color.FromArgb(58, 129, 214), Color.White);
            ModernTheme.StyleButton(btnCapNhatLuong, Color.FromArgb(58, 129, 214), Color.White);
            ModernTheme.StyleButton(btnKhoaTaiKhoan, Color.FromArgb(230, 126, 34), Color.White);
            ModernTheme.StyleButton(btnResetPass, Color.FromArgb(53, 73, 95), Color.White);

            ModernTheme.StyleGrid(dgvNhanSu);

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

            ComboBox comboBox = control as ComboBox;
            if (comboBox != null)
            {
                ModernTheme.StyleDataComboBox(comboBox);
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
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.FlatAppearance.MouseOverBackColor = ControlPaint.Light(backColor, 0.1f);
            button.FlatAppearance.MouseDownBackColor = ControlPaint.Dark(backColor, 0.1f);
            button.BackColor = backColor;
            button.ForeColor = foreColor;
            button.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            button.Cursor = Cursors.Hand;
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

            // Selected row color (lighter)
            grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(166, 210, 248);
            grid.DefaultCellStyle.SelectionForeColor = Color.FromArgb(25, 42, 58);
            grid.RowHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(166, 210, 248);
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
            if (contentWidth < 780)
            {
                contentWidth = 780;
            }

            int left = Math.Max(20, (ClientSize.Width - contentWidth) / 2);
            int top = 26;

            // Title
            label1.AutoSize = true;
            label1.Top = top;
            label1.Left = left + (contentWidth - label1.Width) / 2;

            // Form section
            int formWidth = 620;
            int formLeft = left + (contentWidth - formWidth) / 2;
            int labelLeft = formLeft;
            int inputLeft = formLeft + 170;
            int inputWidth = 320;

            int y = label1.Bottom + 18;

            label2.Left = labelLeft;
            label2.Top = y + 4;
            txtUsername.Left = inputLeft;
            txtUsername.Top = y;
            txtUsername.Width = inputWidth;

            y += 42;
            label3.Left = labelLeft;
            label3.Top = y + 4;
            txtTenHienThi.Left = inputLeft;
            txtTenHienThi.Top = y;
            txtTenHienThi.Width = inputWidth;

            y += 42;
            label5.Left = labelLeft;
            label5.Top = y + 4;
            cboVaiTro.Left = inputLeft;
            cboVaiTro.Top = y;
            cboVaiTro.Width = inputWidth;

            y += 42;
            label4.Left = labelLeft;
            label4.Top = y + 4;
            txtLuongCoBan.Left = inputLeft;
            txtLuongCoBan.Top = y;
            txtLuongCoBan.Width = inputWidth;

            // Buttons row
            int btnY = y + 52;
            int btnSpacing = 12;

            int w1 = GetButtonWidth(btnLuuThongTin, 120);
            int w2 = GetButtonWidth(btnCapNhatLuong, 160);
            int w3 = GetButtonWidth(btnKhoaTaiKhoan, 155);
            int w4 = GetButtonWidth(btnResetPass, 150);

            int totalBtnWidth = w1 + w2 + w3 + w4 + (btnSpacing * 3);
            int btnStart = left + (contentWidth - totalBtnWidth) / 2;

            btnLuuThongTin.SetBounds(btnStart, btnY, w1, 38);
            btnCapNhatLuong.SetBounds(btnLuuThongTin.Right + btnSpacing, btnY, w2, 38);
            btnKhoaTaiKhoan.SetBounds(btnCapNhatLuong.Right + btnSpacing, btnY, w3, 38);
            btnResetPass.SetBounds(btnKhoaTaiKhoan.Right + btnSpacing, btnY, w4, 38);

            // Grid
            int gridTop = btnLuuThongTin.Bottom + 18;
            int gridHeight = ClientSize.Height - gridTop - 24;
            if (gridHeight < 220)
            {
                gridHeight = 220;
            }

            dgvNhanSu.SetBounds(left, gridTop, contentWidth, gridHeight);
        }

        private void LoadData()
        {
            dgvNhanSu.DataSource = qtBUS.LayDanhSachTaiKhoan();

            if (dgvNhanSu.Columns.Count > 0)
            {
                dgvNhanSu.Columns["Id"].Visible = false;
                dgvNhanSu.Columns["Username"].HeaderText = "Tài Khoản";
                dgvNhanSu.Columns["VaiTro"].HeaderText = "Vai Trò";
                dgvNhanSu.Columns["TenHienThi"].HeaderText = "Tên Người Dùng";
                dgvNhanSu.Columns["LuongCoBan"].HeaderText = "Lương CB / Lớp (VNĐ)";
                dgvNhanSu.Columns["TrangThai"].HeaderText = "Trạng Thái";

                dgvNhanSu.Columns["LuongCoBan"].DefaultCellStyle.Format = "N0";
                dgvNhanSu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private void ucQuanTriHeThong_Load(object sender, EventArgs e)
        {
            ApplyTheme();
            LoadData();

            if (!isLayoutHooked)
            {
                Resize += ucQuanTriHeThong_Resize;
                isLayoutHooked = true;
            }

            ApplyResponsiveLayout();
        }

        private void ucQuanTriHeThong_Resize(object sender, EventArgs e)
        {
            ApplyResponsiveLayout();
        }

        private void dgvNhanSu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvNhanSu.Rows[e.RowIndex];
                idDangChon = Convert.ToInt32(row.Cells["Id"].Value);

                txtUsername.Text = row.Cells["Username"].Value.ToString();
                cboVaiTro.Text = row.Cells["VaiTro"].Value.ToString();
                txtTenHienThi.Text = row.Cells["TenHienThi"].Value.ToString();

                string trangThai = row.Cells["TrangThai"].Value.ToString();
                btnKhoaTaiKhoan.Text = trangThai == "Bị khóa" ? "Mở Khóa Tài Khoản" : "Khóa Tài Khoản";

                if (cboVaiTro.Text == "Huấn Luyện Viên" || cboVaiTro.Text == "Nhân Viên")
                {
                    txtLuongCoBan.Text = Convert.ToDecimal(row.Cells["LuongCoBan"].Value).ToString("0");
                    txtLuongCoBan.Enabled = true;
                }
                else
                {
                    txtLuongCoBan.Text = "0";
                    txtLuongCoBan.Enabled = false;
                }
            }
        }

        private void btnLuuThongTin_Click(object sender, EventArgs e)
        {
            if (idDangChon == 0)
            {
                return;
            }

            string kq = qtBUS.CapNhatThongTin(idDangChon, txtUsername.Text.Trim(), cboVaiTro.Text, txtTenHienThi.Text.Trim());

            if (kq == "")
            {
                MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo");
                LoadData();
            }
            else
            {
                MessageBox.Show(kq, "Lỗi");
            }
        }

        private void btnCapNhatLuong_Click(object sender, EventArgs e)
        {
            if (idDangChon == 0)
            {
                MessageBox.Show("Vui lòng chọn một nhân sự từ danh sách!", "Nhắc nhở", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (cboVaiTro.Text != "Huấn Luyện Viên" && cboVaiTro.Text != "Nhân Viên")
            {
                MessageBox.Show("Chức năng cập nhật lương chỉ áp dụng cho Nhân Viên và Huấn Luyện Viên!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal luongMoi;
            if (!decimal.TryParse(txtLuongCoBan.Text, out luongMoi))
            {
                MessageBox.Show("Vui lòng nhập số tiền lương hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string kq = qtBUS.CapNhatLuong(idDangChon, luongMoi);

            if (kq == "")
            {
                MessageBox.Show("Cập nhật lương thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
            else
            {
                MessageBox.Show(kq, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnKhoaTaiKhoan_Click(object sender, EventArgs e)
        {
            if (idDangChon == 0)
            {
                return;
            }

            string hanhDong = btnKhoaTaiKhoan.Text.Contains("Mở") ? "mở khóa" : "khóa";

            if (MessageBox.Show("Bạn có chắc chắn muốn " + hanhDong + " tài khoản này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                string kq = qtBUS.KhoaMoTaiKhoan(idDangChon);
                if (kq == "")
                {
                    MessageBox.Show("Đã " + hanhDong + " thành công!");
                    LoadData();
                }
                else
                {
                    MessageBox.Show(kq, "Lỗi");
                }
            }
        }

        private void btnResetPass_Click(object sender, EventArgs e)
        {
            if (idDangChon == 0)
            {
                return;
            }

            if (MessageBox.Show("Bạn có chắc muốn đặt lại mật khẩu về mặc định (123456)?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string kq = qtBUS.DatLaiMatKhau(idDangChon);
                if (kq == "")
                {
                    MessageBox.Show("Đã đặt lại mật khẩu thành công!");
                }
                else
                {
                    MessageBox.Show(kq);
                }
            }
        }

        private void cboVaiTro_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboVaiTro.Text == "Huấn Luyện Viên" || cboVaiTro.Text == "Nhân Viên")
            {
                txtLuongCoBan.Enabled = true;
            }
            else
            {
                txtLuongCoBan.Text = "0";
                txtLuongCoBan.Enabled = false;
            }
        }

        private int GetButtonWidth(Button button, int minWidth)
        {
            int textWidth = TextRenderer.MeasureText(button.Text, button.Font).Width;
            return Math.Max(minWidth, textWidth + 34);
        }
    }
}
