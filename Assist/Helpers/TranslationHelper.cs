using MoreSlimefall.Harmony;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Localization;

namespace MoreSlimefall.Assist
{
    internal static class TranslationHelper
    {
        public static LocalizedString CreateTranslation(string table, string key, string localized) => 
            LocalizationDirectorLoadTablesPatch.AddTranslation(table, key, localized);
    }
}
