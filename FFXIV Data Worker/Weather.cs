using SaintCoinach;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace FFXIV_Data_Worker
{
    class Weather
    {
        public static readonly DateTime EPOCH = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static string GetThisWeather(DateTime dateTime, string[] zones, int forcastIntervals)
        {
            EorzeaDateTime eorzeaDateTime = new EorzeaDateTime(dateTime);
            Territory t;
            string weatherForcast;
            bool sRankCentralShroudCondition = false; // double rain (2 Eorzean hours into 2nd rain/shower)
            DateTime sRankEasternLaNosceaCondition = new DateTime(); // No rain/showers for 200 RL minutes



            if (zones == null)
            {   
                var enumerator = Territory.territory.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    EorzeaDateTime eorzeaDateTimeIncrements = new EorzeaDateTime(eorzeaDateTime);
                    var pair = enumerator.Current;
                    t = pair.Value;
                    weatherForcast = $"{t.PlaceName}:\r\n";
                    for (int i = 0; i < forcastIntervals; i++)
                    {
                        int forcastIndex = CalculateTarget(eorzeaDateTimeIncrements);
                        if (t.ThisWeatherRate != null) { weatherForcast += $"{eorzeaDateTimeIncrements} ({eorzeaDateTimeIncrements.GetRealTime().ToLocalTime().ToShortTimeString()}) - {Forcast(forcastIndex, t.ThisWeatherRate)}\r\n"; }
                        eorzeaDateTimeIncrements = Increment(eorzeaDateTimeIncrements);
                    }

                    return weatherForcast;
                }
            }
            else
            {
                foreach (var zone in zones)
                {
                    EorzeaDateTime eorzeaDateTimeIncrements = new EorzeaDateTime(eorzeaDateTime);
                    t = Territory.territory.FirstOrDefault(pN => pN.Value.PlaceName == zone).Value;
                    weatherForcast = $"{t.PlaceName}:\r\n";
                    for (int i = 0; i < forcastIntervals; i++)
                    {
                        int forcastIndex = CalculateTarget(eorzeaDateTimeIncrements);
                        
                        if (t.ThisWeatherRate != null) {
                            string weather = Forcast(forcastIndex, t.ThisWeatherRate);
                            DateTime localTime = eorzeaDateTimeIncrements.GetRealTime().ToLocalTime();
                            weatherForcast += $"{eorzeaDateTimeIncrements} ({localTime}) - {weather}\r\n";
                            if (zone == "Central Shroud" && (weather == "Rain" || weather == "Showers"))
                            {
                                if (sRankCentralShroudCondition)
                                {                                    
                                    EorzeaDateTime spawnTime = eorzeaDateTimeIncrements;
                                    spawnTime.Bell = Convert.ToInt32(spawnTime.Bell) + 2;
                                    
                                    weatherForcast += $"\r\nCould spawn @ {spawnTime} {spawnTime.GetRealTime().ToLocalTime()}\r\n\r\n";
                                }
                                sRankCentralShroudCondition = true;
                            }
                            else if (zone == "Eastern La Noscea")
                            {
                                if (weather == "Rain" || weather == "Showers")
                                {
                                    sRankEasternLaNosceaCondition = localTime.AddMinutes(23).AddSeconds(20);
                                }
                                else if (localTime >= sRankEasternLaNosceaCondition.AddMinutes(200) && sRankEasternLaNosceaCondition.Year != 1)
                                {
                                    EorzeaDateTime spawnTime = new EorzeaDateTime(sRankEasternLaNosceaCondition.AddMinutes(200));
                                    weatherForcast += $"\r\nCould spawn @ {spawnTime} {spawnTime.GetRealTime().ToLocalTime()}\r\n\r\n";
                                }
                            }
                            else
                            {
                                sRankCentralShroudCondition = false;                                
                            }

                        }
                        eorzeaDateTimeIncrements = Increment(eorzeaDateTimeIncrements);
                    }

                    return weatherForcast;
                }

            }

            return null;

        }

        public static void GetThisWeather()
        {
            GetThisWeather(DateTime.Now, null, 1);
        }

        public static void GetThisWeather(EorzeaDateTime eorzeaDateTime, string[] zones = null, int forcastIntervals = 1)
        {
            DateTime dateTime = eorzeaDateTime.GetRealTime();
            GetThisWeather(dateTime, zones, forcastIntervals);
        }

        public static void GetThisWeather(string[] zones)
        {
            GetThisWeather(DateTime.Now, zones, 1);
        }

        public static EorzeaDateTime Increment(EorzeaDateTime eDT)
        {
            eDT.Minute = 0;
            if (eDT.Bell < 8)
                eDT.Bell = 8;
            else if (eDT.Bell < 16)
                eDT.Bell = 16;
            else
            { eDT.Bell = 0; eDT.Sun += 1; }

            return eDT;
        }

        private static int CalculateTarget(EorzeaDateTime time)
        {
            var unix = time.GetUnixTime();
            // Get Eorzea hour for weather start
            var bell = unix / 175;
            // Do the magic 'cause for calculations 16:00 is 0, 00:00 is 8 and 08:00 is 16
            var increment = ((uint)(bell + 8 - (bell % 8))) % 24;

            // Take Eorzea days since unix epoch
            var totalDays = (uint)(unix / 4200);

            var calcBase = (totalDays * 0x64) + increment;

            var step1 = (calcBase << 0xB) ^ calcBase;
            var step2 = (step1 >> 8) ^ step1;

            return (int)(step2 % 0x64);
        }

        private static string Forcast(int fI, WeatherRate wR)
        {
            string forcast = string.Empty;

            try
            {
                if (fI < wR.WeatherRate1)
                    forcast = wR.Weather1;
                else if (fI < wR.WeatherRate2)
                    forcast = wR.Weather2;
                else if (fI < wR.WeatherRate3)
                    forcast = wR.Weather3;
                else if (fI < wR.WeatherRate4)
                    forcast = wR.Weather4;
                else if (fI < wR.WeatherRate5)
                    forcast = wR.Weather5;
                else if (fI < wR.WeatherRate6)
                    forcast = wR.Weather6;
                else if (fI < wR.WeatherRate7)
                    forcast = wR.Weather7;
                else if (fI < wR.WeatherRate8)
                    forcast = wR.Weather8;
                else
                    forcast = "None found";
            }
            catch (Exception e)
            {
                forcast = $"Null due to {e.Message}";
            }
            return forcast;
        }

        private static bool CheckCondition(string zone, string weather)
        {
            bool spawnCondition = false;
            if (zone == "Central Shroud" && (weather.Contains("Rain") || weather.Contains("Showers")))
                spawnCondition = true;

            return spawnCondition;
        }
    }

    class Territory
    {
        public string PlaceName { get; set; }
        public WeatherRate ThisWeatherRate { get; set; }

        public static Dictionary<int, Territory> territory;

        public Territory(string placeName, int weatherRateIndex)
        {
            PlaceName = placeName;
            ThisWeatherRate = WeatherRate.weatherRate.FirstOrDefault(w => w.Key == weatherRateIndex).Value;
        }

        public static Dictionary<int, Territory> GetTerritory()
        {
            territory = new Dictionary<int, Territory>
            {
                {128, new Territory ("Limsa Lominsa Upper Decks",131) },
                {129, new Territory ("Limsa Lominsa Lower Decks",132) },
                {130, new Territory ("Ul'dah - Steps of Nald",133) },
                {131, new Territory ("Ul'dah - Steps of Thal",134) },
                {132, new Territory ("New Gridania",129) },
                {133, new Territory ("Old Gridania",130) },
                {134, new Territory ("Middle La Noscea",16) },
                {135, new Territory ("Lower La Noscea",17) },
                {136, new Territory ("Mist",14) },
                {137, new Territory ("Eastern La Noscea",18) },
                {138, new Territory ("Western La Noscea",19) },
                {139, new Territory ("Upper La Noscea",20) },
                {140, new Territory ("Western Thanalan",9) },
                {141, new Territory ("Central Thanalan",10) },
                {145, new Territory ("Eastern Thanalan",11) },
                {146, new Territory ("Southern Thanalan",12) },
                {147, new Territory ("Northern Thanalan",13) },
                {148, new Territory ("Central Shroud",3) },
                {149, new Territory ("The Feasting Grounds",65) },
                {152, new Territory ("East Shroud",4) },
                {153, new Territory ("South Shroud",5) },
                {154, new Territory ("North Shroud",6) },
                {155, new Territory ("Coerthas Central Highlands",21) },
                {156, new Territory ("Mor Dhona",22) },
                {180, new Territory ("Outer La Noscea",24) },
                {183, new Territory ("New Gridania",0) },
                {198, new Territory ("Command Room",14) },
                {238, new Territory ("Old Gridania",2) },
                {250, new Territory ("Wolves' Den Pier",29) },
                {251, new Territory ("Ul'dah - Steps of Nald",7) },
                {339, new Territory ("Mist",135) },
                {340, new Territory ("The Lavender Beds",136) },
                {341, new Territory ("The Goblet",137) },
                {368, new Territory ("The Weeping Saint",21) },
                {397, new Territory ("Coerthas Western Highlands",49) },
                {398, new Territory ("The Dravanian Forelands",50) },
                {399, new Territory ("The Dravanian Hinterlands",51) },
                {400, new Territory ("The Churning Mists",52) },
                {401, new Territory ("The Sea of Clouds",53) },
                {409, new Territory ("Limsa Lominsa Upper Decks",40) },
                {418, new Territory ("Foundation",47) },
                {419, new Territory ("The Pillars",48) },
                {431, new Territory ("Seal Rock",59) },
                {478, new Territory ("Idyllshire",55) },
                {499, new Territory ("The Pillars",139) },
                {512, new Territory ("The Diadem",60) },
                {514, new Territory ("The Diadem",61) },
                {515, new Territory ("The Diadem",62) },
                {518, new Territory ("The Feasting Grounds",64) },
                {554, new Territory ("The Fields of Glory",67) },
                {612, new Territory ("The Fringes",79) },
                {613, new Territory ("The Ruby Sea",83) },
                {614, new Territory ("Yanxia",84) },
                {620, new Territory ("The Peaks",80) },
                {621, new Territory ("The Lochs",81) },
                {622, new Territory ("The Azim Steppe",85) },
                {624, new Territory ("The Diadem",60) },
                {625, new Territory ("The Diadem",61) },
                {628, new Territory ("Kugane",82) },
                {630, new Territory ("The Diadem",71) },
                {635, new Territory ("Rhalgr's Reach",78) },
                {641, new Territory ("Shirogane",141) },
                {656, new Territory ("The Diadem",71) },
                {679, new Territory ("The Royal Airship Landing",76) },
                {681, new Territory ("The House of the Fierce",84) },
                {682, new Territory ("Doman Enclave",84) }
            };

            return territory;
        }
    }

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
                { 90, new WeatherRate("", 0, "", 0, "", 0, "", 0, "", 0, "", 0, "", 0, "", 0) }
            };

            return weatherRate;
        }
        
    }
}
