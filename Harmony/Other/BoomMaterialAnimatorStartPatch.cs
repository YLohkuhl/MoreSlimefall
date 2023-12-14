using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreSlimefall.Harmony.Other
{
    [HarmonyPatch(typeof(BoomMaterialAnimator), nameof(BoomMaterialAnimator.Start))]
    internal class BoomMaterialAnimatorStartPatch
    {
        public static bool Prefix(BoomMaterialAnimator __instance)
        {
            if (!__instance.transform.Find("SlimeRainFallFX(Clone)"))
                return true;

            List<Material> list = new List<Material>();
            foreach (Renderer renderer in __instance.GetComponentsInChildren<Renderer>())
            {
                if (renderer.transform.parent.name.Contains("SlimeRainFallFX"))
                    continue;
                if (renderer.sharedMaterial.HasProperty("_CrackAmount"))
                    list.Add(renderer.material);
            }

            __instance._boomMaterials = list.ToArray();
            __instance._boomSlime = __instance.GetComponent<BoomMaterialAnimator.IBoomMaterialInformer>();
            __instance.Update();
            return false;
        }
    }
}
