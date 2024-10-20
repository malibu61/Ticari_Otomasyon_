using DevExpress.RichEdit.Export;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Charts;

namespace Ticari_Otomasyon_
{


    public partial class FrmKasa : Form
    {
        public string FrmKasa_aktifKullanici;
        public FrmKasa()
        {
            InitializeComponent();
        }

        Sqlbaglantisi bgl = new Sqlbaglantisi();

        void firmaHareketler()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("EXEC FIRMAHAREKETLER", bgl.baglanti());
            da.Fill(dt);
            gridControlGiris1.DataSource = dt;
            bgl.baglanti().Close();
        }

        void muteriHareketler()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("EXEC MUSTERIHAREKETLER", bgl.baglanti());
            da.Fill(dt);
            gridControlGiris2.DataSource = dt;
            bgl.baglanti().Close();
        }

        private void FrmKasa_Load(object sender, EventArgs e)
        {
            firmaHareketler();
            muteriHareketler();

            //Toplam Tutar
            SqlCommand cmd1 = new SqlCommand("SELECT SUM(TUTAR) FROM TBL_FATURADETAY", bgl.baglanti());
            SqlDataReader dr1 = cmd1.ExecuteReader();
            while (dr1.Read())
            {
                lblToplamTutar.Text = dr1[0].ToString();
            }
            bgl.baglanti().Close();

            //Son Ay İçin Ödemeler
            SqlCommand cmd2 = new SqlCommand("SELECT TOP(1)(ELEKTRIK+SU+DOGALGAZ+INTERNET+MAASLAR+EKSTRA) FROM TBL_GIDERLER ORDER BY ID DESC", bgl.baglanti());
            SqlDataReader dr2 = cmd2.ExecuteReader();
            while (dr2.Read())
            {
                lblOdemeler.Text = dr2[0].ToString();
            }
            bgl.baglanti().Close();

            //Son Ay İçin Personel Maaşları
            SqlCommand cmd3 = new SqlCommand("SELECT TOP(1)MAASLAR FROM TBL_GIDERLER ORDER BY ID DESC", bgl.baglanti());
            SqlDataReader dr3 = cmd3.ExecuteReader();
            while (dr3.Read())
            {
                lblPersonelMaaslari.Text = dr3[0].ToString();
            }
            bgl.baglanti().Close();

            //Müşteri Sayısı
            SqlCommand cmd4 = new SqlCommand("SELECT COUNT(*) FROM TBL_MUSTERILER", bgl.baglanti());
            SqlDataReader dr4 = cmd4.ExecuteReader();
            while (dr4.Read())
            {
                lblMusteriSayisi.Text = dr4[0].ToString();
            }
            bgl.baglanti().Close();

            //Firma Sayısı
            SqlCommand cmd5 = new SqlCommand("SELECT COUNT(*) FROM TBL_FIRMALAR", bgl.baglanti());
            SqlDataReader dr5 = cmd5.ExecuteReader();
            while (dr5.Read())
            {
                lblFirmaSayisi.Text = dr5[0].ToString();
            }
            bgl.baglanti().Close();

            //Şehir Sayısı
            SqlCommand cmd6 = new SqlCommand("SELECT COUNT(DISTINCT(IL)) FROM TBL_FIRMALAR ", bgl.baglanti());
            SqlDataReader dr6 = cmd6.ExecuteReader();
            while (dr6.Read())
            {
                lblSehirSayisi.Text = dr6[0].ToString();
            }
            bgl.baglanti().Close();


            //Personel Sayısı
            SqlCommand cmd7 = new SqlCommand("SELECT COUNT(*) FROM TBL_PERSONELLER", bgl.baglanti());
            SqlDataReader dr7 = cmd7.ExecuteReader();
            while (dr7.Read())
            {
                lblPersonelSayisi.Text = dr7[0].ToString();
            }
            bgl.baglanti().Close();


            //Stok Sayısı
            SqlCommand cmd8 = new SqlCommand("SELECT SUM(ADET) FROM TBL_URUNLER", bgl.baglanti());
            SqlDataReader dr8 = cmd8.ExecuteReader();
            while (dr8.Read())
            {
                lblStokSayisi.Text = dr8[0].ToString();
            }
            bgl.baglanti().Close();


            //Aktif Kullanıcı
            lblAktifKullanici.Text = FrmKasa_aktifKullanici;


            //Chart'a giderlerin listelenmesi
            SqlCommand cmdChartGiderler = new SqlCommand("SELECT TOP 1 'ELEKTRIK','SU','DOGALGAZ','INTERNET',ELEKTRIK,SU,DOGALGAZ,INTERNET FROM TBL_GIDERLER ORDER BY ID DESC", bgl.baglanti());
            SqlDataReader okuChartGiderler = cmdChartGiderler.ExecuteReader();
            while (okuChartGiderler.Read())
            {
                for (int i = 0; i <= 3; i++)
                {
                    chart1.Series["Giderler"].Points.AddXY(okuChartGiderler[i].ToString(), okuChartGiderler[i + 4]);
                }
            }
        }

        //Chart'ların timer'a bağlı değişmesi
        int sayac = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            sayac++;

            if (sayac > 0 && sayac <= 5)
            {
                chartControl1.Series["Aylar"].Points.Clear();
                chartControl2.Series["Aylar"].Points.Clear();

                //chartControl1'in değişmesi
                SqlCommand cmd9 = new SqlCommand("SELECT TOP(4) AY,ELEKTRIK FROM TBL_GIDERLER ORDER BY ID DESC", bgl.baglanti());
                SqlDataReader dr9 = cmd9.ExecuteReader();
                while (dr9.Read())
                {
                    chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr9[0], dr9[1]));
                }
                bgl.baglanti().Close();
                lblChartControl1.Text = "Elektrik";


                //chartControl2'nin değişmesi
                SqlCommand cmd10 = new SqlCommand("SELECT TOP(4) AY,DOGALGAZ FROM TBL_GIDERLER ORDER BY ID DESC", bgl.baglanti());
                SqlDataReader dr10 = cmd10.ExecuteReader();
                while (dr10.Read())
                {
                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr10[0], dr10[1]));
                }
                bgl.baglanti().Close();
                lblChartControl2.Text = "Doğalgaz";
            }


            if (sayac > 5 && sayac <= 10)
            {
                chartControl1.Series["Aylar"].Points.Clear();
                chartControl2.Series["Aylar"].Points.Clear();

                //chartControl1'in değişmesi
                SqlCommand cmd11 = new SqlCommand("SELECT TOP(4) AY,SU FROM TBL_GIDERLER ORDER BY ID DESC", bgl.baglanti());
                SqlDataReader dr11 = cmd11.ExecuteReader();
                while (dr11.Read())
                {
                    chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));
                }
                bgl.baglanti().Close();
                lblChartControl1.Text = "Su";


                //chartControl2'nin değişmesi
                SqlCommand cmd12 = new SqlCommand("SELECT TOP(4) AY,EKSTRA FROM TBL_GIDERLER ORDER BY ID DESC", bgl.baglanti());
                SqlDataReader dr12 = cmd12.ExecuteReader();
                while (dr12.Read())
                {
                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr12[0], dr12[1]));
                }
                bgl.baglanti().Close();
                lblChartControl2.Text = "Ekstra";
            }

            if (sayac == 11)
            {
                sayac = 0;
            }
        }

    }
}
