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
    public class IzmjeniLetController : ApiController
    {
        public IHttpActionResult Get(int id)
        {
            Aviokompanije aviokompanije = (Aviokompanije)HttpContext.Current.Application["Aviokompanije"];

            foreach (var a in aviokompanije.ListaAviokompanija)
            {
                foreach (var l in a.Letovi)
                {
                    if (l.Id == id)
                    {
                        return Ok(l);
                    }
                }
            }

            return BadRequest();
        }

        public IHttpActionResult Post(Let let)
        {
            Korisnici korisnici = (Korisnici)HttpContext.Current.Application["Korisnici"];
            Aviokompanije aviokompanije = (Aviokompanije)HttpContext.Current.Application["Aviokompanije"];

            DateTime sada = DateTime.Now;
            bool provjera = true;

            if (let != null)
            {
                foreach (var a in aviokompanije.ListaAviokompanija)
                {
                    foreach (var l in a.Letovi)
                    {
                        if (l.Id == let.Id)
                        {
                            l.DatumPolaska = let.DatumPolaska;
                            l.DatumDolaska = let.DatumDolaska;
                            l.BrojSlobodnihMjesta = let.BrojSlobodnihMjesta;
                            l.BrojZauzetihMjesta = let.BrojZauzetihMjesta;

                            if (let.DatumDolaska <= sada && l.Status != StatusLetaEnum.Otkazan)
                            {
                                l.Status = StatusLetaEnum.Zavrsen;
                            }
                            else if (l.Status != StatusLetaEnum.Otkazan)
                            {
                                l.Status = StatusLetaEnum.Aktivan;
                            }

                            foreach (var k in korisnici.ListaKorisnika)
                            {
                                foreach (var r in k.Rezervacije)
                                {
                                    if (r.Let == l.Id && (r.Status == StatusEnum.Kreirana || r.Status == StatusEnum.Odobrena))
                                    {
                                        provjera = false;
                                    }
                                }

                                if (provjera == true)
                                {
                                    l.Cijena = let.Cijena;
                                }
                            }
                            aviokompanije.AzurirajAviokompanije();
                        }
                    }
                }

                return Ok();
            }

            return BadRequest();
        }
    }
}
