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
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source = VanHien; Initial Catalog = QL_NHAHANG_2; User ID = sa; Password = 123");
        SqlCommand cmd;
        public Form1()
        {
            InitializeComponent();
        }
        string txtTaiKhoan, txtMatKhau;
        int tongSoMonAn = 0;
        double tongTien = 0;
        public Form1(string strTaiKhoan, string strMatKhau)
        {
            InitializeComponent();
            txtTaiKhoan = strTaiKhoan;
            txtMatKhau = strMatKhau;
            setTenNhanVien();
            showPriceCbo();
            showMonAn();
            cboGiaMonAn.Items[0] = "Tìm theo giá";
            setTenNhanVien();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            loadDataLoaiBanAn();
            loadLoaiMonAn();
            setDayAndTime();
            menuStrip1.BackColor = Color.OrangeRed;
            menuStrip1.ForeColor = Color.Black;
            menuStrip1.Text = "File Menu";
            loadBanAn("Tất cả");
            txtTongSoMonAn.Text = tongSoMonAn.ToString();
            txtTongTien.Text = tongTien.ToString();
            setTenNhanVien();
        }
        public void loadDataLoaiBanAn()
        {
            //string query = "SELECT sTenLoaiBanAn FROM TBL_LOAIBANAN";
            //cboLoaiBanAn.DisplayMember = "sTenLoaiBanAn";
            //cboLoaiBanAn.DataSource = ConnectDatabase.getDataTable(query);
            cboLoaiBanAn.Items.Add("Tất cả");
            cboLoaiBanAn.SelectedIndex = 0;

            string query = "SELECT sTenLoaiBanAn FROM TBL_LOAIBANAN";
            try
            {
                con.Open();
                cmd = new SqlCommand(query, con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    cboLoaiBanAn.Items.Add(dr["sTenLoaiBanAn"]);
                }
                dr.Close();
                con.Close();
            }
            catch
            {
                MessageBox.Show("Lỗi khi load loại bàn ăn", "Thông báo");
            }
        }
        public void loadLoaiMonAn()
        {
            cboLoaiMonAn.Items.Add("Tất cả");
            cboLoaiMonAn.SelectedIndex = 0;

            string query = "SELECT sTenLoaiMonAn FROM TBL_LOAIMONAN";
            try
            {
                con.Open();
                cmd = new SqlCommand(query, con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    cboLoaiMonAn.Items.Add(dr["sTenLoaiMonAn"]);
                }
                dr.Close();
                con.Close();
            }
            catch
            {
                MessageBox.Show("Lỗi khi load loại món ăn", "Thông báo");
            }
            //cboLoaiMonAn.ValueMember = "PK_sLoaiMonAnID";
            //cboLoaiMonAn.DisplayMember = "sTenLoaiMonAn";
            //cboLoaiMonAn.DataSource = ConnectDatabase.getDataTable(query);
        }
        public void setDayAndTime()
        {
            DateTime time = DateTime.Now;
            string day = time.Day + "/" + time.Month + "/" + time.Year;
            lbNgay2.Text = day;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lbGio.Text = DateTime.Now.ToString("T");
        }
        public void setTenNhanVien()
        {
            try
            {
                string query = "SELECT * FROM TBL_NHANVIEN WHERE sTenDangNhap = '" + txtTaiKhoan + "' AND sMatKhau = '" + txtMatKhau + "'";
                con.Open();
                cmd = new SqlCommand(query, con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lbTenNhanVien.Text = dr[2].ToString();
                }
                dr.Close();
                con.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error connect database", "Notify");
            }
            // DataTable dt = ConnectDatabase.getDataTable("SELECT * FROM TBL_NHANVIEN WHERE sTenDangNhap = '" + txtTaiKhoan + "' AND sMatKhau = '" + txtMatKhau + "'");
        }
        public void showPriceCbo()
        {
            cboGiaMonAn.Items.Add("20.000");
            cboGiaMonAn.Items.Add("50.000");
            cboGiaMonAn.Items.Add("100.000");
            cboGiaMonAn.Items.Add("200.000");
            cboGiaMonAn.Items.Add("500.000");
            cboGiaMonAn.SelectedIndex = 0;
        }
        public void showMonAn()
        {
            //dataGridView1.Columns[1].Width = 100;

            dataGridView1.ColumnCount = 5;
            dataGridView1.Columns[0].Name = "STT";
            dataGridView1.Columns[0].Width = 40;
            dataGridView1.Columns[1].Name = "Tên món ăn";
            dataGridView1.Columns[1].Width = 138;
            //dataGridView1.Columns[2].Name = "Loại món ăn";
            dataGridView1.Columns[2].Name = "Loại món ăn";
            dataGridView1.Columns[3].Name = "Thành tiền";
            dataGridView1.Columns[4].Name = "Số lượng";
            dataGridView1.Columns[4].Width = 40;


            loadMonAn("Tất cả");

            //DataGridViewTextBoxColumn txtSoLuong = new DataGridViewTextBoxColumn();
            //dataGridView1.Columns.Add(txtSoLuong);
            //txtSoLuong.HeaderText = "Số lượng";


            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            dataGridView1.Columns.Add(btn);
            btn.HeaderText = "Chức năng";
            btn.Text = "Thêm vào";
            btn.Name = "btnThemVao";
            btn.UseColumnTextForButtonValue = true;

        }
        public void loadMonAn(string LoaiMonAn)
        {


            string query = "";
            if (LoaiMonAn == "Món ăn chay")
                query = "SELECT * FROM TBL_MONAN WHERE FK_sLoaiMonAn = 'M-CHAY'";
            else if (LoaiMonAn == "Món ăn mặn")
                query = "SELECT * FROM TBL_MONAN WHERE FK_sLoaiMonAn = 'M-MAN'";
            else if (LoaiMonAn == "Món ăn tráng miệng")
                query = "SELECT * FROM TBL_MONAN WHERE FK_sLoaiMonAn = 'M-TRANGMIENG'";
            else if (LoaiMonAn == "Nước uống")
                query = "SELECT * FROM TBL_MONAN WHERE FK_sLoaiMonAn = 'NC'";
            else
                query = "SELECT * FROM TBL_MONAN";
            loadMonAnQuery(query);
        }
        public void loadMonAnQuery(string query)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            try
            {
                con.Open();
                cmd = new SqlCommand(query, con);
                SqlDataReader dr = cmd.ExecuteReader();
                string[] row = new string[] { };
                while (dr.Read())
                {
                    row = new string[] { dr["PK_iMonAnID"].ToString(), dr["sTenMonAn"].ToString(),
                        dr["FK_sLoaiMonAnID"].ToString(), dr["fGiaMonAn"].ToString(), "1" };
                    dataGridView1.Rows.Add(row);
                }
                dr.Close();
                con.Close();
            }
            catch
            {
                MessageBox.Show("Lỗi tại load món ăn", "Thông báo");
            }
        }
        private void iHereToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void cboLoaiMonAn_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {

            string query = null;
            if (txtTenMonAnSearch.Text == "")
            {
                if (cboLoaiMonAn.Text != "Tất cả")
                {
                    string loaiMonAnID = loadIDToCbo("SELECT * FROM TBL_LOAIMONAN WHERE sTenLoaiMonAn = N'" + cboLoaiMonAn.Text + "'", 0);
                    query = "SELECT * FROM TBL_MONAN WHERE FK_sLoaiMonAnID = '" + loaiMonAnID + "'";
                }
                else
                {
                    query = "SELECT * FROM TBL_MONAN";
                }
            }
            else
            {
                if (cboLoaiMonAn.Text == "Tất cả")
                {
                    query = "SELECT * FROM TBL_MONAN WHERE sTenMonAn LIKE '%' + N'" + txtTenMonAnSearch.Text + "' + '%'";
                }
                else
                {
                    string loaiMonAnID = loadIDToCbo("SELECT * FROM TBL_LOAIMONAN WHERE sTenLoaiMonAn = N'" + cboLoaiMonAn.Text + "'", 0);
                    query = "SELECT * FROM TBL_MONAN WHERE sTenMonAn LIKE '%' + N'" + txtTenMonAnSearch.Text + "' + '%' AND FK_sLoaiMonAnID = '" + loaiMonAnID + "' ";
                }
            }
            if (query != null)
            {
                loadMonAnQuery(query);
            }
        }
        public string loadIDToCbo(string query, int item)
        {
            string result = null;
            //if (con.State == ConnectionState.Closed)
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                //TODO - Button Clicked - Execute Code Here
                //Lấy giá trị trong button ở button click ở rows đó.
                if (lbTenBanAnIF.Text != "Tên bàn ăn")
                {
                    string maMonAn = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                    string soLuong = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                    //string x = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                    //MessageBox.Show(soLuong, "Notify");
                    //Lấy mã hóa đơn mới nhất của bàn ăn
                    string maBanAn = loadIDToCbo("SELECT PK_iBanAnID FROM TBL_BANAN WHERE sTenBanAn = N'" + lbTenBanAnIF.Text + "'", 0);
                    string maHD = loadIDToCbo("SELECT TOP(1) PK_iHoaDonID FROM TBL_HOADON WHERE FK_iBanAnID = '" + maBanAn + "' ORDER BY PK_iHoaDonID DESC", 0);
                    try
                    {
                        con.Open();
                        string query = "INSERT INTO TBL_CHITIETHOADON VALUES ('" + maHD + "', '" + maMonAn + "', " + soLuong + ")";
                        cmd = new SqlCommand(query, con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        tongSoMonAn += Convert.ToInt32(soLuong);
                        txtTongSoMonAn.Text = tongSoMonAn.ToString();

                        //string queryGiaMA = loadIDToCbo("SELECT fGiaMonAn WHERE PK_iMonAnID = '" + maMonAn + "'", 0);
                        //double giaMotMonANn = Convert.ToDouble(queryGiaMA) * Convert.ToDouble(soLuong);
                        //tongTien += giaMotMonANn;
                        //txtTongTien.Text = tongTien.ToString();

                        capNhapTongTien(maMonAn, Convert.ToInt32(soLuong));
                        MessageBox.Show("Thêm thành công số lượng '" + soLuong + "'", "Thông báo");
                    }
                    catch
                    {
                        MessageBox.Show("Lỗi khi thêm món ăn", "Lỗi");
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn bàn ăn trước !!!!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            //Thêm sản phẩm vào chi tiết hóa đơn.
            //B1. Kiểm tra với bàn ăn A đang ở trạng thái nào. 
            //Nếu trạng thái 1 => Add món ăn vào hóa đơn có sản ở chi tiết hóa đơn đó.
            //Nếu trang thái 0 ( Sẳn sàng nhận khách )
            //=> Tạo mới 1 hóa đơn cho bàn ăn đó
            //Chuyển bàn ăn sang trạng thái 1
            //Add món ăn vào bàn ăn 
        }
        //Bàn ăn
        public void capNhapTongTien(string maMonAn, int soLuong)
        {
            try
            {
                string queryGiaMA = loadIDToCbo("SELECT fGiaMonAn FROM TBL_MONAN WHERE PK_iMonAnID = '" + maMonAn + "'", 0);
                double giaMotMonANn = Convert.ToDouble(queryGiaMA) * soLuong;
                tongTien += giaMotMonANn;
                txtTongTien.Text = tongTien.ToString();
            }
            catch
            {
                MessageBox.Show("Lỗi khi cập nhật tổng tiền", "Thông báo");
            }
        }
        private void LayTenBanAnChoLableTenBanAn_click()
        {
            //lbTenBanAnIF.Text = but
        }
        public string giamGia(string LoaiGiamGia)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            try
            {
                string query = "SELECT * FROM TBL_GIAMGIA WHERE PK_sGiamGiaID = '" + LoaiGiamGia + "'";
                cmd = new SqlCommand(query, con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    return dr["fTiLeGiamGia"].ToString();
                }
                con.Close();
            }
            catch
            {
                MessageBox.Show("Lỗi khi lấy thông tin giảm giá");

            }
            return null;
        }
        void btn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            //MessageBox.Show("You have clicked button number " + btn.Text);
            lbTenBanAnIF.Text = btn.Text;
            txtGiaBanAn.Text = string.Format("{0:0,0}", loadIDToCbo("SELECT fPhuThu FROM TBL_BANAN, TBL_LOAIBANAN WHERE FK_sLoaiBanAnID = PK_sLoaiBanAnID AND sTenBanAn = N'" + btn.Text + "'", 0));
            string trangThaiBanAn = loadIDToCbo("SELECT bTrangThaiBanAn FROM TBL_BANAN WHERE sTenBanAn = N'" + btn.Text + "'", 0);
            if (trangThaiBanAn == "True")
            {
                btnBatDau.Text = "Kết thúc";
            }
            else btnBatDau.Text = "Bắt đầu";
        }

        private void cboLoaiBanAn_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadBanAn(cboLoaiBanAn.Text);
        }
        public void ShowMyDialogBox()
        {
            Form1 testDialog = new Form1();

            // Show testDialog as a modal dialog and determine if DialogResult = OK.
            if (testDialog.ShowDialog(this) == DialogResult.OK)
            {
                // Read the contents of testDialog's TextBox.
                //this.txtResult.Text = testDialog.TextBox1.Text;
            }
            else
            {
                //this.txtResult.Text = "Cancelled";
            }
            testDialog.Dispose();
        }
        private void btnBatDau_Click(object sender, EventArgs e)
        {
            string query = null;
            if (lbTenBanAnIF.Text == "Tên bàn ăn")
            {
                MessageBox.Show("Vui lòng chọn bàn ăn trước khi bắt đầu ", "Thông báo");
            }
            else
            {
                if (btnBatDau.Text == "Bắt đầu")
                {
                    query = "UPDATE TBL_BANAN SET bTrangThaiBanAn = 1 WHERE sTenBanAn = N'" + lbTenBanAnIF.Text + "'";
                    btnBatDau.Text = "Kết thúc";
                    txtTongTien.Text = txtGiaBanAn.Text;
                    tongTien = Convert.ToDouble(txtTongTien.Text);
                    try
                    {

                        string maBanAn = loadIDToCbo("SELECT PK_iBanAnID FROM TBL_BANAN WHERE sTenBanAn = N'" + lbTenBanAnIF.Text + "'", 0);
                        string maNhanVien = loadIDToCbo("SELECT PK_iNhanVienID FROM TBL_NHANVIEN WHERE sTenNhanVien = N'" + lbTenNhanVien.Text + "'", 0);
                        string queryCreateHD = null;
                        if (lbTenKhachHang.Text != "Tên khách hàng")
                        {
                            string maKH = loadIDToCbo("SELECT PK_iKhachHangID FROM TBL_BANAN WHERE sTenKhachHang = N'" + lbTenKhachHang + "'", 0);
                            queryCreateHD = "SET DATEFORMAT DMY INSERT INTO TBL_HOADON VALUES ('" + maKH + "','" + maNhanVien + "' '" + maBanAn + "', '" + lbNgay2.Text + "')";
                        }
                        else
                        {
                            queryCreateHD = "SET DATEFORMAT DMY INSERT INTO TBL_HOADON(FK_iNhanVienID, FK_iBanAnID, dNgayLapHoaDon) VALUES ('" + maNhanVien + "', '" + maBanAn + "', '" + lbNgay2.Text + "')";
                        }

                        if (queryCreateHD != null)
                        {
                            con.Open();
                            cmd = new SqlCommand(queryCreateHD, con);
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Lỗi tạo mới hóa đơn");
                    }
                }
                else
                {
                    if (txtTongTien.Text != "0")
                    {
                        DialogResult dialog = MessageBox.Show("Bạn muốn kết thúc khi chưa thanh toán ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dialog == DialogResult.Yes)
                        {
                            query = "UPDATE TBL_BANAN SET bTrangThaiBanAn = 0 WHERE sTenBanAn = N'" + lbTenBanAnIF.Text + "'";
                            btnBatDau.Text = "Bắt đầu";
                        }
                    }
                    else
                    {
                        query = "UPDATE TBL_BANAN SET bTrangThaiBanAn = 0 WHERE sTenBanAn = N'" + lbTenBanAnIF.Text + "'";
                        btnBatDau.Text = "Bắt đầu";
                    }
                }
                if (query != null)
                {
                    try
                    {
                        con.Open();
                        cmd = new SqlCommand(query, con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        loadBanAn(cboLoaiBanAn.Text);
                    }
                    catch
                    {
                        MessageBox.Show("Lỗi khi bắt đầu bàn ăn", "Thông báo");
                    }
                }

            }
        }

        private void đăngXuấtToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void btnXacNhanKH_Click(object sender, EventArgs e)
        {
            string tenKH = loadIDToCbo("SELECT sTenKhachHang FROM TBL_KHACHHANG WHERE sSoDienThoai = '" + txtSDTKH.Text + "'", 0);
            if (tenKH != null)
            {
                lbTenKhachHang.Text = tenKH;
            }
        }

        private void btnXemChiTietHoaDon_Click(object sender, EventArgs e)
        {
            //this.TopMost = true;
            //this.WindowState = FormWindowState.Maximized;
            string MaBanAn = loadIDToCbo("SELECT * FROM TBL_BANAN WHERE sTenBanAn = N'" + lbTenBanAnIF.Text + "'", 0);
            string MaNhanVien = loadIDToCbo("SELECT * FROM TBL_NHANVIEN WHERE sTenNhanVien = N'" + lbTenNhanVien.Text + "'", 0);
            string maHoaDon = loadIDToCbo("SELECT TOP(1) * FROM TBL_HOADON WHERE FK_iNhanVienID = '" + MaNhanVien + "' AND FK_iBanAnID = '" + MaBanAn + "' ORDER BY PK_iHoaDonID DESC", 0);
            string TenDangNhap = loadIDToCbo("SELECT * FROM TBL_NHANVIEN WHERE sTenNhanVien = N'" + lbTenNhanVien.Text + "'", 3);

            if (maHoaDon == null)
            {
                MessageBox.Show("Bàn này chưa có hóa đơn chưa thanh toán");
            }
            else
            {
                string maBanAn = loadIDToCbo("SELECT * FROM TBL_BANAN WHERE sTenBanAn = N'" + lbTenBanAnIF.Text + "'", 0);
                Form form = new ThongTinChiTietHoaDon(maHoaDon, txtTongTien.Text, maBanAn, TenDangNhap, txtMatKhau);
                this.Hide();
                form.Show();

            }

        }

        private void btnTaoMoi_Click(object sender, EventArgs e)
        {
            Form form = new TaoTaiKhoanKhachHang();
            form.Show();
        }

        public void loadBanAn(string loaiBanAn)
        {
            flowLayoutPanel1.Controls.Clear();
            string query = null;
            if (loaiBanAn == "Tất cả")
            {
                query = "SELECT * FROM TBL_BANAN";
            }
            else
            {
                string loaiBanAnID = loadIDToCbo("SELECT * FROM TBL_LOAIBANAN WHERE sTenLoaiBanAn = N'" + loaiBanAn + "'", 0);
                query = "SELECT * FROM TBL_BANAN WHERE FK_sLoaiBanAnID = '" + loaiBanAnID + "'";
            }
            if (query != null)
            {
                try
                {
                    con.Open();
                    //query = "SELECT * FROM TBL_BANAN";
                    cmd = new SqlCommand(query, con);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        Button btn = new Button();
                        btn.Text = dr[2].ToString();
                        btn.Size = new Size(120, 70);
                        btn.Margin = new Padding(10, 10, 10, 10);
                        flowLayoutPanel1.Controls.Add(btn);
                        btn.Click += new EventHandler(btn_Click);
                        if (dr["bTrangThaiBanAn"].ToString() == "True")
                            btn.BackColor = Color.Red;
                    }
                    flowLayoutPanel1.AutoScroll = true;
                    dr.Close();
                    con.Close();
                }
                catch
                {
                    MessageBox.Show("Lỗi");
                }
            }

        }
    }
}
