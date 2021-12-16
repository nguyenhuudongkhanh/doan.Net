using System;
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
    public partial class DangNhap : Form
    {
        public DangNhap()
        {
            InitializeComponent();
            txtTaiKhoan.Text = "thungantest";
            txtMatKhau.Text = "123";
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            if (txtTaiKhoan.Text == "" || txtMatKhau.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập tài khoản hoặc mật khẩu", "Thông báo");
                return;
            }
            else
            {
                DataTable dt = ConnectDatabase.getDataTable("select * from TBL_NHANVIEN where sTenDangNhap = '" + txtTaiKhoan.Text + "' AND sMatKhau = '" + txtMatKhau.Text + "'");
                if (dt.Rows.Count > 0)
                {
                    this.Hide();
                    Form frm = new Form1(txtTaiKhoan.Text, txtMatKhau.Text);
                    frm.Show();
                }
                else
                {
                    MessageBox.Show("Đăng nhập không thành công", "Thông báo");
                    txtTaiKhoan.Clear();
                    txtMatKhau.Clear();
                    txtTaiKhoan.Focus();
                }
            }
        }

        private void DangNhap_Load(object sender, EventArgs e)
        {
            txtTaiKhoan.Focus();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Bạn có muốn thoát không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
                Application.Exit();
        }

    }
}
