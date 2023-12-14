# <img src="Files/GitHub/iconWeatherSlimerain.png" alt="Slimefall" width="90" align="left"/> More Slimefall

A **Slime Rancher 2 mod** that adds more to the "**Slimefall**" weather, such as tiers.
- **Download Link** : https://www.nexusmods.com/slimerancher2/mods/72

## Tiers

Here is information for the slimes that can rain during the newly added tiers for *each* zone.

The tiers are also named which will be used in the information provided.
- **Normal - Tier 1**
- **Moderate - Tier 2**
- **Severe - Tier 3**

There is also something new added to the mod that has a rare chance of happening during a **Severe** state.
When this occurs, you will receive a slimepedia entry on it.

<details>
<summary> <b>SPOILERS</b> </summary>
"<b>Slimefall Outbreak</b>". This can occur during a severe state of slimefall. More common in zones like starlight or ember.
<br><br>When this occurs, the clouds turn dark and Tarr start to rain from above. The pedia entry sounds cooler, read it when you can.
</details>

### Global

Global spawns are spawns that can occur in almost every zone at the same amount of time each time, unlike zoned spawns which can change depending on the zone.
*EX: Phosphor falls every 5 seconds for each zone, while a zoned version may fall every 10 seconds if changed.*

- **Phosphor (Moderate)**
- **Puddle (Severe, Tier 3 Rain)**
- **Tangle (Severe, Tier 3 Pollen)**
- **Dervish (Severe, Tier 3 Wind)**
- **Yolky (Severe, Rare)**
- **Gold (Severe, Rare)**
- **Lucky (Severe, Rare)**



### Rainbow Fields

#### Tier 1 - Normal

- **Pink**

#### Tier 2 - Moderate

- **Cotton**
- **Tabby**
- **All Moderate Global Spawns**

#### Tier 3 - Severe

- **All Severe Global Spawns**

### Starlight Strand

#### Tier 1 - Normal

- **Pink**
- **Rock**

#### Tier 2 - Moderate

- **Cotton**
- **Angler**
- **Honey**
- **All Moderate Global Spawns**

#### Tier 3 - Severe

- **Flutter**
- **Ringtail**
- **Hunter**
- **All Severe Global Spawns**

### Ember Valley

#### Tier 1 - Normal

- **Pink**
- **Tabby**

#### Tier 2 - Moderate

- **Crystal**
- **Boom**
- **Batty**
- **All Moderate Global Spawns**

#### Tier 3 - Severe
- **Rock**
- **Angler**
- **Ringtail**
- **Fire**
- **All Severe Global Spawns**

### Powderfall Bluffs

#### Tier 1 - Normal

- **Pink**
- **Cotton**

#### Tier 2 - Moderate

- **Rock**
- **Saber**
- **All Moderate Global Spawns**

#### Tier 3 - Severe

- **Hunter**
- **Crystal**
- **Boom**
- **All Severe Global Spawns**



## Extensions

If you're interested, there is support for creating "extensions" for the mod.
These extensions support adding something that could occur during one of the "Slimefall" tiers.

### Setup

The start of doing this is to of course create the project for your mod and set it all up.

Remember to reference the "More Slimefall" mod in your dependencies/references!
This also means that if your extension is published, it should **require** the "More Slimefall" mod to be installed in order for usage.

Once you've did that, create a new class *(perhaps in a new file)* and inherit the "[MoreSlimefallExtension](Extension/MoreSlimefallExtension.cs)" class.
This class is abstract and you should implement the required methods, feel free to make new ones for utility reasons mainly.

- **TIP** : "More Slimefall" has public utility for creating your extension inside of it's namespace, I recommend it for some extra code help.

### Loading

Once your extension is complete, go into your mod `OnSceneWasLoaded` and load the extension.
To do this, you have to use `ExtensionHelper` and either use `LoadExtension` or `LoadAllExtensions`.

The required parameters is either your `MelonAssembly` or an instance of your class that inherits `MoreSlimefallExtension`.
Including the `sceneName` parameter that comes with the `OnSceneWasLoaded` method that is overridden.

There is also an example/template that you could use if you'd like, linked below.
- [SlimefallExtensionExample](Examples/SlimefallExtensionExample.cs)

The abstract class also has documentation if you have the `.xml` file next to the `.dll` of the mod you referenced.


Thanks for using the mod if you did and have fun with making extensions if you wanted!