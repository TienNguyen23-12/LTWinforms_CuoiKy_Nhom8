namespace LTWinforms_CuoiKy_Nhom8.GUI
{
    partial class ucDangKyDay
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
            this.dgvLopTrong = new System.Windows.Forms.DataGridView();
            this.btnDangKyDay = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLopTrong)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(326, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(338, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "DANH SÁCH LỚP HỌC ĐANG TÌM HUẤN LUYỆN VIÊN";
            // 
            // dgvLopTrong
            // 
            this.dgvLopTrong.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLopTrong.Location = new System.Drawing.Point(38, 57);
            this.dgvLopTrong.Name = "dgvLopTrong";
            this.dgvLopTrong.RowHeadersWidth = 51;
            this.dgvLopTrong.RowTemplate.Height = 24;
            this.dgvLopTrong.Size = new System.Drawing.Size(920, 355);
            this.dgvLopTrong.TabIndex = 1;
            // 
            // btnDangKyDay
            // 
            this.btnDangKyDay.Location = new System.Drawing.Point(422, 437);
            this.btnDangKyDay.Name = "btnDangKyDay";
            this.btnDangKyDay.Size = new System.Drawing.Size(104, 23);
            this.btnDangKyDay.TabIndex = 2;
            this.btnDangKyDay.Text = "Đăng ký dạy";
            this.btnDangKyDay.UseVisualStyleBackColor = true;
            this.btnDangKyDay.Click += new System.EventHandler(this.btnDangKyDay_Click);
            // 
            // ucDangKyDay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnDangKyDay);
            this.Controls.Add(this.dgvLopTrong);
            this.Controls.Add(this.label1);
            this.Name = "ucDangKyDay";
            this.Size = new System.Drawing.Size(992, 567);
            this.Load += new System.EventHandler(this.ucDangKyDay_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLopTrong)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvLopTrong;
        private System.Windows.Forms.Button btnDangKyDay;
    }
}
