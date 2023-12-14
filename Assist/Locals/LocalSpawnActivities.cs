using Il2CppMonomiPark.SlimeRancher.Weather.Activity;
using MoreSlimefall.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreSlimefall.Assist
{
    /// <summary>
    /// The <see cref="SpawnActorActivity"/>s that are created with the <see cref="MoreSlimefall"/> mod.<br></br>
    /// Use them if you'd like but make sure to check their <see cref="SpawnActivity.SecondsBetweenSpawns"/> in case you want something different.
    /// They're all setup on GameCore, so do not call before <see cref="MoreSlimefallExtension.OnGameCore"/> otherwise it could be null.
    /// </summary>
    public static class LocalSpawnActivities
    {
        // HASHSETS
        public static readonly List<SpawnActorActivity> _globalSpawnActivities = new List<SpawnActorActivity>();
        public static readonly List<SpawnActorActivity> _nighttimeSpawnActivities = new List<SpawnActorActivity>();

        // RANGES
        public static readonly Il2Cpp.Range ZERO = new Il2Cpp.Range() { Min = 0, Max = 0 };
        public static readonly Il2Cpp.Range ONE_TO_TWO = new Il2Cpp.Range() { Min = 1, Max = 2 };
        public static readonly Il2Cpp.Range TWO_TO_THREE = new Il2Cpp.Range() { Min = 2, Max = 3 };
        public static readonly Il2Cpp.Range THREE_TO_FIVE = new Il2Cpp.Range() { Min = 3, Max = 5 };
        public static readonly Il2Cpp.Range NINTY_TO_ONE_TWENTY = new Il2Cpp.Range() { Min = 90, Max = 120 };

        public static readonly Il2Cpp.Range EVERY_FIVE = new Il2Cpp.Range() { Min = 5, Max = 5 };
        public static readonly Il2Cpp.Range EVERY_TEN = new Il2Cpp.Range() { Min = 10, Max = 10 };
        public static readonly Il2Cpp.Range EVERY_TWENTY = new Il2Cpp.Range() { Min = 20, Max = 20 };
        public static readonly Il2Cpp.Range EVERY_THIRTY = new Il2Cpp.Range() { Min = 30, Max = 30 };
        public static readonly Il2Cpp.Range EVERY_SIXTY = new Il2Cpp.Range() { Min = 60, Max = 60 };

        // OUTBREAK
        public static SpawnActorActivity outbreakRainTarrSlimes;

        // GLOBAL
        public static SpawnActorActivity globalRainPinkSlimes;
        public static SpawnActorActivity globalRainPhosphorSlimes;

        public static SpawnActorActivity globalRainPuddleSlimes;
        public static SpawnActorActivity globalRainTangleSlimes;
        public static SpawnActorActivity globalRainDervishSlimes;

        public static SpawnActorActivity globalRainRareSlimes;

        /*internal static SpawnActorActivity globalRainYolkySlimes;
        internal static SpawnActorActivity globalRainLuckySlimes;
        internal static SpawnActorActivity globalRainGoldSlimes;*/

        // ZONED
        // -- FIELDS
        public static SpawnActorActivity fieldsRainCottonSlimes;
        public static SpawnActorActivity fieldsRainTabbySlimes;

        // -- STRAND
        public static SpawnActorActivity strandRainRockSlimes;

        public static SpawnActorActivity strandRainCottonSlimes;
        public static SpawnActorActivity strandRainAnglerSlimes;
        public static SpawnActorActivity strandRainHoneySlimes;

        public static SpawnActorActivity strandRainFlutterSlimes;
        public static SpawnActorActivity strandRainRingtailSlimes;
        public static SpawnActorActivity strandRainHunterSlimes;

        // -- VALLEY
        public static SpawnActorActivity valleyRainTabbySlimes;

        public static SpawnActorActivity valleyRainCrystalSlimes;
        public static SpawnActorActivity valleyRainBoomSlimes;
        public static SpawnActorActivity valleyRainBattySlimes;

        public static SpawnActorActivity valleyRainRockSlimes;
        public static SpawnActorActivity valleyRainAnglerSlimes;
        public static SpawnActorActivity valleyRainRingtailSlimes;
        public static SpawnActorActivity valleyRainFireSlimes;

        // -- BLUFFS
        public static SpawnActorActivity bluffsRainCottonSlimes;

        public static SpawnActorActivity bluffsRainRockSlimes;
        public static SpawnActorActivity bluffsRainSaberSlimes;

        public static SpawnActorActivity bluffsRainHunterSlimes;
        public static SpawnActorActivity bluffsRainCrystalSlimes;
        public static SpawnActorActivity bluffsRainBoomSlimes;

        internal static void Load(string sceneName)
        {
            switch (sceneName)
            {
                case "GameCore":
                    {
                        #region OUTBREAK
                        // TARR
                        outbreakRainTarrSlimes = CreateActivity(Get<SlimeDefinition>("Tarr"), "Outbreak Rain Tarr Slimes");
                        outbreakRainTarrSlimes.SecondsBetweenSpawns = ONE_TO_TWO;
                        #endregion

                        #region GLOBAL
                        // PINK
                        globalRainPinkSlimes = CreateActivity(Get<SlimeDefinition>("Pink"), "Global Rain Pink Slimes");

                        // PHOSPHOR
                        globalRainPhosphorSlimes = CreateActivity(Get<SlimeDefinition>("Phosphor"), "Global Rain Phosphor Slimes");
                        globalRainPhosphorSlimes.SecondsBetweenSpawns = EVERY_FIVE;

                        // TANGLE
                        globalRainPuddleSlimes = CreateActivity(Get<SlimeDefinition>("Puddle"), "Global Rain Puddle Slimes");
                        globalRainPuddleSlimes.SecondsBetweenSpawns = EVERY_THIRTY;

                        // TANGLE
                        globalRainTangleSlimes = CreateActivity(Get<SlimeDefinition>("Tangle"), "Global Rain Tangle Slimes");
                        globalRainTangleSlimes.SecondsBetweenSpawns = EVERY_THIRTY;

                        // DERVISH
                        globalRainDervishSlimes = CreateActivity(Get<SlimeDefinition>("Dervish"), "Global Rain Dervish Slimes");
                        globalRainDervishSlimes.SecondsBetweenSpawns = EVERY_THIRTY;

                        // RARE
                        globalRainRareSlimes = CreateActivity(null, "Global Rain Rare Slimes");
                        globalRainRareSlimes.SecondsBetweenSpawns = NINTY_TO_ONE_TWENTY;

                        /* YOLKY
                        globalRainYolkySlimes = CreateActivity(Get<SlimeDefinition>("Yolky"), "Global Rain Yolky Slimes");
                        globalRainYolkySlimes.SecondsBetweenSpawns = EVERY_THIRTY;

                        // LUCKY
                        globalRainLuckySlimes = CreateActivity(Get<SlimeDefinition>("Lucky"), "Global Rain Lucky Slimes");
                        globalRainLuckySlimes.SecondsBetweenSpawns = EVERY_SIXTY;

                        // GOLD
                        globalRainGoldSlimes = CreateActivity(Get<SlimeDefinition>("Gold"), "Global Rain Gold Slimes");
                        globalRainGoldSlimes.SecondsBetweenSpawns = EVERY_SIXTY;*/

                        // LIST OF GLOBAL ACTIVITIES
                        _globalSpawnActivities.AddRange(new List<SpawnActorActivity>()
                        {
                            globalRainPinkSlimes,
                            globalRainPhosphorSlimes,
                            globalRainPuddleSlimes,
                            globalRainTangleSlimes,
                            globalRainDervishSlimes,
                            globalRainRareSlimes
                            /*globalRainYolkySlimes,
                            globalRainLuckySlimes,
                            globalRainGoldSlimes*/
                        });
                        #endregion

                        #region ZONED

                        #region RAINBOW_FIELDS
                        // #MODERATE
                        // COTTON
                        fieldsRainCottonSlimes = CreateActivity(Get<SlimeDefinition>("Cotton"), "Fields Rain Cotton Slimes");
                        fieldsRainCottonSlimes.SecondsBetweenSpawns = THREE_TO_FIVE;

                        // TABBY
                        fieldsRainTabbySlimes = CreateActivity(Get<SlimeDefinition>("Tabby"), "Fields Rain Tabby Slimes");
                        fieldsRainTabbySlimes.SecondsBetweenSpawns = THREE_TO_FIVE;
                        #endregion

                        #region STARLIGHT_STRAND
                        // #NORMAL
                        // ROCK
                        strandRainRockSlimes = CreateActivity(Get<SlimeDefinition>("Rock"), "Strand Rain Rock Slimes");

                        // #MODERATE
                        // COTTON
                        strandRainCottonSlimes = CreateActivity(Get<SlimeDefinition>("Cotton"), "Strand Rain Cotton Slimes");
                        strandRainCottonSlimes.SecondsBetweenSpawns = THREE_TO_FIVE;

                        // ANGLER
                        strandRainAnglerSlimes = CreateActivity(Get<SlimeDefinition>("Angler"), "Strand Rain Angler Slimes");
                        strandRainAnglerSlimes.SecondsBetweenSpawns = EVERY_FIVE;

                        // HONEY
                        strandRainHoneySlimes = CreateActivity(Get<SlimeDefinition>("Honey"), "Strand Rain Honey Slimes");
                        strandRainHoneySlimes.SecondsBetweenSpawns = EVERY_FIVE;

                        // #SEVERE
                        // FLUTTER
                        strandRainFlutterSlimes = CreateActivity(Get<SlimeDefinition>("Flutter"), "Strand Rain Flutter Slimes");
                        strandRainFlutterSlimes.SecondsBetweenSpawns = EVERY_TEN;

                        // RINGTAIL
                        strandRainRingtailSlimes = CreateActivity(Get<SlimeDefinition>("Ringtail"), "Strand Rain Ringtail Slimes");
                        strandRainRingtailSlimes.SecondsBetweenSpawns = EVERY_TWENTY;

                        // HUNTER
                        strandRainHunterSlimes = CreateActivity(Get<SlimeDefinition>("Hunter"), "Strand Rain Hunter Slimes");
                        strandRainHunterSlimes.SecondsBetweenSpawns = EVERY_TWENTY;
                        #endregion

                        #region EMBER_VALLEY
                        // #NORMAL
                        // TABBY
                        valleyRainTabbySlimes = CreateActivity(Get<SlimeDefinition>("Tabby"), "Valley Rain Tabby Slimes");

                        // #MODERATE
                        // CRYSTAL
                        valleyRainCrystalSlimes = CreateActivity(Get<SlimeDefinition>("Crystal"), "Valley Rain Crystal Slimes");
                        valleyRainCrystalSlimes.SecondsBetweenSpawns = THREE_TO_FIVE;

                        // BOOM
                        valleyRainBoomSlimes = CreateActivity(Get<SlimeDefinition>("Boom"), "Valley Rain Boom Slimes");
                        valleyRainBoomSlimes.SecondsBetweenSpawns = EVERY_FIVE;

                        // BOOM
                        valleyRainBattySlimes = CreateActivity(Get<SlimeDefinition>("Batty"), "Valley Rain Batty Slimes");
                        valleyRainBattySlimes.SecondsBetweenSpawns = EVERY_FIVE;

                        // #SEVERE
                        // ROCK
                        valleyRainRockSlimes = CreateActivity(Get<SlimeDefinition>("Rock"), "Valley Rain Rock Slimes");
                        valleyRainRockSlimes.SecondsBetweenSpawns = EVERY_TEN;

                        // ANGLER
                        valleyRainAnglerSlimes = CreateActivity(Get<SlimeDefinition>("Angler"), "Valley Rain Angler Slimes");
                        valleyRainAnglerSlimes.SecondsBetweenSpawns = EVERY_TWENTY;

                        // RINGTAIL
                        valleyRainRingtailSlimes = CreateActivity(Get<SlimeDefinition>("Ringtail"), "Valley Rain Ringtail Slimes");
                        valleyRainRingtailSlimes.SecondsBetweenSpawns = EVERY_TWENTY;

                        // FIRE
                        valleyRainFireSlimes = CreateActivity(Get<SlimeDefinition>("Fire"), "Valley Rain Fire Slimes");
                        valleyRainFireSlimes.SecondsBetweenSpawns = EVERY_THIRTY;
                        #endregion

                        #region POWDERFALL_BLUFFS
                        // #NORMAL
                        // SABER
                        bluffsRainCottonSlimes = CreateActivity(Get<SlimeDefinition>("Cotton"), "Bluffs Rain Cotton Slimes");

                        // #MODERATE
                        // COTTON
                        bluffsRainRockSlimes = CreateActivity(Get<SlimeDefinition>("Rock"), "Bluffs Rain Rock Slimes");
                        bluffsRainRockSlimes.SecondsBetweenSpawns = THREE_TO_FIVE;

                        bluffsRainSaberSlimes = CreateActivity(Get<SlimeDefinition>("Saber"), "Bluffs Rain Saber Slimes");
                        bluffsRainSaberSlimes.SecondsBetweenSpawns = EVERY_FIVE;

                        // #SEVERE
                        // HUNTER
                        bluffsRainHunterSlimes = CreateActivity(Get<SlimeDefinition>("Hunter"), "Bluffs Rain Hunter Slimes");
                        bluffsRainHunterSlimes.SecondsBetweenSpawns = EVERY_TEN;

                        // CRYSTAL
                        bluffsRainCrystalSlimes = CreateActivity(Get<SlimeDefinition>("Crystal"), "Bluffs Rain Crystal Slimes");
                        bluffsRainCrystalSlimes.SecondsBetweenSpawns = EVERY_TWENTY;

                        // BOOM
                        bluffsRainBoomSlimes = CreateActivity(Get<SlimeDefinition>("Boom"), "Bluffs Rain Boom Slimes");
                        bluffsRainBoomSlimes.SecondsBetweenSpawns = EVERY_THIRTY;
                        #endregion

                        #endregion

                        _nighttimeSpawnActivities.AddRange(new List<SpawnActorActivity>()
                        {
                            globalRainPhosphorSlimes,
                            strandRainRingtailSlimes,
                            valleyRainRingtailSlimes
                        });
                        break;
                    }
            }
        }

        public static SpawnActorActivity CreateActivity(IdentifiableType identifiable, string name)
        {
            SpawnActorActivity baseSpawnActivity = Get<SpawnActorActivity>("Rain Pink Slimes");
            SpawnActorActivity spawnActivity = ScriptableObject.CreateInstance<SpawnActorActivity>();
            spawnActivity.hideFlags |= HideFlags.HideAndDontSave;
            spawnActivity.name = name;
            spawnActivity.ActorType = identifiable;

            spawnActivity.ConnectedFX = baseSpawnActivity.ConnectedFX;
            spawnActivity.SpawnStrategy = baseSpawnActivity.SpawnStrategy;
            spawnActivity.ShouldConnectedFX = true;
            spawnActivity.SecondsBetweenSpawns = TWO_TO_THREE;
            return spawnActivity;
        }
    }
}
