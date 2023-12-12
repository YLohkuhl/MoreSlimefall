using HarmonyLib;
using Il2CppMonomiPark.SlimeRancher.Weather;
using Il2CppMonomiPark.SlimeRancher.World;
using MoreSlimefall.Assist;
using MoreSlimefall.Data.Weather;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class Utility
{
    public static class PrefabUtils
    {
        public static Transform DisabledParent;

        static PrefabUtils()
        {
            DisabledParent = new GameObject("DeactivedObject").transform;
            DisabledParent.gameObject.SetActive(false);
            UnityEngine.Object.DontDestroyOnLoad(DisabledParent.gameObject);
            DisabledParent.gameObject.hideFlags |= HideFlags.HideAndDontSave;
        }

        public static GameObject CopyPrefab(GameObject prefab)
        {
            var newG = UnityEngine.Object.Instantiate(prefab, DisabledParent);
            return newG;
        }
    }

    public static T Get<T>(string name) where T : UnityEngine.Object => Resources.FindObjectsOfTypeAll<T>().FirstOrDefault(found => found.name.Equals(name));

    public static Color LoadHex(string hexCode)
    {
        if (!hexCode.Contains("#"))
            hexCode = "#" + hexCode;
        ColorUtility.TryParseHtmlString(hexCode, out var returnedColor);
        return returnedColor;
    }

    public static class URandoms
    {
        public static float GetFloat(float high)
        {
            return (float)(new System.Random().NextDouble() * (double)high);
        }

        public static T PickWeight<T>(Dictionary<T, float> weightMap, T ifEmpty)
        {
            T result = ifEmpty;
            float num = 0f;
            foreach (KeyValuePair<T, float> keyValuePair in weightMap)
            {
                float value = keyValuePair.Value;
                if ((double)value > 0.0)
                {
                    num += value;
                    if (num == value || GetFloat(num) < value)
                    {
                        result = keyValuePair.Key;
                    }
                }
                else if ((double)value < 0.0)
                {
                    throw new ArgumentException("weightMap", "Weight less than 0: " + keyValuePair);
                }
            }
            return result;
        }
    }
}

internal static class WeatherTest
{
    public static void RunState(ZoneDefinition zone, WeatherPatternDefinition weatherPattern, WeatherStateDefinition weatherState)
    {
        SceneContext.Instance.WeatherRegistry.RunPatternState(zone, new WeatherPattern(weatherPattern).TryCast<IWeatherPattern>(), weatherState?.TryCast<IWeatherState>());
    }

    public static void Outbreak()
    {
        RunState(Get<ZoneDefinition>("RainbowFields"), LocalWeathers.slimeRainPatternFields, SlimeRainOutbreak.slimeRainOutbreak);
    }
}

internal static class WeatherExtensions
{
    public static WeatherStateDefinition ToWeatherState(this IWeatherState iState) => iState?.TryCast<WeatherStateDefinition>();

    public static WeatherPatternDefinition ToWeatherPattern(this IWeatherPattern iPattern) => iPattern?.TryCast<WeatherPatternDefinition>();
}

internal static class Extensions
{
    public static bool IsNull(this object obj) => obj == null;

    public static bool IsNotNull(this object obj) => !obj.IsNull();

    public static T[] TryAdd<T>(this T[] array, T item)
    {
        if (!array.Contains(item))
            return array.AddToArray(item);
        return array;
    }

    public static void TryAdd<T>(this ICollection<T> collection, T item)
    {
        if (!collection.Contains(item))
            collection.Add(item);
    }

    public static void TryAdd<T>(this Il2CppSystem.Collections.Generic.List<T> list, T item)
    {
        if (!list.Contains(item))
            list.Add(item);
    }

    public static Sprite ToSprite(this Texture2D texture)
    {
        Sprite sprite = Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 1f);
        sprite.hideFlags = texture.hideFlags;
        sprite.name = texture.name;
        return sprite;
    }
}