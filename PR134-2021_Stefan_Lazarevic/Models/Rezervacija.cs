using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PR134_2021_Stefan_Lazarevic.Models
{
    public class Rezervacija
    {
        public int Id { get; set; }
        public string Korisnik { get; set; }
        public int Let { get; set; }
        public int BrojPutnika { get; set; }
        public int Cijena { get; set; }
        public StatusEnum Status { get; set; }

        public List<int> ListaId = new List<int>();

        public int GenerisiIdRezervacije()
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