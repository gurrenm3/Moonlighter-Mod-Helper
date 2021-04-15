using HarmonyLib;
using Moonlighter_Mod_Helper.Api.Items;

namespace Moonlighter_Mod_Helper.Patches
{
    [HarmonyPatch(typeof(CultureManager), nameof(CultureManager.Init))]
    internal class CultureManager_Init
    {
        private static bool loadedCustomItems;

        [HarmonyPrefix]
        internal static bool Prefix(CultureManager __instance)
        {
            return true;
        }

        [HarmonyPostfix]
        internal static void Postfix(CultureManager __instance)
        {
            if (loadedCustomItems)
                return;

            /*Item.InitCustomItems();
            Item.InitInternalCustomItems();*/
            loadedCustomItems = true;
            Main.LogMessage("Custom Items loaded");
        }
    }
}
