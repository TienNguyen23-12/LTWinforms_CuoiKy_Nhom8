namespace LTWinforms_CuoiKy_Nhom8.GUI
{
    partial class ucQuanLyGoiTap
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
            this.txtThoiHan = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTenGoi = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMaGoi = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtGiaTien = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.dgvGoiTap = new System.Windows.Forms.DataGridView();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.btnKhoa = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnThem = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGoiTap)).BeginInit();
            this.SuspendLayout();
            // 
            // txtThoiHan
            // 
            this.txtThoiHan.Location = new System.Drawing.Point(142, 126);
            this.txtThoiHan.Name = "txtThoiHan";
            this.txtThoiHan.Size = new System.Drawing.Size(217, 22);
            this.txtThoiHan.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(52, 129);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 16);
            this.label3.TabIndex = 10;
            this.label3.Text = "Thời hạn";
            // 
            // txtTenGoi
            // 
            this.txtTenGoi.Location = new System.Drawing.Point(142, 82);
            this.txtTenGoi.Name = "txtTenGoi";
            this.txtTenGoi.Size = new System.Drawing.Size(217, 22);
            this.txtTenGoi.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(52, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 16);
            this.label2.TabIndex = 8;
            this.label2.Text = "Tên Gói";
            // 
            // txtMaGoi
            // 
            this.txtMaGoi.Location = new System.Drawing.Point(142, 36);
            this.txtMaGoi.Name = "txtMaGoi";
            this.txtMaGoi.Size = new System.Drawing.Size(217, 22);
            this.txtMaGoi.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(52, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "Mã Gói";
            // 
            // txtGiaTien
            // 
            this.txtGiaTien.Location = new System.Drawing.Point(540, 36);
            this.txtGiaTien.Name = "txtGiaTien";
            this.txtGiaTien.Size = new System.Drawing.Size(217, 22);
            this.txtGiaTien.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(450, 39);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 16);
            this.label6.TabIndex = 12;
            this.label6.Text = "Giá tiền";
            // 
            // dgvGoiTap
            // 
            this.dgvGoiTap.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGoiTap.Location = new System.Drawing.Point(55, 305);
            this.dgvGoiTap.Name = "dgvGoiTap";
            this.dgvGoiTap.RowHeadersWidth = 51;
            this.dgvGoiTap.RowTemplate.Height = 24;
            this.dgvGoiTap.Size = new System.Drawing.Size(900, 188);
            this.dgvGoiTap.TabIndex = 25;
            this.dgvGoiTap.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvGoiTap_CellClick);
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.Location = new System.Drawing.Point(390, 241);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(75, 23);
            this.btnTimKiem.TabIndex = 24;
            this.btnTimKiem.Text = "Tìm kiếm";
            this.btnTimKiem.UseVisualStyleBackColor = true;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Location = new System.Drawing.Point(142, 241);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(217, 22);
            this.txtTimKiem.TabIndex = 23;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(70, 244);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 16);
            this.label7.TabIndex = 22;
            this.label7.Text = "Tìm kiếm";
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.Location = new System.Drawing.Point(548, 176);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(75, 23);
            this.btnLamMoi.TabIndex = 21;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = true;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // btnKhoa
            // 
            this.btnKhoa.Location = new System.Drawing.Point(434, 176);
            this.btnKhoa.Name = "btnKhoa";
            this.btnKhoa.Size = new System.Drawing.Size(75, 23);
            this.btnKhoa.TabIndex = 20;
            this.btnKhoa.Text = "Khóa";
            this.btnKhoa.UseVisualStyleBackColor = true;
            this.btnKhoa.Click += new System.EventHandler(this.btnKhoa_Click);
            // 
            // btnSua
            // 
            this.btnSua.Location = new System.Drawing.Point(309, 176);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(75, 23);
            this.btnSua.TabIndex = 19;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = true;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnThem
            // 
            this.btnThem.Location = new System.Drawing.Point(169, 176);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(75, 23);
            this.btnThem.TabIndex = 18;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = true;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // ucQuanLyGoiTap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvGoiTap);
            this.Controls.Add(this.btnTimKiem);
            this.Controls.Add(this.txtTimKiem);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnLamMoi);
            this.Controls.Add(this.btnKhoa);
            this.Controls.Add(this.btnSua);
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.txtGiaTien);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtThoiHan);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtTenGoi);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtMaGoi);
            this.Controls.Add(this.label1);
            this.Name = "ucQuanLyGoiTap";
            this.Size = new System.Drawing.Size(993, 564);
            this.Load += new System.EventHandler(this.ucQuanLyGoiTap_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGoiTap)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtThoiHan;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTenGoi;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMaGoi;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtGiaTien;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dgvGoiTap;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Button btnKhoa;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnThem;
    }
}
