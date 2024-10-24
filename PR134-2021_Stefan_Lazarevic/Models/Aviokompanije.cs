using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace PR134_2021_Stefan_Lazarevic.Models
{
    public class Aviokompanije
    {
        public List<Aviokompanija> ListaAviokompanija { get; set; }

        public Aviokompanije(string putanja)
        {
            using (StreamReader reader = new StreamReader(putanja))
            {
                string json = reader.ReadToEnd();

                ListaAviokompanija = JsonConvert.DeserializeObject<List<Aviokompanija>>(json);
            }
        }

        public void AzurirajAviokompanije()
        {
                string putanja = HostingEnvironment.MapPath("~/App_Data/aviokompanije.json");
                string json = JsonConvert.SerializeObject(ListaAviokompanija);

                if (File.Exists(putanja))
                {
                    using (FileStream fs = new FileStream(putanja, FileMode.Open, FileAccess.Read, FileShare.None))
                    {
                        fs.Close();
                    }
                }

                File.WriteAllText(putanja, json);
        }
    }
}