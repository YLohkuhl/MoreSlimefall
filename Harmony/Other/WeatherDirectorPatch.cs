using HarmonyLib;
using Il2CppMonomiPark.SlimeRancher;
using Il2CppMonomiPark.SlimeRancher.Weather;
using MelonLoader;
using MoreSlimefall.Components;
using MoreSlimefall.Data.Weathers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreSlimefall.Harmony.Other
{
    [HarmonyPatch(typeof(WeatherDirector))]
    internal class WeatherDirectorPatch
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(WeatherDirector.RunState))]
        public static void RunStatePrefix(WeatherDirector __instance, ref IWeatherState state)
        {
            if (!__instance)
                return;

            if (state?.ToWeatherState() == SlimeRainOutbreak.slimeRainOutbreak)
            {
                var slimeRainControl = __instance.GetComponent<WeatherVFXControl>()?._slimeRainControl;
                if (!slimeRainControl)
                    return;
                slimeRainControl.gameObject.GetComponent<SlimeRainOutbreakVFXControl>().SetSlimeRainOutbreak(true);
            }
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(WeatherDirector.StopState))]
        public static void StopStatePostfix(WeatherDirector __instance, ref IWeatherState state)
        {
            if (!__instance)
                return;

            if (state?.ToWeatherState() == SlimeRainOutbreak.slimeRainOutbreak)
            {
                var slimeRainControl = __instance.GetComponent<WeatherVFXControl>()?._slimeRainControl;
                if (!slimeRainControl)
                    return;
                slimeRainControl.gameObject.GetComponent<SlimeRainOutbreakVFXControl>().SetSlimeRainOutbreak(false);
            }
        }
    }
}
