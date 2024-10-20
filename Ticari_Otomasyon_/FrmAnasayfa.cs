using DevExpress.DataProcessing.InMemoryDataProcessor;
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
using System.Xml;

namespace Ticari_Otomasyon_
{
    public partial class FrmAnasayfa : Form
    {

        Sqlbaglantisi bgl = new Sqlbaglantisi();
        public FrmAnasayfa()
        {
            InitializeComponent();
        }

        private void FrmAnasayfa_Load(object sender, EventArgs e)
        {
            //Azalan Stoklar İçin
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter("SELECT URUNAD,SUM(ADET) AS 'ADET' FROM TBL_URUNLER GROUP BY URUNAD HAVING SUM(ADET)<=20 ORDER BY ADET ASC", bgl.baglanti());
            da1.Fill(dt1);
            gridControl1.DataSource = dt1;
            bgl.baglanti().Close();


            //Azalan Stoklar İçin
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("SELECT TARIH, SAAT, BASLIK FROM TBL_NOTLAR", bgl.baglanti());
            da2.Fill(dt2);
            gridControl2.DataSource = dt2;
            bgl.baglanti().Close();

            //Son 10 Hareket İçin
            DataTable dt3 = new DataTable();
            SqlDataAdapter da3 = new SqlDataAdapter("EXEC FIRMAHAREKETLER_SON10", bgl.baglanti());
            da3.Fill(dt3);
            gridControl3.DataSource = dt3;
            bgl.baglanti().Close();


            //En çok iş yapılan 10 firma
            DataTable dt4 = new DataTable();
            SqlDataAdapter da4 = new SqlDataAdapter("SELECT AD,TELEFON1 FROM TBL_FIRMALAR", bgl.baglanti());
            da4.Fill(dt4);
            gridControl4.DataSource = dt4;
            bgl.baglanti().Close();


            //web browser ekleme
            webView1.Url = "https://www.tcmb.gov.tr/wps/wcm/connect/tr/tcmb+tr/main+page+site+area/bugun";
            webView2.Url = "https://edition.cnn.com/";

            //Haberler için XML çekme
            XmlTextReader xtR = new XmlTextReader("https://bigpara.hurriyet.com.tr/SiteMap/news.xml"); 
            while(xtR.Read())
            {
                if(xtR.Name== "news:title")
                {
                    listBox1.Items.Add(xtR.ReadString());
                }
            }
        }
    }
}
