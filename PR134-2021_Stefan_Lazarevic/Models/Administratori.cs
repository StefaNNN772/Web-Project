using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace PR134_2021_Stefan_Lazarevic.Models
{
    public class Administratori
    {
        public List<Korisnik> ListaAdministratora { get; set; }

        public Administratori(string putanja)
        {
            using (StreamReader reader = new StreamReader(putanja))
            {
                string json = reader.ReadToEnd();
                //deserialize koriscenjem paketa
                ListaAdministratora = JsonConvert.DeserializeObject<List<Korisnik>>(json);
            }
        }

        public void AzurirajAdministratore()
        {
            string putanja = HostingEnvironment.MapPath("~/App_Data/admini.json");
            string json = JsonConvert.SerializeObject(ListaAdministratora);

            // Upisivanje u datoteku tako sto se sav sadrzaj obrise i upise novi
            File.WriteAllText(putanja, json);
        }

        public Korisnik PronadjiPrijava(Korisnik korisnik)
        {
            foreach (var k in ListaAdministratora)
                if (k.Lozinka.Equals(korisnik.Lozinka) && k.KorisnickoIme.Equals(korisnik.KorisnickoIme))
                    return k;

            return null;
        }
    }
}