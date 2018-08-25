using System.Collections.Generic;

namespace FFXIV_Data_Worker
{
    class WeatherRate
    {        
        public string Weather1 { get; set; }
        public int WeatherRate1 { get; set; }
        public string Weather2 { get; set; }
        public int WeatherRate2 { get; set; }
        public string Weather3 { get; set; }
        public int WeatherRate3 { get; set; }
        public string Weather4 { get; set; }
        public int WeatherRate4 { get; set; }
        public string Weather5 { get; set; }
        public int WeatherRate5 { get; set; }
        public string Weather6 { get; set; }
        public int WeatherRate6 { get; set; }
        public string Weather7 { get; set; }
        public int WeatherRate7 { get; set; }
        public string Weather8 { get; set; }
        public int WeatherRate8 { get; set; }

        public static Dictionary<int,  WeatherRate> weatherRate;

        public WeatherRate(string w1,  int wR1,  string w2,  int wR2,  string w3,  int wR3,  string w4,  int wR4,  string w5,  int wR5,  string w6,  int wR6,  string w7,  int wR7,  string w8,  int wR8)
        {
            Weather1 = w1;
            WeatherRate1 = wR1;
            Weather2 = w2;
            WeatherRate2 = WeatherRate1 + wR2;
            Weather3 = w3;
            WeatherRate3 = WeatherRate2 + wR3;
            Weather4 = w4;
            WeatherRate4 = WeatherRate3 + wR4;
            Weather5 = w5;
            WeatherRate5 = WeatherRate4 + wR5;
            Weather6 = w6;
            WeatherRate6 = WeatherRate5 + wR6;
            Weather7 = w7;
            WeatherRate7 = WeatherRate6 + wR7;
            Weather8 = w8;
            WeatherRate8 = WeatherRate7 + wR8;
        }

