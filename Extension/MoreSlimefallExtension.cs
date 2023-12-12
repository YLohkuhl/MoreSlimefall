using Il2CppInterop.Runtime.InteropTypes;
using Il2CppMonomiPark.SlimeRancher.Weather;
using Il2CppMonomiPark.SlimeRancher.World;
using MelonLoader;
using MoreSlimefall.Assist.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreSlimefall.Extension
{
    /// <summary>
    /// 
    /// Inherit <see cref="MoreSlimefallExtension"/> into another class to create an extension to the <see cref="MoreSlimefall"/> mod.
    /// 
    /// <code>
    /// 
    /// Use <see cref="ExtensionHelper.LoadExtension"/> in your <see cref="MelonMod.OnSceneWasLoaded"/> on the "GameCore" scene to load the extension.<br></br><br></br>
    /// 
    /// <see cref="ExtensionHelper.LoadAllExtensions"/> is also optional to load ALL extensions within the <see cref="MelonBase.MelonAssembly"/>.
    /// This includes every class that inherits <see cref="MoreSlimefallExtension"/>.<br></br><br></br>
    /// 
    /// <b>DO NOT</b> load the extension in the <see cref="OnGameCore"/> method or any of the abstract methods. It won't work and could potentially cause other issues.
    /// 
    /// </code>
    /// </summary>
    public abstract class MoreSlimefallExtension
    {
        public Il2CppSystem.Collections.Generic.List<WeatherPatternDefinition.Transition> StateTransitions = new();

        public Il2CppSystem.Collections.Generic.List<WeatherStateDefinition.ActivityIntensityMapping> StateActivities = new();

        /// <summary>
        /// The <see cref="WeatherStateDefinition"/> of this <see cref="MoreSlimefallExtension"/>."/>
        /// </summary>
        public WeatherStateDefinition StateDefinition { get; set; }

        /// <summary>
        /// The <see cref="string"/> state name of this <see cref="StateDefinition"/>. (Typically without " State" at the end)
        /// </summary>
        public string StateName { get; set; }

        /// <summary>
        /// The <see cref="string"/> name of this <see cref="StateDefinition"/>. (Typically with " State" at the end)
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// The <see cref="int"/> tier of which this <see cref="MoreSlimefallExtension"/> can occur.
        /// </summary>
        public abstract int SlimefallTier { get; }

        /// <summary>
        /// The <see cref="float"/> duration of which this <see cref="MoreSlimefallExtension"/> can last for.
        /// </summary>
        public abstract float MinDurationHours { get; }

        /// <summary>
        /// The <see cref="float"/> chance of this <see cref="MoreSlimefallExtension"/> stopping after <see cref="MinDurationHours"/> has surpassed.
        /// </summary>
        public abstract float StopChancePerHour { get; }

        /// <summary>
        /// The <see cref="float"/> chance of this <see cref="MoreSlimefallExtension"/> starting once the <see cref="SlimefallTier"/> has started.
        /// </summary>
        public abstract float StartChancePerHour { get; }

        /// <summary>
        /// The <see cref="ZoneDefinition"/> zones of this <see cref="MoreSlimefallExtension"/> to appear in. Use the <see cref="WeatherZones"/> presets to specify.
        /// </summary>
        public abstract ZoneDefinition[] ShowForZones { get; }

        /// <summary>
        /// The initil
        /// </summary>
        public abstract void OnInitialize();

        /// <summary>
        /// Loaded on "SystemCore". Mostly won't need this but could be used for very early things.
        /// </summary>
        public abstract void OnSystemCore();

        /// <summary>
        /// Loaded on "GameCore". Where most of everything should be created, feel free to use separate methods and call them here.
        /// </summary>
        public abstract void OnGameCore();

        /// <summary>
        /// Loaded on "zoneCore". Loads everytime a save is loaded, usually only used for specific situations.
        /// </summary>
        public abstract void OnZoneCore();
    }
}
