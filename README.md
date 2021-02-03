
# Dyson Sphere Program (DSP) Plugins

(Instructions based on [the README here](https://github.com/Touhma/DSP_Plugins)).

## Installation
First, add Bepinex to your DSP game folder: https://bepinex.github.io/bepinex_docs/master/articles/user_guide/installation/index.html?tabs=tabid-win
Then download the latest DLL from here: https://github.com/mushroom/DSP_Plugins/releases

Then add it to the Depinex plugins folder, which you created earlier as part of the installation procedure: `steamapps\common\Dyson Sphere Program\BepInEx\plugins`

## Compilation
To compile this, you require a `Libs` folder in the root directory, as per the BepInEx documentation. This needs to have the following files in it:
* `0Harmony.dll`
* `Assembly-CSharp.dll` (taken from the DSP managed folder)
* `BepInEx.dll`
* `BepInEx.Harmony.dll`
* `UnityEngine.CoreModule.dll` (taken from the DSP managed folder)
* `UnityEngine.dll` (taken from the DSP managed folder)

## Contribution
Pull requests and issues welcome, this is likey going to end up being somewhat of a rag-tag bunch of mods that do various things. Please discuss this with me on discord first, my username is CodIsAFish#6818, and you can find me on the [DSP Modding Discord Server](https://discord.gg/S7DekByDwY)
