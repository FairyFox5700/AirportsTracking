using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airports
{
    public class Program
    {
        static void Main(string[] args)
        {
            Graph.AStarMinPath("DME", "FRA");
            Console.WriteLine("\nDEIKSTA");
            Graph.DijkstraMinPath("DME", "FRA");
            Console.ReadKey();
        }
    }

  
}
