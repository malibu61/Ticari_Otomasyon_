using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticari_Otomasyon_
{
    internal class Sqlbaglantisi
    {

        public SqlConnection baglanti()
        {
            SqlConnection baglan = new SqlConnection(@"Data Source=MUHAMMETALI45\SQLEXPRESS;Initial Catalog=(203_Udemy)DboTicariOtomasyon;Integrated Security=True");
            baglan.Open();
            return baglan;
        }



    }
}
