using HarmonyLib;
using Il2CppMonomiPark.SlimeRancher.Pedia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Localization;

namespace MoreSlimefall.Assist
{
    internal static class PediaHelper
    {
        internal static HashSet<PediaEntry> pediasToPatch = new HashSet<PediaEntry>();

        public static void RegisterPediaEntry(PediaEntry pediaEntry)
        {
            if (!pediasToPatch.Contains(pediaEntry))
                pediasToPatch.Add(pediaEntry);
        }

        public static FixedPediaEntry CreateFixedEntry(Sprite icon, string name, string persistenceSuffix, PediaHighlightSet highlightSet,
            LocalizedString title, LocalizedString intro, PediaEntryDetail[] entryDetails, bool isUnlockedInitially = false)
        {
            if (Get<FixedPediaEntry>(name))
                return null;

            FixedPediaEntry fixedPediaEntry = ScriptableObject.CreateInstance<FixedPediaEntry>();
            fixedPediaEntry.hideFlags |= HideFlags.HideAndDontSave;
            fixedPediaEntry.name = name;

            fixedPediaEntry._icon = icon;
            fixedPediaEntry._title = title;
            fixedPediaEntry._description = intro;
            fixedPediaEntry._persistenceSuffix = persistenceSuffix;

            fixedPediaEntry._details = entryDetails;
            fixedPediaEntry._highlightSet = highlightSet;
            // fixedPediaEntry._unlockInfoProvider = SceneContext.Instance.PediaDirector.Cast<IUnlockInfoProvider>();
            fixedPediaEntry._isUnlockedInitially = isUnlockedInitially;

            RegisterPediaEntry(fixedPediaEntry);
            return fixedPediaEntry;
        }

        public static void AddPediaToCategory(PediaEntry pediaEntry, PediaCategory pediaCategory)
        {
            if (!pediaCategory)
                return;

            LookupDirector director = SRSingleton<GameContext>.Instance.LookupDirector;
            if (!director._categories[director._categories.IndexOf(pediaCategory.GetRuntimeCategory())].Contains(pediaEntry))
                director.AddPediaEntryToCategory(pediaEntry, pediaCategory);
        }
    }
}
