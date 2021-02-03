using BepInEx;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Reflection;
using OpCodes = System.Reflection.Emit.OpCodes;

namespace DSP_Plugin
{
    [BepInPlugin("tv.watchingyour.plugins.PlanetChanger", "Planet Changer", "1.0.0.0")]
    public class PlanetChangerPlugin : BaseUnityPlugin
    {
        /// <summary>
        /// Called by harmony when it starts, applies the patch to the executable
        /// </summary>
        void Awake()
        {
            var harmony = new Harmony("tv.watchingyour.plugins.PlanetChanger");
            Harmony.CreateAndPatchAll(typeof(Patch));
        }

        [HarmonyPatch(typeof(StarGen))]
        private class Patch
        {
            /// <summary>
            /// Do a live edit on the IL code for the planet generation
            /// </summary>
            /// <param name="instructions">The raw IL instructions for the method</param>
            /// <returns>A modified stream of IL instructions</returns>
            [HarmonyPatch("CreateStarPlanets")]
            static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
            {
                // Put all the instructions in an array as we need to be able to peek forward
                CodeInstruction[] instructionArray = instructions.ToArray();

                // Get the field info for the planetCount field in the StarData class
                FieldInfo starInfo_planetCount = AccessTools.Field(typeof(StarData), "planetCount");

                // We have to skip the initializations for the following:
                // * Black hole
                // * Neutron Star
                // * White Dwarf
                // * Giant Star
                // This is due to them having fixed numbers of planets initialized and to fix would require more invasive changes
                int skipped = 0;

                // Loop through all the instructions
                for (int i = 0; i < instructionArray.Length; i++)
                {
                    // Get the current instruction
                    CodeInstruction instruction = instructionArray[i];

                    // Make sure it's not the last instruction and then check if it loads a constant
                    if (i != (instructionArray.Length - 1) && instruction.LoadsConstant())
                    {
                        // If a constant was loaded, then check if that constant is assigned to the planet count in the generator
                        if (instructionArray[i + 1].StoresField(starInfo_planetCount))
                        {
                            // We have to skip the first few assignements, detailed above with declaration of the variable
                            if(skipped < 7)
                            {
                                skipped++;
                            }
                            else
                            {
                                // If this is not one of the special cases, override the opcode of the IL instruction
                                // for the constant value to be 8 for every system
                                instruction.opcode = OpCodes.Ldc_I4_8;
                            }
                        }
                    }

                    // Add the instruction to the iEnumerable to return, modified if required
                    yield return instruction;
                }
            }
        }
    }
}
