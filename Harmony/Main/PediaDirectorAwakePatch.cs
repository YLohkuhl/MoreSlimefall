using HarmonyLib;
using Il2CppMonomiPark.SlimeRancher.Pedia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static MoreSlimefall.Assist.PediaHelper;

namespace MoreSlimefall.Harmony
{
    [HarmonyPatch(typeof(PediaDirector), nameof(PediaDirector.Awake))]
    internal static class PediaDirectorAwakePatch
    {
        public static void Prefix(PediaDirector __instance)
        {
            foreach (var pediaEntry in pediasToPatch)
            {
                if (!pediaEntry)
                    continue;

                pediaEntry._unlockInfoProvider = __instance.Cast<IUnlockInfoProvider>();
                if (pediaEntry._isUnlockedInitially)
                    __instance._initUnlocked = __instance._initUnlocked.AddItem(pediaEntry).ToArray();
            }
        }
    }
}
