using Il2CppMonomiPark.SlimeRancher.Weather;
using Il2CppMonomiPark.SlimeRancher.Weather.Activity;
using Il2CppMonomiPark.SlimeRancher.Weather.Conditions;
using MoreSlimefall.Assist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreSlimefall.Data.Weathers
{
    internal class SlimeRainModerateFields
    {
        internal static WeatherStateDefinition slimeRainModerateFields;
        internal static Il2CppSystem.Collections.Generic.List<WeatherStateDefinition.ActivityIntensityMapping> moderateFieldsActivities = new();

        internal static float toNoneChance = 0.5f;
        internal static float toPreviousChance = 0.5f;
        internal static float toModerateChance = 0.005f;
        internal static float toSevereChance = 0.001f;

        public static void Initialize()
        {
            slimeRainModerateFields = WeatherHelper.CreateWeatherState("Slime Rain Moderate State Fields", moderateFieldsActivities, 2, 2);
            slimeRainModerateFields.StateName = "Slime Rain Moderate";
        }

        public static void Load(string sceneName)
        {
            switch (sceneName)
            {
                case "GameCore":
                    {
                        CreateActivities();
                        CreateTransitions();

                        WeatherHelper.RegisterWeatherState(slimeRainModerateFields);
                        break;
                    }
            }
        }

        private static void CreateActivities()
        {
            foreach (var activity in LocalWeathers.slimeRainStateFields.Activities)
                moderateFieldsActivities.TryAdd(activity);

            moderateFieldsActivities.TryAdd(WeatherHelper.CreateStateActivity(LocalSpawnActivities.fieldsRainTabbySlimes, 1));
            moderateFieldsActivities.TryAdd(WeatherHelper.CreateStateActivity(LocalSpawnActivities.fieldsRainCottonSlimes, 1));
            moderateFieldsActivities.TryAdd(WeatherHelper.CreateStateActivity(LocalSpawnActivities.globalRainPhosphorSlimes, 1));
        }

        private static void CreateTransitions()
        {
            Il2CppSystem.Collections.Generic.List<WeatherPatternDefinition.Transition> transitions = new();
            transitions.TryAdd(WeatherHelper.CreatePatternTransition(null, toNoneChance, Array.Empty<AbstractWeatherCondition>()));
            transitions.TryAdd(WeatherHelper.CreatePatternTransition(LocalWeathers.slimeRainStateFields, toPreviousChance, Array.Empty<AbstractWeatherCondition>()));
            transitions.TryAdd(WeatherHelper.CreatePatternTransition(SlimeRainSevereFields.slimeRainSevereFields, toSevereChance, Array.Empty<AbstractWeatherCondition>()));

            LocalWeathers.slimeRainPatternFields.RunningTransitions[0].Transitions.TryAdd(WeatherHelper.CreatePatternTransition(slimeRainModerateFields, toModerateChance, Array.Empty<AbstractWeatherCondition>()));
            LocalWeathers.slimeRainPatternFields.RunningTransitions.TryAdd(new WeatherPatternDefinition.TransitionList() 
            {
                FromState = slimeRainModerateFields,
                Transitions = transitions
            });
        }
    }
}
