using Moonlighter_Mod_Helper.Api;
using UnityEngine;
using static EquipmentItemMaster;

namespace Moonlighter_Mod_Helper.Extensions
{
    public static class ItemMasterExt
    {
        public static GameObject GetPrefab(this ItemMaster itemMaster)
        {
            return ItemDatabase.GetPrefab(itemMaster.name);
        }

        public static Sprite GetSprite(this ItemMaster itemMaster)
        {
            return ItemDatabase.GetSprite(itemMaster);
        }

        public static bool HasItem(this ItemMaster itemMaster)
        {
            return ItemDatabase.HasItem(itemMaster.name);
        }

        public static bool IsFromDLC(this ItemMaster itemMaster)
        {
            return ItemDatabase.IsFromDLC(itemMaster);
        }

        public static bool IsFromDLC(this ItemMaster itemMaster, string dlcId)
        {
            return ItemDatabase.IsFromDLC(itemMaster, dlcId);
        }

        public static bool IsGenerated(this ItemMaster itemMaster)
        {
            return ItemDatabase.IsGenerated(itemMaster);
        }

        public static void UpgradeItemToPlusLevel(this ItemMaster itemMaster, int plusLevel)
        {
            ItemDatabase.UpgradeItemToPlusLevel(itemMaster, plusLevel);
        }

        public static ItemStack SpawnItem(this ItemMaster itemMaster, Vector3 position, float radius, Transform parent = null)
        {
            return new ItemDrop().SpawnItem(itemMaster.GetPrefab().name, position, radius, parent);
        }

        public static void SetItemName(this ItemMaster itemMaster, string newIdentifier, string name)
        {
            LocalizationHelper.AddTermData(newIdentifier, name);
            itemMaster.name = name;
            itemMaster.nameKey = newIdentifier;
        }

        public static void SetItemDescription(this ItemMaster itemMaster, string newIdentifier, string description)
        {
            LocalizationHelper.AddTermData(newIdentifier, description);
            itemMaster.description = description;
            itemMaster.descriptionKey = newIdentifier;
        }

        public static T DuplicateAs<T> (this ItemMaster itemMaster) where T : ItemMaster, new()
        {
            T newItem = new T();
            itemMaster.DuplicateAs(ref newItem);
            return newItem;
        }

        public static T DuplicateAs<T>(this ItemMaster itemMaster, ref T newItem) where T : ItemMaster
        {
            newItem.canAppearInChest = itemMaster.canAppearInChest;
            newItem.chestWeight = itemMaster.chestWeight;
            newItem.culture = itemMaster.culture;
            newItem.description = itemMaster.description;
            newItem.descriptionKey = itemMaster.descriptionKey;
            newItem.fixedChestStack = itemMaster.fixedChestStack;
            newItem.goldValue = itemMaster.goldValue;
            newItem.isDestroyedOnRunEnded = itemMaster.isDestroyedOnRunEnded;
            newItem.maxChestStack = itemMaster.maxChestStack;
            newItem.maxStack = itemMaster.maxStack;
            newItem.minChestStack = itemMaster.minChestStack;
            newItem.name = itemMaster.name;
            newItem.nameKey = itemMaster.nameKey;
            newItem.plusLevel = itemMaster.plusLevel;
            newItem.tier = itemMaster.tier;
            newItem.wandererWeaponGoldCost = itemMaster.wandererWeaponGoldCost;
            newItem.wandererWeaponSlimeCost = itemMaster.wandererWeaponSlimeCost;
            newItem.worldSpriteName = itemMaster.worldSpriteName;
            return newItem;
        }
    }
}
