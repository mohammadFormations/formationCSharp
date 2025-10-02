using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serie1
{
    public static class SpeakingClock
    {
        public static string GoodDay(int heure)
        {
            if (heure < 6 && heure >= 0) return "Merveilleuse nuit!";
            else if (heure < 12 && heure >= 6) return "Bonne matinée !";
            else if (heure == 12) return "Bon Appétit";
            else if (heure <= 18 && heure >= 13) return "Profitez de votre après-midi !";
            else if (heure >= 18 && heure < 24) return "Passez une bonne soirée!";

            return "Bien Jouée!";
        }
        public static void Journee(int heure)
        {
            string message = GoodDay(heure);
            Console.WriteLine($"Il est heure {heure}, " + message);
        }
    }

    
}