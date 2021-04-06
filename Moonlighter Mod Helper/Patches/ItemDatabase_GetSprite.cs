using HarmonyLib;
using Moonlighter_Mod_Helper.Api;
using UnityEngine;

namespace Moonlighter_Mod_Helper.Patches
{
    [HarmonyPatch(typeof(ItemDatabase), nameof(ItemDatabase.GetSprite))]
    internal class ItemDatabase_GetSprite
    {
        [HarmonyPrefix]
        internal static bool Prefix(ItemMaster master, out Sprite __state)
        {
            __state = SpriteRegister.GetFromRegister(master.worldSpriteName, ignoreWarnings:true);
            return __state ? false : true;
        }

        [HarmonyPostfix]
        internal static void Postfix(ItemMaster master, Sprite __state, ref Sprite __result)
        {
            if (__state)
                __result = __state;
        }
    }
}