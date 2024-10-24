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
    public class IzmijeniAviokompanijuController : ApiController
    {
        public IHttpActionResult Get(int id)
        {
            Aviokompanije aviokompanije = (Aviokompanije)HttpContext.Current.Application["Aviokompanije"];

            foreach (var a in aviokompanije.ListaAviokompanija)
            {
                if (a.Id == id)
                    return Ok(a);
            }

            return BadRequest();
        }

        public IHttpActionResult Post(Aviokompanija aviokompanija)
        {
            Aviokompanije aviokompanije = (Aviokompanije)HttpContext.Current.Application["Aviokompanije"];
            if (aviokompanija != null)
            {
                foreach (var a in aviokompanije.ListaAviokompanija)
                {
                    if (a.Id == aviokompanija.Id)
                    {
                        a.Naziv = aviokompanija.Naziv;
                        a.Adresa = aviokompanija.Adresa;
                        a.Kontakt = aviokompanija.Kontakt;
                        foreach (var l in a.Letovi)
                        {
                            if (a.Id == l.IdAviokompanije)
                            {
                                l.Aviokompanija = aviokompanija.Naziv;
                            }
                        }

                        foreach (var r in a.ListaRecenzija)
                        {
                            if (a.Id == r.IdAviokompanije)
                            {
                                r.Aviokompanija= aviokompanija.Naziv;
                            }
                        }

                        aviokompanije.AzurirajAviokompanije();
                    }
                }

                return Ok();
            }

            return BadRequest();
        }
    }
}
