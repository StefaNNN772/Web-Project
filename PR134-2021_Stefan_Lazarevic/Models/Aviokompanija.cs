using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PR134_2021_Stefan_Lazarevic.Models
{
    public class Aviokompanija
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Adresa { get; set; }
        public string Kontakt { get; set; }
        public List<Let> Letovi { get; set; }
        public List<Recenzija> ListaRecenzija { get; set; }
        public bool Obrisana { get; set; }

        public static List<int> ListaId = new List<int>();
        public int GenerisiId()
        {
            Random random = new Random();
            int noviId;

            do
            {
                noviId = random.Next();
            } while (ListaId.Contains(noviId));

            ListaId.Add(noviId);
            return noviId;
        }
    }
}