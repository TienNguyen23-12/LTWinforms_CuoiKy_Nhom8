namespace LTWinforms_CuoiKy_Nhom8.GUI
{
    partial class ucDangNhap
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.linkLabelDangKy = new System.Windows.Forms.LinkLabel();
            this.btnDangNhap = new System.Windows.Forms.Button();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.linkQuenMatKhau = new System.Windows.Forms.LinkLabel();
            this.btnThoat = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // linkLabelDangKy
            // 
            this.linkLabelDangKy.AutoSize = true;
            this.linkLabelDangKy.Location = new System.Drawing.Point(13, 233);
            this.linkLabelDangKy.Name = "linkLabelDangKy";
            this.linkLabelDangKy.Size = new System.Drawing.Size(205, 16);
            this.linkLabelDangKy.TabIndex = 11;
            this.linkLabelDangKy.TabStop = true;
            this.linkLabelDangKy.Text = "Chưa có tài khoản? Đăng ký ngay";
            this.linkLabelDangKy.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelDangKy_LinkClicked);
            // 
            // btnDangNhap
            // 
            this.btnDangNhap.Location = new System.Drawing.Point(496, 121);
            this.btnDangNhap.Name = "btnDangNhap";
            this.btnDangNhap.Size = new System.Drawing.Size(139, 23);
            this.btnDangNhap.TabIndex = 10;
            this.btnDangNhap.Text = "Đăng Nhập";
            this.btnDangNhap.UseVisualStyleBackColor = true;
            this.btnDangNhap.Click += new System.EventHandler(this.btnDangNhap_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(161, 180);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(268, 22);
            this.txtPassword.TabIndex = 9;
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(161, 118);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(268, 22);
            this.txtUsername.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 180);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "Mật khẩu:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 121);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "Tên đăng nhập:";
            // 
            // linkQuenMatKhau
            // 
            this.linkQuenMatKhau.AutoSize = true;
            this.linkQuenMatKhau.Location = new System.Drawing.Point(289, 233);
            this.linkQuenMatKhau.Name = "linkQuenMatKhau";
            this.linkQuenMatKhau.Size = new System.Drawing.Size(103, 16);
            this.linkQuenMatKhau.TabIndex = 21;
            this.linkQuenMatKhau.TabStop = true;
            this.linkQuenMatKhau.Text = "Quên mật khẩu?";
            this.linkQuenMatKhau.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkQuenMatKhau_LinkClicked);
            // 
            // btnThoat
            // 
            this.btnThoat.Location = new System.Drawing.Point(496, 180);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(139, 23);
            this.btnThoat.TabIndex = 22;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // ucDangNhap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.linkQuenMatKhau);
            this.Controls.Add(this.linkLabelDangKy);
            this.Controls.Add(this.btnDangNhap);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "ucDangNhap";
            this.Size = new System.Drawing.Size(663, 406);
            this.Load += new System.EventHandler(this.ucDangNhap_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel linkLabelDangKy;
        private System.Windows.Forms.Button btnDangNhap;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel linkQuenMatKhau;
        private System.Windows.Forms.Button btnThoat;
    }
}
