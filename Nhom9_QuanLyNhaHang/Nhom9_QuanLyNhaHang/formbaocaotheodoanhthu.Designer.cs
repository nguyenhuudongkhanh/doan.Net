namespace Nhom9_QuanLyNhaHang
{
    partial class formbaocaotheodoanhthu
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
            this.btnxem = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtma = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Rage Italic", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(57, 168);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(882, 47);
            this.label2.TabIndex = 9;
            this.label2.Text = "CHỌN MÃ HÓA ĐƠN ĐỂ XEM BÁO CÁO CỦA HÓA ĐƠN  NHÉ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnxem
            // 
            this.btnxem.Location = new System.Drawing.Point(462, 283);
            this.btnxem.Name = "btnxem";
            this.btnxem.Size = new System.Drawing.Size(121, 67);
            this.btnxem.TabIndex = 8;
            this.btnxem.Text = "xem báo cáo ";
            this.btnxem.UseVisualStyleBackColor = true;
            this.btnxem.Click += new System.EventHandler(this.btnxem_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(329, 260);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 23);
            this.label1.TabIndex = 7;
            this.label1.Text = "Tìm Theo Mã ";
            // 
            // txtma
            // 
            this.txtma.Location = new System.Drawing.Point(462, 257);
            this.txtma.Name = "txtma";
            this.txtma.Size = new System.Drawing.Size(121, 20);
            this.txtma.TabIndex = 6;
            // 
            // formbaocaotheodoanhthu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(996, 519);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnxem);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtma);
            this.Name = "formbaocaotheodoanhthu";
            this.Text = "formbaocaotheodoanhthu";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnxem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtma;
    }
}