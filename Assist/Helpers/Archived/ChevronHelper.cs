using Il2CppMonomiPark.SlimeRancher.UI.Map;
using Il2CppMonomiPark.SlimeRancher.UI.SDFUI;
using Il2CppMonomiPark.SlimeRancher.UI.Weather;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreSlimefall.Assist
{
    internal class ChevronHelper
    {
        // Will be useful for anything future that adds more chevrons or anything like that
        public static WeatherIndicatorConfig.LevelStyle AddWeatherChevron(Color fillColor, Color borderColor)
        {
            WeatherIndicatorConfig indicatorConfig = Get<WeatherIndicatorConfig>("Weather Indicator Config");
            WeatherIndicatorConfig.LevelStyle levelStyle = new WeatherIndicatorConfig.LevelStyle()
            {
                FillColor = fillColor,
                BorderColor = borderColor
            };
            indicatorConfig._levels = indicatorConfig._levels.ToArray().TryAdd(levelStyle);

            MapUI mapUI = Get<GameObject>("MapUI").GetComponent<MapUI>();
            foreach (WeatherIndicator indicator in mapUI._zoomedInUI._weatherIndicators)
            {
                GameObject chevron = UnityEngine.Object.Instantiate(indicator.transform.Find("Chevrons/Chevron").gameObject);
                chevron.transform.name = "Chevron";
                chevron.transform.SetParent(indicator.transform.Find("Chevrons"), false);
                indicator._levels = indicator._levels.ToArray().TryAdd(chevron.GetComponent<SDFChevron>());
            }
            return levelStyle;
        }
    }
}
