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
    public class MojeRezervacijeController : ApiController
    {
        public IHttpActionResult Get(string korisnickoIme)
        {
            Korisnici korisnici = (Korisnici)HttpContext.Current.Application["Korisnici"];
            List<Rezervacija> ret = new List<Rezervacija>();
            foreach (var k in korisnici.ListaKorisnika)
            {
                if (k.KorisnickoIme == korisnickoIme)
                {
                    foreach (var r in k.Rezervacije)
                    {
                        ret.Add(r);
                    }
                    return Ok(ret);
                }
            }
            return BadRequest();
        }
    }
}
