using MoreSlimefall.Assist;
using Il2CppMonomiPark.SlimeRancher.Weather.Conditions;
using Il2CppMonomiPark.SlimeRancher.Weather;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Il2CppMonomiPark.ScriptedValue;

namespace MoreSlimefall.Data.Weathers
{
    internal class SlimeRainSevereFields
    {
        internal static WeatherStateDefinition slimeRainSevereFields;
        internal static Il2CppSystem.Collections.Generic.List<WeatherStateDefinition.ActivityIntensityMapping> severeFieldsActivities = new();

        internal static float toNoneChance = 0.5f;
        internal static float toPreviousChance = 0.5f;
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
                case "PlayerCore":
                    {
                        HandleOutbreakTransition();
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
                severeFieldsActivities.TryAdd(WeatherHelper.CreateStateActivity(globalActivity, 1));
            }
        }

        private static void CreateTransitions()
        {
            Il2CppSystem.Collections.Generic.List<WeatherPatternDefinition.Transition> transitions = new();
            transitions.TryAdd(WeatherHelper.CreatePatternTransition(null, toNoneChance, Array.Empty<AbstractWeatherCondition>()));
            transitions.TryAdd(WeatherHelper.CreatePatternTransition(SlimeRainModerateFields.slimeRainModerateFields, toPreviousChance, Array.Empty<AbstractWeatherCondition>()));

            LocalWeathers.slimeRainPatternFields.RunningTransitions.TryAdd(new WeatherPatternDefinition.TransitionList()
            {
                FromState = slimeRainSevereFields,
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
                        if (transitionList?.FromState == slimeRainSevereFields)
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
