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
    public partial class FrmFaturaUrunler : Form
    {
        Sqlbaglantisi bgl = new Sqlbaglantisi();
        public FrmFaturaUrunler()
        {
            InitializeComponent();
        }

        public void listeleFaturaUrunler()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM TBL_FATURADETAY WHERE FATURAID=" + id.ToString() + "", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }


        public string id;
        private void FrmFaturaUrunler_Load(object sender, EventArgs e)
        {
            listeleFaturaUrunler();
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            DataRow dRow = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            FrmFaturaUrunDuzenleme frFaturaUrunDuzenleme = new FrmFaturaUrunDuzenleme();
            if (frFaturaUrunDuzenleme != null)
            {
                frFaturaUrunDuzenleme.id = dRow["FATURAURUNID"].ToString();
                frFaturaUrunDuzenleme.ad = dRow["URUNAD"].ToString();
                frFaturaUrunDuzenleme.miktar = dRow["MIKTAR"].ToString();
                frFaturaUrunDuzenleme.fiyat = dRow["FIYAT"].ToString();
                frFaturaUrunDuzenleme.tutar = dRow["TUTAR"].ToString();
                frFaturaUrunDuzenleme.faturaid = dRow["FATURAID"].ToString();

                frFaturaUrunDuzenleme.Show();
                this.Close();
            }
        }
    }
}
