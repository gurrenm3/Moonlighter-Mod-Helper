using HarmonyLib;
using Moonlighter_Mod_Helper.Api;
using System;
using System.Linq;
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

            if (ThrowIfNameNullOrEmpty(name))
                return;

            // Remove spaces and try to find the name
            var clean = name.Replace(" ", "");
            var cleanedItem = ItemDatabase.Instance?.itemCollections[plusLevel]?.items.FirstOrDefault(i => i != null && i.name == clean);
            if (cleanedItem != null)
            {
                __result = cleanedItem;
                return;
            }

            // Check ItemRegister
            if (ItemRegister.TryGetItem<ConsumableItemMaster>(name, out var consumeMaster))
            {
                __result = consumeMaster;
                return;
            }

            if (ItemRegister.TryGetItem<WeaponEquipmentMaster>(name, out var weaponMaster))
            {
                __result = weaponMaster;
                return;
            }

            if (ItemRegister.TryGetItem<EquipmentItemMaster>(name, out var equipMaster))
            {
                __result = equipMaster;
                return;
            }

            if (ItemRegister.TryGetItem<ItemMaster>(name, out var itemMaster))
            {
                __result = itemMaster;
                return;
            }
        }

        private static bool ThrowIfNameNullOrEmpty(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                Main.LogWarning("Warning! Can't finish executing \"ItemDatabase.GetItemByName\"" +
                    " because itemName is null or empty");
                return true;
            }
            return false;
        }
    }
}