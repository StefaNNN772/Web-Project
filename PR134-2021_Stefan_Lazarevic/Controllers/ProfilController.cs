using PR134_2021_Stefan_Lazarevic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace PR134_2021_Stefan_Lazarevic.Controllers
{
    public class ProfilController : ApiController
    {
        [Route("api/profil/{korisnickoIme}")]
        public IHttpActionResult Post(Korisnik korisnik, string korisnickoIme)
        {
            Korisnici korisnici = (Korisnici)HttpContext.Current.Application["Korisnici"];
            Administratori administratori = (Administratori)HttpContext.Current.Application["Administratori"];

            foreach (var a in administratori.ListaAdministratora)
            {
                if (a.KorisnickoIme.Equals(korisnickoIme))
                {
                    a.KorisnickoIme = korisnik.KorisnickoIme;
                    a.Ime = korisnik.Ime;
                    a.KorisnickoIme = korisnik.KorisnickoIme;
                    a.Lozinka = korisnik.Lozinka;
                    a.Prezime = korisnik.Prezime;
                    a.Pol = korisnik.Pol;
                    a.Email = korisnik.Email;
                    a.DatumRodjenja = korisnik.DatumRodjenja;

                    administratori.AzurirajAdministratore();

                    return Ok(a);
                }
            }

            foreach (var k in korisnici.ListaKorisnika)
            {
                if (k.KorisnickoIme == korisnickoIme)
                {
                    k.Ime = korisnik.Ime;
                    k.KorisnickoIme = korisnik.KorisnickoIme;
                    k.Lozinka = korisnik.Lozinka;
                    k.Prezime = korisnik.Prezime;
                    k.Pol = korisnik.Pol;
                    k.Email = korisnik.Email;
                    k.DatumRodjenja = korisnik.DatumRodjenja;

                    korisnici.AzurirajKorisnike();

                    return Ok(k);
                }
            }
            return BadRequest();
        }
    }
}
