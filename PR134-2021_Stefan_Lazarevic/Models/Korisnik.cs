using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PR134_2021_Stefan_Lazarevic.Models
{
    public class Korisnik
    {
        public string KorisnickoIme { get; set; }
        public string Lozinka { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Email { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public string Pol { get; set; }
        public TipKorisnikaEnum TipKorisnika { get; set; }
        public List<Rezervacija> Rezervacije { get; set; }
    }
}