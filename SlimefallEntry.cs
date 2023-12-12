global using Il2Cpp;
global using UnityEngine;
global using static Utility;
using MelonLoader;
using MoreSlimefall;
using MoreSlimefall.Data.Weather;
using MoreSlimefall.Assist;
using Il2CppMonomiPark.SlimeRancher;
using MoreSlimefall.Components;
using MoreSlimefall.Data;
using MoreSlimefall.Assist.Extension;

[assembly: MelonInfo(typeof(SlimefallEntry), "More Slimefall", "1.0.0", "FruitsyOG")]
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
        }

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            // ExtensionHelper.LoadAllExtensions(MelonAssembly.Assembly, sceneName);
            /*if (sceneName.Equals("UICore"))
                ChevronHelper.AddWeatherChevron(LoadHex("#e63737"), LoadHex("#803636"));*/

            if (sceneName.Contains("zone") && sceneName.Contains("Weather"))
            {
                var weatherVFX = UnityEngine.Object.FindObjectOfType<WeatherVFXControl>();
                if (weatherVFX)
                    weatherVFX._slimeRainControl?.gameObject?.AddComponent<SlimeRainOutbreakVFXControl>();
            }

            // -- OTHER
            LocalAssets.Load(sceneName);
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

            // -- LATE OTHER
            LocalDictionaries.Load(sceneName);

            // ExtensionHelper.LoadExtension(new ExtensionTest(), sceneName);
        }
    }
}