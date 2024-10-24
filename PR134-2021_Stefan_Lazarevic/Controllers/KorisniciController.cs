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
    public class KorisniciController : ApiController
    {
        public IHttpActionResult Get()
        {
            Korisnici korisnici = (Korisnici)HttpContext.Current.Application["Korisnici"];
            List<Korisnik> ret = new List<Korisnik>();
            foreach (var korisnik in korisnici.ListaKorisnika)
            {
                ret.Add(korisnik);
            }
            return Ok(ret);
        }

        [Route("api/korisnici/{korisnik}")]
        public IHttpActionResult Post(Korisnik korisnik)
        {
            Korisnici korisnici = (Korisnici)HttpContext.Current.Application["Korisnici"];

            bool postoji = false;

            foreach (var k in korisnici.ListaKorisnika)
            {
                if (k.KorisnickoIme.Equals(korisnik.KorisnickoIme) || k.Email.Equals(korisnik.Email))
                {
                    postoji = true;
                }
            }

            if (postoji == false)
            {
                korisnik.Rezervacije = new List<Rezervacija>();
                korisnik.TipKorisnika = TipKorisnikaEnum.Putnik;
                korisnici.ListaKorisnika.Add(korisnik);
                korisnici.AzurirajKorisnike();

                return Ok(korisnik);
            }

            return BadRequest();
        }
    }
}
