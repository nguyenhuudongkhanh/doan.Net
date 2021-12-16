using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Reflection;

namespace Nhom9_QuanLyNhaHang
{
    public partial class MonAn : Form
    {
        KetNoiDatabase db = new KetNoiDatabase();
        DataTable dtloaiMonAn,dtMonAn;
        DataColumn[] keyMonAn = new DataColumn[1];
        bool themMoi = false;

        public MonAn()
        {
            InitializeComponent();
        }
        public void loadanh(PictureBox a)
        {

            string ha = dtgvMonAn.CurrentRow.Cells[3].Value.ToString();
            a.Image = new Bitmap(Application.StartupPath + "\\img_food\\" + ha);
        }
        private void btnborw_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            string file = openFileDialog1.FileName;
            if (string.IsNullOrEmpty(file))
                return;
            txtHinhAnh.Text = file;
            Image hinh = Image.FromFile(txtHinhAnh.Text);
            pictureBox1.Image = hinh;
            //OpenFileDialog open = new OpenFileDialog();
            //open.Filter = open.Filter = "JPG files (*.jpg)|*.jpg|All files (*.*)|*.*";
            //open.FilterIndex = 1;
            //open.RestoreDirectory = true;
            //if (open.ShowDialog() == DialogResult.OK)
            //{
            //    pictureBox1.ImageLocation = open.FileName;
            //    txthinhanh.Text = open.FileName;
            //}
        }
        private byte[] converImgToByte()
        {
            FileStream fs;
            fs = new FileStream(txtHinhAnh.Text, FileMode.Open, FileAccess.Read);
            byte[] picbyte = new byte[fs.Length];
            fs.Read(picbyte, 0, System.Convert.ToInt32(fs.Length));
            fs.Close();
            return picbyte;
        }
        void MonAn_databiding()
        {
            txtMaMonAn.DataBindings.Add("Text", dtMonAn, "PK_iMonAnID");
            cbbLoaiMonAn.DataBindings.Add("SelectedValue", dtMonAn, "FK_sLoaiMonAnID");
            //cbgiamgia.DataBindings.Add("SelectedValue", dtmonan, "FK_sGiamGiaID");
            txtTenMonAn.DataBindings.Add("Text", dtMonAn, "sTenMonAn");
            txtGia.DataBindings.Add("Text", dtMonAn, "fGiaMonAn");
            txtHinhAnh.DataBindings.Add("Text", dtMonAn, "sHinhAnhMonAn");
            cboTrangThai.DataBindings.Add("Text", dtMonAn, "bTrangThai");
          


        }
        public void LamMoiDuLieu()
        {
            txtMaMonAn.Text = "";
            cbbLoaiMonAn.Text = "";
            //cbgiamgia.Text = "";
            txtTenMonAn.Text = "";
            txtGia.Text = "";
            txtHinhAnh.Text = "";
            cboTrangThai.Text = "";
           

        }
        public void DuLieuComboBox()
        {

            cboTrangThai.Items.Add("True");
            cboTrangThai.Items.Add("False");
            
        }

