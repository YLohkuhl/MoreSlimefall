using Il2CppInterop.Runtime;
using Il2CppMonomiPark.SlimeRancher.Weather;
using Il2CppMonomiPark.SlimeRancher.Weather.Conditions;
using Il2CppMonomiPark.SlimeRancher.World;
using Il2CppSystem.Linq;
using MelonLoader;
using MoreSlimefall.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static MoreSlimefall.Assist.LocalDictionaries;

namespace MoreSlimefall.Assist.Extension
{
    public static class ExtensionHelper
    {
        public static void LoadAllExtensions(MelonAssembly melonAssembly, string sceneName)
        {
            var extensions = melonAssembly.Assembly.GetTypes().Where(type => type.IsSubclassOf(typeof(MoreSlimefallExtension)));
            foreach (var extension in extensions)
            {
                MoreSlimefallExtension ext = Activator.CreateInstance(extension) as MoreSlimefallExtension;
                LoadExtension(ext, sceneName);
            }
        }

        public static void LoadExtension(MoreSlimefallExtension extension, string sceneName)
        {
            MoreSlimefallExtension ext = extension;

            if (ext.SlimefallTier < 1)
                throw new MoreSlimefallException($"{ext.GetType().Namespace}.{ext.GetType().Name}.SlimefallTier subceeded the minimum tier. Tier {ext.SlimefallTier} was given, Tier {1} is the minimum tier.");
            else if (ext.SlimefallTier > 3)
                throw new MoreSlimefallException($"{ext.GetType().Namespace}.{ext.GetType().Name}.SlimefallTier exceeded the maximum tier. Tier {ext.SlimefallTier} was given, Tier {3} is the maximum tier.");

            switch (sceneName)
            {
                case "SystemCore":
                    {
                        ext.OnSystemCore();
                        break;
                    }
                case "GameCore":
                    {
                        ext.StateDefinition = WeatherHelper.CreateWeatherState(ext.Name, ext.StateActivities, ext.SlimefallTier, ext.MinDurationHours);

                        // ext.OnInitialize();
                        ext.OnGameCore();
                        GenerateExtensionTransitions(ext);

                        WeatherHelper.RegisterWeatherState(ext.StateDefinition);
                        break;
                    }
                case "zoneCore":
                    {
                        ext.OnZoneCore();
                        break;
                    }
            }
        }

        public static void GenerateExtensionTransitions(MoreSlimefallExtension extension)
        {
            MoreSlimefallExtension ext = extension;

            foreach (ZoneDefinition zone in ext.ShowForZones)
            {
                if (IL2CPP_zoneToPatternDict.ContainsKey(zone))
                {
                    var toArray = IL2CPP_zoneToPatternDict[zone].RunningTransitions.ToArray();
                    int index = IL2CPP_zoneToPatternDict[zone].RunningTransitions.IndexOf(toArray.FirstOrDefault(x => x?.FromState == IL2CPP_zoneToStatesDict[zone][ext.SlimefallTier - 1]));
                    IL2CPP_zoneToPatternDict[zone].RunningTransitions[index].Transitions
                        .TryAdd(WeatherHelper.CreatePatternTransition(ext.StartChancePerHour, ext.StateDefinition, Array.Empty<AbstractWeatherCondition>()));
                }
            }
            ext.StateTransitions.TryAdd(WeatherHelper.CreatePatternTransition(ext.StopChancePerHour, null, Array.Empty<AbstractWeatherCondition>()));

            foreach (ZoneDefinition zone in ext.ShowForZones)
            {
                if (IL2CPP_zoneToPatternDict.ContainsKey(zone))
                {
                    IL2CPP_zoneToPatternDict[zone].RunningTransitions.TryAdd(new WeatherPatternDefinition.TransitionList()
                    {
                        FromState = ext.StateDefinition,
                        Transitions = ext.StateTransitions
                    });
                }
            }
        }
    }
}
