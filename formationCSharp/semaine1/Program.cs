using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using Serie1;

namespace semaine1
{
    internal class Program
    {



        static void Main(string[] args)
        {
            /*           ElementaryOperations.BasicOperation(3, 4, '+');
                       ElementaryOperations.BasicOperation(6, 2, '/');
                       ElementaryOperations.BasicOperation(3, 0, '/');
                       ElementaryOperations.BasicOperation(6, 9, 'L');

                       // integer devision 
                       Console.WriteLine("integer devision ");

                       ElementaryOperations.IntegerDivision(12, -4);
                       ElementaryOperations.IntegerDivision(13, -4);
                       ElementaryOperations.IntegerDivision(12, 0);

                       // power 

                       ElementaryOperations.Pow(2, -4);
                       ElementaryOperations.Pow(3, 2);
                       ElementaryOperations.Pow(-2, 3);*/


            // journe 
            /*  SpeakingClock.Journee(-1);
              SpeakingClock.Journee(0);
              SpeakingClock.Journee(1);
              SpeakingClock.Journee(5);
              SpeakingClock.Journee(6);
              SpeakingClock.Journee(11);
              SpeakingClock.Journee(12);
              SpeakingClock.Journee(13);
              SpeakingClock.Journee(17);
              SpeakingClock.Journee(18);
              SpeakingClock.Journee(20);
              SpeakingClock.Journee(23);
              SpeakingClock.Journee(29);*/
            /*
                        Pyramid.PyramidConstruction(5, true);
                        Pyramid.PyramidConstruction(5, false);*/

            Console.WriteLine(Factorial.Factorial_(0));
            Console.WriteLine(Factorial.Factorial_(1));
            Console.WriteLine(Factorial.Factorial_(5));
            Console.WriteLine(Factorial.FactorialRecursive(0));
            Console.WriteLine(Factorial.FactorialRecursive(1));
            Console.WriteLine(Factorial.FactorialRecursive(5));

        }

    }
}
