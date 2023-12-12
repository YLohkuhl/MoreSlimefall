using Il2CppMonomiPark.SlimeRancher.Weather;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreSlimefall.Assist
{
    internal class LocalWeathers
    {
        // STATES
        internal static WeatherStateDefinition rainLightState;
        internal static WeatherStateDefinition rainMedState;
        internal static WeatherStateDefinition rainHeavyState;

        internal static WeatherStateDefinition windHeavyState;
        internal static WeatherStateDefinition pollenHeavyState;

        internal static WeatherStateDefinition slimeRainStateFields;
        internal static WeatherStateDefinition slimeRainStateStrand;
        internal static WeatherStateDefinition slimeRainStateValley;
        internal static WeatherStateDefinition slimeRainStateBluffs;

        // PATTERNS
        internal static WeatherPatternDefinition slimeRainPatternFields;
        internal static WeatherPatternDefinition slimeRainPatternStrand;
        internal static WeatherPatternDefinition slimeRainPatternValley;
        internal static WeatherPatternDefinition slimeRainPatternBluffs;

        public static void Load(string sceneName)
        {
            switch (sceneName)
            {
                case "GameCore":
                    {
                        // STATES
                        rainLightState = Get<WeatherStateDefinition>("Rain Light State");
                        rainMedState = Get<WeatherStateDefinition>("Rain Med State");
                        rainHeavyState = Get<WeatherStateDefinition>("Rain Heavy State");

                        windHeavyState = Get<WeatherStateDefinition>("Wind Heavy State");
                        pollenHeavyState = Get<WeatherStateDefinition>("Pollen Heavy State");

                        slimeRainStateFields = Get<WeatherStateDefinition>("Slime Rain State Fields");
                        slimeRainStateStrand = Get<WeatherStateDefinition>("Slime Rain State Strand");
                        slimeRainStateValley = Get<WeatherStateDefinition>("Slime Rain State Valley");
                        slimeRainStateBluffs = Get<WeatherStateDefinition>("Slime Rain State Bluffs");

                        slimeRainStateFields.MapTier = 1;
                        slimeRainStateStrand.MapTier = 1;
                        slimeRainStateValley.MapTier = 1;
                        slimeRainStateBluffs.MapTier = 1;

                        // PATTERNS
                        slimeRainPatternFields = Get<WeatherPatternDefinition>("Slime Rain Pattern Fields");
                        slimeRainPatternStrand = Get<WeatherPatternDefinition>("Slime Rain Pattern Strand");
                        slimeRainPatternValley = Get<WeatherPatternDefinition>("Slime Rain Pattern Valley");
                        slimeRainPatternBluffs = Get<WeatherPatternDefinition>("Slime Rain Pattern Bluffs");
                        break;
                    }
            }
        }
    }
}
