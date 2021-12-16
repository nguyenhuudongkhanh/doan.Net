using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace Nhom9_QuanLyNhaHang
{
    public partial class TaoTaiKhoanKhachHang : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source = VanHien; Initial Catalog = QL_NHAHANG_2; User ID = sa; Password = 123");
        SqlCommand cmd;
        public TaoTaiKhoanKhachHang()
        {
            InitializeComponent();
            txtDiaChi.Text = "";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnTaoMoi_Click(object sender, EventArgs e)
        {
            if (txtSoDienThoai.Text.Trim().Equals("") || txtTenKhachHang.Text.Trim().Equals(""))
            {
                MessageBox.Show("Vui lòng nhập các thông tin cần thiết");
                txtSoDienThoai.Text = "";
            }
            else
            {
                if (isCheckSDT(txtSoDienThoai.Text.Trim()))
                {
                    MessageBox.Show("Số điện thoại đã được đăng kí");
                }
                else
                {
                    string query = null;
                    if (!txtDiaChi.Text.Equals(""))
                        query = "INSERT INTO TBL_KHACHHANG(sTenKhachHang, sSoDienThoai, sDiaChi, iDiemSo) VALUES (N'" + txtTenKhachHang.Text + "', '" + txtSoDienThoai.Text + "', '" + txtDiaChi.Text + "', 0)";
                    else query = "INSERT INTO TBL_KHACHHANG(sTenKhachHang, sSoDienThoai, iDiemSo) VALUES (N'" + txtTenKhachHang.Text + "', '" + txtSoDienThoai.Text + "', 0)";
                    if (query != null)
                    {
                        try
                        {
                            con.Open();
                            cmd = new SqlCommand(query, con);
                            cmd.ExecuteNonQuery();
                            con.Close();
                            MessageBox.Show("Thêm khách hàng thành công");
                            this.Hide();
                        }
                        catch
                        {
                            MessageBox.Show("Lỗi khi thêm khách hàng");
                        }
                    }
                }

            }



        }
        public bool isCheckSDT(string soDienThoai)
        {
            bool isCheck = false;
            try
            {
                string query = "SELECT sSoDienThoai FROM TBL_KHACHHANG WHERE sSoDienThoai = '" + soDienThoai + "'";
                con.Open();
                cmd = new SqlCommand(query, con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    isCheck = true;
                }
                else isCheck = false;
                con.Close();
            }
            catch
            {
                isCheck = false;
            }
            return isCheck;
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Bạn có muốn thoát không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
                Application.Exit();
        }

        private void TaoTaiKhoanKhachHang_Load(object sender, EventArgs e)
        {

        }
    }
}
