using Il2CppInterop.Runtime.InteropTypes.Arrays;
using Il2CppMonomiPark.SlimeRancher.Weather;
using Il2CppMonomiPark.SlimeRancher.World;
using MoreSlimefall.Assist.Extension;
using MoreSlimefall.Data.Weather;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreSlimefall.Assist
{
    internal class LocalDictionaries
    {
        internal static Il2CppSystem.Collections.Generic.Dictionary<ZoneDefinition, WeatherPatternDefinition> IL2CPP_zoneToPatternDict;
        internal static Il2CppSystem.Collections.Generic.Dictionary<ZoneDefinition, Il2CppReferenceArray<WeatherStateDefinition>> IL2CPP_zoneToStatesDict; 

        private static Dictionary<ZoneDefinition, WeatherPatternDefinition> zoneToPatternDict
        {
            get
            {
                return new()
                {
                    {
                        WeatherZones.RAINBOW_FIELDS,
                        LocalWeathers.slimeRainPatternFields
                    },
                    {
                        WeatherZones.STARLIGHT_STRAND,
                        LocalWeathers.slimeRainPatternStrand
                    },
                    {
                        WeatherZones.EMBER_VALLEY,
                        LocalWeathers.slimeRainPatternValley
                    },
                    {
                        WeatherZones.POWDERFALL_BLUFFS,
                        LocalWeathers.slimeRainPatternBluffs
                    }
                };
            }
        }

        private static Dictionary<ZoneDefinition, WeatherStateDefinition[]> zoneToStatesDict
        {
            get
            {
                return new()
                {
                    {
                        WeatherZones.RAINBOW_FIELDS,
                        new WeatherStateDefinition[]
                        {
                            LocalWeathers.slimeRainStateFields,
                            SlimeRainModerateFields.slimeRainModerateFields,
                            SlimeRainSevereFields.slimeRainSevereFields
                        }
                    },
                    {
                        WeatherZones.STARLIGHT_STRAND,
                        new WeatherStateDefinition[]
                        {
                            LocalWeathers.slimeRainStateStrand,
                            SlimeRainModerateStrand.slimeRainModerateStrand,
                            SlimeRainSevereStrand.slimeRainSevereStrand
                        }
                    },
                    {
                        WeatherZones.EMBER_VALLEY,
                        new WeatherStateDefinition[]
                        {
                            LocalWeathers.slimeRainStateValley
                        }
                    },
                    {
                        WeatherZones.POWDERFALL_BLUFFS,
                        new WeatherStateDefinition[]
                        {
                            LocalWeathers.slimeRainStateBluffs
                        }
                    }
                };
            }
        }

        public static void Load(string sceneName)
        {
            switch (sceneName)
            {
                case "GameCore":
                    {
                        IL2CPP_zoneToPatternDict = new();
                        IL2CPP_zoneToStatesDict = new();

                        foreach (var keyValuePair in zoneToPatternDict)
                            IL2CPP_zoneToPatternDict.TryAdd(keyValuePair.Key, keyValuePair.Value);
                        foreach (var keyValuePair in zoneToStatesDict)
                            IL2CPP_zoneToStatesDict.TryAdd(keyValuePair.Key, keyValuePair.Value);
                        break;
                    }
            }
        }
    }
}
