using LTWinforms_CuoiKy_Nhom8.BUS;
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
    public partial class ucQuanLySanPham : UserControl
    {
        SanPhamBUS spBUS = new SanPhamBUS();
        int maSPDangChon = 0;

        public ucQuanLySanPham()
        {
            InitializeComponent();
        }

        private void ucQuanLySanPham_Load(object sender, EventArgs e)
        {
            LoadData();
            btnSua.Enabled = false;
        }

        private void LoadData()
        {
            dgvSanPham.DataSource = spBUS.LayDanhSachSanPhamAdmin();
            if (dgvSanPham.Columns.Count > 0)
            {
                dgvSanPham.Columns["MaSP"].Visible = false;
                dgvSanPham.Columns["GiaTien"].DefaultCellStyle.Format = "N0";
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
                chkDangBan.Checked = (trangThai == "Đang bán");

                btnSua.Enabled = true;
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
            if (string.IsNullOrEmpty(ten)) { MessageBox.Show("Tên không được để trống"); return; }

            decimal gia = decimal.Parse(txtGiaTien.Text);

            string kq = spBUS.LuuSanPham(maSPDangChon, ten, gia, chkDangBan.Checked);
            if (kq == "")
            {
                MessageBox.Show("Đã lưu thông tin sản phẩm!");
                LoadData();
            }
            else
            {
                MessageBox.Show("Lỗi: " + kq);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (maSPDangChon == 0)
            {
                MessageBox.Show("Vui lòng chọn một sản phẩm từ danh sách trước khi sửa!", "Thông báo");
                return;
            }

            string tenMoi = txtTenSP.Text.Trim();
            decimal giaMoi = 0;

            if (string.IsNullOrEmpty(tenMoi))
            {
                MessageBox.Show("Tên sản phẩm không được để trống!");
                return;
            }

            if (!decimal.TryParse(txtGiaTien.Text, out giaMoi))
            {
                MessageBox.Show("Giá tiền phải là số hợp lệ!");
                return;
            }

            if (MessageBox.Show($"Bạn có chắc muốn cập nhật thông tin cho sản phẩm này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string kq = spBUS.CapNhatSanPham(maSPDangChon, tenMoi, giaMoi, chkDangBan.Checked);

                if (kq == "")
                {
                    MessageBox.Show("Cập nhật sản phẩm thành công!", "Thành công");
                    LoadData(); 
                    btnSua.Enabled = false; 
                    maSPDangChon = 0; 
                }
                else
                {
                    MessageBox.Show("Lỗi: " + kq);
                }
            }
        }
    }
}
