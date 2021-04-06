using HarmonyLib;
using Moonlighter_Mod_Helper.Api;
using UnityEngine;

namespace Moonlighter_Mod_Helper.Patches
{
    [HarmonyPatch(typeof(ItemDatabase), nameof(ItemDatabase.GetSpriteByItemName))]
    internal class ItemDatabase_GetSpriteByItemName
    {
        [HarmonyPrefix]
        internal static bool Prefix(string name, out Sprite __state)
        {
            __state = SpriteRegister.GetFromRegister(name, ignoreWarnings: true);
            return __state ? false : true;
        }

        [HarmonyPostfix]
        internal static void Postfix(ItemDatabase __instance, string name, Sprite __state, ref Sprite __result)
        {
            if (__state)
                __result = __state;

            if (__result || __state)
                return;

            foreach (var itemCollection in ItemDatabase.Instance.itemCollections)
            {
                var spriteByName = itemCollection.items?.FirstOrDefault(item => item.name == name);
                if (spriteByName == null)
                    continue;

                __result = ItemDatabase.GetSpriteByItemName(spriteByName.worldSpriteName);
                break;
            }
        }
    }
}