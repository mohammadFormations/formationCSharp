using System;
using System.Xml.Serialization;
using Or.Business;

namespace Or.Models
{

    [XmlRoot]
    public class Transaction
    {

        [XmlElement("Identifiant")]
        public int IdTransaction { get; set; }


        [XmlIgnore]
        public DateTime Horodatage { get; set; }

        [XmlElement("Date")]

        public string Date
        {
            get
            {
                return Horodatage.ToString("dd/MM/yyyy hh:mm:ss");
            }
            set
            {

            }
        }
 

        [XmlElement("Type")]
        public string TypeTransactionstringifie { get; set; }


        [XmlElement("CompteExpediteur")]
        public string CompteExpediteur
        {
            get

            {
                if (Expediteur == 0) return null;
                return Expediteur.ToString();
            }
            set { }

        }

        [XmlIgnore]
        public int Expediteur { get; set; }

        [XmlElement("CompteDestinataire")]
        public string CompteDestinataire
        {
            get
            {
                if (Destinataire == 0) return null;
                return Destinataire.ToString();
            }
            set { }
        }



        [XmlIgnore]
        public int Destinataire { get; set; }

        [XmlElement("Montant")]
        public decimal Montant { get; set; }

        public Transaction () { }
        public Transaction(int idTransaction, DateTime horodatage, decimal montant, int expediteur, int destinataire)
        {
            IdTransaction = idTransaction;
            Horodatage = horodatage;
            Montant = montant;
            Expediteur = expediteur;
            Destinataire = destinataire;
            TypeTransactionstringifie = Tools.TypeTransacConverter(Tools.TypeTransaction(expediteur, destinataire));
        }

    }
}
