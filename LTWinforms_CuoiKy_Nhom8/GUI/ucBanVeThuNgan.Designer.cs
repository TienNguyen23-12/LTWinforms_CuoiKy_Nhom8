namespace LTWinforms_CuoiKy_Nhom8.GUI
{
    partial class ucBanVeThuNgan
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
            this.tcThuNgan = new System.Windows.Forms.TabControl();
            this.tpThuNgan = new System.Windows.Forms.TabPage();
            this.tpPhanHoi = new System.Windows.Forms.TabPage();
            this.btnDuyetOnline = new System.Windows.Forms.Button();
            this.radLopHoc = new System.Windows.Forms.RadioButton();
            this.radGoiTap = new System.Windows.Forms.RadioButton();
            this.dgvLichSu = new System.Windows.Forms.DataGridView();
            this.btnThanhToan = new System.Windows.Forms.Button();
            this.txtSoTien = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboDichVu = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboHoiVien = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvPhanHoi = new System.Windows.Forms.DataGridView();
            this.btnDaDoc = new System.Windows.Forms.Button();
            this.radSanPham = new System.Windows.Forms.RadioButton();
            this.tcThuNgan.SuspendLayout();
            this.tpThuNgan.SuspendLayout();
            this.tpPhanHoi.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLichSu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhanHoi)).BeginInit();
            this.SuspendLayout();
            // 
            // tcThuNgan
            // 
            this.tcThuNgan.Controls.Add(this.tpThuNgan);
            this.tcThuNgan.Controls.Add(this.tpPhanHoi);
            this.tcThuNgan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcThuNgan.Location = new System.Drawing.Point(0, 0);
            this.tcThuNgan.Multiline = true;
            this.tcThuNgan.Name = "tcThuNgan";
            this.tcThuNgan.SelectedIndex = 0;
            this.tcThuNgan.Size = new System.Drawing.Size(995, 566);
            this.tcThuNgan.TabIndex = 0;
            this.tcThuNgan.SelectedIndexChanged += new System.EventHandler(this.tcThuNgan_SelectedIndexChanged);
            // 
            // tpThuNgan
            // 
            this.tpThuNgan.Controls.Add(this.radSanPham);
            this.tpThuNgan.Controls.Add(this.btnDuyetOnline);
            this.tpThuNgan.Controls.Add(this.radLopHoc);
            this.tpThuNgan.Controls.Add(this.radGoiTap);
            this.tpThuNgan.Controls.Add(this.dgvLichSu);
            this.tpThuNgan.Controls.Add(this.btnThanhToan);
            this.tpThuNgan.Controls.Add(this.txtSoTien);
            this.tpThuNgan.Controls.Add(this.label3);
            this.tpThuNgan.Controls.Add(this.cboDichVu);
            this.tpThuNgan.Controls.Add(this.label2);
            this.tpThuNgan.Controls.Add(this.cboHoiVien);
            this.tpThuNgan.Controls.Add(this.label1);
            this.tpThuNgan.Location = new System.Drawing.Point(4, 25);
            this.tpThuNgan.Name = "tpThuNgan";
            this.tpThuNgan.Padding = new System.Windows.Forms.Padding(3);
            this.tpThuNgan.Size = new System.Drawing.Size(987, 537);
            this.tpThuNgan.TabIndex = 0;
            this.tpThuNgan.Text = "Thu ngân & Duyệt đơn";
            this.tpThuNgan.UseVisualStyleBackColor = true;
            // 
            // tpPhanHoi
            // 
            this.tpPhanHoi.Controls.Add(this.btnDaDoc);
            this.tpPhanHoi.Controls.Add(this.dgvPhanHoi);
            this.tpPhanHoi.Location = new System.Drawing.Point(4, 25);
            this.tpPhanHoi.Name = "tpPhanHoi";
            this.tpPhanHoi.Padding = new System.Windows.Forms.Padding(3);
            this.tpPhanHoi.Size = new System.Drawing.Size(987, 537);
            this.tpPhanHoi.TabIndex = 1;
            this.tpPhanHoi.Text = "Tin nhắn Phản hồi";
            this.tpPhanHoi.UseVisualStyleBackColor = true;
            // 
            // btnDuyetOnline
            // 
            this.btnDuyetOnline.Location = new System.Drawing.Point(554, 128);
            this.btnDuyetOnline.Name = "btnDuyetOnline";
            this.btnDuyetOnline.Size = new System.Drawing.Size(96, 23);
            this.btnDuyetOnline.TabIndex = 21;
            this.btnDuyetOnline.Text = "Duyệt Online";
            this.btnDuyetOnline.UseVisualStyleBackColor = true;
            this.btnDuyetOnline.Click += new System.EventHandler(this.btnDuyetOnline_Click);
            // 
            // radLopHoc
            // 
            this.radLopHoc.AutoSize = true;
            this.radLopHoc.Location = new System.Drawing.Point(465, 20);
            this.radLopHoc.Name = "radLopHoc";
            this.radLopHoc.Size = new System.Drawing.Size(142, 20);
            this.radLopHoc.TabIndex = 20;
            this.radLopHoc.Text = "Thanh toán lớp học";
            this.radLopHoc.UseVisualStyleBackColor = true;
            this.radLopHoc.CheckedChanged += new System.EventHandler(this.radLopHoc_CheckedChanged);
            // 
            // radGoiTap
            // 
            this.radGoiTap.AutoSize = true;
            this.radGoiTap.Checked = true;
            this.radGoiTap.Location = new System.Drawing.Point(302, 20);
            this.radGoiTap.Name = "radGoiTap";
            this.radGoiTap.Size = new System.Drawing.Size(129, 20);
            this.radGoiTap.TabIndex = 19;
            this.radGoiTap.TabStop = true;
            this.radGoiTap.Text = "Mua gói tập Gym";
            this.radGoiTap.UseVisualStyleBackColor = true;
            this.radGoiTap.CheckedChanged += new System.EventHandler(this.radGoiTap_CheckedChanged);
            // 
            // dgvLichSu
            // 
            this.dgvLichSu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLichSu.Location = new System.Drawing.Point(64, 211);
            this.dgvLichSu.Name = "dgvLichSu";
            this.dgvLichSu.RowHeadersWidth = 51;
            this.dgvLichSu.RowTemplate.Height = 24;
            this.dgvLichSu.Size = new System.Drawing.Size(861, 305);
            this.dgvLichSu.TabIndex = 18;
            // 
            // btnThanhToan
            // 
            this.btnThanhToan.Location = new System.Drawing.Point(429, 128);
            this.btnThanhToan.Name = "btnThanhToan";
            this.btnThanhToan.Size = new System.Drawing.Size(96, 23);
            this.btnThanhToan.TabIndex = 17;
            this.btnThanhToan.Text = "Thanh toán";
            this.btnThanhToan.UseVisualStyleBackColor = true;
            this.btnThanhToan.Click += new System.EventHandler(this.btnThanhToan_Click);
            // 
            // txtSoTien
            // 
            this.txtSoTien.Location = new System.Drawing.Point(554, 64);
            this.txtSoTien.Name = "txtSoTien";
            this.txtSoTien.ReadOnly = true;
            this.txtSoTien.Size = new System.Drawing.Size(161, 22);
            this.txtSoTien.TabIndex = 16;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(426, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 16);
            this.label3.TabIndex = 15;
            this.label3.Text = "Tiền thanh toán";
            // 
            // cboDichVu
            // 
            this.cboDichVu.FormattingEnabled = true;
            this.cboDichVu.Location = new System.Drawing.Point(211, 110);
            this.cboDichVu.Name = "cboDichVu";
            this.cboDichVu.Size = new System.Drawing.Size(172, 24);
            this.cboDichVu.TabIndex = 14;
            this.cboDichVu.SelectedIndexChanged += new System.EventHandler(this.cboDichVu_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(61, 113);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(137, 16);
            this.label2.TabIndex = 13;
            this.label2.Text = "Chọn dịch vụ / Lớp nợ:";
            // 
            // cboHoiVien
            // 
            this.cboHoiVien.FormattingEnabled = true;
            this.cboHoiVien.Location = new System.Drawing.Point(211, 61);
            this.cboHoiVien.Name = "cboHoiVien";
            this.cboHoiVien.Size = new System.Drawing.Size(172, 24);
            this.cboHoiVien.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(127, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 16);
            this.label1.TabIndex = 11;
            this.label1.Text = "Khách hàng";
            // 
            // dgvPhanHoi
            // 
            this.dgvPhanHoi.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPhanHoi.Location = new System.Drawing.Point(83, 76);
            this.dgvPhanHoi.Name = "dgvPhanHoi";
            this.dgvPhanHoi.RowHeadersWidth = 51;
            this.dgvPhanHoi.RowTemplate.Height = 24;
            this.dgvPhanHoi.Size = new System.Drawing.Size(850, 238);
            this.dgvPhanHoi.TabIndex = 0;
            // 
            // btnDaDoc
            // 
            this.btnDaDoc.Location = new System.Drawing.Point(427, 380);
            this.btnDaDoc.Name = "btnDaDoc";
            this.btnDaDoc.Size = new System.Drawing.Size(75, 23);
            this.btnDaDoc.TabIndex = 1;
            this.btnDaDoc.Text = "Đọc";
            this.btnDaDoc.UseVisualStyleBackColor = true;
            this.btnDaDoc.Click += new System.EventHandler(this.btnDaDoc_Click);
            // 
            // radSanPham
            // 
            this.radSanPham.AutoSize = true;
            this.radSanPham.Location = new System.Drawing.Point(641, 20);
            this.radSanPham.Name = "radSanPham";
            this.radSanPham.Size = new System.Drawing.Size(116, 20);
            this.radSanPham.TabIndex = 22;
            this.radSanPham.Text = "Mua sản phẩm";
            this.radSanPham.UseVisualStyleBackColor = true;
            this.radSanPham.CheckedChanged += new System.EventHandler(this.radSanPham_CheckedChanged);
            // 
            // ucBanVeThuNgan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tcThuNgan);
            this.Name = "ucBanVeThuNgan";
            this.Size = new System.Drawing.Size(995, 566);
            this.Load += new System.EventHandler(this.ucBanVeThuNgan_Load);
            this.tcThuNgan.ResumeLayout(false);
            this.tpThuNgan.ResumeLayout(false);
            this.tpThuNgan.PerformLayout();
            this.tpPhanHoi.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLichSu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhanHoi)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcThuNgan;
        private System.Windows.Forms.Button btnDuyetOnline;
        private System.Windows.Forms.RadioButton radLopHoc;
        private System.Windows.Forms.RadioButton radGoiTap;
        private System.Windows.Forms.DataGridView dgvLichSu;
        private System.Windows.Forms.Button btnThanhToan;
        private System.Windows.Forms.TextBox txtSoTien;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboDichVu;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboHoiVien;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tpPhanHoi;
        private System.Windows.Forms.TabPage tpThuNgan;
        private System.Windows.Forms.Button btnDaDoc;
        private System.Windows.Forms.DataGridView dgvPhanHoi;
        private System.Windows.Forms.RadioButton radSanPham;
    }
}
