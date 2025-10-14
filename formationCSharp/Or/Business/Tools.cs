using MaterialDesignThemes.Wpf;
using Or.Models;
using System;
using System.Globalization;

namespace Or.Business
{
    /// <summary>
    /// Enumération décrivant le type de transaction
    /// Facilite la prise en charge par GestionBancaire
    /// </summary>
    public enum Operation
    {
        DepotSimple = 0,
        RetraitSimple = 1,
        InterCompte = 2
    }

    public static class Tools
    {
        public static DateTime ConversionDate(string horodatage)
        {
            CultureInfo culture = CultureInfo.CurrentCulture;
            return DateTime.ParseExact(horodatage, "dd/MM/yyyy HH:mm:ss", culture);
        }

        /// <summary>
        /// Est ce que l'identifiant désigne l'extérieur ?
        /// </summary>
        /// <param name="identifiant"></param>
        /// <returns></returns>
        private static bool EstExterieur(int identifiant)
        {
            return identifiant == 0;
        }

        /// <summary>
        /// Est ce que la transaction est Extérieur -> Extérieur ?
        /// Si oui, elle est invalide
        /// </summary>
        /// <param name="expediteur"></param>
        /// <param name="destinataire"></param>
        /// <returns></returns>
        public static bool EstTransactionExterieure(int expediteur, int destinataire)
        {
            return expediteur + destinataire == 0;
        }


        public static Operation TypeTransaction(int expediteur, int destinataire)
        {
            // 0 -> Compte
            if (EstExterieur(expediteur))
            {
                return Operation.DepotSimple;
            }
            // Compte -> Compte
            else if (EstExterieur(destinataire))
            {
                return Operation.RetraitSimple;
            }
            // Compte -> Compte
            else
            {
                return Operation.InterCompte;
            }
        }

        public static string TypeTransacConverter(Operation operation)
        {
            // 0 -> Compte
            switch (operation)
            {
                case Operation.DepotSimple:
                    return "Dépôt";
                case Operation.RetraitSimple:
                    return "Retrait";
                case Operation.InterCompte:
                    return "Virement";
                default:
                    // je ne trouve pas le bon type d'exception
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// appliquer une transaction si elle vérifie toutes les conditions
        /// </summary>
        /// <param name="t"></param>
        /// <param name="carte"></param>
        public static void AppliquerUneTransaction(Transaction t, Carte carte)
        {
            Compte ex = null;
            Compte de = null;
            Operation type;

            if (t.Expediteur != 0)
            {
                ex = SqlRequests.RetrouverUnCompteParId(t.Expediteur);
            }
            if (t.Destinataire != 0)
            {
                de = SqlRequests.RetrouverUnCompteParId(t.Destinataire);
            }


            // ne rien faire si le titulaire de la carte
            // n'est ni le destinataire, ni l'expediteur.
            if (carte.Id != ex?.IdentifiantCarte && carte.Id != de?.IdentifiantCarte)
            {
                return;
            }

            type = TypeTransaction(t.Expediteur, t.Destinataire);

            if (type == Operation.InterCompte)
            {
                GererUnVirement(carte, t, ex, de);
            }
            else if (type == Operation.DepotSimple)
            {

                GererUnDepot(t, de);
            }
            else
            {
                GererRetrait(carte, t, ex, de);
            }

        }

        /// <summary>
        /// effectuer un virement
        /// </summary>
        /// <param name="carte"></param>
        /// <param name="t"></param>
        /// <param name="ex"></param>
        /// <param name="de"></param>
        private static void GererUnVirement(Carte carte, Transaction t, Compte ex, Compte de)
        {
            CodeResultatTransaction resCarte = carte.EstRetraitAutoriseNiveauCarte(t, ex, de);
            bool retraitValide = ex.EstRetraitValide(t);
            if (retraitValide && resCarte == CodeResultatTransaction.Success)
            {
                SqlRequests.EffectuerModificationOperationInterCompte(t, ex.IdentifiantCarte, de.IdentifiantCarte);
            }
        }

        /// <summary>
        /// effectuer un depot
        /// </summary>
        /// <param name="t"></param>
        /// <param name="de"></param>
        private static void GererUnDepot(Transaction t, Compte de)
        {
            if (de.EstDepotValide(t))
            {
                SqlRequests.EffectuerModificationOperationSimple(t, de.IdentifiantCarte);

            }
        }

        /// <summary>
        /// effectuer un retrait
        /// </summary>
        /// <param name="carte"></param>
        /// <param name="t"></param>
        /// <param name="ex"></param>
        /// <param name="de"></param>
        private static void GererRetrait(Carte carte, Transaction t, Compte ex, Compte de)
        {
            Compte compteBanque = new Compte(0, 0, TypeCompte.Courant, 0);
            CodeResultatTransaction retourCarte = carte.EstRetraitAutoriseNiveauCarte(t, ex, compteBanque);
            CodeResultatTransaction retourCompte = ex.EstRetraitValide(t) ? CodeResultatTransaction.Success : CodeResultatTransaction.PlafondMaxAutoriseDepasse;
            if (retourCarte == CodeResultatTransaction.Success && retourCompte == CodeResultatTransaction.Success)
            {
                SqlRequests.EffectuerModificationOperationSimple(t, ex.Id);
            }
        }

    }
}
