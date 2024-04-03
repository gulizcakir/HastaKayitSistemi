using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaKayitSistemi
{
    public class HastahaneListesi
    {
        public List<Hastahane> Hastahaneler { get; set; }

        public HastahaneListesi()
        {
            Hastahaneler = new List<Hastahane>();
        }

        public void HastahaneEkle(Hastahane yeniHastahane)
        {
            Hastahaneler.Add(yeniHastahane);
        }
    }
}
