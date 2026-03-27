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

        private void LoadData(string tuKhoa = "")
        {
            dgvLopHoc.DataSource = lopBUS.LayDanhSachLopHoc(tuKhoa);

            if (dgvLopHoc.Columns.Count > 0)
            {
                if (dgvLopHoc.Columns.Contains("MaPhongTap"))
                {
                    dgvLopHoc.Columns["MaPhongTap"].Visible = false;
                }

                dgvLopHoc.Columns["MaLop"].HeaderText = "Mã Lớp";
                dgvLopHoc.Columns["TenLop"].HeaderText = "Tên Lớp Học";
                dgvLopHoc.Columns["TenHLV"].HeaderText = "Huấn Luyện Viên";
                dgvLopHoc.Columns["ThoiGian"].HeaderText = "Thời Gian";
                dgvLopHoc.Columns["PhongTap"].HeaderText = "Phòng Tập";

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
            listPhong.Insert(0, new PhongTap { 
                MaPhong = "", TenPhong = "-- Chọn phòng tập --" 
            }); 

            cboPhongTap.DataSource = listPhong;
            cboPhongTap.DisplayMember = "TenPhong";
            cboPhongTap.ValueMember = "MaPhong";

            LoadData();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaLop.Clear();
            txtTenLop.Clear();
            txtThoiGian.Clear();
            txtMaLop.Enabled = true;

            cboHLV.SelectedIndex = 0; 
            cboPhongTap.SelectedIndex = 0; 

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

            string hlvChon = cboHLV.SelectedValue?.ToString();
            string phongChon = cboPhongTap.SelectedValue?.ToString();

            LopHoc lop = new LopHoc()
            {
                MaLop = txtMaLop.Text.Trim(),
                TenLop = txtTenLop.Text.Trim(),
                MaHLV = string.IsNullOrEmpty(hlvChon) ? null : hlvChon,
                ThoiGian = txtThoiGian.Text.Trim(),
                PhongTap = string.IsNullOrEmpty(phongChon) ? null : phongChon,
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

            string hlvChon = cboHLV.SelectedValue?.ToString();
            string phongChon = cboPhongTap.SelectedValue?.ToString();

            LopHoc lop = new LopHoc()
            {
                MaLop = txtMaLop.Text.Trim(),
                TenLop = txtTenLop.Text.Trim(),
                MaHLV = string.IsNullOrEmpty(hlvChon) ? null : hlvChon,
                ThoiGian = txtThoiGian.Text.Trim(),
                PhongTap = string.IsNullOrEmpty(phongChon) ? null : phongChon
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
            LoadData(txtTimKiem.Text.Trim());
        }

        private void dgvLopHoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvLopHoc.Rows[e.RowIndex];

                txtMaLop.Text = row.Cells["MaLop"].Value?.ToString();
                txtTenLop.Text = row.Cells["TenLop"].Value?.ToString();
                txtThoiGian.Text = row.Cells["ThoiGian"].Value?.ToString();

                string tenHLV = row.Cells["TenHLV"].Value?.ToString();
                int idxHLV = cboHLV.FindStringExact(tenHLV);
                cboHLV.SelectedIndex = idxHLV >= 0 ? idxHLV : 0;

                string tenPhong = row.Cells["PhongTap"].Value?.ToString();
                int idxPhong = cboPhongTap.FindStringExact(tenPhong);
                cboPhongTap.SelectedIndex = idxPhong >= 0 ? idxPhong : 0;

                txtMaLop.Enabled = false;
            }
        }
    }
}
