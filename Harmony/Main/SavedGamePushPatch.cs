using Il2CppMonomiPark.SlimeRancher.DataModel;
using Il2CppMonomiPark.SlimeRancher.Weather;
using Il2CppMonomiPark.SlimeRancher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using static MoreSlimefall.Assist.WeatherHelper;
using static MoreSlimefall.Assist.PediaHelper;

namespace MoreSlimefall.Harmony
{
    [HarmonyPatch(typeof(SavedGame), nameof(SavedGame.Push), typeof(GameModel))]
    internal static class SavedGamePatch
    {
        public static void Prefix(SavedGame __instance)
        {
            foreach (var pediaEntry in pediasToPatch)
                if (pediaEntry)
                    __instance.pediaEntryLookup.TryAdd(pediaEntry.PersistenceId, pediaEntry);

            var stateTranslation = __instance._weatherStateTranslation;
            foreach (var weatherState in weatherStatesToPatch)
            {
                if (weatherState)
                {
                    stateTranslation.RawLookupDictionary.TryAdd(weatherState.Guid, weatherState.TryCast<IWeatherState>());
                    stateTranslation.ReverseLookupTable._indexTable = stateTranslation.ReverseLookupTable._indexTable.ToArray().TryAdd(weatherState.Guid);

                    stateTranslation.InstanceLookupTable._primaryIndex = stateTranslation.InstanceLookupTable._primaryIndex.ToArray().TryAdd(weatherState.Guid);
                    stateTranslation.InstanceLookupTable._reverseIndex.TryAdd(weatherState.Guid, stateTranslation.InstanceLookupTable._primaryIndex.Length - 1);
                }
            }
        }
    }
}
