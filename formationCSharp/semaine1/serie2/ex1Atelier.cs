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
            Console.Write("tab : ");
            PrintTab(tab);
            Console.WriteLine("Somme : " + SumTab(tab));
        }


        public static int SumTab(int[] tab)
        {
            int sum = 0;
            foreach (int i in tab) { sum+= i; }
            return sum;
        }

        public static void OperationPrinter(int[] tab, char operation, int b)
        {
            Console.WriteLine("Opération sur un tableau :");
            Console.Write("tab : ");
            PrintTab(tab);
            Console.WriteLine("ope : " + operation + ' ' + b);
            int[] tab2 = OpeTab(tab, operation, b);
            Console.Write("res : ");
            PrintTab(tab2);
        }
        public static int[] OpeTab(int[] tab, char operation, int b)
        {
            switch (operation)
            {
                case '+':
                    AddValue(tab, b);
                    return tab;
                case '-':
                    SubValue(tab, b);
                    return tab;
                case '*':
                    MulValue(tab, b);
                    return tab;
                case char op when op == '/' && b != 0:
                    DivValue(tab, b);
                    return tab;
                default:
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


        public static void PrintTab(int[] tab, String header = "")
        {
            Console.Write(header);
            Console.Write('[');
            foreach (int i in tab)
            {
                Console.Write(i);
                Console.Write(' ');
            }
            Console.WriteLine(']');
        }


        public static void AddValue(int[] tab, int b)
        {
            for (int i = 0; i < tab.Length; i++)
            {
                tab[i] += b;
            }
        }

        public static void SubValue(int[] tab, int b)
        {
            for (int i = 0; i < tab.Length; i++)
            {
                tab[i] -= b;
            }
        }

        public static void MulValue(int[] tab, int b)
        {
            for (int i = 0; i < tab.Length; i++)
            {
                tab[i] *= b;
            }
        }

        public static void DivValue(int[] tab, int b)
        {
            for (int i = 0; i < tab.Length; i++)
            {
                tab[i] /= b;
            }
        }

    }
}