using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PR134_2021_Stefan_Lazarevic.Models
{
    public class Recenzija
    {
        public int Id { get; set; }
        public string Recezent { get; set; }
        public int IdLeta { get; set; }
        public string Aviokompanija { get; set; }
        public int IdAviokompanije { get; set; }
        public string Naslov { get; set; }
        public string Sadrzaj { get; set; }
        public StatusRecenzijeEnum StatusRecenzije { get; set; }

        public static List<int> ListaId = new List<int>();
        public static int GenerisiId()
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