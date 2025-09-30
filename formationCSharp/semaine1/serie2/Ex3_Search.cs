using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serie2
{
    public static class Search
    {
        public static int LinearSearch(int[] tableau, int valeur)
        {
            for (int i = 0; i < tableau.Length; i++)
            {
                if (tableau[i] == valeur) return i;
            }
            //TODO
            return -1;
        }

        public static int BinarySearch(int[] tableau, int valeur)
        {
            int start = 0;
            int end = tableau.Length - 1;
            int mid = 0;
            while (start <= end)
            {
                mid = (start + end) / 2;
                // _ 8 4 5
                Console.WriteLine($"hello{end}{mid}{start} ");
                if (tableau[mid] < valeur) {
                    start = mid + 1;
                } else
                {
                    end = mid - 1;
                }

            }
            Console.WriteLine($"hello{end}{mid}{start} ");
            if (tableau[mid] == valeur) return mid;
            return -1;
        }
    }
}