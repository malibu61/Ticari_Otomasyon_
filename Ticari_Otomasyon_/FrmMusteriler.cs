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
    public partial class FrmMusteriler : Form
    {
        Sqlbaglantisi bgl = new Sqlbaglantisi();

        public FrmMusteriler()
        {
            InitializeComponent();
        }
        void musteriListele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM TBL_MUSTERILER", bgl.baglanti());
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

        void temizle()
        {
            Txtid.Text = string.Empty;
            TxtAd.Text = string.Empty;
            TxtSoyad.Text = string.Empty;
            MskTelefon1.Text = string.Empty;
            MskTelefon2.Text = string.Empty;
            MskTC.Text = string.Empty;
            TxtMail.Text = string.Empty;
            Cmbil.Text = string.Empty;
            Cmbilce.Text = string.Empty;
            RchAdres.Text = string.Empty;
            TxtVergiDairesi.Text = string.Empty;
        }

        private void FrmMusteriler_Load(object sender, EventArgs e)
        {
            musteriListele();
            ilListele();
        }

        private void Cmbil_SelectedIndexChanged(object sender, EventArgs e)
        {
            ilceListele();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komutGuncelle = new SqlCommand("INSERT INTO TBL_MUSTERILER (AD,SOYAD,TELEFON,TELEFON2,TC,MAIL,IL,ILCE,ADRES,VERGIDAIRE) VALUES  (@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9,@P10,@P11)", bgl.baglanti());

            komutGuncelle.Parameters.AddWithValue("@P2", TxtAd.Text);
            komutGuncelle.Parameters.AddWithValue("@P3", TxtSoyad.Text);
            komutGuncelle.Parameters.AddWithValue("@P4", MskTelefon1.Text);
            komutGuncelle.Parameters.AddWithValue("@P5", MskTelefon2.Text);
            komutGuncelle.Parameters.AddWithValue("@P6", MskTC.Text);
            komutGuncelle.Parameters.AddWithValue("@P7", TxtMail.Text);
            komutGuncelle.Parameters.AddWithValue("@P8", Cmbil.Text);
            komutGuncelle.Parameters.AddWithValue("@P9", Cmbilce.Text);
            komutGuncelle.Parameters.AddWithValue("@P10", RchAdres.Text);
            komutGuncelle.Parameters.AddWithValue("@P11", TxtVergiDairesi.Text);

            komutGuncelle.ExecuteNonQuery();
            MessageBox.Show("Yeni Müşteri Ekleme İşlemi Başarılı");
            musteriListele();
            bgl.baglanti().Close();


        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dRow = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dRow != null)
            {
                Txtid.Text = dRow["ID"].ToString();
                TxtAd.Text = dRow["AD"].ToString();
                TxtSoyad.Text = dRow["SOYAD"].ToString();
                MskTelefon1.Text = dRow["TELEFON"].ToString();
                MskTelefon2.Text = dRow["TELEFON2"].ToString();
                MskTC.Text = dRow["TC"].ToString();
                TxtMail.Text = dRow["MAIL"].ToString();
                Cmbil.Text = dRow["IL"].ToString();
                Cmbilce.Text = dRow["ILCE"].ToString();
                RchAdres.Text = dRow["ADRES"].ToString();
                TxtVergiDairesi.Text = dRow["VERGIDAIRE"].ToString();
            }
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {

            DialogResult soru;
            soru = MessageBox.Show(Txtid.Text + " ID'li Veriyi Silmek İstediğinizden Emin Misiniz?", "Onaylama", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (soru == DialogResult.Yes)
            {
                SqlCommand komutsilme = new SqlCommand("DELETE FROM TBL_MUSTERILER WHERE ID=@P1", bgl.baglanti());
                komutsilme.Parameters.AddWithValue("@P1", Txtid.Text);
                komutsilme.ExecuteNonQuery();
                bgl.baglanti().Close();

                MessageBox.Show("Silme İşlemi Başarılı", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            musteriListele();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komutGuncelle = new SqlCommand("UPDATE TBL_MUSTERILER SET AD=@p1,SOYAD=@p2, TELEFON=@p3,TELEFON2=@p4,TC=@p5," +
                "MAIL=@p6,IL=@p7,ILCE=@p8,ADRES=@p9,VERGIDAIRE=@p10  WHERE ID=@pID", bgl.baglanti());

            komutGuncelle.Parameters.AddWithValue("@pID", int.Parse(Txtid.Text));
            komutGuncelle.Parameters.AddWithValue("@p1", TxtAd.Text);
            komutGuncelle.Parameters.AddWithValue("@p2", TxtSoyad.Text);
            komutGuncelle.Parameters.AddWithValue("@p3", MskTelefon1.Text);
            komutGuncelle.Parameters.AddWithValue("@p4", MskTelefon2.Text);
            komutGuncelle.Parameters.AddWithValue("@p5", MskTC.Text);
            komutGuncelle.Parameters.AddWithValue("@p6", TxtMail.Text);
            komutGuncelle.Parameters.AddWithValue("@p7", Cmbil.Text);
            komutGuncelle.Parameters.AddWithValue("@p8", Cmbilce.Text);
            komutGuncelle.Parameters.AddWithValue("@p9", RchAdres.Text);
            komutGuncelle.Parameters.AddWithValue("@p10", TxtVergiDairesi.Text);

            komutGuncelle.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show(Txtid.Text + " ID'li Veri Güncellendi", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);

            musteriListele();
        }

        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            e.Appearance.BackColor = Color.White;

            e.Appearance.BackColor2 = Color.DarkCyan;
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }
    }
}
