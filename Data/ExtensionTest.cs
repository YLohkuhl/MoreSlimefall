using Il2CppMonomiPark.SlimeRancher.Pedia;
using Il2CppMonomiPark.SlimeRancher.Weather;
using Il2CppMonomiPark.SlimeRancher.Weather.Activity;
using Il2CppMonomiPark.SlimeRancher.World;
using MelonLoader;
using MoreSlimefall.Assist;
using MoreSlimefall.Assist.Extension;
using MoreSlimefall.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreSlimefall.Data
{
    internal class ExtensionTest : MoreSlimefallExtension
    {
        public override string Name => "Slime Rain Extension State";

        public override int SlimefallTier => 3;

        public override float MinDurationHours => 3;

        public override float StopChancePerHour => 0;

        public override float StartChancePerHour => 1;

        public override ZoneDefinition[] ShowForZones => new ZoneDefinition[]
        {
            WeatherZones.RAINBOW_FIELDS,
            WeatherZones.STARLIGHT_STRAND
        };

        public static FixedPediaEntry extensionTestEntry;
        public static UnlockPediaActivity unlockExtensionPedia;
        public static WeatherTypeMetadata extensionTestMetadata;

        public override void OnInitialize() => extensionTestMetadata = WeatherHelper.CreateWeatherMetadata("Extension", Get<Sprite>("iconSlimeFlutter"), null);

        public override void OnSystemCore() { }

        public override void OnGameCore()
        {
            var baseUnlockPedia = Get<UnlockPediaActivity>("Unlock Slime Rain Pedia");
            unlockExtensionPedia = ScriptableObject.CreateInstance<UnlockPediaActivity>();
            unlockExtensionPedia.name = "Unlock Slime Rain Extension Pedia";

            unlockExtensionPedia.Metadata = extensionTestMetadata;
            unlockExtensionPedia.WeatherVFXType = baseUnlockPedia.WeatherVFXType;
            unlockExtensionPedia.VisualIntensityThreshold = baseUnlockPedia.VisualIntensityThreshold;

            StateActivities.TryAdd(WeatherHelper.CreateStateActivity(1, Get<RegionalVFXActivity>("Run Slime Rain VFX")));
            StateActivities.TryAdd(WeatherHelper.CreateStateActivity(1, unlockExtensionPedia));
            StateActivities.TryAdd(WeatherHelper.CreateStateActivity(1, LocalSpawnActivities.strandRainFlutterSlimes));
            StateActivities.TryAdd(WeatherHelper.CreateStateActivity(1, LocalSpawnActivities.strandRainRockSlimes));
            StateActivities.TryAdd(WeatherHelper.CreateStateActivity(1, LocalSpawnActivities.strandRainAnglerSlimes));

            extensionTestEntry = WeatherHelper.AddWeatherPedia("Extension", extensionTestMetadata.Icon, "ooo flutter", "OoooOOo extension test", "I love more slimefall extensions oh I just LOVE IT aspdgodsajg");
            extensionTestMetadata.PediaEntry = extensionTestEntry;
        }

        public override void OnZoneCore() => MelonLogger.Msg("This is a test");
    }
}
