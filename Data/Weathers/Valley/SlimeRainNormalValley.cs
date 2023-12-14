using Il2CppMonomiPark.SlimeRancher.Weather.Activity;
using Il2CppSystem;
using MoreSlimefall.Assist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreSlimefall.Data.Weathers
{
    internal class SlimeRainNormalValley
    {
        public static void Load(string sceneName)
        {
            switch (sceneName)
            {
                case "GameCore":
                    {
                        var toArray = LocalWeathers.slimeRainStateValley.Activities.ToArray();
                        LocalWeathers.slimeRainStateValley.Activities.Remove(toArray.FirstOrDefault(x => x?.Activity == Get<SpawnActorActivity>("Rain Pink Slimes")));
                        LocalWeathers.slimeRainStateValley.Activities.Remove(toArray.FirstOrDefault(x => x?.Activity == Get<SpawnActorActivity>("Rain Tabby Slimes")));
                        LocalWeathers.slimeRainStateValley.Activities.Remove(toArray.FirstOrDefault(x => x?.Activity == Get<SpawnActorActivity>("Rain Rock Slimes")));

                        LocalWeathers.slimeRainStateValley.Activities.TryAdd(WeatherHelper.CreateStateActivity(1, LocalSpawnActivities.globalRainPinkSlimes));
                        LocalWeathers.slimeRainStateValley.Activities.TryAdd(WeatherHelper.CreateStateActivity(1, LocalSpawnActivities.valleyRainTabbySlimes));
                        break;
                    }
            }
        }
    }
}
