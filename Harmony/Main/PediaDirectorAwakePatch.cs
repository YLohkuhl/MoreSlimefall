using HarmonyLib;
using Il2CppMonomiPark.SlimeRancher.Pedia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static MoreSlimefall.Assist.WeatherHelper;

namespace MoreSlimefall.Harmony
{
    [HarmonyPatch(typeof(PediaDirector), nameof(PediaDirector.Awake))]
    internal static class PediaDirectorAwakePatch
    {
        public static void Prefix(PediaDirector __instance)
        {
            PediaCategory weatherCategory = __instance._pediaConfiguration.Categories.ToArray().First(x => x.name == "Weather");
            foreach (var fixedPediaEntry in weatherPediasToPatch)
            {
                fixedPediaEntry._unlockInfoProvider = __instance.Cast<IUnlockInfoProvider>();
                if (!weatherCategory._items.FirstOrDefault(x => x == fixedPediaEntry))
                    weatherCategory._items = weatherCategory._items.ToArray().TryAdd(fixedPediaEntry);
            }
        }
    }
}
