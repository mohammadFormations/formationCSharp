using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetArgent.GestionBancaire;

namespace ProjetArgent
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Banque bnp = new Banque();
            bnp.ProcessBatch();
        }
    }
}
