using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nhom9_QuanLyNhaHang
{
    class KetNoiDatabase
    {
        public static SqlConnection con;
        SqlDataAdapter da;
        DataSet ds = new DataSet();
        public static string ConStr;
        Appsetting appset = new Appsetting();
        public KetNoiDatabase()
        {   ConStr = appset.getconnectionstring("Nhom9_QuanLyNhaHang.Properties.Settings.QL_NHAHANG_3ConnectionString");
            //ConStr = @"Data source=DESKTOP-7O741U9\SQLEXPRESS; Initial Catalog=QL_NHAHANG_3; Integrated security=true; uid=sa; pwd=28072001";
            con = new SqlConnection(ConStr);

        }
        public KetNoiDatabase(string severName, string dbName, bool auth, string uid, string pasw)
        {
          string ConStr;
            if (auth)
                ConStr = string.Format(@"Data Source={0}; Initial Catalog={1} ;integrated security=true;", severName, dbName);
            else
                ConStr = string.Format(@"Data Source={0}; Initial Catalog={1} ; integrated security=fale;uid={2};pasw={3}", severName, dbName, uid, pasw);
            con = new SqlConnection(ConStr);
        }
        public bool testConnection(string conection)
        {
            try
            {
                con = new SqlConnection(conection);
                con.Open();
                return true;
            }
            catch (SqlException ex)
            {
                return false;
            }
        }
        public DataTable LAYDULIEU(string lenhsql)
        {
            da = new SqlDataAdapter(lenhsql, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;

        }
        //cap nhat du lieu
        public void CAPNHATDULIEU(string lenhsql, DataTable table)
        {
            da = new SqlDataAdapter(lenhsql, con);
            SqlCommandBuilder cmdbuider = new SqlCommandBuilder(da);
            da.Update(table);

        }
        //them xoa sua
        public void THEMXOASUA(string sql)
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlCommand cm = new SqlCommand(sql, con);
            cm.ExecuteNonQuery();
            con.Close();

        }
        public static void MoKetNoi()
        {
            con = new SqlConnection(ConStr);
            con.Open();
        }
        void ketnoi()
        {
            con = new SqlConnection(ConStr);
            con.Open();
        }

        void ngatketnoi()
        {
            con.Close();
        }

        public void thucthiketnoi(string strsql)
        {
            ketnoi();
            SqlCommand com = new SqlCommand(strsql, con);
            com.ExecuteNonQuery();
            ngatketnoi();

        }

        public static void DongKetNoi()
        {
            con.Close();
        }

        public void luuNhanVien(DataTable dtNhanVien, string maNhanVien, string tenChucVu, string tenNhanVien,string tenDangNhap, string matKhau,
            string soDienThoai, string email,string queQuan, string gioiTinh, string trangThai)
        {
            DataRow newrow = dtNhanVien.NewRow();
            newrow[0] = maNhanVien;
            newrow[1] = tenChucVu;
            newrow[2] = tenNhanVien;
            newrow[3] = tenDangNhap;
            newrow[4] = matKhau;
            newrow[5] = soDienThoai;
            newrow[6] = email;
            newrow[7] = queQuan;
            newrow[8] = gioiTinh;
            newrow[9] = trangThai;
            dtNhanVien.Rows.Add(newrow);
        }

        public void xoaNhanVien(DataTable dtNhanVien, string maNhanVien)
        {
            DataRow r = dtNhanVien.Rows.Find(maNhanVien);
            if (r != null)
                r.Delete();
        }
        //----------khach hang
        public void luuKhachHang(DataTable dtKhachHang, string maKhachHang, string tenKhachHang, string soDienThoai, string queQuan, string diemSo)
        {
            DataRow newrow = dtKhachHang.NewRow();
            newrow[0] = maKhachHang;
            newrow[1] = tenKhachHang;
            newrow[2] = soDienThoai;
            newrow[3] = queQuan;
            newrow[4] = diemSo;
            dtKhachHang.Rows.Add(newrow);
        }

        public void xoaKhachHang(DataTable dtKhachHang, string maKhachHang)
        {
            DataRow r = dtKhachHang.Rows.Find(maKhachHang);
            if (r != null)
                r.Delete();
        }

        //--loai ban an
        public void luuLoaiBanAn(DataTable dtLoaiBanAn, string maLoai, string tenLoai, string phuThu)
        {
            DataRow newrow = dtLoaiBanAn.NewRow();
            newrow[0] = maLoai;
            newrow[1] = tenLoai;
            newrow[2] = phuThu;
            dtLoaiBanAn.Rows.Add(newrow);
        }

        public void xoaLoaiBanAn(DataTable dtLoaiBanAn, string maLoai)
        {
            DataRow r = dtLoaiBanAn.Rows.Find(maLoai);
            if (r != null)
                r.Delete();
        }
        //-loai mon an
        public void luuLoaiMonAn(DataTable dtLoaiMonAn, string maLoai, string tenLoai)
        {
            DataRow newrow = dtLoaiMonAn.NewRow();
            newrow[0] = maLoai;
            newrow[1] = tenLoai;
            dtLoaiMonAn.Rows.Add(newrow);
        }

        public void xoaLoaiMonAn(DataTable dtLoaiMonAn, string maLoai)
        {
            DataRow r = dtLoaiMonAn.Rows.Find(maLoai);
            if (r != null)
                r.Delete();
        }
        //-chuc vu
        public void luuChucVu(DataTable dtChucVu, string maChucVu, string tenChucVu)
        {
            DataRow newrow = dtChucVu.NewRow();
            newrow[0] = maChucVu;
            newrow[1] = tenChucVu;
            dtChucVu.Rows.Add(newrow);
        }

        public void xoaChucVu(DataTable dtChucVu, string maChucVu)
        {
            DataRow r = dtChucVu.Rows.Find(maChucVu);
            if (r != null)
                r.Delete();
        }
        //--ban an
        // db.luuBanAn(dtBanAn, txtMaBanAn.Text, txtTenBanAn.Text, cbbLoaiBanAn.SelectedValue.ToString(), cbbTrangThai.SelectedItem.ToString());
        public void luuBanAn(DataTable dtBanAn, string maBanAn, string loaiBanAn, string tenBanAn, string trangThai)
        {
            DataRow newrow = dtBanAn.NewRow();
            newrow[0] = maBanAn;
            newrow[1] = loaiBanAn;
            newrow[2] = tenBanAn;
            newrow[3] = trangThai;
            dtBanAn.Rows.Add(newrow);
        }

        public void xoaBanAn(DataTable dtBanAn, string maBanAn)
        {
            DataRow r = dtBanAn.Rows.Find(maBanAn);
            if (r != null)
                r.Delete();
        }
        //-hoadon
        public void luuHoaDon(DataTable dtHoaDon, string maHoaDon, string tenKhachHang, string tenNhanVien, string BanAn,string ngayLap)
        {
            DataRow newrow = dtHoaDon.NewRow();
            newrow[0] = maHoaDon;
            newrow[1] = tenKhachHang;
            newrow[2] = tenNhanVien;
            newrow[3] = BanAn;
            newrow[4] = ngayLap;
            dtHoaDon.Rows.Add(newrow);        
        }

        public void xoaHoaDon(DataTable dtHoaDon, string maHoaDon)
        {
            DataRow r = dtHoaDon.Rows.Find(maHoaDon);
            if (r != null)
                r.Delete();
        }
        //monan
        public void luuMonAn(DataTable dtMonAn, string maMonAn, string loaiMonAn, string tenMonAn, int gia, string hinhAnh, string trangThai)
        {
            DataRow newrow = dtMonAn.NewRow();
            newrow[0] = maMonAn;
            newrow[1] = loaiMonAn;
            newrow[2] = tenMonAn;
            newrow[3] = gia;
            newrow[4] = hinhAnh;
            newrow[5] = trangThai;
            dtMonAn.Rows.Add(newrow);
        }

        public void xoaMonAn(DataTable dtMonAn, string maMonAn)
        {
            DataRow r = dtMonAn.Rows.Find(maMonAn);
            if (r != null)
                r.Delete();
        }
        //chi tiet hoa don
        public void luuCTHD(DataTable dtHoaDon, string maCTHD, string maHoaDon, string tenMonAn, int sl)
        {
            DataRow newrow = dtHoaDon.NewRow();
            newrow[0] = maCTHD;
            newrow[1] = maHoaDon;
            newrow[2] = tenMonAn;
            newrow[3] = sl;
            dtHoaDon.Rows.Add(newrow);
        }

        public void xoaCTHD(DataTable dtHoaDon, string maCTHD)
        {
            DataRow r = dtHoaDon.Rows.Find(maCTHD);
            if (r != null)
                r.Delete();
        }

        //thanh toan
        //db.luuThanhToan(dtThanhToan, txtMaThanhToan.Text, txtMaHoaDon.Text, tongTien, ngayTT.Text);
        public void luuThanhToan(DataTable dtThanhToan, string maThanhToan, string maHoaDon,string hinhThucTT, int tongTien, string ngay)
        {
            DataRow newrow = dtThanhToan.NewRow();
            newrow[0] = maThanhToan;
            newrow[1] = maHoaDon;
            newrow[2] = hinhThucTT;
            newrow[3] = tongTien;
            newrow[4] = ngay;
            dtThanhToan.Rows.Add(newrow);
        }

        public void xoaThanhToan(DataTable dtThanhToan, string maThanhToan)
        {
            DataRow r = dtThanhToan.Rows.Find(maThanhToan);
            if (r != null)
                r.Delete();
        }
        //hinh thuc thanh toan
        public void luuHinhThucThanhToan(DataTable dtHinhThucThanhToan, string maThanhToan, string tenThanhToan, string trangThai)
        {
            DataRow newrow = dtHinhThucThanhToan.NewRow();
            newrow[0] = maThanhToan;
            newrow[1] = tenThanhToan;
            newrow[2] = trangThai;
            dtHinhThucThanhToan.Rows.Add(newrow);
        }

        public void xoaHinhThucThanhToan(DataTable dtHinhThucThanhToan, string maThanhToan)
        {
            DataRow r = dtHinhThucThanhToan.Rows.Find(maThanhToan);
            if (r != null)
                r.Delete();
        }
    }
  
}
