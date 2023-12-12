using Il2CppMonomiPark.SlimeRancher.Weather;
using Il2CppMonomiPark.SlimeRancher.Weather.Activity;
using MoreSlimefall.Assist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreSlimefall.Data.Weather
{
    internal class SlimeRainNormalFields
    {
        public static void Load(string sceneName)
        {
            switch (sceneName)
            {
                case "GameCore":
                    {
                        var toArray = LocalWeathers.slimeRainStateFields.Activities.ToArray();
                        LocalWeathers.slimeRainStateFields.Activities.Remove(toArray.FirstOrDefault(x => x?.Activity == Get<SpawnActorActivity>("Rain Pink Slimes")));

                        LocalWeathers.slimeRainStateFields.Activities.TryAdd(WeatherHelper.CreateStateActivity(1, LocalSpawnActivities.globalRainPinkSlimes));
                        LocalWeathers.slimeRainStateFields.Activities.TryAdd(WeatherHelper.CreateStateActivity(1, LocalSpawnActivities.fieldsRainCottonSlimes));
                        break;
                    }
            }
        }
    }
}
