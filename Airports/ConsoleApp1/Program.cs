using System;
using Airports;
namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Graph.GetNextPorts();
             Graph.DijkstraMinPath("DME", "LAS");
            // Graph.PrintPorts("DME");
            //Graph.SearchPointsWithBfs("DME", "ASF");
            //AirlineData.WriteCsw();
         var data =   AirlineData.ReturnListOfAirportsBy2City("Las Vegas", "Bangkok");

        }
    }
}
