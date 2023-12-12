using Il2CppMonomiPark.SlimeRancher.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoreSlimefall.Extension;

namespace MoreSlimefall.Assist.Extension
{
    /// <summary>
    /// Preset zones for specifying in <see cref="MoreSlimefallExtension.ShowForZones"/>.
    /// </summary>
    public static class WeatherZones
    {
        public static ZoneDefinition RAINBOW_FIELDS { get { return Get<ZoneDefinition>("RainbowFields"); } }

        public static ZoneDefinition STARLIGHT_STRAND { get { return Get<ZoneDefinition>("Luminous Strand"); } }

        public static ZoneDefinition EMBER_VALLEY { get { return Get<ZoneDefinition>("Rumbling Gorge"); } }

        public static ZoneDefinition POWDERFALL_BLUFFS { get { return Get<ZoneDefinition>("Powderfall Bluffs"); } }
    }
}
