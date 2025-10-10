using Or.Business;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Or.Models


{
    public enum CodeResultatTransaction
    {
        Success=0,
        PlafondMaxAutoriseDepasse=1,
        MontantInvalide=2,
        SoldeInsuffisant=3,
        VirementVersLivret=4,
        AutreCarteInterdite=5,
        TransactionExterneExterne=6,
        VirementAvecExterneNonAut=7
    }
    public class Carte
    {
        public long Id { get; set; }
        public decimal Plafond { get; set; }
        public decimal PlafondAjour { get; set; }
        public string PrenomClient { get; set; }
        public string NomClient { get; set; }
        public List<int> ListComptesId { get; set; }
        public List<Transaction> Historique { get; private set; }
        
        public Carte(long id, string prenom, string nom, decimal plafondMax = 0)
        {
            Id = id;
            PrenomClient = prenom;
            NomClient = nom;
            Plafond = plafondMax == 0 ? 500 : plafondMax;
            ListComptesId = new List<int>();
            Historique = new List<Transaction>();
        }

        public void AlimenterHistoriqueEtListeComptes(List<Transaction> hist, List<int> comptesId)
        {
            ListComptesId = comptesId;
            Historique = hist;
        }

        public void AlimenterPlafondActualise(List<Transaction> hist, List<int> comptesId)
        {
            decimal plafondAjour = GetPlafondActualise();
            PlafondAjour = plafondAjour == 0 ? 500 : plafondAjour;
        }

        public void AjoutTransactionValidee(Transaction transac)
        {
            Historique.Add(transac);
        }

        // -------------------------------------------------------------------------------------------------------------
        //                              Contraintes sur les retraits et virements
        // -------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Est-ce que le retrait (retrait simple, virement) est il autorisé au niveau de la carte ? 
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="Expediteur"></param>
        /// <param name="Destinataire"></param>
        /// <returns></returns>
        public CodeResultatTransaction EstRetraitAutoriseNiveauCarte(Transaction transaction, Compte Expediteur, Compte Destinataire)
        {
            CodeResultatTransaction res = EstOperationAutoriseeContraintesComptes(Expediteur, Destinataire);
            return res != CodeResultatTransaction.Success ? res : EstEligibleMaximumRetraitHebdomadaire(transaction.Montant, transaction.Horodatage);

        }

        /// <summary>
        /// Test d'éligibilité par rapport au plafond maximal de la carte
        /// </summary>
        /// <param name="montant"></param>
        /// <param name="dateEffet"></param>
        /// <returns></returns>
        private CodeResultatTransaction EstEligibleMaximumRetraitHebdomadaire(decimal montant, DateTime dateEffet)
        {

            List<Transaction> retraitsHisto = Historique.Where(x => (x.Horodatage > dateEffet.AddDays(-10)) && ListComptesId.Contains(x.Expediteur)).Select(x => x).ToList();
            decimal sommeHisto = montant + retraitsHisto.Sum(x => x.Montant);
            return (sommeHisto < Plafond ? CodeResultatTransaction.Success : CodeResultatTransaction.PlafondMaxAutoriseDepasse);
        }

        /// <summary>
        /// retourner le plafond actualisé après prendre en compte l'historique des transactions
        /// </summary>
        /// <param name="dateEffect"></param>
        /// <returns></returns>
        public decimal GetPlafondActualise()
        {
            DateTime dateEffet = DateTime.Now;
            List<Transaction> retraitsHisto = Historique.Where(x => (x.Horodatage > dateEffet.AddDays(-10)) && ListComptesId.Contains(x.Expediteur)).Select(x => x).ToList();
            return Plafond - retraitsHisto.Sum(x => x.Montant);
        }


        /// <summary>
        /// Est-ce que les contraintes sur les comptes bancaires sont respectées ? 
        /// </summary>
        /// <param name="Expediteur"></param>
        /// <param name="Destinataire"></param>
        /// <returns></returns>
        private CodeResultatTransaction EstOperationAutoriseeContraintesComptes(Compte Expediteur, Compte Destinataire)
        {
            // Est-ce que la transaction demandée est possible ?
            if (Tools.EstTransactionExterieure(Expediteur.Id, Destinataire.Id))
            {
                return CodeResultatTransaction.TransactionExterneExterne;
            }

            // Opération Interne 
            if (EstOperationInterne(Expediteur.Id, Destinataire.Id))
            {
                return CodeResultatTransaction.Success;
            }
            // Opération externe
            else
            {
                return EstOperationExterneAutorise(Expediteur, Destinataire);
            }
        }

        /// <summary>
        /// Le compte appartient-il à la carte ? 
        /// </summary>
        /// <param name="idtCpt"></param>
        /// <returns></returns>
        private bool EstComptePresent(int idtCpt)
        {
            return ListComptesId.Exists(x => x == idtCpt);
        }

        /// <summary>
        /// Est ce qu'il s'agit d'une opération interne possible en principe ? 
        /// </summary>
        /// <param name="cptExt"></param>
        /// <param name="cptDest"></param>
        /// <returns></returns>
        private bool EstOperationInterne(int cptExt, int cptDest)
        {
            Operation operation = Tools.TypeTransaction(cptExt, cptDest);
            return 
               (
                operation == Operation.DepotSimple || 
                operation == Operation.RetraitSimple || 
               (operation == Operation.InterCompte && EstComptePresent(cptExt) && EstComptePresent(cptDest))
               );
        }

        /// <summary>
        /// S'agit il d'une opération inter-compte externe possible ? 
        /// </summary>
        /// <param name="Expediteur"></param>
        /// <param name="Destinataire"></param>
        /// <returns></returns>
        private CodeResultatTransaction EstOperationExterneAutorise(Compte Expediteur, Compte Destinataire)
        {
            Operation operation = Tools.TypeTransaction(Expediteur.Id, Destinataire.Id);

            bool opEntreComptes = operation == Operation.InterCompte;

            bool OpCourantCourant = Expediteur.TypeDuCompte == TypeCompte.Courant && Destinataire.TypeDuCompte == TypeCompte.Courant;

            if (!opEntreComptes) return CodeResultatTransaction.VirementAvecExterneNonAut;
            if (!OpCourantCourant) return CodeResultatTransaction.VirementVersLivret;
            return CodeResultatTransaction.Success;
        }

        public static string Label(CodeResultatTransaction codeTransaction)
        {
            switch (codeTransaction)
            {
                case CodeResultatTransaction.MontantInvalide:
                    return "Montant invalide.";
                case CodeResultatTransaction.VirementVersLivret:
                    return "Virements Entre Compte Active et Livret Attachées à 2 cartes différentes.";
                case CodeResultatTransaction.PlafondMaxAutoriseDepasse:
                    return "Le Plafond Maximum De Transaction Authorisé Est Dépassé.";
                case CodeResultatTransaction.SoldeInsuffisant:
                    return "Le Solde Est Insuffisant Pour Ta Transaction";
                case CodeResultatTransaction.VirementAvecExterneNonAut:
                    return " C'est Une Transaction Avec Un Externe Qui Est Non Autorisé.";
                case CodeResultatTransaction.TransactionExterneExterne:
                    return "Transaction Externe Externe Non Authorisé.";
                case CodeResultatTransaction.AutreCarteInterdite:
                    return "Erreur Inconnue Avec La Carte. Il faut Informer les dévélopeurs";
                default:
                    return "Si tu vois ce message Cours";
            }
        }

    }
}