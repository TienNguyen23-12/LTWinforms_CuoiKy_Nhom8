using LTWinforms_CuoiKy_Nhom8.BUS;
using LTWinforms_CuoiKy_Nhom8.DAL;
using LTWinforms_CuoiKy_Nhom8.DTO;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace LTWinforms_CuoiKy_Nhom8.GUI
{
    public partial class ucBanVeThuNgan : UserControl
    {
        private readonly HoaDonBUS hdBUS = new HoaDonBUS();
        private readonly DangKyLopBUS dkBUS = new DangKyLopBUS();
        private readonly HoiVienBUS hvBUS = new HoiVienBUS();
        private readonly QLTTDataContext db = new QLTTDataContext();

        private bool isThemeApplied;
        private bool isLayoutHooked;

        public ucBanVeThuNgan()
        {
            InitializeComponent();

            dgvLichSu.CellFormatting += dgvLichSu_CellFormatting;
            dgvPhanHoi.CellFormatting += dgvPhanHoi_CellFormatting;
        }

        private void ucBanVeThuNgan_Load(object sender, EventArgs e)
        {
            ApplyTheme();

            cboHoiVien.DataSource = db.HoiViens.Where(x => x.IsActive == true).ToList();
            cboHoiVien.DisplayMember = "HoTen";
            cboHoiVien.ValueMember = "MaHoiVien";

            cboHoiVien.SelectedIndexChanged -= cboHoiVien_SelectedIndexChanged;
            cboHoiVien.SelectedIndexChanged += cboHoiVien_SelectedIndexChanged;

            LoadLichSu();
            LoadPhanHoi();
            LoadDichVu();

            if (!isLayoutHooked)
            {
                Resize += ucBanVeThuNgan_Resize;
                isLayoutHooked = true;
            }

            ApplyResponsiveLayout();
            BeginInvoke(new Action(ApplyResponsiveLayout));
        }

        private void ucBanVeThuNgan_Resize(object sender, EventArgs e)
        {
            ApplyResponsiveLayout();
        }

        private string GetSelectedMaHoiVien()
        {
            if (cboHoiVien.SelectedValue is string)
            {
                string value = cboHoiVien.SelectedValue.ToString();
                if (!string.IsNullOrWhiteSpace(value) && value != "System.Data.DataRowView")
                {
                    return value;
                }
            }

            HoiVien hv = cboHoiVien.SelectedItem as HoiVien;
            return hv != null ? hv.MaHoiVien : "";
        }

        private void LoadLichSu()
        {
            dgvLichSu.DataSource = hdBUS.LayLichSuGiaoDich();

            if (dgvLichSu.Columns.Count > 0)
            {
                SetHeaderText(dgvLichSu, "Mã hóa đơn", "MaHoaDon", "Mã_HĐ", "Ma_HD");
                SetHeaderText(dgvLichSu, "Mã hội viên", "MaHoiVien", "Mã_HV", "Ma_HV");
                SetHeaderText(dgvLichSu, "Họ tên khách", "HoTen", "Tên_Khách", "Ten_Khach");
                SetHeaderText(dgvLichSu, "Dịch vụ", "TenDichVu", "Dịch_Vụ", "Dich_Vu");
                SetHeaderText(dgvLichSu, "Thu ngân", "ThuNgan", "Thu_Ngân", "Thu_Ngan");
                SetHeaderText(dgvLichSu, "Loại dịch vụ", "LoaiDichVu", "Loai_Dich_Vu");
                SetHeaderText(dgvLichSu, "Số tiền (VNĐ)", "SoTien", "Số_tiền_(VND)", "So_tien_(VND)");
                SetHeaderText(dgvLichSu, "Ngày thanh toán", "NgayThanhToan", "Ngày_thanh_toán", "Ngay_thanh_toan");
                SetHeaderText(dgvLichSu, "Trạng thái", "TrangThai", "Trạng_Thái", "Trang_Thai");
                SetHeaderText(dgvLichSu, "Ghi chú", "GhiChu", "Ghi_Chu");

                DataGridViewColumn colSoTien = GetColumn(dgvLichSu, "SoTien", "Số_tiền_(VND)", "So_tien_(VND)");
                if (colSoTien != null)
                {
                    colSoTien.DefaultCellStyle.Format = "N0";
                }

                DataGridViewColumn colNgayThanhToan = GetColumn(dgvLichSu, "NgayThanhToan", "Ngày_thanh_toán", "Ngay_thanh_toan");
                if (colNgayThanhToan != null)
                {
                    colNgayThanhToan.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                }

                NormalizeHeaderUnderscoreToSpace(dgvLichSu);
                dgvLichSu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private void LoadDichVu()
        {
            cboDichVu.DataSource = null;
            txtSoTien.Text = "0 VNĐ";

            if (radGoiTap.Checked)
            {
                cboDichVu.DataSource = db.GoiTaps.Where(x => x.IsActive == true).ToList();
                cboDichVu.DisplayMember = "TenGoi";
                cboDichVu.ValueMember = "MaGoi";
                return;
            }

            if (radLopHoc.Checked)
            {
                string maHV = GetSelectedMaHoiVien();
                if (string.IsNullOrWhiteSpace(maHV))
                {
                    return;
                }

                var listNoLop = (from dk in db.DangKyLops
                                 join lop in db.LopHocs on dk.MaLop equals lop.MaLop
                                 where dk.MaHoiVien == maHV &&
                                       ((dk.TrangThaiThanhToan ?? "").Trim() == "Chờ thanh toán")
                                 select new
                                 {
                                     IdDangKy = dk.Id,
                                     TenHienThi = lop.TenLop,
                                     GiaTien = lop.GiaTien
                                 }).ToList();

                cboDichVu.DataSource = listNoLop;
                cboDichVu.DisplayMember = "TenHienThi";
                cboDichVu.ValueMember = "IdDangKy";

                if (listNoLop.Count == 0)
                {
                    cboDichVu.DataSource = null;
                    txtSoTien.Text = "0 VNĐ";
                }

                return;
            }

            if (radSanPham.Checked)
            {
                cboDichVu.DataSource = db.SanPhams.Where(x => x.IsActive == true).ToList();
                cboDichVu.DisplayMember = "TenSP";
                cboDichVu.ValueMember = "MaSP";
            }
        }

        private void LoadPhanHoi()
        {
            var data = hvBUS.LayTatCaPhanHoi();
            dgvPhanHoi.DataSource = data;

            dgvPhanHoi.Visible = true;
            btnDaDoc.Visible = true;
            dgvPhanHoi.BringToFront();
            btnDaDoc.BringToFront();

            if (dgvPhanHoi.Columns.Count > 0)
            {
                SetHeaderText(dgvPhanHoi, "Mã phản hồi", "Id", "Mã_PH", "Ma_PH");
                SetHeaderText(dgvPhanHoi, "Mã hội viên", "MaHoiVien", "Mã_HV", "Ma_HV");
                SetHeaderText(dgvPhanHoi, "Họ tên hội viên", "HoTen", "Tên_Khách", "Ten_Khach");
                SetHeaderText(dgvPhanHoi, "Nội dung", "NoiDung", "Nội_Dung", "Noi_Dung");
                SetHeaderText(dgvPhanHoi, "Trạng thái", "TrangThai", "Trạng_Thái", "Trang_Thai");
                SetHeaderText(dgvPhanHoi, "Ngày gửi", "NgayGui", "Ngày_Gửi", "Ngay_Gui");

                DataGridViewColumn colNgayGui = GetColumn(dgvPhanHoi, "NgayGui", "Ngày_Gửi", "Ngay_Gui");
                if (colNgayGui != null)
                {
                    colNgayGui.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                }

                NormalizeHeaderUnderscoreToSpace(dgvPhanHoi);
                dgvPhanHoi.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private void cboHoiVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radLopHoc.Checked)
            {
                LoadDichVu();
            }
        }

        private void cboDichVu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboDichVu.SelectedItem == null)
            {
                txtSoTien.Text = "0 VNĐ";
                return;
            }

            try
            {
                var type = cboDichVu.SelectedItem.GetType();
                var prop = type.GetProperty("GiaTien");

                if (prop != null)
                {
                    object giaTienObj = prop.GetValue(cboDichVu.SelectedItem, null);
                    decimal giaTien = Convert.ToDecimal(giaTienObj);
                    txtSoTien.Text = giaTien.ToString("N0") + " VNĐ";
                }
                else
                {
                    txtSoTien.Text = "0 VNĐ";
                }
            }
            catch
            {
                txtSoTien.Text = "0 VNĐ";
            }
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (cboHoiVien.SelectedValue == null || cboDichVu.SelectedValue == null)
            {
                ModernMessageBox.Show("Vui lòng chọn Khách hàng và Dịch vụ cần thanh toán!", "Cảnh báo", ModernMessageType.Warning);
                return;
            }

            string maHV = GetSelectedMaHoiVien();
            if (string.IsNullOrWhiteSpace(maHV))
            {
                ModernMessageBox.Show("Không xác định được hội viên!", "Lỗi", ModernMessageType.Error);
                return;
            }

            if (radGoiTap.Checked)
            {
                string maGoi = cboDichVu.SelectedValue.ToString();
                if (ModernMessageBox.Show("Xác nhận bán gói Gym cho khách " + cboHoiVien.Text + "?", "Xác nhận", MessageBoxButtons.YesNo, ModernMessageType.Question) == DialogResult.Yes)
                {
                    string kq = hdBUS.ThanhToanGiaHan(maHV, maGoi, Session.IdTaiKhoan);
                    if (kq == "")
                    {
                        ModernMessageBox.Show("Thanh toán thành công!", "Thành công", ModernMessageType.Success);
                        LoadLichSu();
                    }
                    else
                    {
                        ModernMessageBox.Show(kq, "Lỗi hệ thống", ModernMessageType.Error);
                    }
                }
            }
            else if (radLopHoc.Checked)
            {
                int idDangKy = Convert.ToInt32(cboDichVu.SelectedValue);
                if (MessageBox.Show("Xác nhận thu tiền lớp học này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string kq = dkBUS.XacNhanThuTien(idDangKy, Session.IdTaiKhoan);
                    if (kq == "")
                    {
                        ModernMessageBox.Show("Thanh toán lớp học thành công!", "Thành công", ModernMessageType.Success);
                        LoadLichSu();
                        LoadDichVu();
                    }
                    else
                    {
                        ModernMessageBox.Show(kq, "Lỗi hệ thống", ModernMessageType.Error);
                    }
                }
            }
            else if (radSanPham.Checked)
            {
                string tenSP = cboDichVu.Text;
                decimal giaTien = Convert.ToDecimal(txtSoTien.Text.Replace(" VNĐ", "").Replace(",", ""));

                if (MessageBox.Show("Xác nhận bán [" + tenSP + "] cho khách " + cboHoiVien.Text + "?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string kq = hdBUS.BanSanPhamTaiQuay(maHV, tenSP, giaTien, Session.IdTaiKhoan);
                    if (kq == "")
                    {
                        ModernMessageBox.Show("Thanh toán thành công!", "Thành công", ModernMessageType.Success);
                        LoadLichSu();
                    }
                    else
                    {
                        ModernMessageBox.Show(kq, "Lỗi hệ thống", ModernMessageType.Error);
                    }
                }
            }
        }

        private void radGoiTap_CheckedChanged(object sender, EventArgs e)
        {
            if (radGoiTap.Checked)
            {
                LoadDichVu();
                btnDuyetOnline.Enabled = true;
            }
        }

        private void radLopHoc_CheckedChanged(object sender, EventArgs e)
        {
            if (radLopHoc.Checked)
            {
                LoadDichVu();
                btnDuyetOnline.Enabled = true;
            }
        }

        private void radSanPham_CheckedChanged(object sender, EventArgs e)
        {
            if (radSanPham.Checked)
            {
                LoadDichVu();
                btnDuyetOnline.Enabled = false;
            }
        }

        private void btnDuyetOnline_Click(object sender, EventArgs e)
        {
            if (dgvLichSu.CurrentRow == null)
            {
                ModernMessageBox.Show("Vui lòng chọn một dòng 'Chờ thanh toán' trên bảng để duyệt!", "Nhắc nhở", ModernMessageType.Warning);
                return;
            }

            object maHdObj = GetCellValue(dgvLichSu.CurrentRow, "MaHoaDon", "Mã_HĐ", "Ma_HD");
            object trangThaiObj = GetCellValue(dgvLichSu.CurrentRow, "TrangThai", "Trạng_Thái", "Trang_Thai");

            if (maHdObj == null || trangThaiObj == null)
            {
                ModernMessageBox.Show("Không đọc được dữ liệu hóa đơn từ dòng đang chọn.", "Lỗi", ModernMessageType.Error);
                return;
            }

            int maHD = Convert.ToInt32(maHdObj);
            string trangThai = Convert.ToString(trangThaiObj);

            if (trangThai == "Đã thanh toán")
            {
                ModernMessageBox.Show("Đơn này đã được thanh toán rồi!", "Thông báo", ModernMessageType.Info);
                return;
            }

            if (ModernMessageBox.Show("Xác nhận đã thu tiền cho hóa đơn #" + maHD + "?", "Xác nhận duyệt Online", MessageBoxButtons.YesNo, ModernMessageType.Question) == DialogResult.Yes)
            {
                string kq = hdBUS.DuyetHoaDonOnline(maHD, Session.IdTaiKhoan);
                if (kq == "")
                {
                    ModernMessageBox.Show("Duyệt thành công! Gói tập đã được kích hoạt cho khách.", "Thành công", ModernMessageType.Success);
                    LoadLichSu();
                }
                else
                {
                    ModernMessageBox.Show(kq, "Lỗi", ModernMessageType.Error);
                }
            }
        }

        private void tcThuNgan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tcThuNgan.SelectedTab == tpThuNgan)
            {
                LoadLichSu();
                LoadDichVu();
            }
            else if (tcThuNgan.SelectedTab == tpPhanHoi)
            {
                LoadPhanHoi();
            }

            ApplyResponsiveLayout();
        }

        private void btnDaDoc_Click(object sender, EventArgs e)
        {
            if (dgvPhanHoi.CurrentRow == null)
            {
                ModernMessageBox.Show("Vui lòng chọn 1 tin nhắn để xử lý.", "Nhắc nhở", ModernMessageType.Warning);
                return;
            }

            object idObj = GetCellValue(dgvPhanHoi.CurrentRow, "Id", "Mã_PH", "Ma_PH");
            object trangThaiObj = GetCellValue(dgvPhanHoi.CurrentRow, "TrangThai", "Trạng_Thái", "Trang_Thai");

            if (idObj == null || trangThaiObj == null)
            {
                ModernMessageBox.Show("Không đọc được dữ liệu phản hồi từ dòng đang chọn.", "Lỗi", ModernMessageType.Error);
                return;
            }

            int id = Convert.ToInt32(idObj);
            string trangThai = Convert.ToString(trangThaiObj);

            if (trangThai == "Đã xử lý")
            {
                ModernMessageBox.Show("Tin nhắn này đã được xử lý rồi.", "Thông báo", ModernMessageType.Info);
                return;
            }

            if (hvBUS.DaXuLyPhanHoi(id))
            {
                ModernMessageBox.Show("Đã đánh dấu xử lý xong.", "Thành công", ModernMessageType.Success);
                LoadPhanHoi();
            }
        }

        private void dgvLichSu_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            string colName = dgvLichSu.Columns[e.ColumnIndex].Name;
            if (!IsAnyOf(colName, "TrangThai", "Trạng_Thái", "Trang_Thai") || e.Value == null)
            {
                return;
            }

            string tt = e.Value.ToString();
            if (tt == "Chờ thanh toán")
            {
                e.CellStyle.ForeColor = Color.Red;
                e.CellStyle.Font = new Font(dgvLichSu.Font, FontStyle.Bold);
            }
            else
            {
                e.CellStyle.ForeColor = Color.Green;
            }
        }

        private void dgvPhanHoi_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            string colName = dgvPhanHoi.Columns[e.ColumnIndex].Name;
            if (!IsAnyOf(colName, "TrangThai", "Trạng_Thái", "Trang_Thai") || e.Value == null)
            {
                return;
            }

            if (e.Value.ToString() == "Chưa đọc")
            {
                e.CellStyle.ForeColor = Color.Red;
                e.CellStyle.Font = new Font(dgvPhanHoi.Font, FontStyle.Bold);
            }
            else
            {
                e.CellStyle.ForeColor = Color.Green;
            }
        }

        private static DataGridViewColumn GetColumn(DataGridView grid, params string[] names)
        {
            foreach (string name in names)
            {
                if (grid.Columns.Contains(name))
                {
                    return grid.Columns[name];
                }
            }

            return null;
        }

        private static void SetHeaderText(DataGridView grid, string headerText, params string[] names)
        {
            DataGridViewColumn column = GetColumn(grid, names);
            if (column != null)
            {
                column.HeaderText = headerText;
            }
        }

        private static void NormalizeHeaderUnderscoreToSpace(DataGridView grid)
        {
            foreach (DataGridViewColumn column in grid.Columns)
            {
                if (!string.IsNullOrEmpty(column.HeaderText) && column.HeaderText.Contains("_"))
                {
                    column.HeaderText = column.HeaderText.Replace("_", " ");
                }
            }
        }

        private static object GetCellValue(DataGridViewRow row, params string[] names)
        {
            if (row == null || row.DataGridView == null)
            {
                return null;
            }

            foreach (string name in names)
            {
                if (row.DataGridView.Columns.Contains(name))
                {
                    return row.Cells[name].Value;
                }
            }

            return null;
        }

        private static bool IsAnyOf(string value, params string[] candidates)
        {
            foreach (string candidate in candidates)
            {
                if (string.Equals(value, candidate, StringComparison.Ordinal))
                {
                    return true;
                }
            }

            return false;
        }

        private void ApplyTheme()
        {
            if (isThemeApplied)
            {
                return;
            }

            BackColor = Color.White;
            tpThuNgan.BackColor = Color.White;
            tpPhanHoi.BackColor = Color.White;

            StyleLabel(label1);
            StyleLabel(label2);
            StyleLabel(label3);

            StyleInput(cboHoiVien);
            StyleInput(cboDichVu);
            StyleInput(txtSoTien);

            StyleRadio(radGoiTap);
            StyleRadio(radLopHoc);
            StyleRadio(radSanPham);

            StylePrimaryButton(btnThanhToan);
            StyleSecondaryButton(btnDuyetOnline);
            StylePrimaryButton(btnDaDoc);

            StyleGrid(dgvLichSu);
            StyleGrid(dgvPhanHoi);

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

        private void StyleRadio(RadioButton radio)
        {
            radio.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            radio.ForeColor = Color.FromArgb(44, 62, 80);
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
        }

        private void ApplyResponsiveLayout()
        {
            int w = tpThuNgan.ClientSize.Width;
            int left = Math.Max(24, (w - 900) / 2);
            int top = 24;

            radGoiTap.SetBounds(left + 220, top, 150, 24);
            radLopHoc.SetBounds(radGoiTap.Right + 20, top, 160, 24);
            radSanPham.SetBounds(radLopHoc.Right + 20, top, 130, 24);

            top += 44;
            label1.SetBounds(left + 70, top + 5, 90, 24);
            cboHoiVien.SetBounds(left + 170, top, 250, 30);

            label3.SetBounds(left + 450, top + 5, 110, 24);
            txtSoTien.SetBounds(left + 565, top, 180, 30);

            top += 44;
            label2.SetBounds(left + 20, top + 5, 145, 24);
            cboDichVu.SetBounds(left + 170, top, 250, 30);

            btnThanhToan.SetBounds(left + 450, top - 2, 120, 34);
            btnDuyetOnline.SetBounds(btnThanhToan.Right + 14, top - 2, 140, 34);

            int gridTop = top + 54;
            int lichSuHeight = Math.Max(180, tpThuNgan.ClientSize.Height - gridTop - 18);
            dgvLichSu.SetBounds(left, gridTop, 900, lichSuHeight);

            int phLeft = 20;
            int phTop = 20;
            int phWidth = Math.Max(300, tpPhanHoi.ClientSize.Width - 40);
            int phHeight = Math.Max(180, tpPhanHoi.ClientSize.Height - 90);

            dgvPhanHoi.SetBounds(phLeft, phTop, phWidth, phHeight);
            btnDaDoc.SetBounds(phLeft + (phWidth - 100) / 2, dgvPhanHoi.Bottom + 10, 100, 34);
        }
    }
}
