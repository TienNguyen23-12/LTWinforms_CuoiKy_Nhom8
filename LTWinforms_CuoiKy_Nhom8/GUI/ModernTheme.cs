using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace LTWinforms_CuoiKy_Nhom8.GUI
{
    internal static class ModernTheme
    {
        public static readonly Color PageBackground = Color.FromArgb(245, 248, 252);
        public static readonly Color CardBackground = Color.White;
        public static readonly Color TextPrimary = Color.FromArgb(37, 48, 66);
        public static readonly Color TextSecondary = Color.FromArgb(96, 109, 128);
        public static readonly Color Border = Color.FromArgb(221, 229, 239);

        public static void StyleLabel(Label label, bool title = false)
        {
            label.ForeColor = title ? TextPrimary : TextSecondary;
            label.Font = title
                ? new Font("Segoe UI", 14F, FontStyle.Bold)
                : new Font("Segoe UI", 10F, FontStyle.Bold);
        }

        public static void StyleInput(Control control)
        {
            control.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            control.ForeColor = TextPrimary;
            control.BackColor = Color.White;

            TextBox tb = control as TextBox;
            if (tb != null)
            {
                tb.BorderStyle = BorderStyle.FixedSingle;
            }

            RichTextBox rtb = control as RichTextBox;
            if (rtb != null)
            {
                rtb.BorderStyle = BorderStyle.FixedSingle;
            }

            ComboBox cb = control as ComboBox;
            if (cb != null)
            {
                StyleDataComboBox(cb);
            }
        }

        public static void StyleDataComboBox(ComboBox comboBox)
        {
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList; 
            comboBox.FlatStyle = FlatStyle.Flat;
            comboBox.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            comboBox.ForeColor = TextPrimary;
            comboBox.BackColor = Color.White;

            comboBox.DrawMode = DrawMode.OwnerDrawFixed;
            comboBox.ItemHeight = 28;
            comboBox.IntegralHeight = false;
            comboBox.MaxDropDownItems = 8;
            comboBox.DropDownHeight = 230;

            comboBox.DrawItem -= ComboBox_DrawItem;
            comboBox.DrawItem += ComboBox_DrawItem;
        }

        private static void ComboBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if (comboBox == null || e.Index < 0)
            {
                return;
            }

            bool isSelected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
            Color backColor = isSelected ? Color.FromArgb(58, 129, 214) : Color.White;
            Color foreColor = isSelected ? Color.White : TextPrimary;

            using (SolidBrush backBrush = new SolidBrush(backColor))
            using (SolidBrush textBrush = new SolidBrush(foreColor))
            {
                e.Graphics.FillRectangle(backBrush, e.Bounds);
                string text = comboBox.GetItemText(comboBox.Items[e.Index]);
                Rectangle textRect = new Rectangle(e.Bounds.X + 8, e.Bounds.Y + 4, e.Bounds.Width - 12, e.Bounds.Height - 8);
                e.Graphics.DrawString(text, comboBox.Font, textBrush, textRect);
            }

            e.DrawFocusRectangle();
        }

        public static void StyleCard(Panel panel)
        {
            panel.BackColor = CardBackground;
            panel.BorderStyle = BorderStyle.FixedSingle;
            EnableAutoRounded(panel, 12);
        }

        public static void StyleButton(Button button, Color backColor, Color foreColor)
        {
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.FlatAppearance.MouseOverBackColor = ControlPaint.Light(backColor, 0.08f);
            button.FlatAppearance.MouseDownBackColor = ControlPaint.Dark(backColor, 0.08f);
            button.BackColor = backColor;
            button.ForeColor = foreColor;
            button.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            button.Cursor = Cursors.Hand;
            button.Height = 38;

            if (button.Region != null)
            {
                button.Region.Dispose();
                button.Region = null;
            }
        }

        public static void StyleGrid(DataGridView grid)
        {
            grid.BorderStyle = BorderStyle.None;
            grid.BackgroundColor = Color.White;
            grid.EnableHeadersVisualStyles = false;
            grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(58, 129, 214);
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            grid.ColumnHeadersHeight = 40;

            grid.DefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            grid.RowTemplate.Height = 32;
            grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(246, 250, 255);
            grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(212, 232, 252);
            grid.DefaultCellStyle.SelectionForeColor = TextPrimary;

            grid.GridColor = Border;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.MultiSelect = false;
            grid.ReadOnly = true;
            grid.AllowUserToAddRows = false;
            grid.AllowUserToResizeRows = false;
            grid.RowHeadersVisible = false;
        }

        private static void EnableAutoRounded(Control control, int radius)
        {
            control.SizeChanged += delegate { RoundControl(control, radius); };
            control.HandleCreated += delegate { RoundControl(control, radius); };
            RoundControl(control, radius);
        }

        private static void RoundControl(Control control, int radius)
        {
            Rectangle rect = new Rectangle(0, 0, control.Width, control.Height);
            if (rect.Width <= 0 || rect.Height <= 0)
            {
                return;
            }

            int diameter = radius * 2;
            GraphicsPath path = new GraphicsPath();
            path.StartFigure();
            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();

            if (control.Region != null)
            {
                control.Region.Dispose();
            }

            control.Region = new Region(path);
            path.Dispose();
        }
    }
}