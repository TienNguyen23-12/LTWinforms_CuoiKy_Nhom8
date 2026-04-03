using LTWinforms_CuoiKy_Nhom8.BUS;
using LTWinforms_CuoiKy_Nhom8.DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace LTWinforms_CuoiKy_Nhom8.GUI
{
    public partial class ucTinNhan : UserControl
    {
        private readonly TinNhanBUS chatBUS = new TinNhanBUS();
        private int idKhachHangDangChat = 0;
        private int idTinNhanDangSua = 0;
        private bool isThemeApplied;
        private Panel pnlSoanTinLine;
        private bool isLayoutHooked;

        public ucTinNhan()
        {
            InitializeComponent();
            lbxNoiDungChat.ContextMenuStrip = contextMenuStrip1;

            lbxNoiDungChat.DrawMode = DrawMode.OwnerDrawVariable;
            lbxNoiDungChat.MeasureItem += lbxNoiDungChat_MeasureItem;
            lbxNoiDungChat.DrawItem += lbxNoiDungChat_DrawItem;
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

            EnsureComposerUnderline();

            txtSoanTin.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            txtSoanTin.ForeColor = Color.FromArgb(44, 62, 80);
            txtSoanTin.BorderStyle = BorderStyle.None;
            txtSoanTin.BackColor = Color.White;

            StylePrimaryButton(btnGui);
            StyleSecondaryButton(btnLamMoi);

            StyleContactGrid(dgvDanhSachLienHe);
            StyleChatList(lbxNoiDungChat);

            isThemeApplied = true;
        }

        private void EnsureComposerUnderline()
        {
            if (pnlSoanTinLine != null)
            {
                return;
            }

            pnlSoanTinLine = new Panel();
            pnlSoanTinLine.Height = 1;
            pnlSoanTinLine.BackColor = Color.FromArgb(200, 208, 220);
            Controls.Add(pnlSoanTinLine);
            pnlSoanTinLine.BringToFront();
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
            listBox.BackColor = Color.FromArgb(248, 251, 255);
            listBox.BorderStyle = BorderStyle.None;
            listBox.HorizontalScrollbar = false;
            listBox.IntegralHeight = false;
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

            int chatListHeight = Math.Max(180, areaHeight - 90);
            lbxNoiDungChat.SetBounds(rightLeft, areaTop, rightWidth, chatListHeight);

            int composerTop = lbxNoiDungChat.Bottom + 10;
            int sendButtonWidth = 92;
            int inputWidth = rightWidth - sendButtonWidth - 10;

            txtSoanTin.SetBounds(rightLeft, composerTop + 4, inputWidth, 22);
            pnlSoanTinLine.SetBounds(rightLeft, txtSoanTin.Bottom + 3, inputWidth, 1);

            btnGui.SetBounds(txtSoanTin.Right + 10, composerTop, sendButtonWidth, 34);
            btnLamMoi.SetBounds(rightLeft, pnlSoanTinLine.Bottom + 8, 100, 34);
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
                DateTime thoiDiem = Convert.ToDateTime(tn.ThoiGian);

                TinNhanItem item = new TinNhanItem()
                {
                    Id = tn.Id,
                    LaToi = tn.LaToi,
                    NoiDungGoc = tn.NoiDung,
                    TenHienThi = tn.TenHienThi,
                    ThoiGian = thoiDiem,
                    TextHienThi = tn.NoiDung
                };

                lbxNoiDungChat.Items.Add(item);
            }

            if (lbxNoiDungChat.Items.Count > 0)
            {
                lbxNoiDungChat.TopIndex = lbxNoiDungChat.Items.Count - 1;
            }

            lbxNoiDungChat.Invalidate();
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
                ModernMessageBox.Show("Vui lòng chọn 1 người bên trái để nhắn tin!", "Cảnh báo", ModernMessageType.Warning);
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
                    ModernMessageBox.Show("Chỉ được sửa tin nhắn của chính mình!", "Cảnh báo", ModernMessageType.Warning);
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
                    ModernMessageBox.Show("Chỉ được xóa tin nhắn của chính mình!", "Cảnh báo", ModernMessageType.Warning);
                    return;
                }

                if (ModernMessageBox.Show("Thu hồi tin nhắn này?", "Xác nhận", MessageBoxButtons.YesNo, ModernMessageType.Question) == DialogResult.Yes)
                {
                    chatBUS.XoaTinNhan(selectedMsg.Id);
                    LoadTinNhan();
                }
            }
        }

        private void lbxNoiDungChat_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            if (e.Index < 0 || e.Index >= lbxNoiDungChat.Items.Count)
            {
                e.ItemHeight = 32;
                return;
            }

            TinNhanItem item = lbxNoiDungChat.Items[e.Index] as TinNhanItem;
            if (item == null)
            {
                e.ItemHeight = 32;
                return;
            }

            int maxBubbleWidth = Math.Max(200, (int)(lbxNoiDungChat.ClientSize.Width * 0.68f));
            int bubblePaddingH = 12;
            int bubblePaddingV = 10;

            Size messageSize = TextRenderer.MeasureText(
                item.NoiDungGoc ?? "",
                lbxNoiDungChat.Font,
                new Size(maxBubbleWidth - (bubblePaddingH * 2), 0),
                TextFormatFlags.WordBreak | TextFormatFlags.Left | TextFormatFlags.TextBoxControl);

            int senderHeight = item.LaToi ? 0 : 16;
            int bubbleHeight = messageSize.Height + (bubblePaddingV * 2);
            int timeHeight = 14;

            e.ItemHeight = senderHeight + bubbleHeight + timeHeight + 14;
        }

        private void lbxNoiDungChat_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0 || e.Index >= lbxNoiDungChat.Items.Count)
            {
                return;
            }

            TinNhanItem item = lbxNoiDungChat.Items[e.Index] as TinNhanItem;
            if (item == null)
            {
                return;
            }

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rowRect = e.Bounds;
            rowRect.Inflate(-2, -1);

            using (SolidBrush rowBrush = new SolidBrush(lbxNoiDungChat.BackColor))
            {
                g.FillRectangle(rowBrush, e.Bounds); // bỏ nền xanh khi selected
            }

            int sidePadding = 14;
            int bubblePaddingH = 12;
            int bubblePaddingV = 10;
            int maxBubbleWidth = Math.Max(200, (int)(lbxNoiDungChat.ClientSize.Width * 0.68f));

            string message = item.NoiDungGoc ?? "";
            string senderName = item.LaToi ? "Tôi" : (string.IsNullOrEmpty(item.TenHienThi) ? "Người dùng" : item.TenHienThi);
            string timeText = item.ThoiGian.ToString("HH:mm dd/MM");

            using (Font senderFont = new Font("Segoe UI", 8.5F, FontStyle.Bold))
            using (Font timeFont = new Font("Segoe UI", 7.8F, FontStyle.Regular))
            {
                Size messageSize = TextRenderer.MeasureText(
                    message,
                    lbxNoiDungChat.Font,
                    new Size(maxBubbleWidth - (bubblePaddingH * 2), 0),
                    TextFormatFlags.WordBreak | TextFormatFlags.Left | TextFormatFlags.TextBoxControl);

                int bubbleWidth = Math.Min(maxBubbleWidth, messageSize.Width + (bubblePaddingH * 2));
                int bubbleHeight = messageSize.Height + (bubblePaddingV * 2);
                int senderHeight = item.LaToi ? 0 : 16;

                int bubbleX = item.LaToi
                    ? rowRect.Right - sidePadding - bubbleWidth
                    : rowRect.Left + sidePadding;

                if (!item.LaToi)
                {
                    Rectangle senderRect = new Rectangle(bubbleX + 2, rowRect.Top + 2, maxBubbleWidth, senderHeight);
                    TextRenderer.DrawText(
                        g,
                        senderName,
                        senderFont,
                        senderRect,
                        Color.FromArgb(88, 102, 118),
                        TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);
                }

                int bubbleY = rowRect.Top + senderHeight + 2;
                Rectangle bubbleRect = new Rectangle(bubbleX, bubbleY, bubbleWidth, bubbleHeight);

                Color bubbleColor = item.LaToi ? Color.FromArgb(46, 134, 222) : Color.FromArgb(255, 255, 255);
                Color textColor = item.LaToi ? Color.White : Color.FromArgb(43, 54, 68);
                Color borderColor = item.LaToi ? Color.FromArgb(46, 134, 222) : Color.FromArgb(220, 228, 238);

                Rectangle shadowRect = bubbleRect;
                shadowRect.Offset(0, 1);

                using (GraphicsPath shadowPath = CreateRoundedRectPath(shadowRect, 14))
                using (SolidBrush shadowBrush = new SolidBrush(Color.FromArgb(28, 0, 0, 0)))
                {
                    g.FillPath(shadowBrush, shadowPath);
                }

                using (GraphicsPath bubblePath = CreateRoundedRectPath(bubbleRect, 14))
                using (SolidBrush bubbleBrush = new SolidBrush(bubbleColor))
                using (Pen borderPen = new Pen(borderColor))
                {
                    g.FillPath(bubbleBrush, bubblePath);
                    g.DrawPath(borderPen, bubblePath);
                }

                // Chỉ vẽ mũi tên cho tin nhắn của tôi (bên phải)
                if (item.LaToi)
                {
                    using (GraphicsPath tailPath = CreateBubbleTailPath(bubbleRect, true))
                    using (SolidBrush tailBrush = new SolidBrush(bubbleColor))
                    using (Pen tailPen = new Pen(borderColor))
                    {
                        g.FillPath(tailBrush, tailPath);
                        g.DrawPath(tailPen, tailPath);
                    }
                }

                Rectangle messageRect = new Rectangle(
                    bubbleRect.Left + bubblePaddingH,
                    bubbleRect.Top + bubblePaddingV,
                    bubbleRect.Width - (bubblePaddingH * 2),
                    bubbleRect.Height - (bubblePaddingV * 2));

                TextRenderer.DrawText(
                    g,
                    message,
                    lbxNoiDungChat.Font,
                    messageRect,
                    textColor,
                    TextFormatFlags.WordBreak | TextFormatFlags.Left | TextFormatFlags.TextBoxControl);

                Size timeSize = TextRenderer.MeasureText(timeText, timeFont);
                int timeX = item.LaToi ? bubbleRect.Right - timeSize.Width : bubbleRect.Left;
                Rectangle timeRect = new Rectangle(timeX, bubbleRect.Bottom + 4, timeSize.Width + 2, 14);

                TextRenderer.DrawText(
                    g,
                    timeText,
                    timeFont,
                    timeRect,
                    Color.FromArgb(130, 140, 150),
                    TextFormatFlags.Left | TextFormatFlags.VerticalCenter);
            }
        }

        private GraphicsPath CreateRoundedRectPath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int d = radius * 2;

            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();

            return path;
        }

        private GraphicsPath CreateBubbleTailPath(Rectangle bubbleRect, bool laToi)
        {
            GraphicsPath path = new GraphicsPath();

            if (laToi)
            {
                Point p1 = new Point(bubbleRect.Right - 12, bubbleRect.Bottom - 8);
                Point p2 = new Point(bubbleRect.Right + 5, bubbleRect.Bottom - 5);
                Point p3 = new Point(bubbleRect.Right - 4, bubbleRect.Bottom - 16);
                path.AddPolygon(new[] { p1, p2, p3 });
            }
            else
            {
                Point p1 = new Point(bubbleRect.Left + 12, bubbleRect.Bottom - 8);
                Point p2 = new Point(bubbleRect.Left - 5, bubbleRect.Bottom - 5);
                Point p3 = new Point(bubbleRect.Left + 4, bubbleRect.Bottom - 16);
                path.AddPolygon(new[] { p1, p2, p3 });
            }

            path.CloseFigure();
            return path;
        }
    }

    public class TinNhanItem
    {
        public int Id { get; set; }
        public bool LaToi { get; set; }
        public string NoiDungGoc { get; set; }
        public string TextHienThi { get; set; }
        public string TenHienThi { get; set; }
        public DateTime ThoiGian { get; set; }

        public override string ToString()
        {
            return TextHienThi;
        }
    }
}
