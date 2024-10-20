using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;

namespace Ticari_Otomasyon_
{
    public partial class FrmMailGonderme : Form
    {
        public FrmMailGonderme()
        {
            InitializeComponent();
        }

        public string mail;

        private void FrmMailGonderme_Load(object sender, EventArgs e)
        {
            TxtMailAdres.Text = mail;
        }

        private void BtnGonder_Click(object sender, EventArgs e)
        {
            MailMessage mesajim = new MailMessage();
            SmtpClient istemci = new SmtpClient();
            istemci.Credentials = new System.Net.NetworkCredential("muhammedd45@icloud.com", "parola");
            istemci.Port = 587;
            istemci.Host = "smtp.icloud.com";
            istemci.EnableSsl = true;
            mesajim.To.Add(TxtMailMesaj.Text);
            mesajim.From = new MailAddress("muhammed45@icloud.com");
            mesajim.Subject = TxtMailKonu.Text;
            mesajim.Body = TxtMailMesaj.Text;
            istemci.Send(mesajim);
        }
    }
}
