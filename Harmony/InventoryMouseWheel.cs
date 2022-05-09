using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using UnityEngine;

public class EasyInventory : IModApi
{
    public void InitMod(Mod mod)
    {
        Debug.Log("Loading OCB Inventory Mouse Wheel Patch: " + GetType());
        var harmony = new Harmony(GetType().ToString());
        harmony.PatchAll(Assembly.GetExecutingAssembly());
    }
}
