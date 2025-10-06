using Bataille_Navale;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bataille_navale
{
    internal class Program
    {
        static void Main(string[] args)
        {

            /*
             reponses aux questions:
             2 a bateau coulé <=> toutes les positions dans la liste position sont touchées 
             7 a une partie est terminee quand tout les position de tout les bateau sont touché
             dans ma propre implementation je coule les positions des que
            la derniere position d un bateau est touche

            */

            Plateau plat = new Plateau();
            plat.LancementPartie();
            Console.ReadLine();
        }
    }
}
