using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ticari_Otomasyon_
{
    public partial class FrmNotlar : Form
    {
        Sqlbaglantisi bgl = new Sqlbaglantisi();
        public FrmNotlar()
        {
            InitializeComponent();
        }

        void listeleNotlar()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM TBL_NOTLAR", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void temizle()
        {
            Txtid.Text = string.Empty;
            MskTarih.Text = string.Empty;
            MskSaat.Text = string.Empty;
            TxtBaslik.Text = string.Empty;
            TxtOlusturan.Text = string.Empty;
            TxtHitap.Text = string.Empty;
            RchDetay.Text = string.Empty;
        }
        private void FrmNotlar_Load(object sender, EventArgs e)
        {
            listeleNotlar();
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komutEkle = new SqlCommand("INSERT INTO TBL_NOTLAR (TARIH, SAAT, BASLIK, DETAY, OLUSTURAN, HITAP) VALUES (@P1, @P2, @P3, @P4, @P5, @P6)", bgl.baglanti());
            komutEkle.Parameters.AddWithValue("@P1", MskTarih.Text);
            komutEkle.Parameters.AddWithValue("@P2", MskSaat.Text);
            komutEkle.Parameters.AddWithValue("@P3", TxtBaslik.Text);
            komutEkle.Parameters.AddWithValue("@P4", RchDetay.Text);
            komutEkle.Parameters.AddWithValue("@P5", TxtOlusturan.Text);
            komutEkle.Parameters.AddWithValue("@P6", RchDetay.Text);
            komutEkle.ExecuteNonQuery();
            bgl.baglanti().Close();

            MessageBox.Show("Not Başarıyla Sisteme Eklendi.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listeleNotlar();
            temizle();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komutSilme = new SqlCommand("DELETE FROM TBL_NOTLAR WHERE ID=@P1", bgl.baglanti());
            komutSilme.Parameters.AddWithValue("@P1", Txtid.Text);
            komutSilme.ExecuteNonQuery();
            bgl.baglanti().Close();

            MessageBox.Show("Not Silme İşlemi Başarılı.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            listeleNotlar();
            temizle();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dRow = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dRow != null)
            {
                Txtid.Text = dRow[0].ToString();
                MskTarih.Text = dRow[1].ToString();
                MskSaat.Text = dRow[2].ToString();
                TxtBaslik.Text = dRow[3].ToString();
                RchDetay.Text = dRow[4].ToString();
                TxtOlusturan.Text = dRow[5].ToString();
                TxtHitap.Text = dRow[6].ToString();
            }
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komutGuncelle = new SqlCommand("UPDATE TBL_NOTLAR SET TARIH=@P1, SAAT=@P2, BASLIK=@P3, DETAY=@P4, OLUSTURAN=@P5, HITAP=@P6 WHERE ID=@PID", bgl.baglanti());
            komutGuncelle.Parameters.AddWithValue("@PID", Txtid.Text);
            komutGuncelle.Parameters.AddWithValue("@P1", MskTarih.Text);
            komutGuncelle.Parameters.AddWithValue("@P2", MskSaat.Text);
            komutGuncelle.Parameters.AddWithValue("@P3", TxtBaslik.Text);
            komutGuncelle.Parameters.AddWithValue("@P4", RchDetay.Text);
            komutGuncelle.Parameters.AddWithValue("@P5", TxtOlusturan.Text);
            komutGuncelle.Parameters.AddWithValue("@P6", RchDetay.Text);
            komutGuncelle.ExecuteNonQuery();
            bgl.baglanti().Close();

            MessageBox.Show("Not Güncelleme İşlemi Başarıyla Gerçekleşti.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listeleNotlar();
            temizle();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            DataRow dRow = gridView1.GetDataRow(gridView1.FocusedRowHandle);

            FrmNotDetay frNotDetay = new FrmNotDetay();
            frNotDetay.detayaciklama = dRow["DETAY"].ToString();
            frNotDetay.Show();
        }
    }
}
