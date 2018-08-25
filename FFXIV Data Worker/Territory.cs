using System.Collections.Generic;
using System.Linq;

namespace FFXIV_Data_Worker
{
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
                {128, new Territory ("Limsa Lominsa Upper Decks",14) },
                {129, new Territory ("Limsa Lominsa Lower Decks",15) },
                {130, new Territory ("Ul'dah - Steps of Nald",7) },
                {131, new Territory ("Ul'dah - Steps of Thal",8) },
                {132, new Territory ("New Gridania",1) },
                {133, new Territory ("Old Gridania",2) },
                {134, new Territory ("Middle La Noscea",16) },
                {135, new Territory ("Lower La Noscea",17) },
                {137, new Territory ("Eastern La Noscea",18) },
                {138, new Territory ("Western La Noscea",19) },
                {139, new Territory ("Upper La Noscea",20) },
                {140, new Territory ("Western Thanalan",9) },
                {141, new Territory ("Central Thanalan",10) },
                {145, new Territory ("Eastern Thanalan",11) },
                {146, new Territory ("Southern Thanalan",12) },
                {147, new Territory ("Northern Thanalan",13) },
                {148, new Territory ("Central Shroud",3) },
                {152, new Territory ("East Shroud",4) },
                {153, new Territory ("South Shroud",5) },
                {154, new Territory ("North Shroud",6) },
                {155, new Territory ("Coerthas Central Highlands",21) },
                {156, new Territory ("Mor Dhona",22) },
                {180, new Territory ("Outer La Noscea",24) },
                {339, new Territory ("Mist",32) },
                {340, new Territory ("The Lavender Beds",34) },
                {341, new Territory ("The Goblet",33) },
                {397, new Territory ("Coerthas Western Highlands",49) },
                {398, new Territory ("The Dravanian Forelands",50) },
                {399, new Territory ("The Dravanian Hinterlands",51) },
                {400, new Territory ("The Churning Mists",52) },
                {401, new Territory ("The Sea of Clouds",53) },
                {402, new Territory ("Azys Lla",54) },
                {418, new Territory ("Foundation",47) },
                {419, new Territory ("The Pillars",48) },
                {478, new Territory ("Idyllshire",55) },
                {612, new Territory ("The Fringes",79) },
                {613, new Territory ("The Ruby Sea",83) },
                {614, new Territory ("Yanxia",84) },
                {620, new Territory ("The Peaks",80) },
                {621, new Territory ("The Lochs",81) },
                {622, new Territory ("The Azim Steppe",85) },
                {628, new Territory ("Kugane",82) },
                {635, new Territory ("Rhalgr's Reach",78) },
                {641, new Territory ("Shirogane",82) },
                {682, new Territory ("Doman Enclave",84) },
                {732, new Territory ("Eureka Anemos",91) },
                {763, new Territory ("Eureka Pagos",94) }
            };

            return territory;
        }
    }
}
