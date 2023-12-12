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
        internal static HashSet<FixedPediaEntry> weatherPediasToPatch = new HashSet<FixedPediaEntry>();
        internal static HashSet<WeatherStateDefinition> weatherStatesToPatch = new HashSet<WeatherStateDefinition>();

        public static void RegisterWeatherState(WeatherStateDefinition weatherState)
        {
            weatherStatesToPatch.TryAdd(weatherState);
            Get<WeatherStateCollection>("All Weather States")?.items?.TryAdd(weatherState);
        }

        public static WeatherStateDefinition.ActivityIntensityMapping CreateStateActivity(float intensity, AbstractActivity activity)
        {
            return new WeatherStateDefinition.ActivityIntensityMapping()
            {
                Activity = activity,
                Intensity = intensity
            };
        }

        public static WeatherPatternDefinition.Transition CreatePatternTransition(float chancePerHour, WeatherStateDefinition toState, AbstractWeatherCondition[] conditions)
        {
            return new WeatherPatternDefinition.Transition()
            {
                ToState = toState,
                Conditions = conditions,
                ChancePerHour = chancePerHour
            };
        }

        public static WeatherTypeMetadata CreateWeatherMetadata(string metadataName, Sprite weatherIcon, PediaEntry weatherPediaEntry)
        {
            WeatherTypeMetadata weatherMetadata = ScriptableObject.CreateInstance<WeatherTypeMetadata>();
            weatherMetadata.hideFlags |= HideFlags.HideAndDontSave;
            weatherMetadata.name = metadataName;
            weatherMetadata.AnalyticsName = metadataName.Replace(" ", "").Replace("Metadata", "");
            weatherMetadata.Icon = weatherIcon;
            weatherMetadata.PediaEntry = weatherPediaEntry;
            return weatherMetadata;
        }

        public static WeatherStateDefinition CreateWeatherState(string stateName, Il2CppSystem.Collections.Generic.List<WeatherStateDefinition.ActivityIntensityMapping> stateActivities, int stateTier = 0, float minDurationHours = 3)
        {
            WeatherStateDefinition weatherStateDefinition = ScriptableObject.CreateInstance<WeatherStateDefinition>();
            weatherStateDefinition.hideFlags |= HideFlags.HideAndDontSave;
            weatherStateDefinition.name = stateName;
            weatherStateDefinition.Guid = "WeatherStateDefinition." + stateName.Replace(" ", "");
            weatherStateDefinition.StateName = stateName.Replace(" State", "");

            weatherStateDefinition.MapTier = stateTier;
            weatherStateDefinition.Activities = stateActivities;
            weatherStateDefinition.MinDurationHours = minDurationHours;

            RegisterWeatherState(weatherStateDefinition);
            return weatherStateDefinition;
        }

        public static FixedPediaEntry AddWeatherPedia(string pediaEntryName, Sprite pediaIcon, string pediaTitle, string pediaIntro, string pediaDescription)
        {
            string textId = pediaEntryName.ToLower().Replace(" ", "_");

            LocalizedString title = TranslationHelper.CreateTranslation("Pedia", "t." + textId, pediaTitle);
            LocalizedString intro = TranslationHelper.CreateTranslation("Pedia", "m.intro." + textId, pediaIntro);
            LocalizedString description = TranslationHelper.CreateTranslation("PediaPage", "m.desc." + textId, pediaDescription);

            PediaEntry.PediaPagesEntry[] pediaPagesEntries = new PediaEntry.PediaPagesEntry[]
            {
                new PediaEntry.PediaPagesEntry()
                {
                    PediaPage = Get<PediaPage>("Description"),
                    Text = description,
                    TextGamepad = description,
                    TextPS4 = description
                }
            };

            FixedPediaEntry fixedPediaEntry = PediaHelper.CreateFixedEntry(pediaEntryName, textId, pediaIcon, Get<PediaTemplate>("DescriptionPediaTemplate"), title, intro, pediaPagesEntries);
            weatherPediasToPatch.TryAdd(fixedPediaEntry);
            PediaHelper.RegisterPediaEntry(fixedPediaEntry);

            return fixedPediaEntry;
        }
    }
}
