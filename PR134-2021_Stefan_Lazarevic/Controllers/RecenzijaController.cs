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
    public class RecenzijaController : ApiController
    {
        //Dobijanje svih recenzija za neku aviokompaniju
        public IHttpActionResult Get(int idAviokompanije)
        {
            Aviokompanije aviokompanije = (Aviokompanije)HttpContext.Current.Application["Aviokompanije"];
            List<Recenzija> ret = new List<Recenzija>();
            foreach (var l in aviokompanije.ListaAviokompanija)
            {
                if (l.Id == idAviokompanije)
                {
                    foreach (var r in l.ListaRecenzija)
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
