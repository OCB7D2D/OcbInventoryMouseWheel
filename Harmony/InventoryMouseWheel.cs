using HarmonyLib;
using System.Reflection;

public class InventoryMouseWheel : IModApi
{

    public void InitMod(Mod mod)
    {
        Log.Out("OCB Harmony Patch: " + GetType().ToString());
        Harmony harmony = new Harmony(GetType().ToString());
        harmony.PatchAll(Assembly.GetExecutingAssembly());
        ReOrderMods();
    }


    // ****************************************************
    // Implementation to postpone our xml patching.
    // Allows us to run after our dependencies :-)
    // ****************************************************
    private void ReOrderMods()
    {
        var list = ModManager.loadedMods.list;
        int myPos = -1, depPos = -1;
        if (list == null) return;
        // Find position of mods we depend on
        for (int i = 0; i < list.Count; i += 1)
        {
            switch (list[i].Name)
            {
                case "SMXui":
                    depPos = i + 1;
                    break;
                case "OcbInventoryMouseWheel":
                case "OcbInventoryMouseWheelAL":
                case "OcbInventoryMouseWheelSMX":
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
        var item = list[myPos];
        list.RemoveAt(myPos);
        if (depPos > myPos) depPos--;
        list.Insert(depPos, item);
    }

}
