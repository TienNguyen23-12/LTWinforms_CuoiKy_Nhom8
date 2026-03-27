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
        DangKyLopBUS dkBUS = new DangKyLopBUS();
        QLTTDataContext db = new QLTTDataContext();

        public ucBanVeThuNgan()
        {
            InitializeComponent();
        }

        private void LoadLichSu()
        {
            dgvLichSu.DataSource = hdBUS.LayLichSuGiaoDich();
            if (dgvLichSu.Columns.Count > 0)
            {
                dgvLichSu.Columns["SoTien"].DefaultCellStyle.Format = "N0";
                dgvLichSu.Columns["NgayThanhToan"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                dgvLichSu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private void LoadDichVu()
        {
            if (radGoiTap.Checked)
            {
                cboDichVu.DataSource = db.GoiTaps.Where(x => x.IsActive == true).ToList();
                cboDichVu.DisplayMember = "TenGoi";
                cboDichVu.ValueMember = "MaGoi";
            }
            else if (radLopHoc.Checked)
            {
                if (cboHoiVien.SelectedValue == null) return;
                string maHV = cboHoiVien.SelectedValue.ToString();

                var listNoLop = (from dk in db.DangKyLops
                                 join lop in db.LopHocs on dk.MaLop equals lop.MaLop
                                 where dk.MaHoiVien == maHV && dk.TrangThaiThanhToan == "Chờ thanh toán"
                                 select new
                                 {
                                     IdDangKy = dk.Id, 
                                     TenHienThi = lop.TenLop,
                                     GiaTien = lop.GiaTien
                                 }).ToList();

                cboDichVu.DataSource = listNoLop;
                cboDichVu.DisplayMember = "TenHienThi";
                cboDichVu.ValueMember = "IdDangKy";

                if (listNoLop.Count == 0)
                {
                    cboDichVu.DataSource = null;
                    txtSoTien.Text = "0 VNĐ";
                }
            }
        }

        private void cboHoiVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboHoiVien.SelectedValue != null && cboHoiVien.SelectedValue is string)
            {
                if (radLopHoc.Checked)
                {
                    LoadDichVu(); 
                }
            }
        }

        private void cboDichVu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboDichVu.SelectedItem == null)
            {
                txtSoTien.Text = "0 VNĐ";
                return;
            }

            try
            {
                if (radGoiTap.Checked)
                {
                    var goiDangChon = (GoiTap)cboDichVu.SelectedItem;

                    decimal giaTien = Convert.ToDecimal(goiDangChon.GiaTien);
                    txtSoTien.Text = giaTien.ToString("N0") + " VNĐ";
                }
                else if (radLopHoc.Checked)
                {
                    var type = cboDichVu.SelectedItem.GetType();
                    var prop = type.GetProperty("GiaTien");

                    if (prop != null)
                    {
                        var giaTienObj = prop.GetValue(cboDichVu.SelectedItem, null);
                        decimal giaTien = Convert.ToDecimal(giaTienObj);
                        txtSoTien.Text = giaTien.ToString("N0") + " VNĐ";
                    }
                }
            }
            catch (Exception)
            {
                txtSoTien.Text = "0 VNĐ";
            }
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (cboHoiVien.SelectedValue == null || cboDichVu.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn Khách hàng và Dịch vụ cần thanh toán!", "Cảnh báo");
                return;
            }

            string maHV = cboHoiVien.SelectedValue.ToString();
            int idNhanVien = Session.IdTaiKhoan;

            if (radGoiTap.Checked)
            {
                string maGoi = cboDichVu.SelectedValue.ToString();
                if (MessageBox.Show($"Xác nhận bán gói Gym cho khách {cboHoiVien.Text}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string kq = hdBUS.ThanhToanGiaHan(maHV, maGoi, idNhanVien);
                    if (kq == "")
                    {
                        MessageBox.Show("Thanh toán thành công! Thẻ của khách đã được gia hạn.", "Thành công");
                        LoadLichSu();
                    }
                    else MessageBox.Show(kq, "Lỗi hệ thống");
                }
            }
            else if (radLopHoc.Checked)
            {
                int idDangKy = Convert.ToInt32(cboDichVu.SelectedValue);
                if (MessageBox.Show($"Xác nhận thu tiền lớp học này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string kq = dkBUS.XacNhanThuTien(idDangKy, Session.IdTaiKhoan);
                    if (kq == "")
                    {
                        MessageBox.Show("Thanh toán lớp học thành công!", "Thành công");
                        LoadLichSu();
                        LoadDichVu(); 
                    }
                    else MessageBox.Show(kq, "Lỗi hệ thống");
                }
            }
        }

        private void dgvLichSu_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ucBanVeThuNgan_Load(object sender, EventArgs e)
        {
            cboHoiVien.DataSource = db.HoiViens.Where(x => x.IsActive == true).ToList();
            cboHoiVien.DisplayMember = "HoTen";
            cboHoiVien.ValueMember = "MaHoiVien";

            LoadLichSu();
            LoadDichVu();
        }

        private void radGoiTap_CheckedChanged(object sender, EventArgs e)
        {
            if (radGoiTap.Checked)
            {
                LoadDichVu();
            }
        }

        private void radLopHoc_CheckedChanged(object sender, EventArgs e)
        {
            if (radLopHoc.Checked)
            {
                LoadDichVu();
            }
        }
    }
}
