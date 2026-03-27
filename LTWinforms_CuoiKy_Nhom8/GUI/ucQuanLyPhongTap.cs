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
    public partial class ucQuanLyPhongTap : UserControl
    {
        PhongTapBUS ptBUS = new PhongTapBUS();
        QLTTDataContext db = new QLTTDataContext();

        public ucQuanLyPhongTap()
        {
            InitializeComponent();
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
            var listNhanVien = db.TaiKhoans.Where(x => x.Role == 2 && x.IsActive == true).ToList();
            listNhanVien.Insert(0, new TaiKhoan { 
                Id = 0, Username = "-- Chưa phân công --" 
            }); 

            cboPhuTrach.DataSource = listNhanVien;
            cboPhuTrach.DisplayMember = "Username";
            cboPhuTrach.ValueMember = "Id";

            LoadData();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaPhong.Text) || string.IsNullOrEmpty(txtTenPhong.Text))
            {
                MessageBox.Show("Vui lòng nhập đủ Mã và Tên phòng!", "Cảnh báo"); 
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
                MessageBox.Show("Thêm thành công!"); btnLamMoi_Click(sender, e); 
            }
            else
            {
                MessageBox.Show(kq, "Lỗi");
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
                MessageBox.Show("Sửa thành công!"); btnLamMoi_Click(sender, e); 
            }
            else
            {
                MessageBox.Show(kq, "Lỗi");
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
                MessageBox.Show("Cập nhật trạng thái thành công!"); 
                btnLamMoi_Click(sender, e); 
            }
            else
            {
                MessageBox.Show(kq, "Lỗi");
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaPhong.Clear(); 
            txtTenPhong.Clear(); 
            txtGhiChu.Clear(); 
            txtTimKiem.Clear();
            cboPhuTrach.SelectedValue = 0;
            txtMaPhong.Enabled = true;
            btnKhoa.Text = "Khóa";
            LoadData();
            txtMaPhong.Focus();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            LoadData(txtTimKiem.Text.Trim());
        }

        private void dgvPhongTap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvPhongTap.Rows[e.RowIndex];
                txtMaPhong.Text = row.Cells["MaPhong"].Value?.ToString();
                txtTenPhong.Text = row.Cells["TenPhong"].Value?.ToString();
                txtGhiChu.Text = row.Cells["GhiChu"].Value?.ToString();

                if (row.Cells["IdNguoiPhuTrach"].Value != null)
                {
                    cboPhuTrach.SelectedValue = Convert.ToInt32(row.Cells["IdNguoiPhuTrach"].Value);
                }
                else cboPhuTrach.SelectedValue = 0;

                txtMaPhong.Enabled = false; 
                btnKhoa.Text = row.Cells["TrangThai"].Value?.ToString() == "Tạm ngưng" ? "Mở khóa" : "Khóa";
            }
        }
    }
}
