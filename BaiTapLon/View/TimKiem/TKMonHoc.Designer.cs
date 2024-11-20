namespace BaiTapLon.View.TimKiem
{
    partial class TKMonHoc
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
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox = new System.Windows.Forms.ComboBox();
            this.btnThoat = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.MaMon = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenMon = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SoTinChi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LoaiMon = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TongSoBuoiHoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtTK = new System.Windows.Forms.TextBox();
            this.btnTK = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(86, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 18);
            this.label2.TabIndex = 30;
            this.label2.Text = "Lựa chọn tiêu chí";
            // 
            // comboBox
            // 
            this.comboBox.FormattingEnabled = true;
            this.comboBox.Location = new System.Drawing.Point(211, 41);
            this.comboBox.Name = "comboBox";
            this.comboBox.Size = new System.Drawing.Size(265, 26);
            this.comboBox.TabIndex = 29;
            // 
            // btnThoat
            // 
            this.btnThoat.Location = new System.Drawing.Point(584, 82);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(87, 42);
            this.btnThoat.TabIndex = 28;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgv);
            this.groupBox2.Location = new System.Drawing.Point(14, 144);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(838, 219);
            this.groupBox2.TabIndex = 27;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Hiển thị";
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MaMon,
            this.TenMon,
            this.SoTinChi,
            this.LoaiMon,
            this.TongSoBuoiHoc});
            this.dgv.Location = new System.Drawing.Point(6, 23);
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersVisible = false;
            this.dgv.RowHeadersWidth = 51;
            this.dgv.RowTemplate.Height = 24;
            this.dgv.Size = new System.Drawing.Size(817, 180);
            this.dgv.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.Location = new System.Drawing.Point(219, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(257, 29);
            this.label1.TabIndex = 26;
            this.label1.Text = "TÌM KIẾM MÔN HỌC";
            // 
            // MaMon
            // 
            this.MaMon.DataPropertyName = "MaMon";
            this.MaMon.HeaderText = "Mã môn";
            this.MaMon.MinimumWidth = 6;
            this.MaMon.Name = "MaMon";
            this.MaMon.Width = 125;
            // 
            // TenMon
            // 
            this.TenMon.DataPropertyName = "TenMon";
            this.TenMon.HeaderText = "Tên môn";
            this.TenMon.MinimumWidth = 6;
            this.TenMon.Name = "TenMon";
            this.TenMon.Width = 225;
            // 
            // SoTinChi
            // 
            this.SoTinChi.DataPropertyName = "SoTinChi";
            this.SoTinChi.HeaderText = "Số tín chỉ";
            this.SoTinChi.MinimumWidth = 6;
            this.SoTinChi.Name = "SoTinChi";
            this.SoTinChi.Width = 125;
            // 
            // LoaiMon
            // 
            this.LoaiMon.DataPropertyName = "LoaiMon";
            this.LoaiMon.HeaderText = "Loại môn";
            this.LoaiMon.MinimumWidth = 6;
            this.LoaiMon.Name = "LoaiMon";
            this.LoaiMon.Width = 125;
            // 
            // TongSoBuoiHoc
            // 
            this.TongSoBuoiHoc.DataPropertyName = "TongSoBuoiHoc";
            this.TongSoBuoiHoc.HeaderText = "Tổng số buổi học";
            this.TongSoBuoiHoc.MinimumWidth = 6;
            this.TongSoBuoiHoc.Name = "TongSoBuoiHoc";
            this.TongSoBuoiHoc.Width = 200;
            // 
            // txtTK
            // 
            this.txtTK.Location = new System.Drawing.Point(211, 90);
            this.txtTK.Name = "txtTK";
            this.txtTK.Size = new System.Drawing.Size(265, 24);
            this.txtTK.TabIndex = 31;
            // 
            // btnTK
            // 
            this.btnTK.Location = new System.Drawing.Point(482, 82);
            this.btnTK.Name = "btnTK";
            this.btnTK.Size = new System.Drawing.Size(96, 42);
            this.btnTK.TabIndex = 32;
            this.btnTK.Text = "Tìm kiếm";
            this.btnTK.UseVisualStyleBackColor = true;
            this.btnTK.Click += new System.EventHandler(this.btnTK_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(143, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 18);
            this.label3.TabIndex = 33;
            this.label3.Text = "Giá trị";
            // 
            // TKMonHoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(856, 379);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnTK);
            this.Controls.Add(this.txtTK);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBox);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.Name = "TKMonHoc";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TKMonHoc";
            this.Load += new System.EventHandler(this.TKMonHoc_Load);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaMon;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenMon;
        private System.Windows.Forms.DataGridViewTextBoxColumn SoTinChi;
        private System.Windows.Forms.DataGridViewTextBoxColumn LoaiMon;
        private System.Windows.Forms.DataGridViewTextBoxColumn TongSoBuoiHoc;
        private System.Windows.Forms.TextBox txtTK;
        private System.Windows.Forms.Button btnTK;
        private System.Windows.Forms.Label label3;
    }
}