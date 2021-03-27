using HarmonyLib;
using Moonlighter_Mod_Helper.Api;
using UnityEngine;

namespace Moonlighter_Mod_Helper.Patches
{
    [HarmonyPatch(typeof(ItemDatabase), nameof(ItemDatabase.GetSpriteByItemName))]
    internal class ItemDatabase_GetSpriteByItemName
    {
        [HarmonyPostfix]
        internal static void Postfix(string name, ref Sprite __result)
        {
            var sprite = SpriteRegister.GetFromRegister(name);
            if (sprite)
                __result = sprite;
        }
    }
}