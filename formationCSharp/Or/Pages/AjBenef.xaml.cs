using Or.Business;
using Or.Models;
using System.Windows;
using System.Windows.Navigation;

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
                if (SqlRequests.EstBeneficiairePotentielByIdtCpt(numCompte, NumCarte))
                {
                    SqlRequests.AjoutBeneficiaire(numCompte, NumCarte);
                }
                else
                {
                    MessageBox.Show("Compte Inaccessible comme benificière");
                }

            }
            else
            {
                MessageBox.Show("Numero De Compte Invalide");
            }
        }

    }
}
