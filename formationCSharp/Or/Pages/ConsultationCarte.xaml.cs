using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Xml.Serialization;
using Or.Business;
using Or.Models;
using Or.Serializeurs;
using System.Xml;
using System.Xml.Serialization;
using System;

namespace Or.Pages
{
    /// <summary>
    /// Logique ht'interaction pour ConsultationCarte.xaml
    /// </summary>
    public partial class ConsultationCarte : PageFunction<long>
    {
        public ConsultationCarte(long numCarte)
        {
            InitializeComponent();
            Carte c = SqlRequests.InfosCarte(numCarte);
            
            Numero.Text = c.Id.ToString();
            Prenom.Text = c.PrenomClient;
            Nom.Text = c.NomClient;

            listView.ItemsSource = SqlRequests.ListeComptesAssociesCarte(numCarte);
        }
        private void GoDetailsCompte(object sender, RoutedEventArgs e)
        {
            PageFunctionNavigate(new DetailsCompte(long.Parse(Numero.Text), (int)(sender as Button).CommandParameter));
        }

        private void GoHistoTransactions(object sender, RoutedEventArgs e)
        {
            PageFunctionNavigate(new HistoriqueTransactions(long.Parse(Numero.Text)));
        }

        private void GoVirement(object sender, RoutedEventArgs e)
        {
            PageFunctionNavigate(new Virement(long.Parse(Numero.Text)));
        }

        private void GoRetrait(object sender, RoutedEventArgs e)
        {
            PageFunctionNavigate(new Retrait(long.Parse(Numero.Text)));
        }

        private void GoDepot(object sender, RoutedEventArgs e)
        {
            PageFunctionNavigate(new Depot(long.Parse(Numero.Text)));
        }

        private void GoBenef(object sender, RoutedEventArgs e)
        {
            PageFunctionNavigate(new Benef(long.Parse(Numero.Text)));
        }

        void PageFunctionNavigate(PageFunction<long> page)
        {
            page.Return += new ReturnEventHandler<long>(PageFunction_Return);
            NavigationService.Navigate(page);
        }

        void PageFunction_Return(object sender, ReturnEventArgs<long> e)
        {
            listView.ItemsSource = SqlRequests.ListeComptesAssociesCarte(long.Parse(Numero.Text));
        }

        private void ListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            GridView gridView = listView.View as GridView;
            if (gridView != null)
            {
                double totalWidth = listView.ActualWidth - SystemParameters.VerticalScrollBarWidth;
                gridView.Columns[0].Width = totalWidth * 0.10; // 10%
                gridView.Columns[1].Width = totalWidth * 0.30; // 40%
                gridView.Columns[2].Width = totalWidth * 0.30; // 20%
                gridView.Columns[3].Width = totalWidth * 0.30; // 20%
            }
        }

        public void SerialiserComptesTransaction(long numCarte)
        {

            ExportCompte exportCompte = new ExportCompte();
            List<Compte> comptes = SqlRequests.ListeComptesAssociesCarte(numCarte);
            exportCompte.Comptes = comptes;

            XmlSerializer serializer = new XmlSerializer(typeof(ExportCompte));

            using (TextWriter stream = new StreamWriter("C:\\Users\\FORMATION\\Desktop\\comptes.xml"))
            {
                foreach (Compte compte in comptes)
                {
                    ExportCompteTransactions transacs = new ExportCompteTransactions();
                    List<Transaction> transactions = SqlRequests.ListeTransactionsAssociesCompte(compte.Id);
                    transacs.Transactions = transactions.Count > 10 ? transactions.GetRange(0, 10) : transactions;
                    compte.Transactions = transacs;
                }

                serializer.Serialize(stream, exportCompte);
            }
        }

        void ExportComptes(object sender, RoutedEventArgs e)
        {
            SerialiserComptesTransaction(long.Parse(Numero.Text));
        }

        public ExportCompte DeSerialiserTransactions(string nomFichier)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ExportCompte));
            // gérer les erreurs avant la fermeture.
            FileStream fs = new FileStream(nomFichier, FileMode.Open);
            XmlReader reader = XmlReader.Create(fs);
            ExportCompte compte;
            compte = (ExportCompte)serializer.Deserialize(reader);
            fs.Close();
            return compte;
            
        }

        private static int CompareTransactions(Transaction t1, Transaction t2)
        {
            return t1.Horodatage.CompareTo(t2.Horodatage);
        }

        void ImportComptes(object sender, RoutedEventArgs e)
        {
            ExportCompte extractCompte = DeSerialiserTransactions("C:\\Users\\FORMATION\\Desktop\\comptes.xml");
            List<Transaction> transactions = ExtractTransactionsDuComptes(extractCompte);
            transactions.Sort(CompareTransactions);
            TraitementTransactionsImportees(transactions);
        }

        private List<Transaction> ExtractTransactionsDuComptes(ExportCompte exportCompte)
        {
            List<Transaction> transactions = new List<Transaction>();
            foreach (Compte compte in exportCompte.Comptes)
            {
                foreach (Transaction transaction in compte.Transactions.Transactions)
                {
                    transactions.Add(transaction);
                }
            }
            return transactions;
        }

        private void TraitementTransactionsImportees(List<Transaction> transactions)
        {
            Carte carte = SqlRequests.InfosCarte(long.Parse(Numero.Text));
            foreach (Transaction t in transactions)
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

                if (t.Expediteur != 0 && t.Destinataire != 0)
                {
                    type = Operation.InterCompte;
                }
                else if (t.Destinataire == 0)
                {
                    type = Operation.RetraitSimple;
                }
                else if (t.Expediteur == 0)
                {
                    type = Operation.DepotSimple;
                }
                else
                {
                    continue;
                }

                if (type == Operation.InterCompte)
                {
                    CodeResultatTransaction resCarte = carte.EstRetraitAutoriseNiveauCarte(t, ex, de);
                    bool retraitValide = ex.EstRetraitValide(t);
                    if (retraitValide && resCarte == CodeResultatTransaction.Success)
                    {
                        SqlRequests.EffectuerModificationOperationInterCompte(t, ex.IdentifiantCarte, de.IdentifiantCarte);
                    }
                }
                else if (type == Operation.DepotSimple)
                {

                    if (de.EstDepotValide(t))
                    {
                        SqlRequests.EffectuerModificationOperationSimple(t, de.IdentifiantCarte);

                    }
                }
                else
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

    }
}
