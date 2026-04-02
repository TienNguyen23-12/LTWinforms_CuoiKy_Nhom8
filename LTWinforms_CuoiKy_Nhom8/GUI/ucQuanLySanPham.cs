using LTWinforms_CuoiKy_Nhom8.BUS;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace LTWinforms_CuoiKy_Nhom8.GUI
{
    public partial class ucQuanLySanPham : UserControl
    {
        private readonly SanPhamBUS spBUS = new SanPhamBUS();
        private int maSPDangChon = 0;
        private bool isThemeApplied;
        private bool isLayoutHooked;

        public ucQuanLySanPham()
        {
            InitializeComponent();
        }

        private void ucQuanLySanPham_Load(object sender, EventArgs e)
        {
            ApplyTheme();
            LoadData();

            if (!isLayoutHooked)
            {
                Resize += ucQuanLySanPham_Resize;
                isLayoutHooked = true;
            }

            ApplyResponsiveLayout();
        }

        private void ucQuanLySanPham_Resize(object sender, EventArgs e)
        {
            ApplyResponsiveLayout();
        }

        private void ApplyTheme()
        {
            if (isThemeApplied)
            {
                return;
            }

            BackColor = Color.White;

            label1.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            label1.ForeColor = Color.FromArgb(34, 49, 63);

            StyleLabel(label2);
            StyleLabel(label3);

            StyleInput(txtTenSP);
            StyleInput(txtGiaTien);

            chkDangBan.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            chkDangBan.ForeColor = Color.FromArgb(44, 62, 80);

            StylePrimaryButton(btnLuu);
            StyleSecondaryButton(btnThem);
            btnThem.Text = "Reset";

            StyleGrid(dgvSanPham);

            isThemeApplied = true;
        }

        private void StyleLabel(Label label)
        {
            label.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            label.ForeColor = Color.FromArgb(44, 62, 80);
        }

        private void StyleInput(TextBox textBox)
        {
            ModernTheme.StyleInput(textBox);
        }

        private void StylePrimaryButton(Button button)
        {
            StyleButton(button, Color.FromArgb(46, 134, 222), Color.White);
        }

        private void StyleSecondaryButton(Button button)
        {
            StyleButton(button, Color.FromArgb(52, 73, 94), Color.White);
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
            int left = Math.Max(20, (ClientSize.Width - contentWidth) / 2);

            label1.AutoSize = true;
            label1.Top = 16;
            label1.Left = left + (contentWidth - label1.Width) / 2;

            int formLeft = left + 40;
            int labelWidth = 110;
            int inputWidth = 340;

            int y = label1.Bottom + 18;

            label2.SetBounds(formLeft, y + 6, labelWidth, 24);
            txtTenSP.SetBounds(formLeft + labelWidth + 10, y, inputWidth, 30);

            y += 42;
            label3.SetBounds(formLeft, y + 6, labelWidth, 24);
            txtGiaTien.SetBounds(formLeft + labelWidth + 10, y, inputWidth, 30);

            y += 40;
            chkDangBan.SetBounds(formLeft + labelWidth + 10, y, 140, 24);

            int btnY = y + 36;
            btnLuu.SetBounds(formLeft, btnY, 120, 34);
            btnThem.SetBounds(btnLuu.Right + 12, btnY, 120, 34);

            int gridTop = btnLuu.Bottom + 16;
            int gridHeight = Math.Max(220, ClientSize.Height - gridTop - 20);
            dgvSanPham.SetBounds(left, gridTop, contentWidth, gridHeight);
        }

        private void LoadData()
        {
            dgvSanPham.DataSource = spBUS.LayDanhSachSanPhamAdmin();

            if (dgvSanPham.Columns.Count > 0)
            {
                if (dgvSanPham.Columns.Contains("MaSP")) dgvSanPham.Columns["MaSP"].Visible = false;
                if (dgvSanPham.Columns.Contains("TenSP")) dgvSanPham.Columns["TenSP"].HeaderText = "Tên sản phẩm";
                if (dgvSanPham.Columns.Contains("GiaTien"))
                {
                    dgvSanPham.Columns["GiaTien"].HeaderText = "Giá tiền (VNĐ)";
                    dgvSanPham.Columns["GiaTien"].DefaultCellStyle.Format = "N0";
                }
                if (dgvSanPham.Columns.Contains("TrangThai")) dgvSanPham.Columns["TrangThai"].HeaderText = "Trạng thái";

                dgvSanPham.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private void dgvSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvSanPham.Rows[e.RowIndex];

                maSPDangChon = Convert.ToInt32(row.Cells["MaSP"].Value);

                txtTenSP.Text = row.Cells["TenSP"].Value.ToString();
                txtGiaTien.Text = row.Cells["GiaTien"].Value.ToString();

                string trangThai = row.Cells["TrangThai"].Value.ToString();
                chkDangBan.Checked = trangThai == "Đang bán";
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            maSPDangChon = 0;
            txtTenSP.Clear();
            txtGiaTien.Text = "0";
            chkDangBan.Checked = true;
            txtTenSP.Focus();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string ten = txtTenSP.Text.Trim();
            if (string.IsNullOrEmpty(ten))
            {
                ModernMessageBox.Show("Tên không được để trống", "Cảnh báo", ModernMessageType.Warning);
                return;
            }

            decimal gia;
            if (!decimal.TryParse(txtGiaTien.Text.Replace(",", ""), out gia))
            {
                ModernMessageBox.Show("Giá tiền không hợp lệ!", "Lỗi", ModernMessageType.Error);
                return;
            }

            string kq = spBUS.LuuSanPham(maSPDangChon, ten, gia, chkDangBan.Checked);
            if (kq == "")
            {
                ModernMessageBox.Show("Đã lưu thông tin sản phẩm!", "Thông báo", ModernMessageType.Success);
                LoadData();
                maSPDangChon = 0;
            }
            else
            {
                ModernMessageBox.Show("Lỗi: " + kq, "Lỗi", ModernMessageType.Error);
            }
        }
    }
}