        private void MonAn_Load(object sender, EventArgs e)
        {
            DuLieuComboBox();
            dtMonAn = db.LAYDULIEU("Select*from TBL_MONAN");
            dtloaiMonAn = db.LAYDULIEU("Select * from TBL_LOAIMONAN");
            keyMonAn[0] = dtMonAn.Columns[0];
            dtMonAn.PrimaryKey = keyMonAn;

            cbbLoaiMonAn.DataSource = dtloaiMonAn;
            cbbLoaiMonAn.DisplayMember = "sTenLoaiMonAn";
            cbbLoaiMonAn.ValueMember = "PK_sLoaiMonAnID";
            //cbgiamgia.DataSource = dtgiamgia;
            //cbgiamgia.DisplayMember = "sTenGiamGia";
            //cbgiamgia.ValueMember = "PK_sGiamGiaID";

            dtgvMonAn.AutoGenerateColumns = false;
            dtgvMonAn.DataSource = dtMonAn;

            dtgvMonAn.Columns[0].DataPropertyName = "PK_iMonAnID";
            DataGridViewComboBoxColumn cmbo = (DataGridViewComboBoxColumn)dtgvMonAn.Columns[1];
            cmbo.DataSource = dtloaiMonAn;
            cmbo.DisplayMember = "sTenLoaiMonAn";
            cmbo.ValueMember = "PK_sLoaiMonAnID";
            cmbo.DataPropertyName = "FK_sLoaiMonAnID";
            dtgvMonAn.Columns[2].DataPropertyName = "sTenMonAn";
            dtgvMonAn.Columns[3].DataPropertyName = "fGiaMonAn";
            dtgvMonAn.Columns[4].DataPropertyName = "sHinhAnhMonAn";
            dtgvMonAn.Columns[5].DataPropertyName = "bTrangThai";

            MonAn_databiding();


            //SqlConnection con = new SqlConnection(@"Data source=DESKTOP-95N5PBV; Initial Catalog=QL_NHAHANG; Integrated security=true; uid=sa; pwd=1");
            //SqlCommand cm = new SqlCommand("select * from TBL_MONAN",con);
            //SqlDataAdapter sd = new SqlDataAdapter(cm);
            //DataTable dt = new DataTable();
            //sd.Fill(dt);
            //dt.Columns.Add("Pic", Type.GetType("System.Byte[]"));
            //foreach (DataRow drow in dt.Rows)
            //{
            //    drow["Pic"] = File.ReadAllBytes(drow["sHinhAnhMonAn"].ToString());
            //}
            //dtgvMonAn.DataSource = dt;

            //SqlConnection con = new SqlConnection(@"Data source=DESKTOP-95N5PBV; Initial Catalog=QL_NHAHANG; Integrated security=true; uid=sa; pwd=1");
            //con.Open();
            //SqlDataAdapter sd = new SqlDataAdapter();
            //sd.SelectCommand = new SqlCommand("select * from TBL_MONAN", con);
            //DataTable dt = new DataTable();
            //sd.Fill(dt);
            //dtgvMonAn.DataSource = dt;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult h = MessageBox.Show("Bạn có chắc muốn thoát không?", "Error", MessageBoxButtons.YesNoCancel);
            if (h == DialogResult.Yes)
                Application.Exit();
        }

        private void dtgvMonAn_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void dtgvMonAn_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dtgvMonAn_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ////loadanh(pictureBox1);
            //txtmamonan.Text = dtgvMonAn.CurrentRow.Cells[0].Value.ToString();
            //cbbloaimonan.Text = dtgvMonAn.CurrentRow.Cells[1].Value.ToString();
            //cbgiamgia.Text = dtgvMonAn.CurrentRow.Cells[2].Value.ToString();
            //txttenmonan.Text = dtgvMonAn.CurrentRow.Cells[3].Value.ToString();
            //txtgiamonan.Text = dtgvMonAn.CurrentRow.Cells[4].Value.ToString();
            //txthinhanh.Text = dtgvMonAn.CurrentRow.Cells[5].Value.ToString();
            //cmbTrangThai.Text = dtgvMonAn.CurrentRow.Cells[6].Value.ToString();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LamMoiDuLieu();

            txtMaMonAn.Enabled = true;
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
            txtMaMonAn.DataBindings.Clear();
            cbbLoaiMonAn.DataBindings.Clear();
            txtTenMonAn.DataBindings.Clear();
            txtGia.DataBindings.Clear();
            txtHinhAnh.DataBindings.Clear();
            cboTrangThai.DataBindings.Clear();

            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = btnLuu.Enabled = true;
            cbbLoaiMonAn.Enabled = txtTenMonAn.Enabled = txtGia.Enabled = txtHinhAnh.Enabled = cboTrangThai.Enabled = false;
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = true;
            btnLuu.Enabled = true;
            MonAn_databiding();

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            themMoi = true;
            LamMoiDuLieu();
            tangId();
            txtMaMonAn.DataBindings.Clear();
            cbbLoaiMonAn.DataBindings.Clear();
            txtTenMonAn.DataBindings.Clear();
            txtGia.DataBindings.Clear();
            txtHinhAnh.DataBindings.Clear();
            cboTrangThai.DataBindings.Clear();

