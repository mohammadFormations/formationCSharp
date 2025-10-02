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

        /// <summary>
        /// changer les format des dates dans un paragraphes pour etre en format socialiste
        /// date a format originale: AAAA-MM-JJ
        /// date formaté           : JJ.MM.AA
        /// </summary>
        /// <param name="report"></param>
        /// <returns></returns>
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

        /// <summary>
        /// valider si une date a un format socialiste valide
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
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

        /// <summary>
        /// reformater une date dans un format socialiste
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
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


        /// <summary>
        /// valider la taille d'une chaine de caractères chaine qui est egale a taille
        /// </summary>
        /// <param name="chaine"></param>
        /// <param name="taille"></param>
        /// <returns></returns>
        public static bool IsTailleChaineValide( string chaine, int taille)
        {
            return chaine.Length == taille;
        }

        /// <summary>
        /// valider si une chaine de caractères est un nombre entier
        /// </summary>
        /// <param name="age"></param>
        /// <returns></returns>
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


        /// <summary>
        /// valider la civilite socialiste
        /// </summary>
        /// <param name="civilite"></param>
        /// <returns></returns>
        public static bool IsCiviliteValid(string civilite)
        {
            string[] civiliteAcceptables = new string[3] { "M.", "Mme", "Mlle" };
            foreach (string civiliteAcceptable in civiliteAcceptables)
            {
                if (String.Compare(civilite, civiliteAcceptable) == 0) return true;
            }
            return false;
        }
        
        /// <summary>
        /// valider un nom est alphabetique
        /// </summary>
        /// <param name="prenom"></param>
        /// <returns></returns>
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
        
        /// <summary>
        /// valider si un string est alphabetic charactères entre A et Z ou a et z
        /// </summary>
        /// <param name="chaine"></param>
        /// <returns></returns>
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
