namespace LTWinforms_CuoiKy_Nhom8.GUI
{
    partial class ucDichVuHoiVien
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
            this.btnGuiPhanHoi = new System.Windows.Forms.Button();
            this.txtPhanHoi = new System.Windows.Forms.RichTextBox();
            this.tpLichSu = new System.Windows.Forms.TabPage();
            this.dgvLichSu = new System.Windows.Forms.DataGridView();
            this.tpLopHoc = new System.Windows.Forms.TabPage();
            this.btnLoc = new System.Windows.Forms.Button();
            this.chkLocNgay = new System.Windows.Forms.CheckBox();
            this.dtpDenNgay = new System.Windows.Forms.DateTimePicker();
            this.dtpTuNgay = new System.Windows.Forms.DateTimePicker();
            this.cboLocHLV = new System.Windows.Forms.ComboBox();
            this.btnDangKyLop = new System.Windows.Forms.Button();
            this.dgvLopHoc = new System.Windows.Forms.DataGridView();
            this.tpGoiTap = new System.Windows.Forms.TabPage();
            this.btnDangKyGoi = new System.Windows.Forms.Button();
            this.dgvGoiTap = new System.Windows.Forms.DataGridView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.btnHuyDangKy = new System.Windows.Forms.Button();
            this.tpLichSu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLichSu)).BeginInit();
            this.tpLopHoc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLopHoc)).BeginInit();
            this.tpGoiTap.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGoiTap)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnGuiPhanHoi
            // 
            this.btnGuiPhanHoi.Location = new System.Drawing.Point(447, 494);
            this.btnGuiPhanHoi.Name = "btnGuiPhanHoi";
            this.btnGuiPhanHoi.Size = new System.Drawing.Size(75, 23);
            this.btnGuiPhanHoi.TabIndex = 9;
            this.btnGuiPhanHoi.Text = "Phản hồi";
            this.btnGuiPhanHoi.UseVisualStyleBackColor = true;
            this.btnGuiPhanHoi.Click += new System.EventHandler(this.btnGuiPhanHoi_Click);
            // 
            // txtPhanHoi
            // 
            this.txtPhanHoi.Location = new System.Drawing.Point(155, 368);
            this.txtPhanHoi.Name = "txtPhanHoi";
            this.txtPhanHoi.Size = new System.Drawing.Size(672, 96);
            this.txtPhanHoi.TabIndex = 8;
            this.txtPhanHoi.Text = "";
            // 
            // tpLichSu
            // 
            this.tpLichSu.Controls.Add(this.btnHuyDangKy);
            this.tpLichSu.Controls.Add(this.dgvLichSu);
            this.tpLichSu.Location = new System.Drawing.Point(4, 25);
            this.tpLichSu.Name = "tpLichSu";
            this.tpLichSu.Padding = new System.Windows.Forms.Padding(3);
            this.tpLichSu.Size = new System.Drawing.Size(983, 308);
            this.tpLichSu.TabIndex = 2;
            this.tpLichSu.Text = "Lịch sử của tôi";
            this.tpLichSu.UseVisualStyleBackColor = true;
            // 
            // dgvLichSu
            // 
            this.dgvLichSu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLichSu.Location = new System.Drawing.Point(151, 40);
            this.dgvLichSu.Name = "dgvLichSu";
            this.dgvLichSu.RowHeadersWidth = 51;
            this.dgvLichSu.RowTemplate.Height = 24;
            this.dgvLichSu.Size = new System.Drawing.Size(672, 150);
            this.dgvLichSu.TabIndex = 0;
            this.dgvLichSu.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvLichSu_CellFormatting);
            // 
            // tpLopHoc
            // 
            this.tpLopHoc.Controls.Add(this.btnLoc);
            this.tpLopHoc.Controls.Add(this.chkLocNgay);
            this.tpLopHoc.Controls.Add(this.dtpDenNgay);
            this.tpLopHoc.Controls.Add(this.dtpTuNgay);
            this.tpLopHoc.Controls.Add(this.cboLocHLV);
            this.tpLopHoc.Controls.Add(this.btnDangKyLop);
            this.tpLopHoc.Controls.Add(this.dgvLopHoc);
            this.tpLopHoc.Location = new System.Drawing.Point(4, 25);
            this.tpLopHoc.Name = "tpLopHoc";
            this.tpLopHoc.Padding = new System.Windows.Forms.Padding(3);
            this.tpLopHoc.Size = new System.Drawing.Size(983, 308);
            this.tpLopHoc.TabIndex = 1;
            this.tpLopHoc.Text = "Lớp học";
            this.tpLopHoc.UseVisualStyleBackColor = true;
            // 
            // btnLoc
            // 
            this.btnLoc.Location = new System.Drawing.Point(401, 56);
            this.btnLoc.Name = "btnLoc";
            this.btnLoc.Size = new System.Drawing.Size(117, 23);
            this.btnLoc.TabIndex = 6;
            this.btnLoc.Text = "Lọc dữ liệu";
            this.btnLoc.UseVisualStyleBackColor = true;
            this.btnLoc.Click += new System.EventHandler(this.btnLoc_Click);
            // 
            // chkLocNgay
            // 
            this.chkLocNgay.AutoSize = true;
            this.chkLocNgay.Location = new System.Drawing.Point(805, 31);
            this.chkLocNgay.Name = "chkLocNgay";
            this.chkLocNgay.Size = new System.Drawing.Size(113, 20);
            this.chkLocNgay.TabIndex = 5;
            this.chkLocNgay.Text = "Lọc theo ngày";
            this.chkLocNgay.UseVisualStyleBackColor = true;
            this.chkLocNgay.CheckedChanged += new System.EventHandler(this.chkLocNgay_CheckedChanged);
            // 
            // dtpDenNgay
            // 
            this.dtpDenNgay.CustomFormat = "dd/MM/yyyy";
            this.dtpDenNgay.Enabled = false;
            this.dtpDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDenNgay.Location = new System.Drawing.Point(583, 28);
            this.dtpDenNgay.Name = "dtpDenNgay";
            this.dtpDenNgay.Size = new System.Drawing.Size(200, 22);
            this.dtpDenNgay.TabIndex = 4;
            // 
            // dtpTuNgay
            // 
            this.dtpTuNgay.CustomFormat = "dd/MM/yyyy";
            this.dtpTuNgay.Enabled = false;
            this.dtpTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTuNgay.Location = new System.Drawing.Point(334, 28);
            this.dtpTuNgay.Name = "dtpTuNgay";
            this.dtpTuNgay.Size = new System.Drawing.Size(200, 22);
            this.dtpTuNgay.TabIndex = 3;
            // 
            // cboLocHLV
            // 
            this.cboLocHLV.FormattingEnabled = true;
            this.cboLocHLV.Location = new System.Drawing.Point(151, 27);
            this.cboLocHLV.Name = "cboLocHLV";
            this.cboLocHLV.Size = new System.Drawing.Size(121, 24);
            this.cboLocHLV.TabIndex = 2;
            // 
            // btnDangKyLop
            // 
            this.btnDangKyLop.Location = new System.Drawing.Point(401, 264);
            this.btnDangKyLop.Name = "btnDangKyLop";
            this.btnDangKyLop.Size = new System.Drawing.Size(75, 23);
            this.btnDangKyLop.TabIndex = 1;
            this.btnDangKyLop.Text = "Đăng ký";
            this.btnDangKyLop.UseVisualStyleBackColor = true;
            this.btnDangKyLop.Click += new System.EventHandler(this.btnDangKyLop_Click);
            // 
            // dgvLopHoc
            // 
            this.dgvLopHoc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLopHoc.Location = new System.Drawing.Point(151, 97);
            this.dgvLopHoc.Name = "dgvLopHoc";
            this.dgvLopHoc.RowHeadersWidth = 51;
            this.dgvLopHoc.RowTemplate.Height = 24;
            this.dgvLopHoc.Size = new System.Drawing.Size(654, 150);
            this.dgvLopHoc.TabIndex = 0;
            // 
            // tpGoiTap
            // 
            this.tpGoiTap.Controls.Add(this.btnDangKyGoi);
            this.tpGoiTap.Controls.Add(this.dgvGoiTap);
            this.tpGoiTap.Location = new System.Drawing.Point(4, 25);
            this.tpGoiTap.Name = "tpGoiTap";
            this.tpGoiTap.Padding = new System.Windows.Forms.Padding(3);
            this.tpGoiTap.Size = new System.Drawing.Size(983, 308);
            this.tpGoiTap.TabIndex = 0;
            this.tpGoiTap.Text = "Gói tập";
            this.tpGoiTap.UseVisualStyleBackColor = true;
            // 
            // btnDangKyGoi
            // 
            this.btnDangKyGoi.Location = new System.Drawing.Point(439, 254);
            this.btnDangKyGoi.Name = "btnDangKyGoi";
            this.btnDangKyGoi.Size = new System.Drawing.Size(75, 23);
            this.btnDangKyGoi.TabIndex = 6;
            this.btnDangKyGoi.Text = "Đăng ký";
            this.btnDangKyGoi.UseVisualStyleBackColor = true;
            this.btnDangKyGoi.Click += new System.EventHandler(this.btnDangKyGoi_Click);
            // 
            // dgvGoiTap
            // 
            this.dgvGoiTap.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGoiTap.Location = new System.Drawing.Point(69, 58);
            this.dgvGoiTap.Name = "dgvGoiTap";
            this.dgvGoiTap.RowHeadersWidth = 51;
            this.dgvGoiTap.RowTemplate.Height = 24;
            this.dgvGoiTap.Size = new System.Drawing.Size(845, 172);
            this.dgvGoiTap.TabIndex = 5;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpGoiTap);
            this.tabControl1.Controls.Add(this.tpLopHoc);
            this.tabControl1.Controls.Add(this.tpLichSu);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(991, 337);
            this.tabControl1.TabIndex = 4;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // btnHuyDangKy
            // 
            this.btnHuyDangKy.Location = new System.Drawing.Point(443, 244);
            this.btnHuyDangKy.Name = "btnHuyDangKy";
            this.btnHuyDangKy.Size = new System.Drawing.Size(75, 23);
            this.btnHuyDangKy.TabIndex = 10;
            this.btnHuyDangKy.Text = "Hủy đăng ký";
            this.btnHuyDangKy.UseVisualStyleBackColor = true;
            this.btnHuyDangKy.Click += new System.EventHandler(this.btnHuyDangKy_Click);
            // 
            // ucDichVuHoiVien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnGuiPhanHoi);
            this.Controls.Add(this.txtPhanHoi);
            this.Controls.Add(this.tabControl1);
            this.Name = "ucDichVuHoiVien";
            this.Size = new System.Drawing.Size(991, 861);
            this.Load += new System.EventHandler(this.ucDichVuHoiVien_Load);
            this.tpLichSu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLichSu)).EndInit();
            this.tpLopHoc.ResumeLayout(false);
            this.tpLopHoc.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLopHoc)).EndInit();
            this.tpGoiTap.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGoiTap)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnGuiPhanHoi;
        private System.Windows.Forms.RichTextBox txtPhanHoi;
        private System.Windows.Forms.TabPage tpLichSu;
        private System.Windows.Forms.DataGridView dgvLichSu;
        private System.Windows.Forms.TabPage tpLopHoc;
        private System.Windows.Forms.Button btnDangKyLop;
        private System.Windows.Forms.DataGridView dgvLopHoc;
        private System.Windows.Forms.TabPage tpGoiTap;
        private System.Windows.Forms.Button btnDangKyGoi;
        private System.Windows.Forms.DataGridView dgvGoiTap;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.ComboBox cboLocHLV;
        private System.Windows.Forms.Button btnLoc;
        private System.Windows.Forms.CheckBox chkLocNgay;
        private System.Windows.Forms.DateTimePicker dtpDenNgay;
        private System.Windows.Forms.DateTimePicker dtpTuNgay;
        private System.Windows.Forms.Button btnHuyDangKy;
    }
}
