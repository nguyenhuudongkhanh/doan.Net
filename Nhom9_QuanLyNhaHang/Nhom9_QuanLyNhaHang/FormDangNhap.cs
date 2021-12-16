using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nhom9_QuanLyNhaHang
{
    public partial class FormDangNhap : Form
    {
        private SqlConnection Con = null;
        public FormDangNhap()
        {
            InitializeComponent();
        }

        private void btndangnhap_Click(object sender, EventArgs e)
        {
            //Con = new SqlConnection();
            //Con.ConnectionString = "Data Source=DESKTOP-95N5PBV;database=QL_NHAHANG_2; integrated security=true;"; ;
            //Con.Open();
            //string select = "Select * From TBL_NHANVIEN where sTenNhanVien='" + txttennhanvien.Text + "' and  sTenDangNhap ='" + txtus.Text + "' and sMatKhau='" + txtmk.Text + "' and FK_sChucVuID='THUNGAN'";
            //SqlCommand cmd = new SqlCommand(select, Con);
            //SqlDataReader reader = cmd.ExecuteReader();
            //if ((string.IsNullOrEmpty(txttennhanvien.Text)) || (string.IsNullOrEmpty(txtus.Text)) || (string.IsNullOrEmpty(txtmk.Text)))
            //{
            //    MessageBox.Show("Vui lòng điền đủ thông tin", "err", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);

            //}
            //else if (reader.Read())
            //{
            //    reader.Read();
            //    MessageBox.Show("Đăng nhập vào hệ thống (Quyền Admin) !", "Thông báo !");
            //    TrangChu.quyen = "THUNGAN";
            //    this.Hide();
            //    //this.Close();
            //}
            //else
            //{
            //    MessageBox.Show("Đăng nhập vào hệ thống (Quyền user) !", "Thông báo !");
            //    TrangChu.quyen = "TAPVU";
            //    this.Hide();
            //    //this.Close();
            //}
            //TrangChu frm = new TrangChu();
            ////frm.Show();
            //frm.ShowDialog();
            //cmd.Dispose();
            //reader.Close();
            //reader.Dispose();

            Con = new SqlConnection();
            Con.ConnectionString = "Data Source=VanHien;database=QL_NHAHANG_3; integrated security=true;"; ;
            Con.Open();
            string select = "Select * From TBL_NHANVIEN where  sTenDangNhap ='" + txtus.Text + "' and sMatKhau='" + txtmk.Text + "' and FK_sChucVuID='THUNGAN'";
            SqlCommand cmd = new SqlCommand(select, Con);
            SqlDataReader reader = cmd.ExecuteReader();
            if ((string.IsNullOrEmpty(txtus.Text)) || (string.IsNullOrEmpty(txtmk.Text)))
            {
                MessageBox.Show("Vui lòng điền đủ thông tin", "err", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);

            }
            else if (reader.Read())
            {
                reader.Read();
                MessageBox.Show("Đăng nhập vào hệ thống (Quyền Admin) !", "Thông báo !");
                TrangChu.quyen = "THUNGAN";

                this.Hide();
                //this.Close();
            }
            else
            {
                MessageBox.Show("Đăng nhập vào hệ thống (Quyền user) !", "Thông báo !");
                TrangChu.quyen = "TAPVU";
                this.Hide();
                //this.Close();
            }
            TrangChu frm = new TrangChu();
            //frm.Show();
            frm.ShowDialog();
            cmd.Dispose();
            reader.Close();
            reader.Dispose();


        }
        public static string taikhoan = "";

    }
}


