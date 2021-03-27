using HarmonyLib;
using Moonlighter_Mod_Helper.Api;
using UnityEngine;

namespace Moonlighter_Mod_Helper.Patches
{
    [HarmonyPatch(typeof(ItemDatabase), nameof(ItemDatabase.GetSprite))]
    internal class ItemDatabase_GetSprite
    {
        [HarmonyPostfix]
        internal static void Postfix(ItemMaster master, ref Sprite __result)
        {
            var sprite = SpriteRegister.GetFromRegister(master.worldSpriteName);
            if (sprite)
                __result = sprite;
        }
    }
}