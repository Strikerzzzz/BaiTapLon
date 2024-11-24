namespace BaiTapLon.View.TimKiem
{
    partial class TKDiem
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtGiaTri = new System.Windows.Forms.TextBox();
            this.cboLuaChon = new System.Windows.Forms.ComboBox();
            this.btnThoat = new System.Windows.Forms.Button();
            this.btnTK = new System.Windows.Forms.Button();
            this.IDDiem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenSinhVien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenMon = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenLoaiDiem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GiaTriDiem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LanThi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IDDiem,
            this.TenSinhVien,
            this.TenMon,
            this.TenLoaiDiem,
            this.GiaTriDiem,
            this.LanThi});
            this.dataGridView1.Location = new System.Drawing.Point(12, 220);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1014, 231);
            this.dataGridView1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.Location = new System.Drawing.Point(419, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(197, 29);
            this.label1.TabIndex = 2;
            this.label1.Text = "TÌM KIẾM ĐIỂM";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.Location = new System.Drawing.Point(207, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(135, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Lựa chọn giá trị :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label3.Location = new System.Drawing.Point(210, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Giá trị :";
            // 
            // txtGiaTri
            // 
            this.txtGiaTri.Location = new System.Drawing.Point(384, 104);
            this.txtGiaTri.Name = "txtGiaTri";
            this.txtGiaTri.Size = new System.Drawing.Size(320, 22);
            this.txtGiaTri.TabIndex = 8;
            // 
            // cboLuaChon
            // 
            this.cboLuaChon.FormattingEnabled = true;
            this.cboLuaChon.Location = new System.Drawing.Point(384, 55);
            this.cboLuaChon.Name = "cboLuaChon";
            this.cboLuaChon.Size = new System.Drawing.Size(320, 24);
            this.cboLuaChon.TabIndex = 7;
            // 
            // btnThoat
            // 
            this.btnThoat.Location = new System.Drawing.Point(684, 161);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(87, 31);
            this.btnThoat.TabIndex = 11;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // btnTK
            // 
            this.btnTK.Location = new System.Drawing.Point(282, 161);
            this.btnTK.Name = "btnTK";
            this.btnTK.Size = new System.Drawing.Size(87, 31);
            this.btnTK.TabIndex = 10;
            this.btnTK.Text = "Tìm Kiếm";
            this.btnTK.UseVisualStyleBackColor = true;
            this.btnTK.Click += new System.EventHandler(this.btnTK_Click);
            // 
            // IDDiem
            // 
            this.IDDiem.DataPropertyName = "IDDiem";
            this.IDDiem.HeaderText = "ID điểm";
            this.IDDiem.MinimumWidth = 6;
            this.IDDiem.Name = "IDDiem";
            this.IDDiem.Width = 80;
            // 
            // TenSinhVien
            // 
            this.TenSinhVien.DataPropertyName = "TenSinhVien";
            this.TenSinhVien.HeaderText = "Tên sinh viên";
            this.TenSinhVien.MinimumWidth = 6;
            this.TenSinhVien.Name = "TenSinhVien";
            this.TenSinhVien.Width = 170;
            // 
            // TenMon
            // 
            this.TenMon.DataPropertyName = "TenMon";
            this.TenMon.HeaderText = "Tên môn";
            this.TenMon.MinimumWidth = 6;
            this.TenMon.Name = "TenMon";
            this.TenMon.Width = 150;
            // 
            // TenLoaiDiem
            // 
            this.TenLoaiDiem.DataPropertyName = "TenLoaiDiem";
            this.TenLoaiDiem.HeaderText = "Tên loại điểm";
            this.TenLoaiDiem.MinimumWidth = 6;
            this.TenLoaiDiem.Name = "TenLoaiDiem";
            this.TenLoaiDiem.Width = 150;
            // 
            // GiaTriDiem
            // 
            this.GiaTriDiem.DataPropertyName = "GiaTriDiem";
            this.GiaTriDiem.HeaderText = "Giá trị điểm";
            this.GiaTriDiem.MinimumWidth = 6;
            this.GiaTriDiem.Name = "GiaTriDiem";
            this.GiaTriDiem.Width = 110;
            // 
            // LanThi
            // 
            this.LanThi.DataPropertyName = "LanThi";
            this.LanThi.HeaderText = "Lần thi";
            this.LanThi.MinimumWidth = 6;
            this.LanThi.Name = "LanThi";
            this.LanThi.Width = 80;
            // 
            // TKDiem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1044, 496);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnTK);
            this.Controls.Add(this.txtGiaTri);
            this.Controls.Add(this.cboLuaChon);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "TKDiem";
            this.Text = "TKDiem";
            this.Load += new System.EventHandler(this.TKDiem_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtGiaTri;
        private System.Windows.Forms.ComboBox cboLuaChon;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.Button btnTK;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDDiem;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenSinhVien;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenMon;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenLoaiDiem;
        private System.Windows.Forms.DataGridViewTextBoxColumn GiaTriDiem;
        private System.Windows.Forms.DataGridViewTextBoxColumn LanThi;
    }
}