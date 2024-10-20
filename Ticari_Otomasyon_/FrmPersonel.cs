using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ticari_Otomasyon_
{
    public partial class FrmPersonel : Form
    {
        Sqlbaglantisi bgl = new Sqlbaglantisi();
        public FrmPersonel()
        {
            InitializeComponent();
        }

        void personelListele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM TBL_PERSONELLER", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void ilListele()
        {
            SqlCommand komut = new SqlCommand("SELECT SEHIR FROM TBL_ILLER", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                Cmbil.Properties.Items.Add(dr[0]);
            }
        }

        void ilceListele()
        {
            Cmbilce.Properties.Items.Clear();
            SqlCommand komut = new SqlCommand("SELECT ILCE FROM TBL_ILCELER WHERE SEHIR=@P1", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", (Cmbil.SelectedIndex + 1));
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                Cmbilce.Properties.Items.Add(dr[0]);
            }

        }

        void temizle()
        {
            Txtid.Text = "";
            TxtAd.Text = "";
            TxtSoyad.Text = "";
            MskTelefon.Text = "";
            MskTC.Text = "";
            TxtMail.Text = "";
            Cmbil.Text = "";
            Cmbilce.Text = "";
            RchAdres.Text = "";
            TxtGorev.Text = "";
        }

        private void FrmPersonel_Load(object sender, EventArgs e)
        {
            personelListele();
            ilListele();
        }

        private void Cmbil_SelectedIndexChanged(object sender, EventArgs e)
        {
            ilceListele();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komutEkle = new SqlCommand("INSERT INTO TBL_PERSONELLER (AD, SOYAD, TELEFON, TC, MAIL, IL, ILCE, ADRES, GOREV) VALUES (@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9)", bgl.baglanti());
            komutEkle.Parameters.AddWithValue("@P1", TxtAd.Text);
            komutEkle.Parameters.AddWithValue("@P2", TxtSoyad.Text);
            komutEkle.Parameters.AddWithValue("@P3", MskTelefon.Text);
            komutEkle.Parameters.AddWithValue("@P4", MskTC.Text);
            komutEkle.Parameters.AddWithValue("@P5", TxtMail.Text);
            komutEkle.Parameters.AddWithValue("@P6", Cmbil.Text);
            komutEkle.Parameters.AddWithValue("@P7", Cmbilce.Text);
            komutEkle.Parameters.AddWithValue("@P8", RchAdres.Text);
            komutEkle.Parameters.AddWithValue("@P9", TxtGorev.Text);
            komutEkle.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Personel Sisteme Başarıyla Kaydedilmiştir.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            personelListele();
            temizle();
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dRow = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dRow != null)
            {
                Txtid.Text = dRow["ID"].ToString();
                TxtAd.Text = dRow["AD"].ToString();
                TxtSoyad.Text = dRow["SOYAD"].ToString();
                MskTelefon.Text = dRow["TELEFON"].ToString();
                MskTC.Text = dRow["TC"].ToString();
                TxtMail.Text = dRow["MAIL"].ToString();
                Cmbil.Text = dRow["IL"].ToString();
                Cmbilce.Text = dRow["ILCE"].ToString();
                RchAdres.Text = dRow["ADRES"].ToString();
                TxtGorev.Text = dRow["GOREV"].ToString();
            }
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komutGuncelle = new SqlCommand("UPDATE TBL_PERSONELLER SET AD=@P1, SOYAD=@P2, TELEFON=@P3, TC=@P4, MAIL=@P5, IL=@P6, ILCE=@P7, ADRES=@P8, GOREV=@P9 WHERE ID=@PID", bgl.baglanti());

            komutGuncelle.Parameters.AddWithValue("@PID", Txtid.Text);
            komutGuncelle.Parameters.AddWithValue("@P1", TxtAd.Text);
            komutGuncelle.Parameters.AddWithValue("@P2", TxtSoyad.Text);
            komutGuncelle.Parameters.AddWithValue("@P3", MskTelefon.Text);
            komutGuncelle.Parameters.AddWithValue("@P4", MskTC.Text);
            komutGuncelle.Parameters.AddWithValue("@P5", TxtMail.Text);
            komutGuncelle.Parameters.AddWithValue("@P6", Cmbil.Text);
            komutGuncelle.Parameters.AddWithValue("@P7", Cmbilce.Text);
            komutGuncelle.Parameters.AddWithValue("@P8", RchAdres.Text);
            komutGuncelle.Parameters.AddWithValue("@P9", TxtGorev.Text);

            komutGuncelle.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Personel Bilgisi Başarıyla Güncellendi", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);

            personelListele();
            temizle();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komutSilme = new SqlCommand("DELETE FROM TBL_PERSONELLER WHERE ID=@P1", bgl.baglanti());
            komutSilme.Parameters.AddWithValue("@P1", Txtid.Text);
            komutSilme.ExecuteNonQuery();
            bgl.baglanti().Close();

            MessageBox.Show(Txtid.Text+" ID'sine Sahip Personeli Silme İşlemi Başarıyla Gerçekleşti.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.None);
            temizle();
            personelListele();
        }

        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            e.Appearance.BackColor = Color.White;

            e.Appearance.BackColor2 = Color.DarkCyan;
        }
    }
}
