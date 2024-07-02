using Il2CppInterop.Runtime.InteropTypes.Arrays;
using Il2CppMonomiPark.SlimeRancher.Weather;
using Il2CppMonomiPark.SlimeRancher.World;
using MoreSlimefall.Assist.Extension;
using MoreSlimefall.Data.Weathers;
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

        private static Dictionary<ZoneDefinition, WeatherPatternDefinition> ZoneToPatternDict
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

        private static Dictionary<ZoneDefinition, WeatherStateDefinition[]> ZoneToStatesDict
        {
            get
            {
                return new()
                {
                    {
                        WeatherZones.RAINBOW_FIELDS,
                        new[]
                        {
                            LocalWeathers.slimeRainStateFields,
                            SlimeRainModerateFields.slimeRainModerateFields,
                            SlimeRainSevereFields.slimeRainSevereFields
                        }
                    },
                    {
                        WeatherZones.STARLIGHT_STRAND,
                        new[]
                        {
                            LocalWeathers.slimeRainStateStrand,
                            SlimeRainModerateStrand.slimeRainModerateStrand,
                            SlimeRainSevereStrand.slimeRainSevereStrand
                        }
                    },
                    {
                        WeatherZones.EMBER_VALLEY,
                        new[]
                        {
                            LocalWeathers.slimeRainStateValley,
                            SlimeRainModerateValley.slimeRainModerateValley,
                            SlimeRainSevereValley.slimeRainSevereValley
                        }
                    },
                    {
                        WeatherZones.POWDERFALL_BLUFFS,
                        new[]
                        {
                            LocalWeathers.slimeRainStateBluffs,
                            SlimeRainModerateBluffs.slimeRainModerateBluffs,
                            SlimeRainSevereBluffs.slimeRainSevereBluffs
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

                        foreach (var keyValuePair in ZoneToPatternDict)
                            IL2CPP_zoneToPatternDict.TryAdd(keyValuePair.Key, keyValuePair.Value);
                        foreach (var keyValuePair in ZoneToStatesDict)
                            IL2CPP_zoneToStatesDict.TryAdd(keyValuePair.Key, keyValuePair.Value);
                        break;
                    }
            }
        }
    }
}
