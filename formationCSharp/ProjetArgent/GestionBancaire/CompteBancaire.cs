using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetArgent.GestionBancaire;

namespace ProjetArgent.GestionBancaire
{
    public class CompteBancaire
    {

        private decimal _solde;
        private TypeCompteEnum.TypeCompte _type;
        public  CarteBancaire _carteBancaire;

        public int Id = 0;

        public CompteBancaire(int id, TypeCompteEnum.TypeCompte type, CarteBancaire carteBancaire, decimal solde)
        {
            _solde = solde;
            _type = type;
            _carteBancaire = carteBancaire;
            Id = id;
        }

        public bool DeposerDeLargent(decimal montant)
        {
            if (montant <= 0) return false;
            _solde += montant;
            return true;
        }

        public bool RecevoirUneTransaction(decimal montant)
        {
            if (montant <= 0) return false;
            _solde += montant;
            return true;
        }

        public bool EffectuerUnPrelevement(decimal montant)
        {
            if (montant <= 0) return false;
            _solde += montant;
            return true;
        }


        public bool RetirerDeLargent(decimal montant, DateTime datime)
        {
            if (montant <= 0) return false;
            if (montant > _solde) return false;
            if (_carteBancaire.GetMaximumMontant(datime) < montant) return false;
            _solde -= montant;

            return true;

        }

        public bool EffectuerUnVirement(decimal montant, DateTime datime)
        {
            if (montant <= 0) return false;
            if (montant > _solde) return false;
            if (_carteBancaire.GetMaximumMontant(datime) < montant) return false;

            _solde -= montant;
            return true;
        }

        public bool IsTransactionPossibleEntreComptes(CompteBancaire destinataire)
        {
            if (destinataire._carteBancaire.Numero == _carteBancaire.Numero) return true;
            if (destinataire._type == TypeCompteEnum.TypeCompte.Courant
                && _type == TypeCompteEnum.TypeCompte.Courant) return true;
            return false;
          
        }

        public bool EffectuerUneTransactionVersUnCompte(CompteBancaire destinataire, Transaction transaction)
        {
            if (!IsTransactionPossibleEntreComptes(destinataire)) return false;
            if (!EffectuerUnVirement(transaction.Montant, transaction.Horodatage)) return false;

            destinataire.DeposerDeLargent(transaction.Montant);
            _carteBancaire.MAJHistorique(transaction);
            return true;

        }
    }
}
