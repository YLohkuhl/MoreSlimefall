using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreSlimefall.Assist
{
    internal class LocalSpawnWeights
    {
        internal static Dictionary<IdentifiableType, float> _weightedRareFallingActors = new Dictionary<IdentifiableType, float>();
        internal static List<IdentifiableType> _cachedIdentifiableTypes = new List<IdentifiableType>();

        public static void Load(string sceneName)
        {
            switch (sceneName)
            {
                case "GameCore":
                    {
                        _cachedIdentifiableTypes.AddRange(new List<IdentifiableType>()
                        {
                            Get<SlimeDefinition>("Pink"),
                            Get<SlimeDefinition>("Yolky"),
                            Get<SlimeDefinition>("Lucky"),
                            Get<SlimeDefinition>("Gold"),
                        });

                        _weightedRareFallingActors = new()
                        {
                            {
                                _cachedIdentifiableTypes.FirstOrDefault(x => x.name.Equals("Pink")), 1
                            },
                            {
                                _cachedIdentifiableTypes.FirstOrDefault(x => x.name.Equals("Yolky")), 0.0004f
                            },
                            {
                                _cachedIdentifiableTypes.FirstOrDefault(x => x.name.Equals("Lucky")), 0.00025f
                            },
                            {
                                _cachedIdentifiableTypes.FirstOrDefault(x => x.name.Equals("Gold")), 0.00025f
                            }
                        };
                        break;
                    }
            }
        }
    }
}
