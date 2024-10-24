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
    public class RezervacijaUpravljanjeController : ApiController
    {
        public IHttpActionResult Get()
        {
            Korisnici korisnici = (Korisnici)HttpContext.Current.Application["Korisnici"];
            List<Rezervacija> ret = new List<Rezervacija>();
            foreach (var k in korisnici.ListaKorisnika)
            {
                foreach (var r in k.Rezervacije)
                {
                    ret.Add(r);
                }
            }
            return Ok(ret);
        }

        public IHttpActionResult Post(Rezervacija rezervacija)
        {
            Korisnici korisnici = (Korisnici)HttpContext.Current.Application["Korisnici"];
            Aviokompanije aviokompanije = (Aviokompanije)HttpContext.Current.Application["Aviokompanije"];
            Korisnik ret = null;
            foreach (var k in korisnici.ListaKorisnika)
            {
                foreach (var r in k.Rezervacije)
                {
                    if (r.Id == rezervacija.Id)
                    {
                        r.Status = rezervacija.Status;

                        if (rezervacija.Status == StatusEnum.Otkazana)
                        {
                            foreach (var a in aviokompanije.ListaAviokompanija)
                            {
                                foreach (var l in a.Letovi)
                                {
                                    if (l.Id == rezervacija.Let)
                                    {
                                        l.BrojSlobodnihMjesta += rezervacija.BrojPutnika;
                                        l.BrojZauzetihMjesta -= rezervacija.BrojPutnika;
                                    }
                                }
                            }
                        }
                        ret = k;
                    }
                }
            }

            korisnici.AzurirajKorisnike();
            aviokompanije.AzurirajAviokompanije();
            return Ok(ret);
        }
    }
}
