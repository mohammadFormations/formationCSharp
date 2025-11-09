
using Or.Business;
using Or.Models;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows;

/// <summary>
/// Logique d'interaction pour Benef.xaml
/// </summary>
/// 

namespace Or.Pages
{

    public partial class Benef : PageFunction<long>
    {
        public long NumCarte;

        /// <summary>
        /// Constructeur partiel de la page Benef.
        /// requeter les bénéficiaires associé a une carte
        /// alimenter list view avec les informations nécéssaires
        /// </summary>
        /// <param name="numCarte"></param>
        public Benef(long numCarte)
        {
            InitializeComponent();
            Carte c = SqlRequests.InfosCarte(numCarte);
            List<Beneficiaire> beneficiaires = SqlRequests.ListeBeneficiairesAssocieClient(numCarte);
            Numero.Text = c.Id.ToString();
            NumCarte = numCarte;
            Prenom.Text = c.PrenomClient;
            Nom.Text = c.NomClient;

            listView.ItemsSource = beneficiaires;
        }

        /// <summary>
        /// effet secondaire du retour vers la page Benef
        /// realimenter ListView avec une liste des bénéficiaires mise-à-jour.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void PageFunction_Return(object sender, ReturnEventArgs<long> e)
        {
            List<Beneficiaire> beneficiaires = SqlRequests.ListeBeneficiairesAssocieClient(NumCarte);
            listView.ItemsSource = beneficiaires;
        }

        void PageFunctionNavigate(PageFunction<long> page)
        {
            page.Return += new ReturnEventHandler<long>(PageFunction_Return);
            NavigationService.Navigate(page);
        }

        /// <summary>
        /// naviger ves la page AjBenef sur l'appui du bouton "Ajouter un bénéficiaire"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GoAjBenef(object sender, RoutedEventArgs e)
        {
            PageFunctionNavigate(new AjBenef(long.Parse(Numero.Text)));
        }


        private void Retour_Click(object sender, RoutedEventArgs e)
        {
            OnReturn(null);
        }

        private void ListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            GridView gridView = listView.View as GridView;
            if (gridView != null)
            {
                double totalWidth = listView.ActualWidth - SystemParameters.VerticalScrollBarWidth;
                gridView.Columns[0].Width = totalWidth * 0.20; // 20%
                gridView.Columns[1].Width = totalWidth * 0.25; // 25%
                gridView.Columns[2].Width = totalWidth * 0.25; // 25%
                gridView.Columns[3].Width = totalWidth * 0.30; // 30%
            }
        }

        /// <summary>
        /// suppression d'un bénéficiaire , Mise-à-jour de la liste des bénéficiaires.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteBenef(object sender, RoutedEventArgs e)
        {
            SqlRequests.SuppresionBeneficiaire((int)(sender as Button).CommandParameter, long.Parse(Numero.Text));

            List<Beneficiaire> beneficiaires = SqlRequests.ListeBeneficiairesAssocieClient(NumCarte);
            
            listView.ItemsSource = beneficiaires;
        }

    }


}