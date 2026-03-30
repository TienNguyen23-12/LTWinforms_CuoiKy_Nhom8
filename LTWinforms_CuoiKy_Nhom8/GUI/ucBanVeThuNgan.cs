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
                if (dgvLichSu.Columns.Contains("SoTien"))
                {
                    dgvLichSu.Columns["SoTien"].DefaultCellStyle.Format = "N0";
                }

                if (dgvLichSu.Columns.Contains("NgayThanhToan"))
                {
                    dgvLichSu.Columns["NgayThanhToan"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                }

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
                if (dgvPhanHoi.Columns.Contains("Ngày_Gửi"))
                {
                    dgvPhanHoi.Columns["Ngày_Gửi"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                }

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
                MessageBox.Show("Vui lòng chọn Khách hàng và Dịch vụ cần thanh toán!", "Cảnh báo");
                return;
            }

            string maHV = GetSelectedMaHoiVien();
            if (string.IsNullOrWhiteSpace(maHV))
            {
                MessageBox.Show("Không xác định được hội viên!", "Lỗi");
                return;
            }

            if (radGoiTap.Checked)
            {
                string maGoi = cboDichVu.SelectedValue.ToString();
                if (MessageBox.Show("Xác nhận bán gói Gym cho khách " + cboHoiVien.Text + "?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string kq = hdBUS.ThanhToanGiaHan(maHV, maGoi, Session.IdTaiKhoan);
                    if (kq == "")
                    {
                        MessageBox.Show("Thanh toán thành công!", "Thành công");
                        LoadLichSu();
                    }
                    else
                    {
                        MessageBox.Show(kq, "Lỗi hệ thống");
                    }
                }
            }
            else if (radLopHoc.Checked)
            {
                int idDangKy = Convert.ToInt32(cboDichVu.SelectedValue);
                if (MessageBox.Show("Xác nhận thu tiền lớp học này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    string kq = dkBUS.XacNhanThuTien(idDangKy, Session.IdTaiKhoan);
                    if (kq == "")
                    {
                        MessageBox.Show("Thanh toán lớp học thành công!", "Thành công");
                        LoadLichSu();
                        LoadDichVu();
                    }
                    else
                    {
                        MessageBox.Show(kq, "Lỗi hệ thống");
                    }
                }
            }
            else if (radSanPham.Checked)
            {
                string tenSP = cboDichVu.Text;
                decimal giaTien = Convert.ToDecimal(txtSoTien.Text.Replace(" VNĐ", "").Replace(",", ""));

                if (MessageBox.Show("Xác nhận bán [" + tenSP + "] cho khách " + cboHoiVien.Text + "?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    string kq = hdBUS.BanSanPhamTaiQuay(maHV, tenSP, giaTien, Session.IdTaiKhoan);
                    if (kq == "")
                    {
                        MessageBox.Show("Thanh toán thành công!", "Thành công");
                        LoadLichSu();
                    }
                    else
                    {
                        MessageBox.Show(kq, "Lỗi hệ thống");
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
                MessageBox.Show("Vui lòng chọn một dòng 'Chờ thanh toán' trên bảng để duyệt!", "Nhắc nhở");
                return;
            }

            int maHD = Convert.ToInt32(dgvLichSu.CurrentRow.Cells["Mã_HĐ"].Value);
            string trangThai = dgvLichSu.CurrentRow.Cells["Trạng_Thái"].Value.ToString();

            if (trangThai == "Đã thanh toán")
            {
                MessageBox.Show("Đơn này đã được thanh toán rồi!", "Thông báo");
                return;
            }

            if (MessageBox.Show("Xác nhận đã thu tiền cho hóa đơn #" + maHD + "?", "Xác nhận duyệt Online", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string kq = hdBUS.DuyetHoaDonOnline(maHD, Session.IdTaiKhoan);
                if (kq == "")
                {
                    MessageBox.Show("Duyệt thành công! Gói tập đã được kích hoạt cho khách.", "Thành công");
                    LoadLichSu();
                }
                else
                {
                    MessageBox.Show(kq, "Lỗi");
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
                MessageBox.Show("Vui lòng chọn 1 tin nhắn để xử lý.");
                return;
            }

            int id = Convert.ToInt32(dgvPhanHoi.CurrentRow.Cells["Mã_PH"].Value);
            string trangThai = dgvPhanHoi.CurrentRow.Cells["Trạng_Thái"].Value.ToString();

            if (trangThai == "Đã xử lý")
            {
                MessageBox.Show("Tin nhắn này đã được xử lý rồi!", "Thông báo");
                return;
            }

            if (hvBUS.DaXuLyPhanHoi(id))
            {
                MessageBox.Show("Đã đánh dấu xử lý xong!", "Thành công");
                LoadPhanHoi();
            }
        }

        private void dgvLichSu_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvLichSu.Columns[e.ColumnIndex].Name == "Trạng_Thái" && e.Value != null)
            {
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
        }

        private void dgvPhanHoi_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvPhanHoi.Columns[e.ColumnIndex].Name == "Trạng_Thái" && e.Value != null)
            {
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
        }

        private void dgvLichSu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
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
