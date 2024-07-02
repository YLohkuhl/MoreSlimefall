global using Il2Cpp;
global using UnityEngine;
global using static Utility;
using MelonLoader;
using MoreSlimefall;
using MoreSlimefall.Data.Weathers;
using MoreSlimefall.Assist;
using Il2CppMonomiPark.SlimeRancher;
using MoreSlimefall.Components;

[assembly: MelonInfo(typeof(SlimefallEntry), "More Slimefall", "1.0.3", "YLohkuhl", "https://www.nexusmods.com/slimerancher2/mods/72")]
[assembly: MelonGame("MonomiPark", "SlimeRancher2")]
[assembly: MelonColor(0, 84, 231, 222)]
namespace MoreSlimefall
{
    internal class SlimefallEntry : MelonMod
    {
        public override void OnInitializeMelon()
        {
            // -- SLIME RAIN STATES
            // -- OUTBREAK
            SlimeRainOutbreak.Initialize();

            // --- FIELDS
            SlimeRainModerateFields.Initialize();
            SlimeRainSevereFields.Initialize();

            // --- STRAND
            SlimeRainModerateStrand.Initialize();
            SlimeRainSevereStrand.Initialize();

            // -- VALLEY
            SlimeRainModerateValley.Initialize();
            SlimeRainSevereValley.Initialize();

            // -- BLUFFS
            SlimeRainModerateBluffs.Initialize();
            SlimeRainSevereBluffs.Initialize();
        }

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            /*if (sceneName.Equals("UICore"))
                ChevronHelper.AddWeatherChevron(LoadHex("#e63737"), LoadHex("#803636"));*/

            if (sceneName.Contains("zone") && sceneName.Contains("Weather"))
            {
                var weatherVFX = UnityEngine.Object.FindObjectOfType<WeatherVFXControl>();
                if (weatherVFX)
                    weatherVFX._slimeRainControl?.gameObject?.AddComponent<SlimeRainOutbreakVFXControl>();
            }

            // -- OTHER
            // LocalAssets.Load(sceneName);
            LocalWeathers.Load(sceneName);
            LocalSpawnActivities.Load(sceneName);
            LocalSpawnWeights.Load(sceneName);

            // -- SLIME RAIN STATES
            // -- OUTBREAK
            SlimeRainOutbreak.Load(sceneName);

            // --- FIELDS
            SlimeRainNormalFields.Load(sceneName);
            SlimeRainModerateFields.Load(sceneName);
            SlimeRainSevereFields.Load(sceneName);

            // --- STRAND
            SlimeRainNormalStrand.Load(sceneName);
            SlimeRainModerateStrand.Load(sceneName);
            SlimeRainSevereStrand.Load(sceneName);

            // -- VALLEY
            SlimeRainNormalValley.Load(sceneName);
            SlimeRainModerateValley.Load(sceneName);
            SlimeRainSevereValley.Load(sceneName);

            // -- BLUFFS
            SlimeRainNormalBluffs.Load(sceneName);
            SlimeRainModerateBluffs.Load(sceneName);
            SlimeRainSevereBluffs.Load(sceneName);

            // -- LATE OTHER
            LocalDictionaries.Load(sceneName);

            // ExtensionHelper.LoadExtension(new ExtensionTest(), sceneName);
        }
    }
}