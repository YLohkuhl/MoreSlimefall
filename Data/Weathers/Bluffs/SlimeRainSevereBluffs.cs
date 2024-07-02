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
    internal class SlimeRainSevereBluffs
    {
        internal static WeatherStateDefinition slimeRainSevereBluffs;
        internal static Il2CppSystem.Collections.Generic.List<WeatherStateDefinition.ActivityIntensityMapping> severeBluffsActivities = new();

        internal static float toNoneChance = 0.5f;
        internal static float toPreviousChance = 0.5f;
        internal static float toOutbreakChance = 0.002f;

        public static void Initialize()
        {
            slimeRainSevereBluffs = WeatherHelper.CreateWeatherState("Slime Rain Severe State Bluffs", severeBluffsActivities, 3, 1);
            slimeRainSevereBluffs.StateName = "Slime Rain Severe";
        }

        public static void Load(string sceneName)
        {
            switch (sceneName)
            {
                case "GameCore":
                    {
                        CreateActivities();
                        CreateTransitions();

                        WeatherHelper.RegisterWeatherState(slimeRainSevereBluffs);
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
            foreach (var activity in SlimeRainModerateBluffs.slimeRainModerateBluffs.Activities)
                severeBluffsActivities.TryAdd(activity);

            severeBluffsActivities.TryAdd(WeatherHelper.CreateStateActivity(LocalSpawnActivities.bluffsRainHunterSlimes, 1));
            severeBluffsActivities.TryAdd(WeatherHelper.CreateStateActivity(LocalSpawnActivities.bluffsRainCrystalSlimes, 1));
            severeBluffsActivities.TryAdd(WeatherHelper.CreateStateActivity(LocalSpawnActivities.bluffsRainBoomSlimes, 1));

            foreach (var globalActivity in LocalSpawnActivities._globalSpawnActivities)
            {
                if (severeBluffsActivities.ToArray().FirstOrDefault(x => x?.Activity == globalActivity).IsNotNull())
                    continue;
                if (globalActivity == LocalSpawnActivities.globalRainPuddleSlimes || globalActivity == LocalSpawnActivities.globalRainTangleSlimes)
                    continue;
                severeBluffsActivities.TryAdd(WeatherHelper.CreateStateActivity(globalActivity, 1));
            }
        }

        private static void CreateTransitions()
        {
            Il2CppSystem.Collections.Generic.List<WeatherPatternDefinition.Transition> transitions = new();
            transitions.TryAdd(WeatherHelper.CreatePatternTransition(null, toNoneChance, Array.Empty<AbstractWeatherCondition>()));
            transitions.TryAdd(WeatherHelper.CreatePatternTransition(SlimeRainModerateBluffs.slimeRainModerateBluffs, toPreviousChance, Array.Empty<AbstractWeatherCondition>()));

            LocalWeathers.slimeRainPatternBluffs.RunningTransitions.TryAdd(new WeatherPatternDefinition.TransitionList()
            {
                FromState = slimeRainSevereBluffs,
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
                        if (transitionList?.FromState == slimeRainSevereBluffs)
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
