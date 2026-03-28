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
    public partial class ucQuanLyNhanSu : UserControl
    {
        NhanSuBUS nsBUS = new NhanSuBUS();
        int idNhanSuDangChon = 0;

        public ucQuanLyNhanSu()
        {
            InitializeComponent();
        }

        private void LoadData(string tuKhoa = "")
        {
            dgvNhanSu.DataSource = nsBUS.LayDanhSachNhanSu(tuKhoa);

            if (dgvNhanSu.Columns.Count > 0)
            {
                dgvNhanSu.Columns["IdTaiKhoan"].Visible = false; 

                dgvNhanSu.Columns["Username"].HeaderText = "Tài Khoản";
                dgvNhanSu.Columns["VaiTro"].HeaderText = "Vai Trò";
                dgvNhanSu.Columns["HoTen"].HeaderText = "Họ và Tên";

                dgvNhanSu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                dgvNhanSu.Columns["Username"].FillWeight = 25; 
                dgvNhanSu.Columns["VaiTro"].FillWeight = 25;   
                dgvNhanSu.Columns["HoTen"].FillWeight = 50;    
            }
        }

        private void dgvNhanSu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvNhanSu.Rows[e.RowIndex];
                idNhanSuDangChon = Convert.ToInt32(row.Cells["IdTaiKhoan"].Value);
                string hoTen = row.Cells["HoTen"].Value.ToString();
                string vaiTro = row.Cells["VaiTro"].Value.ToString();

                lblTenNhanSu.Text = $"Đang chọn: {hoTen} ({vaiTro})";
            }
        }

        private void btnChamCong_Click(object sender, EventArgs e)
        {
            if (idNhanSuDangChon == 0)
            {
                MessageBox.Show("Vui lòng chọn 1 nhân sự từ danh sách trước!", "Cảnh báo");
                return;
            }

            string trangThai = radCoMat.Checked ? "Có mặt" : "Vắng mặt";

            string kq = nsBUS.ChamCongHomNay(idNhanSuDangChon, trangThai);
            if (kq == "")
            {
                MessageBox.Show($"Đã điểm danh [{trangThai}] thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(kq, "Lỗi");
            }
        }

        private void btnPhat_Click(object sender, EventArgs e)
        {
            if (idNhanSuDangChon == 0)
            {
                MessageBox.Show("Vui lòng chọn 1 nhân sự từ danh sách trước!", "Cảnh báo");
                return;
            }

            decimal tienPhat = 0;
            if (string.IsNullOrEmpty(txtTienPhat.Text) || !decimal.TryParse(txtTienPhat.Text.Replace(",", ""), out tienPhat))
            {
                MessageBox.Show("Vui lòng nhập số tiền phạt hợp lệ!", "Cảnh báo");
                return;
            }

            string lyDo = txtLyDoPhat.Text.Trim();
            if (string.IsNullOrEmpty(lyDo))
            {
                MessageBox.Show("Vui lòng nhập lý do phạt!", "Cảnh báo");
                return;
            }

            if (MessageBox.Show($"Xác nhận phạt nhân sự này số tiền {tienPhat:N0} VNĐ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                string kq = nsBUS.GhiNhanPhat(idNhanSuDangChon, tienPhat, lyDo);
                if (kq == "")
                {
                    MessageBox.Show("Đã ghi nhận phạt thành công!", "Thông báo");
                    txtTienPhat.Clear();
                    txtLyDoPhat.Clear();
                }
                else
                {
                    MessageBox.Show(kq, "Lỗi");
                }
            }
        }

        private void cQuanLyNhanSu_Load(object sender, EventArgs e)
        {
            LoadData();
            radCoMat.Checked = true; 
            lblTenNhanSu.Text = "Vui lòng chọn 1 nhân sự từ danh sách bên trái!";
        }
    }
}
