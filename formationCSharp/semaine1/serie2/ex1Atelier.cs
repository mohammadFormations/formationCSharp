using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serie2
{
    public static class TasksTables
    {
        public static void SommePrinter(int[] tab)
        {
            Console.WriteLine("Somme des éléments d'un tableau :");
            PrintTab(tab, "tab : ");
            Console.WriteLine("Somme : " + SumTab(tab));
        }


        public static int SumTab(int[] tab)
        {
            int sum = 0;
            foreach (int i in tab) { sum+= i; }
            return sum;
        }

        public static int[] OpeTab(int[] tab, char operation, int b)
        {
            // affichage en tete et tableaux avant (side effect)
            Console.WriteLine("Opération sur un tableau :");
            PrintTab(tab, "tab : ");
            Console.WriteLine("ope : " + operation + ' ' + b);

            switch (operation)
            {
                case '+':
                    AddValue(tab, b);
                    PrintTab(tab, "res : ");
                    return tab;
                case '-':
                    SubValue(tab, b);
                    PrintTab(tab, "res : ");
                    return tab;
                case '*':
                    MulValue(tab, b);
                    PrintTab(tab, "res : ");
                    return tab;
                case char op when op == '/' && b != 0:
                    DivValue(tab, b);
                    PrintTab(tab, "res : ");
                    return tab;
                default:
                    PrintTab(new int[0], "res : ");
                    return new int[0];
            }
        }

        public static int[] ConcatTab(int[] tab1, int[] tab2)
        {
            int[] concatenated_array = new int[tab1.Length + tab2.Length];
            for (int i = 0; i < tab1.Length; i++)
            {
                concatenated_array[i] = tab1[i];

            }
            for (int i = 0; i < tab2.Length; i++)
            {
                concatenated_array[i + tab1.Length] = tab2[i];

            }
            return concatenated_array;
        }

        /// <summary>
        /// afficher un tableau avec un titre optionel
        /// </summary>
        /// <param name="tab"></param>
        /// <param name="header"></param>
        public static void PrintTab(int[] tab, String header = "")
        {
            Console.Write(header);
            Console.Write('[');
            for(int i= 0; i < tab.Length - 1; i++)
            {
                Console.Write(tab[i]);
                Console.Write(", ");
            }


            if (tab.Length > 0) Console.Write(tab[tab.Length - 1]);
            Console.WriteLine(']');
        }


        /// <summary>
        /// addition tout les elements d'un tableau tab par une valeur b
        /// </summary>
        /// <param name="tab"></param>
        /// <param name="b"></param>
        public static void AddValue(int[] tab, int b)
        {
            for (int i = 0; i < tab.Length; i++)
            {
                tab[i] += b;
            }
        }

        /// <summary>
        /// soustrait tout les elements d'un tableau tab par une valeur b
        /// </summary>
        /// <param name="tab"></param>
        /// <param name="b"></param>
        public static void SubValue(int[] tab, int b)
        {
            for (int i = 0; i < tab.Length; i++)
            {
                tab[i] -= b;
            }
        }
        /// <summary>
        /// multiplie tout les elements d'un tableau tab par une valeur b
        /// </summary>
        /// <param name="tab"></param>
        /// <param name="b"></param>
        public static void MulValue(int[] tab, int b)
        {
            for (int i = 0; i < tab.Length; i++)
            {
                tab[i] *= b;
            }
        }

        /// <summary>
        /// divise tout les elements d'un tableau tab par une valeur b
        /// </summary>
        /// <param name="tab"></param>
        /// <param name="b"></param>
        public static void DivValue(int[] tab, int b)
        {
            for (int i = 0; i < tab.Length; i++)
            {
                tab[i] /= b;
            }
        }
    }
}