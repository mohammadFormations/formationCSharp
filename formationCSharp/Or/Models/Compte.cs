using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Or.Serializeurs;

namespace Or.Models
{
    public enum TypeCompte { Courant, Livret }

    [XmlRoot]
    public class Compte
    {
        public Compte() { }

        [XmlElement("Identifiant")]
        public int Id { get; set; }
        public long IdentifiantCarte { get; set; }

        [XmlElement("Type")]
        public TypeCompte TypeDuCompte { get; set; }


        [XmlElement("Solde")]
        public decimal Solde { get; set; }


        [XmlElement("Transactions")]
        public ExportCompteTransactions Transactions { get; set; }



        public Compte(int id, long identifiantCarte, TypeCompte type, decimal soldeInitial)
        {
            Id = id;
            IdentifiantCarte = identifiantCarte;
            TypeDuCompte = type;
            Solde = soldeInitial;
        }


        /// <summary>
        /// Action de dépôt d'argent sur le compte bancaire
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns>Statut du dépôt</returns>
        public bool EstDepotValide(Transaction transaction)
        {
            if (transaction.Montant > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Action de retrait d'argent sur le compte bancaire
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns>Statut du retrait</returns>
        public bool EstRetraitValide(Transaction transaction)
        {
            if (EstRetraitAutorise(transaction.Montant))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool EstRetraitAutorise(decimal montant)
        {
            return Solde >= montant && montant > 0;
        }

    }
}
