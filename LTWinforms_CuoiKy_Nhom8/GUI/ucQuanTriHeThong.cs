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
    public partial class ucQuanTriHeThong : UserControl
    {
        QuanTriBUS qtBUS = new QuanTriBUS();
        int idDangChon = 0;

        public ucQuanTriHeThong()
        {
            InitializeComponent();
        }

        private void LoadData()
        {
            dgvNhanSu.DataSource = qtBUS.LayDanhSachTaiKhoan();

            if (dgvNhanSu.Columns.Count > 0)
            {
                dgvNhanSu.Columns["Id"].Visible = false;
                dgvNhanSu.Columns["Username"].HeaderText = "Tài Khoản";
                dgvNhanSu.Columns["VaiTro"].HeaderText = "Vai Trò";
                dgvNhanSu.Columns["TenHienThi"].HeaderText = "Tên Người Dùng";
                dgvNhanSu.Columns["LuongCoBan"].HeaderText = "Lương CB / Lớp (VNĐ)";
                dgvNhanSu.Columns["TrangThai"].HeaderText = "Trạng Thái";

                dgvNhanSu.Columns["LuongCoBan"].DefaultCellStyle.Format = "N0";
                dgvNhanSu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private void ucQuanTriHeThong_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dgvNhanSu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvNhanSu.Rows[e.RowIndex];
                idDangChon = Convert.ToInt32(row.Cells["Id"].Value);

                txtUsername.Text = row.Cells["Username"].Value.ToString();
                cboVaiTro.Text = row.Cells["VaiTro"].Value.ToString();
                txtTenHienThi.Text = row.Cells["TenHienThi"].Value.ToString();

                string trangThai = row.Cells["TrangThai"].Value.ToString();

                if (trangThai == "Bị khóa")
                {
                    btnKhoaTaiKhoan.Text = "Mở Khóa Tài Khoản";
                }
                else
                {
                    btnKhoaTaiKhoan.Text = "Khóa Tài Khoản";
                }

                if (cboVaiTro.Text == "Huấn Luyện Viên")
                {
                    txtLuongCoBan.Text = Convert.ToDecimal(row.Cells["LuongCoBan"].Value).ToString("0");
                    txtLuongCoBan.Enabled = true; 
                }
                else
                {
                    txtLuongCoBan.Text = "0";
                    txtLuongCoBan.Enabled = false; 
                }
            }
        }

        private void btnLuuThongTin_Click(object sender, EventArgs e)
        {
            if (idDangChon == 0)
            {
                return;
            }

            string kq = qtBUS.CapNhatThongTin(idDangChon, txtUsername.Text.Trim(), cboVaiTro.Text, txtTenHienThi.Text.Trim());

            if (kq == "")
            {
                MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo");
                LoadData();
            }
            else
            {
                MessageBox.Show(kq, "Lỗi");
            }
        }

        private void btnCapNhatLuong_Click(object sender, EventArgs e)
        {
            if (idDangChon == 0)
            {
                MessageBox.Show("Vui lòng chọn một nhân sự từ danh sách!", "Nhắc nhở", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (cboVaiTro.Text != "Huấn Luyện Viên")
            {
                MessageBox.Show("Chức năng này chỉ áp dụng cho Huấn Luyện Viên!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal luongMoi = 0;
            if (!decimal.TryParse(txtLuongCoBan.Text, out luongMoi))
            {
                MessageBox.Show("Vui lòng nhập số tiền lương hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string kq = qtBUS.CapNhatLuong(idDangChon, luongMoi);

            if (kq == "")
            {
                MessageBox.Show("Cập nhật lương thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
            else
            {
                MessageBox.Show(kq, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnKhoaTaiKhoan_Click(object sender, EventArgs e)
        {
            if (idDangChon == 0)
            {
                return;
            }

            string hanhDong = btnKhoaTaiKhoan.Text.Contains("Mở") ? "mở khóa" : "khóa";

            if (MessageBox.Show($"Bạn có chắc chắn muốn {hanhDong} tài khoản này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                string kq = qtBUS.KhoaMoTaiKhoan(idDangChon);
                if (kq == "")
                {
                    MessageBox.Show($"Đã {hanhDong} thành công!");
                    LoadData();
                }
                else
                {
                    MessageBox.Show(kq, "Lỗi");
                }
            }
        }

        private void btnResetPass_Click(object sender, EventArgs e)
        {
            if (idDangChon == 0)
            {
                return;
            }

            if (MessageBox.Show($"Bạn có chắc muốn đặt lại mật khẩu về mặc định (123456)?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string kq = qtBUS.DatLaiMatKhau(idDangChon);
                if (kq == "")
                {
                    MessageBox.Show("Đã đặt lại mật khẩu thành công!");
                }
                else
                {
                    MessageBox.Show(kq);
                }
            }
        }

        private void cboVaiTro_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboVaiTro.Text == "Huấn Luyện Viên")
            {
                txtLuongCoBan.Enabled = true;
            }
            else
            {
                txtLuongCoBan.Text = "0";
                txtLuongCoBan.Enabled = false;
            }
        }
    }
}
