using Il2CppMonomiPark.SlimeRancher.Weather.Activity;
using MoreSlimefall.Assist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreSlimefall.Data.Weathers
{
    internal class SlimeRainNormalBluffs
    {
        public static void Load(string sceneName)
        {
            switch (sceneName)
            {
                case "GameCore":
                    {
                        var toArray = LocalWeathers.slimeRainStateBluffs.Activities.ToArray();
                        LocalWeathers.slimeRainStateBluffs.Activities.Remove(toArray.FirstOrDefault(x => x?.Activity == Get<SpawnActorActivity>("Rain Pink Slimes")));
                        LocalWeathers.slimeRainStateBluffs.Activities.Remove(toArray.FirstOrDefault(x => x?.Activity == Get<SpawnActorActivity>("Rain Cotton Slimes")));
                        LocalWeathers.slimeRainStateBluffs.Activities.Remove(toArray.FirstOrDefault(x => x?.Activity == Get<SpawnActorActivity>("Rain Saber Slimes")));

                        LocalWeathers.slimeRainStateBluffs.Activities.TryAdd(WeatherHelper.CreateStateActivity(1, LocalSpawnActivities.globalRainPinkSlimes));
                        LocalWeathers.slimeRainStateBluffs.Activities.TryAdd(WeatherHelper.CreateStateActivity(1, LocalSpawnActivities.bluffsRainCottonSlimes));
                        break;
                    }
            }
        }
    }
}
