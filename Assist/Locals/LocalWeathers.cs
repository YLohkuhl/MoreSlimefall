using Il2CppMonomiPark.SlimeRancher.Weather;
using MoreSlimefall.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreSlimefall.Assist
{
    /// <summary>
    /// Preset <see cref="WeatherStateDefinition"/> and <see cref="WeatherPatternDefinition"/> that the <see cref="MoreSlimefall"/> mod uses.<br></br>
    /// Feel free to use them for any of your needs, call on <see cref="MoreSlimefallExtension.OnGameCore"/> or afterwards otherwise they could be null.
    /// </summary>
    public static class LocalWeathers
    {
        #region STATES
        public static WeatherStateDefinition rainLightState 
        { 
            get 
            { 
                return Get<WeatherStateDefinition>("Rain Light State"); 
            } 
        }

        public static WeatherStateDefinition rainMedState 
        { 
            get 
            { 
                return Get<WeatherStateDefinition>("Rain Med State"); 
            } 
        
        }
        public static WeatherStateDefinition rainHeavyState 
        { 
            get 
            { 
                return Get<WeatherStateDefinition>("Rain Heavy State"); 
            } 
        }

        public static WeatherStateDefinition windHeavyState 
        { 
            get 
            { 
                return Get<WeatherStateDefinition>("Wind Heavy State"); 
            } 
        }

        public static WeatherStateDefinition pollenHeavyState 
        { 
            get 
            { 
                return Get<WeatherStateDefinition>("Pollen Heavy State"); 
            } 
        }

        public static WeatherStateDefinition slimeRainStateFields
        {
            get
            {
                return Get<WeatherStateDefinition>("Slime Rain State Fields");
            }
        }

        public static WeatherStateDefinition slimeRainStateStrand
        {
            get
            {
                return Get<WeatherStateDefinition>("Slime Rain State Strand");
            }
        }

        public static WeatherStateDefinition slimeRainStateValley
        {
            get
            {
                return Get<WeatherStateDefinition>("Slime Rain State Valley");
            }
        }

        public static WeatherStateDefinition slimeRainStateBluffs
        {
            get
            {
                return Get<WeatherStateDefinition>("Slime Rain State Bluffs");
            }
        }
        #endregion

        #region PATTERNS
        public static WeatherPatternDefinition slimeRainPatternFields
        {
            get
            {
                return Get<WeatherPatternDefinition>("Slime Rain Pattern Fields");
            }
        }

        public static WeatherPatternDefinition slimeRainPatternStrand
        {
            get
            {
                return Get<WeatherPatternDefinition>("Slime Rain Pattern Strand");
            }
        }

        public static WeatherPatternDefinition slimeRainPatternValley
        {
            get
            {
                return Get<WeatherPatternDefinition>("Slime Rain Pattern Valley");
            }
        }

        public static WeatherPatternDefinition slimeRainPatternBluffs
        {
            get
            {
                return Get<WeatherPatternDefinition>("Slime Rain Pattern Bluffs");
            }
        }
        #endregion

        internal static void Load(string sceneName)
        {
            switch (sceneName)
            {
                case "GameCore":
                    {
                        slimeRainStateFields.MapTier = 1;
                        slimeRainStateStrand.MapTier = 1;
                        slimeRainStateValley.MapTier = 1;
                        slimeRainStateBluffs.MapTier = 1;
                        break;
                    }
            }
        }
    }
}
