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
    public class AviokompanijaUpravljanjeController : ApiController
    {
        public IHttpActionResult Get()
        {
            Aviokompanije aviokompanije = (Aviokompanije)HttpContext.Current.Application["Aviokompanije"];
            List<Aviokompanija> ret = new List<Aviokompanija>();

            foreach (var a in aviokompanije.ListaAviokompanija)
            {
                ret.Add(a);
            }

            return Ok(ret);
        }

        [Route("api/aviokompanijaUpravljanje/{korisnickoIme}")]
        public IHttpActionResult Post(Aviokompanija aviokompanija, string korisnickoIme)
        {
            Korisnici korisnici = (Korisnici)HttpContext.Current.Application["Korisnici"];
            Aviokompanije aviokompanije = (Aviokompanije)HttpContext.Current.Application["Aviokompanije"];
            if (aviokompanija != null)
            {
                aviokompanija.Id = aviokompanija.GenerisiId();
                aviokompanija.Letovi = new List<Let>();
                aviokompanija.ListaRecenzija = new List<Recenzija>();
                aviokompanija.Obrisana = false;
                aviokompanije.ListaAviokompanija.Add(aviokompanija);
                aviokompanije.AzurirajAviokompanije();

                return Ok();
            }

            return BadRequest();
        }

        public IHttpActionResult Delete(int id)
        {
            Aviokompanije aviokompanije = (Aviokompanije)HttpContext.Current.Application["Aviokompanije"];

            bool brisanje = true;

            foreach (var a in aviokompanije.ListaAviokompanija)
            {
                if (a.Id == id)
                {
                    foreach (var l in a.Letovi)
                    {
                        if (l.Status == StatusLetaEnum.Aktivan && a.Id == l.IdAviokompanije)
                        {
                            brisanje = false;
                        }
                    }

                    if (brisanje == true)
                    {
                        a.Obrisana = true;
                        aviokompanije.AzurirajAviokompanije();
                        return Ok();
                    }
                }
            }

            return BadRequest();
        }
    }
}
