using HarmonyLib;

namespace Moonlighter_Mod_Helper.Patches
{
    [HarmonyPatch(typeof(MainMenuPanel), nameof(MainMenuPanel.Awake))]
    internal class MainMenuPanel_Enable
    {
        [HarmonyPostfix]
        internal static void Postfix(MainMenuPanel __instance)
        {

        }
    }
}