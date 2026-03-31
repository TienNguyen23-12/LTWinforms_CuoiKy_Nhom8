using System;
using System.Collections.Generic;
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
        private static readonly Color InputLineColor = Color.FromArgb(210, 214, 224);
        private static readonly Color InputLineFocusColor = Color.FromArgb(210, 214, 224);

        private sealed class UnderlineTextBoxState
        {
            public Panel LinePanel;
            public EventHandler LayoutHandler;
            public EventHandler EnterHandler;
            public EventHandler LeaveHandler;
            public EventHandler VisibleHandler;
            public EventHandler ParentChangedHandler;
            public EventHandler DisposedHandler;
        }

        private static readonly Dictionary<TextBox, UnderlineTextBoxState> underlineTextBoxStates = new Dictionary<TextBox, UnderlineTextBoxState>();

        private static readonly Dictionary<Control, EventHandler> roundedHandlers = new Dictionary<Control, EventHandler>();
        private static readonly Dictionary<Button, PaintEventHandler> smoothButtonPaintHandlers = new Dictionary<Button, PaintEventHandler>();
        private static readonly Dictionary<Button, EventHandler> smoothButtonInvalidateHandlers = new Dictionary<Button, EventHandler>();
        private static readonly Dictionary<Button, MouseEventHandler> smoothButtonMouseInvalidateHandlers = new Dictionary<Button, MouseEventHandler>();

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

            TextBox tb = control as TextBox;
            if (tb != null)
            {
                ApplyUnderlineTextBox(tb);
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
            if (button == null)
            {
                return;
            }

            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.FlatAppearance.BorderColor = backColor;
            button.FlatAppearance.MouseOverBackColor = backColor;
            button.FlatAppearance.MouseDownBackColor = backColor;
            button.BackColor = backColor;
            button.ForeColor = foreColor;
            button.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            button.Cursor = Cursors.Hand;
            button.Height = 34;
            button.UseVisualStyleBackColor = false;
            button.TabStop = false;

            EnableSmoothRoundedButton(button, 10);
        }

        public static void ApplyRoundedButtons(Control root, int radius = 10)
        {
            if (root == null)
            {
                return;
            }

            Button button = root as Button;
            if (button != null)
            {
                EnableSmoothRoundedButton(button, radius);
            }

            foreach (Control child in root.Controls)
            {
                ApplyRoundedButtons(child, radius);
            }
        }

        private static void EnableSmoothRoundedButton(Button button, int radius)
        {
            if (button == null)
            {
                return;
            }

            PaintEventHandler oldPaintHandler;
            if (smoothButtonPaintHandlers.TryGetValue(button, out oldPaintHandler))
            {
                button.Paint -= oldPaintHandler;
            }

            EventHandler oldInvalidateHandler;
            if (smoothButtonInvalidateHandlers.TryGetValue(button, out oldInvalidateHandler))
            {
                button.MouseEnter -= oldInvalidateHandler;
                button.MouseLeave -= oldInvalidateHandler;
                button.EnabledChanged -= oldInvalidateHandler;
                button.TextChanged -= oldInvalidateHandler;
                button.ForeColorChanged -= oldInvalidateHandler;
                button.BackColorChanged -= oldInvalidateHandler;
                button.SizeChanged -= oldInvalidateHandler;
                button.Disposed -= SmoothRoundedButton_Disposed;
            }

            MouseEventHandler oldMouseInvalidateHandler;
            if (smoothButtonMouseInvalidateHandlers.TryGetValue(button, out oldMouseInvalidateHandler))
            {
                button.MouseDown -= oldMouseInvalidateHandler;
                button.MouseUp -= oldMouseInvalidateHandler;
            }

            PaintEventHandler paintHandler = delegate (object sender, PaintEventArgs e)
            {
                Button btn = sender as Button;
                if (btn == null || btn.Width <= 1 || btn.Height <= 1)
                {
                    return;
                }

                Point mousePoint = btn.PointToClient(Control.MousePosition);
                bool isHover = btn.ClientRectangle.Contains(mousePoint);
                bool isPressed = isHover && (Control.MouseButtons & MouseButtons.Left) == MouseButtons.Left;

                Color fillColor = btn.BackColor;
                if (!btn.Enabled)
                {
                    fillColor = ControlPaint.Light(btn.BackColor, 0.35f);
                }
                else if (isPressed)
                {
                    fillColor = ControlPaint.Dark(btn.BackColor, 0.10f);
                }
                else if (isHover)
                {
                    fillColor = ControlPaint.Light(btn.BackColor, 0.08f);
                }

                Color parentColor = btn.Parent != null ? btn.Parent.BackColor : SystemColors.Control;

                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
                e.Graphics.Clear(parentColor);

                RectangleF rect = new RectangleF(0.5f, 0.5f, btn.Width - 1f, btn.Height - 1f);
                int realRadius = Math.Min(radius, btn.Height / 2);

                using (GraphicsPath path = CreateRoundedPathF(rect, realRadius))
                using (SolidBrush fillBrush = new SolidBrush(fillColor))
                {
                    e.Graphics.FillPath(fillBrush, path);
                }

                Rectangle contentRect = new Rectangle(
                    btn.Padding.Left,
                    0,
                    Math.Max(0, btn.Width - btn.Padding.Left - btn.Padding.Right),
                    btn.Height);

                int textLeft = contentRect.Left;

                if (btn.Image != null)
                {
                    int imgY = (btn.Height - btn.Image.Height) / 2;
                    int imgX;

                    if (btn.ImageAlign == ContentAlignment.MiddleLeft ||
                        btn.ImageAlign == ContentAlignment.TopLeft ||
                        btn.ImageAlign == ContentAlignment.BottomLeft)
                    {
                        imgX = contentRect.Left;
                    }
                    else if (btn.ImageAlign == ContentAlignment.MiddleRight ||
                             btn.ImageAlign == ContentAlignment.TopRight ||
                             btn.ImageAlign == ContentAlignment.BottomRight)
                    {
                        imgX = contentRect.Right - btn.Image.Width;
                    }
                    else
                    {
                        imgX = contentRect.Left + (contentRect.Width - btn.Image.Width) / 2;
                    }

                    e.Graphics.DrawImage(btn.Image, imgX, imgY, btn.Image.Width, btn.Image.Height);

                    if (btn.TextImageRelation == TextImageRelation.ImageBeforeText ||
                        btn.TextImageRelation == TextImageRelation.Overlay)
                    {
                        textLeft = imgX + btn.Image.Width + 4;
                    }
                }

                TextFormatFlags flags = TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine | TextFormatFlags.EndEllipsis;

                if (btn.TextAlign == ContentAlignment.MiddleLeft ||
                    btn.TextAlign == ContentAlignment.TopLeft ||
                    btn.TextAlign == ContentAlignment.BottomLeft)
                {
                    flags |= TextFormatFlags.Left;
                }
                else if (btn.TextAlign == ContentAlignment.MiddleRight ||
                         btn.TextAlign == ContentAlignment.TopRight ||
                         btn.TextAlign == ContentAlignment.BottomRight)
                {
                    flags |= TextFormatFlags.Right;
                }
                else
                {
                    flags |= TextFormatFlags.HorizontalCenter;
                }

                Rectangle textRect = new Rectangle(
                    textLeft,
                    0,
                    Math.Max(0, btn.Width - textLeft - btn.Padding.Right),
                    btn.Height);

                TextRenderer.DrawText(e.Graphics, btn.Text, btn.Font, textRect, btn.ForeColor, flags);
            };

            EventHandler invalidateHandler = delegate { button.Invalidate(); };
            MouseEventHandler mouseInvalidateHandler = delegate { button.Invalidate(); };

            smoothButtonPaintHandlers[button] = paintHandler;
            smoothButtonInvalidateHandlers[button] = invalidateHandler;
            smoothButtonMouseInvalidateHandlers[button] = mouseInvalidateHandler;

            button.Paint += paintHandler;
            button.MouseEnter += invalidateHandler;
            button.MouseLeave += invalidateHandler;
            button.MouseDown += mouseInvalidateHandler;
            button.MouseUp += mouseInvalidateHandler;
            button.EnabledChanged += invalidateHandler;
            button.TextChanged += invalidateHandler;
            button.ForeColorChanged += invalidateHandler;
            button.BackColorChanged += invalidateHandler;
            button.SizeChanged += invalidateHandler;
            button.Disposed += SmoothRoundedButton_Disposed;

            button.Invalidate();
        }

        private static void SmoothRoundedButton_Disposed(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button == null)
            {
                return;
            }

            PaintEventHandler paintHandler;
            if (smoothButtonPaintHandlers.TryGetValue(button, out paintHandler))
            {
                button.Paint -= paintHandler;
                smoothButtonPaintHandlers.Remove(button);
            }

            EventHandler invalidateHandler;
            if (smoothButtonInvalidateHandlers.TryGetValue(button, out invalidateHandler))
            {
                button.MouseEnter -= invalidateHandler;
                button.MouseLeave -= invalidateHandler;
                button.EnabledChanged -= invalidateHandler;
                button.TextChanged -= invalidateHandler;
                button.ForeColorChanged -= invalidateHandler;
                button.BackColorChanged -= invalidateHandler;
                button.SizeChanged -= invalidateHandler;
                button.Disposed -= SmoothRoundedButton_Disposed;
                smoothButtonInvalidateHandlers.Remove(button);
            }

            MouseEventHandler mouseInvalidateHandler;
            if (smoothButtonMouseInvalidateHandlers.TryGetValue(button, out mouseInvalidateHandler))
            {
                button.MouseDown -= mouseInvalidateHandler;
                button.MouseUp -= mouseInvalidateHandler;
                smoothButtonMouseInvalidateHandlers.Remove(button);
            }
        }

        private static GraphicsPath CreateRoundedPathF(RectangleF rect, float radius)
        {
            GraphicsPath path = new GraphicsPath();

            float maxRadius = Math.Min(rect.Width, rect.Height) / 2f;
            float safeRadius = Math.Max(1f, Math.Min(radius, maxRadius));
            float diameter = safeRadius * 2f;

            path.StartFigure();
            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();

            return path;
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

        public static void EnableAutoRounded(Control control, int radius)
        {
            if (control == null)
            {
                return;
            }

            EventHandler oldHandler;
            if (roundedHandlers.TryGetValue(control, out oldHandler))
            {
                control.SizeChanged -= oldHandler;
                control.HandleCreated -= oldHandler;
                control.Disposed -= RoundedControl_Disposed;
                roundedHandlers.Remove(control);
            }

            EventHandler handler = delegate { RoundControl(control, radius); };
            roundedHandlers[control] = handler;

            control.SizeChanged += handler;
            control.HandleCreated += handler;
            control.Disposed += RoundedControl_Disposed;

            RoundControl(control, radius);
        }

        private static void RoundedControl_Disposed(object sender, EventArgs e)
        {
            Control control = sender as Control;
            if (control == null)
            {
                return;
            }

            EventHandler handler;
            if (roundedHandlers.TryGetValue(control, out handler))
            {
                control.SizeChanged -= handler;
                control.HandleCreated -= handler;
                control.Disposed -= RoundedControl_Disposed;
                roundedHandlers.Remove(control);
            }
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

        public static void ApplyUnderlineTextInputs(Control root)
        {
            if (root == null)
            {
                return;
            }

            TextBox tb = root as TextBox;
            if (tb != null)
            {
                ApplyUnderlineTextBox(tb);
            }

            foreach (Control child in root.Controls)
            {
                ApplyUnderlineTextInputs(child);
            }
        }

        private static void ApplyUnderlineTextBox(TextBox textBox)
        {
            if (textBox == null)
            {
                return;
            }

            if (textBox.Multiline)
            {
                textBox.BorderStyle = BorderStyle.FixedSingle;
                return;
            }

            UnderlineTextBoxState state;
            if (!underlineTextBoxStates.TryGetValue(textBox, out state))
            {
                state = new UnderlineTextBoxState();

                state.LinePanel = new Panel();
                state.LinePanel.Height = 1;
                state.LinePanel.BackColor = InputLineColor;
                state.LinePanel.Enabled = false;
                state.LinePanel.TabStop = false;

                state.LayoutHandler = delegate { UpdateUnderlineTextBox(textBox); };
                state.EnterHandler = delegate { UpdateUnderlineTextBox(textBox); };
                state.LeaveHandler = delegate { UpdateUnderlineTextBox(textBox); };
                state.VisibleHandler = delegate { UpdateUnderlineTextBox(textBox); };
                state.ParentChangedHandler = delegate { EnsureUnderlineParent(textBox); UpdateUnderlineTextBox(textBox); };
                state.DisposedHandler = delegate { RemoveUnderlineTextBox(textBox); };

                underlineTextBoxStates[textBox] = state;

                textBox.LocationChanged += state.LayoutHandler;
                textBox.SizeChanged += state.LayoutHandler;
                textBox.Enter += state.EnterHandler;
                textBox.Leave += state.LeaveHandler;
                textBox.VisibleChanged += state.VisibleHandler;
                textBox.ParentChanged += state.ParentChangedHandler;
                textBox.Disposed += state.DisposedHandler;
            }

            textBox.BorderStyle = BorderStyle.None;
            EnsureUnderlineParent(textBox);
            UpdateUnderlineTextBox(textBox);
        }

        private static void EnsureUnderlineParent(TextBox textBox)
        {
            UnderlineTextBoxState state;
            if (!underlineTextBoxStates.TryGetValue(textBox, out state))
            {
                return;
            }

            if (textBox.Parent == null)
            {
                return;
            }

            if (state.LinePanel.Parent != textBox.Parent)
            {
                if (state.LinePanel.Parent != null)
                {
                    state.LinePanel.Parent.Controls.Remove(state.LinePanel);
                }

                textBox.Parent.Controls.Add(state.LinePanel);
            }
        }

        private static void UpdateUnderlineTextBox(TextBox textBox)
        {
            UnderlineTextBoxState state;
            if (!underlineTextBoxStates.TryGetValue(textBox, out state))
            {
                return;
            }

            if (textBox.Parent == null || state.LinePanel.Parent == null)
            {
                return;
            }

            Color inputBack = textBox.Parent.BackColor;
            textBox.BackColor = inputBack;

            int width = textBox.Width;
            if (width < 1)
            {
                width = 1;
            }

            state.LinePanel.SetBounds(textBox.Left, textBox.Bottom + 1, width, 1);
            state.LinePanel.Visible = textBox.Visible;
            state.LinePanel.BackColor = InputLineColor;
            state.LinePanel.BringToFront();
        }

        private static void RemoveUnderlineTextBox(TextBox textBox)
        {
            UnderlineTextBoxState state;
            if (!underlineTextBoxStates.TryGetValue(textBox, out state))
            {
                return;
            }

            textBox.LocationChanged -= state.LayoutHandler;
            textBox.SizeChanged -= state.LayoutHandler;
            textBox.Enter -= state.EnterHandler;
            textBox.Leave -= state.LeaveHandler;
            textBox.VisibleChanged -= state.VisibleHandler;
            textBox.ParentChanged -= state.ParentChangedHandler;
            textBox.Disposed -= state.DisposedHandler;

            if (state.LinePanel != null)
            {
                if (state.LinePanel.Parent != null)
                {
                    state.LinePanel.Parent.Controls.Remove(state.LinePanel);
                }

                state.LinePanel.Dispose();
            }

            underlineTextBoxStates.Remove(textBox);
        }
    }
}