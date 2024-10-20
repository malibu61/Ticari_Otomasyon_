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
    public partial class FrmBankalar : Form
    {
        Sqlbaglantisi bgl = new Sqlbaglantisi();

        public FrmBankalar()
        {
            InitializeComponent();
        }

        void bankaListele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("EXEC BANKACAGIR", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }


        void firmaListele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter komut = new SqlDataAdapter("SELECT ID,AD FROM TBL_FIRMALAR", bgl.baglanti());
            komut.Fill(dt);

            LookUEFirma.Properties.NullText = "Bir Firma Seçiniz";
            LookUEFirma.Properties.ValueMember = "ID";
            LookUEFirma.Properties.DisplayMember = "AD";

            LookUEFirma.Properties.DataSource = dt;
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
            Cmbilce.Text = string.Empty;
            SqlCommand komut = new SqlCommand("SELECT ILCE FROM TBL_ILCELER WHERE SEHIR=@P1", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", (Cmbil.SelectedIndex + 1).ToString());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                Cmbilce.Properties.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();
            labelControl12.Text = (Cmbil.SelectedIndex + 1).ToString();
        }


        void temizle()
        {
            Txtid.Text = "";
            TxtBankaAdi.Text = "";
            Cmbil.Text = string.Empty;
            Cmbilce.Text = string.Empty;
            TxtSube.Text = "";
            Txtiban.Text = "";
            TxtHesapNo.Text = "";
            TxtYetkili.Text = "";
            MskTelefon.Text = "";
            MskTarih.Text = "";
            TxtHesapTuru.Text = "";
            LookUEFirma.Text = "";
        }

        private void FrmBankalar_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the '___203_Udemy_DboTicariOtomasyonDataSet.TBL_FIRMALAR' table. You can move, or remove it, as needed.
            this.tBL_FIRMALARTableAdapter.Fill(this.___203_Udemy_DboTicariOtomasyonDataSet.TBL_FIRMALAR);
            ilListele();
            bankaListele();
            firmaListele();
        }

        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            e.Appearance.BackColor = Color.White;

            e.Appearance.BackColor2 = Color.DarkCyan;
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komutEkle = new SqlCommand("INSERT INTO TBL_BANKALAR (BANKAADI, SUBE, IBAN, HESAPNO, YETKILI, TARIH, HESAPTURU, FIRMAID, IL, ILCE) VALUES (@P1, @P2, @P3, @P4, @P5, @P6, @P7, @P8, @P9, @P10)", bgl.baglanti());
            komutEkle.Parameters.AddWithValue("@P1", TxtBankaAdi.Text);
            komutEkle.Parameters.AddWithValue("@P2", TxtSube.Text);
            komutEkle.Parameters.AddWithValue("@P3", Txtiban.Text);
            komutEkle.Parameters.AddWithValue("@P4", TxtHesapNo.Text);
            komutEkle.Parameters.AddWithValue("@P5", TxtYetkili.Text);
            komutEkle.Parameters.AddWithValue("@P6", MskTarih.Text);
            komutEkle.Parameters.AddWithValue("@P7", TxtHesapTuru.Text);
            komutEkle.Parameters.AddWithValue("@P8", LookUEFirma.EditValue);
            komutEkle.Parameters.AddWithValue("@P9", Cmbil.Text);
            komutEkle.Parameters.AddWithValue("@P10", Cmbilce.Text);
            komutEkle.ExecuteNonQuery();
            MessageBox.Show("Banka Başarıyla Eklendi");
            bankaListele();
            temizle();
            firmaListele();
        }

        private void Cmbil_SelectedIndexChanged(object sender, EventArgs e)
        {
            ilceListele();
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
                TxtBankaAdi.Text = dRow["BANKAADI"].ToString();
                Cmbil.Text = dRow["IL"].ToString();
                Cmbilce.Text = dRow["ILCE"].ToString();
                TxtSube.Text = dRow["SUBE"].ToString();
                Txtiban.Text = dRow["IBAN"].ToString();
                TxtHesapNo.Text = dRow["HESAPNO"].ToString();
                TxtYetkili.Text = dRow["YETKILI"].ToString();
                MskTelefon.Text = dRow["TELEFON"].ToString();
                MskTarih.Text = dRow["TARIH"].ToString();
                TxtHesapTuru.Text = dRow["HESAPTURU"].ToString();
                LookUEFirma.Text = dRow["FIRMAAD"].ToString();
            }
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komutSilme = new SqlCommand("DELETE FROM TBL_BANKALAR WHERE ID=@P1", bgl.baglanti());
            komutSilme.Parameters.AddWithValue("@P1", Txtid.Text);
            komutSilme.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Banka Bilgisi Başarıyla Silindi", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            bankaListele();
            temizle();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komutGuncelle = new SqlCommand("UPDATE TBL_BANKALAR SET BANKAADI=@P1, SUBE=@P2, IBAN=@P3, HESAPNO=@P4, YETKILI=@P5, TARIH=@P6, HESAPTURU=@P7, FIRMAID=@P8, IL=@P9, ILCE=@P10 WHERE ID=@PID", bgl.baglanti());
            komutGuncelle.Parameters.AddWithValue("@PID", Txtid.Text);
            komutGuncelle.Parameters.AddWithValue("@P1", TxtBankaAdi.Text);
            komutGuncelle.Parameters.AddWithValue("@P2", TxtSube.Text);
            komutGuncelle.Parameters.AddWithValue("@P3", Txtiban.Text);
            komutGuncelle.Parameters.AddWithValue("@P4", TxtHesapNo.Text);
            komutGuncelle.Parameters.AddWithValue("@P5", TxtYetkili.Text);
            komutGuncelle.Parameters.AddWithValue("@P6", MskTarih.Text);
            komutGuncelle.Parameters.AddWithValue("@P7", TxtHesapTuru.Text);
            komutGuncelle.Parameters.AddWithValue("@P8", LookUEFirma.EditValue);
            komutGuncelle.Parameters.AddWithValue("@P9", Cmbil.Text);
            komutGuncelle.Parameters.AddWithValue("@P10", Cmbilce.Text);
            komutGuncelle.ExecuteNonQuery();
            MessageBox.Show(Txtid.Text + " ID'li Banka Başarıyla Güncellendi", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            bankaListele();
            temizle();
            firmaListele();
        }
    }
}
