using Il2CppMonomiPark.SlimeRancher.Pedia;
using Il2CppMonomiPark.SlimeRancher.Weather;
using Il2CppMonomiPark.SlimeRancher.Weather.Activity;
using Il2CppMonomiPark.SlimeRancher.Weather.Conditions;
using Il2CppMonomiPark.SlimeRancher.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Localization;

namespace MoreSlimefall.Assist
{
    public static class WeatherHelper
    {
        internal static HashSet<WeatherStateDefinition> weatherStatesToPatch = new HashSet<WeatherStateDefinition>();

        public static void RegisterWeatherState(WeatherStateDefinition weatherState)
        {
            weatherStatesToPatch.TryAdd(weatherState);
            Get<WeatherStateCollection>("All Weather States")?.items?.TryAdd(weatherState);
        }

        public static WeatherStateDefinition.ActivityIntensityMapping CreateStateActivity(AbstractActivity activity, float intensity)
        {
            return new WeatherStateDefinition.ActivityIntensityMapping()
            {
                Activity = activity,
                Intensity = intensity
            };
        }

        public static WeatherPatternDefinition.Transition CreatePatternTransition(WeatherStateDefinition toState, float chancePerHour, AbstractWeatherCondition[] conditions)
        {
            return new WeatherPatternDefinition.Transition()
            {
                ToState = toState,
                Conditions = conditions,
                ChancePerHour = chancePerHour
            };
        }

        public static WeatherTypeMetadata CreateWeatherMetadata(Sprite icon, string name, PediaEntry pediaEntry)
        {
            WeatherTypeMetadata weatherMetadata = ScriptableObject.CreateInstance<WeatherTypeMetadata>();
            weatherMetadata.hideFlags |= HideFlags.HideAndDontSave;
            weatherMetadata.name = name;

            weatherMetadata.Icon = icon;
            weatherMetadata.PediaEntry = pediaEntry;
            weatherMetadata.AnalyticsName = name.Replace(" ", "").Replace("Metadata", "");
            return weatherMetadata;
        }

        public static WeatherStateDefinition CreateWeatherState(string name, Il2CppSystem.Collections.Generic.List<WeatherStateDefinition.ActivityIntensityMapping> activities, int mapTier = 0, float minDurationHours = 3)
        {
            WeatherStateDefinition weatherStateDefinition = ScriptableObject.CreateInstance<WeatherStateDefinition>();
            weatherStateDefinition.hideFlags |= HideFlags.HideAndDontSave;
            weatherStateDefinition.name = name;

            weatherStateDefinition.Guid = "WeatherStateDefinition." + name.Replace(" ", "");
            weatherStateDefinition.StateName = name.Replace("State", "");

            weatherStateDefinition.MapTier = mapTier;
            weatherStateDefinition.Activities = activities;
            weatherStateDefinition.MinDurationHours = minDurationHours;

            RegisterWeatherState(weatherStateDefinition);
            return weatherStateDefinition;
        }
    }
}
