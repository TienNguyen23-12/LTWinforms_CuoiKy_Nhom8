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
        HuanLuyenVienBUS hlvBUS = new HuanLuyenVienBUS();

        public ucLuongThuong()
        {
            InitializeComponent();
        }

        private void ucLuongThuong_Load(object sender, EventArgs e)
        {
            decimal tongTien = 0;
            dgvLuong.DataSource = hlvBUS.XemBangLuong(Session.IdTaiKhoan, out tongTien);

            txtTongLuong.Text = tongTien.ToString("N0") + " VNĐ";

            if (dgvLuong.Columns.Count > 0)
            {
                dgvLuong.Columns["MaLop"].HeaderText = "Mã Lớp";
                dgvLuong.Columns["TenLop"].HeaderText = "Tên Lớp Giảng Dạy";
                dgvLuong.Columns["ThoiGian"].HeaderText = "Lịch Dạy";
                dgvLuong.Columns["TienCong"].HeaderText = "Tiền Công (VNĐ)";

                dgvLuong.Columns["TienCong"].DefaultCellStyle.Format = "N0";
                dgvLuong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }
    }
}
