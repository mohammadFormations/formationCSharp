using Or.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Or.Models
{
    public class Beneficiaire
    {
        public int IdCpt { get; set; }

        public long NumCarte { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }

        public Beneficiaire(int idCpt, long numCarte, string nom=null, string prenom=null)
        {
            IdCpt = idCpt;
            NumCarte = numCarte;
            Nom = nom;
            Prenom = prenom;
        }

    }
}
