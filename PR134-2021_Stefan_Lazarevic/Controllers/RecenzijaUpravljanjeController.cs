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
    public class RecenzijaUpravljanjeController : ApiController
    {
        [Route("api/RecenzijaUpravljanje/{idAviokompanije}/{idRecenzije}/{odobrena}")]
        public IHttpActionResult Post(int idAviokompanije, int idRecenzije, string odobrena)
        {
            Aviokompanije aviokompanije = (Aviokompanije)HttpContext.Current.Application["Aviokompanije"];
            Korisnici korisnici = (Korisnici)HttpContext.Current.Application["Korisnici"];

            foreach (var a in aviokompanije.ListaAviokompanija)
            {
                if (a.Id == idAviokompanije)
                {
                    foreach (var r in a.ListaRecenzija)
                    {
                        if (r.Id == idRecenzije)
                        {
                            if (odobrena == "Odobrena")
                            {
                                r.StatusRecenzije = StatusRecenzijeEnum.Odobrena;
                            }
                            else if (odobrena == "Odbijena")
                            {
                                r.StatusRecenzije = StatusRecenzijeEnum.Odbijena;
                            }
                        }
                    }
                }
            }

            aviokompanije.AzurirajAviokompanije();
            korisnici.AzurirajKorisnike();
            return Ok();
        }
    }
}
