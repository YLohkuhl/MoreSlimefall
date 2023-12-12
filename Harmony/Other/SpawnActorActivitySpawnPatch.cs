using HarmonyLib;
using Il2CppMonomiPark.SlimeRancher.Weather.Activity;
using MoreSlimefall.Assist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelonLoader;
using Il2CppMonomiPark.SlimeRancher.Weather;

namespace MoreSlimefall.Harmony.Other
{
    [HarmonyPatch(typeof(SpawnActorActivity), nameof(SpawnActorActivity.Spawn))]
    internal class SpawnActorActivitySpawnPatch
    {
        /*internal static Dictionary<bool, float> goldLuckyWeights = new()
        {
            { false, 1 },
            { true, 0.0025f },
        };*/


        public static void Postfix(SpawnActorActivity __instance)
        {
            if (__instance == LocalSpawnActivities.globalRainRareSlimes)
                if (__instance.ActorType)
                    __instance.ActorType = null;
        }

        public static bool Prefix(SpawnActorActivity __instance)
        {
            if (LocalSpawnActivities._nighttimeSpawnActivities.FirstOrDefault(x => x == __instance))
                if (!IsNighttime())
                    return false;

            if (__instance == LocalSpawnActivities.globalRainPuddleSlimes)
                if (!IsHeavyRain())
                    return false;

            if (__instance == LocalSpawnActivities.globalRainTangleSlimes)
                if (!IsHighPollen())
                    return false;

            if (__instance == LocalSpawnActivities.globalRainDervishSlimes)
                if (!IsHighWind())
                    return false;

            if (__instance == LocalSpawnActivities.globalRainRareSlimes)
            {
                var picked = URandoms.PickWeight(LocalSpawnWeights._weightedRareFallingActors, null);
                if (picked.IsNotNull() || !picked.Equals(LocalSpawnActivities.globalRainPinkSlimes))
                    __instance.ActorType = picked;
                else
                    return false;
            }

            return true;
        }

        public static bool IsNighttime()
        {
            float currHour = SceneContext.Instance.TimeDirector.CurrHour();
            return 6 < 18 && (currHour <= 6 || currHour >= 18);
        }

        public static bool IsHeavyRain()
        {
            var weatherDirector = UnityEngine.Object.FindObjectOfType<WeatherDirector>();
            return weatherDirector._runningStates.ToArray().FirstOrDefault(x => x?.ToWeatherState() == LocalWeathers.rainHeavyState, null).IsNotNull();
        }

        public static bool IsHighWind()
        {
            var weatherDirector = UnityEngine.Object.FindObjectOfType<WeatherDirector>();
            return weatherDirector._runningStates.ToArray().FirstOrDefault(x => x?.ToWeatherState() == LocalWeathers.windHeavyState, null).IsNotNull();
        }

        public static bool IsHighPollen()
        {
            var weatherDirector = UnityEngine.Object.FindObjectOfType<WeatherDirector>();
            return weatherDirector._runningStates.ToArray().FirstOrDefault(x => x?.ToWeatherState() == LocalWeathers.pollenHeavyState, null).IsNotNull();
        }
    }
}
