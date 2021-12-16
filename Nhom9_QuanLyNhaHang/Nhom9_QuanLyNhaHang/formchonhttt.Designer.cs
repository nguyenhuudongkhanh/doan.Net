namespace Nhom9_QuanLyNhaHang
{
    partial class formchonhttt
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
            this.label2.Location = new System.Drawing.Point(-2, 178);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(996, 71);
            this.label2.TabIndex = 13;
            this.label2.Text = "CHỌN MÃ THANH TOÁN ĐỂ XEM BÁO CÁO CỦA TỪNG LOẠI THANH TOÁN  NHÉ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnxem
            // 
            this.btnxem.Location = new System.Drawing.Point(403, 293);
            this.btnxem.Name = "btnxem";
            this.btnxem.Size = new System.Drawing.Size(121, 67);
            this.btnxem.TabIndex = 12;
            this.btnxem.Text = "xem báo cáo ";
            this.btnxem.UseVisualStyleBackColor = true;
            this.btnxem.Click += new System.EventHandler(this.btnxem_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(270, 270);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 23);
            this.label1.TabIndex = 11;
            this.label1.Text = "Tìm Theo Mã Loại";
            // 
            // txtma
            // 
            this.txtma.Location = new System.Drawing.Point(403, 267);
            this.txtma.Name = "txtma";
            this.txtma.Size = new System.Drawing.Size(121, 20);
            this.txtma.TabIndex = 10;
            // 
            // formchonhttt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(992, 538);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnxem);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtma);
            this.Name = "formchonhttt";
            this.Text = "formchonhttt";
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