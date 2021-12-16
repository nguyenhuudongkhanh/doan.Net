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
    public partial class HINHTHUCTHANHTOAN : Form
    {
        KetNoiDatabase db = new KetNoiDatabase();
        DataTable dtHinhThucThanhToan;
        DataColumn[] keyHinhThucThanhToan = new DataColumn[1];
        bool themMoi = false;
        public HINHTHUCTHANHTOAN()
        {
            InitializeComponent();
        }

        public void LamMoiDuLieu()
        {
            txtmathanhtoan.Text = "";
            txttentahnhtoan.Text = "";
            cmbtrangthai.Text = "";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            themMoi = true;
            LamMoiDuLieu();
            txtmathanhtoan.DataBindings.Clear();
            txttentahnhtoan.DataBindings.Clear();
            cmbtrangthai.DataBindings.Clear();

            txtmathanhtoan.Enabled = txttentahnhtoan.Enabled = cmbtrangthai.Enabled=true;
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = false;
            btnLuu.Enabled = true;

            dtgvHinhThucThanhToan.AllowUserToAddRows = false;
            dtgvHinhThucThanhToan.ReadOnly = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult h = MessageBox.Show("Bạn có chắc muốn thoát không?", "Error", MessageBoxButtons.YesNoCancel);
            if (h == DialogResult.Yes)
                Application.Exit();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void HINHTHUCTHANHTOAN_Load(object sender, EventArgs e)
        {
            dtHinhThucThanhToan = db.LAYDULIEU("Select*from TBL_HINHTHUCTHANHTOAN");
            keyHinhThucThanhToan[0] = dtHinhThucThanhToan.Columns[0];
            dtHinhThucThanhToan.PrimaryKey = keyHinhThucThanhToan;

            dtgvHinhThucThanhToan.AutoGenerateColumns = false;
            dtgvHinhThucThanhToan.DataSource = dtHinhThucThanhToan;

            dtgvHinhThucThanhToan.Columns[0].DataPropertyName = "PK_sHinhThucThanhToanID";
            dtgvHinhThucThanhToan.Columns[1].DataPropertyName = "sTenHinhThucThanhToan";
            dtgvHinhThucThanhToan.Columns[2].DataPropertyName = "bTrangThai";

            HinhThucThanhToan_databiding();

            cmbtrangthai.Items.Add("True");
            cmbtrangthai.Items.Add("False");
            cmbtrangthai.SelectedIndex = 0;
        }

        void HinhThucThanhToan_databiding()
        {
            txtmathanhtoan.DataBindings.Add("Text", dtHinhThucThanhToan, "PK_sHinhThucThanhToanID");
            txttentahnhtoan.DataBindings.Add("Text", dtHinhThucThanhToan, "sTenHinhThucThanhToan");
            cmbtrangthai.DataBindings.Add("Text", dtHinhThucThanhToan, "bTrangThai");
            


        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (txtmathanhtoan.Text.Equals(""))
            {
                errorProvider1.SetError(txtmathanhtoan, "không được để rỗng");
                return;
            }

            errorProvider2.Clear();
            if (txttentahnhtoan.Text.Equals(""))
            {
                errorProvider2.SetError(txttentahnhtoan, "không được để rỗng");
                return;
            }
            if (themMoi)
            {
                if (txtmathanhtoan.Text != "" || txttentahnhtoan.Text != "" || cmbtrangthai.Text != "")
                {
                    db.luuHinhThucThanhToan(dtHinhThucThanhToan, txtmathanhtoan.Text,txttentahnhtoan.Text, cmbtrangthai.SelectedItem.ToString());

                }
                HinhThucThanhToan_databiding();
            }
            else
            {
                dtgvHinhThucThanhToan.Refresh();
            }
            txtmathanhtoan.Enabled = txttentahnhtoan.Enabled = cmbtrangthai.Enabled = false;
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = true;
            btnLuu.Enabled = true;
            dtgvHinhThucThanhToan.AllowUserToAddRows = false;
            dtgvHinhThucThanhToan.ReadOnly = true;  
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            themMoi = false;

            txtmathanhtoan.Enabled = txttentahnhtoan.Enabled = cmbtrangthai.Enabled=true;
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = false;
            btnLuu.Enabled = true;

            dtgvHinhThucThanhToan.ReadOnly = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DataTable dtNV_tam = null;
            if (MessageBox.Show("Bạn có chắc không", "Cảnh báo xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                == DialogResult.Yes)
            {
                dtNV_tam = db.LAYDULIEU("select distinct PK_sHinhThucThanhToanID from TBL_HINHTHUCTHANHTOAN where PK_sHinhThucThanhToanID='" + txtmathanhtoan.Text + "'");
                if (dtNV_tam.Rows.Count == 0)
                {
                    db.xoaHinhThucThanhToan(dtHinhThucThanhToan, txtmathanhtoan.Text);
                }
                else
                    MessageBox.Show("Hinh thanh toán tồn tại không xóa được");
            }
        }

        private void btnCapNhap_Click(object sender, EventArgs e)
        {
            try
            {
                db.CAPNHATDULIEU("select * from TBL_HINHTHUCTHANHTOAN", dtHinhThucThanhToan);
                MessageBox.Show("Cập nhập thành công", "Thông Báo");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            DialogResult = MessageBox.Show("Bạn chắc chắn hủy không ? ", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (DialogResult == DialogResult.Yes)
            {

                LamMoiDuLieu();
            }
            else
            {
                return;
            }
            //txtMaHoaDon.Enabled = true;
            txtmathanhtoan.DataBindings.Clear();
            txttentahnhtoan.DataBindings.Clear();
            cmbtrangthai.DataBindings.Clear();

            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = btnLuu.Enabled = true;
            txtmathanhtoan.Enabled = txttentahnhtoan.Enabled = cmbtrangthai.Enabled = false;
            HinhThucThanhToan_databiding();
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            dtgvHinhThucThanhToan.DataSource = db.LAYDULIEU("select * from TBL_HINHTHUCTHANHTOAN where sTenHinhThucThanhToan like N'%" + txtTimKiem.Text+ "%'");
        }
    }
}
