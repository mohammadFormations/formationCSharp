using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serie3
{
    public static class AdministrativeTasks
    {
        public static string EliminateSeditiousThoughts(string text, string[] prohibitedTerms)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(text);

            for(int i = 0; i < prohibitedTerms.Length; i++)
            {
                strBuilder.Replace(prohibitedTerms[i], new string('x', prohibitedTerms[i].Length));
            } 
            return strBuilder.ToString();
        }

        public static bool ControlFormat(string line)
        {
            string civilite = line.Substring(0, 4).Trim() ;
            string nom = line.Substring(4, 12).Trim();
            string prenom = line.Substring(16, 12).Trim();
            string age = line.Substring(28, 2);

            return IsCiviliteValid(civilite) && IsStringValidNumber(age)
                && IsStringAlphabetic(nom) && IsStringAlphabetic(prenom);
        }

        public static string ChangeDate(string report)
        {
            int tailleDateSocialiste = 10;
            StringBuilder socializedReportBuilder = new StringBuilder();

            /*
             iterer sur toutes les sous chaines de 10 characteres si elles sont des dates non socialistes on les socialize
             */
            for (int i = 0; i < report.Length - tailleDateSocialiste;)
            {
                string datePossible = report.Substring(i, 10);
                if (isDateSocialisteValide(datePossible))
                {
                    socializedReportBuilder.Append(SocialiserUneDate(datePossible));
                    i += 10;

                } else
                {

                    socializedReportBuilder.Append(report.Substring(i, 1));
                    i ++;
                }
            }


            
            return socializedReportBuilder.ToString();
        }

        public static bool isDateSocialisteValide(string date)
        {
            if (date.Length != 10) return false;

            string[] elementsDuDate = date.Split('-');
            if (elementsDuDate.Length != 3) return false;

            // valider l anné
            if (!IsStringValidNumber(elementsDuDate[0])) return false;
            if (!IsTailleChaineValide(elementsDuDate[0], 4)) return false;

            // valider l le mois
            if (!IsStringValidNumber(elementsDuDate[1])) return false;
            if (!IsTailleChaineValide(elementsDuDate[1], 2)) return false;

            // valider le jour
            if (!IsStringValidNumber(elementsDuDate[2])) return false;
            if (!IsTailleChaineValide(elementsDuDate[2], 2)) return false;

            return true;
        }


        public static string SocialiserUneDate(string date)
        {
            // date non socialiste aaaa-mm-jj
            // date socialiste jj.mm.aa
            StringBuilder dateBuilder = new StringBuilder();
            string dateSocialiste = dateBuilder
                .Append(date.Substring(8, 2))
                .Append(".")
                .Append(date.Substring(5, 2))
                .Append(".")
                .Append(date.Substring(2, 2))
                .ToString();
            return dateSocialiste;
        }

        public static bool IsTailleChaineValide( string chaine, int taille)
        {
            return chaine.Length == taille;
        }


        public static bool IsStringValidNumber(string age)
        {
            foreach (char digit in age.ToCharArray())
            {
                if (digit < '0' || digit > '9')
                {
                    return false;
                }
            }
            return true;
            
        }

        public static bool IsCiviliteValid(string civilite)
        {
            string[] civiliteAcceptables = new string[3] { "M.", "Mme", "Mlle" };
            foreach (string civiliteAcceptable in civiliteAcceptables)
            {
                if (String.Compare(civilite, civiliteAcceptable) == 0) return true;
            }
            return false;
        }
        public static bool IsPrenomValid(string prenom)
        {
            foreach (char letter in prenom.ToCharArray())
            {
                if (!((letter >= 'A' && letter <= 'Z') || (letter >= 'a' && letter <= 'z')))
                {
                    return false;
                }
            }
            return true;
        }
        public static bool IsStringAlphabetic(string chaine)
        {
            foreach (char letter in chaine.ToCharArray())
            {
                if (!((letter >= 'A' && letter <= 'Z') || (letter >= 'a' && letter <= 'z')))
                {
                    return false;
                }
            }
            return true;
        }

        public static void AffichageComplet(string[] chaines)
        {
            AffichageTitre();
            int indice = 1;
            foreach(string chaine in chaines)
            {
                AffichageLigne(chaine, indice);
                AffichageStatus(ControlFormat(chaine));
            }
        }

        public static void AffichageTitre()
        {
            Console.WriteLine("Recensement des résidents :");
        }

        public static void AffichageLigne(string chaine, int indice)
        {
            Console.WriteLine("Ligne " + indice + "[" + chaine + "]");
        }

        public static void AffichageStatus(bool status)
        {
            Console.WriteLine(status ? "status OK" : "status KO");
        }

    }
}
