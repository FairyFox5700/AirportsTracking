using System;
using System.Collections.Generic;
using System.Text;

using System.Drawing;
using System.Linq;
using System.Diagnostics;

namespace Airports
{
    public class NextAirport
    {
        public Airport Current { get; set; }
        public string IATA { get; set; }
        public string Name  { get; set; }
        public double Weight { get; set; }
        public NextAirport previous { get; set; }
        public LinkedList<Airport> adjacementList = new LinkedList<Airport>();
        public NextAirport(string cur, string name, double price, LinkedList<Airport> nextPorts)
        {
            IATA = cur;
            Name = name;
            Weight = price;
            adjacementList = nextPorts;
        }
        public NextAirport(Airport cur, string name, double price, LinkedList<Airport> nextPorts)
        {
            Current = cur;
            IATA = cur.IATA;
            Name = name;
            Weight = price;
            adjacementList = nextPorts;
        }
    }
    public class Graph
    {
        private const int PRICE_FOR_1_KILOMETR = 32;
        static Dictionary<NextAirport, NextAirport> Path { get; set; }

        static NextAirport Source { get; set; }

        static NextAirport Destination { get; set; }

        ///protected internal static Airport Airport { get; set; }
        // protected internal static LinkedList<Airport> adjacementList = new LinkedList<Airport>();


        ///https://stackoverflow.com/questions/6366408/calculating-distance-between-two-latitude-and-longitude-geocoordinates/44703178#44703178
        protected internal static double GetPath(Airport source, Airport destination)
        {
            var first = Math.PI * source.Latitude / 180;
            var firts2 = Math.PI * destination.Latitude / 180;
            var second = Math.PI * source.Longitude / 180;
            var second2 = Math.PI * destination.Latitude / 180;
            var theta = source.Longitude - destination.Longitude;
            var rTheta = Math.PI * theta / 180;
            var distance = Math.Sin(first) * Math.Sin(firts2) + Math.Cos(first) * Math.Cos(firts2) * Math.Cos(rTheta);
            distance = Math.Acos(distance);
            distance = distance * 180 / Math.PI;
            distance = distance * 60 * 1.1515 * 1.609344;//in kilometers
            return distance;
        }
        protected internal static double GetPriceByPath(string sourceCode, string destinationCode)
        {
            var source = AirlineData.GetAirPort(sourceCode);
            var destination = AirlineData.GetAirPort(destinationCode);
            var distance = GetPath(source, destination);
            return distance * PRICE_FOR_1_KILOMETR;

        }

        protected static internal void DijkstraMinPath(string sourceCode, string destinationCode)
        {
            try
            {
                AirlineData.LoadData();
                var watch = Stopwatch.StartNew();
                var priotityQueue  = new Queue<NextAirport>();
                List<string> visited = new List<string>();
                var path = new Dictionary<NextAirport, NextAirport>();
                Airport source = AirlineData.GetAirPort(sourceCode);
                var next = AirlineData.GetNextStation(sourceCode);
                next.Weight = 0;//sorce
                priotityQueue.Enqueue(next);
                Graph.Source = next;
                double BestPrice = 0;
   
                path[next] = null;
                while (priotityQueue.Count > 0)
                {

                    var _flight = priotityQueue.Dequeue();
                    if (_flight.Current.IATA == destinationCode)
                    {
                        watch.Stop();
                        Graph.Destination = _flight;
                        Graph.Path = path;
                        Console.WriteLine("Time spended {0}", watch.ElapsedMilliseconds);
                        Console.WriteLine("Count of visited vertices = {0}", visited.Count);
                        ReturnMinPath();
                        
                        return;
                    }
                    visited.Add(_flight.IATA);

                    foreach (var neighbour in _flight.adjacementList)
                    {

                        if (neighbour != null)
                        {
                            NextAirport _next = AirlineData.GetNextStation(neighbour.IATA);
                            if (!visited.Contains(neighbour.IATA) && _next != null)
                            {
                                var best = GetPriceByPath(_flight.IATA, neighbour.IATA) + _flight.Weight;//maybe best price
                                if (best < _next.Weight)
                                {   
                                    _next.Weight = best;
                                    priotityQueue.Enqueue(_next);
                                    path[_next] = _flight;
                                    SortQueue(ref priotityQueue);
                                }
                      
                                
                            }
                        }
                    }
                }

                Console.ReadKey();
            }
            catch (Exception r)

            {
                Console.WriteLine(r);
            }

        
        }
        protected static internal void ReturnMinPath()
        {
           
            List<NextAirport> result = new List<NextAirport>();
            var current = Destination;
            result.Add(current);
            while (current!= Source)
            {
                current = Path[current];
                result.Add(current);              
            }
            Console.WriteLine("From airport with code {0} and name {1}  to airport  {2} and name {3}\n you can get with the best price {4}", Source.IATA, Source.Current.AirportName, Destination.IATA, Destination.Name, Destination.Weight);
            Console.WriteLine(" Count of vetrices between two vertices = {0}", result.Count);
            Console.WriteLine("Path with minimal cost: ");
            for (int i = result.Count-1; i >= 0; i--)
                Console.Write(result[i].IATA +" ->>>");
           
           
           

        }
     
        protected static internal void SearchPointsWithBfs(string sourceCode, string searchCode)
        {
            try
            {
                var watch = Stopwatch.StartNew();
                AirlineData.LoadData();
                Airport source = AirlineData.GetAirPort(sourceCode);
                var next = AirlineData.GetNextStation(sourceCode);
                var que = new Queue<NextAirport>();
                var visited = new List<Airport>();
                que.Enqueue(next);
                visited.Add(source);
                var counter = 0;
                while(que.Count>0)
                {
                    var vertex = que.Dequeue();
                    if (vertex.IATA == searchCode)
                    {                
                        watch.Stop();
                        Console.WriteLine("Airport with code {0} and name {1} finded from start airport {2}", vertex.IATA, vertex.Current.AirportName, sourceCode);
                        Console.WriteLine("Count of vetrices between two vertices = {0}", counter);
                        Console.WriteLine("Count of visited vertices = {0}", visited.Count);
                        Console.WriteLine("Time spended {0}", watch.ElapsedMilliseconds);
                        return;
                    }
                    foreach(var neighbour in vertex.adjacementList)
                    {
                        if (neighbour != null)
                        {
                            if (!(visited.Contains(neighbour)))
                            {
                                que.Enqueue(AirlineData.GetNextStation(neighbour.IATA));
                                visited.Add(neighbour);
                                counter++;
                            }
                        }
                    }
        
                }      
            }
            catch (Exception r)
            {
                Console.WriteLine(r);
            }
           
        }
        
        protected static internal void SortQueue( ref Queue<NextAirport> queue)
        {

            queue.OrderBy(d => d.Weight);
           
        }
    }
}


