using System;
using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using UnityEngine;

public class InventoryMouseWheel : IModApi
{
    static readonly Type TypeItemStack = AccessTools.TypeByName("Quartz.ItemStack");

    public void InitMod(Mod mod)
    {
        Debug.Log("Loading OCB Inventory Mouse Wheel Patch: " + GetType());
        var harmony = new Harmony(GetType().ToString());
        harmony.PatchAll(Assembly.GetExecutingAssembly());
    }
}
