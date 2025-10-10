
using Or.Business;
using Or.Models;
using Or.Pages;
using Or.Serializeurs;
using System.Collections.Generic;
using System.IO;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows;
using System.Xml.Serialization;
/// <summary>
/// Logique ht'interaction pour Benef.xaml
/// </summary>
/// 

namespace Or.Pages
{

    public partial class Benef : PageFunction<long>
    {
        public long NumCarte;

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
        private void GoAjBenef(object sender, RoutedEventArgs e)
        {
            PageFunctionNavigate(new AjBenef(long.Parse(Numero.Text)));
        }


        // TODO mhh change this function in order to support returning to mutliple pages based on the caller identity
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
                gridView.Columns[0].Width = totalWidth * 0.20; // 10%
                gridView.Columns[1].Width = totalWidth * 0.25; // 40%
                gridView.Columns[2].Width = totalWidth * 0.25; // 20%
                gridView.Columns[3].Width = totalWidth * 0.30; // 20%
            }
        }

        private void DeleteBenef(object sender, RoutedEventArgs e)
        {
            SqlRequests.SuppresionBeneficiaire((int)(sender as Button).CommandParameter, long.Parse(Numero.Text));

            List<Beneficiaire> beneficiaires = SqlRequests.ListeBeneficiairesAssocieClient(NumCarte);
            
            listView.ItemsSource = beneficiaires;
        }

    }


}