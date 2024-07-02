using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreSlimefall.Assist
{
    internal class LocalAssets
    {
        // TEXTURE2D
        internal static Texture2D iconWeatherTarrain;

        // SPRITE
        internal static Sprite iconWeatherTarrainSpr;

        public static void PatchLoad()
        {
            iconWeatherTarrain = AB.more_slimefall.LoadAsset("iconWeatherTarrain").Cast<Texture2D>();
            iconWeatherTarrainSpr = iconWeatherTarrain.ToSprite();
        }
    }
}
