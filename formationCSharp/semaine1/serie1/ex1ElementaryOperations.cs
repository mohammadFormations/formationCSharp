using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serie1
{
    public static class ElementaryOperations
    {
        public static void BasicOperation(int a, int b, char operation)
        {
            int value = 0;
            bool failure = false;
            switch (operation)
            {
                case '+':
                    value = a + b;
                    break;
                case '-':
                    value = a - b;
                    break;
                case '*':
                    value = a * b;
                    break;
                case char op when op == '/'  && b != 0:
                    value = a / b;
                    break;
                default:
                    failure = true;
                    break;
            }
            if (failure)
            {
                Console.WriteLine($"{a}  {operation} {b} = Operation Invalide");

            } else
            {
                Console.WriteLine($"{a}  {operation} {b} = {value}");
            }
        }

        public static void IntegerDivision(int a, int b)
        {
            if (b == 0)
            {
                Console.WriteLine($"{a} : {b} = Opération invalide.");
                return;

            }
            Console.WriteLine($"{a} = {b} * {a / b} + {a % b}");
        }

        public static void Pow(int a, int b)
        {
            if (b < 0)
            {
                Console.WriteLine($"{a} ^ {b} = Opération invalide.");
                return;

            }
            Console.WriteLine($"{a} ^ {b} = {Math.Pow(a, b)} ");
        }
    }
}