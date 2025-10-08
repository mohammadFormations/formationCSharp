using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetArgent.GestionBancaire
{
    internal class HistoriqueDeTransactions
    {
        private Stack<Transaction> _historiqueDesTransactions;

        public HistoriqueDeTransactions()
        {
            _historiqueDesTransactions = new Stack<Transaction>();
        }

        public void AddTransaction(Transaction transaction)
        {
            _historiqueDesTransactions.Push(transaction);
        }

        public decimal GetMontantPendantSpan(DateTime date, TimeSpan span)
        {
            decimal montant = 0;
            foreach (Transaction transaction in _historiqueDesTransactions)
            {
                if (date - transaction.Horodatage > span) continue;
                montant += transaction.Montant;
            }
            return montant;
        }

    }
}
