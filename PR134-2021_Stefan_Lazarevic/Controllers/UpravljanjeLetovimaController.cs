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
    public class UpravljanjeLetovimaController : ApiController
    {
        public IHttpActionResult Get()
        {
            Aviokompanije aviokompanije = (Aviokompanije)HttpContext.Current.Application["Aviokompanije"];
            List<Let> ret = new List<Let>();

            foreach (var a in aviokompanije.ListaAviokompanija)
            {
                foreach (var l in a.Letovi)
                {
                    ret.Add(l);
                }
            }

            return Ok(ret);
        }

        [Route("api/upravljanjeLetovima/{korisnickoIme}")]
        public IHttpActionResult Post(Let let, string korisnickoIme)
        {
            Aviokompanije aviokompanije = (Aviokompanije)HttpContext.Current.Application["Aviokompanije"];

            if (let != null)
            {
                let.Id = let.GenerisiIdLeta();
                let.Status = StatusLetaEnum.Aktivan;
                let.BrojZauzetihMjesta = 0;
                let.Obrisan = false;
                foreach (var a in aviokompanije.ListaAviokompanija)
                {
                    if (let.IdAviokompanije == a.Id)
                    {
                        let.Aviokompanija = a.Naziv;
                        a.Letovi.Add(let);
                    }
                }
                aviokompanije.AzurirajAviokompanije();
                return Ok();
            }

            return BadRequest();
        }

        public IHttpActionResult Delete(int id)
        {
            Korisnici korisnici = (Korisnici)HttpContext.Current.Application["Korisnici"];
            Aviokompanije aviokompanije = (Aviokompanije)HttpContext.Current.Application["Aviokompanije"];

            bool provjera = true;

            foreach (var a in aviokompanije.ListaAviokompanija)
            {
                foreach (var l in a.Letovi)
                {
                    if (l.Id == id)
                    {
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
                                l.Obrisan = true;
                            }
                        }
                        aviokompanije.AzurirajAviokompanije();

                        return Ok();
                    }
                }
            }

            return BadRequest();
        }
    }
}
