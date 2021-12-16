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
    public partial class TrangChu : Form
    {
        public static string quyen;
        public TrangChu()
        {
            InitializeComponent();
        }

        private void đăngNhậpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormDangNhap fr = new FormDangNhap();

            fr.MdiParent = this;
            fr.Show();
        }

        private void TrangChu_Load(object sender, EventArgs e)
        {
            if (quyen == "THUNGAN")
            {
                danhMụcToolStripMenuItem.Enabled = true;
                chứcNăngToolStripMenuItem.Enabled = true;
                quảnLýNhânViênToolStripMenuItem.Enabled = true;
                chiTiếtHóaĐơnToolStripMenuItem.Enabled = true;
                giảmGiáToolStripMenuItem1.Enabled = true;
                hìnhThứcThanhToánToolStripMenuItem.Enabled = true;


            }
            else if (quyen == "TAPVU")
            {
                danhMụcToolStripMenuItem.Enabled = true;
                chứcNăngToolStripMenuItem.Enabled = true;

            }
        }

        private void đổiMậtKhẩuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoiMatKhau fr = new DoiMatKhau();
            fr.MdiParent = this;
            fr.Show();
        }

        private void kháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KhachHang fr = new KhachHang();
            fr.MdiParent = this;
            fr.Show();
        }

        private void hóaĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HoaDon fr = new HoaDon();
            fr.MdiParent = this;
            fr.Show();
        }

        private void chiTiếtHóaĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChiTietHoaDon fr = new ChiTietHoaDon();
            fr.MdiParent = this;
            fr.Show();
        }

        private void giảmGiáToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void hìnhThứcThanhToánToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HINHTHUCTHANHTOAN fr = new HINHTHUCTHANHTOAN();
            fr.MdiParent = this;
            fr.Show();
        }

        private void mónĂnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MonAn fr = new MonAn();
            fr.MdiParent = this;
            fr.Show();
        }

        private void loạiMónĂnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoaiMonAn fr = new LoaiMonAn();
            fr.MdiParent = this;
            fr.Show();
        }

        private void bànĂnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BanAn fr = new BanAn();
            fr.MdiParent = this;
            fr.Show();
        }

        private void loạiBànĂnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoaiBanAn fr = new LoaiBanAn();
            fr.MdiParent = this;
            fr.Show();
        }

        private void giảmGiáToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //GiamGia fr = new GiamGia();
            //fr.MdiParent = this;
            //fr.Show();
        }

        private void nhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NhanVien fr = new NhanVien();
            fr.MdiParent = this;
            fr.Show();
        }

        private void chứcVụToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChucVu fr = new ChucVu();
            fr.MdiParent = this;
            fr.Show();
        }

        private void thanhToanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ThanhToan fr = new ThanhToan();
            fr.MdiParent = this;
            fr.Show();
        }

        private void tàiKhoảngToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
