using Il2CppMonomiPark.SlimeRancher.Weather.Conditions;
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
                case "PlayerCore":
                    {
                        HandleOutbreakTransition();
                        break;
                    }
            }
        }

        private static void CreateActivities()
        {
            foreach (var activity in SlimeRainModerateStrand.slimeRainModerateStrand.Activities)
                severeStrandActivities.TryAdd(activity);

            severeStrandActivities.TryAdd(WeatherHelper.CreateStateActivity(LocalSpawnActivities.strandRainHoneySlimes, 1));
            severeStrandActivities.TryAdd(WeatherHelper.CreateStateActivity(LocalSpawnActivities.strandRainFlutterSlimes, 1));
            severeStrandActivities.TryAdd(WeatherHelper.CreateStateActivity(LocalSpawnActivities.strandRainRingtailSlimes, 1));
            severeStrandActivities.TryAdd(WeatherHelper.CreateStateActivity(LocalSpawnActivities.strandRainHunterSlimes, 1));

            foreach (var globalActivity in LocalSpawnActivities._globalSpawnActivities)
            {
                if (severeStrandActivities.ToArray().FirstOrDefault(x => x?.Activity == globalActivity).IsNotNull())
                    continue;
                severeStrandActivities.TryAdd(WeatherHelper.CreateStateActivity(globalActivity, 1));
            }
        }

        private static void CreateTransitions()
        {
            Il2CppSystem.Collections.Generic.List<WeatherPatternDefinition.Transition> transitions = new();
            transitions.TryAdd(WeatherHelper.CreatePatternTransition(null, toNoneChance, Array.Empty<AbstractWeatherCondition>()));
            transitions.TryAdd(WeatherHelper.CreatePatternTransition(SlimeRainModerateStrand.slimeRainModerateStrand, toPreviousChance, Array.Empty<AbstractWeatherCondition>()));

            LocalWeathers.slimeRainPatternStrand.RunningTransitions.TryAdd(new WeatherPatternDefinition.TransitionList()
            {
                FromState = slimeRainSevereStrand,
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
                        if (transitionList?.FromState == slimeRainSevereStrand)
                        {
                            if (transitionList.Transitions.ToArray().FirstOrDefault(x => x?.ToState == SlimeRainOutbreak.slimeRainOutbreak).IsNull())
                                transitionList.Transitions.TryAdd(WeatherHelper.CreatePatternTransition(SlimeRainOutbreak.slimeRainOutbreak, toOutbreakChance, Array.Empty<AbstractWeatherCondition>()));
                        }
                    }
                }
            }
        }
    }
}
