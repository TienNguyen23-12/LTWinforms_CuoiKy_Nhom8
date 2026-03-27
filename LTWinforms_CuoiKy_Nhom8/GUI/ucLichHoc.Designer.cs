namespace LTWinforms_CuoiKy_Nhom8.GUI
{
    partial class ucLichHoc
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
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.dgvLich = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLich)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Location = new System.Drawing.Point(424, 33);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(44, 16);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "label1";
            // 
            // dgvLich
            // 
            this.dgvLich.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLich.Location = new System.Drawing.Point(41, 96);
            this.dgvLich.Name = "dgvLich";
            this.dgvLich.RowHeadersWidth = 51;
            this.dgvLich.RowTemplate.Height = 24;
            this.dgvLich.Size = new System.Drawing.Size(902, 398);
            this.dgvLich.TabIndex = 1;
            // 
            // ucLichHoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvLich);
            this.Controls.Add(this.lblTieuDe);
            this.Name = "ucLichHoc";
            this.Size = new System.Drawing.Size(989, 558);
            this.Load += new System.EventHandler(this.ucLichHoc_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLich)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.DataGridView dgvLich;
    }
}
