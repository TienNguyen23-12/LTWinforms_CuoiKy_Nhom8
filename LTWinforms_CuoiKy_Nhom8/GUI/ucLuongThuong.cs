using LTWinforms_CuoiKy_Nhom8.BUS;
using LTWinforms_CuoiKy_Nhom8.DTO;
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
    public partial class ucLuongThuong : UserControl
    {
        NhanSuBUS nsBUS = new NhanSuBUS();

        public ucLuongThuong()
        {
            InitializeComponent();
        }

        private void ucLuongThuong_Load(object sender, EventArgs e)
        {
            dtpTuNgay.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpDenNgay.Value = DateTime.Now;

            btnXemLuong_Click(sender, e);
        }

        private void btnXemLuong_Click(object sender, EventArgs e)
        {
            DateTime tuNgay = dtpTuNgay.Value.Date;
            DateTime denNgay = dtpDenNgay.Value.Date;

            if (Session.Role == 1) 
            {
                dgvBangLuong.DataSource = nsBUS.TinhBangLuongChiTiet(tuNgay, denNgay);
            }
            else 
            {
                dgvBangLuong.DataSource = nsBUS.TinhLuongCaNhan(Session.IdTaiKhoan, Session.Role, tuNgay, denNgay);
            }

            if (dgvBangLuong.Columns.Count > 0)
            {
                dgvBangLuong.Columns["MaNhanSu"].HeaderText = "Mã NS";
                dgvBangLuong.Columns["HoTen"].HeaderText = "Họ và Tên";
                dgvBangLuong.Columns["VaiTro"].HeaderText = "Vai Trò";
                dgvBangLuong.Columns["SoNgayLam"].HeaderText = "Số Công / Buổi";

                dgvBangLuong.Columns["TienPhat"].HeaderText = "Bị Phạt (VNĐ)";
                dgvBangLuong.Columns["TienPhat"].DefaultCellStyle.Format = "N0";
                dgvBangLuong.Columns["TienPhat"].DefaultCellStyle.ForeColor = Color.Red;

                dgvBangLuong.Columns["ThucLanh"].HeaderText = "Thực Lãnh (VNĐ)";
                dgvBangLuong.Columns["ThucLanh"].DefaultCellStyle.Format = "N0";
                dgvBangLuong.Columns["ThucLanh"].DefaultCellStyle.ForeColor = Color.Green;
                dgvBangLuong.Columns["ThucLanh"].DefaultCellStyle.Font = new Font(dgvBangLuong.Font, FontStyle.Bold);

                dgvBangLuong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvBangLuong.RowHeadersVisible = false;
            }

            TinhTongLuong();
        }

        private void TinhTongLuong()
        {
            decimal tongTien = 0;

            foreach (DataGridViewRow row in dgvBangLuong.Rows)
            {
                if (row.Cells["ThucLanh"].Value != null)
                {
                    tongTien += Convert.ToDecimal(row.Cells["ThucLanh"].Value);
                }
            }

            txtTongLuong.Text = tongTien.ToString("N0") + " VNĐ";
            txtTongLuong.ForeColor = Color.DarkGreen;
            txtTongLuong.Font = new Font(txtTongLuong.Font, FontStyle.Bold);
        }
    }
}
