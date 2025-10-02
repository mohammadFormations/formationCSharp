using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serie1
{
    public static class Pyramid

    /*
     1 a 2*j -1
     1 b n(n + 1) / 2
     2 a position sommet x,y = n, n
     2b gauche(j) = n-j
        droite(j) = j*2 + 1
        */
    {
        public static void PyramidConstruction(int n, bool isSmooth)
        {
            for(int k=0; k < n; k++)
            {
                DisplayLine(k, n, isSmooth || k % 2 == 0);
            }
        }

        public static void DisplayLine(int j, int n, bool plus)
        {
            char block = '+';
            if (!plus) block = '-';
            for (int i = 0; i < n - j; i++) Console.Write(' ');
            for (int i = 0; i < j * 2 + 1; i++) Console.Write(block);
            Console.WriteLine();
        }
    }
}