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
    public class RezervacijaController : ApiController
    {
        public IHttpActionResult Get(string korisnickoIme)
        {
            Korisnici korisnici = (Korisnici)HttpContext.Current.Application["Korisnici"];
            Aviokompanije aviokompanije = (Aviokompanije)HttpContext.Current.Application["Aviokompanije"];

            foreach (var a in aviokompanije.ListaAviokompanija)
            {
                foreach (var k in korisnici.ListaKorisnika)
                {
                    foreach (var r in k.Rezervacije)
                    {
                        foreach (var l in a.Letovi)
                        {
                            if (l.Id == r.Let)
                            {
                                if (l.Status == StatusLetaEnum.Zavrsen)
                                {
                                    r.Status = StatusEnum.Zavrsena;
                                    aviokompanije.AzurirajAviokompanije();
                                    korisnici.AzurirajKorisnike();
                                }
                            }
                        }
                    }
                }
            }

            foreach (var k in korisnici.ListaKorisnika)
            {
                if (k.KorisnickoIme.Equals(korisnickoIme))
                {
                    return Ok(k.Rezervacije);
                }
            }
            return BadRequest();
        }

        public IHttpActionResult Post(Rezervacija rezervacija)
        {
            Korisnici korisnici = (Korisnici)HttpContext.Current.Application["Korisnici"];
            Aviokompanije aviokompanije = (Aviokompanije)HttpContext.Current.Application["Aviokompanije"];

            foreach (var a in aviokompanije.ListaAviokompanija)
            {
                foreach (var l in a.Letovi)
                {
                    if (l.Id == rezervacija.Let)
                    {
                        if (l.BrojSlobodnihMjesta < rezervacija.BrojPutnika)
                        {
                            return BadRequest("Trenutno nema toliko dostupnih karata!");
                        }
                        l.BrojSlobodnihMjesta -= rezervacija.BrojPutnika;
                        l.BrojZauzetihMjesta += rezervacija.BrojPutnika;
                        rezervacija.Cijena = l.Cijena * rezervacija.BrojPutnika;
                        if (l.Status == StatusLetaEnum.Zavrsen)
                        {
                            rezervacija.Status = StatusEnum.Zavrsena;
                        }
                    }
                }
            }


            foreach (var k in korisnici.ListaKorisnika)
            {
                if (k.KorisnickoIme == rezervacija.Korisnik)
                {
                    rezervacija.Id = rezervacija.GenerisiIdRezervacije();

                    k.Rezervacije.Add(rezervacija);

                    aviokompanije.AzurirajAviokompanije();
                    korisnici.AzurirajKorisnike();

                    return Ok(k);
                }
            }

            return BadRequest("Neuspijesno kreiranje rezervacije");
        }
    }
}
