using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Serie4
{
    public static class ClassCouncil
    {
        public static void SchoolMeans(string input, string output)
        {
            // dictionaire <matiere, total des notes>
            Dictionary<string, decimal> totals = new Dictionary<string, Decimal>();

            // dictionaire <matiere, nombre des eleves>
            Dictionary<string, int> counts = new Dictionary<string, int>();

            using (FileStream fs = File.OpenRead(input))
            using (StreamReader reader = new StreamReader(fs))
            {
                string line = reader.ReadLine();
                while (line != null)
                {
                    ProcessLineReading(line, totals, counts);
                    line = reader.ReadLine();
                }
            }

            // calculer les moynnes 
            Dictionary<string, decimal> moyennes = CalculerLesMoyennes(totals, counts);


            using (FileStream fs = File.Create(output))
            using (StreamWriter writer = new StreamWriter(fs))
            {
                Dictionary<string, decimal>.KeyCollection matieres = moyennes.Keys;
                foreach (string matiere in matieres)
                {
                    moyennes.TryGetValue(matiere, out decimal moyenne);
                    string formatedLine = FormatMoyennesLine(matiere, moyenne);
                    writer.WriteLine(formatedLine);
                }
            }

        }


        public static string FormatMoyennesLine(string matiere, decimal moyenne)
        {
            return $"{matiere},{moyenne.ToString("00.0", CultureInfo.CreateSpecificCulture("en-US"))}";
        }

        public static void ProcessLineReading(string line, Dictionary<string, decimal> totals, Dictionary<string, int> counts)
        {
            // elements: nom, matiere, note
            String[] elements = line.Split(',');
            bool state = decimal.TryParse(elements[2],NumberStyles.Number,CultureInfo.InvariantCulture, out decimal note);
            // ignore the grade if the state is not good
            if (!state) return;
            string matiere = elements[1];

            UpdateCounts(matiere, counts);
            UpdateTotals(matiere, note, totals);


        }

        public static void UpdateTotals(string matiere, decimal note, Dictionary<string, decimal> totals)
        {
            if (totals.TryGetValue(matiere, out decimal total))
            {
                totals[matiere] = total + note;
            }
            else
            {
                totals[matiere] = note;
            }

        }

        public static void UpdateCounts(string matiere, Dictionary<string, int> counts)
        {
            if (counts.TryGetValue(matiere, out int count))
            {
                counts[matiere] = count + 1;
            }
            else
            {
                counts[matiere] = 1;
            }

        }

        public static Dictionary<string, Decimal> CalculerLesMoyennes(Dictionary<string, Decimal> totals, Dictionary<string, int> counts)
        {

            Dictionary<string, Decimal> moyennes = new Dictionary<string, Decimal>();
            Dictionary<string, decimal>.KeyCollection matieres = totals.Keys;
            foreach (string matiere in matieres)
            {
                totals.TryGetValue(matiere, out decimal total);
                counts.TryGetValue(matiere, out int count);
                moyennes[matiere] = total / count;
            }

            return moyennes;
        }


    }

}
