using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serie1
{
    public static class Factorial
    {
        public static int Factorial_(int n)
        {
            int result = 1;
            for(int i =1; i < n + 1; i++)
            {
                result *= i;
            }
            return result;
         }

        public static int FactorialRecursive(int n)
        {
            if (n == 0 || n == 1) return 1;
            return n * FactorialRecursive(n- 1);
        }
    }
}