        public static Dictionary<int, WeatherRate> GetWeatherRate()
        {
            weatherRate = new Dictionary<int,  WeatherRate>
            {
                { 0, new WeatherRate("Fair Skies",  100,  "",  0,  "",  0,  "",  0,  "",  0,  "",  0,  "",  0,  "",  0) }, 
                { 1, new WeatherRate("Rain", 5, "Rain", 15, "Fog", 10, "Clouds", 10, "Fair Skies", 15, "Clear Skies", 30, "Fair Skies", 15, "", 0) }, 
                { 2, new WeatherRate("Rain", 5, "Rain", 15, "Fog", 10, "Clouds", 10, "Fair Skies", 15, "Clear Skies", 30, "Fair Skies", 15, "", 0) }, 
                { 3, new WeatherRate("Thunder", 5, "Rain", 15, "Fog", 10, "Clouds", 10, "Fair Skies", 15, "Clear Skies", 30, "Fair Skies", 15, "", 0) }, 
                { 4, new WeatherRate("Thunder", 5, "Rain", 15, "Fog", 10, "Clouds", 10, "Fair Skies", 15, "Clear Skies", 30, "Fair Skies", 15, "", 0) }, 
                { 5, new WeatherRate("Fog", 5, "Thunderstorms", 5, "Thunder", 15, "Fog", 5, "Clouds", 10, "Fair Skies", 30, "Clear Skies", 30, "", 0) }, 
                { 6, new WeatherRate("Fog", 5, "Showers", 5, "Rain", 15, "Fog", 5, "Clouds", 10, "Fair Skies", 30, "Clear Skies", 30, "", 0) }, 
                { 7, new WeatherRate("Clear Skies", 40, "Fair Skies", 20, "Clouds", 25, "Fog", 10, "Rain", 5, "", 0, "", 0, "", 0) }, 
                { 8, new WeatherRate("Clear Skies", 40, "Fair Skies", 20, "Clouds", 25, "Fog", 10, "Rain", 5, "", 0, "", 0, "", 0) }, 
                { 9, new WeatherRate("Clear Skies", 40, "Fair Skies", 20, "Clouds", 25, "Fog", 10, "Rain", 5, "", 0, "", 0, "", 0) }, 
                { 10, new WeatherRate("Dust Storms", 15, "Clear Skies", 40, "Fair Skies", 20, "Clouds", 10, "Fog", 10, "Rain", 5, "", 0, "", 0) }, 
                { 11, new WeatherRate("Clear Skies", 40, "Fair Skies", 20, "Clouds", 10, "Fog", 10, "Rain", 5, "Showers", 15, "", 0, "", 0) }, 
                { 12, new WeatherRate("Heat Waves", 20, "Clear Skies", 40, "Fair Skies", 20, "Clouds", 10, "Fog", 10, "", 0, "", 0, "", 0) }, 
                { 13, new WeatherRate("Clear Skies", 5, "Fair Skies", 15, "Clouds", 30, "Fog", 50, "", 0, "", 0, "", 0, "", 0) }, 
                { 14, new WeatherRate("Clouds", 20, "Clear Skies", 30, "Fair Skies", 30, "Fog", 10, "Rain", 10, "", 0, "", 0, "", 0) }, 
                { 15, new WeatherRate("Clouds", 20, "Clear Skies", 30, "Fair Skies", 30, "Fog", 10, "Rain", 10, "", 0, "", 0, "", 0) }, 
                { 16, new WeatherRate("Clouds", 20, "Clear Skies", 30, "Fair Skies", 20, "Wind", 10, "Fog", 10, "Rain", 10, "", 0, "", 0) }, 
                { 17, new WeatherRate("Clouds", 20, "Clear Skies", 30, "Fair Skies", 20, "Wind", 10, "Fog", 10, "Rain", 10, "", 0, "", 0) }, 
                { 18, new WeatherRate("Fog", 5, "Clear Skies", 45, "Fair Skies", 30, "Clouds", 10, "Rain", 5, "Showers", 5, "", 0, "", 0) }, 
                { 19, new WeatherRate("Fog", 10, "Clear Skies", 30, "Fair Skies", 20, "Clouds", 20, "Wind", 10, "Gales", 10, "", 0, "", 0) }, 
                { 20, new WeatherRate("Clear Skies", 30, "Fair Skies", 20, "Clouds", 20, "Fog", 10, "Thunder", 10, "Thunderstorms", 10, "", 0, "", 0) }, 
                { 21, new WeatherRate("Blizzards", 20, "Snow", 40, "Fair Skies", 10, "Clear Skies", 5, "Clouds", 15, "Fog", 10, "", 0, "", 0) }, 
                { 22, new WeatherRate("Clouds", 15, "Fog", 15, "Gloom", 30, "Clear Skies", 15, "Fair Skies", 25, "", 0, "", 0, "", 0) }, 
                { 23, new WeatherRate("Eruptions", 100, "", 0, "", 0, "", 0, "", 0, "", 0, "", 0, "", 0) }, 
                { 24, new WeatherRate("Clear Skies", 30, "Fair Skies", 20, "Clouds", 20, "Fog", 15, "Rain", 15, "", 0, "", 0, "", 0) }, 
                { 25, new WeatherRate("Heat Waves", 100, "", 0, "", 0, "", 0, "", 0, "", 0, "", 0, "", 0) }, 
                { 26, new WeatherRate("Gales", 100, "", 0, "", 0, "", 0, "", 0, "", 0, "", 0, "", 0) }, 
                { 27, new WeatherRate("Snow", 100, "", 0, "", 0, "", 0, "", 0, "", 0, "", 0, "", 0) }, 
                { 28, new WeatherRate("Clouds", 100, "", 0, "", 0, "", 0, "", 0, "", 0, "", 0, "", 0) }, 
                { 29, new WeatherRate("Clouds", 20, "Clear Skies", 30, "Fair Skies", 30, "Fog", 10, "Thunderstorms", 10, "", 0, "", 0, "", 0) }, 
                { 30, new WeatherRate("Gloom", 100, "", 0, "", 0, "", 0, "", 0, "", 0, "", 0, "", 0) }, 
                { 31, new WeatherRate("Louring", 100, "", 0, "", 0, "", 0, "", 0, "", 0, "", 0, "", 0) }, 
                { 32, new WeatherRate("Clouds", 20, "Clear Skies", 30, "Fair Skies", 20, "Fair Skies", 10, "Fog", 10, "Rain", 10, "", 0, "", 0) }, 
                { 33, new WeatherRate("Clear Skies", 40, "Fair Skies", 20, "Clouds", 25, "Fog", 10, "Rain", 5, "", 0, "", 0, "", 0) }, 
                { 34, new WeatherRate("Clouds", 5, "Rain", 15, "Fog", 10, "Clouds", 10, "Fair Skies", 15, "Clear Skies", 30, "Fair Skies", 15, "", 0) }, 
                { 35, new WeatherRate("Snow", 100, "", 0, "", 0, "", 0, "", 0, "", 0, "", 0, "", 0) }, 
                { 36, new WeatherRate("Wind", 100, "", 0, "", 0, "", 0, "", 0, "", 0, "", 0, "", 0) }, 
                { 37, new WeatherRate("Thunder", 100, "", 0, "", 0, "", 0, "", 0, "", 0, "", 0, "", 0) }, 
                { 38, new WeatherRate("Rough Seas", 100, "", 0, "", 0, "", 0, "", 0, "", 0, "", 0, "", 0) }, 
                { 39, new WeatherRate("Rough Seas", 100, "", 0, "", 0, "", 0, "", 0, "", 0, "", 0, "", 0) }, 
                { 40, new WeatherRate("Fog", 100, "", 0, "", 0, "", 0, "", 0, "", 0, "", 0, "", 0) }, 
                { 41, new WeatherRate("Dust Storms", 100, "", 0, "", 0, "", 0, "", 0, "", 0, "", 0, "", 0) }, 
                { 42, new WeatherRate("Blizzards", 100, "", 0, "", 0, "", 0, "", 0, "", 0, "", 0, "", 0) }, 
                { 43, new WeatherRate("Storm Clouds", 100, "", 0, "", 0, "", 0, "", 0, "", 0, "", 0, "", 0) }, 
                { 44, new WeatherRate("Core Radiation", 100, "", 0, "", 0, "", 0, "", 0, "", 0, "", 0, "", 0) }, 
                { 45, new WeatherRate("Tension", 100, "", 0, "", 0, "", 0, "", 0, "", 0, "", 0, "", 0) }, 
                { 46, new WeatherRate("Irradiance", 100, "", 0, "", 0, "", 0, "", 0, "", 0, "", 0, "", 0) }, 
                { 47, new WeatherRate("Snow", 60, "Fair Skies", 10, "Clear Skies", 5, "Clouds", 15, "Fog", 10, "", 0, "", 0, "", 0) }, 
                { 48, new WeatherRate("Snow", 60, "Fair Skies", 10, "Clear Skies", 5, "Clouds", 15, "Fog", 10, "", 0, "", 0, "", 0) }, 
                { 49, new WeatherRate("Blizzards", 20, "Snow", 40, "Fair Skies", 10, "Clear Skies", 5, "Clouds", 15, "Fog", 10, "", 0, "", 0) }, 
                { 50, new WeatherRate("Clouds", 10, "Fog", 10, "Thunder", 10, "Dust Storms", 10, "Clear Skies", 30, "Fair Skies", 30, "", 0, "", 0) }, 
                { 51, new WeatherRate("Clouds", 10, "Fog", 10, "Rain", 10, "Showers", 10, "Clear Skies", 30, "Fair Skies", 30, "", 0, "", 0) }, 
                { 52, new WeatherRate("Clouds", 10, "Gales", 10, "Umbral Static", 20, "Clear Skies", 30, "Fair Skies", 30, "", 0, "", 0, "", 0) }, 
                { 53, new WeatherRate("Clear Skies", 30, "Fair Skies", 30, "Clouds", 10, "Fog", 10, "Wind", 10, "Umbral Wind", 10, "", 0, "", 0) }, 
                { 54, new WeatherRate("Fair Skies", 35, "Clouds", 35, "Thunder", 30, "", 0, "", 0, "", 0, "", 0, "", 0) }, 
                { 55, new WeatherRate("Clouds", 10, "Fog", 10, "Rain", 10, "Showers", 10, "Clear Skies", 30, "Fair Skies", 30, "", 0, "", 0) }, 
                { 56, new WeatherRate("Oppression", 100, "", 0, "", 0, "", 0, "", 0, "", 0, "", 0, "", 0) }, 
                { 57, new WeatherRate("Smoke", 100, "", 0, "", 0, "", 0, "", 0, "", 0, "", 0, "", 0) }, 
                { 58, new WeatherRate("Clear Skies", 100, "", 0, "", 0, "", 0, "", 0, "", 0, "", 0, "", 0) }, 
                { 59, new WeatherRate("Fog", 15, "Rain", 25, "Fair Skies", 60, "", 0, "", 0, "", 0, "", 0, "", 0) }, 
                { 60, new WeatherRate("Fair Skies", 40, "Fog", 35, "Wind", 20, "Umbral Wind", 4, "Hyperelectricity", 1, "", 0, "", 0, "", 0) }, 
                { 61, new WeatherRate("Fair Skies", 40, "Fog", 20, "Wind", 35, "Umbral Wind", 4, "Hyperelectricity", 1, "", 0, "", 0, "", 0) }, 
                { 62, new WeatherRate("Fair Skies", 40, "Fog", 25, "Wind", 25, "Umbral Wind", 8, "Hyperelectricity", 2, "", 0, "", 0, "", 0) }, 
                { 63, new WeatherRate("Rain", 100, "", 0, "", 0, "", 0, "", 0, "", 0, "", 0, "", 0) }, 
                { 64, new WeatherRate("Fair Skies", 60, "Rain", 40, "", 0, "", 0, "", 0, "", 0, "", 0, "", 0) }, 
                { 65, new WeatherRate("Fair Skies", 50, "Rain", 50, "", 0, "", 0, "", 0, "", 0, "", 0, "", 0) }, 
                { 66, new WeatherRate("Multiplicity", 100, "", 0, "", 0, "", 0, "", 0, "", 0, "", 0, "", 0) }, 
                { 67, new WeatherRate("Fair Skies", 35, "Snow", 30, "Blizzards", 20, "Fog", 15, "", 0, "", 0, "", 0, "", 0) }, 
                { 68, new WeatherRate("Heat Waves", 100, "", 0, "", 0, "", 0, "", 0, "", 0, "", 0, "", 0) }, 
                { 69, new WeatherRate("Concordance", 100, "", 0, "", 0, "", 0, "", 0, "", 0, "", 0, "", 0) }, 
                { 70, new WeatherRate("Hyperelectricity", 100, "", 0, "", 0, "", 0, "", 0, "", 0, "", 0, "", 0) }, 
                { 71, new WeatherRate("Fair Skies", 30, "Fog", 30, "Wind", 30, "Umbral Wind", 10, "", 0, "", 0, "", 0, "", 0) }, 
                { 72, new WeatherRate("Fair Skies", 65, "Rain", 35, "", 0, "", 0, "", 0, "", 0, "", 0, "", 0) }, 
                { 73, new WeatherRate("Fair Skies", 65, "Rain", 35, "", 0, "", 0, "", 0, "", 0, "", 0, "", 0) }, 
                { 74, new WeatherRate("Gloom", 100, "", 0, "", 0, "", 0, "", 0, "", 0, "", 0, "", 0) }, 
                { 75, new WeatherRate("Demonic Infinity", 100, "", 0, "", 0, "", 0, "", 0, "", 0, "", 0, "", 0) }, 
                { 76, new WeatherRate("Wyrmstorm", 100, "", 0, "", 0, "", 0, "", 0, "", 0, "", 0, "", 0) }, 
                { 77, new WeatherRate("Revelstorm", 100, "", 0, "", 0, "", 0, "", 0, "", 0, "", 0, "", 0) }, 
                { 78, new WeatherRate("Clear Skies", 15, "Fair Skies", 45, "Clouds", 20, "Fog", 10, "Thunder", 10, "", 0, "", 0, "", 0) }, 
                { 79, new WeatherRate("Clear Skies", 15, "Fair Skies", 45, "Clouds", 20, "Fog", 10, "Thunder", 10, "", 0, "", 0, "", 0) }, 
                { 80, new WeatherRate("Clear Skies", 10, "Fair Skies", 50, "Clouds", 15, "Fog", 10, "Wind", 10, "Dust Storms", 5, "", 0, "", 0) }, 
                { 81, new WeatherRate("Clear Skies", 20, "Fair Skies", 40, "Clouds", 20, "Fog", 10, "Thunderstorms", 10, "", 0, "", 0, "", 0) }, 
                { 82, new WeatherRate("Rain", 10, "Fog", 10, "Clouds", 20, "Fair Skies", 40, "Clear Skies", 20, "", 0, "", 0, "", 0) }, 
                { 83, new WeatherRate("Thunder", 10, "Wind", 10, "Clouds", 15, "Fair Skies", 40, "Clear Skies", 25, "", 0, "", 0, "", 0) }, 
                { 84, new WeatherRate("Showers", 5, "Rain", 10, "Fog", 10, "Clouds", 15, "Fair Skies", 40, "Clear Skies", 20, "", 0, "", 0) }, 
                { 85, new WeatherRate("Gales", 5, "Wind", 5, "Rain", 7, "Fog", 8, "Clouds", 10, "Fair Skies", 40, "Clear Skies", 25, "", 0) }, 
                { 86, new WeatherRate("Umbral Static", 100, "", 0, "", 0, "", 0, "", 0, "", 0, "", 0, "", 0) }, 
                { 87, new WeatherRate("Eternal Bliss", 100, "", 0, "", 0, "", 0, "", 0, "", 0, "", 0, "", 0) }, 
                { 88, new WeatherRate("Dimensional Disruption", 100, "", 0, "", 0, "", 0, "", 0, "", 0, "", 0, "", 0) }, 
                { 89, new WeatherRate("Thunder", 100, "", 0, "", 0, "", 0, "", 0, "", 0, "", 0, "", 0) }, 
                { 90, new WeatherRate("Fair Skies", 100, "", 0, "", 0, "", 0, "", 0, "", 0, "", 0, "", 0) },
                { 91, new WeatherRate("Fair Skies", 30, "Gales", 30, "Showers", 30, "Snow", 10, "", 0, "", 0, "", 0, "", 0) },
                { 92, new WeatherRate("Dimensional Disruption", 100, "", 0, "", 0, "", 0, "", 0, "", 0, "", 0, "", 0) },
                { 93, new WeatherRate("White Cyclone", 100, "", 0, "", 0, "", 0, "", 0, "", 0, "", 0, "", 0) },
                { 94, new WeatherRate("Fair Skies", 10, "Fog", 18, "Heat Wave", 18, "Snow", 18, "Thunder", 18, "Blizzard", 18, "", 0, "", 0) },
                { 95, new WeatherRate("Fair Skies", 100, "", 0, "", 0, "", 0, "", 0, "", 0, "", 0, "", 0) }
            };

            return weatherRate;
        }
        
    }
}
