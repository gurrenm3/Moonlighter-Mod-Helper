using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Moonlighter_Mod_Helper.Extensions
{
    public static class ItemMasterExt
    {
        public static ItemMaster Clone(this ItemMaster from, string newName, string newNameKey)
        {
            return ItemDatabase.CloneItem(newName, newNameKey, from);
        }

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

        /*public static void SetSprite(this ItemMaster itemMaster, Sprite newSprite)
        {
            itemMaster.SetSprite(newSprite, 0);
        }

        public static void SetSprite(this ItemMaster itemMaster, Sprite newSprite, int plusLevel)
        {
            foreach (ItemMaster item in ItemDatabase.Instance.itemCollections[plusLevel].items)
            {
                if (item.name != itemMaster.worldSpriteName)
                    continue;


            }
        }*/
    }
}
