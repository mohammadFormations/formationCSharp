using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Percolation
{
    class Program
    {
        static void Main(string[] args)
        {


            // Keep the console window open
            PercolationSimulation simulator = new PercolationSimulation();
            PclData variable = simulator.MeanPercolationValue(1000, 1000);
            Console.WriteLine($"{variable.Mean}, {variable.StandardDeviation}");
            Console.WriteLine("----------------------");
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
