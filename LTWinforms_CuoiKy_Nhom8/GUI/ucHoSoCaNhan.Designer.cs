namespace LTWinforms_CuoiKy_Nhom8.GUI
{
    partial class ucHoSoCaNhan
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
            this.lblXinChao = new System.Windows.Forms.Label();
            this.lblQuyen = new System.Windows.Forms.Label();
            this.lblThongTinThem = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnLuuMatKhau = new System.Windows.Forms.Button();
            this.txtXacNhanPass = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPassMoi = new System.Windows.Forms.TextBox();
            this.txtPassCu = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnLuuHoSo = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSDT = new System.Windows.Forms.TextBox();
            this.txtHoTen = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.dtpNgaySinh = new System.Windows.Forms.DateTimePicker();
            this.cboGioiTinh = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblXinChao
            // 
            this.lblXinChao.AutoSize = true;
            this.lblXinChao.Location = new System.Drawing.Point(96, 37);
            this.lblXinChao.Name = "lblXinChao";
            this.lblXinChao.Size = new System.Drawing.Size(71, 16);
            this.lblXinChao.TabIndex = 0;
            this.lblXinChao.Text = "lblXinChao";
            // 
            // lblQuyen
            // 
            this.lblQuyen.AutoSize = true;
            this.lblQuyen.Location = new System.Drawing.Point(96, 73);
            this.lblQuyen.Name = "lblQuyen";
            this.lblQuyen.Size = new System.Drawing.Size(60, 16);
            this.lblQuyen.TabIndex = 1;
            this.lblQuyen.Text = "lblQuyen";
            // 
            // lblThongTinThem
            // 
            this.lblThongTinThem.AutoSize = true;
            this.lblThongTinThem.Location = new System.Drawing.Point(96, 111);
            this.lblThongTinThem.Name = "lblThongTinThem";
            this.lblThongTinThem.Size = new System.Drawing.Size(114, 16);
            this.lblThongTinThem.TabIndex = 2;
            this.lblThongTinThem.Text = "lblThongTinThem";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnLuuMatKhau);
            this.groupBox1.Controls.Add(this.txtXacNhanPass);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtPassMoi);
            this.groupBox1.Controls.Add(this.txtPassCu);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(99, 299);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(469, 173);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Đổi mật khẩu";
            // 
            // btnLuuMatKhau
            // 
            this.btnLuuMatKhau.Location = new System.Drawing.Point(377, 44);
            this.btnLuuMatKhau.Name = "btnLuuMatKhau";
            this.btnLuuMatKhau.Size = new System.Drawing.Size(75, 23);
            this.btnLuuMatKhau.TabIndex = 6;
            this.btnLuuMatKhau.Text = "Lưu";
            this.btnLuuMatKhau.UseVisualStyleBackColor = true;
            this.btnLuuMatKhau.Click += new System.EventHandler(this.btnLuuMatKhau_Click);
            // 
            // txtXacNhanPass
            // 
            this.txtXacNhanPass.Location = new System.Drawing.Point(131, 145);
            this.txtXacNhanPass.Name = "txtXacNhanPass";
            this.txtXacNhanPass.PasswordChar = '*';
            this.txtXacNhanPass.Size = new System.Drawing.Size(218, 22);
            this.txtXacNhanPass.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 145);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Xác nhận";
            // 
            // txtPassMoi
            // 
            this.txtPassMoi.Location = new System.Drawing.Point(131, 85);
            this.txtPassMoi.Name = "txtPassMoi";
            this.txtPassMoi.PasswordChar = '*';
            this.txtPassMoi.Size = new System.Drawing.Size(218, 22);
            this.txtPassMoi.TabIndex = 3;
            // 
            // txtPassCu
            // 
            this.txtPassCu.Location = new System.Drawing.Point(131, 27);
            this.txtPassCu.Name = "txtPassCu";
            this.txtPassCu.PasswordChar = '*';
            this.txtPassCu.Size = new System.Drawing.Size(218, 22);
            this.txtPassCu.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Mật khẩu mới";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mật khẩu cũ";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cboGioiTinh);
            this.groupBox2.Controls.Add(this.dtpNgaySinh);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.btnLuuHoSo);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtSDT);
            this.groupBox2.Controls.Add(this.txtHoTen);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(443, 37);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(469, 230);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Đổi thông tin";
            // 
            // btnLuuHoSo
            // 
            this.btnLuuHoSo.Location = new System.Drawing.Point(377, 44);
            this.btnLuuHoSo.Name = "btnLuuHoSo";
            this.btnLuuHoSo.Size = new System.Drawing.Size(75, 23);
            this.btnLuuHoSo.TabIndex = 6;
            this.btnLuuHoSo.Text = "Lưu";
            this.btnLuuHoSo.UseVisualStyleBackColor = true;
            this.btnLuuHoSo.Click += new System.EventHandler(this.btnLuuHoSo_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 145);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 16);
            this.label4.TabIndex = 4;
            this.label4.Text = "Ngày sinh";
            // 
            // txtSDT
            // 
            this.txtSDT.Location = new System.Drawing.Point(131, 85);
            this.txtSDT.Name = "txtSDT";
            this.txtSDT.Size = new System.Drawing.Size(218, 22);
            this.txtSDT.TabIndex = 3;
            // 
            // txtHoTen
            // 
            this.txtHoTen.Location = new System.Drawing.Point(131, 27);
            this.txtHoTen.Name = "txtHoTen";
            this.txtHoTen.Size = new System.Drawing.Size(218, 22);
            this.txtHoTen.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 85);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 16);
            this.label5.TabIndex = 1;
            this.label5.Text = "Số điện thoại";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(19, 30);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 16);
            this.label6.TabIndex = 0;
            this.label6.Text = "Họ và tên";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(19, 186);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(54, 16);
            this.label7.TabIndex = 7;
            this.label7.Text = "Giới tính";
            // 
            // dtpNgaySinh
            // 
            this.dtpNgaySinh.CustomFormat = "dd/MM/yyyy";
            this.dtpNgaySinh.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNgaySinh.Location = new System.Drawing.Point(131, 138);
            this.dtpNgaySinh.Name = "dtpNgaySinh";
            this.dtpNgaySinh.Size = new System.Drawing.Size(218, 22);
            this.dtpNgaySinh.TabIndex = 8;
            // 
            // cboGioiTinh
            // 
            this.cboGioiTinh.FormattingEnabled = true;
            this.cboGioiTinh.Items.AddRange(new object[] {
            "Nam",
            "Nữ",
            "Khác"});
            this.cboGioiTinh.Location = new System.Drawing.Point(131, 178);
            this.cboGioiTinh.Name = "cboGioiTinh";
            this.cboGioiTinh.Size = new System.Drawing.Size(218, 24);
            this.cboGioiTinh.TabIndex = 9;
            // 
            // ucHoSoCaNhan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblThongTinThem);
            this.Controls.Add(this.lblQuyen);
            this.Controls.Add(this.lblXinChao);
            this.Name = "ucHoSoCaNhan";
            this.Size = new System.Drawing.Size(988, 568);
            this.Load += new System.EventHandler(this.ucHoSoCaNhan_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblXinChao;
        private System.Windows.Forms.Label lblQuyen;
        private System.Windows.Forms.Label lblThongTinThem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtXacNhanPass;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPassMoi;
        private System.Windows.Forms.TextBox txtPassCu;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button btnLuuMatKhau;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cboGioiTinh;
        private System.Windows.Forms.DateTimePicker dtpNgaySinh;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnLuuHoSo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSDT;
        private System.Windows.Forms.TextBox txtHoTen;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}
