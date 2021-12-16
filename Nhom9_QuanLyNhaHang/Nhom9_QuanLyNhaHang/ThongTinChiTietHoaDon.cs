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

    public partial class ThongTinChiTietHoaDon : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source = VanHien; Initial Catalog = QL_NHAHANG_2; User ID = sa; Password = 123");
        SqlCommand cmd;
        public ThongTinChiTietHoaDon()
        {
            InitializeComponent();
        }
        string MaHoaDon = null;
        string MaBanAn = null;
        string TenDangNhap = null;
        string MatKhau = null;
        public ThongTinChiTietHoaDon(string maHoaDon, string tongTien, string maBanAn, string tenDangNhap, string matKhau)
        {
            InitializeComponent();
            MaHoaDon = maHoaDon;
            txtMaHoaDon.Text = maHoaDon;
            txtTongTien.Text = tongTien;
            loadCboHTTT();
            loadTenMonAns();
            loadChiTietHoaDon();
            MaBanAn = maBanAn;
            TenDangNhap = tenDangNhap;
            MatKhau = matKhau;
            txtSoLuong.Text = "1";
        }
        //public double tinhTongTien()
        //{

        //}
        private void button1_Click(object sender, EventArgs e)
        {
            string maMonAn = loadQuery("SELECT * FROM TBL_monAn WHERE sTenMonAn =  N'" + comboBox1.Text + "'", 0);
            string giaMonAn = loadQuery("SELECT * FROM TBL_monAn WHERE sTenMonAn =  N'" + comboBox1.Text + "'", 4);
            try
            {
                string query = "INSERT INTO TBL_CHITIETHOADON VALUES ('" + MaHoaDon + "', '" + maMonAn + "', '" + txtSoLuong.Text + "')";
                con.Open();
                cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                con.Close();
                loadChiTietHoaDon();
                double tongTien = Convert.ToDouble(txtTongTien.Text);
                tongTien += Convert.ToDouble(giaMonAn);
                txtTongTien.Text = tongTien.ToString();
            }
            catch
            {
                MessageBox.Show("Lỗi khi thêm món ăn vào hóa đơn");
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
        public void loadChiTietHoaDon()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            dataGridView1.ColumnCount = 3;
            dataGridView1.Columns[0].Name = "Mã chi tiết";
            dataGridView1.Columns[0].Width = 150;
            dataGridView1.Columns[1].Name = "Mã món ăn";
            dataGridView1.Columns[1].Width = 150;

            dataGridView1.Columns[2].Name = "Số lượng";
            dataGridView1.Columns[2].Width = 160;

            loadChiTietMonAn();

            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            dataGridView1.Columns.Add(btn);
            btn.HeaderText = "Chức năng";
            btn.Text = "Xóa";
            btn.Name = "btnThemVao";
            btn.UseColumnTextForButtonValue = true;
        }
        public void loadChiTietMonAn()
        {
            if (MaHoaDon != null)
            {
                string query = "SELECT * FROM TBL_CHITIETHOADON WHERE FK_iHoaDonID = '" + MaHoaDon + "'";
                try
                {
                    con.Open();
                    cmd = new SqlCommand(query, con);
                    SqlDataReader dr = cmd.ExecuteReader();
                    string[] row = new string[] { };
                    while (dr.Read())
                    {
                        row = new string[] { dr["PK_iChiTietHoaDonID"].ToString(), dr["FK_iMonAn"].ToString(), dr["iSoLuong"].ToString() };
                        dataGridView1.Rows.Add(row);
                    }
                    dr.Close();
                    con.Close();
                }
                catch
                {
                    MessageBox.Show("Loi");
                }
            }
        }
        public string loadQuery(string query, int item)
        {
            string result = null;
            //if (con.State == ConnectionState.Closed)
            con.Open();
            cmd = new SqlCommand(query, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
                result = dr[item].ToString();
            else result = null;
            dr.Close();
            con.Close();
            return result;
        }
        public void loadCboHTTT()
        {
            comboBox2.Items.Clear();
            comboBox2.Refresh();
            //comboBox2.Items.Add(loadQuery("SELECT * FROM TBL_HINHTHUCTHANHTOAN WHERE bTrangThai = 1", 0));
            try
            {
                string query = "SELECT * FROM TBL_HINHTHUCTHANHTOAN WHERE bTrangThai = 1";
                con.Open();
                cmd = new SqlCommand(query, con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    comboBox2.Items.Add(dr["sTenHinhThucThanhToan"].ToString());

                }
                dr.Close();
                con.Close();
                comboBox2.SelectedIndex = 1;
            }
            catch
            {

            }
        }
        public void loadTenMonAns()
        {
            comboBox1.Items.Clear();
            comboBox1.Refresh();
            //comboBox2.Items.Add(loadQuery("SELECT * FROM TBL_HINHTHUCTHANHTOAN WHERE bTrangThai = 1", 0));
            try
            {
                string query = "SELECT * FROM TBL_MONAN WHERE bTrangThai = 1";
                con.Open();
                cmd = new SqlCommand(query, con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    comboBox1.Items.Add(dr["sTenMonAn"].ToString());

                }
                dr.Close();
                con.Close();
                comboBox1.SelectedIndex = 1;
            }
            catch
            {

            }
        }

        private void ThongTinChiTietHoaDon_Load(object sender, EventArgs e)
        {


        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            string timeNow = "";
            DateTime d1 = DateTime.Now;
            timeNow = d1.Year + "-" + d1.Month + "-" + d1.Day;

            string HTTT = loadQuery("SELECT * FROM tbl_Hinhthucthanhtoan WHERE sTenHinhThucThanhToan = N'" + comboBox2.Text + "'", 0);
            string query = "INSERT INTO TBL_THANHTOAN VALUES ('" + MaHoaDon + "', '" + HTTT + "', '" + txtTongTien.Text + "', '" + timeNow + "')";
            try
            {
                con.Open();
                cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Thanh toán thành công, bạn sẽ quay về phiên làm việc");

                try
                {
                    string queryUpdate = "UPDATE TBL_BANAN SET bTrangThaiBanAn = 0 WHERE PK_iBanAnID = '" + MaBanAn + "'";
                    con.Open();
                    cmd = new SqlCommand(queryUpdate, con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                catch
                {
                    MessageBox.Show("Lỗi khi cập nhập lại mã bàn ăn");
                }

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                this.Hide();
                Form form = new Form1(TenDangNhap, MatKhau);
                form.Show();
            }
            catch
            {
                MessageBox.Show("Có lỗi khi thanh toán");
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }

            this.Hide();
            Form form = new Form1(TenDangNhap, MatKhau);
            form.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtDonGia.Text = loadQuery("SELECT * FROM TBL_MONAN WHERE sTenMonAn = N'" + comboBox1.Text + "'", 4);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                string maMonAn = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                try
                {
                    string query = "DELETE FROM TBL_CHITIETHOADON WHERE FK_iHoaDonID = '" + MaHoaDon + "' AND FK_iMonAn = '" + maMonAn + "'";
                    con.Open();
                    cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    con.Close();

                    loadChiTietHoaDon();
                }
                catch
                {
                    MessageBox.Show("Lỗi khi loại bỏ món ăn");
                    return;
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                xoaChiTietHoaDonALL();
                xoaHoaDon();
                updateBanAn();


                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                this.Hide();
                Form form = new Form1(TenDangNhap, MatKhau);
                form.Show();
            }
            catch
            {
                MessageBox.Show("Hủy hóa đơn không thành công");
            }
        }
        public void xoaChiTietHoaDonALL()
        {
            if (checkEmpCT() == false)
            {
                return;
            }
            else
            {
                try
                {
                    con.Open();
                    string query = "DELETE FROM TBL_CHITIETHOADON WHERE FK_iHoaDonID = '" + MaHoaDon + "'";
                    cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                catch
                {
                    MessageBox.Show("Lỗi khi xóa tất cả chi tiết hóa đơn");
                    return;
                }
            }
        }
        public void xoaHoaDon()
        {
            try
            {
                con.Open();
                string query = "DELETE FROM TBL_HOADON WHERE PK_iHoaDonID = '" + MaHoaDon + "'";
                cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch
            {
                MessageBox.Show("Lỗi khi xóa hóa đơn");
                return;
            }
        }
        public void updateBanAn()
        {
            try
            {
                string query = "UPDATE TBL_BANAN SET bTrangThaiBanAn = 0 WHERE PK_iBanAnID = '" + MaBanAn + "'";
                con.Open();
                cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch
            {
                MessageBox.Show("Lỗi khi cập nhật trạng thái bàn ăn");
                return;
            }
        }
        public bool checkEmpCT()
        {
            bool isCheck = false;
            try
            {
                string query = "SELECT COUNT(*) FROM TBL_CHITIETHOADON WHERE FK_iHoaDonID = '" + MaHoaDon + "'";
                con.Open();
                cmd = new SqlCommand(query, con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    isCheck = true;
                }
                else
                {
                    isCheck = false;
                }
                dr.Close();
                con.Close();
            }
            catch
            {
                MessageBox.Show("Loi dem mon an");
            }
            return isCheck;
        }
    }
}
