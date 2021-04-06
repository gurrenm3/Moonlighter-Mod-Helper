using HarmonyLib;
using Moonlighter_Mod_Helper.Api;
using System;
using UnityEngine;

namespace Moonlighter_Mod_Helper.Patches
{
    [HarmonyPatch(typeof(ItemDatabase), nameof(ItemDatabase.GetItemByName), new Type[] { typeof(string), typeof(int) })]
    internal class ItemDatabase_GetItemByName
    {
        [HarmonyPrefix]
        internal static bool Prefix(string name)
        {
            return true;
        }

        [HarmonyPostfix]
        internal static void Postfix(string name, int plusLevel, ref ItemMaster __result)
        {
            if (__result != null)
                return;

            if (string.IsNullOrEmpty(name))
            {
                Main.Log(BepInEx.Logging.LogLevel.Warning, "Warning! Can't finish executing \"ItemDatabase.GetItemByName\"" +
                    " because itemName is null or empty");
                return;
            }

            name = name.Replace(" ", "");
            foreach (ItemMaster itemMaster in ItemDatabase.Instance?.itemCollections[plusLevel]?.items)
            {
                if (itemMaster is null || itemMaster.name != name)
                    continue;

                Console.WriteLine("not null");
                __result = itemMaster;
                break;
            }
        }
    }
}