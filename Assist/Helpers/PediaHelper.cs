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

        public static void RegisterPediaEntry(PediaEntry pediaEntry) => pediasToPatch.TryAdd(pediaEntry);

        public static FixedPediaEntry CreateFixedEntry(string pediaEntryName, string pediaTextId, Sprite pediaIcon, PediaTemplate pediaTemplate,
            LocalizedString pediaTitle, LocalizedString pediaIntro, PediaEntry.PediaPagesEntry[] pediaPageEntries, bool unlockedInitially = false)
        {
            if (Get<FixedPediaEntry>(pediaEntryName))
                return null;

            FixedPediaEntry fixedPediaEntry = ScriptableObject.CreateInstance<FixedPediaEntry>();

            fixedPediaEntry.hideFlags |= HideFlags.HideAndDontSave;
            fixedPediaEntry.name = pediaEntryName;
            fixedPediaEntry._title = pediaTitle;
            fixedPediaEntry._description = pediaIntro;

            fixedPediaEntry._icon = pediaIcon;
            fixedPediaEntry._textId = pediaTextId;
            fixedPediaEntry._template = pediaTemplate;
            fixedPediaEntry._pageEntries = pediaPageEntries;
            fixedPediaEntry._unlockInfoProvider = SceneContext.Instance?.PediaDirector?.Cast<IUnlockInfoProvider>();
            fixedPediaEntry._isUnlockedInitially = unlockedInitially;

            return fixedPediaEntry;
        }
    }
}
