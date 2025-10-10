using Or.Business;
using Or.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Or.Pages
{
    /// <summary>
    /// Logique d'interaction pour AjBenef.xaml
    /// </summary>
    public partial class AjBenef : PageFunction<long>
    {
        public long NumCarte;

        public AjBenef(long numCarte)
        {
            InitializeComponent();
            NumCompte.Text = 0.ToString();
            NumCarte = numCarte;
        }


        private void Retour_Click(object sender, RoutedEventArgs e)
        {
            OnReturn(null);
        }


        private void AjouterUnBenif(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(NumCompte.Text.Trim(' '), out int numCompte) && numCompte > 0)
            {
                Console.WriteLine("test");
                if (SqlRequests.EstBeneficiairePotentielByIdtCpt(numCompte))
                SqlRequests.AjoutBeneficiaire(numCompte, NumCarte);

            }
            else
            {
                MessageBox.Show("Numero De Compte Invalide");
            }
        }

    }
}
