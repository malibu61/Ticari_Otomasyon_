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
    public partial class FrmGiderler : Form
    {
        Sqlbaglantisi bgl = new Sqlbaglantisi();
        public FrmGiderler()
        {
            InitializeComponent();
        }

        void giderListele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM TBL_GIDERLER ORDER BY ID ASC", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
            bgl.baglanti().Close();
        }

        void temizle()
        {
            Txtid.Text = string.Empty;
            CmbAy.Text = string.Empty;
            CmbYil.Text = string.Empty;
            TxtElektrik.Text = string.Empty;
            TxtSu.Text = string.Empty;
            TxtDogalgaz.Text = string.Empty;
            TxtInternet.Text = string.Empty;
            TxtMaaslar.Text = string.Empty;
            TxtEkstra.Text = string.Empty;
            RchNotlar.Text = string.Empty;

        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        private void FrmGiderler_Load(object sender, EventArgs e)
        {
            giderListele();
        }

        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            e.Appearance.BackColor = Color.White;

            e.Appearance.BackColor2 = Color.DarkCyan;
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komutEkle = new SqlCommand("INSERT INTO TBL_GIDERLER (AY, YIL, ELEKTRIK, SU, DOGALGAZ, INTERNET, MAASLAR, EKSTRA, NOTLAR) VALUES (@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9)", bgl.baglanti());
            komutEkle.Parameters.AddWithValue("@P1", CmbAy.Text);
            komutEkle.Parameters.AddWithValue("@P2", CmbYil.Text);
            komutEkle.Parameters.AddWithValue("@P3", decimal.Parse(TxtElektrik.Text));
            komutEkle.Parameters.AddWithValue("@P4", decimal.Parse(TxtSu.Text));
            komutEkle.Parameters.AddWithValue("@P5", decimal.Parse(TxtDogalgaz.Text));
            komutEkle.Parameters.AddWithValue("@P6", decimal.Parse(TxtInternet.Text));
            komutEkle.Parameters.AddWithValue("@P7", decimal.Parse(TxtMaaslar.Text));
            komutEkle.Parameters.AddWithValue("@P8", decimal.Parse(TxtEkstra.Text));
            komutEkle.Parameters.AddWithValue("@P9", RchNotlar.Text);

            komutEkle.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Gider Ekleme İşlemi Başarıyla Gerçekleşti.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            giderListele();
            temizle();
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {

        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dRow = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dRow != null)
            {
                Txtid.Text = dRow[0].ToString();
                CmbAy.Text = dRow[1].ToString();
                CmbYil.Text = dRow[2].ToString();
                TxtElektrik.Text = dRow[3].ToString();
                TxtSu.Text = dRow[4].ToString();
                TxtDogalgaz.Text = dRow[5].ToString();
                TxtInternet.Text = dRow[6].ToString();
                TxtMaaslar.Text = dRow[7].ToString();
                TxtEkstra.Text = dRow[8].ToString();
                RchNotlar.Text = dRow[9].ToString();
            }

        }

        private void BtnSil_Click(object sender, EventArgs e)
        {


            DialogResult soru;
            soru = MessageBox.Show(Txtid.Text + " ID'sine Sahip Veriyi Silmek İstediğinize Emin Misiniz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (soru == DialogResult.Yes)
            {
                SqlCommand komutSilme = new SqlCommand("DELETE FROM TBL_GIDERLER WHERE ID=@P1", bgl.baglanti());
                komutSilme.Parameters.AddWithValue("@P1", Txtid.Text);
                komutSilme.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show(Txtid.Text + " ID'sine Sahip Veri Başarıyla Silindi.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                giderListele();
            }

            else
            {
                giderListele();
            }
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komutEkle = new SqlCommand("UPDATE TBL_GIDERLER SET AY=@P1, YIL=@P2, ELEKTRIK=@P3, SU=@P4, DOGALGAZ=@P5, INTERNET=@P6, MAASLAR=@P7, EKSTRA=@P8, NOTLAR=@P9 WHERE ID=@PID", bgl.baglanti());

            komutEkle.Parameters.AddWithValue("@PID", Txtid.Text);
            komutEkle.Parameters.AddWithValue("@P1", CmbAy.Text);
            komutEkle.Parameters.AddWithValue("@P2", CmbYil.Text);
            komutEkle.Parameters.AddWithValue("@P3", decimal.Parse(TxtElektrik.Text));
            komutEkle.Parameters.AddWithValue("@P4", decimal.Parse(TxtSu.Text));
            komutEkle.Parameters.AddWithValue("@P5", decimal.Parse(TxtDogalgaz.Text));
            komutEkle.Parameters.AddWithValue("@P6", decimal.Parse(TxtInternet.Text));
            komutEkle.Parameters.AddWithValue("@P7", decimal.Parse(TxtMaaslar.Text));
            komutEkle.Parameters.AddWithValue("@P8", decimal.Parse(TxtEkstra.Text));
            komutEkle.Parameters.AddWithValue("@P9", RchNotlar.Text);

            komutEkle.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Gider Güncelleme İşlemi Başarıyla Gerçekleşti.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            giderListele();
            temizle();
        }
    }
}
