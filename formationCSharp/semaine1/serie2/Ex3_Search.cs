using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serie2
{
    public static class Search
    {

        /// <summary>
        /// recherche iterative dans tableau pour trouver valeur
        /// </summary>
        /// <param name="tableau"></param>
        /// <param name="valeur"></param>
        /// <returns>
        /// retourne indice de valeur dans tableau si trouvé
        /// sinon on retourne -1
        /// </returns>
        public static int LinearSearch(int[] tableau, int valeur)
        {
            for (int i = 0; i < tableau.Length; i++)
            {
                if (tableau[i] == valeur) return i;
            }
            return -1;
        }

        /// <summary>
        /// recherche en dichotomie dans tableau pour trouver valeur
        /// </summary>
        /// <param name="tableau"></param>
        /// <param name="valeur"></param>
        /// <returns>
        /// retourne indice de valeur dans tableau si trouvé
        /// sinon on retourne -1
        /// </returns>
        public static int BinarySearch(int[] tableau, int valeur)
        {
            int start = 0;
            int end = tableau.Length - 1;
            int mid = 0;
            while (start <= end)
            {
                mid = (start + end) / 2;
                if (tableau[mid] < valeur) {
                    start = mid + 1;
                } else
                {
                    end = mid - 1;
                }
            }
            if (tableau[mid] == valeur) return mid;
            return -1;
        }
    }
}