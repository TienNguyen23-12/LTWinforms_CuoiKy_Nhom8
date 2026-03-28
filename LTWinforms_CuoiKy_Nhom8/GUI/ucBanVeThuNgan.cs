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
        HoiVienBUS hvBUS = new HoiVienBUS();
        QLTTDataContext db = new QLTTDataContext();

        public ucBanVeThuNgan()
        {
            InitializeComponent();
            dgvLichSu.CellFormatting += dgvLichSu_CellFormatting;
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
            cboDichVu.DataSource = null;
            txtSoTien.Text = "0 VNĐ";

            if (radGoiTap.Checked)
            {
                cboDichVu.DataSource = db.GoiTaps.Where(x => x.IsActive == true).ToList();
                cboDichVu.DisplayMember = "TenGoi";
                cboDichVu.ValueMember = "MaGoi";
            }
            else if (radLopHoc.Checked)
            {
                if (cboHoiVien.SelectedValue == null)
                {
                    return;
                }
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
            else if (radSanPham.Checked)
            {
                cboDichVu.DataSource = db.SanPhams.Where(x => x.IsActive == true).ToList();
                cboDichVu.DisplayMember = "TenSP";
                cboDichVu.ValueMember = "MaSP";
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
                var type = cboDichVu.SelectedItem.GetType();
                var prop = type.GetProperty("GiaTien");

                if (prop != null)
                {
                    var giaTienObj = prop.GetValue(cboDichVu.SelectedItem, null);
                    decimal giaTien = Convert.ToDecimal(giaTienObj);
                    txtSoTien.Text = giaTien.ToString("N0") + " VNĐ";
                }
                else
                {
                    txtSoTien.Text = "0 VNĐ";
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
                        MessageBox.Show("Thanh toán thành công!", "Thành công");
                        LoadLichSu();
                    }
                    else MessageBox.Show(kq, "Lỗi hệ thống");
                }
            }
            else if (radLopHoc.Checked)
            {
                int idDangKy = Convert.ToInt32(cboDichVu.SelectedValue);
                if (MessageBox.Show($"Xác nhận thu tiền lớp học này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
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
            else if (radSanPham.Checked)
            {
                string tenSP = cboDichVu.Text; 

                decimal giaTien = Convert.ToDecimal(txtSoTien.Text.Replace(" VNĐ", "").Replace(",", ""));

                if (MessageBox.Show($"Xác nhận bán [{tenSP}] cho khách {cboHoiVien.Text}?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    string kq = hdBUS.BanSanPhamTaiQuay(maHV, tenSP, giaTien, Session.IdTaiKhoan);

                    if (kq == "")
                    {
                        MessageBox.Show("Thanh toán thành công!", "Thành công");
                        LoadLichSu(); 
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
                btnDuyetOnline.Enabled = true;
            }
        }

        private void radLopHoc_CheckedChanged(object sender, EventArgs e)
        {
            if (radLopHoc.Checked)
            {
                LoadDichVu();
                btnDuyetOnline.Enabled = true;
            }
        }

        private void btnDuyetOnline_Click(object sender, EventArgs e)
        {
            if (dgvLichSu.CurrentRow != null)
            {
                int maHD = Convert.ToInt32(dgvLichSu.CurrentRow.Cells["Mã_HĐ"].Value);
                string trangThai = dgvLichSu.CurrentRow.Cells["Trạng_Thái"].Value.ToString();

                if (trangThai == "Đã thanh toán")
                {
                    MessageBox.Show("Đơn này đã được thanh toán rồi!", "Thông báo");
                    return;
                }

                if (MessageBox.Show($"Xác nhận đã thu tiền cho hóa đơn #{maHD}?", "Xác nhận duyệt Online", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string kq = hdBUS.DuyetHoaDonOnline(maHD, Session.IdTaiKhoan);
                    if (kq == "")
                    {
                        MessageBox.Show("Duyệt thành công! Gói tập đã được kích hoạt cho khách.", "Thành công");
                        LoadLichSu();
                    }
                    else MessageBox.Show(kq, "Lỗi");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một dòng 'Chờ thanh toán' trên bảng để duyệt!", "Nhắc nhở");
            }
        }

        private void dgvLichSu_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvLichSu.Columns[e.ColumnIndex].Name == "Trạng_Thái" && e.Value != null)
            {
                string tt = e.Value.ToString();
                if (tt == "Chờ thanh toán")
                {
                    e.CellStyle.ForeColor = Color.Red;
                    e.CellStyle.Font = new Font(dgvLichSu.Font, FontStyle.Bold);
                }
                else
                {
                    e.CellStyle.ForeColor = Color.Green;
                }
            }
        }

        private void tcThuNgan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tcThuNgan.SelectedIndex == 0) 
            {
                LoadLichSu();
                LoadDichVu();
            }
            else if (tcThuNgan.SelectedIndex == 1) 
            {
                LoadPhanHoi();
            }
        }

        private void LoadPhanHoi()
        {
             dgvPhanHoi.DataSource = hvBUS.LayTatCaPhanHoi();

            if (dgvPhanHoi.Columns.Count > 0)
            {
                dgvPhanHoi.Columns["Ngày_Gửi"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                dgvPhanHoi.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private void btnDaDoc_Click(object sender, EventArgs e)
        {
            if (dgvPhanHoi.CurrentRow != null)
            {
                int id = Convert.ToInt32(dgvPhanHoi.CurrentRow.Cells["Mã_PH"].Value);
                string trangThai = dgvPhanHoi.CurrentRow.Cells["Trạng_Thái"].Value.ToString();

                if (trangThai == "Đã xử lý")
                {
                    MessageBox.Show("Tin nhắn này đã được xử lý rồi!", "Thông báo");
                    return;
                }

                if (hvBUS.DaXuLyPhanHoi(id))
                {
                    MessageBox.Show("Đã đánh dấu xử lý xong!", "Thành công");
                    LoadPhanHoi();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn 1 tin nhắn để xử lý.");
            }
        }

        private void dgvPhanHoi_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvPhanHoi.Columns[e.ColumnIndex].Name == "Trạng_Thái" && e.Value != null)
            {
                if (e.Value.ToString() == "Chưa đọc")
                {
                    e.CellStyle.ForeColor = Color.Red;
                    e.CellStyle.Font = new Font(dgvPhanHoi.Font, FontStyle.Bold);
                }
                else e.CellStyle.ForeColor = Color.Green;
            }
        }

        private void radSanPham_CheckedChanged(object sender, EventArgs e)
        {
            if (radSanPham.Checked)
            {
                LoadDichVu();
                btnDuyetOnline.Enabled = false;
            }
        }
    }
}
