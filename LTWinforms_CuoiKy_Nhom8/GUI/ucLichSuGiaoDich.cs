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
    public partial class ucLichSuGiaoDich : UserControl
    {
        HoaDonBUS hdBUS = new HoaDonBUS();
        public ucLichSuGiaoDich()
        {
            InitializeComponent();
        }

        private void ucLichSuGiaoDich_Load(object sender, EventArgs e)
        {
            dgvLichSu.DataSource = hdBUS.LayLichSuCuaKhachHang(Session.IdTaiKhoan);

            if (dgvLichSu.Columns.Count > 0)
            {
                dgvLichSu.Columns["MaHoaDon"].HeaderText = "Mã Hóa Đơn";
                dgvLichSu.Columns["TenGoi"].HeaderText = "Dịch Vụ / Lớp Học";
                dgvLichSu.Columns["SoTien"].HeaderText = "Số Tiền (VNĐ)";
                dgvLichSu.Columns["NgayThanhToan"].HeaderText = "Ngày Giao Dịch";
                dgvLichSu.Columns["TrangThai"].HeaderText = "Trạng Thái";

                dgvLichSu.Columns["SoTien"].DefaultCellStyle.Format = "N0";
                dgvLichSu.Columns["NgayThanhToan"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                dgvLichSu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }
    }
}
