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
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgvLopHoc = new System.Windows.Forms.DataGridView();
            this.btnDangKyLop = new System.Windows.Forms.Button();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgvGoiTap = new System.Windows.Forms.DataGridView();
            this.btnDangKyGoi = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.dgvLichSu = new System.Windows.Forms.DataGridView();
            this.tabPage3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLopHoc)).BeginInit();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGoiTap)).BeginInit();
            this.tabControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLichSu)).BeginInit();
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
            // 
            // txtPhanHoi
            // 
            this.txtPhanHoi.Location = new System.Drawing.Point(155, 368);
            this.txtPhanHoi.Name = "txtPhanHoi";
            this.txtPhanHoi.Size = new System.Drawing.Size(672, 96);
            this.txtPhanHoi.TabIndex = 8;
            this.txtPhanHoi.Text = "";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.dgvLichSu);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(983, 308);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Lịch sử của tôi";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnDangKyLop);
            this.tabPage2.Controls.Add(this.dgvLopHoc);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(983, 308);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dgvLopHoc
            // 
            this.dgvLopHoc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLopHoc.Location = new System.Drawing.Point(151, 85);
            this.dgvLopHoc.Name = "dgvLopHoc";
            this.dgvLopHoc.RowHeadersWidth = 51;
            this.dgvLopHoc.RowTemplate.Height = 24;
            this.dgvLopHoc.Size = new System.Drawing.Size(654, 150);
            this.dgvLopHoc.TabIndex = 0;
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
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnDangKyGoi);
            this.tabPage1.Controls.Add(this.dgvGoiTap);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(983, 308);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
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
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(991, 337);
            this.tabControl1.TabIndex = 4;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
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
            this.tabPage3.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLopHoc)).EndInit();
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGoiTap)).EndInit();
            this.tabControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLichSu)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnGuiPhanHoi;
        private System.Windows.Forms.RichTextBox txtPhanHoi;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridView dgvLichSu;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnDangKyLop;
        private System.Windows.Forms.DataGridView dgvLopHoc;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button btnDangKyGoi;
        private System.Windows.Forms.DataGridView dgvGoiTap;
        private System.Windows.Forms.TabControl tabControl1;
    }
}
