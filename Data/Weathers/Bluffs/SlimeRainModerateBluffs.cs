﻿using Il2CppMonomiPark.SlimeRancher.Weather.Conditions;
using Il2CppMonomiPark.SlimeRancher.Weather;
using MoreSlimefall.Assist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreSlimefall.Data.Weathers
{
    internal class SlimeRainModerateBluffs
    {
        internal static WeatherStateDefinition slimeRainModerateBluffs;
        internal static Il2CppSystem.Collections.Generic.List<WeatherStateDefinition.ActivityIntensityMapping> moderateBluffsActivities = new();

        internal static float toNoneChance = 0.5f;
        internal static float toPreviousChance = 0.5f;
        internal static float toModerateChance = 0.005f;
        internal static float toSevereChance = 0.001f;

        public static void Initialize()
        {
            slimeRainModerateBluffs = WeatherHelper.CreateWeatherState("Slime Rain Moderate State Bluffs", moderateBluffsActivities, 2, 2);
            slimeRainModerateBluffs.StateName = "Slime Rain Moderate";
        }

        public static void Load(string sceneName)
        {
            switch (sceneName)
            {
                case "GameCore":
                    {
                        CreateActivities();
                        CreateTransitions();

                        WeatherHelper.RegisterWeatherState(slimeRainModerateBluffs);
                        break;
                    }
            }
        }

        private static void CreateActivities()
        {
            foreach (var activity in LocalWeathers.slimeRainStateBluffs.Activities)
                moderateBluffsActivities.TryAdd(activity);

            moderateBluffsActivities.TryAdd(WeatherHelper.CreateStateActivity(1, LocalSpawnActivities.bluffsRainRockSlimes));
            moderateBluffsActivities.TryAdd(WeatherHelper.CreateStateActivity(1, LocalSpawnActivities.bluffsRainSaberSlimes));
            moderateBluffsActivities.TryAdd(WeatherHelper.CreateStateActivity(1, LocalSpawnActivities.globalRainPhosphorSlimes));
        }

        private static void CreateTransitions()
        {
            Il2CppSystem.Collections.Generic.List<WeatherPatternDefinition.Transition> transitions = new();
            transitions.TryAdd(WeatherHelper.CreatePatternTransition(toNoneChance, null, Array.Empty<AbstractWeatherCondition>()));
            transitions.TryAdd(WeatherHelper.CreatePatternTransition(toPreviousChance, LocalWeathers.slimeRainStateBluffs, Array.Empty<AbstractWeatherCondition>()));
            transitions.TryAdd(WeatherHelper.CreatePatternTransition(toSevereChance, SlimeRainSevereBluffs.slimeRainSevereBluffs, Array.Empty<AbstractWeatherCondition>()));

            LocalWeathers.slimeRainPatternBluffs.RunningTransitions[0].Transitions.TryAdd(WeatherHelper.CreatePatternTransition(toModerateChance, slimeRainModerateBluffs, Array.Empty<AbstractWeatherCondition>()));
            LocalWeathers.slimeRainPatternBluffs.RunningTransitions.TryAdd(new WeatherPatternDefinition.TransitionList()
            {
                FromState = slimeRainModerateBluffs,
                Transitions = transitions
            });
        }
    }
}
