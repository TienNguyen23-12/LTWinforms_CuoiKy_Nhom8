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
    public partial class ucQuanLyHoiVien : UserControl
    {
        HoiVienBUS hvBUS = new HoiVienBUS();

        public ucQuanLyHoiVien()
        {
            InitializeComponent();
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
            }
        }

        private void ucQuanLyHoiVien_Load(object sender, EventArgs e)
        {
            LoadData();
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
                MessageBox.Show("Vui lòng nhập đủ Mã và Họ Tên!", "Cảnh báo");
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
                MessageBox.Show("Thêm hội viên thành công!", "Thông báo");
                LoadData();
                btnLamMoi_Click(sender, e); 
            }
            else
            {
                MessageBox.Show(kq, "Lỗi");
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
                MessageBox.Show("Cập nhật thành công!", "Thông báo");
                LoadData();
                btnLamMoi_Click(sender, e);
            }
            else
            {
                MessageBox.Show(kq, "Lỗi");
            }
        }

        private void btnKhoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn khóa hội viên này?", "Xác nhận", 
                                    MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string kq = hvBUS.KhoaHoiVien(txtMaHoiVien.Text.Trim());
                if (kq == "")
                {
                    MessageBox.Show("Đã khóa hội viên!", "Thông báo");
                    LoadData();
                    btnLamMoi_Click(sender, e);
                }
                else
                {
                    MessageBox.Show(kq, "Lỗi");
                }
            }
        }

        private void dgvHoiVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvHoiVien.Rows[e.RowIndex];
                txtMaHoiVien.Text = row.Cells["MaHoiVien"].Value?.ToString();
                txtHoTen.Text = row.Cells["HoTen"].Value?.ToString();
                cboGioiTinh.Text = row.Cells["GioiTinh"].Value?.ToString();
                txtSDT.Text = row.Cells["SDT"].Value?.ToString();

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
