using FullSerializer;
using UnityEngine;

namespace Moonlighter_Mod_Helper.Api
{
    public class ItemDatabaseHelper
	{
		// Copied from ItemDatabase.CloneItem
        public static void AddToDatabase(ItemMaster itemToAdd)
        {
			var cultureInfo = CultureManager.Instance.GetCultureInfo(itemToAdd.culture);
			if (cultureInfo._discoveries.FirstOrDefault(discover => discover.master == itemToAdd) != null)
            {
				Main.Log(BepInEx.Logging.LogLevel.Warning, $"Warning! Tried adding one or more duplicates of the " +
					$"item \"{itemToAdd.name}\" to the Item Database. Duplicate items won't be added.");
				return;
			}
			
			cultureInfo.AddItem(itemToAdd);
			ItemDatabase.Instance.itemCollections[0].items.Add(itemToAdd);
			//ItemDatabase.Instance.itemCollections[0].createdItems.Add(itemToAdd); //removed because it was preventing custom items from showing in the Notebook

			FsJSONManager.Serialize<ItemMaster>(itemToAdd, out fsData data);
			for (int i = 1; i < ItemDatabase.Instance.itemCollections.Length; i++)
			{
				ItemMaster item = new ItemMaster();
				FsJSONManager.Deserialize<ItemMaster>(fsJsonPrinter.CompressedJson(data), ref item, false);
				ItemDatabase.UpgradeItemToPlusLevel(item, i);

				ItemDatabase.Instance.itemCollections[i].items.Add(itemToAdd);
				//ItemDatabase.Instance.itemCollections[i].createdItems.Add(itemToAdd);  //same as above
			}
		}


		public static Sprite GetSpriteByItemName(string itemName)
        {
			int plusLevel = GameManager.Instance.GetCurrentGamePlusLevel();
			return GetSpriteByItemName(itemName, plusLevel);
		}

		public static Sprite GetSpriteByItemName(string itemName, int plusLevel)
		{
			var items = ItemDatabase.Instance.itemCollections[plusLevel].items;
			var itemMaster = items.FirstOrDefault(item => item.name == itemName);
			return ItemDatabase.GetSprite(itemMaster);
		}


		public static Sprite GetSpriteByWorldSpriteName(string worldSpriteName)
		{
			int plusLevel = GameManager.Instance.GetCurrentGamePlusLevel();
			return GetSpriteByWorldSpriteName(worldSpriteName, plusLevel);
		}

		public static Sprite GetSpriteByWorldSpriteName(string worldSpriteName, int plusLevel)
		{
			var items = ItemDatabase.Instance.itemCollections[plusLevel].items;
			var itemMaster = items.FirstOrDefault(item => item.worldSpriteName == worldSpriteName);
			return ItemDatabase.GetSprite(itemMaster);
		}
	}
}
