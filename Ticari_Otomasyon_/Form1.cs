using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ticari_Otomasyon_
{
    public partial class FrmAnaModul : Form
    {
        public string Form1_FrmKasa_aktifKullanici;
        public FrmAnaModul()
        {
            InitializeComponent();
        }

        FrmAnasayfa frAnasayfa;
        private void FrmAnaModul_Load(object sender, EventArgs e)
        {
            if (frAnasayfa == null)
            {
                frAnasayfa = new FrmAnasayfa();
                frAnasayfa.MdiParent = this;
                frAnasayfa.Show();
            }
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frAnasayfa == null)
            {
                frAnasayfa = new FrmAnasayfa();
                frAnasayfa.MdiParent = this;
                frAnasayfa.Show();
            }
        }

        FrmUrunler frUrunler;
        private void BtnUrunler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frUrunler == null || frUrunler.IsDisposed)
            {
                frUrunler = new FrmUrunler();
                frUrunler.MdiParent = this;
                frUrunler.Show();
            }
        }

        FrmMusteriler frMusteriler;
        private void BtnMusteriler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frMusteriler == null || frMusteriler.IsDisposed)
            {
                frMusteriler = new FrmMusteriler();
                frMusteriler.MdiParent = this;
                frMusteriler.Show();
            }
        }


        FrmFirmalar frFirmalar;

        private void BtnFirmalar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frFirmalar == null || frFirmalar.IsDisposed)
            {
                frFirmalar = new FrmFirmalar();
                frFirmalar.MdiParent = this;
                frFirmalar.Show();
            }
        }

        FrmPersonel frPersonel;
        private void BtnPersoneller_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frPersonel == null || frPersonel.IsDisposed)
            {
                frPersonel = new FrmPersonel();
                frPersonel.MdiParent = this;
                frPersonel.Show();
            }
        }

        FrmRehber frRehber;
        private void BtnRehber_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frRehber == null || frRehber.IsDisposed)
            {
                frRehber = new FrmRehber();
                frRehber.MdiParent = this;
                frRehber.Show();
            }
        }

        FrmGiderler frGiderler;
        private void BtnGiderler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frGiderler == null || frGiderler.IsDisposed)
            {
                frGiderler = new FrmGiderler();
                frGiderler.MdiParent = this;
                frGiderler.Show();
            }

        }

        FrmBankalar frBankalar;
        private void BtnBankalar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frBankalar == null || frBankalar.IsDisposed)
            {
                frBankalar = new FrmBankalar();
                frBankalar.MdiParent = this;
                frBankalar.Show();
            }
        }

        FrmFaturalar frFaturalar;
        private void BtnFaturalar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frFaturalar == null || frFaturalar.IsDisposed)
            {
                frFaturalar = new FrmFaturalar();
                frFaturalar.MdiParent = this;
                frFaturalar.Show();
            }
        }

        FrmNotlar frNotlar;
        private void BtnNotlar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frNotlar == null || frNotlar.IsDisposed)
            {
                frNotlar = new FrmNotlar();
                frNotlar.MdiParent = this;
                frNotlar.Show();
            }

        }

        FrmHareketler frHareketler;
        private void BtnHareketler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frHareketler == null || frHareketler.IsDisposed)
            {
                frHareketler = new FrmHareketler();
                frHareketler.MdiParent = this;
                frHareketler.Show();
            }
        }

        FrmRaporlar frRaporlar;
        private void BtnRaporlar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frRaporlar == null || frRaporlar.IsDisposed)
            {
                frRaporlar = new FrmRaporlar();
                frRaporlar.MdiParent = this;
                frRaporlar.Show();
            }

        }

        FrmStoklar frStoklar;
        private void BtnStoklar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frStoklar == null || frStoklar.IsDisposed)
            {
                frStoklar = new FrmStoklar();
                frStoklar.MdiParent = this;
                frStoklar.Show();
            }
        }

        FrmAyarlar frAyarlar;
        private void BtnAyarlar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frAyarlar == null || frAyarlar.IsDisposed)
            {
                frAyarlar = new FrmAyarlar();
                frAyarlar.Show();
            }
        }

        FrmKasa frKasa;

        private void BtnKasa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frKasa == null || frKasa.IsDisposed)
            {
                frKasa = new FrmKasa();
                frKasa.MdiParent = this;
                frKasa.FrmKasa_aktifKullanici = Form1_FrmKasa_aktifKullanici;
                frKasa.Show();
            }
        }
    }
}
