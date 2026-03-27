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
            this.label1 = new System.Windows.Forms.Label();
            this.cboHoiVien = new System.Windows.Forms.ComboBox();
            this.cboDichVu = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSoTien = new System.Windows.Forms.TextBox();
            this.btnThanhToan = new System.Windows.Forms.Button();
            this.dgvLichSu = new System.Windows.Forms.DataGridView();
            this.radGoiTap = new System.Windows.Forms.RadioButton();
            this.radLopHoc = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLichSu)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(55, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Khách hàng";
            // 
            // cboHoiVien
            // 
            this.cboHoiVien.FormattingEnabled = true;
            this.cboHoiVien.Location = new System.Drawing.Point(139, 51);
            this.cboHoiVien.Name = "cboHoiVien";
            this.cboHoiVien.Size = new System.Drawing.Size(172, 24);
            this.cboHoiVien.TabIndex = 1;
            this.cboHoiVien.SelectedIndexChanged += new System.EventHandler(this.cboHoiVien_SelectedIndexChanged);
            // 
            // cboDichVu
            // 
            this.cboDichVu.FormattingEnabled = true;
            this.cboDichVu.Location = new System.Drawing.Point(570, 69);
            this.cboDichVu.Name = "cboDichVu";
            this.cboDichVu.Size = new System.Drawing.Size(172, 24);
            this.cboDichVu.TabIndex = 3;
            this.cboDichVu.SelectedIndexChanged += new System.EventHandler(this.cboDichVu_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(420, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(137, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Chọn dịch vụ / Lớp nợ:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(55, 123);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Tiền thanh toán";
            // 
            // txtSoTien
            // 
            this.txtSoTien.Location = new System.Drawing.Point(183, 123);
            this.txtSoTien.Name = "txtSoTien";
            this.txtSoTien.ReadOnly = true;
            this.txtSoTien.Size = new System.Drawing.Size(161, 22);
            this.txtSoTien.TabIndex = 5;
            // 
            // btnThanhToan
            // 
            this.btnThanhToan.Location = new System.Drawing.Point(423, 123);
            this.btnThanhToan.Name = "btnThanhToan";
            this.btnThanhToan.Size = new System.Drawing.Size(96, 23);
            this.btnThanhToan.TabIndex = 6;
            this.btnThanhToan.Text = "Thanh toán";
            this.btnThanhToan.UseVisualStyleBackColor = true;
            this.btnThanhToan.Click += new System.EventHandler(this.btnThanhToan_Click);
            // 
            // dgvLichSu
            // 
            this.dgvLichSu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLichSu.Location = new System.Drawing.Point(58, 206);
            this.dgvLichSu.Name = "dgvLichSu";
            this.dgvLichSu.RowHeadersWidth = 51;
            this.dgvLichSu.RowTemplate.Height = 24;
            this.dgvLichSu.Size = new System.Drawing.Size(861, 305);
            this.dgvLichSu.TabIndex = 7;
            this.dgvLichSu.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLichSu_CellClick);
            // 
            // radGoiTap
            // 
            this.radGoiTap.AutoSize = true;
            this.radGoiTap.Checked = true;
            this.radGoiTap.Location = new System.Drawing.Point(385, 15);
            this.radGoiTap.Name = "radGoiTap";
            this.radGoiTap.Size = new System.Drawing.Size(129, 20);
            this.radGoiTap.TabIndex = 8;
            this.radGoiTap.TabStop = true;
            this.radGoiTap.Text = "Mua gói tập Gym";
            this.radGoiTap.UseVisualStyleBackColor = true;
            this.radGoiTap.CheckedChanged += new System.EventHandler(this.radGoiTap_CheckedChanged);
            // 
            // radLopHoc
            // 
            this.radLopHoc.AutoSize = true;
            this.radLopHoc.Location = new System.Drawing.Point(548, 15);
            this.radLopHoc.Name = "radLopHoc";
            this.radLopHoc.Size = new System.Drawing.Size(142, 20);
            this.radLopHoc.TabIndex = 9;
            this.radLopHoc.Text = "Thanh toán lớp học";
            this.radLopHoc.UseVisualStyleBackColor = true;
            this.radLopHoc.CheckedChanged += new System.EventHandler(this.radLopHoc_CheckedChanged);
            // 
            // ucBanVeThuNgan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.radLopHoc);
            this.Controls.Add(this.radGoiTap);
            this.Controls.Add(this.dgvLichSu);
            this.Controls.Add(this.btnThanhToan);
            this.Controls.Add(this.txtSoTien);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cboDichVu);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboHoiVien);
            this.Controls.Add(this.label1);
            this.Name = "ucBanVeThuNgan";
            this.Size = new System.Drawing.Size(995, 566);
            this.Load += new System.EventHandler(this.ucBanVeThuNgan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLichSu)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboHoiVien;
        private System.Windows.Forms.ComboBox cboDichVu;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSoTien;
        private System.Windows.Forms.Button btnThanhToan;
        private System.Windows.Forms.DataGridView dgvLichSu;
        private System.Windows.Forms.RadioButton radGoiTap;
        private System.Windows.Forms.RadioButton radLopHoc;
    }
}
