namespace LTWinforms_CuoiKy_Nhom8.GUI
{
    partial class ucQuanTriHeThong
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtTenHienThi = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtLuongCoBan = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnLuuThongTin = new System.Windows.Forms.Button();
            this.btnCapNhatLuong = new System.Windows.Forms.Button();
            this.btnKhoaTaiKhoan = new System.Windows.Forms.Button();
            this.btnResetPass = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.cboVaiTro = new System.Windows.Forms.ComboBox();
            this.dgvNhanSu = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNhanSu)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(354, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(212, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "QUẢN TRỊ HỆ THỐNG & NHÂN SỰ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(152, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Tài khoản đang chọn:";
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(320, 63);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(292, 22);
            this.txtUsername.TabIndex = 2;
            // 
            // txtTenHienThi
            // 
            this.txtTenHienThi.Location = new System.Drawing.Point(320, 115);
            this.txtTenHienThi.Name = "txtTenHienThi";
            this.txtTenHienThi.Size = new System.Drawing.Size(292, 22);
            this.txtTenHienThi.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(152, 115);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "Tên hiển thị:";
            // 
            // txtLuongCoBan
            // 
            this.txtLuongCoBan.Location = new System.Drawing.Point(320, 198);
            this.txtLuongCoBan.Name = "txtLuongCoBan";
            this.txtLuongCoBan.Size = new System.Drawing.Size(292, 22);
            this.txtLuongCoBan.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(152, 198);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 16);
            this.label4.TabIndex = 5;
            this.label4.Text = "Lương";
            // 
            // btnLuuThongTin
            // 
            this.btnLuuThongTin.Location = new System.Drawing.Point(102, 251);
            this.btnLuuThongTin.Name = "btnLuuThongTin";
            this.btnLuuThongTin.Size = new System.Drawing.Size(75, 23);
            this.btnLuuThongTin.TabIndex = 7;
            this.btnLuuThongTin.Text = "Lưu";
            this.btnLuuThongTin.UseVisualStyleBackColor = true;
            this.btnLuuThongTin.Click += new System.EventHandler(this.btnLuuThongTin_Click);
            // 
            // btnCapNhatLuong
            // 
            this.btnCapNhatLuong.Location = new System.Drawing.Point(205, 251);
            this.btnCapNhatLuong.Name = "btnCapNhatLuong";
            this.btnCapNhatLuong.Size = new System.Drawing.Size(125, 23);
            this.btnCapNhatLuong.TabIndex = 8;
            this.btnCapNhatLuong.Text = "Cập nhật lương";
            this.btnCapNhatLuong.UseVisualStyleBackColor = true;
            this.btnCapNhatLuong.Click += new System.EventHandler(this.btnCapNhatLuong_Click);
            // 
            // btnKhoaTaiKhoan
            // 
            this.btnKhoaTaiKhoan.Location = new System.Drawing.Point(336, 251);
            this.btnKhoaTaiKhoan.Name = "btnKhoaTaiKhoan";
            this.btnKhoaTaiKhoan.Size = new System.Drawing.Size(161, 23);
            this.btnKhoaTaiKhoan.TabIndex = 9;
            this.btnKhoaTaiKhoan.Text = "Khóa / Mở khóa";
            this.btnKhoaTaiKhoan.UseVisualStyleBackColor = true;
            this.btnKhoaTaiKhoan.Click += new System.EventHandler(this.btnKhoaTaiKhoan_Click);
            // 
            // btnResetPass
            // 
            this.btnResetPass.Location = new System.Drawing.Point(518, 251);
            this.btnResetPass.Name = "btnResetPass";
            this.btnResetPass.Size = new System.Drawing.Size(119, 23);
            this.btnResetPass.TabIndex = 10;
            this.btnResetPass.Text = "Đặt lại Mật khẩu";
            this.btnResetPass.UseVisualStyleBackColor = true;
            this.btnResetPass.Click += new System.EventHandler(this.btnResetPass_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(152, 157);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 16);
            this.label5.TabIndex = 11;
            this.label5.Text = "Vai trò:";
            // 
            // cboVaiTro
            // 
            this.cboVaiTro.FormattingEnabled = true;
            this.cboVaiTro.Items.AddRange(new object[] {
            "Nhân Viên",
            "Huấn Luyện Viên",
            "Hội Viên"});
            this.cboVaiTro.Location = new System.Drawing.Point(320, 157);
            this.cboVaiTro.Name = "cboVaiTro";
            this.cboVaiTro.Size = new System.Drawing.Size(292, 24);
            this.cboVaiTro.TabIndex = 12;
            this.cboVaiTro.SelectedIndexChanged += new System.EventHandler(this.cboVaiTro_SelectedIndexChanged);
            // 
            // dgvNhanSu
            // 
            this.dgvNhanSu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNhanSu.Location = new System.Drawing.Point(102, 303);
            this.dgvNhanSu.Name = "dgvNhanSu";
            this.dgvNhanSu.RowHeadersWidth = 51;
            this.dgvNhanSu.RowTemplate.Height = 24;
            this.dgvNhanSu.Size = new System.Drawing.Size(798, 203);
            this.dgvNhanSu.TabIndex = 13;
            this.dgvNhanSu.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvNhanSu_CellClick);
            // 
            // ucQuanTriHeThong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvNhanSu);
            this.Controls.Add(this.cboVaiTro);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnResetPass);
            this.Controls.Add(this.btnKhoaTaiKhoan);
            this.Controls.Add(this.btnCapNhatLuong);
            this.Controls.Add(this.btnLuuThongTin);
            this.Controls.Add(this.txtLuongCoBan);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtTenHienThi);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "ucQuanTriHeThong";
            this.Size = new System.Drawing.Size(985, 549);
            this.Load += new System.EventHandler(this.ucQuanTriHeThong_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNhanSu)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtTenHienThi;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtLuongCoBan;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnLuuThongTin;
        private System.Windows.Forms.Button btnCapNhatLuong;
        private System.Windows.Forms.Button btnKhoaTaiKhoan;
        private System.Windows.Forms.Button btnResetPass;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboVaiTro;
        private System.Windows.Forms.DataGridView dgvNhanSu;
    }
}
