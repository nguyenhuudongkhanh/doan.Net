﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nhom9_QuanLyNhaHang
{
    public partial class formbaocaotheodoanhthu : Form
    {
        public formbaocaotheodoanhthu()
        {
            InitializeComponent();
        }

        private void btnxem_Click(object sender, EventArgs e)
        {
            if ((string.IsNullOrEmpty(txtma.Text)))
            {
                MessageBox.Show("Vui lòng điền mã để xem báo cáo ", "err", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);

            }
            else
            {
                truyendulieu.maloaihoadon = txtma.Text;
                FormBaoCaoHoaDon fr = new FormBaoCaoHoaDon();
                fr.ShowDialog();
            }
        }
    }
}
