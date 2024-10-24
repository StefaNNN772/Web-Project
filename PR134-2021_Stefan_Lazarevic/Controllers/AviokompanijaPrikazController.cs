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
    public class AviokompanijaPrikazController : ApiController
    {
        public IHttpActionResult Get()
        {
            Aviokompanije aviokompanije = (Aviokompanije)HttpContext.Current.Application["Aviokompanije"];
            List<Aviokompanija> ret = new List<Aviokompanija>();

            foreach (var a in aviokompanije.ListaAviokompanija)
            {
                if (a.Obrisana == false)
                {
                    ret.Add(a);
                }
            }

            return Ok(ret);
        }
    }
}
