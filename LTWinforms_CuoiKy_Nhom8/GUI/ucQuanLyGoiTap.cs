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
    public partial class ucQuanLyGoiTap : UserControl
    {
        GoiTapBUS gtBUS = new GoiTapBUS();

        public ucQuanLyGoiTap()
        {
            InitializeComponent();
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
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaGoi.Text) || string.IsNullOrEmpty(txtTenGoi.Text))
            {
                MessageBox.Show("Nhập đủ thông tin Mã và Tên gói!", "Cảnh báo"); return;
            }

            GoiTap gt = new GoiTap()
            {
                MaGoi = txtMaGoi.Text.Trim(),
                TenGoi = txtTenGoi.Text.Trim(),
                ThoiHanThang = int.Parse(txtThoiHan.Text.Trim()),
                GiaTien = decimal.Parse(txtGiaTien.Text.Trim()),
                IsActive = true
            };

            string kq = gtBUS.ThemGoiTap(gt);
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
            GoiTap gt = new GoiTap()
            {
                MaGoi = txtMaGoi.Text.Trim(),
                TenGoi = txtTenGoi.Text.Trim(),
                ThoiHanThang = int.Parse(txtThoiHan.Text.Trim()),
                GiaTien = decimal.Parse(txtGiaTien.Text.Trim())
            };
            string kq = gtBUS.SuaGoiTap(gt);
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
            if (MessageBox.Show("Khóa gói tập này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string kq = gtBUS.KhoaGoiTap(txtMaGoi.Text.Trim());
                if (kq == "") 
                { 
                    MessageBox.Show("Đã khóa!"); 
                    btnLamMoi_Click(sender, e); 
                }
                else
                {
                    MessageBox.Show(kq);
                }
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaGoi.Clear();
            txtTenGoi.Clear();
            txtThoiHan.Clear();
            txtGiaTien.Clear();
            txtTimKiem.Clear();

            txtMaGoi.Enabled = true;
            txtMaGoi.Focus();
            LoadData();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            LoadData(txtTimKiem.Text.Trim());
        }

        private void dgvGoiTap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvGoiTap.Rows[e.RowIndex];
                txtMaGoi.Text = row.Cells["MaGoi"].Value?.ToString();
                txtTenGoi.Text = row.Cells["TenGoi"].Value?.ToString();
                txtThoiHan.Text = row.Cells["ThoiHanThang"].Value?.ToString();

                txtGiaTien.Text = Convert.ToDecimal(row.Cells["GiaTien"].Value).ToString("0");
                txtMaGoi.Enabled = false;
            }
        }

        private void ucQuanLyGoiTap_Load(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
