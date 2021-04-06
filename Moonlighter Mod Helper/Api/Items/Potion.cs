using Moonlighter_Mod_Helper.Extensions;
using System.Collections;
using UnityEngine;

namespace Moonlighter_Mod_Helper.Api.Items
{
    public abstract class Potion : CraftableItem, IConsumable
    {
        private static EquipmentItemMaster equipmentItemMaster;

        public override RecipeCollectionType RecipeCollectionType { get; set; } = RecipeCollectionType.Witch;
        public override string CollectionName { get; set; } = "potions";

        public abstract void CanConsume(ConsumableChecker consumableChecker);
        public abstract void Consume();

        public Potion()
        {
            
        }


        public float GetBonusFromNGP()
        {
            GameSlot currentGameSlot = GameManager.Instance.currentGameSlot;
            if (currentGameSlot.gameType != GameSlot.GameType.NewGamePlus)
                return 0f;

            string floatName = currentGameSlot.wasDlcCompleted ? "ngpPotionsHealFactor" : "ngpPostDLCPotionsHealFactor";
            return Constants.GetFloat(floatName);
        }

        public void PlayPotionSound()
        {
            HeroMerchant.Instance.heroMerchantController.PlayPotionSound();
        }

        public void DestroyEffects()
        {
            HeroMerchant.Instance.DestroyEffects();
        }


        public static EquipmentItemMaster GetEquipmentItemMaster<T>() where T : Potion, new()
        {
            if (equipmentItemMaster != null)
                return equipmentItemMaster;

            equipmentItemMaster = new EquipmentItemMaster();
            equipmentItemMaster = GetItemMaster<T>().ConvertTo<EquipmentItemMaster>();
            equipmentItemMaster.stats = new StatsModificator();
            equipmentItemMaster.equipmentSlot = EquipmentItemMaster.EquipmentSlot.Potion;
            ItemDatabaseHelper.AddToDatabase(equipmentItemMaster);

            return equipmentItemMaster;
        }

        public static new ItemStack CreateItemStack<T>() where T : Potion, new()
        {
            return CreateItemStack<T>(GetEquipmentItemMaster<T>());
        }

        public static new ItemStack CreateItemStack<T>(EquipmentItemMaster itemMaster) where T : Potion, new()
        {
            var itemStack = Item.CreateItemStack<T>(itemMaster);

            var consumableItemMaster = new ConsumableItemMaster();
            consumableItemMaster = itemStack.master.ConvertTo<ConsumableItemMaster>();
            itemStack.AddConsumable(consumableItemMaster);

            return itemStack;
        }
    }
}
