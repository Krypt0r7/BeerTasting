using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerSession.ViewModels
{
    public class Systemet
    {
        public int ID { get; set; }
        public int Nr { get; set; }
        public int ArtikelId { get; set; }
        public int Varunummer { get; set; }
        public string Namn { get; set; }
        public string Namn2 { get; set; }
        public decimal PrisInkMoms { get; set; }
        public int Pant { get; set; }
        public decimal VolymIml { get; set; }
        public decimal PrisPerLiter { get; set; }
        public DateTime? Saljstart { get; set; }
        public string Varugrupp { get; set; }
        public string Typ { get; set; }
        public string Stil { get; set; }
        public string Forpackning { get; set; }
        public string Ursprung { get; set; }
        public string Land { get; set; }
        public string Producent { get; set; }
        public string Leverantör { get; set; }
        public decimal Alkoholhalt { get; set; }
        public string RavarorDesc { get; set; }
    }
}
