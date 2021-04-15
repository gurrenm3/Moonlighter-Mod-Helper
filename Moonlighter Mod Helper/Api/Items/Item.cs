using UnityEngine;
using Moonlighter_Mod_Helper.Extensions;
using System;

namespace Moonlighter_Mod_Helper.Api.Items
{
    public abstract class Item : MonoBehaviour
    {
        public static event EventHandler LoadItemIntoGame;
        internal static event EventHandler InternalLoadItemIntoGame;

        public ItemMaster itemMaster;

        public abstract string Name { get; set; }
        public abstract string NameKey { get; set; }
        public abstract string Description { get; set; }
        public abstract string DescriptionKey { get; set; }
        public abstract string SpriteKey { get; set; }
        public abstract string SpriteFilePath { get; set; }


        public Item()
        {
            
        }

        /*internal void OnRegisterItemType()
        {
            Console.WriteLine("========================= Registering Item Master");
            itemMaster = GetItemMaster();
            ItemRegister.AddItemMaster(itemMaster);
        }*/

        public virtual void RegisterSprite()
        {
            if (string.IsNullOrEmpty(SpriteKey) || string.IsNullOrEmpty(SpriteFilePath))
                return;

            if (!SpriteRegister.IsInRegister(SpriteKey))
            {
                var sprite = new Texture2D(32, 32).LoadFromFile(SpriteFilePath).CreateSpriteFromTexture(1);
                SpriteRegister.AddToRegister(SpriteKey, sprite);
            }
        }

        public ItemMaster GetItemMaster()
        {
            if (itemMaster != null)
                return itemMaster;

            if (ItemRegister.TryGetItem<ItemMaster>(Name, out var result))
            {
                itemMaster = result;
                return itemMaster;
            }

            itemMaster = ItemDatabase.GetItemByName("HP Potion I").Duplicate();
            itemMaster.SetItemName(NameKey, Name);
            itemMaster.SetItemDescription(DescriptionKey, Description);
            itemMaster.worldSpriteName = SpriteKey;

            ItemRegister.AddItemMaster(itemMaster);

            return itemMaster;
        }

        /*public ItemStack CreateItemStack()
        {
            return CreateItemStack(GetItemMaster());
        }*/

        public ItemStack CreateItemStack<T>(ItemMaster itemMaster) where T : Item
        {
            return CreateItemStack<T, ItemMaster>(itemMaster);
        }

        public ItemStack CreateItemStack<T>(EquipmentItemMaster equipmentItemMaster) where T : Item
        {
            return CreateItemStack<T, EquipmentItemMaster>(equipmentItemMaster);
        }

        protected ItemStack CreateItemStack<Type, Master>(Master itemMaster) where Type : Item where Master : ItemMaster, new()
        {
            var itemStack = ItemStack.Create(itemMaster);
            itemStack.gameObject.GetOrAddComponent<Type>();
            return itemStack;
        }


        /*public static ItemMaster GetItemMaster<T>() where T : Item, new()
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
        }*/

        internal static void InitCustomItems()
        {
            LoadItemIntoGame?.Invoke(null, EventArgs.Empty);
        }

        internal static void InitInternalCustomItems()
        {
            InternalLoadItemIntoGame?.Invoke(null, EventArgs.Empty);
        }
    }
}