            cbbLoaiMonAn.Enabled = txtTenMonAn.Enabled= txtGia.Enabled = txtHinhAnh.Enabled = cboTrangThai.Enabled = true;
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = true;
            btnLuu.Enabled = true;

            dtgvMonAn.AllowUserToAddRows = false;
            dtgvMonAn.ReadOnly = false;
        }

        void tangId()
        {
            int r = dtgvMonAn.RowCount - 1;
            int soNV = int.Parse(dtgvMonAn.Rows[r].Cells[0].Value.ToString()) + 1;
            txtMaMonAn.Text = soNV.ToString();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            themMoi = false;
            txtMaMonAn.Enabled = cbbLoaiMonAn.Enabled = txtTenMonAn.Enabled = txtGia.Enabled = txtHinhAnh.Enabled = cboTrangThai.Enabled = true;
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = false;
            btnLuu.Enabled = true;

            dtgvMonAn.ReadOnly = false;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            int so;
            errorProvider1.Clear();
            if (txtTenMonAn.Text.Equals(""))
            {
                errorProvider1.SetError(txtTenMonAn, "không được để rỗng");
                return;
            }

            errorProvider2.Clear();
            if (txtGia.Text.Equals(""))
            {
                errorProvider2.SetError(txtGia, "không được để rỗng");
                return;
            }

            if (!int.TryParse(txtGia.Text, out so))
            {
                errorProvider2.SetError(txtGia, "Bạn phải nhập số");
                txtGia.Clear();
                return;
            }
            if (themMoi)
            {
                if (txtMaMonAn.Text != "" || cbbLoaiMonAn.Text != "" || txtTenMonAn.Text != "" || txtGia.Text != "" || txtHinhAnh.Text != "" || cboTrangThai.Text!="")
                {
                    int gia = int.Parse(txtGia.Text);
                    db.luuMonAn(dtMonAn, txtMaMonAn.Text, cbbLoaiMonAn.SelectedValue.ToString(), txtTenMonAn.Text, gia,txtHinhAnh.Text,cboTrangThai.SelectedItem.ToString());

                }
                MonAn_databiding();
            }
            else
            {
                dtgvMonAn.Refresh();
            }
            txtMaMonAn.Enabled = cbbLoaiMonAn.Enabled = txtTenMonAn.Enabled = txtGia.Enabled = txtHinhAnh.Enabled = cboTrangThai.Enabled = false;
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = true;
            btnLuu.Enabled = true;
            dtgvMonAn.AllowUserToAddRows = false;
            dtgvMonAn.ReadOnly = true;  
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DataTable dtNV_tam = null;
            if (MessageBox.Show("Bạn có chắc không", "Cảnh báo xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                == DialogResult.Yes)
            {
                dtNV_tam = db.LAYDULIEU("select distinct PK_iMonAnID from TBL_MONAN where PK_iMonAnID='" + txtMaMonAn.Text + "'");
                if (dtNV_tam.Rows.Count == 0)
                {
                    db.xoaMonAn(dtMonAn, txtMaMonAn.Text);
                }
                else
                    MessageBox.Show("Món Ăn tồn tại không xóa được");
            }
        }

        private void btnCapNhap_Click(object sender, EventArgs e)
        {
            try
            {
                db.CAPNHATDULIEU("select * from TBL_MONAN", dtMonAn);
                MessageBox.Show("Thêm thành công", "Thông Báo");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            dtgvMonAn.DataSource = db.LAYDULIEU("select * from TBL_MONAN where sTenMonAn like N'%" + txtTimKiem.Text + "%'");
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new BaoCaoMonAn().Show();
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }
    }
}
