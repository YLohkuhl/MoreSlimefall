using Il2CppMonomiPark.SlimeRancher.World;
using MelonLoader;
using MoreSlimefall.Assist.Extension;
using MoreSlimefall.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreSlimefall.Examples
{
    // Inherit the extension class.
    // Let it add all the abstract methods, fields, etc with potential fixes or whatever is preferred.
    // Feel free to delete all of these comments afterwards, makes cleaner code but use them if you need.
    internal class SlimefallExtensionExample : MoreSlimefallExtension
    {
        // The name of your extension WeatherStateDefinition in-game, only really seen code-wise.
        public override string Name => "Slime Rain Extension State";


        // The tier of which this can occur on. Min is 1, Max is 3. Will error if you go outside of these.
        public override int SlimefallTier => 3;


        // Minimum duration hours, usually it's 3 in-game hours but this can be lowered or made higher.
        public override float MinDurationHours => 3;


        // The chance of it stopping the state "per hour", this may also depend on the minimum duration hours but yeah.
        public override float StopChancePerHour => 0.5f;


        // The chance of it starting the state "per hour",
        // this is connected to the tier that this state can occur on and it will determine the transition with this chance.
        public override float StartChancePerHour => 0.001f;


        // The zones of which this weather can naturally appear in. Use WeatherZones for preset zone definitions.
        public override ZoneDefinition[] ShowForZones => new ZoneDefinition[]
        {
            WeatherZones.RAINBOW_FIELDS,
        };


        // I've never really been able to use this but it happens very early.
        public override void OnSystemCore()
        {
            MelonLogger.Msg(GetType().Name + " Ran OnSystemCore!!");
        }

        // Generally where everything should be created, act as if it's using loading stuff on GameCore in a regular mod.
        public override void OnGameCore()
        {
            MelonLogger.Msg(GetType().Name + " Ran OnGameCore!!");
        }

        // Stuff that happens when a save is loaded and basically when the game starts loading, can have some usages if you need to do stuff in that area of things.
        public override void OnZoneCore()
        {
            MelonLogger.Msg(GetType().Name + " Ran OnZoneCore!!");
        }
    }
}
