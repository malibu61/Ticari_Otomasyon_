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
    public partial class FrmAyarlar : Form
    {
        Sqlbaglantisi bgl = new Sqlbaglantisi();
        public FrmAyarlar()
        {
            InitializeComponent();
        }

        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT* FROM TBL_ADMIN", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
            bgl.baglanti().Close();
        }

        private void FrmAyarlar_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komutEkle = new SqlCommand("INSERT INTO TBL_ADMIN (KullaniciAdi, Parola, HesapDurumu) values (@p1, @p2, @p3)", bgl.baglanti());
            komutEkle.Parameters.AddWithValue("@p1", txtKullaniciAdi.Text.ToString());
            komutEkle.Parameters.AddWithValue("@p2", txtParola.Text.ToString());
            komutEkle.Parameters.AddWithValue("@p3", true);
            komutEkle.ExecuteNonQuery();
            bgl.baglanti().Close();

            MessageBox.Show("Admin Sisteme Eklendi.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);

            listele();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dRow = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dRow != null)
            {
                txtKullaniciAdi.Text = dRow[0].ToString();
                txtParola.Text = dRow[1].ToString();
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komutGuncelle = new SqlCommand("UPDATE TBL_ADMIN SET Parola=@p2, HesapDurumu=@p3 where KullaniciAdi=@p1", bgl.baglanti());
            komutGuncelle.Parameters.AddWithValue("@p1", txtKullaniciAdi.Text);
            komutGuncelle.Parameters.AddWithValue("@p2", txtParola.Text);

            if (radioBtnPasif.Checked)
            {
                komutGuncelle.Parameters.AddWithValue("@p3", false);
            }
            else
            {
                komutGuncelle.Parameters.AddWithValue("@p3", true);
            }

            komutGuncelle.ExecuteNonQuery();
            bgl.baglanti().Close();

            MessageBox.Show("Admin Bilgisi Güncellendi.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Hand);

            listele();
        }
    }
}
