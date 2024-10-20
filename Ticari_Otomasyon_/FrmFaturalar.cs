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
    public partial class FrmFaturalar : Form
    {
        Sqlbaglantisi bgl = new Sqlbaglantisi();
        public FrmFaturalar()
        {
            InitializeComponent();
        }

        void faturaBilgiListele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM TBL_FATURABILGI", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        private void FrmFaturalar_Load(object sender, EventArgs e)
        {
            faturaBilgiListele();
        }

        void temizle()
        {
            Txtid.Text = "";
            TxtSeri.Text = "";
            TxtSiraNo.Text = "";
            MskTarih.Text = "";
            MskSaat.Text = "";
            TxtVergiDaire.Text = "";
            TxtAlici.Text = "";
            TxtTeslimAlan.Text = "";
            TxtTeslimEden.Text = "";

        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            if (TxtPersonel.Text != string.Empty)
            {
                SqlCommand komutEkle = new SqlCommand("INSERT INTO TBL_FATURABILGI (SERI, SIRANO, TARIH, SAAT, VERGIDAIRE, ALICI, TESLIMEDEN, TESLIMALAN) VALUES (@P1, @P2, @P3, @P4, @P5, @P6, @P7, @P8)", bgl.baglanti());

                komutEkle.Parameters.AddWithValue("@P1", TxtSeri.Text);
                komutEkle.Parameters.AddWithValue("@P2", TxtSiraNo.Text);
                komutEkle.Parameters.AddWithValue("@P3", MskTarih.Text);
                komutEkle.Parameters.AddWithValue("@P4", MskSaat.Text);
                komutEkle.Parameters.AddWithValue("@P5", TxtVergiDaire.Text);
                komutEkle.Parameters.AddWithValue("@P6", TxtAlici.Text);
                komutEkle.Parameters.AddWithValue("@P7", TxtTeslimEden.Text);
                komutEkle.Parameters.AddWithValue("@P8", TxtTeslimAlan.Text);

                komutEkle.ExecuteNonQuery();
                bgl.baglanti().Close();

                temizle();
                MessageBox.Show("Fatura Bilgisi Başarıyla Eklendi.");
                faturaBilgiListele();

            }
            if (TxtPersonel.Text == string.Empty)
            {
                double miktar, tutar, fiyat;
                miktar = double.Parse(TxtMiktar.Text);
                fiyat = double.Parse(TxtFiyat.Text);
                tutar = miktar * fiyat;

                SqlCommand komutEkle = new SqlCommand("INSERT INTO TBL_FIRMAHAREKETLER (URUNID, ADET,PERSONEL,FIRMA, FIYAT,TOPLAM, FATURAID, TARIH) VALUES (@P1, @P2, @P3, @P4, @P5, @P6, @P7, @P8)", bgl.baglanti());

                komutEkle.Parameters.AddWithValue("@P1", TxtUrunid.Text);
                komutEkle.Parameters.AddWithValue("@P2", TxtMiktar.Text);
                komutEkle.Parameters.AddWithValue("@P3", TxtPersonel.Text);
                komutEkle.Parameters.AddWithValue("@P3", TxtFirma.Text);
                komutEkle.Parameters.AddWithValue("@P3", TxtFiyat.Text);
                komutEkle.Parameters.AddWithValue("@P3", tutar);
                komutEkle.Parameters.AddWithValue("@P4", TxtFaturaid.Text);
                komutEkle.Parameters.AddWithValue("@P5", mskTarihh.Text);

                komutEkle.ExecuteNonQuery();
                bgl.baglanti().Close();
                temizle();
                MessageBox.Show("Toplam: " + tutar.ToString() + "TL'lik Fatura Detay Başarıyla Eklendi.");

                //Stok sayısını azaltma
                SqlCommand cmdStokSayisi = new SqlCommand("UPDATE TBL_URUNLER SET ADET=ADET-@S1 WHERE ID@S2");
                cmdStokSayisi.Parameters.AddWithValue("@S1", TxtMiktar.Text);
                cmdStokSayisi.Parameters.AddWithValue("@S2", TxtUrunid.Text);
                cmdStokSayisi.ExecuteNonQuery();
                bgl.baglanti().Close();

            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dRow = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dRow != null)
            {
                Txtid.Text = dRow[0].ToString();
                TxtSeri.Text = dRow[1].ToString();
                TxtSiraNo.Text = dRow[2].ToString();
                MskTarih.Text = dRow[3].ToString();
                MskSaat.Text = dRow[4].ToString();
                TxtVergiDaire.Text = dRow[5].ToString();
                TxtAlici.Text = dRow[6].ToString();
                TxtTeslimAlan.Text = dRow[7].ToString();
                TxtTeslimEden.Text = dRow[8].ToString();
            }
        }

        private void BtnTemizle_Click_1(object sender, EventArgs e)
        {
            temizle();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komutSilme = new SqlCommand("DELETE FROM TBL_FATURABILGI WHERE FATURABILGIID=@P1", bgl.baglanti());
            komutSilme.Parameters.AddWithValue("@P1", Txtid.Text);
            komutSilme.ExecuteNonQuery();
            bgl.baglanti().Close();

            MessageBox.Show("Fatura Silme İşlemi Başarılı Şekilde Gerçekleşti.");
            faturaBilgiListele();
            temizle();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komutGuncelle = new SqlCommand("UPDATE TBL_FATURABILGI SET SERI=@P1, SIRANO=@P2, TARIH=@P3, SAAT=@P4, VERGIDAIRE=@P5, ALICI=@P6, TESLIMEDEN=@P7, TESLIMALAN=@P8 WHERE FATURABILGIID=@PID", bgl.baglanti());

            komutGuncelle.Parameters.AddWithValue("@PID", Txtid.Text);
            komutGuncelle.Parameters.AddWithValue("@P1", char.Parse(TxtSeri.Text));
            komutGuncelle.Parameters.AddWithValue("@P2", TxtSiraNo.Text);
            komutGuncelle.Parameters.AddWithValue("@P3", MskTarih.Text);
            komutGuncelle.Parameters.AddWithValue("@P4", MskSaat.Text);
            komutGuncelle.Parameters.AddWithValue("@P5", TxtVergiDaire.Text);
            komutGuncelle.Parameters.AddWithValue("@P6", TxtAlici.Text);
            komutGuncelle.Parameters.AddWithValue("@P7", TxtTeslimEden.Text);
            komutGuncelle.Parameters.AddWithValue("@P8", TxtTeslimAlan.Text);
            komutGuncelle.ExecuteNonQuery();
            bgl.baglanti().Close();

            MessageBox.Show("FaturaBilgi Güncelleme İşlemi Başarılı.");
            faturaBilgiListele();
            temizle();
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            FrmFaturaUrunler frFaturaUrunler = new FrmFaturaUrunler();

            DataRow dRow = gridView1.GetDataRow(gridView1.FocusedRowHandle);

            if (dRow != null)
            {
                frFaturaUrunler.id = dRow["FATURABILGIID"].ToString();
                frFaturaUrunler.Show();
                FrmFaturaUrunDuzenleme frFaturaUrunDuzenleme = new FrmFaturaUrunDuzenleme();

            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (TxtUrunid.Text != string.Empty)
            {
                SqlCommand cmdBul = new SqlCommand("SELECT * FROM TBL_URUNLER WHERE ID='" + TxtUrunid.Text + "'", bgl.baglanti());
                SqlDataReader drBul = cmdBul.ExecuteReader();
                while (drBul.Read())
                {
                    TxtFiyat.Text = drBul["SATISFIYAT"].ToString();
                    TxtMiktar.Text = (1).ToString();
                }
            }
        }
    }
}
