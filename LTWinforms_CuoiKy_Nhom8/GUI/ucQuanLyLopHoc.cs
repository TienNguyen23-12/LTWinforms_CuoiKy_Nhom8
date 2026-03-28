using LTWinforms_CuoiKy_Nhom8.BUS;
using LTWinforms_CuoiKy_Nhom8.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LTWinforms_CuoiKy_Nhom8.GUI
{
    public partial class ucQuanLyLopHoc : UserControl
    {
        LopHocBUS lopBUS = new LopHocBUS();
        QLTTDataContext db = new QLTTDataContext();

        public ucQuanLyLopHoc()
        {
            InitializeComponent();
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

                if (dgvLopHoc.Columns.Contains("MaLop")) dgvLopHoc.Columns["MaLop"].HeaderText = " Mã Lớp";
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
                if (dgvLopHoc.Columns.Contains("NgayBatDau")) dgvLopHoc.Columns["NgayBatDau"].HeaderText = "Ngày Bắt Đầu";
                if (dgvLopHoc.Columns.Contains("TrangThai")) dgvLopHoc.Columns["TrangThai"].HeaderText = "Trạng Thái";

                dgvLopHoc.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private void ucQuanLyLopHoc_Load(object sender, EventArgs e)
        {
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

            listLocHLV.Insert(0, new HuanLuyenVien { MaHLV = "NULL", TenHLV = "-- Lớp chưa phân công HLV --" });

            listLocHLV.Insert(0, new HuanLuyenVien { MaHLV = "", TenHLV = "-- Tất cả HLV --" });

            cboLocHLV.DataSource = listLocHLV;
            cboLocHLV.DisplayMember = "TenHLV";
            cboLocHLV.ValueMember = "MaHLV";

            if (cboTrangThai.Items.Count > 0) cboTrangThai.SelectedIndex = 0;

            txtTimKiem_Leave(sender, e);
            LoadData();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaLop.Clear();
            txtTenLop.Clear();
            txtThoiGian.Clear();
            txtGiaTien.Clear();
            txtSoLuongToiDa.Clear();
            txtMaLop.Enabled = true;
            txtSoBuoi.Clear();

            cboHLV.SelectedIndex = 0;
            cboPhongTap.SelectedIndex = 0;
            if (cboTrangThai.Items.Count > 0) cboTrangThai.SelectedIndex = 0;
            dtpNgayBatDau.Value = DateTime.Now;

            cboLocHLV.SelectedIndex = 0;
            dtpLocTuNgay.Checked = false;
            dtpLocDenNgay.Checked = false;

            txtTimKiem.Text = "";
            txtTimKiem_Leave(sender, e); 

            LoadData();
            txtMaLop.Focus();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaLop.Text) || string.IsNullOrEmpty(txtTenLop.Text))
            {
                MessageBox.Show("Vui lòng nhập đủ Mã lớp và Tên lớp!", "Cảnh báo");
                return;
            }

            decimal giaTien = 0;
            if (!string.IsNullOrEmpty(txtGiaTien.Text) && !decimal.TryParse(txtGiaTien.Text.Replace(",", ""), out giaTien))
            {
                MessageBox.Show("Giá tiền phải là số hợp lệ!", "Lỗi");
                return;
            }

            int siSo = 1;
            if (!string.IsNullOrEmpty(txtSoLuongToiDa.Text) && !int.TryParse(txtSoLuongToiDa.Text, out siSo))
            {
                MessageBox.Show("Sĩ số tối đa phải là số nguyên!", "Lỗi");
                return;
            }

            int soBuoi = 0;
            if (!string.IsNullOrEmpty(txtSoBuoi.Text) && !int.TryParse(txtSoBuoi.Text, out soBuoi))
            {
                MessageBox.Show("Số buổi học phải là số nguyên!", "Lỗi");
                return;
            }

            string hlvChon = cboHLV.SelectedValue?.ToString();
            string phongChon = cboPhongTap.SelectedValue?.ToString();

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
                TrangThai = cboTrangThai.SelectedItem?.ToString() ?? "Chuẩn bị",
                IsActive = true
            };

            string kq = lopBUS.ThemLop(lop);
            if (kq == "")
            {
                MessageBox.Show("Thêm thành công!");
                btnLamMoi_Click(sender, e);
            }
            else
            {
                MessageBox.Show(kq);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaLop.Text))
            {
                return;
            }

            decimal giaTien = 0;
            if (!string.IsNullOrEmpty(txtGiaTien.Text) && !decimal.TryParse(txtGiaTien.Text.Replace(",", ""), out giaTien)) return;

            int siSo = 1;
            if (!string.IsNullOrEmpty(txtSoLuongToiDa.Text) && !int.TryParse(txtSoLuongToiDa.Text, out siSo)) return;

            int soBuoi = 0;
            if (!string.IsNullOrEmpty(txtSoBuoi.Text) && !int.TryParse(txtSoBuoi.Text, out soBuoi)) return;

            string hlvChon = cboHLV.SelectedValue?.ToString();
            string phongChon = cboPhongTap.SelectedValue?.ToString();

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
                TrangThai = cboTrangThai.SelectedItem?.ToString() ?? "Chuẩn bị"
            };

            string kq = lopBUS.SuaLop(lop);
            if (kq == "")
            {
                MessageBox.Show("Sửa thành công!");
                btnLamMoi_Click(sender, e);
            }
            else
            {
                MessageBox.Show(kq);
            }
        }

        private void btnKhoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaLop.Text))
            {
                return;
            }

            if (MessageBox.Show("Cập nhật trạng thái lớp học này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string kq = lopBUS.KhoaLop(txtMaLop.Text.Trim());
                if (kq == "")
                {
                    MessageBox.Show("Đã cập nhật!");
                    btnLamMoi_Click(sender, e);
                }
                else
                {
                    MessageBox.Show(kq, "Lỗi");
                }
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string tuKhoa = (txtTimKiem.Text == "Nhập từ khóa tìm kiếm...") ? "" : txtTimKiem.Text.Trim();
            string locHLV = cboLocHLV.SelectedValue?.ToString();

            DateTime? tuNgay = null;
            DateTime? denNgay = null;

            if (dtpLocTuNgay.Checked && dtpLocDenNgay.Checked)
            {
                tuNgay = dtpLocTuNgay.Value.Date;
                denNgay = dtpLocDenNgay.Value.Date;
            }

            LoadData(tuKhoa, locHLV, tuNgay, denNgay);
        }

        private void dgvLopHoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvLopHoc.Rows[e.RowIndex];

                txtMaLop.Text = row.Cells["MaLop"].Value?.ToString();
                txtTenLop.Text = row.Cells["TenLop"].Value?.ToString();
                txtThoiGian.Text = row.Cells["ThoiGian"].Value?.ToString();

                if (row.Cells["GiaTien"].Value != null) txtGiaTien.Text = Convert.ToDecimal(row.Cells["GiaTien"].Value).ToString("0");
                else txtGiaTien.Text = "0";

                if (row.Cells["SoLuongToiDa"].Value != null) txtSoLuongToiDa.Text = row.Cells["SoLuongToiDa"].Value.ToString();
                else txtSoLuongToiDa.Text = "1";

                if (row.Cells["SoBuoi"].Value != null) txtSoBuoi.Text = row.Cells["SoBuoi"].Value.ToString();
                else txtSoBuoi.Text = "0";

                if (row.Cells["NgayBatDau"].Value != null) dtpNgayBatDau.Value = Convert.ToDateTime(row.Cells["NgayBatDau"].Value);
                if (row.Cells["TrangThai"].Value != null) cboTrangThai.Text = row.Cells["TrangThai"].Value.ToString();

                string tenHLV = row.Cells["TenHLV"].Value?.ToString();
                int idxHLV = cboHLV.FindStringExact(tenHLV);
                cboHLV.SelectedIndex = idxHLV >= 0 ? idxHLV : 0;

                string tenPhong = row.Cells["PhongTap"].Value?.ToString();
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
    }
}
