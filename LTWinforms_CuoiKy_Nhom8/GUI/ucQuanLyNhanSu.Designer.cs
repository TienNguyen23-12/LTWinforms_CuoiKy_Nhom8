namespace LTWinforms_CuoiKy_Nhom8.GUI
{
    partial class ucQuanLyNhanSu
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
            this.dgvNhanSu = new System.Windows.Forms.DataGridView();
            this.lblTenNhanSu = new System.Windows.Forms.Label();
            this.radCoMat = new System.Windows.Forms.RadioButton();
            this.radVangMat = new System.Windows.Forms.RadioButton();
            this.btnChamCong = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTienPhat = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtLyDoPhat = new System.Windows.Forms.RichTextBox();
            this.btnPhat = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNhanSu)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvNhanSu
            // 
            this.dgvNhanSu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNhanSu.Location = new System.Drawing.Point(30, 44);
            this.dgvNhanSu.Name = "dgvNhanSu";
            this.dgvNhanSu.RowHeadersWidth = 51;
            this.dgvNhanSu.RowTemplate.Height = 24;
            this.dgvNhanSu.Size = new System.Drawing.Size(240, 467);
            this.dgvNhanSu.TabIndex = 0;
            this.dgvNhanSu.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvNhanSu_CellClick);
            // 
            // lblTenNhanSu
            // 
            this.lblTenNhanSu.AutoSize = true;
            this.lblTenNhanSu.Location = new System.Drawing.Point(30, 22);
            this.lblTenNhanSu.Name = "lblTenNhanSu";
            this.lblTenNhanSu.Size = new System.Drawing.Size(44, 16);
            this.lblTenNhanSu.TabIndex = 1;
            this.lblTenNhanSu.Text = "label1";
            // 
            // radCoMat
            // 
            this.radCoMat.AutoSize = true;
            this.radCoMat.Location = new System.Drawing.Point(324, 44);
            this.radCoMat.Name = "radCoMat";
            this.radCoMat.Size = new System.Drawing.Size(70, 20);
            this.radCoMat.TabIndex = 2;
            this.radCoMat.TabStop = true;
            this.radCoMat.Text = "Có mặt";
            this.radCoMat.UseVisualStyleBackColor = true;
            // 
            // radVangMat
            // 
            this.radVangMat.AutoSize = true;
            this.radVangMat.Location = new System.Drawing.Point(506, 44);
            this.radVangMat.Name = "radVangMat";
            this.radVangMat.Size = new System.Drawing.Size(85, 20);
            this.radVangMat.TabIndex = 3;
            this.radVangMat.TabStop = true;
            this.radVangMat.Text = "Vắng mặt";
            this.radVangMat.UseVisualStyleBackColor = true;
            // 
            // btnChamCong
            // 
            this.btnChamCong.Location = new System.Drawing.Point(397, 86);
            this.btnChamCong.Name = "btnChamCong";
            this.btnChamCong.Size = new System.Drawing.Size(104, 23);
            this.btnChamCong.TabIndex = 4;
            this.btnChamCong.Text = "Chấm công";
            this.btnChamCong.UseVisualStyleBackColor = true;
            this.btnChamCong.Click += new System.EventHandler(this.btnChamCong_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(324, 153);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "Tiền phạt";
            // 
            // txtTienPhat
            // 
            this.txtTienPhat.Location = new System.Drawing.Point(397, 150);
            this.txtTienPhat.Name = "txtTienPhat";
            this.txtTienPhat.Size = new System.Drawing.Size(294, 22);
            this.txtTienPhat.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(324, 193);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "Lý do";
            // 
            // txtLyDoPhat
            // 
            this.txtLyDoPhat.Location = new System.Drawing.Point(401, 193);
            this.txtLyDoPhat.Name = "txtLyDoPhat";
            this.txtLyDoPhat.Size = new System.Drawing.Size(290, 96);
            this.txtLyDoPhat.TabIndex = 8;
            this.txtLyDoPhat.Text = "";
            // 
            // btnPhat
            // 
            this.btnPhat.Location = new System.Drawing.Point(397, 316);
            this.btnPhat.Name = "btnPhat";
            this.btnPhat.Size = new System.Drawing.Size(104, 23);
            this.btnPhat.TabIndex = 9;
            this.btnPhat.Text = "Phạt";
            this.btnPhat.UseVisualStyleBackColor = true;
            this.btnPhat.Click += new System.EventHandler(this.btnPhat_Click);
            // 
            // ucQuanLyNhanSu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnPhat);
            this.Controls.Add(this.txtLyDoPhat);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtTienPhat);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnChamCong);
            this.Controls.Add(this.radVangMat);
            this.Controls.Add(this.radCoMat);
            this.Controls.Add(this.lblTenNhanSu);
            this.Controls.Add(this.dgvNhanSu);
            this.Name = "ucQuanLyNhanSu";
            this.Size = new System.Drawing.Size(1000, 564);
            this.Load += new System.EventHandler(this.cQuanLyNhanSu_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNhanSu)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvNhanSu;
        private System.Windows.Forms.Label lblTenNhanSu;
        private System.Windows.Forms.RadioButton radCoMat;
        private System.Windows.Forms.RadioButton radVangMat;
        private System.Windows.Forms.Button btnChamCong;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTienPhat;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox txtLyDoPhat;
        private System.Windows.Forms.Button btnPhat;
    }
}
