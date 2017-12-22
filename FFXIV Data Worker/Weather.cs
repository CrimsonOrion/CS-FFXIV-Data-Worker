using SaintCoinach;
using System;
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
            sRankEasternLaNosceaCondition = DateTime.Now;



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
}
