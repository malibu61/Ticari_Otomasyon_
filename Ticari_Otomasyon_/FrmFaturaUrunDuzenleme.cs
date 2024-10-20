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
    public partial class FrmFaturaUrunDuzenleme : Form
    {
        Sqlbaglantisi bgl = new Sqlbaglantisi();
        public FrmFaturaUrunDuzenleme()
        {
            InitializeComponent();
        }


        public string id, ad, miktar, fiyat, tutar, faturaid;

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komutSilme = new SqlCommand("DELETE FROM TBL_FATURADETAY WHERE FATURAURUNID=@PID", bgl.baglanti());
            komutSilme.Parameters.AddWithValue("@PID", TxtUrunid.Text.ToString());
            komutSilme.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show(TxtUrunid.Text.ToString() + " ID'li Fatura Detay Bilgisi Başarıyla Silindi.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);

            FrmFaturaUrunler frFaturaUrunler = new FrmFaturaUrunler();
            frFaturaUrunler.id = TxtFaturaid.Text.ToString();
            this.Hide();
            frFaturaUrunler.Show();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {

            SqlCommand komutGuncelle = new SqlCommand("UPDATE TBL_FATURADETAY SET URUNAD=@P2, MIKTAR=@P3, FIYAT=@P4, TUTAR=@P5,FATURAID=@P6 WHERE FATURAURUNID=@PID", bgl.baglanti());
            komutGuncelle.Parameters.AddWithValue("@PID", int.Parse(TxtUrunid.Text));
            komutGuncelle.Parameters.AddWithValue("@P2", TxtUrunAd.Text);
            komutGuncelle.Parameters.AddWithValue("@P3", int.Parse(TxtMiktar.Text));
            komutGuncelle.Parameters.AddWithValue("@P4", decimal.Parse(TxtFiyat.Text));

            tutar = (double.Parse(TxtFiyat.Text) * double.Parse(TxtMiktar.Text)).ToString(); //Tutar Hesabı

            komutGuncelle.Parameters.AddWithValue("@P5", decimal.Parse(tutar));

            komutGuncelle.Parameters.AddWithValue("@P6", TxtFaturaid.Text);
            komutGuncelle.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show(TxtUrunid.Text + " ID'li Fatura Detay Güncelleme İşlemi Başarılı.");

            FrmFaturaUrunler frFaturaUrunler = new FrmFaturaUrunler();
            frFaturaUrunler.id = TxtFaturaid.Text.ToString();
            //frFaturaUrunler.listeleFaturaUrunler();  //Çalışmadı
            //frFaturaUrunler.Refresh();  //Çalışmadı

            this.Hide();
            frFaturaUrunler.Show();
        }

        private void FrmFaturaUrunDuzenleme_Load(object sender, EventArgs e)
        {
            TxtUrunid.Text = id;
            TxtUrunAd.Text = ad;
            TxtMiktar.Text = miktar;
            TxtFiyat.Text = fiyat;
            TxtTutar.Text = tutar;
            TxtFaturaid.Text = faturaid;
        }
    }
}
