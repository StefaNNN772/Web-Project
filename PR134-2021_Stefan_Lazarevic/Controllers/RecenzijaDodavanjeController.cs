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
    public class RecenzijaDodavanjeController : ApiController
    {
        //Dobijanje svih recenzija za aviokompanije kod interfejsa za dodavanje
        public IHttpActionResult Get(int idLeta, string korisnickoIme)
        {
            Aviokompanije aviokompanije = (Aviokompanije)HttpContext.Current.Application["Aviokompanije"];
            List<Recenzija> ret = new List<Recenzija>();

            foreach (var a in aviokompanije.ListaAviokompanija)
            {
                foreach (var n in a.Letovi)
                {
                    foreach (var k in a.ListaRecenzija)
                    {
                        if (n.Id == idLeta && k.Recezent == korisnickoIme && a.Obrisana == false)
                        {
                            ret.Add(k);
                        }
                    }
                }
                return Ok(ret);
            }

            return BadRequest();
        }

        //Postavljanje nove recenzije
        public IHttpActionResult Post(Recenzija recenzija)
        {
            Aviokompanije aviokompanije = (Aviokompanije)HttpContext.Current.Application["Aviokompanije"];

            recenzija.Id = Recenzija.GenerisiId();

            foreach (var l in aviokompanije.ListaAviokompanija)
            {
                foreach (var r in l.Letovi)
                {
                    if (r.Id == recenzija.IdLeta)
                    {
                        recenzija.Aviokompanija = l.Naziv;
                        recenzija.IdAviokompanije = l.Id;
                        l.ListaRecenzija.Add(recenzija);
                    }
                }
            }

            aviokompanije.AzurirajAviokompanije();
            return Ok();
        }
    }
}
