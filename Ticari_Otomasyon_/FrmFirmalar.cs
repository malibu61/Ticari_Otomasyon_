using DevExpress.XtraEditors;
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
    public partial class FrmFirmalar : Form
    {
        Sqlbaglantisi bgl = new Sqlbaglantisi();

        public FrmFirmalar()
        {
            InitializeComponent();
        }

        void cariKodAciklamalar()
        {
            SqlCommand komut = new SqlCommand("SELECT FIRMAKOD1 FROM TBL_KODLAR", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                RchKod1.Text = dr[0].ToString();
            }
            bgl.baglanti().Close();
        }

        void temizle()
        {
            Txtid.Text = "";
            TxtAd.Text = "";
            TxtSektor.Text = "";
            TxtYetkili.Text = "";
            TxtYetkiliGorev.Text = "";
            MskTC.Text = "";
            MskTelefon1.Text = "";
            MskTelefon2.Text = "";
            MskTelefon3.Text = "";
            MskFax.Text = "";
            TxtMail.Text = "";
            Cmbil.Text = "";
            Cmbilce.Text = "";
            TxtVergiDairesi.Text = "";
            RchAdres.Text = "";
            TxtKod1.Text = "";
            TxtKod2.Text = "";
            TxtKod3.Text = "";
        }

        void firmaListele()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM TBL_FIRMALAR", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
            bgl.baglanti().Close();
        }

        private void FrmFirmalar_Load(object sender, EventArgs e)
        {
            ilListele();
            firmaListele();
            temizle();
            cariKodAciklamalar();
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        void ilListele()
        {
            SqlCommand komut = new SqlCommand("SELECT SEHIR FROM TBL_ILLER", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                Cmbil.Properties.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();
        }


        void ilceListele()
        {
            Cmbilce.Properties.Items.Clear();
            Cmbilce.Text = "";
            SqlCommand komut = new SqlCommand("SELECT ILCE FROM TBL_ILCELER WHERE SEHIR=@P1", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", (Cmbil.SelectedIndex + 1).ToString());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                Cmbilce.Properties.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);

            if (dr != null)
            {
                Txtid.Text = dr[0].ToString();
                TxtAd.Text = dr[1].ToString();
                TxtYetkiliGorev.Text = dr[2].ToString();
                TxtYetkili.Text = dr[3].ToString();
                MskTC.Text = dr[4].ToString();
                TxtSektor.Text = dr[5].ToString();
                MskTelefon1.Text = dr[6].ToString();
                MskTelefon2.Text = dr[7].ToString();
                MskTelefon3.Text = dr[8].ToString();
                TxtMail.Text = dr[9].ToString();
                MskFax.Text = dr[10].ToString();
                Cmbil.Text = dr[11].ToString();
                Cmbilce.Text = dr[12].ToString();
                TxtVergiDairesi.Text = dr[13].ToString();
                RchAdres.Text = dr[14].ToString();
                TxtKod1.Text = dr[15].ToString();
                TxtKod2.Text = dr[16].ToString();
                TxtKod3.Text = dr[17].ToString();

            }

        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komutEkle = new SqlCommand("INSERT INTO TBL_FIRMALAR (AD,YETKILISTATU,YETKILIADSOYAD,YETKILITC,SEKTOR,TELEFON1,TELEFON2,TELEFON3,MAIL,FAX,IL,ILCE,VERGIDAIRE,ADRES,OZELKOD1,OZELKOD2,OZELKOD3) VALUES (@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9,@P10,@P11,@P12,@P13,@P14,@P15,@P16,@P17)", bgl.baglanti());

            komutEkle.Parameters.AddWithValue("@P1", TxtAd.Text);
            komutEkle.Parameters.AddWithValue("@P2", TxtYetkiliGorev.Text);
            komutEkle.Parameters.AddWithValue("@P3", TxtYetkili.Text);
            komutEkle.Parameters.AddWithValue("@P4", MskTC.Text);
            komutEkle.Parameters.AddWithValue("@P5", TxtSektor.Text);
            komutEkle.Parameters.AddWithValue("@P6", MskTelefon1.Text);
            komutEkle.Parameters.AddWithValue("@P7", MskTelefon2.Text);
            komutEkle.Parameters.AddWithValue("@P8", MskTelefon3.Text);
            komutEkle.Parameters.AddWithValue("@P9", TxtMail.Text);
            komutEkle.Parameters.AddWithValue("@P10", MskFax.Text);
            komutEkle.Parameters.AddWithValue("@P11", Cmbil.Text);
            komutEkle.Parameters.AddWithValue("@P12", Cmbilce.Text);
            komutEkle.Parameters.AddWithValue("@P13", TxtVergiDairesi.Text);
            komutEkle.Parameters.AddWithValue("@P14", RchAdres.Text);
            komutEkle.Parameters.AddWithValue("@P15", TxtKod1.Text);
            komutEkle.Parameters.AddWithValue("@P16", TxtKod2.Text);
            komutEkle.Parameters.AddWithValue("@P17", TxtKod3.Text);

            komutEkle.ExecuteNonQuery();
            bgl.baglanti().Close();
            firmaListele();
            MessageBox.Show("Firma Bilgisi Başarıyla Sisteme Eklendi", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);

            temizle();

        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void Cmbil_SelectedIndexChanged(object sender, EventArgs e)
        {
            ilceListele();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komutSilme = new SqlCommand("DELETE FROM TBL_FIRMALAR WHERE ID=@P1 ", bgl.baglanti());
            komutSilme.Parameters.AddWithValue("@P1", Txtid.Text);
            komutSilme.ExecuteNonQuery();
            bgl.baglanti().Close();
            temizle();
            MessageBox.Show("Silme İşleminiz Başarıyla Gerçekleşti", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            firmaListele();
            ilListele();


        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komutGuncelle = new SqlCommand("UPDATE TBL_FIRMALAR SET AD=@P1, YETKILISTATU=@P2, YETKILIADSOYAD=@P3, YETKILITC=@P4, SEKTOR=@P5, TELEFON1=@P6, TELEFON2=@P7, TELEFON3=@P8, MAIL=@P9, FAX=@P10, IL=@P11, ILCE=@P12, VERGIDAIRE=@P13, ADRES=@P14, OZELKOD1=@P15, OZELKOD2=@P16, OZELKOD3=@P17 WHERE ID=@PID", bgl.baglanti());

            komutGuncelle.Parameters.AddWithValue("@PID", Txtid.Text);
            komutGuncelle.Parameters.AddWithValue("@P1", TxtAd.Text);
            komutGuncelle.Parameters.AddWithValue("@P2", TxtYetkiliGorev.Text);
            komutGuncelle.Parameters.AddWithValue("@P3", TxtYetkili.Text);
            komutGuncelle.Parameters.AddWithValue("@P4", MskTC.Text);
            komutGuncelle.Parameters.AddWithValue("@P5", TxtSektor.Text);
            komutGuncelle.Parameters.AddWithValue("@P6", MskTelefon1.Text);
            komutGuncelle.Parameters.AddWithValue("@P7", MskTelefon2.Text);
            komutGuncelle.Parameters.AddWithValue("@P8", MskTelefon3.Text);
            komutGuncelle.Parameters.AddWithValue("@P9", TxtMail.Text);
            komutGuncelle.Parameters.AddWithValue("@P10", MskFax.Text);
            komutGuncelle.Parameters.AddWithValue("@P11", Cmbil.Text);
            komutGuncelle.Parameters.AddWithValue("@P12", Cmbilce.Text);
            komutGuncelle.Parameters.AddWithValue("@P13", TxtVergiDairesi.Text);
            komutGuncelle.Parameters.AddWithValue("@P14", RchAdres.Text);
            komutGuncelle.Parameters.AddWithValue("@P15", TxtKod1.Text);
            komutGuncelle.Parameters.AddWithValue("@P16", TxtKod2.Text);
            komutGuncelle.Parameters.AddWithValue("@P17", TxtKod3.Text);

            komutGuncelle.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Firma Bilgisi Başarıyla Güncellendi", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);

            firmaListele();
            temizle();
        }

        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            e.Appearance.BackColor = Color.White;

            e.Appearance.BackColor2 = Color.DarkCyan;
        }
    }
}
