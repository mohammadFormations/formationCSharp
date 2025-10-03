using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Percolation
{
    public struct PclData
    {
        /// <summary>
        /// Moyenne 
        /// </summary>
        public double Mean { get; set; }
        /// <summary>
        /// Ecart-type
        /// </summary>
        public double StandardDeviation { get; set; }
        /// <summary>
        /// Fraction
        /// </summary>
        public double Fraction { get; set; }
    }

    public class PercolationSimulation
    {
        public PclData MeanPercolationValue(int size, int t)
        {
            double sumPi2 = 0;
            double sumMoy = 0;
            PclData result = new PclData();
            for ( int i = 0; i < t; i++)
            {
                double percolation = PercolationValue(size);
                sumMoy += percolation;
                sumPi2 += percolation * percolation;

            }
            result.Mean = sumMoy/t;
            result.StandardDeviation = Math.Sqrt((sumPi2 / t) - (result.Mean * result.Mean));
            return result;
        }

        public double PercolationValue(int size)
        {
            Percolation percolation = new Percolation(size);
            Random rand = new Random();
            while (!percolation.Percolate())
            {
                int i = rand.Next(size);
                int j = rand.Next(size);
                percolation.Open(i, j);
            }

            int openCases = percolation.countOpenCases();

            return openCases / (double) (size * size);
        }
    }
}
