using System;
using System.Collections.Generic;
using System.Drawing;// Image tipini kullanabilmek için eklenmiş bir using direktifi
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HastaKayitSistemi
{
    public class Hasta
    {
        private string _hastaEmail;
        private string _hastaTelefon;
        public string Teshis { get; set; }
        public string TeshisDr { get; set; }
        public string hastaAdiSoyadi { get; set; }
        public string hastaCinsiyet { get; set; }
        public string hastaAdres { get; set; }
        public string hastaMeslek { get; set; }
        public int hastaYas { get; set; }
        public Image hastaFotograf { get; set; }

        public string HastaEmail
        {
            get { return _hastaEmail; }
            set
            {
                if (IsValidEmail(value))
                {
                    _hastaEmail = value;
                }
                else
                {
                    MessageBox.Show("Geçerli bir e-posta adresi giriniz.");
                }
            }
        }

        public string hastaTelefon
        {
            get { return _hastaTelefon; }
            set
            {
                // Maske karakterlerini temizle
                _hastaTelefon = value.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var mailAddress = new System.Net.Mail.MailAddress(email);
                return mailAddress.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
