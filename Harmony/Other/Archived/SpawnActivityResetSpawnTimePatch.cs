using HarmonyLib;
using Il2CppMonomiPark.SlimeRancher.Weather.Activity;
using MoreSlimefall.Assist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreSlimefall.Harmony.Other
{
    [HarmonyPatch(typeof(SpawnActivity), nameof(SpawnActivity.ResetSpawnTime))]
    internal class SpawnActivityResetSpawnTimePatch
    {
        public static void Postfix(SpawnActivity __instance)
        {
            if (__instance == LocalSpawnActivities.globalRainRareSlimes)
                if (__instance.SecondsBetweenSpawns.IsNotNull())
                    __instance.SecondsBetweenSpawns = null;
        }

        public static bool Prefix(SpawnActivity __instance)
        {
            if (__instance == LocalSpawnActivities.globalRainRareSlimes)
            {
                var picked = URandoms.PickWeight(LocalSpawnWeights._weightedRareFallingActors, (null, null));
                if (picked.Item1)
                    __instance.SecondsBetweenSpawns = picked.Item2;
                else
                    return false;
            }
            return true;
        }
    }
}
