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
    public partial class FrmAdmin : Form
    {
        public FrmAdmin()
        {
            InitializeComponent();
        }

        Sqlbaglantisi bgl = new Sqlbaglantisi();

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (TxtParola.UseSystemPasswordChar == true)
            {
                TxtParola.UseSystemPasswordChar = false;
            }

            else if (TxtParola.UseSystemPasswordChar == false)
            {
                TxtParola.UseSystemPasswordChar = true;
                //simpleButton2.ImageOptions.Image.Equals();
            }
        }

        int girisSayisi = 3;

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            SqlCommand komut3 = new SqlCommand("SELECT HesapDurumu FROM TBL_ADMIN WHERE (KullaniciAdi=@P4)", bgl.baglanti());
            komut3.Parameters.AddWithValue("@P4", TxtKullaniciAdi.Text.ToString());
            SqlDataReader dr1 = komut3.ExecuteReader();
            while (dr1.Read())
            {
                if (bool.Parse(dr1[0].ToString()) == false)
                {
                    MessageBox.Show("Hesabınız Kilitlenmiştir. Açmak İçin Yöneticinize Başvurunuz.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                else
                {

                    SqlCommand komut = new SqlCommand("SELECT * FROM TBL_ADMIN WHERE (KullaniciAdi=@P1 and Parola=@P2 and HesapDurumu=1)", bgl.baglanti());
                    komut.Parameters.AddWithValue("@P1", TxtKullaniciAdi.Text.ToString());
                    komut.Parameters.AddWithValue("@P2", TxtParola.Text.ToString());
                    SqlDataReader dr = komut.ExecuteReader();

                    if (dr.Read())
                    {
                        FrmAnaModul frAnaModul = new FrmAnaModul();
                        frAnaModul.Show();
                        this.Hide();
                        frAnaModul.Form1_FrmKasa_aktifKullanici= dr["KullaniciAdi"].ToString();
                    }

                    else
                    {
                        girisSayisi--;
                        MessageBox.Show("Kullanıcı Adı veya Parola Hatalı. Kalan Hakkınız: " + girisSayisi.ToString(), "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        if (girisSayisi == 0)
                        {
                            MessageBox.Show("Kullanıcı Adı veya Parola Hatalı. Kalan Hakkınız: " + girisSayisi.ToString() + ". Hesabınız Kilitlenmiştir. Açmak İçin Yöneticinize Başvurunuz.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            SqlCommand komut2 = new SqlCommand("UPDATE TBL_ADMIN  set HesapDurumu=0 where KullaniciAdi=@P3", bgl.baglanti());
                            komut2.Parameters.AddWithValue("@P3", TxtKullaniciAdi.Text.ToString());
                            komut2.ExecuteNonQuery();
                            this.Close();
                        }
                    }
                }
            }

            bgl.baglanti().Close();

        }
    }
}
