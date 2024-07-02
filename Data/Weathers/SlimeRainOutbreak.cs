using Il2CppMonomiPark.SlimeRancher.Weather.Conditions;
using Il2CppMonomiPark.SlimeRancher.Weather;
using MoreSlimefall.Assist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Il2CppMonomiPark.SlimeRancher.Weather.Activity;
using Il2CppMonomiPark.SlimeRancher;
using Il2CppMonomiPark.SlimeRancher.Pedia;
using UnityEngine.Localization;
using UnityEngine.VFX;
using Il2CppMonomiPark.ScriptedValue;
using MelonLoader;

namespace MoreSlimefall.Data.Weathers
{
    internal class SlimeRainOutbreak
    {
        internal static CurrentWeatherStates currentRainLightStates;
        internal static CurrentWeatherStates currentRainMedStates;
        internal static CurrentWeatherStates currentRainHeavyStates;

        internal static GameObject slimeRainOutbreakFallFX;
        internal static FixedPediaEntry outbreakEntry;
        internal static WeatherTypeMetadata outbreakMetadata;
        internal static UnlockPediaActivity unlockOutbreakPedia;
        internal static WeatherStateDefinition slimeRainOutbreak;
        internal static readonly Il2CppSystem.Collections.Generic.List<WeatherStateDefinition.ActivityIntensityMapping> outbreakActivities = new();

        internal static Color outbreakColor = new Color32(39, 44, 56, 255);
        // internal static Color outbreakTintColor = new Color32(20, 23, 28, 255);

        internal static float toNoneChance = 0.7f;
        // internal static float toPreviousChance = 0.5f;

        public static void Initialize()
        {
            // MAIN
            slimeRainOutbreak = WeatherHelper.CreateWeatherState("Slime Rain Outbreak State", outbreakActivities, 3, 0.5f);
            slimeRainOutbreak.StateName = "Slime Rain Outbreak";

            outbreakMetadata = WeatherHelper.CreateWeatherMetadata("Outbreak Metadata", null, null);

            // CURRENT WEATHER STATES
            currentRainLightStates = ScriptableObject.CreateInstance<CurrentWeatherStates>();
            currentRainLightStates.hideFlags |= HideFlags.HideAndDontSave;
            currentRainLightStates.name = "Current Weather States Light Rain";

            currentRainMedStates = ScriptableObject.CreateInstance<CurrentWeatherStates>();
            currentRainMedStates.hideFlags |= HideFlags.HideAndDontSave;
            currentRainMedStates.name = "Current Weather States Med Rain";

            currentRainHeavyStates = ScriptableObject.CreateInstance<CurrentWeatherStates>();
            currentRainHeavyStates.hideFlags |= HideFlags.HideAndDontSave;
            currentRainHeavyStates.name = "Current Weather States Heavy Rain";
        }

        public static void Load(string sceneName)
        {
            switch (sceneName)
            {
                case "GameCore":
                    {
                        currentRainLightStates.State = LocalWeathers.rainLightState;
                        currentRainMedStates.State = LocalWeathers.rainMedState;
                        currentRainHeavyStates.State = LocalWeathers.rainHeavyState;

                        CreateRainFallFX();
                        CreateActivities();
                        CreateTransitions();

                        WeatherHelper.RegisterWeatherState(slimeRainOutbreak);
                        break;
                    }
                case "PlayerCore":
                    {
                        HandleOutbreakTransitions();
                        break;
                    }
            }
        }

        internal static void CreatePedia()
        {
            outbreakEntry = PediaHelper.CreateFixedEntry(LocalAssets.iconWeatherTarrainSpr, "Outbreak", "outbreak", Get<PediaHighlightSet>("TutorialPediaTemplate"),
                TranslationHelper.CreateTranslation("Pedia", "t.outbreak", "Slime Outbreak"),
                TranslationHelper.CreateTranslation("Pedia", "m.intro.outbreak", "You've got to be kidding me..."),
                [
                    new PediaEntryDetail()
                    {
                        Section = Get<PediaDetailSection>("About"),
                        Text = TranslationHelper.CreateTranslation("PediaPage", "m.desc.outbreak",
                            "A Tarr outbreak from above, the rare but deadly phenomenon of when the falling of cute slimes is disrupted in the clouds and starts to materialize a batch of ravenous rainbows. " +
                            "When slimefall is at an unusually high level, various slimes will fall, and there is the potential occurrence of a large, unstoppable outbreak of Tarr in the sky. " +
                            "It's best to take cover or dare you fight back..."
                        ),
                        TextGamepad = new(),
                        TextPS4 = new()
                    }
                ]
            );

            PediaHelper.AddPediaToCategory(outbreakEntry, Get<PediaCategory>("Weather"));

            outbreakMetadata.PediaEntry = outbreakEntry;
        }

