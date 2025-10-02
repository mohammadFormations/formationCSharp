using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serie1
{
    public static class Factorial
    {
        /// <summary>
        /// calculer le factoriel d'un nombre n d'une manière itérative
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int Factorial_(int n)
        {
            // multiplier tout les nombres de 1 a N iterativement
            int result = 1;
            for(int i =1; i < n + 1; i++)
            {
                result *= i;
            }
            return result;
         }

        /// <summary>
        /// calculer le factoriel d'un nombre d'une manière recursive
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int FactorialRecursive(int n)
        {
            // cas du base
            if (n == 0 || n == 1) return 1;

            // cas recursive
            return n * FactorialRecursive(n- 1);
        }
    }
}