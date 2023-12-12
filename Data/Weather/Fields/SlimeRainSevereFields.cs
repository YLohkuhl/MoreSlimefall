using MoreSlimefall.Assist;
using Il2CppMonomiPark.SlimeRancher.Weather.Conditions;
using Il2CppMonomiPark.SlimeRancher.Weather;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreSlimefall.Data.Weather
{
    internal class SlimeRainSevereFields
    {
        internal static WeatherStateDefinition slimeRainSevereFields;
        internal static Il2CppSystem.Collections.Generic.List<WeatherStateDefinition.ActivityIntensityMapping> severeFieldsActivities = new();

        internal static float toNoneChance = 0; // 0.5f;
        internal static float toPreviousChance = 0; // 0.5f;
        internal static float toOutbreakChance = 0.001f;

        public static void Initialize()
        {
            slimeRainSevereFields = WeatherHelper.CreateWeatherState("Slime Rain Severe State Fields", severeFieldsActivities, 3, 1);
            slimeRainSevereFields.StateName = "Slime Rain Severe";
        }

        public static void Load(string sceneName)
        {
            switch (sceneName)
            {
                case "GameCore":
                    {
                        CreateActivities();
                        CreateTransitions();

                        WeatherHelper.RegisterWeatherState(slimeRainSevereFields);
                        break;
                    }
            }
        }

        private static void CreateActivities()
        {
            foreach (var activity in SlimeRainModerateFields.slimeRainModerateFields.Activities)
                severeFieldsActivities.TryAdd(activity);

            foreach (var globalActivity in LocalSpawnActivities._globalSpawnActivities)
            {
                if (severeFieldsActivities.ToArray().FirstOrDefault(x => x?.Activity == globalActivity).IsNotNull())
                    continue;
                severeFieldsActivities.TryAdd(WeatherHelper.CreateStateActivity(1, globalActivity));
            }
        }

        private static void CreateTransitions()
        {
            Il2CppSystem.Collections.Generic.List<WeatherPatternDefinition.Transition> transitions = new();
            transitions.TryAdd(WeatherHelper.CreatePatternTransition(toNoneChance, null, Array.Empty<AbstractWeatherCondition>()));
            transitions.TryAdd(WeatherHelper.CreatePatternTransition(toPreviousChance, SlimeRainModerateFields.slimeRainModerateFields, Array.Empty<AbstractWeatherCondition>()));
            transitions.TryAdd(WeatherHelper.CreatePatternTransition(toOutbreakChance, SlimeRainOutbreak.slimeRainOutbreak, Array.Empty<AbstractWeatherCondition>()));

            LocalWeathers.slimeRainPatternFields.RunningTransitions.TryAdd(new WeatherPatternDefinition.TransitionList()
            {
                FromState = slimeRainSevereFields,
                Transitions = transitions
            });
        }
    }
}
