using HarmonyLib;
using MelonLoader;
using MoreSlimefall.Assist;
using MoreSlimefall.Data.Weathers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreSlimefall.Harmony.Main
{
    [HarmonyPatch(typeof(LookupDirector), nameof(LookupDirector.Awake))]
    internal static class LookupDirectorAwakePatch
    {
        public static void Prefix()
        {
            LocalAssets.PatchLoad();
            SlimeRainOutbreak.CreatePedia();
        }
    }
}
