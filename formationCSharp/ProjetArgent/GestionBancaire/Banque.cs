using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetArgent.IO.FileStreams;
using ProjetArgent.IO.ObjectBuilder;
using ProjetArgent.IO.serializeurs;
using ProjetArgent.GestionBancaire.Enums;

namespace ProjetArgent.GestionBancaire
{
    public class Banque
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

        public void ProcessBatch()
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

        public static void ProcessTransaction(Transaction transaction)
        {
            if (transaction.ExpediteurId == 0)
            {
                CompteBancaire destinateur = CompteBancaires.FirstOrDefault(compte => compte.Id == transaction.DestinateurId);
                bool status = destinateur.DeposerDeLargent(transaction.Montant);
                compteRenduFileWriter.writeCompteRendu($"{transaction.NumTransaction};{(status ? StatusTransactionEnum.StatusTransaction.OK : StatusTransactionEnum.StatusTransaction.KO)}");

            } else if (transaction.DestinateurId == 0)
            {
                CompteBancaire expediteur = CompteBancaires.FirstOrDefault(compte => compte.Id == transaction.ExpediteurId);
                bool status = expediteur.RetirerDeLargent(transaction.Montant, transaction.Horodatage);
                compteRenduFileWriter.writeCompteRendu($"{transaction.NumTransaction};{(status ? StatusTransactionEnum.StatusTransaction.OK : StatusTransactionEnum.StatusTransaction.KO)}");
                Console.WriteLine(status);
            } else
            {
                CompteBancaire destinateur = CompteBancaires.FirstOrDefault(compte => compte.Id == transaction.DestinateurId);
                CompteBancaire expediteur = CompteBancaires.FirstOrDefault(compte => compte.Id == transaction.ExpediteurId);
                bool status = expediteur.EffectuerUneTransactionVersUnCompte(destinateur, transaction);
                compteRenduFileWriter.writeCompteRendu($"{transaction.NumTransaction};{(status ? StatusTransactionEnum.StatusTransaction.OK : StatusTransactionEnum.StatusTransaction.KO)}");
                Console.WriteLine(status);

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
    }

    


}
