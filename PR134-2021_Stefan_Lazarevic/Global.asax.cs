using PR134_2021_Stefan_Lazarevic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace PR134_2021_Stefan_Lazarevic
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Letovi letovi = new Letovi(HostingEnvironment.MapPath("~/App_Data/letovi.json"));
            HttpContext.Current.Application["Letovi"] = letovi;

            Korisnici korisnici = new Korisnici(HostingEnvironment.MapPath("~/App_Data/korisnici.json"));
            HttpContext.Current.Application["Korisnici"] = korisnici;

            Administratori administratori = new Administratori(HostingEnvironment.MapPath("~/App_Data/administratori.json"));
            HttpContext.Current.Application["Administratori"] = administratori;

            Aviokompanije aviokompanije = new Aviokompanije(HostingEnvironment.MapPath("~/App_Data/aviokompanije.json"));
            HttpContext.Current.Application["Aviokompanije"] = aviokompanije;
        }
    }
}
