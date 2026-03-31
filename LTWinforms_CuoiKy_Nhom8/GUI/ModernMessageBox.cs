using System;
using System.Drawing;
using System.Windows.Forms;

namespace LTWinforms_CuoiKy_Nhom8.GUI
{
    internal enum ModernMessageType
    {
        Info,
        Success,
        Warning,
        Error,
        Question
    }

    internal static class ModernMessageBox
    {
        public static DialogResult Show(string message, string title, ModernMessageType type)
        {
            return Show(message, title, MessageBoxButtons.OK, type);
        }

        public static DialogResult Show(string message, string title, MessageBoxButtons buttons, ModernMessageType type)
        {
            using (ModernMessageForm frm = new ModernMessageForm(message, title, buttons, type))
            {
                return frm.ShowDialog();
            }
        }

        private sealed class ModernMessageForm : Form
        {
            public ModernMessageForm(string message, string title, MessageBoxButtons buttons, ModernMessageType type)
            {
                Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
                BackColor = Color.White;
                StartPosition = FormStartPosition.CenterScreen;
                FormBorderStyle = FormBorderStyle.FixedDialog;
                MinimizeBox = false;
                MaximizeBox = false;
                ShowInTaskbar = false;
                Text = title;
                ClientSize = new Size(500, 220);

                Panel topBar = new Panel();
                topBar.Dock = DockStyle.Top;
                topBar.Height = 6;
                topBar.BackColor = GetAccent(type);
                Controls.Add(topBar);

                Label lblIcon = new Label();
                lblIcon.SetBounds(22, 34, 48, 48);
                lblIcon.Font = new Font("Segoe UI Symbol", 24F, FontStyle.Bold);
                lblIcon.ForeColor = GetAccent(type);
                lblIcon.TextAlign = ContentAlignment.MiddleCenter;
                lblIcon.Text = GetIcon(type);
                Controls.Add(lblIcon);

                Label lblTitle = new Label();
                lblTitle.SetBounds(84, 34, 392, 28);
                lblTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
                lblTitle.ForeColor = Color.FromArgb(37, 48, 66);
                lblTitle.Text = title;
                Controls.Add(lblTitle);

                Label lblMessage = new Label();
                lblMessage.SetBounds(84, 66, 392, 80);
                lblMessage.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
                lblMessage.ForeColor = Color.FromArgb(62, 73, 88);
                lblMessage.Text = message;
                Controls.Add(lblMessage);

                if (buttons == MessageBoxButtons.YesNo)
                {
                    Button btnYes = new Button();
                    btnYes.Text = "Yes";
                    btnYes.DialogResult = DialogResult.Yes;
                    ModernTheme.StyleButton(btnYes, Color.FromArgb(58, 129, 214), Color.White);
                    btnYes.SetBounds(224, 166, 120, 34);
                    Controls.Add(btnYes);

                    Button btnNo = new Button();
                    btnNo.Text = "No";
                    btnNo.DialogResult = DialogResult.No;
                    ModernTheme.StyleButton(btnNo, Color.FromArgb(95, 106, 120), Color.White);
                    btnNo.SetBounds(356, 166, 120, 34);
                    Controls.Add(btnNo);

                    AcceptButton = btnYes;
                    CancelButton = btnNo;
                }
                else
                {
                    Button btnOk = new Button();
                    btnOk.Text = "OK";
                    btnOk.DialogResult = DialogResult.OK;
                    ModernTheme.StyleButton(btnOk, Color.FromArgb(58, 129, 214), Color.White);
                    btnOk.SetBounds(356, 166, 120, 34);
                    Controls.Add(btnOk);

                    AcceptButton = btnOk;
                }
            }

            private static Color GetAccent(ModernMessageType type)
            {
                if (type == ModernMessageType.Success) return Color.FromArgb(39, 174, 96);
                if (type == ModernMessageType.Warning) return Color.FromArgb(230, 126, 34);
                if (type == ModernMessageType.Error) return Color.FromArgb(192, 57, 43);
                if (type == ModernMessageType.Question) return Color.FromArgb(52, 152, 219);
                return Color.FromArgb(58, 129, 214);
            }

            private static string GetIcon(ModernMessageType type)
            {
                if (type == ModernMessageType.Success) return "✓";
                if (type == ModernMessageType.Warning) return "!";
                if (type == ModernMessageType.Error) return "✕";
                if (type == ModernMessageType.Question) return "?";
                return "i";
            }
        }
    }
}