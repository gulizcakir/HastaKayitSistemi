using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaKayitSistemi
{
    public class DoktorListesi
    {
        public List<Doktor> Doktorlar { get; set; }

        public DoktorListesi()
        {
            Doktorlar = new List<Doktor>();
        }

        public void DoktorEkle(Doktor yeniDoktor)
        {
            Doktorlar.Add(yeniDoktor);
        }
    }
}
