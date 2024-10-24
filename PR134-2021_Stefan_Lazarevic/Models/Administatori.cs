using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace PR134_2021_Stefan_Lazarevic.Models
{
    public class Administatori
    {
        public List<Korisnik> listaAdministatora { get; set; }

        public Administatori(string putanja)
        {
            using (StreamReader reader = new StreamReader(putanja))
            {
                string json = reader.ReadToEnd();
                //deserialize koriscenjem paketa
                listaAdministatora = JsonConvert.DeserializeObject<List<Korisnik>>(json);
            }
        }

        public void AzurirajAdmine()
        {
            string putanja = HostingEnvironment.MapPath("~/App_Data/administratori.json");
            string json = JsonConvert.SerializeObject(listaAdministatora);

            // Upisivanje u datoteku tako sto se sav sadrzaj obrise i upise novi
            File.WriteAllText(putanja, json);
        }

        public Korisnik PronadjiPrijava(Korisnik korisnik)
        {
            foreach (var k in listaAdministatora)
                if (k.Lozinka.Equals(korisnik.Lozinka) && k.KorisnickoIme.Equals(korisnik.KorisnickoIme))
                    return k;

            return null;
        }
    }
}