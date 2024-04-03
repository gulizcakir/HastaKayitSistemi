using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaKayitSistemi
{
    public class Randevu
    {
        public string Hastahane { get; set; }
        public string Bolum { get; set; }
        public string Doktor { get; set; }
        public string Teshis { get; set; }
        public DateTime Tarih { get; set; }
        public string HastaTc { get; set; }

        public override string ToString()
        {
            return $"{Hastahane} - {Bolum} - {Doktor} - {Tarih.ToString("dd/MM/yyyy HH:mm")}- {HastaTc}";
        }
    }
}
