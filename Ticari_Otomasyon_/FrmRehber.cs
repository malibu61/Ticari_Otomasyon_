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

namespace Ticari_Otomasyon_
{
    public partial class FrmRehber : Form
    {
        Sqlbaglantisi bgl = new Sqlbaglantisi();

        public FrmRehber()
        {
            InitializeComponent();
        }

        private void FrmRehber_Load(object sender, EventArgs e)
        {
            //MÜŞTERİLER İÇİN
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT AD,SOYAD,TELEFON,TELEFON2,MAIL FROM TBL_MUSTERILER", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;

            //FİRMALAR İÇİN
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("SELECT YETKILIADSOYAD,YETKILISTATU,TELEFON1,TELEFON2,TELEFON3,MAIL,FAX FROM TBL_FIRMALAR", bgl.baglanti());
            da2.Fill(dt2);
            gridControl2.DataSource = dt2;

        }

        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            e.Appearance.BackColor = Color.White;

            e.Appearance.BackColor2 = Color.DarkCyan;
        }

        private void gridView2_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            e.Appearance.BackColor = Color.White;

            e.Appearance.BackColor2 = Color.DarkCyan;
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            FrmMailGonderme frMail = new FrmMailGonderme();
            DataRow dRow = gridView1.GetDataRow(gridView1.FocusedRowHandle);

            if (dRow != null)
            {
                frMail.mail = dRow["MAIL"].ToString();
            }

            frMail.Show();
        }

        private void gridControl2_DoubleClick(object sender, EventArgs e)
        {
            FrmMailGonderme frMail = new FrmMailGonderme();
            DataRow dRow = gridView2.GetDataRow(gridView2.FocusedRowHandle);

            if (dRow != null)
            {
                frMail.mail = dRow["MAIL"].ToString();
            }

            frMail.Show();
        }
    }
}
