using HarmonyLib;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class InventoryMouseWheel : IModApi
{

    public void InitMod(Mod mod)
    {
        Log.Out("OCB Harmony Patch: " + GetType().ToString());
        Harmony harmony = new Harmony(GetType().ToString());
        harmony.PatchAll(Assembly.GetExecutingAssembly());
    }


    // ****************************************************
    // Implementation to postpone our xml patching.
    // Allows us to run after our dependencies :-)
    // ****************************************************

    // Return in our load order
    [HarmonyPatch(typeof(ModManager))]
    [HarmonyPatch("GetLoadedMods")]
    public class ModManager_GetLoadedMods
    {
        static void Postfix(ref List<Mod> __result)
        {
            int myPos = -1, depPos = -1;
            if (__result == null) return;
            // Find position of mods we depend on
            for (int i = 0; i < __result.Count; i += 1)
            {
                switch (__result[i].Name)
                {
                    // case "SMXui":
                    case "Z_Ravenhearst_ResizedBackpack":
                        depPos = Mathf.Max(depPos, i + 1);
                        break;
                    case "OcbInventoryMouseWheel":
                        myPos = i;
                        break;
                }
            }
            // Didn't detect ourself?
            if (myPos == -1)
            {
                Log.Error("Did not detect our own Mod?");
                return;
            }
            // Detected no dependencies?
            if (depPos == -1) return;
            // Move our mod after deps
            var item = __result[myPos];
            __result.RemoveAt(myPos);
            if (depPos > myPos) depPos--;
            __result.Insert(depPos, item);
        }
    }

}
