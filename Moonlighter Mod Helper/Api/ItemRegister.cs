using Moonlighter_Mod_Helper.Api.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moonlighter_Mod_Helper.Api
{
    public class ItemRegister
    {
        public static ItemRegister Instance { get; set; } = new ItemRegister();
        public List<ItemMaster> ItemMasters { get; set; } = new List<ItemMaster>();
        
        internal Dictionary<string, Type> CustomItemTypes { get; set; } = new Dictionary<string, Type>();
        

        public static void RegisterItemType<T>() where T : Item, new()
        {
            var type = typeof(T);
            if (Instance.CustomItemTypes.ContainsValue(type))
            {
                Main.LogWarning($"Warning. Tried registering the custom item of type \"{type}\" more than once. Duplicate won't be registered");
                return;
            }

            var item = new T();
            item.RegisterSprite();
            Instance.CustomItemTypes.Add(item.Name, type);
        }


        public static bool TryGetItem<T>(string name, out T result) where T : ItemMaster
        {
            result = GetItem<T>(name);
            return (result != null);
        }

        public static T GetItem<T>(string name) where T : ItemMaster
        {
            return Instance.ItemMasters.FirstOrDefault(i => i.name == name) as T;
        }


        public static void AddItemMaster<T>(T itemMaster) where T : ItemMaster
        {
            var masters = Instance.ItemMasters;
            if (ThrowIfItemExists(itemMaster, masters))
                return;

            masters.Add(itemMaster);
        }

        private static bool ThrowIfItemExists<T>(ItemMaster itemMaster, List<T> list) where T : ItemMaster
        {
            if (list.FirstOrDefault(item => item.name == itemMaster.name) != null)
            {
                Main.LogWarning($"Tried adding adding a duplicate of the itemMaster \"{itemMaster.name}\". The duplicate will not be added.");
                return true;
            }

            return false;
        }
    }
}
