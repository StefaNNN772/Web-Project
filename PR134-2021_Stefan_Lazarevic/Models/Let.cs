using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PR134_2021_Stefan_Lazarevic.Models
{
    public class Let
    {
        public int Id { get; set; }
        public string Aviokompanija { get; set; }
        public int IdAviokompanije { get; set; }
        public string PolaznaDestinacija { get; set; }
        public string OdredisnaDestinacija { get; set; }
        public DateTime DatumPolaska { get; set; }
        public DateTime DatumDolaska { get; set; }
        public int BrojSlobodnihMjesta { get; set; }
        public int BrojZauzetihMjesta { get; set; }
        public int Cijena { get; set; }
        public StatusLetaEnum Status { get; set; }

        public bool Obrisan { get; set; }

        public static List<int> ListaId = new List<int>();

        public int GenerisiIdLeta()
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