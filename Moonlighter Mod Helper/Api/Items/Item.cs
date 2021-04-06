using UnityEngine;
using Moonlighter_Mod_Helper.Extensions;

namespace Moonlighter_Mod_Helper.Api.Items
{
    public abstract class Item : MonoBehaviour
    {
        private static ItemMaster itemMaster;

        public abstract string Name { get; set; }
        public abstract string NameKey { get; set; }
        public abstract string Description { get; set; }
        public abstract string DescriptionKey { get; set; }
        public abstract string SpriteKey { get; set; }
        public abstract string SpriteFilePath { get; set; }


        public Item()
        {
            SetSprite();
        }

        public virtual void SetSprite()
        {
            if (string.IsNullOrEmpty(SpriteKey) || string.IsNullOrEmpty(SpriteFilePath))
                return;

            if (!SpriteRegister.IsInRegister(SpriteKey))
            {
                var sprite = new Texture2D(32, 32).LoadFromFile(SpriteFilePath).CreateSpriteFromTexture(1);
                
                SpriteRegister.AddToRegister(SpriteKey, sprite);
            }
        }


        public static ItemMaster GetItemMaster<T>() where T : Item, new()
        {
            if (itemMaster != null)
                return itemMaster;

            var potion = new T();

            itemMaster = ItemDatabase.GetItemByName("HP Potion I").Duplicate();
            itemMaster.SetItemName(potion.NameKey, potion.Name);
            itemMaster.SetItemDescription(potion.DescriptionKey, potion.Description);
            itemMaster.worldSpriteName = potion.SpriteKey;

            return itemMaster;
        }

        public static ItemStack CreateItemStack<T>() where T : Item, new()
        {
            return CreateItemStack<T, ItemMaster>(GetItemMaster<T>());
        }

        public static ItemStack CreateItemStack<T>(ItemMaster itemMaster) where T : Item, new()
        {
            return CreateItemStack<T, ItemMaster>(itemMaster);
        }

        public static ItemStack CreateItemStack<T>(EquipmentItemMaster equipmentItemMaster) where T : Item, new()
        {
            return CreateItemStack<T, EquipmentItemMaster>(equipmentItemMaster);
        }

        private static ItemStack CreateItemStack<Type, Master>(Master itemMaster) where Type : Item  where Master : ItemMaster, new()
        {
            var itemStack = ItemStack.Create(itemMaster);
            itemStack.gameObject.GetOrAddComponent<Type>();
            return itemStack;
        }
    }
}
