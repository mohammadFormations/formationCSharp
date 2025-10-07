using ProjetArgent.GestionBancaire;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetArgent.IO.serializeurs
{
    public class TransactionDeserializer
    {
        private int _id;
        private DateTime _horodatage;
        private decimal _montant;
        private int _expediteurId;
        private int _destinateurId;
        public bool ExtractTransactionFromLine(string line, List<CompteBancaire> comptes, List<Transaction> transactions, out Transaction transaction)
        {
            transaction = null;
            if (line == null) return false;
            string[] elements = line.Split(';').ToList().FindAll(el => string.Compare(el, "") != 0).ToArray<string>();
            if (elements.Length != 5) return false;

            if (!SetId(elements[0], transactions)) return false;
            if (!SetHorodatage(elements[1])) return false;
            if (!SetMontant(elements[2])) return false;
            if (!SetExpediteur(elements[3], comptes)) return false;
            if (!SetDestinateur(elements[4], comptes)) return false;


            transaction = new Transaction(_id, _horodatage, _montant, _expediteurId, _destinateurId);
            return true;
        }

        private bool SetExpediteur(string compteBancaireId, List<CompteBancaire> comptes)
        {
            bool state = VerifyCompteBancaire(compteBancaireId, comptes, out int id);
            if (state)
            {
                _expediteurId = id;
                return true;
            }
            return false;
        }


        private bool VerifyCompteBancaire(string compteBancaireId, List<CompteBancaire> comptes, out int compteId)
        {
            compteId = 0;
            if (compteBancaireId == null) return false;

            bool state = int.TryParse(compteBancaireId, out int num);
            if (!state) return false;

            if (num < 0) return false;
            if (num == 0)
            {
                compteId = 0;
                return true;
            }

            foreach (CompteBancaire compte in comptes)
            {
                if (compte.Id == num)
                {
                    compteId = num;
                    return true;
                }
            }

            return false;
        }

        private bool SetDestinateur(string compteBancaireId, List<CompteBancaire> comptes)
        {
            bool state = VerifyCompteBancaire(compteBancaireId, comptes, out int id);
            if (state)
            {
                _destinateurId = id;
                return true;
            }
            return false;
        }

        private bool SetId(string id, List<Transaction> transactions)
        {
            bool state = int.TryParse(id, out _id);
            if (!state) return false;

            foreach (Transaction transaction in transactions)
            {
                if (transaction.NumTransaction == _id)
                {
                    return false;
                }
            }

            return true;
        }

        private bool SetHorodatage(string horodatage)
        {
            bool state = DateTime.TryParseExact(horodatage, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture, 0, out DateTime datime);
            if (!state) return false;
            _horodatage = datime;
            return true;
        }

        private bool SetMontant(string solde)
        {
            bool state = decimal.TryParse(solde, NumberStyles.Number, CultureInfo.InvariantCulture, out _montant);
            if (!state || _montant < 0) return false;
            return true;
        }

    }
}