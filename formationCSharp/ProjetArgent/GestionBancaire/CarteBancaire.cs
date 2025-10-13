using System;

namespace ProjetArgent.GestionBancaire
{
    public class CarteBancaire
    {
        private TimeSpan _spanDePlafond = new TimeSpan(10, 0, 0, 0);

        private readonly HistoriqueDeTransactions _historique;
        private decimal _plafond;


        public string Numero;   

        public CarteBancaire(string numero, decimal plafond)
        {
            Numero = numero;
            _plafond = plafond;
            _historique = new HistoriqueDeTransactions();
        }

        public decimal GetMaximumMontant(DateTime datime)
        {
            return _plafond - _historique.GetMontantPendantSpan(datime, _spanDePlafond);
        }

        public void MAJHistorique(Transaction transaction)
        {
            _historique.AddTransaction(transaction);
        }

    }
}
