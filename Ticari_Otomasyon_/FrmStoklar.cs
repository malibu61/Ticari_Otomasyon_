using DevExpress.XtraCharts;
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
    public partial class FrmStoklar : Form
    {
        public FrmStoklar()
        {
            InitializeComponent();
        }

        Sqlbaglantisi bgl = new Sqlbaglantisi();

        private void FrmStoklar_Load(object sender, EventArgs e)
        {
            //    chartControl1.Series["Series 1"].Points.AddPoint("İstanbul", 35);
            //    chartControl1.Series["Series 1"].Points.AddPoint("İzmir", 30);
            //    chartControl1.Series["Series 1"].Points.AddPoint("Ankara", 15);
            //    chartControl1.Series["Series 1"].Points.AddPoint("Manisa", 20);


            SqlDataAdapter da = new SqlDataAdapter("SELECT URUNAD AS 'ÜRÜN',SUM(ADET) AS 'ADET' FROM TBL_URUNLER GROUP BY URUNAD", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;




            //Chart'a ürün adedini çekme
            SqlCommand cmd = new SqlCommand("SELECT URUNAD AS 'ÜRÜN',SUM(ADET) AS 'ADET' FROM TBL_URUNLER GROUP BY URUNAD", bgl.baglanti());
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                chartControl1.Series["Series 1"].Points.AddPoint(Convert.ToString(dr[0]),
                    int.Parse(dr[1].ToString()));
            }


            //Chart'a şehir sayısını çekme
            SqlCommand cmd2 = new SqlCommand("SELECT IL,COUNT(IL) AS 'FİRMA SAYISI' FROM TBL_FIRMALAR GROUP BY IL ORDER BY COUNT(IL) DESC", bgl.baglanti());
            SqlDataReader dr2 = cmd2.ExecuteReader();
            while (dr2.Read())
            {
                chartControl2.Series["Series 1"].Points.AddPoint((dr2[0].ToString()), int.Parse(dr2[1].ToString()));
            }
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            FrmStokDetay frStokDetay = new FrmStokDetay();
            DataRow dRow = gridView1.GetDataRow(gridView1.FocusedRowHandle);

            if (dRow != null)
            {
                frStokDetay.ad = dRow["ÜRÜN"].ToString();
            }
            frStokDetay.Show();
        }
    }
}
