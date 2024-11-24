namespace BaiTapLon.View.NangCao
{
    partial class NC9
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.cboHocKy = new System.Windows.Forms.ComboBox();
            this.cboSinhVien = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.MaMon = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenMon = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DiemThuongKy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DiemCuoiKy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DiemTrungBinh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DiemHe4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DiemChu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.XepLoai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(315, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(566, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "DANH SÁCH ĐIỂM CỦA SINH VIÊN THEO HỌC KỲ";
            // 
            // cboHocKy
            // 
            this.cboHocKy.FormattingEnabled = true;
            this.cboHocKy.Location = new System.Drawing.Point(463, 91);
            this.cboHocKy.Name = "cboHocKy";
            this.cboHocKy.Size = new System.Drawing.Size(318, 26);
            this.cboHocKy.TabIndex = 1;
            this.cboHocKy.SelectedIndexChanged += new System.EventHandler(this.cboHocKy_SelectedIndexChanged);
            // 
            // cboSinhVien
            // 
            this.cboSinhVien.FormattingEnabled = true;
            this.cboSinhVien.Location = new System.Drawing.Point(463, 158);
            this.cboSinhVien.Name = "cboSinhVien";
            this.cboSinhVien.Size = new System.Drawing.Size(318, 26);
            this.cboSinhVien.TabIndex = 2;
            this.cboSinhVien.SelectedIndexChanged += new System.EventHandler(this.cboSinhVien_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(317, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 18);
            this.label2.TabIndex = 3;
            this.label2.Text = "Lựa chọn: ";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MaMon,
            this.TenMon,
            this.DiemThuongKy,
            this.DiemCuoiKy,
            this.DiemTrungBinh,
            this.DiemHe4,
            this.DiemChu,
            this.XepLoai});
            this.dataGridView1.Location = new System.Drawing.Point(12, 238);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1254, 314);
            this.dataGridView1.TabIndex = 4;
            // 
            // MaMon
            // 
            this.MaMon.DataPropertyName = "MaMon";
            this.MaMon.HeaderText = "Mã Môn";
            this.MaMon.MinimumWidth = 6;
            this.MaMon.Name = "MaMon";
            this.MaMon.Width = 125;
            // 
            // TenMon
            // 
            this.TenMon.DataPropertyName = "TenMon";
            this.TenMon.HeaderText = "Tên Môn";
            this.TenMon.MinimumWidth = 6;
            this.TenMon.Name = "TenMon";
            this.TenMon.Width = 125;
            // 
            // DiemThuongKy
            // 
            this.DiemThuongKy.DataPropertyName = "DiemThuongKy";
            this.DiemThuongKy.HeaderText = "Điểm thường kỳ";
            this.DiemThuongKy.MinimumWidth = 6;
            this.DiemThuongKy.Name = "DiemThuongKy";
            this.DiemThuongKy.Width = 150;
            // 
            // DiemCuoiKy
            // 
            this.DiemCuoiKy.DataPropertyName = "DiemCuoiKy";
            this.DiemCuoiKy.HeaderText = "Điểm Cuối Kỳ";
            this.DiemCuoiKy.MinimumWidth = 6;
            this.DiemCuoiKy.Name = "DiemCuoiKy";
            this.DiemCuoiKy.Width = 150;
            // 
            // DiemTrungBinh
            // 
            this.DiemTrungBinh.DataPropertyName = "DiemTrungBinh";
            this.DiemTrungBinh.HeaderText = "Điểm Trung Bình Học Kỳ (Hệ 10)";
            this.DiemTrungBinh.MinimumWidth = 6;
            this.DiemTrungBinh.Name = "DiemTrungBinh";
            this.DiemTrungBinh.Width = 270;
            // 
            // DiemHe4
            // 
            this.DiemHe4.DataPropertyName = "DiemHe4";
            this.DiemHe4.HeaderText = "Điểm Hệ 4";
            this.DiemHe4.MinimumWidth = 6;
            this.DiemHe4.Name = "DiemHe4";
            this.DiemHe4.Width = 125;
            // 
            // DiemChu
            // 
            this.DiemChu.DataPropertyName = "DiemChu";
            this.DiemChu.HeaderText = "Điểm Chữ";
            this.DiemChu.MinimumWidth = 6;
            this.DiemChu.Name = "DiemChu";
            this.DiemChu.Width = 125;
            // 
            // XepLoai
            // 
            this.XepLoai.DataPropertyName = "XepLoai";
            this.XepLoai.HeaderText = "Xếp Loại";
            this.XepLoai.MinimumWidth = 6;
            this.XepLoai.Name = "XepLoai";
            this.XepLoai.Width = 125;
            // 
            this.dataGridView1.Size = new System.Drawing.Size(1279, 441);
            this.dataGridView1.TabIndex = 4;
            // 
            // NC9
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1271, 573);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboSinhVien);
            this.Controls.Add(this.cboHocKy);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "NC9";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NC9";
            this.Load += new System.EventHandler(this.NC9_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboHocKy;
        private System.Windows.Forms.ComboBox cboSinhVien;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaMon;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenMon;
        private System.Windows.Forms.DataGridViewTextBoxColumn DiemThuongKy;
        private System.Windows.Forms.DataGridViewTextBoxColumn DiemCuoiKy;
        private System.Windows.Forms.DataGridViewTextBoxColumn DiemTrungBinh;
        private System.Windows.Forms.DataGridViewTextBoxColumn DiemHe4;
        private System.Windows.Forms.DataGridViewTextBoxColumn DiemChu;
        private System.Windows.Forms.DataGridViewTextBoxColumn XepLoai;
    }
}