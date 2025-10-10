using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Xml.Serialization;
using Or.Business;
using Or.Models;
using Or.Serializeurs;

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

    }
}
