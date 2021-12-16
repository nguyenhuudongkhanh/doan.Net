using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Nhom9_QuanLyNhaHang
{
    public partial class DangNhapHeThong : Form
    {
        KetNoiDatabase DB = new KetNoiDatabase();
        Appsetting appset = new Appsetting();
        float X, Y;
        bool firsttime = true;

        public DangNhapHeThong()
        {
            InitializeComponent();
        }

        string constring;
        private void btndangnhap_Click(object sender, EventArgs e)
        {
            constring = string.Format("server={0};database={1};Integrated Security=", txtsever.Text, txttendb.Text);
            if (txttendangnhap.Text == "")
                constring += "true";
            else
                constring += string.Format("false;uid={0};pwd={1}", txttendangnhap.Text, txtps.Text);
            if (DB.testConnection(constring))
            {
                MessageBox.Show("ThanhCong");
                
            }
            else
                MessageBox.Show("fali");
        }

        private void btnluu_Click(object sender, EventArgs e)
        {
            if (constring != "")
            {

                appset.setconenctionstring("Nhom9_QuanLyNhaHang.Properties.Settings.QL_NHAHANG_3ConnectionString", constring);
                MessageBox.Show("save");
                new FormDangNhap().Show();
            }
        }

        private void DangNhapHeThong_Load(object sender, EventArgs e)
        {

        }

        private void DangNhapHeThong_Resize(object sender, EventArgs e)
        {
            if (!firsttime)
            {
                float newX = this.Width / X;
                float newY = this.Height / Y;
                setControls(newX, newY, this);
            }
            firsttime = false;
        }
        private void setControls(float newX, float newY, Control cons)
        {
            foreach (Control con in cons.Controls)
            {
                string[] mytag = con.Tag.ToString().Split(new char[] { ':' });
                float a = Convert.ToSingle(mytag[0]) * newX;
                con.Width = (int)(a);

                a = Convert.ToSingle(mytag[1]) * newY;
                con.Height = (int)(a);

                a = Convert.ToSingle(mytag[2]) * newX;
                con.Left = (int)(a);

                a = Convert.ToSingle(mytag[3]) * newY;
                con.Top = (int)(a);

                Single currentSize = Convert.ToSingle(mytag[4]) * newY;
                con.Font = new Font(con.Font.Name, currentSize, con.Font.Style, con.Font.Unit);
            }
        }
    }
}
