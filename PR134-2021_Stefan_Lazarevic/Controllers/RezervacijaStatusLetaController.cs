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
    public class RezervacijaStatusLetaController : ApiController
    {
        public IHttpActionResult Get(string korisnickoIme)
        {
            Korisnici korisnici = (Korisnici)HttpContext.Current.Application["Korisnici"];
            Aviokompanije aviokompanije = (Aviokompanije)HttpContext.Current.Application["Aviokompanije"];

            List<Let> ret = new List<Let>();

            foreach (var k in korisnici.ListaKorisnika)
            {
                if (k.KorisnickoIme == korisnickoIme)
                {
                    foreach (var a in aviokompanije.ListaAviokompanija)
                    {
                        foreach (var l in a.Letovi)
                        {
                            foreach (var r in k.Rezervacije)
                            {
                                if (r.Let == l.Id)
                                {
                                    ret.Add(l);
                                }
                            }
                        }
                    }
                }
            }

            return Ok(ret);
        }
    }
}
