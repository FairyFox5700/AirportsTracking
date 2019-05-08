using System;
using System.Collections.Generic;
using System.Text;

namespace Airports
{
    public class Airport
    {
        public string AirportId { get; set; }
        public string AirportName { get; set; }
        public string CityName { get; set; }
        public string CountryName { get; set; }
        public string IATA { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public Airport(string[] temp)
        {
            switch (temp.Length)
            {
                case 14:
                    AirportId = temp[0].ToString();
                    AirportName = temp[1].ToString();
                    CityName = temp[2].ToString();
                    CountryName = temp[3].ToString();
                    IATA = temp[4].ToString();
                    Latitude = double.Parse(temp[6].ToString(), System.Globalization.CultureInfo.InvariantCulture);
                    Longitude = double.Parse(temp[7].ToString(), System.Globalization.CultureInfo.InvariantCulture);
                    break;
                case 15:

                    AirportId = temp[0].ToString();
                    AirportName = temp[1].ToString();
                    CityName = temp[2].ToString();
                    CountryName = temp[4].ToString();
                    IATA = temp[5].ToString();
                    Latitude = double.Parse(temp[7].ToString(), System.Globalization.CultureInfo.InvariantCulture);
                    Longitude = double.Parse(temp[8].ToString(), System.Globalization.CultureInfo.InvariantCulture);

                    break;
                case 13:
                    AirportId = temp[0].ToString();
                    AirportName = temp[1].ToString();
                    CityName = temp[2].ToString();
                    CountryName = temp[3].ToString();
                    IATA = temp[4].ToString();
                    Latitude = double.Parse(temp[5].ToString(), System.Globalization.CultureInfo.InvariantCulture);
                    Longitude = double.Parse(temp[6].ToString(), System.Globalization.CultureInfo.InvariantCulture);
                    break;
            }
        }
        public Airport()
        {

        }
    }
}
