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
                if (DateTime.TryParse(value, out DateTime horodatage))
                {
                    Horodatage = horodatage;
                }
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
            set
            {
                if (value == null) return;
                if (int.TryParse(value, out int expediteur))
                {
                    Expediteur = expediteur;
                }
            }

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
            set
            {
                if (value == null) return;
                if (int.TryParse(value, out int destinataire))
                {
                    Destinataire = destinataire;
                }
            }
        }



        [XmlIgnore]
        public int Destinataire { get; set; }

        [XmlIgnore]
        public decimal Montant { get; set; }



        [XmlElement("Montant")]
        public string MontantStr
        {
            get
            {
                return Montant.ToString("C2");
            }

            set
            {
                if (value == null) Montant = 0;
                if (decimal.TryParse(value.Replace(".", ",").Trim(new char[] { '€', ' ' }), out decimal montant))
                {
                    Montant = montant;
                }
            }
        }

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
