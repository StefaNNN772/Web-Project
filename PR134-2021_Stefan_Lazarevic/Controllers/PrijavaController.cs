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
    public class PrijavaController : ApiController
    {
        public IHttpActionResult Post(Korisnik korisnik)
        {
            Korisnici korisnici = (Korisnici)HttpContext.Current.Application["Korisnici"];
            Administratori administratori = (Administratori)HttpContext.Current.Application["Administratori"];
            Korisnik trenutni = korisnici.PronadjiPrijava(korisnik);
            Korisnik trenutniAdministrator = administratori.PronadjiPrijava(korisnik);
            if (trenutni != null)
            {
                return Ok(trenutni);
            }

            if (trenutniAdministrator != null)
            {
                return Ok(trenutniAdministrator);
            }

            return BadRequest("Pogrešna kombinacija imena i lozinke");
        }
    }
}
