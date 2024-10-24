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
    public class LetoviController : ApiController
    {
        public IHttpActionResult Get()
        {
            Aviokompanije aviokompanije = (Aviokompanije)HttpContext.Current.Application["Aviokompanije"];
            List<Let> ret = new List<Let>();

            DateTime sada = DateTime.Now;

            foreach (var a in aviokompanije.ListaAviokompanija)
            {
                foreach (var b in a.Letovi)
                {
                    if (b.DatumDolaska <= sada)
                    {
                        b.Status = StatusLetaEnum.Zavrsen;
                    }

                    if (b.Status.ToString() == "Aktivan" && a.Obrisana == false && b.Obrisan == false)
                    {
                        ret.Add(b);
                    }
                }
            }
            aviokompanije.AzurirajAviokompanije();

            return Ok(ret);
        }
    }
}
