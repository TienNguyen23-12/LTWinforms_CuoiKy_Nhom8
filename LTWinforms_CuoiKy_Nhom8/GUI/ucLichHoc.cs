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
    public partial class ucLichHoc : UserControl
    {
        LichHocBUS lichBUS = new LichHocBUS();
        public ucLichHoc()
        {
            InitializeComponent();
        }

        private void ucLichHoc_Load(object sender, EventArgs e)
        {
            if (Session.Role == 1 || Session.Role == 2)
            {
                lblTieuDe.Text = "LỊCH CỦA TOÀN BỘ TRUNG TÂM";
            }
            else if (Session.Role == 4)
            {
                lblTieuDe.Text = "LỊCH DẠY CỦA BẠN";
            }
            else if (Session.Role == 3)
            {
                lblTieuDe.Text = "THỜI KHÓA BIỂU CỦA TÔI";
            }

            dgvLich.DataSource = lichBUS.LayLichTheoQuyen(Session.Role, Session.IdTaiKhoan);

            if (dgvLich.Columns.Count > 0)
            {
                dgvLich.Columns["MaLop"].HeaderText = "Mã Lớp";
                dgvLich.Columns["TenLop"].HeaderText = "Tên Lớp Học";
                dgvLich.Columns["TenHLV"].HeaderText = "Giáo Viên / HLV";
                dgvLich.Columns["ThoiGian"].HeaderText = "Lịch Học (Thời Gian)";
                dgvLich.Columns["PhongTap"].HeaderText = "Phòng Tập";

                dgvLich.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }
    }
}
