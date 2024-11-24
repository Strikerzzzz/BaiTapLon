namespace BaiTapLon.View.TimKiem
{
    partial class TKHocKy
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cboLuaChon = new System.Windows.Forms.ComboBox();
            this.txtGiaTri = new System.Windows.Forms.TextBox();
            this.btnTK = new System.Windows.Forms.Button();
            this.btnThoat = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.IDHocKy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenHocKy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nam = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.Location = new System.Drawing.Point(218, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(231, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "TÌM KIẾM HỌC KỲ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.Location = new System.Drawing.Point(100, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "Lựa chọn giá trị :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label3.Location = new System.Drawing.Point(100, 136);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 18);
            this.label3.TabIndex = 2;
            this.label3.Text = "Giá trị :";
            // 
            // cboLuaChon
            // 
            this.cboLuaChon.FormattingEnabled = true;
            this.cboLuaChon.Location = new System.Drawing.Point(243, 72);
            this.cboLuaChon.Name = "cboLuaChon";
            this.cboLuaChon.Size = new System.Drawing.Size(228, 24);
            this.cboLuaChon.TabIndex = 3;
            // 
            // txtGiaTri
            // 
            this.txtGiaTri.Location = new System.Drawing.Point(243, 132);
            this.txtGiaTri.Name = "txtGiaTri";
            this.txtGiaTri.Size = new System.Drawing.Size(228, 22);
            this.txtGiaTri.TabIndex = 4;
            // 
            // btnTK
            // 
            this.btnTK.Location = new System.Drawing.Point(177, 177);
            this.btnTK.Name = "btnTK";
            this.btnTK.Size = new System.Drawing.Size(87, 31);
            this.btnTK.TabIndex = 6;
            this.btnTK.Text = "Tìm Kiếm";
            this.btnTK.UseVisualStyleBackColor = true;
            this.btnTK.Click += new System.EventHandler(this.btnTK_Click);
            // 
            // btnThoat
            // 
            this.btnThoat.Location = new System.Drawing.Point(416, 177);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(87, 31);
            this.btnThoat.TabIndex = 7;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IDHocKy,
            this.TenHocKy,
            this.Nam});
            this.dataGridView1.Location = new System.Drawing.Point(12, 243);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(655, 217);
            this.dataGridView1.TabIndex = 8;
            // 
            // IDHocKy
            // 
            this.IDHocKy.DataPropertyName = "IDHocKy";
            this.IDHocKy.HeaderText = "Mã học kỳ";
            this.IDHocKy.MinimumWidth = 6;
            this.IDHocKy.Name = "IDHocKy";
            this.IDHocKy.Width = 125;
            // 
            // TenHocKy
            // 
            this.TenHocKy.DataPropertyName = "TenHocKy";
            this.TenHocKy.HeaderText = "Tên Học Kỳ";
            this.TenHocKy.MinimumWidth = 6;
            this.TenHocKy.Name = "TenHocKy";
            this.TenHocKy.Width = 225;
            // 
            // Nam
            // 
            this.Nam.DataPropertyName = "Nam";
            this.Nam.HeaderText = "Năm";
            this.Nam.MinimumWidth = 6;
            this.Nam.Name = "Nam";
            this.Nam.Width = 125;
            // 
            // TKHocKy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(679, 486);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnTK);
            this.Controls.Add(this.txtGiaTri);
            this.Controls.Add(this.cboLuaChon);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "TKHocKy";
            this.Text = "TKHocKy";
            this.Load += new System.EventHandler(this.TKHocKy_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboLuaChon;
        private System.Windows.Forms.TextBox txtGiaTri;
        private System.Windows.Forms.Button btnTK;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDHocKy;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenHocKy;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nam;
    }
}