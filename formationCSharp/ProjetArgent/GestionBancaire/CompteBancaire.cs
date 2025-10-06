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

        private decimal _solde = 0;
        private TypeCompteEnum.TypeCompte _type;
        private  CarteBancaire _carteBancaire;
        private string _carteBancaireId;

        public int Id = 0;

        public CompteBancaire(int id, TypeCompteEnum.TypeCompte type, CarteBancaire carteBancaire, decimal solde = 0)
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

        // duplication de DeposerDeLargent 2 fonction qui on la même fonctionalitée 
        // a ce moment mais ce n'est pas guaranti dans le futur.
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

        public bool EffectueruneTransactionVersUnCompte(CompteBancaire destinataire, Transaction transaction)
        {
           if (EffectuerUnVirement(transaction.Montant, transaction.Horodatage))
            {
                destinataire.DeposerDeLargent(transaction.Montant);
                _carteBancaire.MAJHistorique(transaction);
                return true;
            }
           return false;
        }
    }
}
