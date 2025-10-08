using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetArgent.IO.FileStreams;
using ProjetArgent.IO.ObjectBuilder;
using ProjetArgent.IO.serializeurs;
using ProjetArgent.GestionBancaire.Enums;
using System.Runtime.InteropServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ProjetArgent.GestionBancaire
{
    public static class Banque
    {
        private const string _comptes_path = "comptes.csv";
        private const string _cartes_path = "cartes.csv";
        private const string _transactions_path = "transactions.csv";
        private const string _compte_rendu = "compte_rendu.csv";

        private static CarteFileInput carteFileReader = new CarteFileInput(_cartes_path);
        private static CompteFileInput compteFileReader = new CompteFileInput(_comptes_path);
        private static TransactionFileInput transactionFileReader = new TransactionFileInput(_transactions_path);
        private static CompteRenduFileOutput compteRenduFileWriter = new CompteRenduFileOutput(_compte_rendu);


        public static  List<CompteBancaire> CompteBancaires = new List<CompteBancaire>();
        public static List<CarteBancaire> CartesBancaires = new List<CarteBancaire>();
        public static List<Transaction> Transactions = new List<Transaction>();

        public static void ProcessBatch()
        {
            LireCartes();
            LireComptes();
            LireTransactions();
            ProcessAllTransactions();

        }

        public static void LireCartes()
        {
            CarteDeserializer deserializer = new CarteDeserializer();
            CarteBancaire carteBancaire;
            bool status = deserializer.ExtractCarteFromLine(carteFileReader.ReadCarte(), out carteBancaire);
            while (status)
            {
                CartesBancaires.Add(carteBancaire);
                status = deserializer.ExtractCarteFromLine(carteFileReader.ReadCarte(), out carteBancaire);
            }
        }

        public static void LireComptes()
        {
            CompteBancaireDeserializer deserializer = new CompteBancaireDeserializer();
            bool status = deserializer.ExtractCompteFromLine(compteFileReader.ReadCompte(), CartesBancaires, CompteBancaires,  out CompteBancaire compteBancaire);
            while (status)
            {
                CompteBancaires.Add(compteBancaire);
                status = deserializer.ExtractCompteFromLine(compteFileReader.ReadCompte(), CartesBancaires, CompteBancaires, out compteBancaire);
            }
        }


        public static void LireTransactions()
        {
            TransactionDeserializer deserializer = new TransactionDeserializer();
            bool status = deserializer.ExtractTransactionFromLine(transactionFileReader.ReadTransaction(), CompteBancaires, Transactions, out Transaction transaction);
            while (status)
            {
                Transactions.Add(transaction);
                status = deserializer.ExtractTransactionFromLine(transactionFileReader.ReadTransaction(), CompteBancaires, Transactions, out transaction);
            }
        }
        public static void HandleDeposition(Transaction transaction)
        {
            CompteBancaire destinateur = CompteBancaires.FirstOrDefault(compte => compte.Id == transaction.DestinateurId);
            bool status = destinateur.DeposerDeLargent(transaction.Montant);
            compteRenduFileWriter.FormatEtEcritCompteRendu(transaction.NumTransaction, status);
        }

        public static void HandleRetrait(Transaction transaction)
        {
            CompteBancaire expediteur = CompteBancaires.FirstOrDefault(compte => compte.Id == transaction.ExpediteurId);
            bool status = expediteur.RetirerDeLargent(transaction.Montant, transaction.Horodatage);
            if (status) expediteur._carteBancaire.MAJHistorique(transaction);
            compteRenduFileWriter.FormatEtEcritCompteRendu(transaction.NumTransaction, status);
        }

        public static void HandleVirement(Transaction transaction)
        {
            CompteBancaire destinateur = CompteBancaires.FirstOrDefault(compte => compte.Id == transaction.DestinateurId);
            CompteBancaire expediteur = CompteBancaires.FirstOrDefault(compte => compte.Id == transaction.ExpediteurId);
            bool status = EffectuerUneTransactionVersUnCompte(destinateur, expediteur, transaction);
            compteRenduFileWriter.FormatEtEcritCompteRendu(transaction.NumTransaction, status);

        }


        public static void ProcessTransaction(Transaction transaction)
        {
            if (transaction.ExpediteurId == 0 && transaction.DestinateurId != 0)
            {
                HandleDeposition(transaction);
            } else if (transaction.DestinateurId == 0 && transaction.ExpediteurId != 0)
            {
                HandleRetrait(transaction);

            } else if (transaction.ExpediteurId != 0 && transaction.DestinateurId != 0)
            {
                HandleVirement(transaction);
            }
            else
            {
                compteRenduFileWriter.FormatEtEcritCompteRendu(transaction.NumTransaction, false);
            }
        }

        public static void ProcessAllTransactions()
        {
            foreach (Transaction transaction in Transactions)
            {
                ProcessTransaction(transaction); 
            }
            compteRenduFileWriter.close();
        }

        public static bool IsTransactionPossibleEntreComptes(CompteBancaire destinataire, CompteBancaire expediteur)
        {
            if (destinataire._carteBancaire.Numero == expediteur._carteBancaire.Numero) return true;
            if (destinataire.Type == TypeCompteEnum.TypeCompte.Courant
                && expediteur.Type == TypeCompteEnum.TypeCompte.Courant) return true;
            return false;

        }


        public static bool EffectuerUneTransactionVersUnCompte(CompteBancaire destinataire, CompteBancaire expediteur, Transaction transaction)
        {
            if (!IsTransactionPossibleEntreComptes(destinataire, expediteur)) return false;
            if (!expediteur.EffectuerUnVirement(transaction.Montant, transaction.Horodatage)) return false;

            destinataire.DeposerDeLargent(transaction.Montant);
            expediteur._carteBancaire.MAJHistorique(transaction);
            return true;
        }

    }

    


}
