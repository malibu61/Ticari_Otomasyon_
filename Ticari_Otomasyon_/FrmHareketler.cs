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
    public partial class FrmHareketler : Form
    {

        Sqlbaglantisi bgl = new Sqlbaglantisi();

        public FrmHareketler()
        {
            InitializeComponent();
        }

        void MusteriHareketlerListele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("EXEC MUSTERIHAREKETLER", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void FirmaHareketlerListele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("EXEC FIRMAHAREKETLER", bgl.baglanti());
            da.Fill(dt);
            gridControl2.DataSource = dt;
        }
        private void FrmHareketler_Load(object sender, EventArgs e)
        {
            MusteriHareketlerListele();
            FirmaHareketlerListele();

        }
    }
}
