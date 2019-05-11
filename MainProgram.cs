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
            //Graph.AStarMinPath( "UAK", "IFJ");  // good example ("UAK", "IFJ");  // no such way ("UAK", "YCB")

            /*
            Graph.AStarMinPath("GKA", "MAG");
            Console.WriteLine("\nDEIKSTA BOTTOM");
            Graph.DijkstraMinPath("GKA", "MAG");;
            тест, який показує перевагу А* над Дейкстрою

            Graph.AStarMinPath("YCL", "YWK");
            Console.WriteLine("\nDEIKSTA BOTTOM");
            Graph.DijkstraMinPath("YCL", "YWK");
            ще один тест, який показує перевагу А* над Дейкстрою

            Graph.AStarMinPath("DME", "FRA");
            Console.WriteLine("\nDEIKSTA BOTTOM");
            Graph.DijkstraMinPath("DME", "FRA");
            ще один тест, який показує перевагу А* над Дейкстрою

            */
            Graph.AStarMinPath("DME", "FRA");
            Console.WriteLine("\nDEIKSTA BOTTOM");
            Graph.DijkstraMinPath("DME", "FRA");
            Console.ReadKey();
        }
    }

  
}
