using ProjetArgent.GestionBancaire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetArgent.GestionBancaire
{
    public class Transaction
    {
        public int NumTransaction;
        public int ExpediteurId;
        public int DestinateurId;

        public decimal Montant { get; private set; }
        public DateTime Horodatage { get; private set; }

        Transaction(int numTransaction, DateTime horodatage, decimal montant, int expediteurId, int destinateurId)
        {
            NumTransaction = numTransaction;
            this.Horodatage = horodatage;
            Montant = montant;
            ExpediteurId = expediteurId;
            DestinateurId = destinateurId;
        }
    }
}
