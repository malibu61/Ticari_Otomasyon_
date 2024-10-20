using DevExpress.XtraBars;
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
    public partial class FrmUrunler : Form
    {
        public FrmUrunler()
        {
            InitializeComponent();
        }

        Sqlbaglantisi bgl = new Sqlbaglantisi();
        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from tbl_urunler",
            bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;

        }
        private void FrmUrunler_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("INSERT INTO TBL_URUNLER (URUNAD,MARKA,MODEL,YIL,ADET,ALISFIYAT,SATISFIYAT,DETAY) VALUES (@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8)", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", TxtAd.Text);
            komut.Parameters.AddWithValue("@P2", TxtMarka.Text);
            komut.Parameters.AddWithValue("@P3", TxtModel.Text);
            komut.Parameters.AddWithValue("@P4", MskYil.Text);
            komut.Parameters.AddWithValue("@P5", int.Parse(NudAdet.Text.ToString()));
            komut.Parameters.AddWithValue("@P6", decimal.Parse(TxtAlis.Text));
            komut.Parameters.AddWithValue("@P7", decimal.Parse(TxtSatis.Text));
            komut.Parameters.AddWithValue("@P8", RchDetay.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Ekleme İşleminiz Başarıyla Gerçekleşti", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);

            listele();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komutSilme = new SqlCommand("DELETE FROM TBL_URUNLER WHERE ID=@P1", bgl.baglanti());
            komutSilme.Parameters.AddWithValue("@P1", int.Parse(Txtid.Text));
            komutSilme.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show(Txtid.Text + " Numaralı ID Veritabanından Silindi", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            listele();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            Txtid.Text = dr["ID"].ToString();
            TxtAd.Text = dr["URUNAD"].ToString();
            TxtMarka.Text = dr["MARKA"].ToString();
            TxtModel.Text = dr["MODEL"].ToString();
            MskYil.Text = dr["YIL"].ToString();
            NudAdet.Text = dr["ADET"].ToString();
            TxtAlis.Text = dr["ALISFIYAT"].ToString();
            TxtSatis.Text = dr["SATISFIYAT"].ToString();
            RchDetay.Text = dr["DETAY"].ToString();

        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            Txtid.Text = dr["ID"].ToString();
            TxtAd.Text = dr["URUNAD"].ToString();
            TxtMarka.Text = dr["MARKA"].ToString();
            TxtModel.Text = dr["MODEL"].ToString();
            MskYil.Text = dr["YIL"].ToString();
            NudAdet.Text = dr["ADET"].ToString();
            TxtAlis.Text = dr["ALISFIYAT"].ToString();
            TxtSatis.Text = dr["SATISFIYAT"].ToString();
            RchDetay.Text = dr["DETAY"].ToString();

        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komutGuncelle = new SqlCommand("UPDATE TBL_URUNLER SET URUNAD=@pUrunad,MARKA=@pMarka, MODEL=@pModel,YIL=@pYil,ADET=@pAdet," +
                "ALISFIYAT=@pAlisFiyat,SATISFIYAT=@pSatisFiyat,DETAY=@pDetay WHERE ID=@pID", bgl.baglanti());

            komutGuncelle.Parameters.AddWithValue("@pID", int.Parse(Txtid.Text));
            komutGuncelle.Parameters.AddWithValue("@pUrunad", TxtAd.Text);
            komutGuncelle.Parameters.AddWithValue("@pMarka", TxtMarka.Text);
            komutGuncelle.Parameters.AddWithValue("@pModel", TxtModel.Text);
            komutGuncelle.Parameters.AddWithValue("@pYil", (MskYil.Text.ToString()));
            komutGuncelle.Parameters.AddWithValue("@pAdet", int.Parse(NudAdet.Text).ToString());
            komutGuncelle.Parameters.AddWithValue("@pAlisFiyat", decimal.Parse((TxtAlis.Text).ToString()));
            komutGuncelle.Parameters.AddWithValue("@pSatisFiyat", decimal.Parse((TxtSatis.Text).ToString()));
            komutGuncelle.Parameters.AddWithValue("@pDetay", RchDetay.Text);

            komutGuncelle.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show(Txtid.Text + " ID'li Veri Güncellendi", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);

            listele();

        }

        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            e.Appearance.BackColor = Color.White;

            e.Appearance.BackColor2 = Color.DarkCyan;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Txtid.Text = string.Empty;
            TxtAd.Text = string.Empty;
            TxtMarka.Text = string.Empty;
            TxtModel.Text = string.Empty;
            MskYil.Text = string.Empty;
            NudAdet.Text = string.Empty;
            TxtAlis.Text = string.Empty;
            TxtSatis.Text = string.Empty;
            RchDetay.Text = string.Empty;
        }

    }
}
