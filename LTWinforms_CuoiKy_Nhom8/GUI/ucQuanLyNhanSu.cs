using LTWinforms_CuoiKy_Nhom8.BUS;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace LTWinforms_CuoiKy_Nhom8.GUI
{
    public partial class ucQuanLyNhanSu : UserControl
    {
        private readonly NhanSuBUS nsBUS = new NhanSuBUS();
        private int idNhanSuDangChon = 0;
        private bool isThemeApplied;
        private bool isLayoutHooked;

        public ucQuanLyNhanSu()
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

            ModernTheme.StyleLabel(lblTenNhanSu);
            ModernTheme.StyleLabel(label1);
            ModernTheme.StyleLabel(label2);

            ModernTheme.StyleInput(txtTienPhat);
            ModernTheme.StyleInput(txtLyDoPhat);

            radCoMat.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            radVangMat.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            radCoMat.ForeColor = ModernTheme.TextPrimary;
            radVangMat.ForeColor = ModernTheme.TextPrimary;

            ModernTheme.StyleButton(btnChamCong, Color.FromArgb(58, 129, 214), Color.White);
            ModernTheme.StyleButton(btnPhat, Color.FromArgb(230, 126, 34), Color.White);

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

            RichTextBox richTextBox = control as RichTextBox;
            if (richTextBox != null)
            {
                richTextBox.BorderStyle = BorderStyle.FixedSingle;
            }
        }

        private void StyleRadio(RadioButton radio)
        {
            radio.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            radio.ForeColor = Color.FromArgb(44, 62, 80);
        }

        private void StylePrimaryButton(Button button)
        {
            StyleButton(button, Color.FromArgb(46, 134, 222), Color.White);
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
            grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            grid.ColumnHeadersHeight = 38;

            grid.RowTemplate.Height = 28;
            grid.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 249, 255);
            grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(186, 222, 250);
            grid.DefaultCellStyle.SelectionForeColor = Color.FromArgb(25, 42, 58);

            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.MultiSelect = false;
            grid.ReadOnly = true;
            grid.AllowUserToAddRows = false;
            grid.AllowUserToResizeRows = false;
            grid.RowHeadersVisible = false;
        }

        private void ApplyResponsiveLayout()
        {
            int contentWidth = Math.Min(1100, ClientSize.Width - 40);
            int left = Math.Max(12, (ClientSize.Width - contentWidth) / 2);

            // Left panel = nửa trang
            int panelGap = 20;
            int leftPanelWidth = (contentWidth - panelGap) / 2;
            int rightLeft = left + leftPanelWidth + panelGap;
            int rightWidth = contentWidth - leftPanelWidth - panelGap;

            lblTenNhanSu.SetBounds(left, 16, leftPanelWidth, 24);
            dgvNhanSu.SetBounds(left, 46, leftPanelWidth, ClientSize.Height - 58);

            // Right panel
            int y = 46;

            radCoMat.SetBounds(rightLeft, y, 120, 24);
            radVangMat.SetBounds(rightLeft + 140, y, 120, 24);

            btnChamCong.SetBounds(rightLeft, y + 36, 140, 38);

            y += 100;

            // Cột nhãn + cột ô nhập để luôn thẳng hàng
            int labelWidth = 88;
            int inputGap = 12;
            int inputLeft = rightLeft + labelWidth + inputGap;
            int inputWidth = Math.Min(420, rightWidth - labelWidth - inputGap);

            label1.SetBounds(rightLeft, y + 4, labelWidth, 24);
            txtTienPhat.SetBounds(inputLeft, y, inputWidth, 30);

            y += 44;
            label2.SetBounds(rightLeft, y + 4, labelWidth, 24);
            txtLyDoPhat.SetBounds(inputLeft, y, inputWidth, 120);

            btnPhat.SetBounds(rightLeft, txtLyDoPhat.Bottom + 14, 140, 38);
        }

        private void LoadData(string tuKhoa = "")
        {
            dgvNhanSu.DataSource = nsBUS.LayDanhSachNhanSu(tuKhoa);

            if (dgvNhanSu.Columns.Count > 0)
            {
                dgvNhanSu.Columns["IdTaiKhoan"].Visible = false;

                dgvNhanSu.Columns["Username"].HeaderText = "Tài Khoản";
                dgvNhanSu.Columns["VaiTro"].HeaderText = "Vai Trò";
                dgvNhanSu.Columns["HoTen"].HeaderText = "Họ và Tên";

                dgvNhanSu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                dgvNhanSu.Columns["Username"].FillWeight = 25;
                dgvNhanSu.Columns["VaiTro"].FillWeight = 25;
                dgvNhanSu.Columns["HoTen"].FillWeight = 50;
            }
        }

        private void dgvNhanSu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvNhanSu.Rows[e.RowIndex];
                idNhanSuDangChon = Convert.ToInt32(row.Cells["IdTaiKhoan"].Value);
                string hoTen = row.Cells["HoTen"].Value.ToString();
                string vaiTro = row.Cells["VaiTro"].Value.ToString();

                lblTenNhanSu.Text = "Đang chọn: " + hoTen + " (" + vaiTro + ")";
            }
        }

        private void btnChamCong_Click(object sender, EventArgs e)
        {
            if (idNhanSuDangChon == 0)
            {
                ModernMessageBox.Show("Vui lòng chọn 1 nhân sự từ danh sách trước!", "Cảnh báo", ModernMessageType.Warning);
                return;
            }

            string trangThai = radCoMat.Checked ? "Có mặt" : "Vắng mặt";

            string kq = nsBUS.ChamCongHomNay(idNhanSuDangChon, trangThai);
            if (kq == "")
            {
                ModernMessageBox.Show("Đã điểm danh [" + trangThai + "] thành công!", "Thông báo", ModernMessageType.Success);
            }
            else
            {
                ModernMessageBox.Show(kq, "Lỗi", ModernMessageType.Error);
            }
        }

        private void btnPhat_Click(object sender, EventArgs e)
        {
            if (idNhanSuDangChon == 0)
            {
                ModernMessageBox.Show("Vui lòng chọn 1 nhân sự từ danh sách trước!", "Cảnh báo", ModernMessageType.Warning);
                return;
            }

            decimal tienPhat;
            if (string.IsNullOrEmpty(txtTienPhat.Text) || !decimal.TryParse(txtTienPhat.Text.Replace(",", ""), out tienPhat))
            {
                ModernMessageBox.Show("Vui lòng nhập số tiền phạt hợp lệ!", "Cảnh báo", ModernMessageType.Warning);
                return;
            }

            string lyDo = txtLyDoPhat.Text.Trim();
            if (string.IsNullOrEmpty(lyDo))
            {
                ModernMessageBox.Show("Vui lòng nhập lý do phạt!", "Cảnh báo", ModernMessageType.Warning);
                return;
            }

            if (ModernMessageBox.Show("Xác nhận phạt nhân sự này số tiền " + tienPhat.ToString("N0") + " VNĐ?", "Xác nhận", MessageBoxButtons.YesNo, ModernMessageType.Question) == DialogResult.Yes)
            {
                string kq = nsBUS.GhiNhanPhat(idNhanSuDangChon, tienPhat, lyDo);
                if (kq == "")
                {
                    ModernMessageBox.Show("Đã ghi nhận phạt thành công!", "Thông báo", ModernMessageType.Success);
                    txtTienPhat.Clear();
                    txtLyDoPhat.Clear();
                }
                else
                {
                    ModernMessageBox.Show(kq, "Lỗi", ModernMessageType.Error);
                }
            }
        }

        private void cQuanLyNhanSu_Load(object sender, EventArgs e)
        {
            ApplyTheme();
            LoadData();

            radCoMat.Checked = true;
            lblTenNhanSu.Text = "Vui lòng chọn 1 nhân sự từ danh sách bên trái!";

            if (!isLayoutHooked)
            {
                Resize += ucQuanLyNhanSu_Resize;
                isLayoutHooked = true;
            }

            ApplyResponsiveLayout();
        }

        private void ucQuanLyNhanSu_Resize(object sender, EventArgs e)
        {
            ApplyResponsiveLayout();
        }

        public static void StyleDataComboBox(ComboBox comboBox)
        {
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox.FlatStyle = FlatStyle.Flat;
            comboBox.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            comboBox.ForeColor = ModernTheme.TextPrimary;
            comboBox.BackColor = Color.White;

            comboBox.AutoCompleteMode = AutoCompleteMode.None;
            comboBox.AutoCompleteSource = AutoCompleteSource.None;

            comboBox.DrawMode = DrawMode.OwnerDrawFixed;
            comboBox.ItemHeight = 28;
            comboBox.IntegralHeight = false;
            comboBox.MaxDropDownItems = 8;
            comboBox.DropDownHeight = 230;

            comboBox.DrawItem -= ComboBox_DrawItem;
            comboBox.DrawItem += ComboBox_DrawItem;
        }

        private static void ComboBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if (comboBox == null)
            {
                return;
            }

            // ô hiển thị hiện tại (phần trên control) hoặc chưa có item
            if (e.Index < 0)
            {
                e.DrawBackground();
                return;
            }

            bool isEditPortion = (e.State & DrawItemState.ComboBoxEdit) == DrawItemState.ComboBoxEdit;
            bool isSelected = (e.State & DrawItemState.Selected) == DrawItemState.Selected && !isEditPortion;

            Color backColor = isSelected ? Color.FromArgb(58, 129, 214) : Color.White;
            Color foreColor = isSelected ? Color.White : ModernTheme.TextPrimary;

            using (SolidBrush backBrush = new SolidBrush(backColor))
            using (SolidBrush textBrush = new SolidBrush(foreColor))
            {
                e.Graphics.FillRectangle(backBrush, e.Bounds);

                string text = comboBox.GetItemText(comboBox.Items[e.Index]);
                Rectangle textRect = new Rectangle(e.Bounds.X + 8, e.Bounds.Y + 4, e.Bounds.Width - 12, e.Bounds.Height - 8);
                e.Graphics.DrawString(text, comboBox.Font, textBrush, textRect);
            }

            e.DrawFocusRectangle();
        }
    }
}
