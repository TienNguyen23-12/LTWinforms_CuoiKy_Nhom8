using LTWinforms_CuoiKy_Nhom8.BUS;
using LTWinforms_CuoiKy_Nhom8.DAL;
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
    public partial class ucBanVeThuNgan : UserControl
    {
        HoaDonBUS hdBUS = new HoaDonBUS();
        QLTTDataContext db = new QLTTDataContext();

        public ucBanVeThuNgan()
        {
            InitializeComponent();
        }

        private void LoadData()
        {
            dgvLichSu.DataSource = hdBUS.LayLichSuGiaoDich();
            if (dgvLichSu.Columns.Count > 0)
            {
                dgvLichSu.Columns["SoTien"].DefaultCellStyle.Format = "N0";
                dgvLichSu.Columns["NgayThanhToan"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            }

            cboHoiVien.DataSource = db.HoiViens.Where(x => x.IsActive == true).ToList();
            cboHoiVien.DisplayMember = "HoTen"; 
            cboHoiVien.ValueMember = "MaHoiVien"; 

            cboGoiTap.DataSource = db.GoiTaps.Where(x => x.IsActive == true).ToList();
            cboGoiTap.DisplayMember = "TenGoi";
            cboGoiTap.ValueMember = "MaGoi";
        }

        private void cboHoiVien_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cboGoiTap_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboGoiTap.SelectedItem != null)
            {
                var goiDangChon = (GoiTap)cboGoiTap.SelectedItem;
                txtSoTien.Text = goiDangChon.GiaTien.ToString("N0") + " VNĐ";
            }
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (cboHoiVien.SelectedValue == null || cboGoiTap.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn Khách hàng và Gói tập!", "Cảnh báo");
                return;
            }

            string maHV = cboHoiVien.SelectedValue.ToString();
            string maGoi = cboGoiTap.SelectedValue.ToString();
            int idNhanVien = Session.IdTaiKhoan; 

            if (MessageBox.Show($"Xác nhận thu tiền gói {cboGoiTap.Text} của khách {cboHoiVien.Text}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string kq = hdBUS.ThanhToanGiaHan(maHV, maGoi, idNhanVien);
                if (kq == "")
                {
                    MessageBox.Show("Thanh toán thành công! Thẻ của khách đã được gia hạn.", "Thành công");
                    LoadData();
                }
                else
                {
                    MessageBox.Show(kq, "Lỗi hệ thống");
                }
            }
        }

        private void dgvLichSu_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ucBanVeThuNgan_Load(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