        private static void CreateRainFallFX()
        {
            slimeRainOutbreakFallFX = PrefabUtils.CopyPrefab(Get<GameObject>("SlimeRainFallFX"));
            slimeRainOutbreakFallFX.hideFlags |= HideFlags.HideAndDontSave;
            slimeRainOutbreakFallFX.name = "SlimeRainOutbreakFallFX";

            ParticleSystem puffFXSystem = slimeRainOutbreakFallFX.transform.Find("FX Puff").gameObject.GetComponent<ParticleSystem>();
            puffFXSystem.startColor = outbreakColor;

            ParticleSystemRenderer puffFXRenderer = puffFXSystem.gameObject.GetComponent<ParticleSystemRenderer>();
            // puffFXRenderer.material = UnityEngine.Object.Instantiate(puffFXRenderer.material);
            puffFXRenderer.material.color = outbreakColor;
            puffFXRenderer.material.SetColor("_Emission", outbreakColor);

            LocalSpawnActivities.outbreakRainTarrSlimes.ConnectedFX = slimeRainOutbreakFallFX;
        }

        private static void CreateActivities()
        {
            var baseUnlockPedia = Get<UnlockPediaActivity>("Unlock Slime Rain Pedia");
            unlockOutbreakPedia = ScriptableObject.CreateInstance<UnlockPediaActivity>();
            unlockOutbreakPedia.name = "Unlock Slime Rain Outbreak Pedia";

            unlockOutbreakPedia.Metadata = outbreakMetadata;
            unlockOutbreakPedia.WeatherVFXType = baseUnlockPedia.WeatherVFXType;
            unlockOutbreakPedia.VisualIntensityThreshold = baseUnlockPedia.VisualIntensityThreshold;

            outbreakActivities.TryAdd(WeatherHelper.CreateStateActivity(1, LocalSpawnActivities.outbreakRainTarrSlimes));
            outbreakActivities.TryAdd(WeatherHelper.CreateStateActivity(1, Get<RegionalVFXActivity>("Run Slime Rain VFX")));
            outbreakActivities.TryAdd(WeatherHelper.CreateStateActivity(1, Get<UnlockPediaActivity>("Unlock Slime Rain Pedia")));
            outbreakActivities.TryAdd(WeatherHelper.CreateStateActivity(1, unlockOutbreakPedia));
        }

        private static void CreateTransitions()
        {
            Il2CppSystem.Collections.Generic.List<WeatherPatternDefinition.Transition> transitions = new();
            transitions.TryAdd(WeatherHelper.CreatePatternTransition(toNoneChance, null, Array.Empty<AbstractWeatherCondition>()));
            transitions.TryAdd(WeatherHelper.CreatePatternTransition(1, null,
            [
                currentRainLightStates,
                currentRainMedStates,
                currentRainHeavyStates
            ]));

            WeatherPatternDefinition.TransitionList transitionList = new()
            {
                FromState = slimeRainOutbreak,
                Transitions = transitions
            };

            LocalWeathers.slimeRainPatternFields.RunningTransitions.TryAdd(transitionList);
            LocalWeathers.slimeRainPatternStrand.RunningTransitions.TryAdd(transitionList);
            LocalWeathers.slimeRainPatternValley.RunningTransitions.TryAdd(transitionList);
            LocalWeathers.slimeRainPatternBluffs.RunningTransitions.TryAdd(transitionList);
        }

        private static void HandleOutbreakTransitions()
        {
            ScriptedBool tarrEnabled = Get<ScriptedBool>("TarrEnabled");
            if (!tarrEnabled.Value)
            {
                foreach (var keyValuePair in LocalDictionaries.IL2CPP_zoneToPatternDict)
                {
                    var pattern = keyValuePair.Value;
                    foreach (var transitionList in pattern?.RunningTransitions)
                    {
                        List<WeatherPatternDefinition.Transition> copyOfTransitions = new(transitionList?.Transitions?.ToArray().ToList());
                        foreach (var transition in copyOfTransitions)
                        {
                            if (transition?.ToState == slimeRainOutbreak)
                                transitionList?.Transitions?.Remove(transition);
                        }
                    }
                }
            }
        }
    }
}
