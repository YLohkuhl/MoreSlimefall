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

            moderateValleyActivities.TryAdd(WeatherHelper.CreateStateActivity(1, LocalSpawnActivities.valleyRainCrystalSlimes));
            moderateValleyActivities.TryAdd(WeatherHelper.CreateStateActivity(1, LocalSpawnActivities.valleyRainBoomSlimes));
            moderateValleyActivities.TryAdd(WeatherHelper.CreateStateActivity(1, LocalSpawnActivities.valleyRainBattySlimes));
            moderateValleyActivities.TryAdd(WeatherHelper.CreateStateActivity(1, LocalSpawnActivities.globalRainPhosphorSlimes));
        }

        private static void CreateTransitions()
        {
            Il2CppSystem.Collections.Generic.List<WeatherPatternDefinition.Transition> transitions = new();
            transitions.TryAdd(WeatherHelper.CreatePatternTransition(toNoneChance, null, Array.Empty<AbstractWeatherCondition>()));
            transitions.TryAdd(WeatherHelper.CreatePatternTransition(toPreviousChance, LocalWeathers.slimeRainStateValley, Array.Empty<AbstractWeatherCondition>()));
            transitions.TryAdd(WeatherHelper.CreatePatternTransition(toSevereChance, SlimeRainSevereValley.slimeRainSevereValley, Array.Empty<AbstractWeatherCondition>()));

            LocalWeathers.slimeRainPatternValley.RunningTransitions[0].Transitions.TryAdd(WeatherHelper.CreatePatternTransition(toModerateChance, slimeRainModerateValley, Array.Empty<AbstractWeatherCondition>()));
            LocalWeathers.slimeRainPatternValley.RunningTransitions.TryAdd(new WeatherPatternDefinition.TransitionList()
            {
                FromState = slimeRainModerateValley,
                Transitions = transitions
            });
        }
    }
}
