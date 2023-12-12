using Il2CppMonomiPark.SlimeRancher.Weather.Conditions;
using Il2CppMonomiPark.SlimeRancher.Weather;
using MoreSlimefall.Assist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreSlimefall.Data.Weather
{
    internal class SlimeRainSevereStrand
    {
        internal static WeatherStateDefinition slimeRainSevereStrand;
        internal static Il2CppSystem.Collections.Generic.List<WeatherStateDefinition.ActivityIntensityMapping> severeStrandActivities = new();

        internal static float toNoneChance = 0.5f;
        internal static float toPreviousChance = 0.5f;
        internal static float toOutbreakChance = 0.003f;

        public static void Initialize()
        {
            slimeRainSevereStrand = WeatherHelper.CreateWeatherState("Slime Rain Severe State Strand", severeStrandActivities, 3, 1);
            slimeRainSevereStrand.StateName = "Slime Rain Severe";
        }

        public static void Load(string sceneName)
        {
            switch (sceneName)
            {
                case "GameCore":
                    {
                        CreateActivities();
                        CreateTransitions();

                        WeatherHelper.RegisterWeatherState(slimeRainSevereStrand);
                        break;
                    }
            }
        }

        private static void CreateActivities()
        {
            foreach (var activity in SlimeRainModerateStrand.slimeRainModerateStrand.Activities)
                severeStrandActivities.TryAdd(activity);

            severeStrandActivities.TryAdd(WeatherHelper.CreateStateActivity(1, LocalSpawnActivities.strandRainHoneySlimes));
            severeStrandActivities.TryAdd(WeatherHelper.CreateStateActivity(1, LocalSpawnActivities.strandRainFlutterSlimes));
            severeStrandActivities.TryAdd(WeatherHelper.CreateStateActivity(1, LocalSpawnActivities.strandRainRingtailSlimes));
            severeStrandActivities.TryAdd(WeatherHelper.CreateStateActivity(1, LocalSpawnActivities.strandRainHunterSlimes));

            foreach (var globalActivity in LocalSpawnActivities._globalSpawnActivities)
            {
                if (severeStrandActivities.ToArray().FirstOrDefault(x => x?.Activity == globalActivity).IsNotNull())
                    continue;
                severeStrandActivities.TryAdd(WeatherHelper.CreateStateActivity(1, globalActivity));
            }
        }

        private static void CreateTransitions()
        {
            Il2CppSystem.Collections.Generic.List<WeatherPatternDefinition.Transition> transitions = new();
            transitions.TryAdd(WeatherHelper.CreatePatternTransition(toNoneChance, null, Array.Empty<AbstractWeatherCondition>()));
            transitions.TryAdd(WeatherHelper.CreatePatternTransition(toPreviousChance, SlimeRainModerateStrand.slimeRainModerateStrand, Array.Empty<AbstractWeatherCondition>()));
            transitions.TryAdd(WeatherHelper.CreatePatternTransition(toOutbreakChance, SlimeRainOutbreak.slimeRainOutbreak, Array.Empty<AbstractWeatherCondition>()));

            LocalWeathers.slimeRainPatternStrand.RunningTransitions.TryAdd(new WeatherPatternDefinition.TransitionList()
            {
                FromState = slimeRainSevereStrand,
                Transitions = transitions
            });
        }
    }
}
