using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace PR134_2021_Stefan_Lazarevic.Models
{
    public class Letovi
    {
        public List<Let> ListaLetova { get; set; }

        public Letovi(string putanja)
        {
            using (StreamReader reader = new StreamReader(putanja))
            {
                string json = reader.ReadToEnd();

                ListaLetova = JsonConvert.DeserializeObject<List<Let>>(json);
            }
        }

        public void AzurirajLetove()
        {
            string putanja = HostingEnvironment.MapPath("~/App_Data/letovi.json");
            string json = JsonConvert.SerializeObject(ListaLetova);

            File.WriteAllText(putanja, json);
        }
    }
}