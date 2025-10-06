using ProjetArgent.GestionBancaire;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetArgent.IO.serializeurs
{
    internal class TransactionDeserializer
    {
        private int _id;
        private DateTime _horodatage;
        private decimal _montant;
        private int _expediteurId;
        private int _destinateurId;
        private CompteBancaire _expediteur;
        private CompteBancaire _destinataire;

        public bool ExtractCompteFromLine(string line, List<CompteBancaire> comptes, List<Transaction> transactions, out CompteBancaire compteBancaire)
        {
            compteBancaire = null;
            String[] elements = line.Split(';');
            if (elements.Length != 5) return false;

            if (!SetId(elements[0], transactions)) return false;
            if (!SetHorodatage(elements[1])) return false;
            if (!SetMontant(elements[2])) return false;
            if (!SetExpediteur(elements[3])) return false;
            if (!SetDestinateur(elements[4])) return false;


            compteBancaire = new Transaction(_id, _horodatage, _montant, _expediteurId, _destinateurId);
            return false;
        }

        private bool SetCarteBancaire(string carteBancaireId, List<CarteBancaire> cartes)
        {
            if (carteBancaireId == null || carteBancaireId.Length != 16) return false;

            bool state = int.TryParse(carteBancaireId, out int num);
            if (!state) return false;

            if (num < 0) return false;
            _carteBancaireId = carteBancaireId;

            foreach (CarteBancaire carte in cartes)
            {
                if (carte.Numero == carteBancaireId)
                {
                    _carteBancaire = carte;
                    return true;
                }
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

        private bool SetType(string type)
        {
            bool state = Enum.TryParse(type, out TypeCompteEnum.TypeCompte parsedType);
            if (!state) return false;
            _type = parsedType;
            return true;
        }

        private bool SetSolde(string solde)
        {
            bool state = decimal.TryParse(solde, NumberStyles.Number, CultureInfo.InvariantCulture, out _solde);
            if (!state || _solde < 0) return false;
            return true;
        }

    }
}