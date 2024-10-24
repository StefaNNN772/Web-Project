using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace PR134_2021_Stefan_Lazarevic.Models
{
    public class Korisnici
    {
        public List<Korisnik> ListaKorisnika { get; set; }

        public Korisnici(string putanja)
        {
            using (StreamReader reader = new StreamReader(putanja))
            {
                string json = reader.ReadToEnd();
                //deserialize koriscenjem paketa
                ListaKorisnika = JsonConvert.DeserializeObject<List<Korisnik>>(json);
            }
        }

        public void DodajKorisnika(Korisnik korisnik, string administrator)
        {
            korisnik.Rezervacije = new List<Rezervacija>();
            if (administrator == "nije")
                korisnik.TipKorisnika = TipKorisnikaEnum.Putnik;
            else if (administrator == "jeste")
                korisnik.TipKorisnika = TipKorisnikaEnum.Administrator;
            ListaKorisnika.Add(korisnik);
        }

        public void AzurirajKorisnike()
        {
            string putanja = HostingEnvironment.MapPath("~/App_Data/korisnici.json");
            string json = JsonConvert.SerializeObject(ListaKorisnika);

            if (File.Exists(putanja))
            {
                using (FileStream fs = new FileStream(putanja, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    fs.Close();
                }
            }

            File.WriteAllText(putanja, json);
        }

        public bool Pronadji(Korisnik korisnik)
        {
            foreach (var k in ListaKorisnika)
                if (k.Email.Equals(korisnik.Email) || k.KorisnickoIme.Equals(korisnik.KorisnickoIme))
                    return true;

            return false;
        }

        public Korisnik PronadjiPrijava(Korisnik korisnik)
        {
            foreach (var k in ListaKorisnika)
                if (k.Lozinka.Equals(korisnik.Lozinka) && k.KorisnickoIme.Equals(korisnik.KorisnickoIme))
                    return k;

            return null;
        }
    }
}