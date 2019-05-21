using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Airports
{



    public class AirlineData
    {
        protected internal static List<Route> listOfRoutes = new List<Route>();
        protected internal static List<Route> listOfNeighbourRoutes = new List<Route>();
        protected internal static Airport airport = new Airport();
        protected internal static string[] Airports { get; set; }
        protected internal static string[] Routes { get; set; }
        protected internal static string[] Neighbours { get; set; }
        protected internal static Dictionary<string, NextAirport> DictOfNeighbours { get; set; }





        protected static internal Airport GetAirPort(string airportCodeName)
        {
            try
            {
                foreach (var line in Airports)
                {
                    string[] temp = line.Replace("\"", "").Split(",", System.StringSplitOptions.RemoveEmptyEntries);
                    var name = temp[4].Replace("\"", "");
                    if (temp[4] == airportCodeName)
                    {
                        var airport = CreateAirport(temp);
                        return airport;
                    }

                }
            }
            catch (Exception a)
            {
                Console.WriteLine(a.Message);
            }
            return null;
        }

        protected static internal List<Airport> ReturnListOfAirportsBy2City(string cityOne, string cityTwo)
        {
            var list = new List<Airport>();
            Airports = GetAllInfoFromFile("airports.txt");
            var allAirports = GetAirPort();
            foreach(var airport in allAirports)
            {
                if (airport.CityName == cityOne || airport.CityName == cityTwo)
                {
                    list.Add(airport);
                }
            }
            return list;
        }
    
        protected static internal Airport CreateAirport(string[] temp)
        {
            airport = new Airport(temp);
            return airport;
        }

        protected static internal List<Airport> GetAirPort()
        {
            try
            {
                List<Airport> listOfAirports = new List<Airport>();

                foreach (var line in Airports)
                {
                    string[] temp = line.Replace("\"", "").Split(",", System.StringSplitOptions.RemoveEmptyEntries);
                        var airport = CreateAirport(temp);
                    if(airport!= null)
                        listOfAirports.Add(airport);
                    
                }
                return listOfAirports;
            }
            catch (Exception a)
            {
                Console.WriteLine(a.Message);
            }
            return null;
        }
        //reee
        protected static internal List<Route> GetRoutes()
        {
            try
            {
                foreach (var line in Routes)
                {
                    string[] temp = line.Split(";", System.StringSplitOptions.RemoveEmptyEntries);

                    Route route = new Route
                    {
                        SourceId = temp[1],
                        SourceName = temp[2],
                        DestinationId = temp[3],
                        DestinationName = temp[4],
                        NumbefOfStops = temp[5]
                    };
                    listOfRoutes.Add(route);
                }
                return listOfRoutes;
            }
            catch (Exception a)
            {
                Console.WriteLine(a.Message);
            }
            return null;
        }
        protected static internal void LoadData()
        {
            try
            {
                DictOfNeighbours = new Dictionary<string, NextAirport>();
                Airports = GetAllInfoFromFile("airports.txt");
                Routes = GetAllInfoFromFile("routes.txt");
                Neighbours = GetAllInfoFromFile("nextStations.txt");
                var allAirports = GetAirPort();
                foreach (var airport in allAirports)
                {
                    if (airport != null)
                    {
                        DictOfNeighbours[airport.IATA] = GetNextStation(airport.IATA);
                    }
                }
            }
            catch (Exception a)
            {
                Console.WriteLine(a.Message);
            }
           // return null;
        }
        //reee
        protected static internal List<Route> GetRoutes(string codeName)
        {
            try
            {
                foreach (var port in listOfRoutes)
                {
                    if (port.SourceName == codeName)
                        listOfNeighbourRoutes.Add(port);
                }
                return listOfNeighbourRoutes;
            }
            catch (Exception a)
            {
                Console.WriteLine(a.Message);
            }
            return null;
        }
        //reeee
        protected static internal LinkedList<Airport> GetNeighbours(string codeName, List<Route> routes)
        {
            try
            {
                LinkedList<Airport> neighbours = new LinkedList<Airport>();
                foreach (var port in routes)
                {
                    if ( port != null && port.SourceName == codeName)
                    {
                        var next = GetAirPort(port.DestinationName);

                        if (neighbours.Count == 0)
                        {
                            neighbours?.AddFirst(next);
                        }
                        else
                        {
                            neighbours.AddAfter(neighbours.Last, next);
                        }

                    }
                }
                return neighbours;
            }
            catch (Exception a)
            {
                Console.WriteLine(a.Message);
            }
            return null;
        }

        //reeeee
        protected internal static void WriteCsw()
        {
            try
            {
                var csv = new StringBuilder();
                var routes = GetRoutes();
                var airports = GetAirPort();
                foreach (var airport in airports)
                {
                    if (airport != null)
                    {
                        var source = airport.IATA.ToString();
                        var sourceName = airport.AirportName.ToString();
                        var neighbour = GetNeighbours(source, routes);
                        var adjacements = JsonConvert.SerializeObject(neighbour);
                        var newLine = string.Format("{0};{1};{2}", source, sourceName, adjacements);
                        if (neighbour.Count != 0)
                            csv.AppendLine(newLine);
                    }
                }
                File.WriteAllText("nextStations.txt", csv.ToString());
            }
            catch (Exception a)
            {
                Console.WriteLine(a.Message);
            }
        }
        //raeeaeee
        protected internal static NextAirport GetNextStation(string stationCode)
        {
            try
            {
                foreach (var line in Neighbours)
                {
                    string[] temp = line.Split(";", System.StringSplitOptions.RemoveEmptyEntries);
                        if (temp.Length==3 && temp[0] == stationCode)
                        {
                        var airport = GetAirPort(temp[0]);
                        if (airport != null)
                        {
                            NextAirport nextAirport = new NextAirport(airport, temp[1], int.MaxValue, JsonConvert.DeserializeObject<LinkedList<Airport>>(temp[2]));
                            return nextAirport;
                        }
                        }
                }
            }
            catch (Exception a)
            {
                Console.WriteLine(a.Message);
            }
            return null;

        }
        protected internal static string[] GetAllInfoFromFile( string resource)
        {
            try
            {

                using (var textFile = System.IO.File.OpenText(resource))
                {
                    string[] buffer = textFile.ReadToEnd().Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    return buffer;
                }

            }
            catch (Exception a)
            {
                Console.WriteLine(a.Message);
            }
            return null;

        }
    }
}
 

    
