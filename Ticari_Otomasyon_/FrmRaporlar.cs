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
    public partial class FrmRaporlar : Form
    {
        public FrmRaporlar()
        {
            InitializeComponent();
        }

        Sqlbaglantisi bgl = new Sqlbaglantisi();
        private void FrmRaporlar_Load(object sender, EventArgs e)
        {
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter("SELECT * FROM TBL_FIRMALAR", bgl.baglanti());
            da1.Fill(dt1);
            dataGridView1.DataSource = dt1;



            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("SELECT * FROM TBL_MUSTERILER", bgl.baglanti());
            da2.Fill(dt2);
            dataGridView2.DataSource = dt2;


            //DataTable dt4 = new DataTable();
            //SqlDataAdapter da4 = new SqlDataAdapter("SELECT * FROM TBL_KASA", bgl.baglanti());
            //da4.Fill(dt4);
            //dataGridView4.DataSource = dt4;


            DataTable dt3 = new DataTable();
            SqlDataAdapter da3 = new SqlDataAdapter("SELECT * FROM TBL_GIDERLER", bgl.baglanti());
            da3.Fill(dt3);
            dataGridView3.DataSource = dt3;

            DataTable dt5 = new DataTable();
            SqlDataAdapter da5 = new SqlDataAdapter("SELECT * FROM TBL_PERSONELLER", bgl.baglanti());
            da5.Fill(dt5);
            dataGridView5.DataSource = dt5;
        }
    }
}
