using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetArgent.GestionBancaire
{
    public static class Banque
    {
        public static  List<CompteBancaire> CompteBancaires = new List<CompteBancaire>();
        public static List<CarteBancaire> CartesBancaires = new List<CarteBancaire>();
        public static List<Transaction> Transactions = new List<Transaction>();

    }
}
