using Il2CppMonomiPark.ScriptedValue;
using Il2CppMonomiPark.SlimeRancher.Weather;
using Il2CppMonomiPark.SlimeRancher.Weather.Conditions;
using MoreSlimefall.Assist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreSlimefall.Data.Weathers
{
    internal class SlimeRainModerateValley
    {
        internal static WeatherStateDefinition slimeRainModerateValley;
        internal static Il2CppSystem.Collections.Generic.List<WeatherStateDefinition.ActivityIntensityMapping> moderateValleyActivities = new();

        internal static float toNoneChance = 0.5f;
        internal static float toPreviousChance = 0.5f;
        internal static float toModerateChance = 0.005f;
        internal static float toSevereChance = 0.001f;

        public static void Initialize()
        {
            slimeRainModerateValley = WeatherHelper.CreateWeatherState("Slime Rain Moderate State Valley", moderateValleyActivities, 2, 2);
            slimeRainModerateValley.StateName = "Slime Rain Moderate";
        }

        public static void Load(string sceneName)
        {
            switch (sceneName)
            {
                case "GameCore":
                    {
                        CreateActivities();
                        CreateTransitions();

                        WeatherHelper.RegisterWeatherState(slimeRainModerateValley);
                        break;
                    }
            }
        }

        private static void CreateActivities()
        {
            foreach (var activity in LocalWeathers.slimeRainStateValley.Activities)
                moderateValleyActivities.TryAdd(activity);

            moderateValleyActivities.TryAdd(WeatherHelper.CreateStateActivity(LocalSpawnActivities.valleyRainCrystalSlimes, 1));
            moderateValleyActivities.TryAdd(WeatherHelper.CreateStateActivity(LocalSpawnActivities.valleyRainBoomSlimes, 1));
            moderateValleyActivities.TryAdd(WeatherHelper.CreateStateActivity(LocalSpawnActivities.valleyRainBattySlimes, 1));
            moderateValleyActivities.TryAdd(WeatherHelper.CreateStateActivity(LocalSpawnActivities.globalRainPhosphorSlimes, 1));
        }

        private static void CreateTransitions()
        {
            Il2CppSystem.Collections.Generic.List<WeatherPatternDefinition.Transition> transitions = new();
            transitions.TryAdd(WeatherHelper.CreatePatternTransition(null, toNoneChance, Array.Empty<AbstractWeatherCondition>()));
            transitions.TryAdd(WeatherHelper.CreatePatternTransition(LocalWeathers.slimeRainStateValley, toPreviousChance, Array.Empty<AbstractWeatherCondition>()));
            transitions.TryAdd(WeatherHelper.CreatePatternTransition(SlimeRainSevereValley.slimeRainSevereValley, toSevereChance, Array.Empty<AbstractWeatherCondition>()));

            LocalWeathers.slimeRainPatternValley.RunningTransitions[0].Transitions.TryAdd(WeatherHelper.CreatePatternTransition(slimeRainModerateValley, toModerateChance, Array.Empty<AbstractWeatherCondition>()));
            LocalWeathers.slimeRainPatternValley.RunningTransitions.TryAdd(new WeatherPatternDefinition.TransitionList()
            {
                FromState = slimeRainModerateValley,
                Transitions = transitions
            });
        }
    }
}
