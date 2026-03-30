using LTWinforms_CuoiKy_Nhom8.BUS;
using LTWinforms_CuoiKy_Nhom8.DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace LTWinforms_CuoiKy_Nhom8.GUI
{
    public partial class ucTinNhan : UserControl
    {
        private readonly TinNhanBUS chatBUS = new TinNhanBUS();
        private int idKhachHangDangChat = 0;
        private int idTinNhanDangSua = 0;
        private bool isThemeApplied;
        private bool isLayoutHooked;

        public ucTinNhan()
        {
            InitializeComponent();
            lbxNoiDungChat.ContextMenuStrip = contextMenuStrip1;
        }

        private void ApplyTheme()
        {
            if (isThemeApplied)
            {
                return;
            }

            BackColor = Color.White;

            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label1.ForeColor = Color.FromArgb(34, 49, 63);

            txtSoanTin.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            txtSoanTin.ForeColor = Color.FromArgb(44, 62, 80);
            txtSoanTin.BorderStyle = BorderStyle.FixedSingle;

            StylePrimaryButton(btnGui);
            StyleSecondaryButton(btnLamMoi);

            StyleContactGrid(dgvDanhSachLienHe);
            StyleChatList(lbxNoiDungChat);

            isThemeApplied = true;
        }

        private void StylePrimaryButton(Button button)
        {
            StyleButton(button, Color.FromArgb(46, 134, 222), Color.White);
        }

        private void StyleSecondaryButton(Button button)
        {
            StyleButton(button, Color.FromArgb(52, 73, 94), Color.White);
        }

        private void StyleButton(Button button, Color backColor, Color foreColor)
        {
            ModernTheme.StyleButton(button, backColor, foreColor);
            button.Height = 34;
        }

        private void StyleContactGrid(DataGridView grid)
        {
            grid.BorderStyle = BorderStyle.None;
            grid.BackgroundColor = Color.White;
            grid.EnableHeadersVisualStyles = false;
            grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(46, 134, 222);
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            grid.ColumnHeadersHeight = 36;

            grid.RowTemplate.Height = 28;
            grid.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 249, 255);
            grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(186, 222, 250);
            grid.DefaultCellStyle.SelectionForeColor = Color.FromArgb(25, 42, 58);

            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.MultiSelect = false;
            grid.ReadOnly = true;
            grid.AllowUserToAddRows = false;
            grid.AllowUserToResizeRows = false;
            grid.RowHeadersVisible = false;
        }

        private void StyleChatList(ListBox listBox)
        {
            listBox.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            listBox.ForeColor = Color.FromArgb(44, 62, 80);
            listBox.BackColor = Color.White;
            listBox.BorderStyle = BorderStyle.FixedSingle;
            listBox.HorizontalScrollbar = true;
        }

        private void ApplyResponsiveLayout()
        {
            int contentWidth = Math.Max(760, ClientSize.Width - 24);
            int left = Math.Max(12, (ClientSize.Width - contentWidth) / 2);

            int top = 12;
            int contactWidth = 260;
            int gap = 14;
            int rightLeft = left + contactWidth + gap;
            int rightWidth = contentWidth - contactWidth - gap;

            label1.SetBounds(left, top, contentWidth, 28);

            int areaTop = label1.Bottom + 8;
            int areaHeight = ClientSize.Height - areaTop - 12;

            dgvDanhSachLienHe.SetBounds(left, areaTop, contactWidth, areaHeight);

            int chatListHeight = Math.Max(180, areaHeight - 86);
            lbxNoiDungChat.SetBounds(rightLeft, areaTop, rightWidth, chatListHeight);

            txtSoanTin.SetBounds(rightLeft, lbxNoiDungChat.Bottom + 10, rightWidth - 96, 30);
            btnGui.SetBounds(txtSoanTin.Right + 8, txtSoanTin.Top - 2, 88, 34);

            btnLamMoi.SetBounds(rightLeft, txtSoanTin.Bottom + 8, 100, 34);
        }

        private void LoadDanhSachLienHe()
        {
            dgvDanhSachLienHe.Visible = true;
            dgvDanhSachLienHe.DataSource = chatBUS.LayDanhSachLienHe(Session.Role);

            if (dgvDanhSachLienHe.Columns.Count > 0)
            {
                dgvDanhSachLienHe.Columns["Id"].Visible = false;
                dgvDanhSachLienHe.Columns["TenHienThi"].HeaderText = "Danh sách liên hệ";
                dgvDanhSachLienHe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }

            if (dgvDanhSachLienHe.Rows.Count > 0)
            {
                dgvDanhSachLienHe.Rows[0].Selected = true;
                idKhachHangDangChat = Convert.ToInt32(dgvDanhSachLienHe.Rows[0].Cells["Id"].Value);
                LoadTinNhan();
            }
            else
            {
                idKhachHangDangChat = 0;
                lbxNoiDungChat.Items.Clear();
            }
        }

        private void LoadTinNhan()
        {
            if (idKhachHangDangChat == 0)
            {
                return;
            }

            lbxNoiDungChat.Items.Clear();
            List<dynamic> listTinNhan = (List<dynamic>)chatBUS.LayLichSuChat(Session.IdTaiKhoan, idKhachHangDangChat);

            foreach (dynamic tn in listTinNhan)
            {
                string icon = tn.LaToi ? "👤" : (tn.TenHienThi == "Trung tâm" ? "🏢" : "💬");
                string thoiGian = Convert.ToDateTime(tn.ThoiGian).ToString("HH:mm dd/MM");

                TinNhanItem item = new TinNhanItem()
                {
                    Id = tn.Id,
                    LaToi = tn.LaToi,
                    NoiDungGoc = tn.NoiDung,
                    TextHienThi = "[" + thoiGian + "] " + icon + " " + tn.TenHienThi + ": " + tn.NoiDung
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
            ApplyTheme();
            LoadDanhSachLienHe();

            if (!isLayoutHooked)
            {
                Resize += ucTinNhan_Resize;
                isLayoutHooked = true;
            }

            ApplyResponsiveLayout();
        }

        private void ucTinNhan_Resize(object sender, EventArgs e)
        {
            ApplyResponsiveLayout();
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
            idTinNhanDangSua = 0;
            btnGui.Text = "Gửi";
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
