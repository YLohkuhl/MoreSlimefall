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
    internal class SlimeRainModerateStrand
    {
        internal static WeatherStateDefinition slimeRainModerateStrand;
        internal static Il2CppSystem.Collections.Generic.List<WeatherStateDefinition.ActivityIntensityMapping> moderateStrandActivities = new();

        internal static float toNoneChance = 0.5f;
        internal static float toPreviousChance = 0.5f;
        internal static float toModerateChance = 0.005f;
        internal static float toSevereChance = 0.001f;

        public static void Initialize()
        {
            slimeRainModerateStrand = WeatherHelper.CreateWeatherState("Slime Rain Moderate State Strand", moderateStrandActivities, 2, 2);
            slimeRainModerateStrand.StateName = "Slime Rain Moderate";
        }

        public static void Load(string sceneName)
        {
            switch (sceneName)
            {
                case "GameCore":
                    {
                        CreateActivities();
                        CreateTransitions();

                        WeatherHelper.RegisterWeatherState(slimeRainModerateStrand);
                        break;
                    }
            }
        }

        private static void CreateActivities()
        {
            foreach (var activity in LocalWeathers.slimeRainStateStrand.Activities)
                moderateStrandActivities.TryAdd(activity);

            moderateStrandActivities.TryAdd(WeatherHelper.CreateStateActivity(1, LocalSpawnActivities.strandRainCottonSlimes));
            moderateStrandActivities.TryAdd(WeatherHelper.CreateStateActivity(1, LocalSpawnActivities.strandRainAnglerSlimes));
            moderateStrandActivities.TryAdd(WeatherHelper.CreateStateActivity(1, LocalSpawnActivities.strandRainHoneySlimes));
            moderateStrandActivities.TryAdd(WeatherHelper.CreateStateActivity(1, LocalSpawnActivities.globalRainPhosphorSlimes));
        }

        private static void CreateTransitions()
        {
            Il2CppSystem.Collections.Generic.List<WeatherPatternDefinition.Transition> transitions = new();
            transitions.TryAdd(WeatherHelper.CreatePatternTransition(toNoneChance, null, Array.Empty<AbstractWeatherCondition>()));
            transitions.TryAdd(WeatherHelper.CreatePatternTransition(toPreviousChance, LocalWeathers.slimeRainStateStrand, Array.Empty<AbstractWeatherCondition>()));
            transitions.TryAdd(WeatherHelper.CreatePatternTransition(toSevereChance, SlimeRainSevereStrand.slimeRainSevereStrand, Array.Empty<AbstractWeatherCondition>()));

            LocalWeathers.slimeRainPatternStrand.RunningTransitions[0].Transitions.TryAdd(WeatherHelper.CreatePatternTransition(toModerateChance, slimeRainModerateStrand, Array.Empty<AbstractWeatherCondition>()));
            LocalWeathers.slimeRainPatternStrand.RunningTransitions.TryAdd(new WeatherPatternDefinition.TransitionList() 
            {
                FromState = slimeRainModerateStrand,
                Transitions = transitions
            });
        }
    }
}
