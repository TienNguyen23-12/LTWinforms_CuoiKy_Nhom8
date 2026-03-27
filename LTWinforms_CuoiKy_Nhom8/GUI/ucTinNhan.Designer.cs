namespace LTWinforms_CuoiKy_Nhom8.GUI
{
    partial class ucTinNhan
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
            this.components = new System.ComponentModel.Container();
            this.dgvDanhSachLienHe = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.lbxNoiDungChat = new System.Windows.Forms.ListBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuSua = new System.Windows.Forms.ToolStripMenuItem();
            this.menuXoa = new System.Windows.Forms.ToolStripMenuItem();
            this.txtSoanTin = new System.Windows.Forms.TextBox();
            this.btnGui = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhSachLienHe)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvDanhSachLienHe
            // 
            this.dgvDanhSachLienHe.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDanhSachLienHe.Location = new System.Drawing.Point(14, 49);
            this.dgvDanhSachLienHe.Name = "dgvDanhSachLienHe";
            this.dgvDanhSachLienHe.RowHeadersWidth = 51;
            this.dgvDanhSachLienHe.RowTemplate.Height = 24;
            this.dgvDanhSachLienHe.Size = new System.Drawing.Size(114, 488);
            this.dgvDanhSachLienHe.TabIndex = 0;
            this.dgvDanhSachLienHe.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDanhSachLienHe_CellClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Tin nhắn";
            // 
            // lbxNoiDungChat
            // 
            this.lbxNoiDungChat.ContextMenuStrip = this.contextMenuStrip1;
            this.lbxNoiDungChat.FormattingEnabled = true;
            this.lbxNoiDungChat.ItemHeight = 16;
            this.lbxNoiDungChat.Location = new System.Drawing.Point(149, 49);
            this.lbxNoiDungChat.Name = "lbxNoiDungChat";
            this.lbxNoiDungChat.Size = new System.Drawing.Size(811, 388);
            this.lbxNoiDungChat.TabIndex = 2;
            this.lbxNoiDungChat.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbxNoiDungChat_MouseDown);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuSua,
            this.menuXoa});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(211, 80);
            // 
            // menuSua
            // 
            this.menuSua.Name = "menuSua";
            this.menuSua.Size = new System.Drawing.Size(210, 24);
            this.menuSua.Text = "Sửa tin nhắn";
            this.menuSua.Click += new System.EventHandler(this.menuSua_Click);
            // 
            // menuXoa
            // 
            this.menuXoa.Name = "menuXoa";
            this.menuXoa.Size = new System.Drawing.Size(210, 24);
            this.menuXoa.Text = "Xóa tin nhắn";
            this.menuXoa.Click += new System.EventHandler(this.menuXoa_Click);
            // 
            // txtSoanTin
            // 
            this.txtSoanTin.Location = new System.Drawing.Point(149, 456);
            this.txtSoanTin.Name = "txtSoanTin";
            this.txtSoanTin.Size = new System.Drawing.Size(728, 22);
            this.txtSoanTin.TabIndex = 3;
            this.txtSoanTin.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSoanTin_KeyDown);
            // 
            // btnGui
            // 
            this.btnGui.Location = new System.Drawing.Point(885, 455);
            this.btnGui.Name = "btnGui";
            this.btnGui.Size = new System.Drawing.Size(75, 23);
            this.btnGui.TabIndex = 4;
            this.btnGui.Text = "Gửi";
            this.btnGui.UseVisualStyleBackColor = true;
            this.btnGui.Click += new System.EventHandler(this.btnGui_Click);
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.Location = new System.Drawing.Point(149, 493);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(75, 23);
            this.btnLamMoi.TabIndex = 5;
            this.btnLamMoi.Text = "Tải lại";
            this.btnLamMoi.UseVisualStyleBackColor = true;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // ucTinNhan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnLamMoi);
            this.Controls.Add(this.btnGui);
            this.Controls.Add(this.txtSoanTin);
            this.Controls.Add(this.lbxNoiDungChat);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvDanhSachLienHe);
            this.Name = "ucTinNhan";
            this.Size = new System.Drawing.Size(1001, 553);
            this.Load += new System.EventHandler(this.ucTinNhan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhSachLienHe)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDanhSachLienHe;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lbxNoiDungChat;
        private System.Windows.Forms.TextBox txtSoanTin;
        private System.Windows.Forms.Button btnGui;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuSua;
        private System.Windows.Forms.ToolStripMenuItem menuXoa;
    }
}
