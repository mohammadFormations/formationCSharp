using System;

namespace ProjetArgent.GestionBancaire
{
    public class CompteBancaire
    {

        private decimal _solde;
        public TypeCompteEnum.TypeCompte Type;
        public  CarteBancaire _carteBancaire;

        public int Id = 0;

        public CompteBancaire(int id, TypeCompteEnum.TypeCompte type, CarteBancaire carteBancaire, decimal solde)
        {
            _solde = solde;
            Type = type;
            _carteBancaire = carteBancaire;
            Id = id;
        }

        public bool DeposerDeLargent(decimal montant)
        {
            if (montant <= 0)
            {
                return false;
            }

            _solde += montant;
            return true;
        }
        // Suppression du code mort 
/*
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
*/

        public bool RetirerDeLargent(decimal montant, DateTime datime)
        {
            // Tu peux combiner les deux conditions
            if (montant <= 0 || montant > _solde)
            {
                return false;
            }

            if (_carteBancaire.GetMaximumMontant(datime) < montant) return false;
            _solde -= montant;

            return true;

        }

        // On notera que les deux méthodes RetirerDeLargent et RetirerDeLargent sont identiques, soit tu peux créer une méthode privée qui est appelée dans les deux
        // ou une seule méthode
        public bool EffectuerUnVirement(decimal montant, DateTime datime)
        {
            if (montant <= 0) return false;
            if (montant > _solde) return false;
            if (_carteBancaire.GetMaximumMontant(datime) < montant) return false;

            _solde -= montant;
            return true;
        }

    }
}
