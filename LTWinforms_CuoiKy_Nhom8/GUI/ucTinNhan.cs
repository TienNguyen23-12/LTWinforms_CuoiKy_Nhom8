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
    public partial class ucTinNhan : UserControl
    {
        TinNhanBUS chatBUS = new TinNhanBUS();
        int idKhachHangDangChat = 0;
        int idTinNhanDangSua = 0;

        public ucTinNhan()
        {
            InitializeComponent();
            lbxNoiDungChat.ContextMenuStrip = contextMenuStrip1;
        }

        private void LoadTinNhan()
        {
            if (idKhachHangDangChat == 0)
            {
                return;
            }

            lbxNoiDungChat.Items.Clear();
            var listTinNhan = (List<dynamic>)chatBUS.LayLichSuChat(Session.IdTaiKhoan, idKhachHangDangChat);

            foreach (var tn in listTinNhan)
            {
                string icon = tn.LaToi ? "👤" : (tn.TenHienThi == "Trung tâm" ? "🏢" : "💬");
                string thoiGian = Convert.ToDateTime(tn.ThoiGian).ToString("HH:mm dd/MM");

                TinNhanItem item = new TinNhanItem()
                {
                    Id = tn.Id,
                    LaToi = tn.LaToi, 
                    NoiDungGoc = tn.NoiDung,
                    TextHienThi = $"[{thoiGian}] {icon} {tn.TenHienThi}: {tn.NoiDung}"
                };

                lbxNoiDungChat.Items.Add(item);
            }

            if (lbxNoiDungChat.Items.Count > 0)
            {
                lbxNoiDungChat.TopIndex = lbxNoiDungChat.Items.Count - 1;
            }
        }

        private void ucTinNhan_Load(object sender, EventArgs e)
        {
            dgvDanhSachLienHe.Visible = true;

            dgvDanhSachLienHe.DataSource = chatBUS.LayDanhSachLienHe(Session.Role);

            if (dgvDanhSachLienHe.Columns.Count > 0)
            {
                dgvDanhSachLienHe.Columns["Id"].Visible = false;
                dgvDanhSachLienHe.Columns["TenHienThi"].HeaderText = "Danh sách liên hệ";
                dgvDanhSachLienHe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvDanhSachLienHe.RowHeadersVisible = false;
                dgvDanhSachLienHe.AllowUserToAddRows = false;
            }

            if (dgvDanhSachLienHe.Rows.Count > 0)
            {
                dgvDanhSachLienHe.Rows[0].Selected = true;
                idKhachHangDangChat = Convert.ToInt32(dgvDanhSachLienHe.Rows[0].Cells["Id"].Value);
                LoadTinNhan();
            }
        }

        private void dgvDanhSachLienHe_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                idKhachHangDangChat = Convert.ToInt32(dgvDanhSachLienHe.Rows[e.RowIndex].Cells["Id"].Value);
                LoadTinNhan();
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LoadTinNhan();
            txtSoanTin.Clear();
        }

        private void btnGui_Click(object sender, EventArgs e)
        {
            string noiDung = txtSoanTin.Text.Trim();
            if (string.IsNullOrEmpty(noiDung))
            {
                return;
            }

            if (idKhachHangDangChat == 0)
            {
                MessageBox.Show("Vui lòng chọn 1 người bên trái để nhắn tin!", "Cảnh báo");
                return;
            }

            if (idTinNhanDangSua > 0)
            {
                chatBUS.SuaTinNhan(idTinNhanDangSua, noiDung);
                idTinNhanDangSua = 0;
                btnGui.Text = "Gửi";
            }
            else
            {
                chatBUS.GuiTinNhan(Session.IdTaiKhoan, idKhachHangDangChat, noiDung);
            }

            txtSoanTin.Clear();
            LoadTinNhan();
        }

        private void txtSoanTin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; 
                btnGui_Click(sender, e);
            }
        }

        private void lbxNoiDungChat_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int index = lbxNoiDungChat.IndexFromPoint(e.Location);
                if (index != ListBox.NoMatches)
                {
                    lbxNoiDungChat.SelectedIndex = index;
                }
            }
        }

        private void menuSua_Click(object sender, EventArgs e)
        {
            TinNhanItem selectedMsg = lbxNoiDungChat.SelectedItem as TinNhanItem;
            if (selectedMsg != null)
            {
                if (!selectedMsg.LaToi) 
                {
                    MessageBox.Show("Chỉ được sửa tin nhắn của chính mình!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                idTinNhanDangSua = selectedMsg.Id;
                txtSoanTin.Text = selectedMsg.NoiDungGoc;
                btnGui.Text = "Lưu";
                txtSoanTin.Focus();
            }
        }

        private void menuXoa_Click(object sender, EventArgs e)
        {
            TinNhanItem selectedMsg = lbxNoiDungChat.SelectedItem as TinNhanItem;
            if (selectedMsg != null)
            {
                if (!selectedMsg.LaToi)
                {
                    MessageBox.Show("Chỉ được xóa tin nhắn của chính mình!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show("Thu hồi tin nhắn này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    chatBUS.XoaTinNhan(selectedMsg.Id);
                    LoadTinNhan();
                }
            }
        }
    }

    public class TinNhanItem
    {
        public int Id { get; set; }
        public bool LaToi { get; set; }
        public string NoiDungGoc { get; set; }
        public string TextHienThi { get; set; }

        public override string ToString()
        {
            return TextHienThi;
        }
    }
}
