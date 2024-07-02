﻿using Il2CppMonomiPark.SlimeRancher.Weather.Conditions;
using Il2CppMonomiPark.SlimeRancher.Weather;
using MoreSlimefall.Assist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Il2CppMonomiPark.ScriptedValue;

namespace MoreSlimefall.Data.Weathers
{
    internal class SlimeRainSevereValley
    {
        internal static WeatherStateDefinition slimeRainSevereValley;
        internal static Il2CppSystem.Collections.Generic.List<WeatherStateDefinition.ActivityIntensityMapping> severeValleyActivities = new();

        internal static float toNoneChance = 0.5f;
        internal static float toPreviousChance = 0.5f;
        internal static float toOutbreakChance = 0.003f;

        public static void Initialize()
        {
            slimeRainSevereValley = WeatherHelper.CreateWeatherState("Slime Rain Severe State Valley", severeValleyActivities, 3, 1);
            slimeRainSevereValley.StateName = "Slime Rain Severe";
        }

        public static void Load(string sceneName)
        {
            switch (sceneName)
            {
                case "GameCore":
                    {
                        CreateActivities();
                        CreateTransitions();

                        WeatherHelper.RegisterWeatherState(slimeRainSevereValley);
                        break;
                    }
                case "PlayerCore":
                    {
                        HandleOutbreakTransition();
                        break;
                    }
            }
        }

        private static void CreateActivities()
        {
            foreach (var activity in SlimeRainModerateValley.slimeRainModerateValley.Activities)
                severeValleyActivities.TryAdd(activity);

            severeValleyActivities.TryAdd(WeatherHelper.CreateStateActivity(1, LocalSpawnActivities.valleyRainRockSlimes));
            severeValleyActivities.TryAdd(WeatherHelper.CreateStateActivity(1, LocalSpawnActivities.valleyRainAnglerSlimes));
            severeValleyActivities.TryAdd(WeatherHelper.CreateStateActivity(1, LocalSpawnActivities.valleyRainRingtailSlimes));
            severeValleyActivities.TryAdd(WeatherHelper.CreateStateActivity(1, LocalSpawnActivities.valleyRainFireSlimes));

            foreach (var globalActivity in LocalSpawnActivities._globalSpawnActivities)
            {
                if (severeValleyActivities.ToArray().FirstOrDefault(x => x?.Activity == globalActivity).IsNotNull())
                    continue;
                severeValleyActivities.TryAdd(WeatherHelper.CreateStateActivity(1, globalActivity));
            }
        }

        private static void CreateTransitions()
        {
            Il2CppSystem.Collections.Generic.List<WeatherPatternDefinition.Transition> transitions = new();
            transitions.TryAdd(WeatherHelper.CreatePatternTransition(toNoneChance, null, Array.Empty<AbstractWeatherCondition>()));
            transitions.TryAdd(WeatherHelper.CreatePatternTransition(toPreviousChance, SlimeRainModerateValley.slimeRainModerateValley, Array.Empty<AbstractWeatherCondition>()));

            LocalWeathers.slimeRainPatternValley.RunningTransitions.TryAdd(new WeatherPatternDefinition.TransitionList()
            {
                FromState = slimeRainSevereValley,
                Transitions = transitions
            });
        }

        private static void HandleOutbreakTransition()
        {
            ScriptedBool tarrEnabled = Get<ScriptedBool>("TarrEnabled");
            if (tarrEnabled.Value)
            {
                foreach (var keyValuePair in LocalDictionaries.IL2CPP_zoneToPatternDict)
                {
                    var pattern = keyValuePair.Value;
                    foreach (var transitionList in pattern?.RunningTransitions)
                    {
                        if (transitionList?.FromState == slimeRainSevereValley)
                        {
                            if (transitionList.Transitions.ToArray().FirstOrDefault(x => x?.ToState == SlimeRainOutbreak.slimeRainOutbreak).IsNull())
                                transitionList.Transitions.TryAdd(WeatherHelper.CreatePatternTransition(toOutbreakChance, SlimeRainOutbreak.slimeRainOutbreak, Array.Empty<AbstractWeatherCondition>()));
                        }
                    }
                }
            }
        }
    }
}
