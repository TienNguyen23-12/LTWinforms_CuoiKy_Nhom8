namespace LTWinforms_CuoiKy_Nhom8.GUI
{
    partial class ucTrangChu
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.pnlThanhMenu = new System.Windows.Forms.Panel();
            this.flowMenuBottom = new System.Windows.Forms.FlowLayoutPanel();
            this.btnLichHoc = new System.Windows.Forms.Button();
            this.btnQuanLyPhongTap = new System.Windows.Forms.Button();
            this.btnHoSoCaNhan = new System.Windows.Forms.Button();
            this.btnDangXuat = new System.Windows.Forms.Button();
            this.flowMenuMain = new System.Windows.Forms.FlowLayoutPanel();
            this.btnQuanTriHeThong = new System.Windows.Forms.Button();
            this.btnChamCong = new System.Windows.Forms.Button();
            this.btnThongKeDoanhThu = new System.Windows.Forms.Button();
            this.btnQuanLySanPham = new System.Windows.Forms.Button();
            this.btnQuanLyHoiVien = new System.Windows.Forms.Button();
            this.btnQuanLyGoiTap = new System.Windows.Forms.Button();
            this.btnQuanLyLopHoc = new System.Windows.Forms.Button();
            this.btnBanVeThuNgan = new System.Windows.Forms.Button();
            this.btnTinNhan = new System.Windows.Forms.Button();
            this.btnDangKyDichVu = new System.Windows.Forms.Button();
            this.btnLichSuGiaoDich = new System.Windows.Forms.Button();
            this.btnLuongThuong = new System.Windows.Forms.Button();
            this.panelMenuTop = new System.Windows.Forms.Panel();
            this.lblBrand = new System.Windows.Forms.Label();
            this.lblXinChao = new System.Windows.Forms.Label();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlNoiDungChinh = new System.Windows.Forms.Panel();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblModuleTitle = new System.Windows.Forms.Label();
            this.pnlThanhMenu.SuspendLayout();
            this.flowMenuBottom.SuspendLayout();
            this.flowMenuMain.SuspendLayout();
            this.panelMenuTop.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlThanhMenu
            // 
            this.pnlThanhMenu.BackColor = System.Drawing.Color.FromArgb(24, 35, 49);
            this.pnlThanhMenu.Controls.Add(this.flowMenuBottom);
            this.pnlThanhMenu.Controls.Add(this.flowMenuMain);
            this.pnlThanhMenu.Controls.Add(this.panelMenuTop);
            this.pnlThanhMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlThanhMenu.Location = new System.Drawing.Point(0, 0);
            this.pnlThanhMenu.Name = "pnlThanhMenu";
            this.pnlThanhMenu.Size = new System.Drawing.Size(230, 900);
            this.pnlThanhMenu.TabIndex = 0;
            // 
            // flowMenuBottom
            // 
            this.flowMenuBottom.Controls.Add(this.btnLichHoc);
            this.flowMenuBottom.Controls.Add(this.btnQuanLyPhongTap);
            this.flowMenuBottom.Controls.Add(this.btnHoSoCaNhan);
            this.flowMenuBottom.Controls.Add(this.btnDangXuat);
            this.flowMenuBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowMenuBottom.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowMenuBottom.Location = new System.Drawing.Point(0, 710);
            this.flowMenuBottom.Name = "flowMenuBottom";
            this.flowMenuBottom.Size = new System.Drawing.Size(230, 190);
            this.flowMenuBottom.TabIndex = 2;
            this.flowMenuBottom.WrapContents = false;
            // 
            // btnLichHoc
            // 
            this.btnLichHoc.Location = new System.Drawing.Point(3, 3);
            this.btnLichHoc.Name = "btnLichHoc";
            this.btnLichHoc.Size = new System.Drawing.Size(100, 33);
            this.btnLichHoc.TabIndex = 11;
            this.btnLichHoc.UseVisualStyleBackColor = true;
            this.btnLichHoc.Click += new System.EventHandler(this.btnLichHoc_Click);
            // 
            // btnQuanLyPhongTap
            // 
            this.btnQuanLyPhongTap.Location = new System.Drawing.Point(3, 42);
            this.btnQuanLyPhongTap.Name = "btnQuanLyPhongTap";
            this.btnQuanLyPhongTap.Size = new System.Drawing.Size(100, 33);
            this.btnQuanLyPhongTap.TabIndex = 15;
            this.btnQuanLyPhongTap.Text = "Phòng tập";
            this.btnQuanLyPhongTap.UseVisualStyleBackColor = true;
            this.btnQuanLyPhongTap.Click += new System.EventHandler(this.btnQuanLyPhongTap_Click);
            // 
            // btnHoSoCaNhan
            // 
            this.btnHoSoCaNhan.Location = new System.Drawing.Point(3, 81);
            this.btnHoSoCaNhan.Name = "btnHoSoCaNhan";
            this.btnHoSoCaNhan.Size = new System.Drawing.Size(100, 33);
            this.btnHoSoCaNhan.TabIndex = 13;
            this.btnHoSoCaNhan.Text = "Hồ sơ";
            this.btnHoSoCaNhan.UseVisualStyleBackColor = true;
            this.btnHoSoCaNhan.Click += new System.EventHandler(this.btnHoSoCaNhan_Click);
            // 
            // btnDangXuat
            // 
            this.btnDangXuat.Location = new System.Drawing.Point(3, 120);
            this.btnDangXuat.Name = "btnDangXuat";
            this.btnDangXuat.Size = new System.Drawing.Size(100, 33);
            this.btnDangXuat.TabIndex = 12;
            this.btnDangXuat.Text = "Đăng xuất";
            this.btnDangXuat.UseVisualStyleBackColor = true;
            this.btnDangXuat.Click += new System.EventHandler(this.btnDangXuat_Click);
            // 
            // flowMenuMain
            // 
            this.flowMenuMain.AutoScroll = true;
            this.flowMenuMain.Controls.Add(this.btnQuanTriHeThong);
            this.flowMenuMain.Controls.Add(this.btnChamCong);
            this.flowMenuMain.Controls.Add(this.btnThongKeDoanhThu);
            this.flowMenuMain.Controls.Add(this.btnQuanLySanPham);
            this.flowMenuMain.Controls.Add(this.btnQuanLyHoiVien);
            this.flowMenuMain.Controls.Add(this.btnQuanLyGoiTap);
            this.flowMenuMain.Controls.Add(this.btnQuanLyLopHoc);
            this.flowMenuMain.Controls.Add(this.btnBanVeThuNgan);
            this.flowMenuMain.Controls.Add(this.btnTinNhan);
            this.flowMenuMain.Controls.Add(this.btnDangKyDichVu);
            this.flowMenuMain.Controls.Add(this.btnLichSuGiaoDich);
            this.flowMenuMain.Controls.Add(this.btnLuongThuong);
            this.flowMenuMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowMenuMain.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowMenuMain.Location = new System.Drawing.Point(0, 90);
            this.flowMenuMain.Name = "flowMenuMain";
            this.flowMenuMain.Size = new System.Drawing.Size(230, 810);
            this.flowMenuMain.TabIndex = 1;
            this.flowMenuMain.WrapContents = false;
            // 
            // btnQuanTriHeThong
            // 
            this.btnQuanTriHeThong.Location = new System.Drawing.Point(3, 3);
            this.btnQuanTriHeThong.Name = "btnQuanTriHeThong";
            this.btnQuanTriHeThong.Size = new System.Drawing.Size(100, 33);
            this.btnQuanTriHeThong.TabIndex = 1;
            this.btnQuanTriHeThong.Text = "Quản trị";
            this.btnQuanTriHeThong.UseVisualStyleBackColor = true;
            this.btnQuanTriHeThong.Click += new System.EventHandler(this.btnQuanTriHeThong_Click);
            // 
            // btnChamCong
            // 
            this.btnChamCong.Location = new System.Drawing.Point(3, 42);
            this.btnChamCong.Name = "btnChamCong";
            this.btnChamCong.Size = new System.Drawing.Size(100, 33);
            this.btnChamCong.TabIndex = 16;
            this.btnChamCong.Text = "Chấm công";
            this.btnChamCong.UseVisualStyleBackColor = true;
            this.btnChamCong.Click += new System.EventHandler(this.btnChamCong_Click);
            // 
            // btnThongKeDoanhThu
            // 
            this.btnThongKeDoanhThu.Location = new System.Drawing.Point(3, 81);
            this.btnThongKeDoanhThu.Name = "btnThongKeDoanhThu";
            this.btnThongKeDoanhThu.Size = new System.Drawing.Size(100, 33);
            this.btnThongKeDoanhThu.TabIndex = 2;
            this.btnThongKeDoanhThu.Text = "Thống kê";
            this.btnThongKeDoanhThu.UseVisualStyleBackColor = true;
            this.btnThongKeDoanhThu.Click += new System.EventHandler(this.btnThongKeDoanhThu_Click);
            // 
            // btnQuanLySanPham
            // 
            this.btnQuanLySanPham.Location = new System.Drawing.Point(3, 120);
            this.btnQuanLySanPham.Name = "btnQuanLySanPham";
            this.btnQuanLySanPham.Size = new System.Drawing.Size(100, 33);
            this.btnQuanLySanPham.TabIndex = 17;
            this.btnQuanLySanPham.Text = "Sản phẩm";
            this.btnQuanLySanPham.UseVisualStyleBackColor = true;
            this.btnQuanLySanPham.Click += new System.EventHandler(this.btnQuanLySanPham_Click);
            // 
            // btnQuanLyHoiVien
            // 
            this.btnQuanLyHoiVien.Location = new System.Drawing.Point(3, 159);
            this.btnQuanLyHoiVien.Name = "btnQuanLyHoiVien";
            this.btnQuanLyHoiVien.Size = new System.Drawing.Size(100, 33);
            this.btnQuanLyHoiVien.TabIndex = 3;
            this.btnQuanLyHoiVien.Text = "Hội viên";
            this.btnQuanLyHoiVien.UseVisualStyleBackColor = true;
            this.btnQuanLyHoiVien.Click += new System.EventHandler(this.btnQuanLyHoiVien_Click);
            // 
            // btnQuanLyGoiTap
            // 
            this.btnQuanLyGoiTap.Location = new System.Drawing.Point(3, 198);
            this.btnQuanLyGoiTap.Name = "btnQuanLyGoiTap";
            this.btnQuanLyGoiTap.Size = new System.Drawing.Size(100, 33);
            this.btnQuanLyGoiTap.TabIndex = 4;
            this.btnQuanLyGoiTap.Text = "Gói tập";
            this.btnQuanLyGoiTap.UseVisualStyleBackColor = true;
            this.btnQuanLyGoiTap.Click += new System.EventHandler(this.btnQuanLyGoiTap_Click);
            // 
            // btnQuanLyLopHoc
            // 
            this.btnQuanLyLopHoc.Location = new System.Drawing.Point(3, 237);
            this.btnQuanLyLopHoc.Name = "btnQuanLyLopHoc";
            this.btnQuanLyLopHoc.Size = new System.Drawing.Size(100, 33);
            this.btnQuanLyLopHoc.TabIndex = 14;
            this.btnQuanLyLopHoc.Text = "Lớp học";
            this.btnQuanLyLopHoc.UseVisualStyleBackColor = true;
            this.btnQuanLyLopHoc.Click += new System.EventHandler(this.btnQuanLyLopHoc_Click);
            // 
            // btnBanVeThuNgan
            // 
            this.btnBanVeThuNgan.Location = new System.Drawing.Point(3, 276);
            this.btnBanVeThuNgan.Name = "btnBanVeThuNgan";
            this.btnBanVeThuNgan.Size = new System.Drawing.Size(100, 33);
            this.btnBanVeThuNgan.TabIndex = 5;
            this.btnBanVeThuNgan.Text = "Bán vé";
            this.btnBanVeThuNgan.UseVisualStyleBackColor = true;
            this.btnBanVeThuNgan.Click += new System.EventHandler(this.btnBanVeThuNgan_Click);
            // 
            // btnTinNhan
            // 
            this.btnTinNhan.Location = new System.Drawing.Point(3, 315);
            this.btnTinNhan.Name = "btnTinNhan";
            this.btnTinNhan.Size = new System.Drawing.Size(100, 33);
            this.btnTinNhan.TabIndex = 6;
            this.btnTinNhan.Text = "Tin nhắn";
            this.btnTinNhan.UseVisualStyleBackColor = true;
            this.btnTinNhan.Click += new System.EventHandler(this.btnTinNhan_Click);
            // 
            // btnDangKyDichVu
            // 
            this.btnDangKyDichVu.Location = new System.Drawing.Point(3, 354);
            this.btnDangKyDichVu.Name = "btnDangKyDichVu";
            this.btnDangKyDichVu.Size = new System.Drawing.Size(114, 33);
            this.btnDangKyDichVu.TabIndex = 7;
            this.btnDangKyDichVu.Text = "Đăng ký dịch vụ";
            this.btnDangKyDichVu.UseVisualStyleBackColor = true;
            this.btnDangKyDichVu.Click += new System.EventHandler(this.btnDangKyDichVu_Click);
            // 
            // btnLichSuGiaoDich
            // 
            this.btnLichSuGiaoDich.Location = new System.Drawing.Point(3, 393);
            this.btnLichSuGiaoDich.Name = "btnLichSuGiaoDich";
            this.btnLichSuGiaoDich.Size = new System.Drawing.Size(114, 33);
            this.btnLichSuGiaoDich.TabIndex = 8;
            this.btnLichSuGiaoDich.Text = "Lịch sử giao dịch";
            this.btnLichSuGiaoDich.UseVisualStyleBackColor = true;
            this.btnLichSuGiaoDich.Click += new System.EventHandler(this.btnLichSuGiaoDich_Click);
            // 
            // btnLuongThuong
            // 
            this.btnLuongThuong.Location = new System.Drawing.Point(3, 432);
            this.btnLuongThuong.Name = "btnLuongThuong";
            this.btnLuongThuong.Size = new System.Drawing.Size(100, 33);
            this.btnLuongThuong.TabIndex = 10;
            this.btnLuongThuong.Text = "Lương thưởng";
            this.btnLuongThuong.UseVisualStyleBackColor = true;
            this.btnLuongThuong.Click += new System.EventHandler(this.btnLuongThuong_Click);
            // 
            // panelMenuTop
            // 
            this.panelMenuTop.BackColor = System.Drawing.Color.FromArgb(17, 26, 38);
            this.panelMenuTop.Controls.Add(this.lblBrand);
            this.panelMenuTop.Controls.Add(this.lblXinChao);
            this.panelMenuTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelMenuTop.Location = new System.Drawing.Point(0, 0);
            this.panelMenuTop.Name = "panelMenuTop";
            this.panelMenuTop.Size = new System.Drawing.Size(230, 90);
            this.panelMenuTop.TabIndex = 0;
            // 
            // lblBrand
            // 
            this.lblBrand.AutoSize = true;
            this.lblBrand.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblBrand.ForeColor = System.Drawing.Color.White;
            this.lblBrand.Location = new System.Drawing.Point(20, 13);
            this.lblBrand.Name = "lblBrand";
            this.lblBrand.Size = new System.Drawing.Size(98, 28);
            this.lblBrand.TabIndex = 1;
            this.lblBrand.Text = "SPORTIFY";
            // 
            // lblXinChao
            // 
            this.lblXinChao.AutoSize = true;
            this.lblXinChao.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblXinChao.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblXinChao.Location = new System.Drawing.Point(21, 49);
            this.lblXinChao.Name = "lblXinChao";
            this.lblXinChao.Size = new System.Drawing.Size(63, 20);
            this.lblXinChao.TabIndex = 0;
            this.lblXinChao.Text = "Xin chào";
            this.lblXinChao.Click += new System.EventHandler(this.lblXinChao_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.pnlNoiDungChinh);
            this.pnlMain.Controls.Add(this.pnlHeader);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(230, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1170, 900);
            this.pnlMain.TabIndex = 1;
            // 
            // pnlNoiDungChinh
            // 
            this.pnlNoiDungChinh.BackColor = System.Drawing.Color.FromArgb(238, 242, 247);
            this.pnlNoiDungChinh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlNoiDungChinh.Location = new System.Drawing.Point(0, 56);
            this.pnlNoiDungChinh.Name = "pnlNoiDungChinh";
            this.pnlNoiDungChinh.Size = new System.Drawing.Size(1170, 844);
            this.pnlNoiDungChinh.TabIndex = 1;
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.Controls.Add(this.lblModuleTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1170, 56);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblModuleTitle
            // 
            this.lblModuleTitle.AutoSize = true;
            this.lblModuleTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lblModuleTitle.ForeColor = System.Drawing.Color.FromArgb(44, 62, 80);
            this.lblModuleTitle.Location = new System.Drawing.Point(18, 14);
            this.lblModuleTitle.Name = "lblModuleTitle";
            this.lblModuleTitle.Size = new System.Drawing.Size(98, 28);
            this.lblModuleTitle.TabIndex = 0;
            this.lblModuleTitle.Text = "Tổng quan";
            // 
            // ucTrangChu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlThanhMenu);
            this.Name = "ucTrangChu";
            this.Size = new System.Drawing.Size(1400, 900);
            this.Load += new System.EventHandler(this.ucTrangChu_Load);
            this.pnlThanhMenu.ResumeLayout(false);
            this.flowMenuBottom.ResumeLayout(false);
            this.flowMenuMain.ResumeLayout(false);
            this.panelMenuTop.ResumeLayout(false);
            this.panelMenuTop.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlThanhMenu;
        private System.Windows.Forms.Label lblXinChao;
        private System.Windows.Forms.Button btnTinNhan;
        private System.Windows.Forms.Button btnBanVeThuNgan;
        private System.Windows.Forms.Button btnQuanLyGoiTap;
        private System.Windows.Forms.Button btnQuanLyHoiVien;
        private System.Windows.Forms.Button btnThongKeDoanhThu;
        private System.Windows.Forms.Button btnQuanTriHeThong;
        private System.Windows.Forms.Button btnDangXuat;
        private System.Windows.Forms.Button btnLichHoc;
        private System.Windows.Forms.Button btnLuongThuong;
        private System.Windows.Forms.Button btnLichSuGiaoDich;
        private System.Windows.Forms.Button btnDangKyDichVu;
        private System.Windows.Forms.Panel pnlNoiDungChinh;
        private System.Windows.Forms.Button btnHoSoCaNhan;
        private System.Windows.Forms.Button btnQuanLyLopHoc;
        private System.Windows.Forms.Button btnQuanLyPhongTap;
        private System.Windows.Forms.Button btnChamCong;
        private System.Windows.Forms.Button btnQuanLySanPham;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblModuleTitle;
        private System.Windows.Forms.Panel panelMenuTop;
        private System.Windows.Forms.Label lblBrand;
        private System.Windows.Forms.FlowLayoutPanel flowMenuMain;
        private System.Windows.Forms.FlowLayoutPanel flowMenuBottom;
    }
}
