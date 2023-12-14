using Il2CppMonomiPark.SlimeRancher.Weather.Activity;
using MoreSlimefall.Assist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreSlimefall.Data.Weathers
{
    internal class SlimeRainNormalStrand
    {
        public static void Load(string sceneName)
        {
            switch (sceneName)
            {
                case "GameCore":
                    {
                        var toArray = LocalWeathers.slimeRainStateStrand.Activities.ToArray();
                        LocalWeathers.slimeRainStateStrand.Activities.Remove(toArray.FirstOrDefault(x => x?.Activity == Get<SpawnActorActivity>("Rain Pink Slimes")));
                        LocalWeathers.slimeRainStateStrand.Activities.Remove(toArray.FirstOrDefault(x => x?.Activity == Get<SpawnActorActivity>("Rain Angler Slimes")));
                        LocalWeathers.slimeRainStateStrand.Activities.Remove(toArray.FirstOrDefault(x => x?.Activity == Get<SpawnActorActivity>("Rain Honey Slimes")));

                        LocalWeathers.slimeRainStateStrand.Activities.TryAdd(WeatherHelper.CreateStateActivity(1, LocalSpawnActivities.globalRainPinkSlimes));
                        LocalWeathers.slimeRainStateStrand.Activities.TryAdd(WeatherHelper.CreateStateActivity(1, LocalSpawnActivities.strandRainRockSlimes));
                        break;
                    }
            }
        }
    }
}
