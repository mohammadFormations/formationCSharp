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


        public Beneficiaire(int idCpt, long numCarte)
        {
            IdCpt = idCpt;
            NumCarte = numCarte;
        }

    }
}
