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
}
